using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Collections;

namespace ScriptEditor
{
	//==================================================================================
	//	LoadChara
	//		.datファイルからCharaスクリプトとイメージを読み込む
	//==================================================================================
	public partial class LoadChara
	{
		public LoadChara ( string filename, Chara chara )
		{
			try
			{
				_Load ( filename, chara );
			}
			catch ( ArgumentException e )
			{
				//仮データ
//				TestChara testChara = new TestChara ( chara, EditChara );

				MessageBox.Show ( "LoadChara : 読込データが不適正です\n" + e.Message + "\n" + e.StackTrace );
			}
		}


		//スクリプトを読み込み、Document形式でキャラに保存
		//Charaを指定ドキュメントファイルからロード
		private void _Load ( string filename, Chara chara )
		{
			//ファイルが存在しないとき何もしない
			if ( ! File.Exists ( filename ) )
			{
				MessageBox.Show ( filename + "が見つかりません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error );
				throw new ArgumentException ( "ファイルが存在しませんでした。" );
			}

			//拡張子確認
			if ( Path.GetExtension ( filename ).CompareTo ( ".dat" ) != 0 )
			{
				MessageBox.Show ( filename + "は拡張子が.datと異なります。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error );
				throw new ArgumentException ( "拡張子が.datと異なります。" );
			}

			//初期化
			chara.Clear ();

			//ファイルストリーム開始
			FileStream fstrm = new FileStream ( filename, FileMode.Open, FileAccess.Read );
			BinaryReader biReaderFile = new BinaryReader ( fstrm, Encoding.ASCII );

			Debug.WriteLine ( "fstrm.Length = " + fstrm.Length );

			//==========================================================================
			//スクリプト部分のメモリストリームを作成
			MemoryStream mstrmScript = new MemoryStream ();
			StreamWriter strmWrtScript = new StreamWriter ( mstrmScript, Encoding.ASCII );
			fstrm.Seek ( 0, SeekOrigin.Begin );

			//バージョンの取得
			uint version = biReaderFile.ReadUInt32 ();
			if ( CONST.VER != version )
			{
				MessageBox.Show ( filename + "はバージョンが " + version + "で" + CONST.VER + " と異なります。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error );
				throw new ArgumentException ( "バージョンが異なります。" );
			}

			//スクリプトサイズの取得
			uint sizeScript = biReaderFile.ReadUInt32 ();
			byte[] bufScript = new byte[ sizeScript ];
			biReaderFile.Read ( bufScript, 0, ( int ) sizeScript );
			mstrmScript.Write ( bufScript, 0, ( int ) sizeScript );
			mstrmScript.Seek ( 0, SeekOrigin.Begin );

			//ドキュメントの読込
			Document document = new Document ( mstrmScript );

			strmWrtScript.Close ();
//			mstrmScript.Close ();
			
			//ドキュメント型からキャラへスクリプト部を変換
			DocToChara dtoc = new DocToChara ();
			dtoc.Load ( document, chara );


			//==========================================================================
			//イメージバイナリデータ読込
#if false
			//メインイメージ部 読込
			IEnumerable list =  chara.behavior.ListImage.GetBindingList ();

			foreach ( ImageData imgdt in list )
			{
				//uint固定長でイメージサイズ読込
				uint imageSize = biReaderFile.ReadUInt32 ();

				//実データ読込
				byte[] imageBuffer = new byte[ imageSize ];
				biReaderFile.Read ( imageBuffer, 0, ( int ) imageSize );

				//メモリストリームに一時書出
				MemoryStream mstrmImage = new MemoryStream ();
				mstrmImage.Write ( imageBuffer, 0, ( int ) imageSize );

				//イメージ型の作成
				imgdt.Img = Image.FromStream ( mstrmImage );
			}

			//※ 読込位置はそのまま次へ

			//Efイメージ部 読込
			IEnumerable eflist =  chara.garnish.ListImage.GetBindingList ();
			foreach ( ImageData imgdt in eflist )
			{
				//uint固定長でイメージサイズ読込
				uint imageSize = biReaderFile.ReadUInt32 ();

				//実データ読込
				byte[] imageBuffer = new byte[ imageSize ];
				biReaderFile.Read ( imageBuffer, 0, ( int ) imageSize );

				//メモリストリームに一時書出
				MemoryStream mstrmImage = new MemoryStream ();
				mstrmImage.Write ( imageBuffer, 0, ( int ) imageSize );

				//イメージ型の作成
				imgdt.Img = Image.FromStream ( mstrmImage );
			}
#endif
			LoadImageList ( biReaderFile, chara.behavior.ListImage );
			//※ 読込位置はそのまま次へ
			LoadImageList ( biReaderFile, chara.garnish.ListImage );

			//==========================================================================
			//終了
			biReaderFile.Close ();
//			fstrm.Close ();
		}

		//イメージリストの読込
		private void LoadImageList ( BinaryReader br, ImageList imagelist )
		{
			IEnumerable list =  imagelist.GetBindingList ();
			foreach ( ImageData imgdt in list )
			{
				//uint固定長でイメージサイズ読込
				uint imageSize = br.ReadUInt32 ();

				//実データ読込
				byte[] imageBuffer = new byte[ imageSize ];
				br.Read ( imageBuffer, 0, ( int ) imageSize );

				//メモリストリームに一時書出
				MemoryStream mstrmImage = new MemoryStream ();
				mstrmImage.Write ( imageBuffer, 0, ( int ) imageSize );

				//イメージ型の作成
				imgdt.Img = Image.FromStream ( mstrmImage );
			}
		}
	}

}
