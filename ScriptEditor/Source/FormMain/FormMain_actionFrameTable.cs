using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Diagnostics;


namespace ScriptEditor
{

#if false
	
	public partial class FormMain : Form
	{

		//==================================================================================
		//	フレーム表からのコンテキストメニュー
		//==================================================================================
		
		
		//@todo FrameTableクラスにコンテキストメニューを手動で加えると、
		//		クラス内にFormMainの定義を移項できる


		//----------------------------------------------------------------------------------
		//	アクション
		//----------------------------------------------------------------------------------
		private void アクション追加ToolStripMenuItem1_Click ( object sender, EventArgs e )
		{
//			editChara.AddAction ();
			editChara.EditAction.AddAction ();
			fT_Action.Invalidate ();
		}

		private void アクション挿入ToolStripMenuItem_Click ( object sender, EventArgs e )
		{
//			editChara.InsertAction ();
			editChara.EditAction.InsertAction ();
			fT_Action.Invalidate ();
		}

		private void アクション削除ToolStripMenuItem_Click ( object sender, EventArgs e )
		{
//			editChara.RemoveAction ();
			editChara.EditAction.RemoveAction ();
			fT_Action.Invalidate ();
		}


		//----------------------------------------------------------------------------------
		//	スクリプト
		//----------------------------------------------------------------------------------

		//右クリック時のスクリプトメニュー表示開始
		private void cMS_Script_Opening ( object sender, CancelEventArgs e )
		{
			//マウスのディスプレイ位置からコントロールのクライアント位置を取得
//			Point pt = frameTable.PointToClient ( System.Windows.Forms.Cursor.Position );

			//コピー時と非コピー時
//			if ( frameTable.IsCopy )
			if ( editCompend.IsCopy )
			{
				ペーストToolStripMenuItem.Enabled = true;
				追加してペーストToolStripMenuItem.Enabled = true;
				挿入してペーストToolStripMenuItem.Enabled = true;
			}
			else
			{
				ペーストToolStripMenuItem.Enabled = false;
				追加してペーストToolStripMenuItem.Enabled = false;
				挿入してペーストToolStripMenuItem.Enabled = false;
			}
		}

		private void フレーム追加ToolStripMenuItem_Click ( object sender, EventArgs e )
		{
			chara_undo.Copy ( chara );
			ｱﾝﾄﾞｩToolStripMenuItem.Enabled = true;

			editCompend.AddScript ();
			frameTable.Invalidate ();
		}

		private void フレーム挿入ToolStripMenuItem_Click ( object sender, EventArgs e )
		{
//			editChara.EditAction.InsertScript ();
			editCompend.InsertScript ();
			frameTable.Invalidate ();
		}

		//コピー一時保存
//		private bool isCopy = false;
		//-> EditCompendにフラグを移項
		private void コピーToolStripMenuItem_Click ( object sender, EventArgs e )
		{
//			isCopy = true;
//			editChara.EditAction.CopyTargetScript ();
			editCompend.CopyTargetScript ();
//			frameTable.IsCopy = true;
			frameTable.Invalidate ();
		}

		private void ペーストToolStripMenuItem_Click ( object sender, EventArgs e )
		{
//			isCopy = false;
			editChara.EditAction.PasteScript ();
			frameTable.Invalidate ();
//			dispScript.Disp ( editChara.GetActionScript () );
//			dispChara.DispScript.Disp ( editChara.GetActionScript () );
		}

		private void 追加してペーストToolStripMenuItem_Click ( object sender, EventArgs e )
		{
//			editChara.EditAction.AddAndPasteScript ();
			editCompend.AddAndPasteScript ();
			frameTable.Invalidate ();
		}

		private void 挿入してペーストToolStripMenuItem_Click ( object sender, EventArgs e )
		{
			editChara.EditAction.InsertAndPasteScript ();
			frameTable.Invalidate ();
		}

		private void 削除ToolStripMenuItem_Click ( object sender, EventArgs e )
		{
//			isCopy = false;
			editChara.EditAction.RemScript ();
			frameTable.Invalidate ();
//			dispScript.Disp ( editChara.GetActionScript () );
//			dispChara.DispScript.Disp ( editChara.GetActionScript () );
		}

		//スクロールイベント
		private void panelFrameTable_Scroll ( object sender, ScrollEventArgs e )
		{
//			Debug.WriteLine ( e.NewValue.ToString () );
//			Debug.WriteLine ( panelFrameTable.VerticalScroll.Value );
			frameTable.Invalidate ();
		}

	}
#endif

}
