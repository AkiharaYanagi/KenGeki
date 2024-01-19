using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;


namespace ScriptEditor
{
	using LsRect = List< Rectangle >;

	//イメージを操作する共通クラス
	public class ToolImg
	{
		//イメージ表示のコントロール(親)
		public Ctrl_Image CtrlImg = null;

		//位置
		protected Point prePt = new Point ( 0, 0 );
		protected Point startPt = new Point ( 0, 0 );
		protected bool dragging = false;

		//コンペンド編集
		public EditCompend EditCompend { get; set; } = null;

		//対応ラジオボタン
		public RB_Tool RbTl { get; set; } = null;
		
		//初期化
		public void SetCtrl ( EditCompend ec, RB_Tool rbt, Ctrl_Image ctrlImg )
		{
			EditCompend = ec;	//コンペンド編集
			RbTl = rbt;			//ラジオボタン
			CtrlImg = ctrlImg;	//イメージ表示
		}

		//マウスイベント
		public virtual void MouseDown () { }
		public virtual void MouseMove () { }
		public virtual void MouseUp () { }
	}


	//----------------------------------------------------------------------
	//メインイメージ移動
	public class ToolImg_Main : ToolImg
	{
		public override void MouseDown ()
		{
			Script script = EditCompend.SelectedScript;
			prePt = script.Pos;
			startPt = Cursor.Position;
			dragging = true;
		}
		public override void MouseMove ()
		{
			if ( dragging )
			{
				Script script = EditCompend.SelectedScript;
				Point dragPt = PointUt.PtSub ( Cursor.Position, startPt );
				Point setPt = PointUt.PtAdd ( prePt, dragPt );
				script.Pos = setPt;
			}
		}
		public override void MouseUp ()
		{
			dragging = false;
		}
	}

	//Efイメージ移動
	public class ToolImg_Ef : ToolImg
	{
		public override void MouseDown ()
		{
			EffectGenerate efGnrt =	FormEfGnrt.Inst.GetEfGnrt (); 
			prePt = efGnrt.Pt;
			startPt = Cursor.Position;
			dragging = true;
		}
		public override void MouseMove ()
		{
			if ( dragging )
			{
				EffectGenerate efGnrt =	FormEfGnrt.Inst.GetEfGnrt (); 
				Point dragPt = PointUt.PtSub ( Cursor.Position, startPt );
				Point setPt = PointUt.PtAdd ( prePt, dragPt );
				FormEfGnrt.Inst.SetEfPos ( setPt );
			}
		}
		public override void MouseUp ()
		{
			dragging = false;
		}
	}

	//----------------------------------------------------------------------
	//枠設定
	public class ToolImg_Rect : ToolImg
	{
		private Ctrl_ListRect ctrlRect;
		private Point startRectPt = new Point ( 0, 0 );

		//対象の枠コントロールを設定する
		public void SetCtrlListRect ( Ctrl_ListRect ctrl_lr )
		{
			ctrlRect = ctrl_lr;
		}

		public override void MouseDown ()
		{
			dragging = true;
			startPt = Cursor.Position;
			startRectPt = PointUt.PtSub ( CtrlImg.ptClient, CtrlImg.ptImageBase );
		}
		public override void MouseMove ()
		{
			if ( dragging )
			{
				Point dragPt = PointUt.PtSub ( Cursor.Position, startPt );
				Rectangle r = new Rectangle ( startRectPt.X, startRectPt.Y, dragPt.X, dragPt.Y );

				//CtrlからRectを設定する
				ctrlRect.SetRect ( r );
			}
		}
		public override void MouseUp ()
		{
			dragging = false;
		}
	}

#if false
	public class ToolImg_CRect : ToolImg_Rect
	{
		public ToolImg_CRect () { ctrlRect = FormRect2.Inst.ctrl_ListCRect; }
	}
	public class ToolImg_HRect : ToolImg_Rect
	{
		public ToolImg_HRect() { ctrlRect = FormRect2.Inst.ctrl_ListHRect; }
	}
	public class ToolImg_ARect : ToolImg_Rect
	{
		public ToolImg_ARect() { ctrlRect = FormRect2.Inst.ctrl_ListARect; }
	}
	public class ToolImg_ORect : ToolImg_Rect
	{
		public ToolImg_ORect() { ctrlRect = FormRect2.Inst.ctrl_ListORect; }
	}
#endif
}
