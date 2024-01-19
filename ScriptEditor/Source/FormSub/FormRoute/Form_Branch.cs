using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptEditor
{

	public sealed partial class Form_Branch : Form
	{

		//----------------------------------------------------------------
		//シングルトンパターン	//sealed : 継承不可

		//インスタンス
		private static readonly Form_Branch _instance = new Form_Branch();
		public static Form_Branch Instance { get { return _instance; } }

		//プライベートコンストラクタ
		private Form_Branch ()
		{
			//フォームのロケーションを手動に切り替え
			this.StartPosition = FormStartPosition.Manual;

			InitializeComponent ();

			lbCondition.DisplayMember = "Condition";
			lbTransit.DisplayMember = "Transit";
		}

		//閉じたときに破棄しない
		protected override void OnFormClosing ( FormClosingEventArgs e )
		{
			e.Cancel = true;
			this.Hide ();
		}
		//----------------------------------------------------------------

		//EditCharaを受けてキャラのブランチリスト(スクリプト分岐)を編集する
//		private EditChara refEditChara;
//		public EditChara RefEditChara { set { refEditChara = value; } }
		public EditCompend editCompend { get; set; }

		//コマンドリスト
		public BindingList<Command> listCommand { get; set; }

		//アクションリスト
		public BindingList<Sequence> listSequence { get; set; }

		//フォームを受けて他スクリプトにコピーするかチェックする
//		public static FormMain formMain { get; set; }
//		public FormMain _formMain;
		public DispCompend dispCompend;

		//アクション内すべてのスクリプトに対してブランチのみ編集(既存で異なる場合は上書きされる)
		public static bool allBranch { get; set; }
		
		//初期化
//		public void LoadForm ( FormMain formMain, EditChara editChara )
		public void LoadForm ( EditCompend ec, DispCompend dc )
		{
//			_formMain = formMain;
			dispCompend = dc;

//			refEditChara = editChara;
			editCompend = ec;

//			cbConditionCommand.DataSource = refEditChara.chara.ListCommand;
//			cbTransitAction.DataSource = refEditChara.chara.behavior.ListSequence;

//			tba_BranchFrame.formMain = formMain;
//			tba_BranchFrame.editChara = editChara;
			tba_BranchFrame.Load ( ec, dc );
			tba_BranchFrame.scriptAddress = new ScriptAddress ( ScriptAddress.ITEM.BRANCH_FRAME, 0 );

//			Reset ();
		}

		//キャラデータの設定
		public void SetCharaData ( Chara chara )
		{
			listCommand = chara.ListCommand;
			listSequence = chara.behavior.ListSequence;
			cbConditionCommand.DataSource = chara.ListCommand;
			cbTransitAction.DataSource = chara.behavior.ListSequence;
		}

		//リセット
		public void Reset ()
		{
			//ブランチリストはキャラ読込、スクリプト変更、Form_Branch内で変更される

			//動作条件
//			if ( null == refEditChara ) { return; }
//			if ( null == refEditChara.GetActionScript () ) { return; }
//			if ( null == refEditChara.EditAction.GetScript () ) { return; }
			
			//アクション終了時のスクリプト移項は名前の表示のみ。編集はメインフォームで行う
//			tbNextAction.Text = refEditChara.GetAction ().NextIndex.ToString ();
//			tbNextAction.Text = refEditChara.EditAction.Get ().NextIndex.ToString ();
			Action action = ( Action ) editCompend.GetSequence ();
			if ( null == action ) { return; }
			tbNextAction.Text = action.NextIndex.ToString ();

			//スクリプトが持つブランチリストをリセット
			//LB
			ResetListBoxBranch ();

			//キャラの持つコマンドリスト、アクションリストをリセット
			//CB
			for ( int i = 0; i < listCommand.Count; ++i )
			{
//				cbConditionCommand.SelectedIndex = i;
				listCommand.ResetItem ( i );
			}

			for ( int i = 0; i < listSequence.Count; ++i )
			{
//				cbTransitAction.SelectedIndex = i;
				listSequence.ResetItem ( i );
			}

			lblAllBranch.BackColor = allBranch ? Color.Pink : Color.LightGray;

			//TB
			//--------------------------------------------------------------------
			//動作条件
			if ( lbTransit.SelectedItems.Count < 1 ) { return; }	//遷移先LBが選択されている
//			Script script = refEditChara.GetActionScript ();
//			Script script = refEditChara.EditAction.GetScript ();
			Script script = editCompend.GetScript ();
			if ( null == script ) { return; }				//スクリプトが存在している
			if ( script.ListBranch.Count < 1 ) { return; }	//ブランチの個数が１以上
			int index = lbTransit.SelectedIndex;
			//--------------------------------------------------------------------
			tba_BranchFrame.Text = script.ListBranch[ index ].Frame.i.ToString ();
			tba_BranchFrame.refInt = script.ListBranch[ index ].Frame;
		}

		//スクリプトが持つブランチリストをリセット
		public void ResetListBoxBranch ()
		{
			//動作条件
//			if ( null == refEditChara ) { return; }
//			if ( null == refEditChara.GetActionScript () ) { return; }
//			if ( null == refEditChara.EditAction.GetScript () ) { return; }

			//作業用
//			Script script = refEditChara.EditAction.GetScript ();
			Script script = editCompend.GetScript ();
//			BindingList<Branch> lBranch = refEditChara.GetActionScript ().ListBranch;
			BindingList<Branch> lBranch = script.ListBranch;

			if ( 0 == lBranch.Count )
			{
				//一つもないとき空白を表示
				lBranch.Clear ();
			}

			//データソースの指定
			lbCondition.DataSource = lBranch;
			lbTransit.DataSource = lBranch;

			//リスト個数によってボタンの稼働状態を変更する
			btnDelete.Enabled = ( 0 < lBranch.Count );
			btnUp.Enabled = ( 1 < lBranch.Count );
			btnDown.Enabled = ( 1 < lBranch.Count );
		}

		//リストボックス選択
		//"条件"と"遷移先"の両方を選択する
		private void lbCondition_MouseDown ( object sender, MouseEventArgs e )
		{
			lbTransit.SelectedIndex = lbCondition.SelectedIndex;
		}
		private void lbTransit_MouseDown ( object sender, MouseEventArgs e )
		{
			lbCondition.SelectedIndex = lbTransit.SelectedIndex;
		}

		//"追加"ボタン
		private void btnAddCondition_Click ( object sender, EventArgs e )
		{
			//"条件"と"遷移先"の両方に追加

			//--------------------------------------------------------------------
			//動作条件
			if ( listCommand.Count < 1 ) { return; }		//コマンドリストに１つ以上項目が存在する
			if ( listSequence.Count < 1 ) { return; }		//アクションリストに１つ以上項目が存在する
			//--------------------------------------------------------------------

			//charaデータに追加して更新する
			Script script = editCompend.GetScript ();
//			BindingList<Branch> lBranch = refEditChara.GetActionScript ().ListBranch;
			BindingList<Branch> lBranch = script.ListBranch;
			lBranch.Add ( new Branch (
				cbConditionCommand.SelectedIndex, ( Command ) cbConditionCommand.SelectedItem,
				cbTransitAction.SelectedIndex, ( Action ) cbTransitAction.SelectedItem ) );
			ResetListBoxBranch ();

			//全てのスクリプトにブランチをコピー
			if ( allBranch ) { CopyAllScripts (); }
//			_formMain.CheckSetOtherScript ();
			editCompend.CheckSetScript ();

			//選択
			lbCondition.SelectedIndex = lbCondition.Items.Count - 1;
			lbTransit.SelectedIndex = lbTransit.Items.Count - 1;

//			cbConditionCommand.SelectedIndex = 0;
//			cbTransitAction.SelectedIndex = 0;
		}

		//"削除"ボタン
		private void btnDelete_Click ( object sender, EventArgs e )
		{
			//"条件"と"遷移先"の両方を削除 (ブランチリストは０の場合もある) 
			//--------------------------------------------------------------------
			//動作条件
			if ( lbCondition.SelectedItems.Count < 1 ) { return; }	//条件LBが選択されている
//			Script script = refEditChara.GetActionScript ();
//			Script script = refEditChara.EditAction.GetScript ();
			Script script = editCompend.GetScript ();
			if ( null == script ) { return; }				//スクリプトが存在している
			if ( script.ListBranch.Count < 1 ) { return; }	//ブランチの個数が１以上
			//--------------------------------------------------------------------

			int index = lbCondition.SelectedIndex;
			script.ListBranch.RemoveAt ( index );
			ResetListBoxBranch ();

			//全てのスクリプトにブランチをコピー
			if ( allBranch ) { CopyAllScripts (); }
//			_formMain.CheckSetOtherScript ();
			editCompend.CheckSetScript ();

			
			//再選択

			//残り０個の場合は何もしない
			if ( 0 == script.ListBranch.Count )
			{
			}
			//先頭を削除したとき再度先頭を選択
			else if ( 0 == index )
			{
				lbCondition.SelectedIndex = 0;
				lbTransit.SelectedIndex = 0;
			}
			//それ以外は一つ前を選択
			else
			{
				lbCondition.SelectedIndex = index - 1;
				lbTransit.SelectedIndex = index - 1;
			}

			cbConditionCommand.SelectedIndex = 0;
			cbTransitAction.SelectedIndex = 0;
		}

		private void btnUp_Click ( object sender, EventArgs e )
		{
			//--------------------------------------------------------------------
			//動作条件
			if ( lbCondition.Items.Count < 2 ) { return; }				//リストボックスの個数が2未満のときは何もしない
			if ( lbCondition.SelectedItems.Count < 1 ) { return; }		//選択されていないとき何もしない
			if ( lbCondition.SelectedIndex < 1 ) { return; }			//選択が先頭のとき何もしない
//			Script script = refEditChara.GetActionScript ();
//			Script script = refEditChara.EditAction.GetScript ();
			Script script = editCompend.GetScript ();
			if ( null == script ) { return; }				//スクリプトが存在している
			//--------------------------------------------------------------------

			//"条件"と"遷移先"の両リストボックスを処理

			//一つ前の位置(両方ともインデックスは等しい)
			int index = lbCondition.SelectedIndex - 1;
			BindingList<Branch> lBranch = script.ListBranch;

			//前に追加
//			lbCondition.Items.Insert ( direction, lbCondition.SelectedItem );
//			lbTransit.Items.Insert ( direction, lbTransit.SelectedItem );
//			listBranch.Insert ( direction, listBranch[direction] );
			lBranch.Insert ( index, lBranch[lbCondition.SelectedIndex] );

			//後を削除
//			lbCondition.Items.RemoveAt ( direction + 2 );
//			lbTransit.Items.RemoveAt ( direction + 2 );
			lBranch.RemoveAt ( index + 2 );

			ResetListBoxBranch ();

			//全てのスクリプトにブランチをコピー
			if ( allBranch ) { CopyAllScripts (); }
//			_formMain.CheckSetOtherScript ();
			editCompend.CheckSetScript ();

			//選択を対象に戻す
			lbCondition.SelectedIndex = index;
			lbTransit.SelectedIndex = index;

		}

		private void btnDown_Click ( object sender, EventArgs e )
		{
			//--------------------------------------------------------------------
			//動作条件
			if ( lbCondition.Items.Count < 2 ) { return; }			//リストボックスの個数が2未満のときは何もしない
			if ( lbCondition.SelectedItems.Count < 1 ) { return; }			//選択されていないとき何もしない
			if ( lbCondition.SelectedIndex > lbCondition.Items.Count - 2 ) { return; }		//選択が末尾(以降)のとき何もしない
//			if ( null == refEditChara ) { return; }				//キャラが指定されている
//			Script script = refEditChara.GetActionScript ();
//			Script script = refEditChara.EditAction.GetScript ();
			Script script = editCompend.GetScript ();
			if ( null == script ) { return; }				//スクリプトが存在している
			//--------------------------------------------------------------------

			//"条件"と"遷移先"の両リストボックスを処理

			//一つ次の位置
			int index = lbCondition.SelectedIndex + 2;
			BindingList<Branch> lBranch = script.ListBranch;

			//次に追加
//			lbCondition.Items.Insert ( direction, lbCondition.SelectedItem );
//			lbTransit.Items.Insert ( direction, lbTransit.SelectedItem );
//			listBranch.Insert ( direction, listBranch[direction] );

			lBranch.Insert ( index, lBranch[lbCondition.SelectedIndex] );

			//前を削除
//			lbCondition.Items.RemoveAt ( direction - 2 );
//			lbTransit.Items.RemoveAt ( direction - 2 );
//			listBranch.RemoveAt ( direction - 2 );
			lBranch.RemoveAt ( index - 2 );

			ResetListBoxBranch ();

			//全てのスクリプトにブランチをコピー
			if ( allBranch ) { CopyAllScripts (); }
//			_formMain.CheckSetOtherScript ();
			editCompend.CheckSetScript ();

			//選択を対象に戻す
			lbCondition.SelectedIndex = index - 1;
			lbTransit.SelectedIndex = index - 1;
		}

		private void lbCondition_SelectedIndexChanged ( object sender, EventArgs e )
		{
			//--------------------------------------------------------------------
			//動作条件
			if ( cbConditionCommand.Items.Count < 1 ) { return; }	//条件CBに１つ以上項目が存在する
			if ( cbTransitAction.Items.Count < 1 ) { return; }		//遷移先CBに１つ以上項目が存在する
			if ( lbCondition.Items.Count < 1 ) { return; }			//条件LBに１つ以上項目が存在する
			if ( lbCondition.SelectedItems.Count < 1 ) { return; }	//条件LBが選択されている
			//--------------------------------------------------------------------

			//"条件"と"遷移先"の両リストボックスを処理

			int index = lbCondition.SelectedIndex;
			Script script = editCompend.GetScript ();
//			BindingList<Branch> lBranch = refEditChara.GetActionScript ().ListBranch;
			BindingList<Branch> lBranch = script.ListBranch;

			//リストボックスで指定されている値を示す
//			cbConditionCommand.SelectedIndex = cbConditionCommand.FindString ( listBranch[lbCondition.SelectedIndex].Condition.ToString () );
			cbConditionCommand.SelectedIndex = lBranch[index].IndexCommand;
			cbTransitAction.SelectedIndex = lBranch[index].IndexAction;

//			lbTransit.SelectedIndex = direction;

			//フレーム指定
			tba_BranchFrame.scriptAddress.index = lbCondition.SelectedIndex;
			tba_BranchFrame.Text = lBranch[ index ].Frame.i.ToString ();
		}

		private void lbTransit_SelectedIndexChanged ( object sender, EventArgs e )
		{
			//--------------------------------------------------------------------
			//動作条件
			if ( cbConditionCommand.Items.Count < 1 ) { return; }	//条件CBに１つ以上項目が存在する
			if ( cbTransitAction.Items.Count < 1 ) { return; }		//遷移先CBに１つ以上項目が存在する
			if ( lbTransit.Items.Count < 1 ) { return; }			//遷移先LBに１つ以上項目が存在する
			if ( lbTransit.SelectedItems.Count < 1 ) { return; }	//遷移先LBが選択されている
			//--------------------------------------------------------------------

			int index = lbTransit.SelectedIndex;
			Script script = editCompend.GetScript ();
//			BindingList<Branch> lBranch = refEditChara.GetActionScript ().ListBranch;
			BindingList<Branch> lBranch = script.ListBranch;

			//リストボックスで指定されている値を示す
//			cbTransitAction.SelectedIndex = cbTransitAction.FindString ( listBranch[lbCondition.SelectedIndex].Transit.ToString () );
//			lbCondition.SelectedIndex = lbTransit.SelectedIndex;
			cbConditionCommand.SelectedIndex = lBranch[index].IndexCommand;
			cbTransitAction.SelectedIndex = lBranch[index].IndexAction;
			
//			lbCondition.SelectedIndex = direction;

			//フレーム指定
			tba_BranchFrame.scriptAddress.index = lbCondition.SelectedIndex;
			tba_BranchFrame.Text = lBranch[ index ].Frame.i.ToString ();

//			tba_BranchFrame.refInt = script.ListBranch[ index ].Frame;
			Reset ();
		}

		private void cbConditionCommand_SelectedIndexChanged ( object sender, EventArgs e )
		{
#if false
			//選択中の条件を変更
			//--------------------------------------------------------------------
			//動作条件
			if ( cbConditionCommand.Items.Count < 1 ) { return; }	//条件CBに１つ以上項目が存在する
			if ( cbConditionCommand.SelectedIndex == -1 ) { return; }	//条件CBに１つ以上項目が存在する
			if ( lbCondition.Items.Count < 1 ) { return; }			//条件LBに１つ以上項目が存在する
			if ( lbCondition.SelectedItems.Count < 1 ) { return; }	//条件LBが選択されている
			//--------------------------------------------------------------------

			int index = lbCondition.SelectedIndex;
			BindingList<Branch> lBranch = refChara.GetScript ().ListBranch;

//			lbCondition.Items.Insert ( index, cbConditionCommand.SelectedItem );
//			lbCondition.Items.RemoveAt ( index + 1 );
//			listBranch.Insert ( index, new ActualBranch ( 
//				(Command)cbConditionCommand.SelectedItem, 
//				(Action)cbTransitAction.SelectedItem ) );

			lBranch.Insert ( index, new Branch ( 
				cbConditionCommand.SelectedIndex, (Command)cbConditionCommand.SelectedItem,
				cbTransitAction.SelectedIndex, ( Action ) cbTransitAction.SelectedItem ) );
			lBranch.RemoveAt ( index + 1 );
			lbCondition.SelectedIndex = index;
#endif
		}

		private void cbTransitAction_SelectedIndexChanged ( object sender, EventArgs e )
		{
#if false
			//選択中の遷移先を変更

			//--------------------------------------------------------------------
			//動作条件
			if ( cbTransitAction.Items.Count < 1 ) { return; }			//遷移先CBに１つ以上項目が存在する
			if ( cbTransitAction.SelectedIndex == -1 ) { return; }		//選択されているのは空白でない
			if ( lbTransit.Items.Count < 1 ) { return; }				//遷移先LBに１つ以上項目が存在する
			if ( lbTransit.SelectedItems.Count < 1 ) { return; }		//遷移先LBが選択されている
			//--------------------------------------------------------------------

			int index = lbTransit.SelectedIndex;
//			lbTransit.Items.Insert ( index, cbTransitAction.SelectedItem );
//			lbTransit.Items.RemoveAt ( index + 1 );
			lbCondition.SelectedIndex = index;
			lbTransit.SelectedIndex = index;
#endif	
		}

		private void cbConditionCommand_DropDownClosed ( object sender, EventArgs e )
		{
			//選択中の条件を変更
			//--------------------------------------------------------------------
			//動作条件
			if ( cbConditionCommand.Items.Count < 1 ) { return; }	//条件CBに１つ以上項目が存在する
			if ( cbConditionCommand.SelectedIndex == -1 ) { return; }	//条件CBに１つ以上項目が存在する
			if ( lbCondition.Items.Count < 1 ) { return; }			//条件LBに１つ以上項目が存在する
			if ( lbCondition.SelectedItems.Count < 1 ) { return; }	//条件LBが選択されている
			//--------------------------------------------------------------------

			int index = lbCondition.SelectedIndex;
			Script script = editCompend.GetScript ();
//			BindingList<Branch> lBranch = refEditChara.GetActionScript ().ListBranch;
			BindingList<Branch> lBranch = script.ListBranch;

			lBranch.Insert ( index, new Branch (
				cbConditionCommand.SelectedIndex, ( Command ) cbConditionCommand.SelectedItem,
				cbTransitAction.SelectedIndex, ( Action ) cbTransitAction.SelectedItem ) );
			lBranch.RemoveAt ( index + 1 );
			lbCondition.SelectedIndex = index;

			//全てのスクリプトにブランチをコピー
			if ( allBranch ) { CopyAllScripts (); }
//			_formMain.CheckSetOtherScript ();
			editCompend.CheckSetScript ();
		}

		private void cbTransitAction_DropDownClosed ( object sender, EventArgs e )
		{
			//選択中の遷移先を変更
			//--------------------------------------------------------------------
			//動作条件
			if ( cbConditionCommand.Items.Count < 1 ) { return; }		//条件CBに１つ以上項目が存在する
			if ( cbConditionCommand.SelectedIndex == -1 ) { return; }	//条件CBに１つ以上項目が存在する
			if ( lbCondition.Items.Count < 1 ) { return; }				//条件LBに１つ以上項目が存在する
			if ( lbCondition.SelectedItems.Count < 1 ) { return; }		//条件LBが選択されている
			//--------------------------------------------------------------------

			int index = lbCondition.SelectedIndex;
			Script script = editCompend.GetScript ();
//			BindingList<Branch> lBranch = refEditChara.GetActionScript ().ListBranch;
			BindingList<Branch> lBranch = script.ListBranch;

			lBranch.Insert ( index, new Branch (
				cbConditionCommand.SelectedIndex, ( Command ) cbConditionCommand.SelectedItem,
				cbTransitAction.SelectedIndex, ( Action ) cbTransitAction.SelectedItem ) );
			lBranch.RemoveAt ( index + 1 );
			lbCondition.SelectedIndex = index;

			//全てのスクリプトにブランチをコピー
			if ( allBranch ) { CopyAllScripts (); }
//			_formMain.CheckSetOtherScript ();
			editCompend.CheckSetScript ();
		}

		//全てのスクリプトにブランチをコピー
		private void CopyAllScripts ()
		{
//			Script selectedScript = refEditChara.GetActionScript ();
			Script selectedScript = editCompend.GetScript ();
			Action action = (Action)editCompend.GetSequence ();

			foreach ( Script script in action.ListScript )
			{
				if ( selectedScript == script ) { continue; }
				script.ListBranch.Clear ();
				foreach ( Branch branch in selectedScript.ListBranch )
				{
					script.ListBranch.Add ( branch );
				}
			}
		}
	}
}
