using System;
using System.Windows.Forms;

namespace ScriptEditor
{
	public partial class Ctrl_FormBtn_v : UserControl
	{
		public Ctrl_FormBtn_v ()
		{
			InitializeComponent ();

			this.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		}

		//--------------------------------------------------------------------
		private void BtnFmAction_Click ( object sender, EventArgs e )
		{
//			FormAction.Inst.Active ();
		}
		private void Btn_HideAction_Click ( object sender, EventArgs e )
		{
//			FormAction.Inst.Hidden ();
		}
		//--------------------------------------------------------------------
		private void BtnScriptList_Click ( object sender, EventArgs e )
		{
			Form_ScriptList.Inst.Active ();
		}
		private void Btn_HideScriptList_Click ( object sender, EventArgs e )
		{
			Form_ScriptList.Inst.Hide ();
		}
		//--------------------------------------------------------------------
		private void BtnScriptValue_Click ( object sender, EventArgs e )
		{
			FormScript.Inst.Active ();
		}
		private void Btn_HideScriptValue_Click ( object sender, EventArgs e )
		{
			FormScript.Inst.Hide ();
		}
		//--------------------------------------------------------------------
		private void BtnImg_Click ( object sender, EventArgs e )
		{
			FormImage.Inst.Active ();
		}
		private void Btn_HideImage_Click ( object sender, EventArgs e )
		{
			FormImage.Inst.Hide ();
		}
		//--------------------------------------------------------------------
		private void Btn_RctList_Click ( object sender, EventArgs e )
		{
			FormRect2.Inst.Active ();
		}
		private void Btn_HideRectList_Click ( object sender, EventArgs e )
		{
			FormRect2.Inst.Hide ();
		}
		//--------------------------------------------------------------------
		private void Btn_EfGnrt_Click ( object sender, EventArgs e )
		{
//			FormEfGnrt.Inst.Active ();
			_FormEfGnrt.Inst.Active ();
		}
		private void Btn_HideEfGnrt_Click ( object sender, EventArgs e )
		{
//			FormEfGnrt.Inst.Hidden ();
			_FormEfGnrt.Inst.Hide ();
		}
		//--------------------------------------------------------------------
		private void Btn_Route_Click ( object sender, EventArgs e )
		{
			FormRoute.Inst.Active ();
		}
		private void Btn_HideRoute_Click ( object sender, EventArgs e )
		{
			FormRoute.Inst.Hide ();
		}
		//--------------------------------------------------------------------
		private void Btn_Preview_Click ( object sender, EventArgs e )
		{
//			FormPreview.Inst.Active ();
		}
		private void Btn_HidePreview_Click ( object sender, EventArgs e )
		{
//			FormPreview.Inst.Hidden ();
		}
		//--------------------------------------------------------------------
	}
}
