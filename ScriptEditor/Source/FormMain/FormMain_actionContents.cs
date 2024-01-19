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
		//	[アクション]タブ
		//==================================================================================

		//[アクション]読込時に行う１回の設定
		private void tabAction_Load ()
		{
			EditAction ea = editChara.EditAction;
			DispBehavior db = dispChara.dispBehavior;
			DispAction da = db.dispAction;
			DispScript ds = db.dispScript;

			//------------------------------------------------------------------
			//表示

			//イメージ表示PBにコントロールを設定
			pbi_Action.Load ( ea, db, 
				this.rb_ImageMove, this.rbORect, this.rbHRect, this.rbARect, this.rbCRect,
				this.rb_EfGnrtMove );

			//フレームテーブルにコントロールを設定
			fT_Action.Load ( ea, db, "アクション", splitContainer1.Panel2 );

			//ビヘイビア表示にコントロールを設定
			db.Load ( ea, fT_Action );

			//アクション表示にコントロールを設定
			da.Load ( tbActionName, cbActionNext, tbaBlance,
				dispChara.dispBehavior, editChara, ea, cb_Category );

			//スクリプト表示にコントロールを設定
			ds.Load
			(
				editChara.EditAction, dispChara.dispBehavior, chara.ListImageData, 
				chara.ListEfImageData, chara.garnish.ListSequence, 
				pbi_Action, tbFrame, tbImage,
				tba_PtX, tba_PtY, tba_VelX, tba_VelY, tba_AccX, tba_AccY,
				cb_ClcSt, tbaPower
			);

			//------------------------------------------------------------------
			// サブフォーム初期化

			// [レクト] 枠指定(アクション)

			// [ブランチ]
			Form_Branch.Instance.LoadForm ( editChara.EditAction, dispChara.dispBehavior );
			
			// [エフェクトジェネレート]
			FormEffectGenerate.Instance.LoadForm ( dispChara.dispBehavior, editChara, ea, ds.dispEfGnrt );
		
			//------------------------------------------------------------------
			//他コントロール

			//メニュー
			ｱﾝﾄﾞｩToolStripMenuItem.Enabled = false;

			//ツール
			rb_ImageMove.Checked = true;

			//複数編集
			editChara.EditAction.spanScript = true;
			lblSpan.BackColor = Color.FromArgb ( 0xff, 0xd0, 0xff, 0xd0 );

			//マウスホイールのイベントを登録
//			pbFrameTable.MouseWheel += new System.Windows.Forms.MouseEventHandler ( pbFrameTable_MouseWheel );
		}

		//[アクション]タブ選択時
		public void tabAction_Selected ()
		{
			Debug.Assert ( null != chara );

			//編集対象(アクション・エフェクト)の切替
			editCompend = editChara.EditAction;
			dispCompend = dispChara.dispBehavior;
			frameTable = fT_Action;

//			editCompend.SetSelectedScript ( new EditCompend.SELECTED_SCRIPT ( 0, 0 ) );

			//表示の更新
//			UpdateData ();
		}

		//[アクション]タブ離去時
		public void tabAction_Deselected ()
		{
			//フォームを隠す
			Form_Branch.Instance.Hide ();
			FormEffectGenerate.Instance.Hide ();
		}


		//==================================================================================
		//	コントロール　イベント
		//==================================================================================

		//----------------------------------------------------------------------------------
		//アクション名

		//EnterでBeep音を鳴らさない
		private void tbActionName_KeyPress ( object sender, KeyPressEventArgs e )
		{
			if ( e.KeyChar == ( Char ) Keys.Enter ) { e.Handled = true; } 
		}

		//決定(Enter)時
		private void tbActionName_KeyDown ( object sender, KeyEventArgs e )
		{
			if ( e.KeyCode == Keys.Enter )
			{
//				Action action = editChara.GetAction ();
				Action action = editChara.EditAction.Get ();
				if ( action == null ) { return; }
				action.Name = tbActionName.Text;
			}
		}

		//次アクションIDの値変更
		private void cbActionNext_SelectedIndexChanged ( object sender, EventArgs e )
		{
			if ( -1 == cbActionNext.SelectedIndex ) { return; }
//			editChara.GetAction ().NextIndex = cbActionNext.SelectedIndex;
			editChara.EditAction.Get ().NextIndex = cbActionNext.SelectedIndex;
		}

		//アクションカテゴリの値変更
		private void cb_Category_SelectedIndexChanged ( object sender, EventArgs e )
		{
			if ( -1 == cb_Category.SelectedIndex ) { return; }
//			editChara.GetAction ().category = cb_Category.SelectedIndex;
			editChara.EditAction.Get ().category = cb_Category.SelectedIndex;
		}

		//スクリプト計算状態の値変更
		private void cb_ClcSt_SelectedIndexChanged ( object sender, EventArgs e )
		{
			editChara.EditAction.GetScript().CalcState = ((CLC_ST_OB)cb_ClcSt.SelectedItem).clc_st;
		}

		//==================================================================================
		//	サブフォーム
		//==================================================================================

		//----------------------------------------------------------------------------------
		//イメージ選択フォーム
		//----------------------------------------------------------------------------------
		private void tbImage_Click ( object sender, EventArgs e )
		{
			//動作条件
			if ( null == chara ) { return; }
//			if ( 0 == chara.ListImageData.Count ) { return; }
//			Script script = editChara.GetActionScript ();
			Script script = editChara.EditAction.GetScript ();
			if ( null == script ) { return; }

			//選択用別フォーム
			Form_ImagePreview form_ImagePreview = new Form_ImagePreview ( chara.ListImageData );

			//フォームを開く
			if ( form_ImagePreview.ShowDialog ( this ) == DialogResult.OK )
			{
				//フォームをOKで閉じる

				//イメージの設定
				script.imgIndex = form_ImagePreview.GetImageIndex();

				//スクリプト変更後に他スクリプトにコピーするかチェックする
//				CheckSetOtherScript ();
				editChara.EditAction.CheckSetOtherScript ();

				//表示の更新
				dispCompend.Disp ();
			}

			form_ImagePreview.Dispose ();
		}

		//----------------------------------------------------------------------------------
		//スクリプト分岐(Branch)フォーム
		//----------------------------------------------------------------------------------
		private void btnBranch_Click ( object sender, EventArgs e )
		{
			OpenForm ( false );
		}

		//全てのスクリプトにおける分岐(Branch)フォーム
		private void btnAllBranch_Click ( object sender, EventArgs e )
		{
			OpenForm ( true );
		}

		private void OpenForm ( bool allFlag )
		{
			if ( Form_Branch.Instance.Visible )
			{
				Form_Branch.Instance.Hide ();
			}
			else
			{
				//モードレスでフォームを開く

				//再設定
				Form_Branch.allBranch = allFlag;

				//初期化
				Form_Branch.Instance.Reset ();

				//右隣接で開く
				Form_Branch.Instance.Location = new Point ( this.Location.X + this.Width, this.Location.Y );
				Form_Branch.Instance.Show ();
				Form_Branch.Instance.Activate ();
			}

		}

		//----------------------------------------------------------------------------------
		//	枠設定フォーム
		//----------------------------------------------------------------------------------
		private void btnRect_Click ( object sender, EventArgs e )
		{
			if ( Form_Rect.Instance.Visible )
			{
				Form_Rect.Instance.Close ();
			}
			else
			{
				//右隣接で開く
				Form_Rect.Instance.Location = new Point ( this.Location.X + this.Width, this.Location.Y );
				Form_Rect.Instance.Show ();
				Form_Rect.Instance.Activate ();
			}
		}

		//----------------------------------------------------------------------------------
		//	エフェクト設定フォーム
		//----------------------------------------------------------------------------------
		private void BtnEffect_Click ( object sender, EventArgs e )
		{
			if ( FormEffectGenerate.Instance.Visible )
			{
				FormEffectGenerate.Instance.Close ();
			}
			else
			{
				//右隣接で開く
				FormEffectGenerate.Instance.Location = new Point ( this.Location.X + this.Width, this.Location.Y );
				FormEffectGenerate.Instance.Show ();
				FormEffectGenerate.Instance.Activate ();
			}
		}


		//==================================================================================
		//	複数編集
		//==================================================================================

		//----------------------------------------------------------------------------------
		//	全てのスクリプトの変更トグル
		//----------------------------------------------------------------------------------
		private void lblAll_Click ( object sender, EventArgs e )
		{
			editChara.EditAction.allScript ^= true;
			lblAll.BackColor = editChara.EditAction.allScript ? Color.Pink : Color.LightGray;
		}
		//----------------------------------------------------------------------------------
		//	範囲スクリプトの変更トグル
		//----------------------------------------------------------------------------------
		private void lblSpan_Click ( object sender, EventArgs e )
		{
			editChara.EditAction.spanScript ^= true;
			lblSpan.BackColor = editChara.EditAction.spanScript ? Color.FromArgb ( 0xff, 0xd0, 0xff, 0xd0 ) : Color.LightGray;
		}

#if false

		//スクリプト変更後に他スクリプトにコピーするかチェックする
		public void CheckSetOtherScript ()
		{
			//スクリプトの全てに項目全てをコピー
			if ( editChara.EditAction.allScript )
			{
				editChara.EditAction.CopyToAllScript ();
			}
			//スクリプトの選択範囲に項目全てをコピー
			else if ( editChara.EditAction.spanScript )
			{
				editChara.EditAction.CopyToSpanScript ();
			}
		}
		
#endif

	}
}
