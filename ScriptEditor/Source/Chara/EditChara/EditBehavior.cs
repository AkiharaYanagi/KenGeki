namespace ScriptEditor
{
	//---------------------------------------------------------------------
	// ビヘイビアの編集をする
	//---------------------------------------------------------------------
	public class EditBehavior : EditCompend 
	{
		//シークエンス種類名
		public string kindSequence = "アクション";

		//部分編集：１アクション
		public EditAction EditAction { get; set; } = new EditAction ();

		//キャラデータの設定
		public void SetCharaData ( Chara ch )
		{
			base.SetCharaData ( ch, ch.behavior );
		}

		//取得
		public Action GetAction ()
		{
			return ( Action ) base.SelectedSequence;
		}

		//追加
		public void AddAction ()
		{
			Compend.Bldct_sqc.Add ( new Action ( "new Action" ) );
		}
		public void AddAction ( Action action )
		{
			Compend.Bldct_sqc.Add ( action );
		}

		//挿入
		public void InsertAction ()
		{
			base.InsertSequence ( new Action ( "new Action" ) );
		}

		//削除
		public void RemoveAction ()
		{
			base.RemoveSequence ();
		}

#if false

		//スクリプトの選択
		public override void SetSelectedScript ( SELECTED_SCRIPT ss )
		{
			EditAction.Assosiate ( GetAction () );
			base.SetSelectedScript ( ss );
		}
#endif
	}

}
