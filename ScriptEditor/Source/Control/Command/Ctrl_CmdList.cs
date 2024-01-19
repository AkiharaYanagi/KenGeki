using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScriptEditor
{
	public partial class Ctrl_CmdList :UserControl
	{
		private EditListbox < Command > ED_Cmd = new EditListbox<Command> ();
		
		public Ctrl_CmdList ()
		{
			InitializeComponent ();

			//----------------------------------
			//コントロール(ルート)
			ED_Cmd.Location = new Point ( 3, 3 );
			this.Controls.Add ( ED_Cmd );

			ED_Cmd.SelectedIndexChanged = ()=>
			{
				Command cmd = ED_Cmd.Get ();
				ctrl_Command1.Set ( cmd );
			};
		}

		public void SetCharaData ( Chara ch )
		{
			ED_Cmd.SetData ( ch.BD_Command );
			Command cmd = ED_Cmd.Get ();
			if ( cmd != null )
			{
				ctrl_Command1.Set ( cmd );
			}
		}
	}
}
