using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;

namespace ScriptEditor
{
	public partial class ListCommand : UserControl
	{
		//対象
		BindingList< Command > commands = new BindingList < Command > ();

		public Ctrl_Command CtrlCmd { get; set; } = new Ctrl_Command ();

		public ListCommand ()
		{
			InitializeComponent ();

			Lb_LCmd.DataSource = commands;
			Lb_LCmd.DisplayMember = "Name";
		}

		//データの設置
		public void SetData ( BindingList < Command > lcmd, Ctrl_Command ctrlcmd )
		{
			commands = lcmd;
			Lb_LCmd.DataSource = commands;

			CtrlCmd = ctrlcmd;
		}

		//選択中のデータを取得
		public Command GetSelected ()
		{
			int i = Lb_LCmd.SelectedIndex;
			if ( i < 0 || commands.Count < i ) { return null; }

			return commands[ Lb_LCmd.SelectedIndex ];
		}

		//追加
		private void Btn_Add_Click ( object sender, System.EventArgs e )
		{
			commands.Add ( new Command () );
			commands.ResetBindings ();
		}

		//削除
		private void Btn_Del_Click ( object sender, System.EventArgs e )
		{
			//０のときは何もしない
			if ( commands.Count < 1 ) { return; }

			//１のときは空欄を表示する
			if ( commands.Count < 2 )
			{
				CtrlCmd.Set ( new Command () );
				CtrlCmd.Invalidate ();
			}

			commands.RemoveAt ( commands.Count - 1 );
		}

		//リストボックスでの選択
		private void Lb_LCmd_SelectedIndexChanged ( object sender, System.EventArgs e )
		{
			if ( commands.Count < 1 ) { return; }
			Command cmd = (Command) Lb_LCmd.SelectedItem ;
			CtrlCmd.Set ( cmd );
		}
	}
}
