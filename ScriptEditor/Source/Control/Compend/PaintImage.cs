using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace ScriptEditor
{
	using BL_Sqc = BindingList < Sequence >;
	using BD_Sqc = BindingDictionary < Sequence >;
	using BL_EFGN = BindingList < EffectGenerate >;
	using BD_IMGD = BindingDictionary < ImageData >;

	public sealed class PaintImage 
	{
		//-----------------------------------------------------------
		//キャラデータの参照
		public BD_IMGD ListImage { get; set; } = null;		//イメージリストの参照
		public BD_Sqc BD_Ef { get; set; } = null;			//Efリストの参照
		public BL_Sqc ListEf { get; set; } = null;			//Efリストの参照
		public BD_IMGD ListEfImage { get; set; } = null;	//Efイメージリストの参照

		//イメージ表示基準位置 ( X, Y )
		public Point PtPbImageBase { get; set; } = new Point ( 250, 480 );

		//-----------------------------------------------------
		//内部使用
		private PictureBox PB_Image = new PictureBox();
		private readonly DispRects dispRects = new DispRects ();

		//-----------------------------------------------------
		//コントロールの設定
		public void SetCtrl ( PictureBox pb )
		{
			PB_Image = pb;
		}

		//キャラ読込時
		public void SetCharaData ( BD_IMGD bd_mainImg, BD_Sqc bd_ef, BD_IMGD bd_efImg )
		{
			//データ参照
			ListImage = bd_mainImg;
//			ListEf = lsEf;
			BD_Ef = bd_ef;
			ListEfImage = bd_efImg;
		}

		//内容表示
		public void Paint ( Script script )
		{
			if ( script is null ) { return; }

			//描画対象
			Bitmap bmp = new Bitmap ( PB_Image.Width, PB_Image.Height );
			Graphics g = Graphics.FromImage ( bmp );

			using ( Pen PenWhite = new Pen ( Color.White, 4 ) )
			{
			//基準線
			g.DrawLine ( PenWhite, new Point ( PtPbImageBase.X, 0 ), new Point ( PtPbImageBase.X, PB_Image.Height ) );
			g.DrawLine ( PenWhite, new Point ( 0, PtPbImageBase.Y ), new Point ( PB_Image.Width, PtPbImageBase.Y ) );
			}	//using
			
			//----------------------------------------
			//イメージ
			Image img = null;
			ImageData imgdt = ListImage.Get ( script.ImgName );
			if ( imgdt is null ) { img = MakeDammy ( script ); }
			else { img = imgdt.Img; }

			int x = PtPbImageBase.X + script.Pos.X;
			int y = PtPbImageBase.Y + script.Pos.Y;
			g.DrawImage ( img, x, y, img.Width, img.Height );

			//----------------------------------------
			//エフェクト
			BL_EFGN bl_efgn = script.BD_EfGnrt.GetBindingList ();
			foreach ( EffectGenerate efGnrt in bl_efgn )
			{
				//対象エフェクトとイメージを取得
#if false
				int efID = efGnrt.Id;
				if ( ListEf.Count <= efID ) { return; }
				Effect ef = ( Effect ) ListEf[ efID ];
				if ( null == ef ) { continue; }
				Script efSc = ef.ListScript[ 0 ];
#endif
				Effect ef = (Effect)BD_Ef.Get ( efGnrt.EfName );
				if ( ef is null ) { continue; }
				Script efSc = ef.ListScript[ 0 ];

				//エフェクトのスクリプトからイメージを取得
				ImageData efImgdt = ListEfImage.Get ( efSc.ImgName );
				if ( null == efImgdt ) { return; }
				Image efImg = efImgdt.Img;
				if ( null == efImg ) { continue; }

				//エフェクトのスクリプトから位置を取得
				Point efPt = PointUt.PtAdd ( PtPbImageBase, efSc.Pos );
				efPt = PointUt.PtAdd ( efPt, efGnrt.Pt );	

				//描画
				g.DrawImage ( efImg, efPt.X, efPt.Y, efImg.Width, efImg.Height );
			}

			//枠
			dispRects.Disp ( g, script, PtPbImageBase );	//枠リストの描画

			//----------------------------------------
			//イメージの反映
			PB_Image.Image = bmp;
			PB_Image.Refresh ();
		}

		//ダミー生成
		private Image MakeDammy ( Script script )
		{
			Image img = null;
			using ( Font font = new Font ( "メイリオ", 20 ) )
			{
			Bitmap bmp_nl = new Bitmap ( 512, 256 );
			Graphics g_nl = Graphics.FromImage ( bmp_nl );
			g_nl.DrawString ( "\"" + script.ImgName + "\"", font, Brushes.Orange, 64, 128 );
			g_nl.DrawString ( "is not exist.", font, Brushes.Orange, 64, 160 );
			img = bmp_nl;
			g_nl.Dispose ();
			}	//using
			return img;
		}
	}
}
