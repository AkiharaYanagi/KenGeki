using System;
using System.Windows.Forms;

namespace ScriptEditor
{
	//----------------------------------------------------------------------
	// 数値のみ入力でき、関連付けられたRefFlaotを変更するテキストボックス
	//----------------------------------------------------------------------
	public class TB_Float : TextBox
	{
		//関連付けられる数値
		public RefFlaot refFlaot { get; set; }

		//全てのスクリプトに適用するための操作用
		public EditCompend editCompend { get; set; }

		//スクリプト表示の更新用
		public DispCompend dispCompend { get; set; }


		//選択スクリプトに適用するための操作用
//		public List<RefFlaot> listRefFlaot { get; set; }

		//スクリプト中の識別アドレス
		public ScriptAddress scriptAddress { get; set; }


		//初期化
		public void Load ( EditCompend ec, DispCompend dc )
		{
			editCompend = ec;
			dispCompend = dc;
		}

		//キー押下時
		protected override void  OnKeyPress ( KeyPressEventArgs e )
		{
			char c = e.KeyChar;

			//数字、小数点、マイナス、BackSpaceだけ入力可能
//			if ( c == ( Char ) Keys.Enter ) { e.Handled = true; }
			if ( Char.IsDigit ( c ) || c == '.' || c == '-' || c == '\b' )
			{
				e.Handled = false;
			}

 			base.OnKeyPress(e);
		}

		//キー入力時
		protected override void OnKeyDown ( KeyEventArgs e )
		{
			//テキストが空のとき何もしない
			if ( this.Text.Length == 0 ) { return; }

			//テキストボックスに数値が入力されていてEnterが押されたとき、
			//関連付けられた値を保存
			if ( e.KeyCode == Keys.Enter )
			{
				if ( refFlaot == null ) { return; }

				float value = 0;
				try
				{
					value = float.Parse ( this.Text );
				}
				catch	//float.Parse(s)が失敗したとき
				{
					System.Media.SystemSounds.Question.Play ();
					return;
				}
				refFlaot.f = value;

				//他スクリプト
#if false
				//変更後に全てのスクリプトにコピーするかどうか
//				formMain.CheckSetOtherScript ();

				//スクリプト選択範囲にこの部分だけをコピーするかどうか
//				if ( formMain.spanScript )
				if ( editCompend.spanScript )
				{
					if ( null != scriptAddress ) { return; }

					//アドレスと値を通達する
					editCompend.EditSelectedContents ( scriptAddress, value );
				}
				editCompend.CheckSetOtherScript ();
#endif


				//画面の更新
				dispCompend.Disp ();
			}

			base.OnKeyDown ( e );
		}

		//関連付と更新
		public void Update ( RefFlaot rf )
		{
			this.refFlaot = rf;
			this.Text = rf.f.ToString ();
		}

		public void UpdateText ()
		{
			this.Text = this.refFlaot.f.ToString ();
		}

	}
}
