using System;
using System.Windows.Forms;
using System.ComponentModel;


namespace ScriptEditor
{
	//=============================================================
	// バインディングディクショナリの追加・削除・位置変更を持つコントロール 
	//	ジェネリクス <T> : 制約 IName, new () 
	//=============================================================
	public partial class Ctrl_ListBox < T > : UserControl
		where T : IName, new () 
	{
		private BindingDictionary < T > BD_t = new BindingDictionary < T > ();

		public Ctrl_ListBox ()
		{
			InitializeComponent ();
			ListBox0.DisplayMember = "Name";
		}

		public void Set ( BindingDictionary < T > bd )
		{
			BD_t = bd;
			ListBox0.DataSource = BD_t.GetBindingList ();
		}

		//更新
		public void UpdateData ()
		{
			BindingList < T > BList = BD_t.GetBindingList ();
			
			ListBox0.DataSource = BList;

			int i = 0;
			foreach ( T t in BList )
			{
				BList.ResetItem ( i ++ );
			}
		}

		private void ListBox_SelectedIndexChanged ( object sender, EventArgs e )
		{

		}

		//追加ボタン
		private void BTN_Add_Click ( object sender, EventArgs e )
		{
			string name = "new";
			while ( BD_t.GetDictionary().ContainsKey ( name ) )
			{
				name += "_new";
			}

			//データの作成
			T t = new T
			{
				Name = name
			};

			BD_t.Add ( t );
			BD_t.GetBindingList ().ResetBindings ();

			//更新
			UpdateData ();
		}

		//削除ボタン
		private void BTN_Del_Click ( object sender, EventArgs e )
		{
			BD_t.RemoveAt ( ListBox0.SelectedIndex );
		}

		//上に移動
		private void Btn_Up_Click ( object sender, EventArgs e )
		{
			//--------------------------------------------------------------------
			//動作条件
			if ( ListBox0.Items.Count < 2 ) { return; }				//リストボックスの個数が2未満のときは何もしない
			if ( ListBox0.SelectedItems.Count < 1 ) { return; }		//選択されていないとき何もしない
			if ( ListBox0.SelectedIndex < 1 ) { return; }           //選択が先頭のとき何もしない
			//--------------------------------------------------------------------
			//位置
			int index = ListBox0.SelectedIndex;	//選択位置
			int prev_index = index - 1;	//1つ前の位置

			//---------
			//バインディングリスト内の位置を更新
			BindingList < T > BList = BD_t.GetBindingList ();

			//１つ前の位置に自身をコピー
			BList.Insert ( prev_index, BList[index] );

			//元の位置の後を削除
			BList.RemoveAt ( index + 1 );

			//---------
			//ディクショナリは変更無し
			//---------

#if false
			//全体
			if ( Ckb_All.Checked )
			{
				//現在アクションすべてにブランチをコピー
				EditChara.Inst.EditBehavior.SetBranch ( Scp );
			}
#endif
			//更新
			UpdateData ();

			//リストボックスの選択位置を対象に戻す
			ListBox0.SelectedIndex = prev_index;
		}

		//下に移動
		private void Btn_Down_Click ( object sender, EventArgs e )
		{
			//--------------------------------------------------------------------
			//動作条件
			if ( ListBox0.Items.Count < 2 ) { return; }				//リストボックスの個数が2未満のときは何もしない
			if ( ListBox0.SelectedItems.Count < 1 ) { return; }		//選択されていないとき何もしない
			if ( ListBox0.SelectedIndex > ListBox0.Items.Count - 2 ) { return; }		//選択が末尾(以降)のとき何もしない
			//--------------------------------------------------------------------

			//位置
			int index = ListBox0.SelectedIndex;	//選択位置
			int next_index = index + 2;	//1つ次の位置

			//---------
			//バインディングリスト内の位置を更新
			BindingList < T > BList = BD_t.GetBindingList ();

			//次に追加
			BList.Insert ( next_index, BList [ index ] );

			//現在位置を削除
			BList.RemoveAt ( index );

			//---------
			//ディクショナリは変更無し
			//---------

#if false
			//全体
			if ( Ckb_All.Checked )
			{
				//現在アクションすべてにブランチをコピー
				EditChara.Inst.EditBehavior.SetBranch ( Scp );
			}
#endif

			//更新
			UpdateData ();

			//リストボックスの選択位置を対象に戻す
			ListBox0.SelectedIndex = index + 1;
		}

	}
}
