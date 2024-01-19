using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;


namespace ScriptEditor
{
	//------------------------------------------------------------------
	//	シークエンスの一覧をツリー状に表示し、テキストとして選択を１つ返す
	//------------------------------------------------------------------
	public class SequenceTree : UserControl
	{
		//対象シークエンス
		public BindingDictionary < Sequence > BD_sq { get; set; } = null;

		//編集と表示の参照
		public EditCompend EditCompend { get; set; } = null;
		public DispCompend DispCompend { get; set; } = null;

		//親Ctrl
		public Ctrl_Compend CtrlCompend { get; set; } = null;

		//コンストラクタ
		public SequenceTree ()
		{
			InitializeComponent ();

			treeView1.Scrollable = true;
		}

		private TreeView treeView1;
		private ContextMenuStrip contextMenuStrip1;
		private IContainer components;
		private ToolStripMenuItem すべて展開ToolStripMenuItem;
		private ToolStripMenuItem すべて閉鎖ToolStripMenuItem;

		//コンポーネント初期化
		private void InitializeComponent ()
		{
			this.components = new System.ComponentModel.Container();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.すべて展開ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.すべて閉鎖ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView1.HideSelection = false;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(129, 266);
			this.treeView1.TabIndex = 0;
			this.treeView1.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeSelect);
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.すべて展開ToolStripMenuItem,
            this.すべて閉鎖ToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(181, 70);
			// 
			// すべて展開ToolStripMenuItem
			// 
			this.すべて展開ToolStripMenuItem.Name = "すべて展開ToolStripMenuItem";
			this.すべて展開ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.すべて展開ToolStripMenuItem.Text = "すべて展開";
			this.すべて展開ToolStripMenuItem.Click += new System.EventHandler(this.すべて展開ToolStripMenuItem_Click);
			// 
			// すべて閉鎖ToolStripMenuItem
			// 
			this.すべて閉鎖ToolStripMenuItem.Name = "すべて閉鎖ToolStripMenuItem";
			this.すべて閉鎖ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.すべて閉鎖ToolStripMenuItem.Text = "すべて閉鎖";
			this.すべて閉鎖ToolStripMenuItem.Click += new System.EventHandler(this.すべて閉鎖ToolStripMenuItem_Click);
			// 
			// SequenceTree
			// 
			this.AutoScroll = true;
			this.Controls.Add(this.treeView1);
			this.Name = "SequenceTree";
			this.Size = new System.Drawing.Size(129, 266);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		//環境設定
		public void SetCtrl ( EditCompend ec, DispCompend dc )
		{
			EditCompend = ec;
			DispCompend = dc;
		}

		//データ設定
		public void SetCharaData ( BindingDictionary < Sequence > bd_sq )
		{
			if ( bd_sq is null ) { return; }
			BD_sq = bd_sq;
			BindingList < Sequence > bl = bd_sq.GetBindingList();
			
			//０のときダミー
			if ( 0 >= bd_sq.Count() )
			{
				bd_sq.New ();
			}


			//アクションのときのみアクションカテゴリで分類
			if ( bl[0] is Action ) 
			{
				//カテゴリでツリー構築
				ClassficatinByCategory ( bl );

				//先頭を選択
				treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0];
				string seq_name = treeView1.SelectedNode.Text;
				EditCompend.SelectSequence ( seq_name );
			}
			//アクションではない(エフェクト)のとき
			else
			{
				//名前でツリー構築
				ClassficatinByName ( bl );

				//先頭を選択
				treeView1.SelectedNode = treeView1.Nodes[0];
				string seq_name = treeView1.SelectedNode.Text;
				EditCompend.SelectSequence ( seq_name );
			}

			treeView1.ExpandAll ();
		}

		//先頭を選択
		public void SelectTop ()
		{
			BindingList < Sequence > bl = BD_sq.GetBindingList();

			//アクションのときのみアクションカテゴリで分類
			if ( bl[0] is Action ) 
			{
				treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0];
			}
			//アクションではない(エフェクト)のとき
			else
			{
				treeView1.SelectedNode = treeView1.Nodes[0];
			}
		}

		//更新
		public void UpdateCategory ( BindingList < Sequence > bl )
		{
			//@info ツリー選択時の再構築で順番が変わってしまう
			//->コンペンドの選択イベントでUpdateが呼ばれる

			//アクションフォームにおいて、アクション選択時にカテゴリが異なると
			//フォーム上のカテゴリが表示変更されるのでイベントが発生→再構築

			//全開放->対象コンペンドからツリーを再構築して順番を保持する

			if ( bl is null ) { return; }
			if ( bl.Count <= 0 ) { return; }

			//アクションのときカテゴリの更新
			if ( bl[0] is Action a )
			{
				ClassficatinByCategory ( bl );
			}
			else
			{
				ClassficatinByName ( bl );
			}
		}

		//再構築
		public void Remake ()
		{
			ReExpand ();
			UpdateCategory ( BD_sq.GetBindingList () );
			tmpNd?.Expand ();
		}

		//ツリー選択後
		private void treeView1_AfterSelect ( object sender, TreeViewEventArgs e )
		{
			string name = treeView1.SelectedNode.Text;
			EditCompend.SelectSequence ( name );
			CtrlCompend.Assosiate ();

			DispCompend.Disp ();
#if false

			if ( treeView1.SelectedNode is null ) { return; }

			//アクションのときのみ
			BindingList < Sequence > bl = BD_sq.GetBindingList ();
			TreeNode slctNd = treeView1.SelectedNode;

			if ( bl[0] is Action a )
			{
				//カテゴリのときは何もしない
				foreach ( TreeNode tn in treeView1.Nodes )
				{
					if ( tn == slctNd ) { return; }
				}
				slctNd.Parent.Expand ();
			}

			//選択名
			string name = treeView1.SelectedNode.Text;

			//名前で選択
			//SelectSequence?.Invoke ( name );
			EditCompend.SelectSequence ( name );

			//関連付け
			CtrlCompend.Assosiate ();

			//表示の更新
			DispChara.Inst.Disp ();

			//ツリーの展開
//			treeView1.ExpandAll ();
//			TreeNode[] tna = treeView1.Nodes.Find ( name, true );
//			tna[0].Expand ();
//			slctNd.Expand ();
			tmpNd?.Expand ();

#endif
		}

		//ツリー選択前
		private void treeView1_BeforeSelect ( object sender, TreeViewCancelEventArgs e )
		{
//			ReExpand ();	
		}

		//選択済みノードを一時保存して再展開
		private TreeNode tmpNd = null;	//選択を一時保存
		public void ReExpand ()
		{
			TreeNode tn = treeView1.SelectedNode;
			if ( tn is null ) { return; }

			BindingList < Sequence > bl = BD_sq.GetBindingList ();
			//アクションの場合
			if ( bl[0] is Action a )
			{
				//カテゴリを保存
				tmpNd = tn.Parent;;
			}
			else
			{
				tmpNd = treeView1.SelectedNode;
			}
		}

		//-----------------------------------------------------------------------------
		//名前でツリーを再構築
		private void ClassficatinByName ( BindingList < Sequence > bl )
		{
			treeView1.Nodes.Clear ();

			//名前で直接分類する
			TreeNode [] root = new TreeNode [ bl.Count ];
			
			int index = 0;
			foreach ( Sequence s in bl )
			{
				root [ index ] = new TreeNode ( s.Name );
				++ index;
			}

			treeView1.Nodes.AddRange ( root );
		}

		//カテゴリでツリーを再構築
		private void ClassficatinByCategory ( BindingList < Sequence > bl )
		{
			//クリア
			treeView1.Nodes.Clear ();

			//----------------------------------------------
			//定数でカテゴリを初期化
			string[] names = Enum.GetNames ( typeof ( ActionCategory ) );

			//木の根
			TreeNode[] root = new TreeNode [ names.Length ];

			//追加
			for ( int i = 0; i < names.Length; ++ i )
			{
				root [ i ] = new TreeNode ( names [ i ] );
			}

			//----------------------------------------------
			//各アクションを分類
			foreach ( Action a in bl )
			{
				//カテゴリ名を取得
				string ctg_name = Enum.GetName ( typeof ( ActionCategory ), a.Category );

				//カテゴリからサーチ
				TreeNode tn = Array.Find ( root, n => n.Text == ctg_name );

				//追加
				tn.Nodes.Add ( a.Name );				
			}

			//----------------------------------------------
			//全追加
			treeView1.Nodes.AddRange ( root );

		}

		private void すべて展開ToolStripMenuItem_Click ( object sender, EventArgs e )
		{
			treeView1.ExpandAll ();
		}

		private void すべて閉鎖ToolStripMenuItem_Click ( object sender, EventArgs e )
		{
			treeView1.CollapseAll ();
		}
	}
}
