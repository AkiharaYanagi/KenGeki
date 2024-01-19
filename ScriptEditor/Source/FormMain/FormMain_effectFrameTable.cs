using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;


namespace ScriptEditor
{
	public partial class FormMain : Form
	{
		//==================================================================================
		//	タブ エフェクト
		//==================================================================================
	
		//chara
		//	 継承			Action, Effect : Sequence

		private void EffectContents_Load ()
		{
			//エフェクト内容表示にコントロールを設定
			dispChara.dispGarnish.dispEffect.Load
			(
				tb_EfName, this, editChara, editChara.EditEffect
			);
			

//			dispEffect.Disp ( editChara.GetEffect () );
//			dispCompend.Disp ();
		}

		private void EfScriptContents_Load ()
		{
			//エフェクト表示に設定
//			dispEfScript.chara = chara;
//			dispChara.dispGarnish.dispScript.chara = chara;

			//スクリプト表示にコントロールを設定
//			dispEfScript.Load ( this, editChara, pb_Effect, tb_EfFrame, tb_EfImage, 
//				tbN_EfX, tbN_EfY, tbN_EfVX, tbN_EfVY, tbN_EfAX, tbN_EfAY, 
//				tbN_EfPower );

			dispChara.dispGarnish.dispScript.Load
			(
				editChara.EditEffect, dispChara.dispGarnish, chara.ListEfImageData, 
				chara.ListEfImageData, chara.garnish.ListSequence, 
				pbi_Effect, tb_EfFrame, tb_EfImage,
				tbN_EfX, tbN_EfY, tbN_EfVX, tbN_EfVY, tbN_EfAX, tbN_EfAY,
				cb_ClcStEf, tbN_EfPower
			);
		}

		//[エフェクト]タブ選択時
		public void tabEffect_Selected ()
		{
			//何もしない
			if ( null == chara ) { return; }
			if ( 0 == chara.behavior.ListSequence.Count ) { return; }
			if ( 0 == chara.behavior.ListSequence[ 0 ].ListScript.Count ) { return; }

			//編集対象(アクション・エフェクト)の切替
			editCompend = editChara.EditEffect;
			dispCompend = dispChara.dispGarnish;
			frameTable = fT_Effect;
			
			//初期位置
//			editChara.EditEffect.ResetSelectedScript ();

			//表示の更新
//			dispChara.DispEffectScript ( editChara.GetEffect (), editChara.GetActionScript () );
			DispCompend ();
		}


		//==================================================================================
		//	フレーム表からのコンテキストメニュー
		//==================================================================================

		private void エフェクト追加_Click ( object sender, EventArgs e )
		{
//			editChara.AddEffect ();
			editChara.EditEffect.AddEffect ();
			fT_Effect.Invalidate ();
		}

		private void エフェクト挿入_Click ( object sender, EventArgs e )
		{
//			editChara.InsertEffect ();
			editChara.EditEffect.InsertEffect ();
			fT_Effect.Invalidate ();
		}

		private void エフェクト削除ToolStripMenuItem_Click ( object sender, EventArgs e )
		{
//			editChara.RemoveEffect ();
			editChara.EditEffect.RemoveEffect ();
			fT_Effect.Invalidate ();
		}

	}

}
