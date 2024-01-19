using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace ScriptEditor
{
	public partial class FormBranch : Form
	{
		//---------------------------------------------------------------------
		//シングルトン実体
		public static FormBranch Inst { get; set; } = new FormBranch ();
		
		//プライベートコンストラクタ
		private FormBranch ()
		{
			this.ShowInTaskbar = false;	//タスクバーに非表示

			InitializeComponent ();

			//リストボックス表示の指定
			Lb_Command.DisplayMember = "Condition";
			Lb_Action.DisplayMember = "Transit";
		}
			
		//閉じたときに破棄しない
		protected override void OnFormClosing ( FormClosingEventArgs e )
		{
			e.Cancel = true;
			this.Hide ();
		}
			
		//親フォーム参照
		public FormMain FormMain { get; set; } = null;

		//フォーム位置を親フォームの右端にする
		private void FormBranch_VisibleChanged ( object sender, System.EventArgs e )
		{
			int x = FormMain.Location.X + FormMain.Width + 100;
			int y = FormMain.Location.Y + 100;
			this.Location = new Point ( x, y );
		}
		//---------------------------------------------------------------------
		
		//編集対象
		public Script Scp = null;

		//コンペンド編集
		public EditCompend EditCompend;

		//キャラデータの設定
		public void SetCharaData ( Chara ch )
		{
			EditBehavior eb = EditChara.Inst.EditBehavior;
			Cb_Command.DataSource = ch.ListCommand;
			Cb_Action.DataSource = ch.behavior.BD_Sequence.GetBindingList();
		}

		//関連付け
		public void Assosiate ( Script script )
		{
			Scp = script;
			UpdateData ();
		}

		//更新
		public void UpdateData ()
		{
			Lb_Command.DataSource = Scp.ListBranch;
			Lb_Action.DataSource = Scp.ListBranch;
			int i = 0;
			foreach ( Branch b in Scp.ListBranch )
			{
				Scp.ListBranch.ResetItem ( i ++ );
			}
		}

		//追加ボタン
		private void Btn_Add_Click ( object sender, System.EventArgs e )
		{
			int indexCommand = Cb_Command.SelectedIndex;
			Command command = (Command)Cb_Command.SelectedItem;
			int indexAction = Cb_Action.SelectedIndex;
			Action action = (Action)Cb_Action.SelectedItem;
			Scp.ListBranch.Add ( new Branch ( indexCommand, command, indexAction, action ) );

			//全体
			if ( Ckb_All.Checked )
			{
				//現在アクションすべてにブランチをコピー
				EditChara.Inst.EditBehavior.SetBranch ( Scp );
			}

			UpdateData ();
		}

		//削除ボタン
		private void Btn_Del_Click ( object sender, System.EventArgs e )
		{
			int index = Lb_Command.SelectedIndex;
			Scp.ListBranch.RemoveAt ( index );

			//全体
			if ( Ckb_All.Checked )
			{
				//現在アクションすべてにブランチをコピー
				EditChara.Inst.EditBehavior.SetBranch ( Scp );
			}

			UpdateData ();
		}

		//上へ移動
		private void Btn_Up_Click ( object sender, System.EventArgs e )
		{
			//--------------------------------------------------------------------
			//動作条件
			if ( Lb_Command.Items.Count < 2 ) { return; }				//リストボックスの個数が2未満のときは何もしない
			if ( Lb_Command.SelectedItems.Count < 1 ) { return; }		//選択されていないとき何もしない
			if ( Lb_Command.SelectedIndex < 1 ) { return; }			//選択が先頭のとき何もしない
			//--------------------------------------------------------------------
			BindingList < Branch > bl_Brc = Scp.ListBranch;

			//１つ前の位置
			int index = Lb_Command.SelectedIndex - 1;

			//前に追加
			bl_Brc.Insert ( index, bl_Brc [ Lb_Command.SelectedIndex ] );

			//後を削除
			bl_Brc.RemoveAt ( index + 2 );

			//全体
			if ( Ckb_All.Checked )
			{
				//現在アクションすべてにブランチをコピー
				EditChara.Inst.EditBehavior.SetBranch ( Scp );
			}

			UpdateData ();

			//選択を対象に戻す
			Lb_Command.SelectedIndex = index;
			Lb_Action.SelectedIndex = index;
		}

		//下へ移動
		private void Btn_Down_Click ( object sender, System.EventArgs e )
		{
			//--------------------------------------------------------------------
			//動作条件
			if ( Lb_Command.Items.Count < 2 ) { return; }				//リストボックスの個数が2未満のときは何もしない
			if ( Lb_Command.SelectedItems.Count < 1 ) { return; }		//選択されていないとき何もしない
			if ( Lb_Command.SelectedIndex > Lb_Command.Items.Count - 2 ) { return; }		//選択が末尾(以降)のとき何もしない
			//--------------------------------------------------------------------
			BindingList < Branch > bl_Brc = Scp.ListBranch;

			//１つ次の位置
			int index = Lb_Command.SelectedIndex + 2;

			//次に追加
			bl_Brc.Insert ( index, bl_Brc [ Lb_Command.SelectedIndex ] );

			//前を削除
			bl_Brc.RemoveAt ( index - 2 );

			//全体
			if ( Ckb_All.Checked )
			{
				//現在アクションすべてにブランチをコピー
				EditChara.Inst.EditBehavior.SetBranch ( Scp );
			}

			UpdateData ();

			//選択を対象に戻す
			Lb_Command.SelectedIndex = index - 1;
			Lb_Action.SelectedIndex = index - 1;
		}

		//ドロップダウンリストでコマンドの設定
		private void Cb_Command_DropDownClosed ( object sender, System.EventArgs e )
		{
			Branch brc = (Branch)Lb_Command.SelectedItem;
			if ( null == brc ) { return; }
			brc.Condition = (Command) Cb_Command.SelectedItem;

			int indexBranch = Lb_Command.SelectedIndex;
			int indexCommand = Cb_Command.SelectedIndex;
			Scp.ListBranch [ indexBranch ].IndexCommand = indexCommand;
			Scp.ListBranch [ indexBranch ].Condition = brc.Condition;

			//全体
			if ( Ckb_All.Checked )
			{
				//現在アクションすべてにブランチをコピー
				EditChara.Inst.EditBehavior.SetBranch ( Scp );
			}

			UpdateData ();

			//選択を対象に戻す
			Lb_Command.SelectedIndex = indexBranch;
		}

		//ドロップダウンリストでアクションの設定
		private void Cb_Action_DropDownClosed ( object sender, System.EventArgs e )
		{
			Branch brc = (Branch)Lb_Command.SelectedItem;
			if ( null == brc ) { return; }
			brc.Transit = (Action) Cb_Action.SelectedItem;

			int indexBranch = Lb_Command.SelectedIndex;
			int indexAction = Cb_Action.SelectedIndex;
			Scp.ListBranch [ indexBranch ].IndexAction = indexAction;
			Scp.ListBranch [ indexBranch ].Transit = brc.Transit;

			//全体
			if ( Ckb_All.Checked )
			{
				//現在アクションすべてにブランチをコピー
				EditChara.Inst.EditBehavior.SetBranch ( Scp );
			}

			UpdateData ();

			//選択を対象に戻す
			Lb_Command.SelectedIndex = indexBranch;
		}

		//リストボックスでブランチの選択(コマンド)
		private void Lb_Command_SelectedIndexChanged ( object sender, System.EventArgs e )
		{
			Branch brc = (Branch)Lb_Command.SelectedItem;
			if ( null == brc ) { return; }
			Cb_Command.SelectedItem = brc.Condition;
		}

		//リストボックスでブランチの選択（アクション）
		private void Lb_Action_SelectedIndexChanged ( object sender, System.EventArgs e )
		{
			Branch brc = (Branch)Lb_Action.SelectedItem;
			if ( null == brc ) { return; }
			Cb_Action.SelectedItem = brc.Transit;
		}
	}
}
