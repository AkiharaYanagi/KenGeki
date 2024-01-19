using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace ScriptEditor
{
	using GK_L = GameKeyCommand.LeverCommand;
	using GKC_ST = GameKeyCommand.GameKeyCommandState;

	public partial class Pb_Command : PictureBox
	{
		//対象データ
		private Command Cmd = new Command ();

		//選択
		public SelectKey SlctKey { get; set; } = new SelectKey ();

		//表示
		public DispCommand DspCmd { get; set; } = new DispCommand ();

		//ラジオボタンの参照
		public RadioButton RB_OFF { get; set; } = new RadioButton ();
		public RadioButton RB_PUSH { get; set; } = new RadioButton ();
		public RadioButton RB_RELE { get; set; } = new RadioButton ();
		public RadioButton RB_ON { get; set; } = new RadioButton ();
		public RadioButton RB_WILD { get; set; } = new RadioButton ();
		public RadioButton RB_IS { get; set; } = new RadioButton ();
		public RadioButton RB_NIS { get; set; } = new RadioButton ();


		//--------------------------------------------------------------------------
		//コンストラクタ
		public Pb_Command ()
		{
			InitializeComponent ();
		}

		public Pb_Command ( IContainer container )
		{
			container.Add ( this );
			InitializeComponent ();
		}

		//データの設定
		public void Set ( Command cmd )
		{
			Cmd = cmd;
			DspCmd.Set ( cmd );
			Invalidate ();
		}


		//描画
		private void Pb_Command_Paint ( object sender, PaintEventArgs e )
		{
			DspCmd.Disp ( e );
		}

		//--------------------------------------------------------------------------
		//マウスイベント
		private const int W = 48;	//升幅
		private const int H = 48;	//升高
		private const int BX = 48;	//基準X
		private const int BY = 48;	//基準Y

		private void Pb_Command_MouseDown ( object sender, MouseEventArgs e )
		{
			Point pt = GetCell ();

			//範囲外は何もしない
			int nx = Cmd.ListGameKeyCommand.Count - 1;
			int ny = (int)SelectKey.KeyKind.KEY_H;
			if ( pt.X < 0 || pt.Y < 0 || nx < pt.X || ny < pt.Y ) 
			{
				return; 
			}

			//選択
			SlctKey.Set ( Cmd, pt.X, (SelectKey.KeyKind)pt.Y );

			//レバー位置選択
			SetLvrCell ();

			//ラジオボタンに反映
			RB_Disp ( SlctKey.GetSt ( Cmd ) );
			
			Invalidate ();
		}

		//マウス位置からフレーム表の升目位置を取得する
		private Point GetCell () 
		{
			//マウス位置をコントロールのクライアント位置に直す
			Point pt = PointToClient ( Cursor.Position );
			if ( pt.X < BX || pt.Y < BY ) { return new Point ( -1, -1 ); }

			//升目の位置を計算する
			int pos_x = ( pt.X - BX ) / W;
			int pos_y = ( pt.Y - BY ) / H;

			return new Point ( pos_x, pos_y );
		}

		//マウス位置からレバー選択の升目位置を設定する
		//マウスイベント
		private const int LvrW = 16;	//レバー升幅
		private const int LvrH = 16;	//レバー升高
		private void SetLvrCell ()
		{
			//マウス位置をコントロールのクライアント位置に直す
			Point pt = PointToClient ( Cursor.Position );
			Point pos = GetCell ();

			//範囲チェック
			int nx = Cmd.ListGameKeyCommand.Count - 1;
			if ( pos.X < 0 || nx < pos.X ) { return; }
			if ( pos.Y != 0 ) { return; }

			int base_x = BX + pos.X * W;
			int base_y = BY - pos.Y * H;
			
			//レバー升位置 ( { (0,1,2), (0,1,2) } )
			int px = ( pt.X - base_x ) / LvrW;
			int py = ( pt.Y - base_y ) / LvrH;

			//中心位置のときは何もしない
			if ( px == 1 && py == 1 ) { return; }

			//表示に位置を渡す
			int index = px + py * 3;
			DspCmd.SlctKey.SelectedIndex = index;

			System.Diagnostics.Debug.Write ( "SetLvrCell(); " );
			System.Diagnostics.Debug.WriteLine ( index );

#if false
			//回転順のインデックスに変換
			//  6 5 4
			//  7 * 3
			//  0→1 2
			int index = px + py * 3;
			int[] ary = { 7, 6, 5, 8, 0, 4, 1, 2, 3 };
			if ( -1 == ary[index] ) { return; }

			DspCmd.SelectedIndex = ary [ index ];
#endif
		}

		//ラジオボタンに反映
		private void RB_Disp ( GKC_ST gkcst )
		{
			switch ( gkcst )
			{
			case GKC_ST.KEY_OFF : RB_OFF.Checked  = true; break;
			case GKC_ST.KEY_PUSH: RB_PUSH.Checked = true; break;
			case GKC_ST.KEY_RELE: RB_RELE.Checked = true; break;
			case GKC_ST.KEY_ON  : RB_ON.Checked   = true; break;
			case GKC_ST.KEY_WILD: RB_WILD.Checked = true; break;
			case GKC_ST.KEY_IS  : RB_IS.Checked   = true; break;
			case GKC_ST.KEY_NIS : RB_NIS.Checked  = true; break;
			}
		}
	}
}
