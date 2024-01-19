using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace ScriptEditor
{
	using BD_Sqc = BindingDictionary < Sequence >;
	using BD_IMGD = BindingDictionary < ImageData >;

	//-------------------------------------------------------------------------
	//スクリプトにおけるイメージとそれを編集するツール群をまとめるコントロール
	//-------------------------------------------------------------------------
	public partial class Ctrl_Image : UserControl
	{
		//アクションもしくはエフェクトの編集と表示を参照する
		public EditCompend EditCompend { get; set; } = null;
		public DispCompend DispCompend { get; set; } = null;

		//描画
		public PaintImage paintImage = new PaintImage ();

		public Point ptImageBase;
		public Point ptClient;

		//-----------------------------------------------------
		//ドラッグ＆ドロップ
		private Point prePt = new Point ( 0, 0 );
		private Point startPt = new Point ( 0, 0 );
		private bool dragging = false;

		//ツール選択
		public ToolImg SelectingTool { get; set; } = null;	//選択中ツール

		//ツール
		private ToolImg_Main Tlimg_Main = new ToolImg_Main ();
		private ToolImg_Rect Tlimg_CRect = new ToolImg_Rect ();
		private ToolImg_Rect Tlimg_HRect = new ToolImg_Rect ();
		private ToolImg_Rect Tlimg_ARect = new ToolImg_Rect ();
		private ToolImg_Rect Tlimg_ORect = new ToolImg_Rect ();
		private ToolImg_Ef Tlimg_Ef = new ToolImg_Ef ();

		//==================================================================================
		public Ctrl_Image ()
		{
			InitializeComponent ();

			Tlimg_CRect.SetCtrlListRect ( FormRect2.Inst.Ctrl_AllRect.GetCtrlListCRect () );
			Tlimg_HRect.SetCtrlListRect ( FormRect2.Inst.Ctrl_AllRect.GetCtrlListHRect () );
			Tlimg_ARect.SetCtrlListRect ( FormRect2.Inst.Ctrl_AllRect.GetCtrlListARect () );
			Tlimg_ORect.SetCtrlListRect ( FormRect2.Inst.Ctrl_AllRect.GetCtrlListORect () );

			SelectingTool = Tlimg_Main;
		}

		public void SetEnviron ( EditCompend ec, DispCompend dc )
		{
			EditCompend = ec;
			DispCompend = dc;

			paintImage.SetCtrl ( PB_Image );
			Point ptimgbs = paintImage.PtPbImageBase;

			//ツールの初期化
			Tlimg_Main.SetCtrl ( ec, rB_ToolHand, this );
			Tlimg_CRect.SetCtrl ( ec, rB_ToolCRect, this );
			Tlimg_HRect.SetCtrl ( ec, rB_ToolHRect, this );
			Tlimg_ARect.SetCtrl ( ec, rB_ToolARect, this );
			Tlimg_ORect.SetCtrl ( ec, rB_ToolORect, this );
			Tlimg_Ef.SetCtrl ( ec, rB_ToolEfHand, this );

			rB_ToolHand.SetCtrl ( Tlimg_Main, this, "メインイメージ移動" );
			rB_ToolCRect.SetCtrl ( Tlimg_CRect, this, "接触枠" );
			rB_ToolHRect.SetCtrl ( Tlimg_HRect, this, "当り枠" );
			rB_ToolARect.SetCtrl ( Tlimg_ARect, this, "攻撃枠" );
			rB_ToolORect.SetCtrl ( Tlimg_ORect, this, "相殺枠" );
			rB_ToolEfHand.SetCtrl ( Tlimg_Ef, this, "エフェクトイメージ移動" );

			rB_ToolHand.Select ();
		}

		//キャラデータ読込時
		public void SetCharaData ( BD_IMGD bd_mainImg, BD_Sqc bd_sqc, BD_IMGD bd_efImg )
		{
			paintImage.SetCharaData ( bd_mainImg, bd_sqc, bd_efImg );
		}

		//描画
		protected override void OnPaint ( PaintEventArgs e )
		{
			paintImage.Paint ( EditCompend?.SelectedScript );
			base.OnPaint ( e );
		}

		//イベント・マウスボタン押下時
		private void PB_Image_MouseDown ( object sender, MouseEventArgs e )
		{
			Script sc = EditCompend.SelectedScript;
			//右ドラッグ・表示全体の移動
			if ( MouseButtons.Right == e.Button )
			{
				prePt = paintImage.PtPbImageBase;
				startPt = Cursor.Position;
				dragging = true;
			}
			//左ドラッグ・選択中ツールによる対象物の移動
			else if ( MouseButtons.Left == e.Button )
			{
				if ( null == sc ) { return; }

				//作業用
				ptImageBase = paintImage.PtPbImageBase;
				ptClient = PointToClient ( Cursor.Position );

				SelectingTool.MouseDown ();
				startPt = Cursor.Position;
				dragging = true;
			}

			paintImage.Paint ( sc );
			//FormRect.Inst.UpdateData ();
			FormRect2.Inst.UpdateData ();
		}

		//イベント・マウス移動時
		private void PB_Image_MouseMove ( object sender, MouseEventArgs e )
		{
			//右ドラッグ・表示全体の移動
			if ( MouseButtons.Right == e.Button )
			{
				if ( dragging )
				{
					//ドラッグ量
					Point dragPt = PointUt.PtSub ( Cursor.Position, startPt );

					//反映
					paintImage.PtPbImageBase = PointUt.PtAdd ( prePt, dragPt );
				}
			}

			//左ドラッグ・選択中ツールによる対象物の移動
			else if ( MouseButtons.Left == e.Button )
			{
				//ドラッグ中
				if ( dragging )
				{
					//ドラッグ量
					Point dragPt = PointUt.PtSub ( Cursor.Position, startPt );

					//ツールでの移動
					if ( e.Button == MouseButtons.Left )
					{
						SelectingTool.MouseMove ();
					}
				}
			}
			Script sc = EditCompend.SelectedScript;
			paintImage.Paint ( sc );
			FormRect2.Inst.UpdateData ();
			FormScript.Inst.UpdateData ();
		}

		//イベント・マウスボタン離上
		private void PB_Image_MouseUp ( object sender, MouseEventArgs e )
		{
			dragging = false;
			DispChara.Inst.Disp ();
		}

		//ツール：ラジオボタン
		private void rB_ToolHand_CheckedChanged ( object sender, System.EventArgs e )
		{
			if ( rB_ToolHand.Checked ) { SelectingTool = Tlimg_Main; }
		}
	}
}
