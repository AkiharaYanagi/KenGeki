using System.Windows.Forms;
using System.Drawing;
using ScriptEditor;
using System.Diagnostics;

namespace ScriptEditor
{
	using PR = Properties.Resources;
	using GKC_ST = GameKeyCommand.GameKeyCommandState;

	public class DispCommand
	{
		//対象データ
		public Command Cmd { get; set; } = new Command ();

		//選択中キー
		public SelectKey SlctKey { get; set; } = new SelectKey ();

		//レバー選択位置
//		public int SelectedIndex = 0;
		public readonly int [] ItoLvr = { 1, 2, 3, 6, 9, 8, 7, 4 }; 

		//データ設定
		public void Set ( Command cmd )
		{
			Cmd = cmd;
		}

		//--------------------------------------------------------------------------
		//内部使用定数
		//画像指定(リソース) 
#if false
		private readonly Image [,] AryImgLvr = new Image [,]
		{
			{ PR.Off, PR.CMD_On_01, PR.CMD_Push_01, PR.CMD_Rele_01, PR.wild, PR.CMD_Is_01, PR.CMD_Nis_01 }, 
			{ PR.Off, PR.CMD_On_02, PR.CMD_Push_02, PR.CMD_Rele_02, PR.wild, PR.CMD_Is_02, PR.CMD_Nis_02 }, 
			{ PR.Off, PR.CMD_On_03, PR.CMD_Push_03, PR.CMD_Rele_03, PR.wild, PR.CMD_Is_03, PR.CMD_Nis_03 }, 
			{ PR.Off, PR.CMD_On_06, PR.CMD_Push_06, PR.CMD_Rele_06, PR.wild, PR.CMD_Is_06, PR.CMD_Nis_06 }, 
			{ PR.Off, PR.CMD_On_09, PR.CMD_Push_09, PR.CMD_Rele_09, PR.wild, PR.CMD_Is_09, PR.CMD_Nis_09 }, 
			{ PR.Off, PR.CMD_On_08, PR.CMD_Push_08, PR.CMD_Rele_08, PR.wild, PR.CMD_Is_08, PR.CMD_Nis_08 }, 
			{ PR.Off, PR.CMD_On_07, PR.CMD_Push_07, PR.CMD_Rele_07, PR.wild, PR.CMD_Is_07, PR.CMD_Nis_07 }, 
			{ PR.Off, PR.CMD_On_04, PR.CMD_Push_04, PR.CMD_Rele_04, PR.wild, PR.CMD_Is_04, PR.CMD_Nis_04 }, 
			{ PR.Off, PR.CMD_On_2E, PR.CMD_Push_2E, PR.CMD_Rele_2E, PR.wild, PR.CMD_Is_2E, PR.CMD_Nis_2E }, 
			{ PR.Off, PR.CMD_On_6E, PR.CMD_Push_6E, PR.CMD_Rele_6E, PR.wild, PR.CMD_Is_6E, PR.CMD_Nis_6E }, 
			{ PR.Off, PR.CMD_On_8E, PR.CMD_Push_8E, PR.CMD_Rele_8E, PR.wild, PR.CMD_Is_8E, PR.CMD_Nis_8E }, 
			{ PR.Off, PR.CMD_On_4E, PR.CMD_Push_4E, PR.CMD_Rele_4E, PR.wild, PR.CMD_Is_4E, PR.CMD_Nis_4E }, 
		};
#endif

		private readonly Image [] AryImgLvr_ = new Image []
		{
			PR.CmdAr_OFF, PR.CmdAr_On, PR.CmdAr_Push, PR.CmdAr_Rele, PR.CmdAr_Wild, PR.CmdAr_Is, PR.CmdAr_Nis,
		};

		private readonly Image [,] AryImgBtn = new Image [,]
		{
			{ PR.Off, PR.CmdOn_L , PR.CmdPush_L , PR.CmdRele_L , PR.wild, PR.CmdIs_L , PR.CmdNis_L  }, 
			{ PR.Off, PR.CmdOn_Ma, PR.CmdPush_Ma, PR.CmdRele_Ma, PR.wild, PR.CmdIs_Ma, PR.CmdNis_Ma }, 
			{ PR.Off, PR.CmdOn_Mb, PR.CmdPush_Mb, PR.CmdRele_Mb, PR.wild, PR.CmdIs_Mb, PR.CmdNis_Mb }, 
			{ PR.Off, PR.CmdOn_H , PR.CmdPush_H , PR.CmdRele_H , PR.wild, PR.CmdIs_H , PR.CmdNis_H  }, 
		};
		//--------------------------------------------------------------------------

		private const int W = 32;	//升幅
		private const int H = 32;	//升高
		private const int BX = 32;	//基準X
		private const int BY = 32;	//基準Y

		//描画用定数
		private const int CULUMN = 16;
		private const int ROW = 6;

		public const int CULUMN_WIDTH = 48;
		public const int ROW_HEIGHT = 48;

		public const int DISIT_REVISED_POS_X = 24;
		public const int DISIT_REVISED_POS_Y = 14;
		//--------------------------------------------------------------------------

		//描画
		public void Disp ( PaintEventArgs e )
		{
//			Debug.WriteLine ("pb_Command1_Paint");			
			Graphics g = e.Graphics;

			//リソース使用の宣言
			using ( Font font0 = new Font ( "MS UI Gothic", 14, FontStyle.Regular ) )
			using ( Font font1 = new Font ( "MS UI Gothic", 12, FontStyle.Regular ) )
			using ( Pen pen0 = new Pen ( Color.FromArgb ( 0x80, 0x80, 0xff ), 1 ) )
			using ( Pen pen1 = new Pen ( Color.FromArgb ( 0xa0, 0xa0, 0xa0 ), 1 ) )
			using ( Pen pen2 = new Pen ( Color.FromArgb ( 0x40, 0x40, 0x40 ), 1.5f ) )
			using ( Brush brush_Rect = new SolidBrush ( Color.FromArgb ( 255, 255, 255, 255 ) ) )
			using ( Brush brush_BG = new SolidBrush ( Color.FromArgb ( 255, 220, 220, 220 ) ) )
			using ( Brush brush_BG_ALL = new SolidBrush ( Color.FromArgb ( 255, 180, 180, 180 ) ) )
			using ( Brush brush_Not = new SolidBrush ( Color.FromArgb ( 63, 255, 63, 63 ) ) )
			{

			//描画用一時変数
			Bitmap bmp = new Bitmap ( CULUMN_WIDTH * CULUMN, ROW_HEIGHT * ROW + 1 );
			int w = e.ClipRectangle.Width;
			int h = e.ClipRectangle.Height;
			const int CW = CULUMN_WIDTH;
			const int RH = ROW_HEIGHT;

			//フレーム数
			StringFormat sf = new StringFormat { Alignment = StringAlignment.Center };
			for ( int i = 0; i < w / CULUMN_WIDTH; ++i )
			{
				g.DrawString ( i.ToString (), font0, Brushes.Gray, CW + DISIT_REVISED_POS_X + i * CW, DISIT_REVISED_POS_Y, sf );
			}

			//見出：レバー(十字),ボタン(L,Ma,Mb,H)
			g.DrawImage ( PR.arrow, 0, RH * 1, CW, RH );
			g.DrawImage ( PR.command_L , 0, RH * 2, CW, RH );
			g.DrawImage ( PR.command_Ma, 0, RH * 3, CW, RH );
			g.DrawImage ( PR.command_Mb, 0, RH * 4, CW, RH );
			g.DrawImage ( PR.command_H , 0, RH * 5, CW, RH );

			//全体背景
			//枠外
			g.FillRectangle ( brush_BG_ALL, 0, RH * 6, w, h - RH * 6 );
			//範囲外
			int n = Cmd.ListGameKeyCommand.Count;
			g.FillRectangle ( brush_BG, CW + (CW * n), RH, w - (CW * n), RH * 5 );


			//コマンドキーの表示
			int iFrame = 0;
			foreach ( GameKeyCommand gc in Cmd.ListGameKeyCommand )
			{
				//レバー
				GKC_ST gkcst = gc.GetLvrSt ();
				int indexLvr = (int)gc.GetLever ();
				// g.DrawImage ( AryImgLvr [ indexLvr, (int)gkcstL ], CW + CW * iFrame, RH, CW, RH );
				for ( int i = 0; i < GameKeyData.LVR_NUM; ++ i )
				{
					int imgIndex = (int) gc.Lvr [ i ];
						
					int pos_i = ItoLvr [ i ] - 1;
					int x = CW + CW * iFrame + ( 16 * (pos_i % 3) );
					int y = RH + ( 16 * (2 - (pos_i / 3) ) );

					g.DrawImage ( AryImgLvr_ [ imgIndex ], x, y, 16, 16 );
					
					if ( GKC_ST.KEY_WILD != gc.Lvr [ i ] )
					{
						g.DrawString ( ItoLvr[ i ].ToString (), font1, Brushes.Black, x + 2, y );
					}
				}

				//ボタン
				int iBtn = 0;
				for ( int i = 0; i < GameKeyData.BTN_NUM; ++ i )
				{
					GKC_ST gkcstB = gc.Btn [ iBtn ];
					g.DrawImage ( AryImgBtn [ iBtn, (int)gkcstB ], CW + CW * iFrame, RH * 2 + RH * iBtn, CW, RH );
					++ iBtn;
				}

				//否定
				if ( gc.Not )
				{
					g.FillRectangle ( brush_Not, CW + (CW * iFrame), RH, CW, RH * 5 );
				}

				++ iFrame;
			}


			//基準線
			g.DrawLine ( pen0, CULUMN_WIDTH, 0, CULUMN_WIDTH, h );
			g.DrawLine ( pen0, 0, ROW_HEIGHT, w, ROW_HEIGHT );
			for ( int i = 0; i < w / 20; ++i )
			{
				g.DrawLine ( pen2, CW * ( i + 2 ), 0, CW * ( i + 2 ), h );
			}
			for ( int i = 0; i < h / 20; ++i )
			{
				g.DrawLine ( pen1, 0, RH * ( i + 2 ), w, RH * ( i + 2 ) );
			}

			//ボタンカーソル
			if ( SlctKey.Selecting )
			{
				int crs_x = CW + CW * SlctKey.Frame;
				int crs_y = RH + RH * (int)SlctKey.Kind;
				g.DrawImage ( PR.cursor, crs_x - 4, crs_y - 4, CW + 8, RH + 8 );
			}

			//レバーカーソル
			int si_x = SlctKey.SelectedIndex % 3;
			int si_y = SlctKey.SelectedIndex / 3;
			int Lvr_w = 16;
			int Lvr_h = 16;
			int Lvr_x = CW + ( Lvr_w * si_x ) - 3 + CW * SlctKey.Frame;
			int Lvr_y = CW + ( Lvr_h * si_y ) - 3;
			g.DrawImage ( PR.LvrCur, Lvr_x, Lvr_y, Lvr_w + 6, Lvr_h + 6 );

			}	//リソース使用
		}
	}

}
