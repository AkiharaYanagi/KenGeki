using System;
using System.Windows.Forms;
using System.Drawing;


namespace ScriptEditor
{

	public sealed class _FormEfGnrt : SubForm_Compend
	{
		//コントロール
		private _Ctrl_EfGnrt ctrl_efgnrt = new _Ctrl_EfGnrt ();

		//---------------------------------------------------------------------
		//シングルトン実体
		public static _FormEfGnrt Inst { get; set; } = new _FormEfGnrt ();
		
		//プライベートコンストラクタ
		private _FormEfGnrt ()
		{
			this.Size = new Size ( 700, 600 );
			LoadObject ();
			this.Controls.Add ( ctrl_efgnrt );
		}
		//---------------------------------------------------------------------

		public void SetCharaData ( Chara ch )
		{
			ctrl_efgnrt.SetCharaData ( ch );
		}

		public override void SetEditCompend ( EditCompend ec )
		{
			ctrl_efgnrt.SetEditCompend ( ec );
			base.SetEditCompend ( ec );
		}

		private void InitializeComponent ()
		{
			this.SuspendLayout();
			// 
			// _FormEfGnrt
			// 
			this.ClientSize = new System.Drawing.Size(736, 570);
			this.Name = "_FormEfGnrt";
			this.ResumeLayout(false);

		}
	}
}
