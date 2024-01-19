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

		// -> コントロールクラス(PB_Image)に移項

#if false
	public partial class FormMain : Form
	{
		//----------------------------------------------------------------------------------
		//	スクリプトのイメージ表示と編集 ( pbi_Script )
		//	FormMainでのイベント
		//----------------------------------------------------------------------------------


		

		//ドラッグ＆ドロップ
		private Point startPt = new Point ( 0, 0 );
		private Point dragPt = new Point ( 0, 0 );
		private bool imageDragged = false;

		//イベント・マウスボタン押下時
		private void pbScriptImage_MouseDown ( object sender, MouseEventArgs e )
		{
			//右ドラッグ・表示全体の移動
			if ( e.Button == MouseButtons.Right )
			{
				startPt = dispChara.dispBehavior.dispScript.ptPbImageBase;

				dragPt = System.Windows.Forms.Cursor.Position;
				imageDragged = true;
			}
			//左ドラッグ・選択中ツールによる対象物の移動
			else if ( e.Button == MouseButtons.Left )
			{
//				if ( null == editChara.GetActionScript () ) { return; }
				if ( null == editChara.EditAction.GetScript () ) { return; }

				//作業用
				DispScript ds = dispChara.dispBehavior.dispScript;
				Point ptImageBase = ds.ptPbImageBase;

				//イメージの移動
				if ( rb_ImageMove.Checked )
				{
//					startPt = editChara.GetActionScript ().RefPt.Get ();
					startPt = editChara.EditAction.GetScript ().RefPt.Get ();
				}
				//枠の指定
				else if ( rbCRect.Checked )
				{
					Point clientPt = pbi_Action.PointToClient ( System.Windows.Forms.Cursor.Position );
					startPt.X = clientPt.X - ds.ptPbImageBase.X;
					startPt.Y = clientPt.Y - ds.ptPbImageBase.Y;
				}
				else if ( rbARect.Checked )
				{
					Point clientPt = pbi_Action.PointToClient ( System.Windows.Forms.Cursor.Position );
					startPt.X = clientPt.X - ds.ptPbImageBase.X;
					startPt.Y = clientPt.Y - ds.ptPbImageBase.Y;
				}
				else if ( rbHRect.Checked )
				{
					Point clientPt = pbi_Action.PointToClient ( System.Windows.Forms.Cursor.Position );
					startPt.X = clientPt.X - ds.ptPbImageBase.X;
					startPt.Y = clientPt.Y - ds.ptPbImageBase.Y;
				}
				else if ( rbORect.Checked )
				{
					Point clientPt = pbi_Action.PointToClient ( System.Windows.Forms.Cursor.Position );
					startPt.X = clientPt.X - ds.ptPbImageBase.X;
					startPt.Y = clientPt.Y - ds.ptPbImageBase.Y;
				}

				dragPt = System.Windows.Forms.Cursor.Position;
				imageDragged = true;
			}
		}


		//イベント・マウス移動時
		bool changed = false;
		private void pbScriptImage_MouseMove ( object sender, MouseEventArgs e )
		{
			if ( imageDragged )
			{
				//スクリプト表示
//				DispScript ds = dispChara.DispScript;
				DispScript ds = dispChara.dispBehavior.dispScript;
				
				//カーソル位置
				Point cpt = System.Windows.Forms.Cursor.Position;

				//クライアント位置
				Point clientPt = pbi_Action.PointToClient ( cpt );

				//ドラッグ量
				Point pt = new Point();
				pt.X = startPt.X + cpt.X - dragPt.X;
				pt.Y = startPt.Y + cpt.Y - dragPt.Y;

				Point pt0 = new Point ();
				pt0.X = clientPt.X - startPt.X - ds.ptPbImageBase.X;
				pt0.Y = clientPt.Y - startPt.Y - ds.ptPbImageBase.Y;


				//表示全体の移動
				if ( e.Button == MouseButtons.Right )
				{
					ds.ptPbImageBase = pt;
//					ds.Disp ( editChara.GetActionScript () );
					ds.Disp ();
				}
				//ツールでの移動
				else if ( e.Button == MouseButtons.Left )
				{
//					Script script = editChara.GetActionScript ();
					Script script = editChara.EditAction.GetScript ();

					//ツールの分岐

					//イメージの移動
					if ( rb_ImageMove.Checked )
					{
						script.RefPt.Set ( pt );
						changed = true;
					}
					//ぶつかり枠
					else if ( rbCRect.Checked )
					{
						Rectangle rect = new Rectangle ( startPt.X, startPt.Y, clientPt.X - startPt.X - ds.ptPbImageBase.X, clientPt.Y - startPt.Y - ds.ptPbImageBase.Y );
						changed = true;
						ds.SetCRect ( rect );
					}
					//攻撃枠
					else if ( rbARect.Checked )
					{
//						Point clientPt = pbi_Script.PointToClient ( cpt );
						Rectangle rect = new Rectangle ( startPt.X, startPt.Y, clientPt.X - startPt.X - ds.ptPbImageBase.X, clientPt.Y - startPt.Y - ds.ptPbImageBase.Y );
						changed = true;
						ds.SetARect ( rect );
					}
					//当り枠
					else if ( rbHRect.Checked )
					{
//						Point clientPt = pbi_Script.PointToClient ( cpt );
						Rectangle rect = new Rectangle ( startPt.X, startPt.Y, clientPt.X - startPt.X - ds.ptPbImageBase.X, clientPt.Y - startPt.Y - ds.ptPbImageBase.Y );
						changed = true;
						ds.SetHRect ( rect );
					}
					//相殺枠
					else if ( rbORect.Checked )
					{
//						Point clientPt = pbi_Script.PointToClient ( cpt );
						Rectangle rect = new Rectangle ( startPt.X, startPt.Y, clientPt.X - startPt.X - ds.ptPbImageBase.X, clientPt.Y - startPt.Y - ds.ptPbImageBase.Y );
						changed = true;
						ds.SetORect ( rect );
					}

//					ds.Disp ( script );
					ds.Disp ();
				}
			}
		}

		//イベント・マウスボタン離上
		private void pbScriptImage_MouseUp ( object sender, MouseEventArgs e )
		{
			if ( changed )
			{
				//スクリプト変更後に他スクリプトにコピーするかチェックする
//				CheckSetOtherScript ();
				editChara.EditAction.CheckSetOtherScript ();
				changed = false;
			}
			imageDragged = false;
		}

		//イベント・サイズ変更
		private void pbScriptImage_SizeChanged ( object sender, EventArgs e )
		{
			dispChara.dispBehavior.dispScript.Disp ();
		}

	}

#endif


}

