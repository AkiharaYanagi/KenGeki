using System;
using System.Windows.Forms;

namespace ScriptEditor
{
	//----------------------------------------------------------------------
	// 数値のみ入力でき、関連付けられたコマンドを変更するテキストボックス
	//----------------------------------------------------------------------
	public class TBN_Command : TextBox
	{
		//関連付けられる数値
//		public RefInt refInt { get; set; }

		//全てのスクリプトに適用するための操作用
		public EditCommand editCommand { get; set; }

		//スクリプト表示の更新用
		public DispCommand dispCommand { get; set; }

		//設定対象
		Command command { get; set; }

		//デリゲートを用いたTypeへの値設定
		public System.Action < ScriptEditor.Command, int > SetFunc { get; set; }

		//------------------------------------------------------
		//初期化
		public void Load ( EditCommand ec, DispCommand dc )
		{
			editCommand = ec;
			dispCommand = dc;
			command = editCommand.Cmd;
		}

		//文字キー押下時
		protected override void  OnKeyPress ( KeyPressEventArgs e )
		{
			char c = e.KeyChar;

			//数字、マイナス、BackSpaceだけ入力可能
			if ( c == '\b' )
			{
				e.Handled = false;
			}
			else if ( c == '-' ) 
			{ 
				e.Handled = false; 
			}
			else if ( ! Char.IsDigit ( c ) )
			{
				e.Handled = true; 
			}

 			base.OnKeyPress(e);
		}

		//キーボード入力時
		protected override void OnKeyDown ( KeyEventArgs e )
		{
#if false
			//テキストが空のとき何もしない
			if ( this.Text.Length == 0 ) { return; }

			//コマンドが０のとき何もしない
			if ( null == command ) { return; }

			//テキストボックスに数値が入力されていてEnterが押されたとき、
			//関連付けられた値を保存
			if ( e.KeyCode == Keys.Enter )
			{
				if ( refInt == null ) { return; }

				int value = 0;
				try
				{
					value = int.Parse ( this.Text );
				}
				catch	//int.Parse(s)が失敗したとき
				{
					System.Media.SystemSounds.Question.Play ();
					return;
				}
				refInt.i = value;

				//ロード時に設定したデリゲートから対象タイプに値を設置
				SetFunc ( command, value );

				//画面の更新
//				dispCommand.Disp ( e );
			}
#endif

			base.OnKeyDown ( e );
		}

		//関連付と更新
		public void Select ( ScriptEditor.Command cmd )
		{
			this.command = cmd;
//            refInt = cmd.LimitTime;
//            Text = cmd.LimitTime.i.ToString();
			Invalidate ();
		}

#if false
		public void Update ( RefInt ri )
		{
			this.refInt = ri;
			this.Text = ri.i.ToString ();
		}

		public void UpdateText ()
		{
			this.Text = this.refInt.i.ToString ();
		}
#endif

		private void InitializeComponent ()
		{
			this.SuspendLayout();
			this.ResumeLayout(false);

		}
	}
}
