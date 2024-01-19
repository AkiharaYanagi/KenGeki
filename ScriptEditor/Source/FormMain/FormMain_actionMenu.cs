using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;


namespace ScriptEditor
{

	public partial class FormMain : Form
	{

		//==================================================================================
		//	Tab "ｽｸﾘﾌﾟﾄ" メニュー
		//==================================================================================

		private void ｱﾝﾄﾞｩToolStripMenuItem_Click ( object sender, EventArgs e )
		{
			//chara.Copy ( chara_undo );

//			dispScript.Disp ( editChara.GetActionScript () );
//			dispChara.DispActionScript ( editChara.GetAction (), editChara.GetActionScript () );
//			dispChara.DispActionScript ();
			DispCompend ();

//			pbFrameTable.Invalidate ();
			ｱﾝﾄﾞｩToolStripMenuItem.Enabled = false;
		}

		private void ﾌﾟﾚﾋﾞｭｰToolStripMenuItem_Click ( object sender, EventArgs e )
		{
			//選択を保存
//			EditCompend.SELECTED_SCRIPT ss = editChara.SelectedScript;
			EditCompend.SELECTED_SCRIPT ss = editChara.EditAction.SelectedScript;

//			Action action = editChara.GetAction ();
			Action action = editChara.EditAction.Get ();

			//モードレスで表示
			Form_ActionPreview.Instance.ShowDialog ( chara, ss );
//			Form_ActionPreview.Instance.ShowDialog ( chara, action );

			//終了時に選択を復帰
//			editChara.SetSelectedScript ( ss );
			editChara.EditAction.SetSelectedScript ( ss );
		}

		private void ﾃｽﾄToolStripMenuItem_Click ( object sender, EventArgs e )
		{
			//モーダルで表示
			Form_Test.Instance.Location = new Point ( this.Location.X - 100, this.Location.Y + 100 );
			Form_Test.Instance.Init ();
			Form_Test.Instance.ShowDialog ( chara );
			//			Form_Test.Instance.ShowDialog ( this );
		}
	}

}
