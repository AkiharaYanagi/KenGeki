using System;
using System.Windows.Forms;
using System.Drawing;

namespace ScriptEditor
{
	//---------------------------------------------------------------------
	//@info Show(this);によりメインフォームにより隠れないように変更後、
	// サブフォームが開いているとメインフォームが閉じない問題
	// -> 常時存在するシングルトンとしてvisibleを切替で解決
	//---------------------------------------------------------------------

	//---------------------------------------------------------------------
	//	ScriptEditor 各種設定フォーム
	//---------------------------------------------------------------------
	//継承先フォームは１プロセス１フォームなのでシングルトンで実装する
	//・静的で単一な実体化	//static
	//・継承禁止	//sealed
	//・プライベートコンストラクタ
	//・"Close()" を "Hide()" で置き換える
	//---------------------------------------------------------------------

	public class EditorForm : Form
	{
		//コンストラクタ
		public EditorForm ()
		{
			this.StartPosition = FormStartPosition.Manual;	//位置を手動にする
			this.ShowInTaskbar = false;	//タスクバーに非表示
		}

		//閉じたときに破棄しない
		protected override void OnFormClosing ( FormClosingEventArgs e )
		{
			e.Cancel = true;
			this.Hide ();
		}

		//オブジェクト実体化後の初期化
		//IDEのデザイナ表示でオブジェクトの参照がない状態なのでコンストラクタ後に呼び出す
		protected void LoadObject ()
		{
			this.VisibleChanged += new EventHandler ( EditForm_VisibleChanged );
		}

		//表示反転
		public void VisFlip ()
		{
			this.Visible = ! this.Visible;
		}

		//表示する(既に表示済みのときは最前面にする)
		public void Active ()
		{
			if ( this.Visible == false)
			{
				this.Visible = true;
				this.TopMost = true;
				this.TopMost = false;
			}
			else
			{
				this.TopMost = true;
				this.TopMost = false;
			}
		}


		//---------------------------------------------------------------------
		//親フォーム参照
		public FormMain FormMain { get; set; } = null;
		public Point InitPt { get; set; } = new Point ( 0, 0 );

		//イベント・表示変更時
		public void EditForm_VisibleChanged ( object sender, EventArgs e )
		{
			if ( FormMain is null ) { return; }

			//フォーム位置を親フォームの右端にする
			int x = InitPt.X + FormMain.Location.X + FormMain.Width;
			int y = InitPt.Y + FormMain.Location.Y;
			this.Location = new Point ( x, y );
		}
		//---------------------------------------------------------------------

	}
}
