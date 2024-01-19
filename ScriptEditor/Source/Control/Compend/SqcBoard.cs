using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;


namespace ScriptEditor
{
	public partial class SqcBoard : UserControl
	{
		//アクションもしくはエフェクトの編集と表示を参照する
		//外部でビヘイビアとガーニッシュのいずれかを指定してSet()する
		public EditCompend EditCompend { get; set; } = null;
		public DispCompend DispCompend { get; set; } = null;

		//編集対象シークエンスの参照
		public Sequence Sqc = null;

		//グループ数
//		private int nGroup = 0;

		//-----------------------------------------------------------------------------
		//内部使用定数
		private const int W = 20;	//升幅
		private const int H = 12;	//升高
		private const int BX = 100;	//基準X
		private const int BY = 12;	//基準Y

		private const int HH = 10;	//升高半分


		//-----------------------------------------------------------------------------
		public SqcBoard ()
		{
			this.BackColor = SystemColors.Control;

			InitializeComponent ();
		}

		//環境設定
		public void SetEnviron ( EditCompend ec, DispCompend dc )
		{
			EditCompend = ec;
			DispCompend = dc;
		}

		//データ設定
		public void SetData ( Sequence sqc )
		{
			Sqc = sqc;
		}

		//関連付け
		public void Assosiate ( Sequence sqc )
		{
			Sqc = sqc;
			Invalidate ();
		}

		//-----------------------------------------------------------------------------
		//描画
		private void SqcBoard_Paint ( object sender, PaintEventArgs e )
		{
			pictureBox1.Invalidate ();
		}

		private void pictureBox1_Paint ( object sender, PaintEventArgs e )
		{
			//オブジェクトが設定されていないときデフォルトの表示
			if ( Sqc is null ) { pictureBox1_Paint_Default ( sender, e ); return; }
			if ( Sqc.ListScript is null ) { pictureBox1_Paint_Default ( sender, e ); return;}

			//---------------------------------------------------------
			//共通基準値
			Graphics g = e.Graphics;
			List<Script> ls = Sqc.ListScript;

			//内部使用リソース
			using ( Pen PEN_BAR = new Pen ( Color.FromArgb ( 0x80, 0x80, 0xff ), 1 ) )
			using ( Pen PEN_BASE_BAR = new Pen ( Color.FromArgb ( 0xc0, 0xc0, 0xc0 ), 0.5f ) )
			using ( Pen PEN_SELECTED = new Pen ( Color.FromArgb ( 0x00, 0x00, 0xff ), 2.0f ) )
			using ( SolidBrush BRUSH_0 = new SolidBrush ( Color.FromArgb ( 0xff, 0xd0, 0xff, 0xd0 ) ) )
			using ( StringFormat STR_FMT = new StringFormat () )
			{

			//文字列フォーマット
			STR_FMT.Alignment = StringAlignment.Center;	//文字列の中央揃え(水平)
			STR_FMT.LineAlignment = StringAlignment.Center;	//文字列の中央揃え(垂直)

			//大きさ
//			int minWidth = 1000;
			int scWidth = 100+BX + ( ls.Count + 2 ) * W;
//			this.Width = ( minWidth < scWidth ) ? scWidth: minWidth;
			this.Height = 130 + BY + ( 2 * H );

			pictureBox1.Width = scWidth + 100;
			pictureBox1.Height = H * 7;

			//大きさの一時保存
			int TW = this.Width;
			int TH = this.Height;
			int PW = pictureBox1.Width;

			//個数
			int N = Sqc.ListScript.Count;
			//余剰個数
			const int SURPLUS = 5;

			//選択
			int selectedScript = EditCompend.SelectedScript.Frame;
			int selectedSpanStart = EditCompend.SelectedSpanStart;
			int selectedSpanEnd = EditCompend.SelectedSpanEnd;

			//---------------------------------------------------------
			//フレーム表示部分の背景(存在しない部分を一括描画)
			g.FillRectangle ( Brushes.LightGray, BX, 0, PW - BX, TH );

			//---------------------------------------------------------
			//キャラデータの表示
			g.FillRectangle ( Brushes.AliceBlue, BX, H, W * ls.Count, H );

			//---------------------------------------------------------
			//選択スパン表示
			int wSpan = W * ( 1 + selectedSpanEnd - selectedSpanStart );
			int xSpan = BX + W * selectedSpanStart;
			Rectangle rectSpan = new Rectangle ( xSpan, BY, wSpan, H );
			g.FillRectangle ( BRUSH_0, rectSpan );

			//---------------------------------------------------------
			//スクリプト内容
			int count = 0;
			foreach ( Script s in Sqc.ListScript )
			{
				DrawScp ( g, count, 0, DefineColor.Get ( s.Group ) );	//グループ表示
				DrawScp ( g, count, 2, GetScpCntClr ( s.ListCRect, ScpCntColor [ 1 ] ) );	//接触枠
				DrawScp ( g, count, 3, GetScpCntClr ( s.ListHRect, ScpCntColor [ 2 ] ) );	//防御枠
				DrawScp ( g, count, 4, GetScpCntClr ( s.ListARect, ScpCntColor [ 3 ] ) );	//攻撃枠
				DrawScp ( g, count, 5, GetScpCntClr ( s.ListORect, ScpCntColor [ 4 ] ) );	//相殺枠
				DrawScp ( g, count, 6, GetScpEfGnClr ( s.BD_EfGnrt, ScpCntColor [ 5 ] ) );	//Ef生成

				++ count;
			}


			//---------------------------------------------------------
			//フレーム数
			for ( int i = 0; i < N + SURPLUS; ++i )
			{
				Rectangle r = new Rectangle ( BX + i * W - 10, H + 2, W + 20, H );
				g.DrawString ( i.ToString (), Font, Brushes.Gray, r, STR_FMT );
			}

			//文字
			DrawStr_0 ( g, "Group", 0, STR_FMT );
			DrawStr_0 ( g, "[Frame]", 1, STR_FMT );
			DrawStr_0 ( g, "■ CRect", 2, STR_FMT );
			DrawStr_0 ( g, "■ HRect", 3, STR_FMT );
			DrawStr_0 ( g, "■ ARect", 4, STR_FMT );
			DrawStr_0 ( g, "■ ORect", 5, STR_FMT );
			DrawStr_0 ( g, "EfGnrt", 6, STR_FMT );

			//---------------------------------------------------------
			//基準線
			const int PH = H + H * 6;

			//縦線	
			g.DrawLine ( PEN_BASE_BAR, 0, 0, 0, PH );
			for ( int i = 0; i < N + SURPLUS + 1; ++ i )
			{
				int bx0 = BX + W * i;
				g.DrawLine ( PEN_BASE_BAR, bx0, 0, bx0, PH );
			}

			//横線
			g.DrawLine ( PEN_BASE_BAR, 0, 0, PW, 0 );
#if false
			for ( int i = 0; i < 6; ++ i )
			{
				int by0 = BY + H * i;
				g.DrawLine ( PEN_BASE_BAR, 0, by0, PW, by0 );
			}
			g.DrawLine ( PEN_BASE_BAR, 0, PH - 1, PW, PH - 1 );
#endif

#if false
			g.DrawLine ( PEN_BAR, BX, 0, BX, TH );
			g.DrawLine ( PEN_BAR, 0, BY, PW, BY );
			g.DrawLine ( PEN_BAR, 0, 0, PW, 0 );
			g.DrawLine ( PEN_BAR, 0, BY + H, PW, BY + H );
#endif

			//---------------------------------------------------------
			//選択位置表示
			g.DrawRectangle ( Pens.Firebrick, BX + selectedScript * W, 0, W, PH - 1 );	//フレーム選択
			
			}	//using
		}

		//オブジェクトが設定されていない状態でのIDE表示
		private void pictureBox1_Paint_Default ( object sender, PaintEventArgs e )
		{
		}

		//DrawStringFunc
		private void DrawStr_0 ( Graphics g, string str, int i, StringFormat sf )
		{
			Rectangle r = new Rectangle ( 0, H * i + 2, BX, H );
			using ( Font FONT0 = new Font ( "Meiryo", 8.0f ) )
			{
				g.DrawString ( str, FONT0, StrColorBrushes [ i ], r, sf );
			}
		}
		private readonly Brush[] StrColorBrushes =
		{
			Brushes.Gray, 
			Brushes.Gray, 
			new SolidBrush ( ScpCntColor [ 1 ] ),
			Brushes.Teal, 
			Brushes.LightCoral, 
			Brushes.Goldenrod, 
			Brushes.Turquoise, 
			Brushes.LimeGreen, 
		};

		//DrawScript
		private void DrawScp ( Graphics g, int count, int i, Color clr )
		{
			Rectangle r = new Rectangle ( BX + (W * count), H * i, W, H );
			g.FillRectangle ( new SolidBrush ( clr ), r );
		}
		private Color GetScpCntClr ( List<Rectangle> L_Rect, Color clr )
		{
			Color ret_clr = ( 0 < L_Rect.Count ) ? clr : Color.White;
			return ret_clr;
		}
		private Color GetScpEfGnClr ( BindingDictionary < EffectGenerate > BD_EfGn, Color clr )
		{
			Color ret_clr = ( 0 < BD_EfGn.Count () ) ? clr : Color.White;
			return ret_clr;
		}
		private static readonly Color[] ScpCntColor =
		{
			Color.White,
			Color.FromArgb ( 0x80, 0x80, 0xff ),
			Color.FromArgb ( 0xc0, 0xff, 0xc0 ),
			Color.FromArgb ( 0xff, 0xc0, 0x80 ),
			Color.FromArgb ( 0xff, 0xff, 0xb0 ),
			Color.FromArgb ( 0xc0, 0xff, 0xff ), 
		};


		//----------------------------------------------------------------------------------
		//	マウスイベント
		//----------------------------------------------------------------------------------
		bool bDrag = false;	//ドラッグ中

		//マウスダウンで選択
		private void pictureBox1_MouseDown ( object sender, MouseEventArgs e )
		{
			SelectFromPos ();
		}

		private void pictureBox1_MouseMove ( object sender, MouseEventArgs e )
		{
			if ( bDrag )
			{
				Point pos = GetCell ();
				EditCompend.SelectedSpanEnd = pos.X;
				pictureBox1.Invalidate ();
			}
		}

		private void pictureBox1_MouseUp ( object sender, MouseEventArgs e )
		{
			Point pos = GetCell ();
			bDrag = false;
			pictureBox1.Invalidate ();
		}

		//----------------------------------------------------------------------------------
		//	スクリプト配列
		//----------------------------------------------------------------------------------
		//挿入
		private void Btn_ScpInc_Click ( object sender, System.EventArgs e )
		{
			Script scp = Make_Script_with_Rect ();
			DispCompend.Disp ();
		}

		//追加
		private void Btn_ScpAdd_Click ( object sender, System.EventArgs e )
		{
			Script scp = Make_Script_with_Rect ();
			EditCompend.AddScript ( scp );
			DispCompend.Disp ();
		}

		//削除
		private void Btn_ScpDel_Click ( object sender, System.EventArgs e )
		{
			EditCompend.RemScript ();
			ReSelect ();
			DispCompend.Disp ();
		}

		//複数挿入
		private void Btn_MlcIns_Click ( object sender, System.EventArgs e )
		{
			Script scp = Make_Script_with_Rect ();
			EditCompend.MultiInsert ( scp );
			DispCompend.Disp ();
		}

		//複数追加
		private void Btn_MulAdd_Click ( object sender, System.EventArgs e )
		{
			Script scp = Make_Script_with_Rect ();
			EditCompend.MultiAdd ( scp );
			DispCompend.Disp ();
		}

		//複数削除
		private void Btn_MulDel_Click ( object sender, System.EventArgs e )
		{
			EditCompend.MultiRem ();
			ReSelect ();
			DispCompend.Disp ();
		}


		//----------------------------------------------------------------------------------
		//	グループ
		//----------------------------------------------------------------------------------
		//作成
		private void button1_Click ( object sender, System.EventArgs e )
		{
			EditCompend ec = EditCompend;
			EditScript es = ec.EditScript;

			int start = ec.SelectedSpanStart;
			int end = ec.SelectedSpanEnd;

			List < Script > lsScp = Sqc.ListScript;
			es.MakeGroup ( lsScp, start, end );

			pictureBox1.Invalidate ();
		}

		//解除
		private void button2_Click ( object sender, System.EventArgs e )
		{
			int count = 0;
			int group = 0;
			//グループ解除対象を取得
			foreach ( Script s in Sqc.ListScript )
			{
				if ( EditCompend.SelectedScript.Frame == count )
				{
					group = s.Group;
					break;
				}
				++ count;
			}
			if ( count == Sqc.ListScript.Count ) { return; }

			//対象グループを解除
			foreach ( Script s in Sqc.ListScript )
			{
				if ( group == s.Group )
				{
					s.Group = 0;
				}
			}
//			-- nGroup;

			pictureBox1.Invalidate ();
		}

		//同一化
		private void Btn_Paste_Click ( object sender, System.EventArgs e )
		{
			EditCompend ec = EditCompend;
			EditScript es = ec.EditScript;
			es.PasteGroup ( ec.SelectedScript );

			pictureBox1.Invalidate ();
		}

		//----------------------------------------------------------------------------------
		//	選択
		//----------------------------------------------------------------------------------

		//再選択
		private void ReSelect ()
		{
			int i = EditCompend.SelectedScript.Frame;
			if ( i >= Sqc.ListScript.Count ) { i = Sqc.ListScript.Count - 1; }

			EditCompend.SelectFrame ( i );
			EditCompend.SelectedSpanStart = i;
			EditCompend.SelectedSpanEnd = i;

			Assosiate ();
			DispCompend.Disp ();
		}

		//マウス位置からフレーム表の升目位置を取得する
		private Point GetCell () 
		{
			//マウス位置をコントロールのクライアント位置に直す
			Point pt = pictureBox1.PointToClient ( Cursor.Position );
			if ( pt.X < BX || pt.Y < BY ) { return new Point ( 0, 0 ); }

			//升目に合わせる
			int pos_x = ( pt.X - BX ) / W;
			int pos_y = ( pt.Y - BY ) / H;
//			STS_TXT.Trace ( pos_x.ToString() + "," + pos_y.ToString () );
			return new Point ( pos_x, pos_y );
		}

		//位置から対象を選択
		private void SelectFromPos ()
		{
			EditCompend ec = EditCompend;
			Point pos = GetCell ();
			ec.SelectFrame ( pos.X );
			ec.SelectedSpanStart = pos.X;
			ec.SelectedSpanEnd = pos.X;
			bDrag = true;

			//グループ
			EditScript es = ec.EditScript;
			es.SelectGroup ( ec.SelectedScript.Group );

			//選択位置をステータスに表示
			Script s = EditCompend.SelectedScript;
			STS_TXT.Trace ( "Frame:" + s.Frame.ToString () + ", Group[" + s.Group + "]" );

			//更新
			Assosiate ();
//			DispCompend.Disp ();
			All_Ctrl.Inst.UpdateData ();
		}

		//１つ後ろを選択
		private void SelectBack ()
		{
			if ( Sqc is null ) { return; }
			if ( Sqc.ListScript is null ) { return; }
			if ( Sqc.ListScript.Count < 2 ) { return; }

			int i = EditCompend.SelectedScript.Frame;
			if ( EditCompend.SelectedScript.Frame < 2 ) { return; }

			EditCompend.SelectFrame ( i - 1 );
			EditCompend.SelectedSpanStart = i - 1;
			EditCompend.SelectedSpanEnd = i - 1;

			//更新
			Assosiate ();
			DispCompend.Disp ();
		}

		//関連付け
		private void Assosiate ()
		{
			Script scp = EditCompend.SelectedScript;
			EditCompend.Assosiate ();
			FormScript.Inst.Assosiate ( scp, Sqc );
			FormRoute.Inst.Assosiate ( scp );
//			FormRect.Inst.Assosiate ( scp );
			FormRect2.Inst.Assosiate ( scp );
			FormEfGnrt.Inst.Assosiate ( scp );
		}



		//内部関数
		//仮枠つきスクリプト
		Script Make_Script_with_Rect ()
		{
			Script scp = new Script ();
			scp.ListCRect.Add ( new Rectangle ( -50, -350, 120, 350 ) );
			scp.ListHRect.Add ( new Rectangle ( -60, -360, 130, 370 ) );
			return scp;
		}
	}
}
