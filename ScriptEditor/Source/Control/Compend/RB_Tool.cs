using System;
using System.Windows.Forms;

namespace ScriptEditor
{
	//ツール用ラジオボタン共通クラス
	public class RB_Tool : RadioButton
	{
		//ツールチップ
		private ToolTip TLTP = new ToolTip ();

		//対応ツール
		public ToolImg Tl_Img { get; set; } = null;

		//コントロール
		public Ctrl_Image CtrlImg = null;
		
		//選択したツール
//		public ToolImg SelectedTool { get; set; } = null;


		//コンストラクタ
		public RB_Tool ()
		{
		}

		//設定
		public void SetCtrl ( ToolImg ti, Ctrl_Image ctrlImg, string str )
		{
			Tl_Img = ti;
			CtrlImg = ctrlImg;
			TLTP.SetToolTip ( this, str );
		}

		//イベント：チェック変更時
		protected override void OnCheckedChanged ( EventArgs e )
		{
			if ( this.Checked )
			{
				//選択時に上位コントロールに対応ツールを設定する
				CtrlImg.SelectingTool = Tl_Img;
			}
			base.OnCheckedChanged ( e );
		}
	}


}
