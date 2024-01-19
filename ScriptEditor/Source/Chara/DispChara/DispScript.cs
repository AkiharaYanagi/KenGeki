using System.Windows.Forms;
using System.ComponentModel;


namespace ScriptEditor
{
	using BD_IMGD = BindingDictionary < ImageData >;
	using BL_Sqc = BindingList < Sequence >;

	//==================================================================================
	//	各フォームからコントロールを設定し、Scriptを受けて関連付けと表示をする
	//	関連するすべての項目を再描画する
	//==================================================================================
	public class DispScript
	{
		//-----------------------------------------------------------
		//キャラデータの参照
///		public BD_IMGD ListImage { get; set; } = null;		//イメージリストの参照
		public BL_Sqc ListEf { get; set; } = null;			//Efリストの参照
//		public BD_IMGD ListEfImage { get; set; } = null;	//Efイメージリストの参照

		public BD_IMGD ImageList { get; set; } = null;		//イメージリストの参照
		public BD_IMGD EfImageList { get; set; } = null;	//Efイメージリストの参照

		//-----------------------------------------------------------
		//コントロールの参照
		public TextBox TbFrame { get; set; } = null;		//フレーム数
		public TextBox TbImage { get; set; } = null;		//イメージ名
		public Ctrl_Image CtrlImg { get; set; } = null;		//画像表示
//		private DispRefPt dispRefPt = new DispRefPt();      //イメージ表示差分位置 ( X, Y )

		//-----------------------------------------------------------
		//スクリプト内容表示の子(Disp-)
		public TB_Number tbaPower { get; set; }		//攻撃値

//		private DispRefPt dispRefVel = new DispRefPt ();		//速度 ( VX, VY )
//		private DispRefPt dispRefAcc = new DispRefPt ();        //加速度 ( AX, AY )

#if false
		//枠リストの編集・表示
		public DispRects DispRects = new DispRects ();
		public D_LRRct dispCRect = new D_LRRct ( Color.FromArgb ( 0x400000ff ) );	//ぶつかり枠リスト
		public D_LRRct dispARect = new D_LRRct ( Color.FromArgb ( 0x40ff0000 ) );	//攻撃枠リスト
		public D_LRRct dispHRect = new D_LRRct ( Color.FromArgb ( 0x4000ff00 ) );	//当り枠リスト
		public D_LRRct dispORect = new D_LRRct ( Color.FromArgb ( 0x40ffff00 ) );	//相殺枠リスト
#endif

#if false
		//エフェクト生成
		public DispEffectGenerate dispEfGnrt = new DispEffectGenerate ();
#endif


		//==================================================================================

		//コンストラクタ
		public DispScript ()
		{
		}

		//コンポーネント参照の設定
		public void Set ( Ctrl_Compend ctrlCmpd )
		{
			CtrlImg = ctrlCmpd.Ctrl_Img;	//イメージ表示
//			TbFrame = ctrlCmpd.tBN_Frame;	//フレーム数
//			TbImage = ctrlCmpd.tB_Image;	//イメージ選択

			//枠
//			DispRects.Set ( FormRect.ctrl_ );
		}

#if false
		public void SetCtrlScript ( Ctrl_Script ctrlScript )
		{
			tbaPower = ctrlScript.tBN_Power;
		}
#endif

#if false

		//初回一度の設定
		public void Load
		(
			EditCompend ec, DispCompend dc, BL_ImgDt li,　BL_ImgDt lei, BL_Sqc le, 
			PB_Image _pbImage, PB_Image0 pbImg0,
			TextBox _tbFrame, TextBox _tbImage, TB_Number _tba_PtX, TB_Number _tba_PtY, 
			TB_Number _tba_VelX, TB_Number _tba_VelY, TB_Number _tba_AccX, TB_Number _tba_AccY,
			TB_Number _tbaPower
		)
		{
			//スクリプト取得のためのエディットキャラの設定
//			editCompend = ec;

			//データ参照
			ListImage = li;
			ListEf = le;
			ListEfImage = lei;

			//コントロールの設定
			tbFrame = _tbFrame;
			tbImage = _tbImage;

//			pbImage = _pbImage;
			PbImg0 = pbImg0;

			//各テキストボックスにスクリプトアドレスを設定
			dispRefPt.Load ( ec, dc, _tba_PtX, _tba_PtY,
				new ScriptAddress ( ScriptAddress.ITEM.POS_X, 0 ),
				new ScriptAddress ( ScriptAddress.ITEM.POS_Y, 0 ) );
			dispRefVel.Load ( ec, dc, _tba_VelX, _tba_VelY,
				new ScriptAddress ( ScriptAddress.ITEM.VEL_X, 0 ),
				new ScriptAddress ( ScriptAddress.ITEM.VEL_Y, 0 ) );
			dispRefAcc.Load ( ec, dc, _tba_AccX, _tba_AccY,
				new ScriptAddress ( ScriptAddress.ITEM.ACC_X, 0 ),
				new ScriptAddress ( ScriptAddress.ITEM.ACC_Y, 0 ) );

			//枠リスト
			dispCRect.Load ( ec, dc, Form_Rect.CntxtListRefCRect );
			dispARect.Load ( ec, dc, Form_Rect.CntxtListRefARect );
			dispHRect.Load ( ec, dc, Form_Rect.CntxtListRefHRect );
			dispORect.Load ( ec, dc, Form_Rect.CntxtListRefORect );

			//攻撃値
			tbaPower = _tbaPower;
			tbaPower.Load ( ec, dc );
			tbaPower.scriptAddress = new ScriptAddress( ScriptAddress.ITEM.POWER, 0 );

			//動作状態確認
			if ( ! Condition () ) { throw new System.Exception (); }
		}

		//枠のセット
		public void SetCRect ( Rectangle rect ) { dispCRect.SetRect ( rect ); }
		public void SetARect ( Rectangle rect ) { dispARect.SetRect ( rect ); }
		public void SetHRect ( Rectangle rect ) { dispHRect.SetRect ( rect ); }
		public void SetORect ( Rectangle rect ) { dispORect.SetRect ( rect ); }

		//消去
		public void Clear ()
		{
//			if ( null == pbImage ) { return; }
//			pbImage.Image = null;
		}
#endif

		//キャラ読込時
		public void SetCharaData ( Chara chara )
		{
			//データ参照
			ImageList = chara.behavior.BD_Image;
			ListEf = chara.garnish.BD_Sequence.GetBindingList ();
			EfImageList = chara.garnish.BD_Image;
		}

		//更新
		public void UpdateData ( Script script )
		{
			FormScript.Inst.Update ();
		}

		//内容表示
		public void Disp ( Script script )
		{
#if false

//			Script script = editCompend.GetScript ();

			//動作条件
			if ( null == script ) { return; }
			if ( ! Condition () ) { return; }

			//描画対象
			Bitmap bmp = new Bitmap ( PbImg.Width, PbImg.Height );
			Graphics g = Graphics.FromImage ( bmp );

			//基準線
			g.DrawLine ( penWhite, new Point ( ptPbImageBase.X, 0 ), new Point ( ptPbImageBase.X, PbImg.Height ) );
			g.DrawLine ( penWhite, new Point ( 0, ptPbImageBase.Y ), new Point ( PbImg.Width, ptPbImageBase.Y ) );

			//----------------------------------------
			//スクリプト内容表示

			//フレーム数
			TbFrame.Text = script.Frame.ToString ();

			//イメージID
			if ( ListImage.Count <= script.ImgIndex ) { return; }
			ImageData imgdt = ListImage[ script.ImgIndex ];

			//イメージ
			Image img = imgdt.Img;
			if ( null != img )
			{
				TbImage.Text = imgdt.Name;
				int x = ptPbImageBase.X + script.RefPt.x.i;
				int y = ptPbImageBase.Y + script.RefPt.y.i;
				g.DrawImage ( img, x, y, img.Width, img.Height );
			}
			else
			{
				TbImage.Text = "";
			}
#endif

#if false
			//エフェクトID
			foreach ( EffectGenerate efGnrt in script.ListGenerateEf )
			{
				//対象エフェクトとイメージを取得
				int efID = efGnrt.id.i;
				if ( ListEf.Count <= efID ) { return; }
				Effect ef = ( Effect ) ListEf[ efID ];
				ImageData efImgdt = ListEfImage[ ef.ListScript[ 0 ].imgIndex ];

				//エフェクト
				if ( null == efImgdt ) { continue; }
				if ( null == efImgdt.Img ) { continue; }

				//エフェクトのスクリプトからポイントを取得
				Script efSc = ef.ListScript[ 0 ];
				Point efPt = PointUt.PtAdd ( ptPbImageBase, efSc.RefPt.Get () );
				efPt = PointUt.PtAdd ( efPt, efGnrt.ptGnrt.Get() );	

				Image efImg = efImgdt.Img;
				g.DrawImage ( efImg, efPt.X, efPt.Y, efImg.Width, efImg.Height );
			}
#endif



#if false
			//RefPt
			dispRefPt.Disp ( script.RefPt );			//位置
			dispRefVel.Disp ( script.RefVel );			//速度
			dispRefAcc.Disp ( script.RefAcc );			//加速度

			//枠
			dispCRect.Disp ( g, ptPbImageBase );	//ぶつかり枠リストの描画
			dispARect.Disp ( g, ptPbImageBase );	//攻撃枠リストの描画
			dispHRect.Disp ( g, ptPbImageBase );	//当り枠リストの描画
			dispORect.Disp ( g, ptPbImageBase );	//相殺枠リストの描画

			//攻撃力
			tbaPower.Text = script.Power.i.ToString();

			//エフェクト生成
			dispEfGnrt.Disp ( script );

#endif
			//----------------------------------------
			//イメージの反映
			//PbImg.SetImg ( bmp );
			CtrlImg.Invalidate ();
		}
		
		//動作条件
		//戻値：true 可動, false 不可
		private bool Condition ()
		{
			if ( null == CtrlImg ) { return false; }
			return true;
		}
	}
}

