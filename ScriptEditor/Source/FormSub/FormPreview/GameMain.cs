using System;
using System.Windows.Forms;
using System.Drawing;

namespace ScriptEditor
{
	public class GameMain
	{
		public Chara Chara { get; set; } = null;
		public Sequence Seq { get; set; } = null;
		public PictureBox Pb { get; set; } = null;

		private int frame = 0;
		private readonly Point pt0 = new Point ( 250, 480 );

		public int FPS { get; set; } = 60000;
		public string SleepTime { get; set; } = "SleepTime";
		
		public int BMP_W = 640;
		public int BMP_H = 480;
		private Bitmap Bmp = null;
		public void SetBmpSize ( int w, int h )
		{
			Bmp.Dispose ();
			Bmp = new Bitmap ( w, h );
		}

		public GameMain ()
		{
			Bmp =  new Bitmap ( BMP_W, BMP_H );
		}

		public void SetCharaData ( Chara ch )
		{
			Chara = ch;
		}

		public void Init ()
		{
			frame = 0;
		}
		
		//--------------------------------------------
		public delegate void Setter ( Bitmap bmp );
		public Setter SetterBmp { get; set; } = null;

		//--------------------------------------------

		public void Move ()
		{
			++ frame;
			if ( Seq.ListScript.Count <= frame ) { frame = 0; }
		}


		public void Draw ()
		{
#if false
			//			int W = Pb.Width;
			//			int H = Pb.Height;
			int W = Bmp.Width;
			int H = Bmp.Height;
			
			if ( Seq.ListScript.Count <= frame ) { frame = 0; }
			Script scp = Seq.ListScript [ frame ];
			

			using ( Graphics g = Graphics.FromImage ( Bmp )  )
			using ( Pen PEN0 = new Pen ( Brushes.White, 4.0f ) )
			using ( Font FONT0 = new Font ( "Meiryo", 20.0f ) )
			{
			//クリア
			g.Clear ( Color.Empty );
			
			//基準線
			g.DrawLine ( PEN0, new Point ( 0, pt0.Y ), new Point ( W, pt0.Y ) );
			g.DrawLine ( PEN0, new Point ( pt0.X, 0 ), new Point ( pt0.X, H ) );

			//イメージ
			ImageData imgDt = Chara.behavior.BD_Image.Get ( scp.ImgName );
			Rectangle rect = new Rectangle ( PointUt.PtAdd ( pt0, scp.Pos ), new Size ( 512, 512 ) );
			if ( imgDt != null ) 
			{
				g.DrawImage ( imgDt.Img, rect );
			}


			//[F]
			g.DrawString ( frame.ToString () + "[F]", FONT0, Brushes.White, new Point ( 200, 2 ) );

			//FPS, sleepTime
			float f_fps = FPS / 1000f;
			g.DrawString ( f_fps.ToString ("0.000"), FONT0, Brushes.White, new Point ( 2, 2 ) );
			g.DrawString ( SleepTime, FONT0, Brushes.White, new Point ( 2, 25 ) );
			}	//using

			//描画
			Pb.Image = Bmp;
//			Pb.Invoke ( Setter0, bmp );
//			SetterBmp?.Invoke ( bmp );
#endif
		}
	}
}
