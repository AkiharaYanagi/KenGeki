using System.Windows.Forms;

namespace ScriptEditor
{
	public partial class FormRect2 : SubForm_Compend
	{
		//---------------------------------------------------------------------
		//シングルトン実体
		public static FormRect2 Inst { get; set; } = new FormRect2 ();
		
		//プライベートコンストラクタ
		private FormRect2 ()
		{
			InitializeComponent ();
			base.LoadObject ();
		}
		//---------------------------------------------------------------------

		//枠リスト コントロール
		public Ctrl_AllRect Ctrl_AllRect { get; set; } = new Ctrl_AllRect ();

		//FormMainで実体を確保し、設置する
		public void SetCtrl ( Ctrl_AllRect ctrl )
		{
			Ctrl_AllRect = ctrl;
			this.Controls.Add ( Ctrl_AllRect );
		}


		//環境設定
#if false
		public void SetEnvironment ( EditCompend ec )
		{
			Ctrl_AllRect.SetEnvironment ( ec, ()=>Ctrl_All.Inst.AllDisp() );
		}
#endif
		//編集機能の選択
		public override void SetEditCompend ( EditCompend ec )
		{
			Ctrl_AllRect.SetEditCompend ( ec );
//			Ctrl_AllRect.SetFnDispAll ( ()=>Ctrl_All.Inst.AllDisp() );
//			Ctrl_AllRect.SetFnDispAll ( ()=>All_Ctrl.Inst.Disp () );
			base.SetEditCompend ( ec ); 
		}

#if false
		//関連付け
		public void Assosiate ( Script scp )
		{
			Ctrl_AllRect.Assosiate ( scp );
		}
#endif

		//更新
		public void UpdateData ()
		{
			Ctrl_AllRect.UpdateData ();
		}

	}
}
