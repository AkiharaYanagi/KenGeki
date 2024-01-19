using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Drawing;

namespace ScriptEditor
{
	//==================================================================================
	//	SaveChara
	//		キャラのスクリプトをDocument形式、
	//		イメージアーカイブをpngのbinaryで、
	//		.datファイルに保存する
	//==================================================================================
	public partial class SaveChara
	{
		//-------------------------------------------------------------
		//コンストラクタ
		//引数　filename : 保存ファイル名, chara : 保存対象キャラ
		//-------------------------------------------------------------
		public SaveChara ( string filename, Chara chara )
		{
			//データが無かったら何もしない
			if ( chara == null ) { return; }

			//キャラスクリプトをテキストメモリストリームに変換
			MemoryStream mstrmChara = GetTextMemoryStream ( chara );

			//ストリームリーダにセット
			StreamReader strmReaderChara = new StreamReader ( mstrmChara );

			//==========================================================================
			//確認用テキストファイル(.txt)書出

			//ファイル名の拡張子を.datから.txtに変える
			string filenameTxt = Path.GetFileNameWithoutExtension ( filename ) + ".txt";

			//ファイルにテキスト書出用ストリームを設定する
			StreamWriter strmWriter = new StreamWriter ( filenameTxt, false, Encoding.UTF8 );

			//書出
			mstrmChara.Seek ( 0, SeekOrigin.Begin );			//先頭から
			strmWriter.Write ( strmReaderChara.ReadToEnd () );	//最後まで書出し
			strmWriter.Close ();


			//==========================================================================
			//(スクリプト + イメージ) メモリストリーム書出

			//------------------------------
			//スクリプト部
			//メモリストリーム
			MemoryStream mstrmScript = new MemoryStream ();
			BinaryWriter biWriterScript = new BinaryWriter ( mstrmScript );

			//キャラスクリプトを先頭から読み、ファイル書出用にバイナリで書き込む
			mstrmChara.Seek ( 0, SeekOrigin.Begin );
			BinaryReader biReaderChara = new BinaryReader ( mstrmChara );

			//byte毎に読み込む
			int buf = 0;
			while ( -1 != ( buf = biReaderChara.Read () ) )
			{
				biWriterScript.Write ( ( byte ) buf );
			}

			biWriterScript.Flush ();

			//------------------------------
			//イメージバイナリデータ部
			//メモリストリーム
			MemoryStream mstrmImage = new MemoryStream ();
			BinaryWriter biWriterImage = new BinaryWriter ( mstrmImage );

			WriteListImage ( biWriterImage, chara.behavior.ListImage );
			WriteListImage ( biWriterImage, chara.garnish.ListImage );

			biWriterImage.Flush ();

			//==========================================================================
			//(スクリプト + イメージ) ファイル(.dat)書出

			//書出対象ファイル
			FileStream fstrm = new FileStream ( filename, FileMode.Create, FileAccess.Write );
			BinaryWriter biWriterFile = new BinaryWriter ( fstrm, Encoding.ASCII );

			//ストリーム読書用一時領域
			const int size = 4096;	//バッファサイズ
			byte[] buffer = new byte[ size ];	//バッファ
			int numBytes;		//バイト数

			//ver書出
			biWriterFile.Write ( ( uint ) CONST.VER );

			//スクリプト部・サイズ書出
			biWriterFile.Write ( ( uint ) mstrmScript.Length );

			//スクリプト部・ストリーム書出
			mstrmScript.Seek ( 0, SeekOrigin.Begin );
			while ( ( numBytes = mstrmScript.Read ( buffer, 0, size ) ) > 0 )
			{
				biWriterFile.Write ( buffer, 0, numBytes );
			}

			//イメージ部・ストリーム書出
			mstrmImage.Seek ( 0, SeekOrigin.Begin );
			while ( ( numBytes = mstrmImage.Read ( buffer, 0, size ) ) > 0 )
			{
				biWriterFile.Write ( buffer, 0, numBytes );
			}

			Debug.WriteLine ( "fstrm.Length = " + fstrm.Length );

			//終了
			biWriterFile.Close ();
			biWriterScript.Close ();
			biWriterImage.Close ();
			strmReaderChara.Close ();
		}


		//キャラからをスクリプトの対象となる値を読み込み、
		//キャラスクリプトとしてテキストメモリストリームに変換
		private MemoryStream GetTextMemoryStream ( Chara chara )
		{
			MemoryStream mstrm = new MemoryStream ();
			StreamWriter strmWriter = new StreamWriter ( mstrm, Encoding.UTF8 );

			//-------------------------------------------------------
			//全体タグとバージョン
			strmWriter.Write ( "<Chara>\n" );
			strmWriter.Write ( "<ver>" + CONST.VER + "</ver>\n\n" );

			//-------------------------------------------------------
			//イメージリスト
			ImageList imageList = chara.behavior.ListImage;
			strmWriter.Write ( "<ImageList Num=\"" + imageList.GetBindingList().Count + "\">\n" );
			foreach ( ImageData imageData in imageList.GetBindingList() )
			{
				strmWriter.Write ( "\t<Image Name=\"" + imageData.Name + "\"></Image>\n" );
			}
			strmWriter.Write ( "</ImageList>\n" );

			//-------------------------------------------------------
			//Efイメージリスト
			ImageList efImageList = chara.garnish.ListImage;
			strmWriter.Write ( "<EfImageList Num=\"" + efImageList.GetBindingList().Count + "\">\n" );
			foreach ( ImageData imageData in efImageList.GetBindingList () )
			{
				strmWriter.Write ( "\t<EfImage Name=\"" + imageData.Name + "\"></EfImage>\n" );
			}
			strmWriter.Write ( "</EfImageList>\n" );

			//-------------------------------------------------------
			//アクションリスト
			strmWriter.Write ( "<ActionList Num=\"" + chara.behavior.Bldct_sqc.GetBindingList().Count + "\">\n" );

			//アクション
			foreach ( Action action in chara.behavior.Bldct_sqc.GetBindingList() )
			{
				//attribute値はダブルクォーテーションで囲む
				strmWriter.Write ( "\t<Action" );
				strmWriter.Write ( " Name=\"" + action.Name + "\"" );				//名前
				strmWriter.Write ( " NextIndex=\"" + action.NextIndex + "\"" );		//次アクションID
				strmWriter.Write ( " Category=\"" + (int)action.Category + "\"" );		//アクション属性
				strmWriter.Write ( " Posture=\"" + (int)action.Posture + "\"" );			//アクション体勢
				strmWriter.Write ( " Balance=\"" + action._Balance + "\"" );		//消費バランス値
				strmWriter.Write ( ">\n" );

				//スクリプト
				WriteListScript ( strmWriter, chara, action.ListScript );

				strmWriter.Write ( "\t</Action>\n" );
			}
			strmWriter.Write ( "</ActionList>\n" );

			//-------------------------------------------------------
			//エフェクトリスト
			strmWriter.Write ( "<EfList Num=\"" + chara.garnish.Bldct_sqc.GetBindingList().Count + "\">\n" );

			//エフェクト
			foreach ( Effect effect in chara.garnish.Bldct_sqc.GetBindingList() )
			{
				//attribute値はダブルクォーテーションで囲む
				strmWriter.Write ( "\t<Effect" );
				strmWriter.Write ( " Name=\"" + effect.Name + "\"" );	//名前
				strmWriter.Write ( ">\n" );

				//スクリプト
				WriteListScript ( strmWriter, chara, effect.ListScript );

				strmWriter.Write ( "\t</Effect>\n" );
			}
			strmWriter.Write ( "</EfList>\n" );

			//-------------------------------------------------------
			//コマンドリスト
			strmWriter.Write ( "<CommandList Num=\"" + chara.ListCommand.Count + "\">\n" );
			//コマンド
			foreach ( Command command in chara.ListCommand )
			{
				strmWriter.Write ( "\t<Command Name=\"" + command.Name + "\"" );
				strmWriter.Write ( " Limit=\"" + command.LimitTime.ToString () + "\">\n" );

				//キー
				foreach ( GameKeyCommand gameKey in command.ListGameKeyCommand )
				{
					strmWriter.Write ( "\t\t<Key" );
					
					//否定
					strmWriter.Write ( " Not=\"" + gameKey.Not.ToString () + "\"" );

					//レバー
#if false

					for ( int i = 0; i < GameKeyCommand.LeverCommandNum; ++ i )
					{
						strmWriter.Write ( " Key_" + i.ToString() + "=\"" );
						strmWriter.Write ( gameKey.Lvr[i].ToString () + "\"" );
					}
#endif
					strmWriter.Write ( " IdLvr =\"" );
					strmWriter.Write ( gameKey.IdLvr + "\"" );
					strmWriter.Write ( " Lvr =\"" + gameKey.Lvr[gameKey.IdLvr].ToString () + "\"" );

					//ボタン
					for ( int i = 0; i < GameKeyCommand.BtnNum; ++ i )
					{
						strmWriter.Write ( " Btn_" + i.ToString () + " =\"" + gameKey.Btn[ i ].ToString () + "\"" );
					}
					strmWriter.Write ( ">" );

					strmWriter.Write ( "</Key>\n" );
				}
				strmWriter.Write ( "\t</Command>\n" );
			}
			strmWriter.Write ( "</CommandList>\n" );


			//-------------------------------------------------------
			//基本状態アクションID
#if false
			strmWriter.Write ( "<BasicAction>\n" );

			int indexSpAction = 0;
			string[] enumnames = Enum.GetNames ( typeof ( Chara.BasicAction ) );
			foreach ( string str in enumnames )
			{
				strmWriter.Write ( "\t<" + str );
				strmWriter.Write ( " ID=\"" + chara.BsAction[ indexSpAction ].i + "\"" );
				strmWriter.Write ( " Name=\"" + chara.behavior.Bldct_sqc.GetBindingList()[ chara.BsAction[ indexSpAction ].i ].ToString () + "\"" );
				strmWriter.Write ( "></" + str + ">\n" );
				++indexSpAction;
			}

			strmWriter.Write ( "</BasicAction>\n" );
#endif

			//-------------------------------------------------------
			//全体タグ
			strmWriter.Write ( "</Chara>\n" );

			//終了
			strmWriter.Flush ();

			return mstrm;
		}


		//イメージリストの書出
		private void WriteListImage ( BinaryWriter biWriterImage, ImageList imagelist )
		{
			//ストリーム読込→書出用
			const int size = 4096;	//バッファサイズ
			byte[] buffer = new byte[ size ];	//バッファ
			int numBytes;		//バイト数

			//イメージリスト
			IEnumerable list =  imagelist.GetBindingList ();
			foreach ( ImageData imageData in list )
			{
				//各イメージを一時領域に書出
				MemoryStream tempMstrm = new MemoryStream ();
				imageData.Img.Save ( tempMstrm, System.Drawing.Imaging.ImageFormat.Png );

				//サイズの書込
				biWriterImage.Write ( ( uint ) tempMstrm.Length );

				Debug.Write ( imageData.Name + ", " + ( uint ) tempMstrm.Length );

				//実データの書込
				tempMstrm.Seek ( 0, SeekOrigin.Begin );
				int sumBytes = 0;
				while ( ( numBytes = tempMstrm.Read ( buffer, 0, size ) ) > 0 )
				{
					biWriterImage.Write ( buffer, 0, numBytes );
					sumBytes += numBytes;
				}

				Debug.Write ( " : sumBytes = " + sumBytes + "\n" );

				tempMstrm.Close ();
			}

		}

		//スクリプトの書出
		private void WriteListScript ( StreamWriter strmWriter, Chara chara, List<Script> listScript )
		{
			//スクリプト
			foreach ( Script script in listScript )
			{
				strmWriter.Write ( "\t\t<Script" );
				strmWriter.Write ( " group=\"" + script.Group + "\"" );
//				strmWriter.Write ( " image=\"" + script.ImgIndex + "\"" );
				strmWriter.Write ( " Image Name=\"" + script.ImgName + "\"" );
				strmWriter.Write ( " X=\"" + script.Pos.X + "\" Y=\"" + script.Pos.Y + "\"" );
				strmWriter.Write ( " VX=\"" + script.Vel.X + "\" VY=\"" + script.Vel.Y + "\"" );
				strmWriter.Write ( " AX=\"" + script.Acc.X + "\" AY=\"" + script.Acc.Y + "\"" );
				strmWriter.Write ( " CLC_ST=\"" + (int)script.CalcState + "\"" );
				strmWriter.Write ( " power=\"" + script.Power + "\"" );
				strmWriter.Write ( ">\n" );

				//ブランチリスト
				strmWriter.Write ( "\t\t\t<BranchList Num=\"" + script.ListBranch.Count + "\">\n" );
				//ブランチ
				foreach ( Branch branch in script.ListBranch )
				{
					strmWriter.Write ( "\t\t\t\t<Branch" );
					strmWriter.Write ( " Command=\"" + branch.IndexCommand + "\"" );
					strmWriter.Write ( " CommandName=\"" + chara.ListCommand[ branch.IndexCommand ] + "\"" );
					strmWriter.Write ( " Action=\"" + branch.IndexAction + "\"" );
					strmWriter.Write ( " ActionName=\"" + chara.behavior.Bldct_sqc.GetBindingList()[ branch.IndexAction ] + "\"" );
					strmWriter.Write ( " Frame=\"" + branch.Frame + "\"" );
					strmWriter.Write ( "></Branch>\n" );
				}
				strmWriter.Write ( "\t\t\t</BranchList>\n" );


				//Efジェネレートリスト
				strmWriter.Write ( "\t\t\t<EfGenerateList Num=\"" + script.ListGenerateEf.Count + "\">\n" );
				//Efジェネレート
				foreach ( EffectGenerate efGnrt in script.ListGenerateEf )
				{
					strmWriter.Write ( "\t\t\t\t<EfGenerate" );
					strmWriter.Write ( " EfName=\"" + efGnrt.Name + "\"" );
					strmWriter.Write ( " EfId=\"" + efGnrt.Id + "\"" );
					strmWriter.Write ( " EfGnrt_PtX=\"" + efGnrt.Pt.X + "\"" );
					strmWriter.Write ( " EfGnrt_PtY=\"" + efGnrt.Pt.Y + "\"" );
					strmWriter.Write ( " EfGnrt_PtZ=\"" + efGnrt.Z + "\"" );
					strmWriter.Write ( " Gnrt=\"" + efGnrt.Gnrt + "\"" );
					strmWriter.Write ( " Loop=\"" + efGnrt.Loop + "\"" );
					strmWriter.Write ( " Sync=\"" + efGnrt.Sync + "\"" );
					strmWriter.Write ( "></EfGenerate>\n" );
				}
				strmWriter.Write ( "\t\t\t</EfGenerateList>\n" );

				WriteListRect ( strmWriter, script.ListCRect, "CollisionRect" );	//ぶつかり枠リスト
				WriteListRect ( strmWriter, script.ListARect, "AttackRect" );		//攻撃枠リスト
				WriteListRect ( strmWriter, script.ListHRect, "HitRect" );			//当り枠リスト
				WriteListRect ( strmWriter, script.ListORect, "OffsetRect" );		//相殺枠リスト

				strmWriter.Write ( "\t\t</Script>\n" );
			}
		}

		//枠の書出	
//		private void WriteListRect ( StreamWriter strmWriter, List<RefRect> listRefRect, string name )
		private void WriteListRect ( StreamWriter strmWriter, List<Rectangle> lsRect, string name )
		{
			//枠リスト
			strmWriter.Write ( "\t\t\t<" + name + "List Num=\"" + lsRect.Count + "\">\n" );
			//枠
			foreach ( Rectangle rect in lsRect )
			{
				strmWriter.Write ( "\t\t\t\t<" + name );
				strmWriter.Write ( " X=\"" + rect.X + "\"" );
				strmWriter.Write ( " Y=\"" + rect.Y + "\"" );
				strmWriter.Write ( " W=\"" + rect.Width + "\"" );
				strmWriter.Write ( " H=\"" + rect.Height + "\"" );
				strmWriter.Write ( "></" + name + ">\n" );
			}
			strmWriter.Write ( "\t\t\t</" + name + "List>\n" );

		}
	}
}
