using System.Diagnostics;

namespace ScriptEditor
{
	//==================================================================================
	//	キャラデータのテスト (IO, Edit)
	//		対象データは変更されるためテスト用データを外部から設定する
	//==================================================================================
	public class TestChara
	{
		//コンストラクタ
		public TestChara ( Chara chara )
		{
			Test ( chara );
		}

		//テスト
		public void Test ( Chara ch )
		{
			//EditCharaに設定
			EditChara.Inst.SetCharaDara ( ch );

			//初期化テスト
			TestClearChara ( ch );

			//テストデータ作成
			TestCharaData tcd = new TestCharaData ( ch );

			//コピーテスト
			Chara copyChara = new Chara ();
			TestCopyChara ( copyChara, ch );
		}


		//=====================================================================
		//	個別テスト
		//=====================================================================
		//初期化テスト
		public void TestClearChara ( Chara chara )
		{
			//test chara.Clear()
			//		データ個数が０に初期化されているかテスト
			chara.Clear ();
			Debug.Assert ( 0 == chara.behavior.Bldct_sqc.GetBindingList().Count );
			Debug.Assert ( 0 == chara.garnish.Bldct_sqc.GetBindingList().Count );
			Debug.Assert ( 0 == chara.ListCommand.Count );
		}

		//コピーテスト
		public void TestCopyChara ( Chara dstChara , Chara srcChara )
		{
			dstChara.Copy ( srcChara );

			//リスト個数が同数かテスト
			Debug.Assert ( srcChara.behavior.Bldct_sqc.GetBindingList().Count == dstChara.behavior.Bldct_sqc.GetBindingList().Count );
			Debug.Assert ( srcChara.behavior.ListImage.GetBindingList().Count == dstChara.behavior.ListImage.GetBindingList().Count );
			Debug.Assert ( srcChara.garnish.Bldct_sqc.GetBindingList().Count == dstChara.garnish.Bldct_sqc.GetBindingList().Count );
			Debug.Assert ( srcChara.garnish.ListImage.GetBindingList().Count == dstChara.garnish.ListImage.GetBindingList().Count );
			Debug.Assert ( srcChara.ListCommand.Count == dstChara.ListCommand.Count );
		}

	}

}
