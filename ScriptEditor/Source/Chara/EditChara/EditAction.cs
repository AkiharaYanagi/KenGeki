namespace ScriptEditor
{
	//---------------------------------------------------------------------
	// アクションの編集をする
	//---------------------------------------------------------------------
	public class EditAction : EditSequence
	{
		private Action Action { get; set; } = null;

		public void Assosiate ( Action a )
		{
			base.Assosiate ( a );
		}

	}

}

