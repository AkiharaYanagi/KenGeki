namespace ScriptEditor
{
	//==================================================================================
	//	Effectを受けて表示する
	//==================================================================================
	public class DispEffect : DispSequence
	{
		//エディット
//		public EditEffect editEffect { get; set; }

		//コンストラクタ
		public DispEffect ()
		{
		}

		//ロード
		public void Load ( _Ctrl_Compend ctrlCmpd )
		{
			//参照の設定
//			base.TbSeqName = ctrlCmpd.tB_ActionName;
		}
#if false

		public void Load (
			TextBox tbName, FormMain fm,
			EditChara ec, EditEffect ee )
		{
//			this.editEffect = ee;
			base.TbSeqName = tbName;
		}

#endif

		//表示
		public void Disp ( Effect effect )
		{
//			Effect effect = editEffect.Get ();
//			if ( null == effect ) { return; }


			base.Disp ( effect );
		}
	}
}
