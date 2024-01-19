namespace ScriptEditor
{
	//---------------------------------------------------------------------
	// ガーニッシュの編集をする
	//---------------------------------------------------------------------
	public class EditGarnish : EditCompend 
	{
		public EditEffect EditEffect { get; set; } = null;

		public EditGarnish ()
		{
			EditEffect = new EditEffect (); 
		}

		public void SetCharaData ( Chara ch )
		{
			base.SetCharaData ( ch, ch.garnish );
		}

		//取得
		public Effect Get ()
		{
			return ( Effect ) base.SelectedSequence;
		}

		//追加
		public override void Add ()
		{
//			Compend.ListSequence.Add ( new Effect ( "new Effect" ) );
			Compend.Bldct_sqc.Add ( new Effect ( "new Effect" ) );
		}
		public void AddEffect ()
		{
//			Compend.ListSequence.Add ( new Effect ( "new Effect" ) );
			Compend.Bldct_sqc.Add ( new Effect ( "new Effect" ) );
		}
		public void AddEffect ( Effect effect )
		{
//			Compend.ListSequence.Add ( effect );
			Compend.Bldct_sqc.Add ( effect );
		}

		//挿入
		public override void Insert ()
		{
			base.InsertSequence ( new Effect ( "new Effect" ) );
		}
		public void InsertEffect ()
		{
			base.InsertSequence ( new Effect () );
		}

		//削除
		public void RemoveEffect ()
		{
			base.RemoveSequence ();
		}
	}

}
