using System.Drawing;
using System;

namespace ScriptEditor
{
	//--------------------------------------------
	//	アクションを指定して繰り返し動作する
	//		イメージ表示のみ
	//		[F]に基づく描画
	//--------------------------------------------
	public partial class FormPreview : SubForm_Compend
	{
		//---------------------------------------------------------------------
		//シングルトン実体
		public static FormPreview Inst { get; } = new FormPreview ();

		//プライベートコンストラクタ
		private FormPreview ()
		{
			InitializeComponent ();
			base.InitPt = new Point ( -800, 100 );
			base.LoadObject ();

			GameMain.Pb = this.pictureBox1;
			FrameControl.GameMain = GameMain;
			fcon.FormPreview = this;
			Btn_Advance.Enabled = false;
		}
		//---------------------------------------------------------------------

		private FrameControl fcon = new FrameControl ();
		private GameMain GameMain = new GameMain ();

		public void SetEnviron ( EditCompend ec )
		{
			EditCompend = ec;
		}

		public void SetBmp ( Bitmap bmp )
		{
			this.pictureBox1.Image = bmp;
		}

		protected override void OnShown ( EventArgs e )
		{
			Start ();
			base.OnShown ( e );
		}

		public void Start ()
		{
			GameMain.SetCharaData ( EditChara.Inst.Chara );
			GameMain.SetterBmp = SetBmp;
			GameMain.Seq = EditCompend.SelectedSequence;
			GameMain.Init ();
			fcon.Start ();

			Btn_Start.Enabled = false;
			Btn_Stop.Enabled = true;
		}

		public void _Move ()
		{
			if ( pause )
			{
				if ( advance )
				{
					GameMain.Move ();
					advance = false;
				}
			}
			else
			{
				GameMain.Move ();
			}
		}

		public void _Draw ()
		{
			GameMain.Draw ();
		}

		private void Btn_Start_Click ( object sender, System.EventArgs e )
		{
			Start ();
		}

		private void Btn_Stop_Click ( object sender, System.EventArgs e )
		{
			fcon.End ();
			Btn_Start.Enabled = true;
			Btn_Stop.Enabled = false;
		}

		//--------------------------------------------------------------------------
		//一時停止・コマ送り
		private bool pause = false;
		private bool advance = false;

		private void Btn_Pause_Click ( object sender, EventArgs e )
		{
			if ( pause )
			{
				pause = false;
				Btn_Advance.Enabled = false;
			}
			else
			{
				pause = true;
				Btn_Advance.Enabled = true;
			}
		}

		private void Btn_Advance_Click ( object sender, EventArgs e )
		{
			advance = true;
		}

		//ピクチャボックスのサイズ変更
		private void pictureBox1_SizeChanged ( object sender, EventArgs e )
		{
			GameMain.SetBmpSize ( pictureBox1.Width, pictureBox1.Height );
		}
	}
}
