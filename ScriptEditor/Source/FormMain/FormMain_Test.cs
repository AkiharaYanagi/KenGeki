using System;
using System.Windows.Forms;

namespace ScriptEditor
{
	public partial class FormMain : Form
	{
		//==================================================================================
		//	テスト：キャラデータ
		//==================================================================================
		public void TestCharaData ()
		{
			//ファイル名指定
			string testName = "testChara.dat";
//			settings.LastFilename = testName;

			//キャラのテストデータの作成・書出・読込の動作確認
			try
			{
				//テスト用キャラの作成
				Chara testCharaData = new Chara ();
				TestCharaData tcd = new TestCharaData ();
				tcd.Make ( testCharaData );

#if false
				//テストオブジェクトによる機能のテスト
				TestChara testChara = new TestChara ();
				testChara.Test ( testCharaData );
#if true
				//書出
				SaveChara saveChara = new SaveChara ( testName, testCharaData );
#else
				//既存のとき書出しない
				if ( ! File.Exists ( edittingFilename ) )
				{
					//書出
					SaveChara saveChara = new SaveChara ( edittingFilename, testCharaData );
				}
#endif
				//読込
				LoadChara loadChara = new LoadChara ( testName, chara );
		
				//テストデータからキャラ作成ソースファイルを作成
//				MakeTestCharaData mtcd = new MakeTestCharaData ( chara );
#endif

				//キャラデータの適用
				SetCharaData ( chara );

				//フォームテキストの更新
				SetFormText ( testName );
			}
			catch ( ArgumentException e )
			{
				MessageBox.Show ( "TestCharaData\n" + e.Message );
			}
		}

	}
}
