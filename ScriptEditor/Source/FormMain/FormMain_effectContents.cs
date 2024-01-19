using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Diagnostics;


namespace ScriptEditor
{

	public partial class FormMain : Form
	{
		//==================================================================================
		//	[エフェクト]
		//==================================================================================

		//[エフェクト]初期化
		private void tabEffect_Load ()
		{
			EditEffect ee = editChara.EditEffect;
			DispGarnish dg = dispChara.dispGarnish;

			fT_Effect.Load ( ee, dg, "エフェクト", splitContainer3.Panel2 );
			EffectContents_Load ();
			EfScriptContents_Load ();

			// 枠指定(エフェクト)
			//@todo 枠ボタン(ツール)からの編集機能を追加する

			//@todo ラジオボタン　クラス化



			//スクリプト表示にコントロールを設定
			pbi_Effect.Load ( ee, dg,
				this.rb_efImageMove, this.rb_efORect, this.rb_efHRect, this.rb_efARect, this.rb_efCRect,
				this.rb_EfGnrtMove );

			//エフェクト表示にコントロールを設定
			dispChara.dispGarnish.Load ( editChara.EditEffect, fT_Effect );

			//複数編集
			ee.spanScript = true;
			lbl_EfSpan.BackColor = Color.FromArgb ( 0xff, 0xd0, 0xff, 0xd0 );
		}

		//----------------------------------------------------------------------------------
		//イメージ選択
		//----------------------------------------------------------------------------------
		private void tb_EfImage_Click ( object sender, EventArgs e )
		{
			//動作条件
			if ( null == chara ) { return; }
//			if ( 0 == chara.ListEfImageData.Count ) { return; }

			Script script = editChara.EditEffect.GetScript ();
			if ( null == script ) { return; }

			//選択用別フォーム
			Form_ImagePreview form_ImagePreview = new Form_ImagePreview ( chara.ListEfImageData );

			//フォームを開く
			if ( form_ImagePreview.ShowDialog ( this ) == DialogResult.OK )
			{
				//フォームをOKで閉じる

				//イメージの設定
				script.imgIndex = form_ImagePreview.GetImageIndex ();

				//スクリプト変更後に他スクリプトにコピーするかチェックする
//				CheckSetOtherScript ();
				editChara.EditEffect.CheckSetOtherScript ();

				//表示の更新
				dispChara.dispGarnish.Disp ();
			}

			form_ImagePreview.Dispose ();
		}

	}
}
