using System;
using System.Collections.Generic;
using System.Drawing;

namespace ScriptEditor
{
	//==================================================================================
	//	List<RefRect>に関するコントロールをまとめる
	//==================================================================================
	public class CtrlListRefRect
	{
		//コントロール
		public NUD_ListRect nudRect { get; set; } = null;
		public BTN_ListRect_Add btnListRectAdd { get; set; } = null;
		public BTN_ListRect_Rem btnListRectRem { get; set; } = null;
		public TB_Number tbaRectX { get; set; } = null;
		public TB_Number tbaRectY { get; set; } = null;
		public TB_Number tbaRectW { get; set; } = null;
		public TB_Number tbaRectH { get; set; } = null;

		//コンストラクタ
		public CtrlListRefRect () {}
		public CtrlListRefRect ( NUD_ListRect nud, BTN_ListRect_Add btnAdd, BTN_ListRect_Rem btnRem,
			TB_Number tbaX, TB_Number tbaY, TB_Number tbaW, TB_Number tbaH )
		{
			nudRect = nud;
			btnListRectAdd = btnAdd; btnListRectRem = btnRem;
			tbaRectX = tbaX; tbaRectY = tbaY; tbaRectW = tbaW; tbaRectH = tbaH;
		}
	}

	//==================================================================================
	//	List<RefRect>の表示を扱う
	//==================================================================================
	public class DispListRefRect
	{
		//枠リスト
		private List<RefRect> listRefRect = null;
		public List<RefRect> ListRefRect { set { listRefRect = value; } }

		//コントロール
		public NUD_ListRect nudRect { get; set; } = null;
		public BTN_ListRect_Add btnListRectAdd { get; set; } = null;
		public BTN_ListRect_Rem btnListRectRem { get; set; } = null;
		public TB_Number tbaRectX { get; set; } = null;
		public TB_Number tbaRectY { get; set; } = null;
		public TB_Number tbaRectW { get; set; } = null;
		public TB_Number tbaRectH { get; set; } = null;

		//表示枠色
		public Color color { get; set; } = Color.White;

		//コンストラクタ
		public DispListRefRect ( Color c )
		{
			color = c;
		}

		//再描画
		public void Invalidate ()
		{
			tbaRectX.UpdateData ();
			tbaRectY.UpdateData ();
			tbaRectW.UpdateData ();
			tbaRectH.UpdateData ();
		}

		//初回設定
		public void Load ( EditCompend ec, DispCompend dc, CtrlListRefRect cntxt )
		{
			//添字指定
			this.nudRect = cntxt.nudRect;
			this.nudRect.dispListRefRect = this;
			this.nudRect.dispCompend = dc;

			//追加ボタン
			this.btnListRectAdd = cntxt.btnListRectAdd;
			this.btnListRectAdd.nudRect = cntxt.nudRect;
			this.btnListRectAdd.dispListRefRect = this;
			this.btnListRectAdd.dispCompend = dc;

			//削除ボタン
			this.btnListRectRem = cntxt.btnListRectRem;
			this.btnListRectRem.nudRect = cntxt.nudRect;
			this.btnListRectRem.rrs = this;
			this.btnListRectRem.dispCompend = dc;

			//数値(X,Y,W,H)
			this.tbaRectX = cntxt.tbaRectX;
			this.tbaRectX.Load ( ec, dc);

			this.tbaRectY = cntxt.tbaRectY;
			this.tbaRectY.Load ( ec, dc );

			this.tbaRectW = cntxt.tbaRectW;
			this.tbaRectW.Load ( ec, dc );

			this.tbaRectH = cntxt.tbaRectH;
			this.tbaRectH.Load ( ec, dc );
		}

		//スクリプト変更時にコントロール側からの設定を反映する関連付け
		public void Associate ( List<RefRect> lrr )
		{
			//枠リストを設定
			listRefRect = lrr;

			//個数によって分岐

			//１以上
			if ( 0 < listRefRect.Count )
			{
				//nud最大値から配列のインデックスを取得
				nudRect.ListRefRect = lrr;
				nudRect.Maximum = listRefRect.Count - 1;

				//枠の各値の関連付け
				AssociateRefRect ( nudRect.GetSelectedRefRect() );

				//ボタンに対象リストの設定
				btnListRectAdd.ListRefRect = lrr;
				btnListRectRem.ListRefRect = lrr;
				this.On ();
			}	
			//０のとき
			else
			{
				nudRect.ListRefRect = lrr;
				nudRect.Maximum = 0;
				nudRect.Enabled = false;

				btnListRectAdd.ListRefRect = lrr;
				btnListRectRem.ListRefRect = lrr;
				this.Off ();
			}
		}

		//NUD変更時に枠の各値の関連付け
		public void AssociateRefRect ( RefRect refRect )
		{
			tbaRectX.refInt = refRect.x;
			tbaRectY.refInt = refRect.y;
			tbaRectW.refInt = refRect.w;
			tbaRectH.refInt = refRect.h;
		}

		//Enabled オフ
		public void Off ()
		{
			nudRect.Enabled = false;
			//btnListRectAddは常にオン
			btnListRectRem.Enabled = false;
			tbaRectX.Text = "0";
			tbaRectY.Text = "0";
			tbaRectW.Text = "0";
			tbaRectH.Text = "0";
			tbaRectX.Enabled = false;
			tbaRectY.Enabled = false;
			tbaRectW.Enabled = false;
			tbaRectH.Enabled = false;
		}

		//Enabled オン
		public void On ()
		{
			nudRect.Enabled = true;
			//btnListRectAddは常にオン
			btnListRectRem.Enabled = true;
			tbaRectX.Enabled = true;
			tbaRectY.Enabled = true;
			tbaRectW.Enabled = true;
			tbaRectH.Enabled = true;
		}

		//表示
		public void Disp ( Graphics g, Point ptPbImageBase )
		{
			if ( null == g ) { return; }
			if ( null == listRefRect ) { return; }
			if ( 0 == listRefRect.Count ) { return; }

			//枠リストのすべて
			foreach ( RefRect refRect in listRefRect )
			{
				//ピクチャボックス
				Rectangle drawRect = new Rectangle (
					refRect.x.i + ptPbImageBase.X, refRect.y.i + ptPbImageBase.Y,
					refRect.w.i, refRect.h.i
				);
				g.FillRectangle ( new SolidBrush ( color ), drawRect );
			}

			//選択中の枠
			RefRect selectedRefRect = nudRect.GetSelectedRefRect ();

			//選択中の枠を強調表示
			Rectangle rect = new Rectangle (
				selectedRefRect.x.i + ptPbImageBase.X, selectedRefRect.y.i + ptPbImageBase.Y,
				selectedRefRect.w.i, selectedRefRect.h.i
			);
			g.DrawRectangle ( new Pen ( Brushes.White ), rect );

			//選択中の枠のみをテキストボックスに表示
			tbaRectX.Text = selectedRefRect.x.i.ToString ();
			tbaRectY.Text = selectedRefRect.y.i.ToString ();
			tbaRectW.Text = selectedRefRect.w.i.ToString ();
			tbaRectH.Text = selectedRefRect.h.i.ToString ();
		}

		//枠を設定
		public void SetRect ( Rectangle rect )
		{
			int index = Decimal.ToInt32 ( nudRect.Value );
			if ( listRefRect.Count <= index ) { return; }
			listRefRect[ index ].Set ( rect );
		}

	}

}
