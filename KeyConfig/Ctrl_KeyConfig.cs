using System;
using System.Windows.Forms;

using SlimDX.DirectInput;


namespace ScriptEditor
{
	//ラジオボタンとテキストボックス
	public struct RBTB
	{
		public RadioButton rb;
		public TextBox tb;
		public void Set ( RadioButton r, TextBox t ) { rb = r; tb = t; }
		public static bool operator == ( RBTB rbtb0, RBTB rbtb1 )
		{
			bool b0 = ( rbtb0.rb == rbtb1.rb );
			bool b1 = ( rbtb0.tb == rbtb1.tb );
			return b0 && b1;
		}
		public static bool operator != ( RBTB rbtb0, RBTB rbtb1 )
		{
			bool b0 = ( rbtb0.rb != rbtb1.rb );
			bool b1 = ( rbtb0.tb != rbtb1.tb );
			return b0 && b1;
		}
		public override bool Equals ( object obj )
		{
			return base.Equals ( obj ); 
		}
		public override int GetHashCode ()
		{
			return base.GetHashCode ();
		}
	}

	//コントロール総合
	public partial class Ctrl_KeyConfig : UserControl
	{
		//対象データ
		private KeySettings keyStgs = new KeySettings ();

		//コントロール操作用
		private RBTB [] ARY_RBTB;
		private const int RBTB_NUM = 24;

		//DirectInput入力
		private DxInput dxInput = new DxInput ();

		//現在ラジオボタン位置
		private RBTB rbtbPos;


		//コンストラクタ
		public Ctrl_KeyConfig ()
		{
			InitializeComponent ();

			//入力初期化
			dxInput.Load ();

			//自動読込
			keyStgs.Load ();

			//コントロール操作用
			ARY_RBTB = new RBTB [ RBTB_NUM ];

			ARY_RBTB [ (int)GameInput.P1_UP		].Set ( RB_P1Up,	Tb_P1Up );
			ARY_RBTB [ (int)GameInput.P1_DOWN	].Set ( RB_P1Down,	Tb_P1Down );
			ARY_RBTB [ (int)GameInput.P1_LEFT	].Set ( RB_P1Left,	Tb_P1Left );
			ARY_RBTB [ (int)GameInput.P1_RIGHT	].Set ( RB_P1Right,	Tb_P1Right );
			ARY_RBTB [ (int)GameInput.P1_KEY0	].Set ( RB_P1Btn0,	Tb_P1Btn0 );
			ARY_RBTB [ (int)GameInput.P1_KEY1	].Set ( RB_P1Btn1,	Tb_P1Btn1 );
			ARY_RBTB [ (int)GameInput.P1_KEY2	].Set ( RB_P1Btn2,	Tb_P1Btn2 );
			ARY_RBTB [ (int)GameInput.P1_KEY3	].Set ( RB_P1Btn3,	Tb_P1Btn3 );
			ARY_RBTB [ (int)GameInput.P1_KEY4	].Set ( RB_P1Btn4,	Tb_P1Btn4 );
			ARY_RBTB [ (int)GameInput.P1_KEY5	].Set ( RB_P1Btn5,	Tb_P1Btn5 );
			ARY_RBTB [ (int)GameInput.P1_KEY6	].Set ( RB_P1Btn6,	Tb_P1Btn6 );
			ARY_RBTB [ (int)GameInput.P1_KEY7	].Set ( RB_P1Btn7,	Tb_P1Btn7 );
					   
			ARY_RBTB [ (int)GameInput.P2_UP		].Set ( RB_P2Up,	Tb_P2Up );
			ARY_RBTB [ (int)GameInput.P2_DOWN	].Set ( RB_P2Down,	Tb_P2Down );
			ARY_RBTB [ (int)GameInput.P2_LEFT	].Set ( RB_P2Left,	Tb_P2Left );
			ARY_RBTB [ (int)GameInput.P2_RIGHT	].Set ( RB_P2Right,	Tb_P2Right );
			ARY_RBTB [ (int)GameInput.P2_KEY0	].Set ( RB_P2Btn0,	Tb_P2Btn0  );
			ARY_RBTB [ (int)GameInput.P2_KEY1	].Set ( RB_P2Btn1,	Tb_P2Btn1  );
			ARY_RBTB [ (int)GameInput.P2_KEY2	].Set ( RB_P2Btn2,	Tb_P2Btn2  );
			ARY_RBTB [ (int)GameInput.P2_KEY3	].Set ( RB_P2Btn3,	Tb_P2Btn3  );
			ARY_RBTB [ (int)GameInput.P2_KEY4	].Set ( RB_P2Btn4,	Tb_P2Btn4  );
			ARY_RBTB [ (int)GameInput.P2_KEY5	].Set ( RB_P2Btn5,	Tb_P2Btn5  );
			ARY_RBTB [ (int)GameInput.P2_KEY6	].Set ( RB_P2Btn6,	Tb_P2Btn6  );
			ARY_RBTB [ (int)GameInput.P2_KEY7	].Set ( RB_P2Btn7,	Tb_P2Btn7  );

			//コントロール共通初期化
			foreach ( RBTB rbtb in ARY_RBTB )
			{
				rbtb.rb.PreviewKeyDown += new PreviewKeyDownEventHandler ( RB_PreviewKeyDown );
				rbtb.rb.CheckedChanged += new EventHandler ( RB_Seleceted );
			}

			//関連付け
			Assosiate ( keyStgs );

			//選択位置
			rbtbPos = ARY_RBTB [ 0 ];

			//タイマ
			Timer timer = new Timer ();
			timer.Tick += (e,s)=>
			{
				//入力の更新と取得
				dxInput.Update ();
				DeviceInput di = dxInput.PushInput ();

				//入力があったら対象データとテキストボックスを更新して次へ
				if ( DeviceType.Other != di.Type )
				{
					keyStgs.Set ( GetGameInput ( rbtbPos ), di );
					rbtbPos.tb.Text = di.ToString();
					RBTB_Next ( rbtbPos );
					rbtbPos.rb.Checked = true;
				}
			};
			timer.Interval = 16;
			timer.Start ();
		}


		//ラジオボタン共通イベント　カーソルキーでフォーカスの移動を禁止する
		private void RB_PreviewKeyDown ( object sender, PreviewKeyDownEventArgs e )
		{
			switch ( e.KeyCode )
			{
			case Keys.Up:
			case Keys.Down:
			case Keys.Left:
			case Keys.Right:
				e.IsInputKey = true;
				break;
			}
		}

		//ラジオボタン選択時
		private void RB_Seleceted ( object sender, EventArgs e )
		{
			//発生元を変換
			RadioButton rb = (RadioButton)sender;

			//検索
			foreach ( RBTB rbtb in ARY_RBTB )
			{
				if ( rbtb.rb == rb )
				{
					//選択
					rbtbPos = rbtb;
					break;
				}
			}
		}


		//@info timerからでもstaticでないメンバ変数が変更可能かどうか
		//->	ラムダ式でアクセス可能

		//ラジオボタンを次へ
		private void RBTB_Next ( RBTB rbtb )
		{
			//全検索
			for ( int i = 0; i < RBTB_NUM; ++ i ) 
			{
				//該当したら
				if ( rbtb.rb == ARY_RBTB [ i ].rb )
				{
					//末尾の場合
					if ( i == RBTB_NUM - 1 )
					{
						rbtbPos = ARY_RBTB [ 0 ];
					}
					else
					{
						rbtbPos = ARY_RBTB [ i + 1 ];	// i + 1
					}
				}
			}
		}

		//データ関連付け
		public void Assosiate ( KeySettings stgs )
		{
			foreach ( GameInput gmIpt in Enum.GetValues ( typeof ( GameInput ) ) )
			{
				DeviceInput dvcIpt = stgs.Dct_Gm_Dvc [ gmIpt ];

				ARY_RBTB [ (int)gmIpt ].tb.Text = dvcIpt.ToString();
			}
		}

		//RBTBの選択からGameInputを取得する
		private GameInput GetGameInput ( RBTB rbtb )
		{
			int i = 0;
			foreach ( RBTB _rbtb in ARY_RBTB )
			{
				if ( rbtb == _rbtb )
				{
					return (GameInput)i;
				}
				++ i;
			}
			return GameInput.P1_UP;
		}

		//フォルダ
		private void Btn_Folder_Click ( object sender, EventArgs e )
		{
			FormUtility.OpenCurrentDir ( );
		}

		//保存
		private void Btn_Save_Click ( object sender, EventArgs e )
		{
			keyStgs.Save ();
		}

	}
}
