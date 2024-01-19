using System.ComponentModel;

namespace ScriptEditor
{
	using BL_DCT_Sqc = BindingDictionary < Sequence >;
	using BL_Sqc = BindingList < Sequence >;

	//================================================================
	//	<コンペンド> 		【一覧】シークエンスの継承であるアクションやエフェクトのリストを扱う
	//		┣[] シークエンス
	//		┣[] イメージ
	//================================================================
	public class Compend
	{
		//---------------------------------------------------------------------
		//シークエンスリスト	
		public BL_DCT_Sqc Bldct_sqc = new BL_DCT_Sqc ();

		//イメージリスト
		public ImageList ListImage { get; } = new ImageList ();

		//---------------------------------------------------------------------
		//クリア
		public void Clear ()
		{
			BL_Sqc bl_sqc = Bldct_sqc.GetBindingList ();
			foreach ( Sequence seq in bl_sqc )
			{
				seq.Clear ();
			}
			Bldct_sqc.Clear ();

			ListImage.Clear ();
		}

		//コピー
		public virtual void Copy ( Compend srcCompend )
		{
			Clear ();

			BL_Sqc bl_sqc = Bldct_sqc.GetBindingList ();
			BL_Sqc src_bl_sqc = srcCompend.Bldct_sqc.GetBindingList ();
			foreach ( Sequence seq in bl_sqc )
			{
				bl_sqc.Add ( new Sequence ( seq ) );
			}
			CopyImageList ( srcCompend );
		}

		//イメージデータ部分のコピー
		public void CopyImageList ( Compend srcCompend )
		{
			//イメージデータはディープコピーする
			BindingList < ImageData > bl_imgdt = srcCompend.ListImage.GetBindingList();
			foreach ( ImageData imageData in bl_imgdt )
			{
				ImageData tempImageData = new ImageData ( imageData );
				this.ListImage.Add ( tempImageData.Name, tempImageData );
			}
		}

		//全シークエンス内でのスクリプト最大数
		public int MaxNumScript ()
		{
			int maxNumScript = 0;
			BL_Sqc bl_sqc = Bldct_sqc.GetBindingList ();
			foreach ( Sequence seq in bl_sqc )
			{
				if ( maxNumScript < seq.ListScript.Count )
				{
					maxNumScript = seq.ListScript.Count;
				}
			}
			return maxNumScript;
		}
	}


	//================================================================
	//コンペンド継承：アクションの集合
	//Behavior	ビヘイビア【行動様式、振舞】
	//================================================================
	public class Behavior : Compend
	{
		//Action型指定 インデクサ
		public Action this [ int i ]
		{
			set { base.Bldct_sqc.GetBindingList () [ i ] = value; }
			get { return base.Bldct_sqc.GetBindingList () [ i ] as Action; }
		}

		//Action型指定 コピー
		public override void Copy ( Compend srcCompend )
		{
			base.Clear ();
			BL_Sqc src_bl_sqc = srcCompend.Bldct_sqc.GetBindingList ();
			foreach ( Action ac in src_bl_sqc )
			{
				base.Bldct_sqc.GetBindingList ().Add ( new Action ( ac ) );
			}
			CopyImageList ( srcCompend );
		}
	}


	//================================================================
	//コンペンド継承：エフェクトの集合
	//Garnish	ガーニッシュ【装飾品、付け合わせ】
	//================================================================
	public class Garnish : Compend
	{
		//Effect型指定 インデクサ
		public Effect this [ int i ]
		{
			set { base.Bldct_sqc.GetBindingList () [ i ] = value; }
			get { return base.Bldct_sqc.GetBindingList () [ i ] as Effect; }
		}

		//Effect型指定 コピー
		public override void Copy ( Compend srcCompend )
		{
			base.Clear ();
			BL_Sqc src_bl_sqc = srcCompend.Bldct_sqc.GetBindingList ();
			foreach ( Effect ac in src_bl_sqc )
			{
				base.Bldct_sqc.GetBindingList ().Add ( new Effect ( ac ) );
			}
			CopyImageList ( srcCompend );
		}
	}


}
