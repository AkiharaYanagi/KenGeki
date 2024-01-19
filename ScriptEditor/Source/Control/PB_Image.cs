using System;
using System.Windows.Forms;
using System.Drawing;

namespace ScriptEditor
{
	//----------------------------------------------------------------------------------
	//	イメージ表示と編集
	//----------------------------------------------------------------------------------
	public class PB_Image : PictureBox
	{
		//アクションもしくはエフェクトの編集と表示を参照する
		public EditCompend editCompend { get; set; }
		public DispCompend dispCompend { get; set; }

		//ツール
		public RadioButton rb_ImageMove { get; set; }
		public RadioButton rbORect { get; set; }
		public RadioButton rbHRect { get; set; }
		public RadioButton rbARect { get; set; }
		public RadioButton rbCRect { get; set; }
		public RadioButton rb_EfMove { get; set; }

		//ドラッグ＆ドロップ
		private Point startPt = new Point ( 0, 0 );
		private Point dragPt = new Point ( 0, 0 );
		private bool imageDragged = false;

		//ロード(コントロール設定)
		public void Load
		(
			EditCompend ec, DispCompend dc, 
			RadioButton rb_im, 
			RadioButton rb_or, RadioButton rb_hr, RadioButton rb_ar, RadioButton rb_cr, 
			RadioButton rb_em
		)
		{
			editCompend = ec;
			dispCompend = dc;

			rb_ImageMove = rb_im;
			rbCRect = rb_cr;
			rbARect = rb_ar;
			rbHRect = rb_hr;
			rbORect = rb_or;
			rb_EfMove = rb_em;

			ToolTip tt_CRect = new ToolTip ();
			tt_CRect.SetToolTip ( rb_cr, "ぶつかり枠" );
			ToolTip tt_ARect = new ToolTip ();
			tt_ARect.SetToolTip ( rb_ar, "攻撃枠" );
			ToolTip tt_HRect = new ToolTip ();
			tt_HRect.SetToolTip ( rb_hr, "くらい枠" );
			ToolTip tt_ORect = new ToolTip ();
			tt_ORect.SetToolTip ( rb_or, "相殺枠" );

			ToolTip tt_ImageMove = new ToolTip ();
			tt_ImageMove.SetToolTip ( rb_im, "メインイメージ移動" );
			ToolTip tt_EfImageMove = new ToolTip ();
			tt_EfImageMove.SetToolTip ( rb_em, "エフェクトイメージ移動" );
		}

		//イベント・マウスボタン押下時
		protected override void OnMouseDown ( MouseEventArgs e )
		{
			//右ドラッグ・表示全体の移動
			if ( e.Button == MouseButtons.Right )
			{
				startPt = dispCompend.DispScript.ptPbImageBase;

				dragPt = System.Windows.Forms.Cursor.Position;
				imageDragged = true;
			}
			//左ドラッグ・選択中ツールによる対象物の移動
			else if ( e.Button == MouseButtons.Left )
			{
				Script sc = editCompend.GetScript ();
				if ( null == sc ) { return; }

				//作業用
				DispScript ds = dispCompend.DispScript;
				Point ptImageBase = ds.ptPbImageBase;
				Point clientPt = this.PointToClient ( System.Windows.Forms.Cursor.Position );

				//イメージの移動
				if ( rb_ImageMove.Checked )
				{
					startPt = sc.RefPt.Get ();
				}
				//Efイメージの移動
				else if ( rb_EfMove.Checked )
				{
					int i = editCompend.selectedIndexEfGnrt;
					EffectGenerate efGnrt = sc.ListGenerateEf[ i ];
					startPt = efGnrt.ptGnrt.Get ();
				}
				//枠の指定
				else
				{
					if ( rbCRect.Checked )
					{
						startPt = PointUt.PtSub ( clientPt, ptImageBase );
					}
					else if ( rbARect.Checked )
					{
						startPt = PointUt.PtSub ( clientPt, ptImageBase );
					}
					else if ( rbHRect.Checked )
					{
						startPt = PointUt.PtSub ( clientPt, ptImageBase );
					}
					else if ( rbORect.Checked )
					{
						startPt = PointUt.PtSub ( clientPt, ptImageBase );
					}
				}
				dragPt = System.Windows.Forms.Cursor.Position;
				imageDragged = true;
			}

			base.OnMouseDown ( e );
		}


		//イベント・マウス移動時
		bool changed = false;
		protected override void  OnMouseMove(MouseEventArgs e)
		{
			if ( imageDragged )
			{
				DispScript ds = dispCompend.DispScript;				//スクリプト表示
				Point cpt = System.Windows.Forms.Cursor.Position;	//カーソル位置
				Point clientPt = this.PointToClient ( cpt );		//クライアント位置

				//ドラッグ量
				Point pt = new Point ();
				pt.X = startPt.X + cpt.X - dragPt.X;
				pt.Y = startPt.Y + cpt.Y - dragPt.Y;

				Point pt0 = new Point ();
				pt0.X = clientPt.X - startPt.X - ds.ptPbImageBase.X;
				pt0.Y = clientPt.Y - startPt.Y - ds.ptPbImageBase.Y;


				//表示全体の移動
				if ( e.Button == MouseButtons.Right )
				{
					ds.ptPbImageBase = pt;
//					ds.Disp ();
				}
				//ツールでの移動
				else if ( e.Button == MouseButtons.Left )
				{
					Script script = editCompend.GetScript ();

					//ツールの分岐

					//イメージの移動
					if ( rb_ImageMove.Checked )
					{
						script.RefPt.Set ( pt );
						changed = true;
					}
					//Efイメージの移動
					if ( rb_EfMove.Checked )
					{
						//EfGnrtの取得
						int i = editCompend.selectedIndexEfGnrt;
						EffectGenerate efGnrt = script.ListGenerateEf [ i ];
						efGnrt.ptGnrt.Set ( pt );
						changed = true;
					}
					//各種枠
					else
					{
						Rectangle rect = new Rectangle ( startPt.X, startPt.Y, pt0.X, pt0.Y );
						changed = true;
						if ( rbCRect.Checked )
						{
							ds.SetCRect ( rect );	//ぶつかり枠
						}
						else if ( rbARect.Checked )
						{
							ds.SetARect ( rect );	//攻撃枠
						}
						else if ( rbHRect.Checked )
						{
							ds.SetHRect ( rect );	//当り枠
						}
						else if ( rbORect.Checked )
						{
							ds.SetORect ( rect );	//相殺枠
						}
					}

//					ds.Disp ();
				}
			}
			base.OnMouseMove ( e );
		}


		//イベント・マウスボタン離上
		protected override void OnMouseUp ( MouseEventArgs e )
		{
			if ( changed )
			{
				//スクリプト変更後に他スクリプトにコピーするかチェックする
				editCompend.CheckSetOtherScript ();
				changed = false;
			}
			imageDragged = false;

			base.OnMouseUp ( e );
		}

		//イベント・サイズ変更
		protected override void OnSizeChanged ( EventArgs e )
		{
//			dispScript.Disp ( editChara.GetActionScript () );
//			dispChara.dispBehavior.DispScript.Disp ();

			base.OnSizeChanged ( e );
		}

		private void InitializeComponent ()
		{
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}
	}
}
