namespace ScriptEditor
{
	//---------------------------------------------------------------------
	// シークエンスの編集をする
	//---------------------------------------------------------------------
	public class EditSequence
	{
		public Sequence Seq { get; set; } = null;

		public void Assosiate ( Sequence s )
		{
			Seq = s;
		}

		public void SetName ( string strName )
		{
			Seq.Name = strName;
		}
	}

}
