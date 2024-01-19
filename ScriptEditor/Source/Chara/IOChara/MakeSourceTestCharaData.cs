using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;

namespace ScriptEditor
{
	//==================================================================================
	//	現在のキャラデータからテスト用の手動作成ソース(.cs)を作成する
	//		データファイルの復旧や、バージョンが異なるときにソースから作成できる状態にしておく
	//==================================================================================
	public class MakeTestCharaData
	{
		public MakeTestCharaData ( Chara chara )
		{
			//キャラスクリプトとしてテキストメモリストリームに変換
			MemoryStream mstrm = new MemoryStream ();
			StreamWriter strmWriter = new StreamWriter ( mstrm, Encoding.UTF8 );

			//---------------------------------------
			//※ @"～"は改行を含め文字列定数とする指定子
			//	@文字列内にダブルクォーテーション("～")を含むとき
			//	二重引用符にする(""～"")
			//---------------------------------------

			//ヘッダ
			strmWriter.Write (
@"using System;

namespace ScriptEditor
{
	public partial class TestCharaData
	{
		public void SetCharaData ( Chara chara )
		{
" );
	
			strmWriter.Write ( "\t\t\t// VER:" + CONST.VER.ToString() + "\n" );
			
			//ビヘイビア(アクションリスト)
//			BindingList<Sequence> la = chara.behavior.ListSequence;
			BindingList<Sequence> la = chara.behavior.Bldct_sqc.GetBindingList ();

			//アクションの数だけループ
			strmWriter.Write ( "\t\t\t" + "// SIZE_ACTION = " + la.Count + "; \n" );
			foreach ( Action a in la )
			{
				strmWriter.Write ( "\t\t\t\t" );
				strmWriter.Write ( 
					"Action action = NewAction ( " 
					 + "chara"
					 + ", \"" + a.Name + "\""
					 + "," + a.NextIndex
					 + "," + (int)a.Category
					 + "," + (int)a.Posture
					 + "," + a._Balance
					 + " ); " );
				strmWriter.Write ( "\n" );

				//スクリプト
				List<Script> ls = a.ListScript;
				strmWriter.Write ( "\t\t\t\t" + "// SIZE_SCRIPT = " + ls.Count + ";\n" );

				foreach ( Script s in a.ListScript )
				{
					strmWriter.Write ( "\t\t\t\t{\n" );
					strmWriter.Write ( "\t\t\t\t\t" );
					strmWriter.Write ( "Script script = NewScript ( " );
					strmWriter.Write ( s.ImgIndex );
					strmWriter.Write ( ", " + s.Pos.X );
					strmWriter.Write ( ", " + s.Pos.Y );
					strmWriter.Write ( ", " + s.Vel.X );
					strmWriter.Write ( ", " + s.Vel.Y );
					strmWriter.Write ( ", " + s.Acc.X );
					strmWriter.Write ( ", " + s.Acc.Y );
					strmWriter.Write ( " ); \n" );

					//ブランチを必要個数確保
					BindingList<Branch> lb = s.ListBranch;
					if ( 0 < lb.Count ) { strmWriter.Write ( "\t\t\t\t\t" + "/* SIZE_BRANCH = " + lb.Count + "; */ { " ); }
					for ( int iBranch = 0; iBranch > lb.Count; ++iBranch )
					{
						Branch b = lb[ iBranch ];
						strmWriter.Write ( "Branch branch = new Branch ();" );
						strmWriter.Write ( "branch.IndexCommand = " + b.IndexCommand + "; " );
						strmWriter.Write ( "branch.IndexAction = " + b.IndexAction + "; " );
						strmWriter.Write ( "script.ListBranch.Add ( branch ); " );
					}
					if ( 0 < lb.Count ) { strmWriter.Write ( "}\n" ); }

					//Efジェネレートを必要個数確保
					BindingList<EffectGenerate> le = s.ListGenerateEf;
					if ( 0 < le.Count ) { strmWriter.Write ( "\t\t\t\t\t" + "/* SIZE_EFGNRT = " + le.Count + "; */ { " ); }					for ( int iEfGnrt = 0; iEfGnrt < le.Count; ++iEfGnrt )
					{
						EffectGenerate eg = le[ iEfGnrt ];
						strmWriter.Write ( "EffectGenerate efGnrt = new EffectGenerate (); " );
						strmWriter.Write ( "efGnrt.id.i = " + eg.Id + "; " );
						strmWriter.Write ( "efGnrt.ptGnrt.x.i = " + eg.Pt.X + "; " );
						strmWriter.Write ( "efGnrt.ptGnrt.y.i = " + eg.Pt.Y + "; " );
						strmWriter.Write ( "script.ListGenerateEf.Add ( efGnrt ); " );
					}
					if ( 0 < le.Count ) { strmWriter.Write ( "}\n" ); }

					//アクションにスクリプトを追加
					strmWriter.Write ( "\t\t\t\t\t" + @"action.AddScript ( script ); " + "\n" );

					strmWriter.Write ( "\t\t\t\t}\n\r" );
				}

				//キャラにアクションを追加
				strmWriter.Write ( "\t\t\t\t" + @"chara.behavior.ListSequence.Add ( action ); " + "\n" );
		
			}

			//終了
			strmWriter.Write ( "\t\t}" );

			//関数
			strmWriter.Write (
@"
		private Action NewAction ( Chara chara, string name, int nextIndex, int category, int posture, int balance )
		{
			Action action = new Action ();
			action.Name = name;
			action.NextIndex = nextIndex;
			action.Category = (ActionCategory)category;
			action.Posture = (ActionPosture)posture;
			action.Balance.i = balance;
			return action;
		}

		private Script NewScript ( Action action, int imgIndex, RefPt refPt, RefPt refVel, RefPt refAcc, int power )
		{
			Script script = new Script ();
			script.ImgIndex = imgIndex;
			script.RefPt.x.i = refPt.x.i;
			script.RefPt.y.i = refPt.y.i;
			script.RefVel.x.i = refVel.x.i;
			script.RefVel.y.i = refVel.y.i;
			script.RefAcc.x.i = refAcc.x.i;
			script.RefAcc.y.i = refAcc.y.i;
			script.Power.i = power;
			return script;
		}
	}" );

			//フッタ
			strmWriter.Write ( "\n}" );

	
			//ストリーム書出
			strmWriter.Flush ();

			//==========================================================================
			//テキストファイル(.cs)書出

			//ストリームリーダにセット
			StreamReader strmReaderChara = new StreamReader ( mstrm );

			//ファイルにテキスト書出用ストリームを設定する
			StreamWriter strmWriterFile = new StreamWriter ( "MakeTestCharaData.cs", false, Encoding.UTF8 );

			//書出
			mstrm.Seek ( 0, SeekOrigin.Begin );			//先頭から
			strmWriterFile.Write ( strmReaderChara.ReadToEnd () );	//最後まで書出し
			strmWriterFile.Close ();

			strmWriter.Close ();

		}

		private Action NewAction ( Chara chara, string name, int nextIndex, int category, int posture, int balance )
		{
			Action action = new Action ();
			action.Name = name;
			action.NextIndex = nextIndex;
			action.Category = (ActionCategory)category;
			action.Posture = (ActionPosture)posture;
			action._Balance = balance;
			return action;
		}
	}

}
