namespace ScriptEditor
{
	public partial class FormMain
	{
		private Ctrl_Settings stgs = new Ctrl_Settings();	//プレデータ保存ファイル

		//プレデータテキストファイルから既存データの読込
		public void LoadCtrlData ()
		{
			ctrl_SqcList_Act.LoadData ();
			ctrl_SqcList_Ef.LoadData ();
			ctrl_CmdList1.LoadData ();
			ctrl_Branch1.LoadData ();
			ctrl_Route1.LoadData ();
		}

		//プレデータ上書保存
		public void CtrlSaveOverwrite ()
		{
			ctrl_CmdList1.SaveOverwrite ();
		}
	}
}
