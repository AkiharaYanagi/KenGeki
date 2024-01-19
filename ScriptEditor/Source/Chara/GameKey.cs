namespace ScriptEditor
{
	//------------------------------------------------------------------
	//	ゲーム入力キー
	//------------------------------------------------------------------
	// ◆ ゲームメインにおける1フレーム中のキー入力状態(レバー, ボタン)
	// ◆ 8方向レバー(Lever)、ボタン４つ(Button)を表す
	//    レバーアイコン表示順は12369874(テンキー表示で１から反時計回り)
	// ◆ レバー別方向も同時押しに対応する
	// ◆ データとして現在フレーム、前フレーム情報を持つ
	// (..., -2, [-1, 0], 2, 3, ...)[F]
	//
	//------------------------------------------------------------------
	// ゲーム入力定義
	//	1[F]における入力の種類
	//
	//	【Lever】
	// [7] [8] [9]
	//
	// [4] [_] [6]
	//				【Button】
	// [1] [2] [3]	,[0], [1], [2], [3]
	//------------------------------------------------------------------

	//====================================================================
	//ゲーム入力保存
	//1[f]と、その前の[F]における入力の状態
	public class GameKeyData
	{
		//方向キー
		public enum Lever
		{
			LVR_1 = 0,
			LVR_2 = 1,
			LVR_3 = 2,
			LVR_6 = 3,
			LVR_9 = 4,
			LVR_8 = 5,
			LVR_7 = 6,
			LVR_4 = 7,
		}
		public const int LVR_NUM = 8;	//個数
		
		//ボタン
		public const int BTN_NUM = 4;	//個数

		//方向キー状態
		public bool [] Lvr { set; get; } = new bool [ LVR_NUM ];
		public bool [] PreLbr { set; get; } = new bool [ LVR_NUM ];
 
		//ボタン状態
		public bool [] Btn { set; get; } = new bool [ BTN_NUM ];
		public bool [] PreBtn { set; get; } = new bool [ BTN_NUM ];

		//--------------------------------------------------------------------
		//コンストラクタ
		public GameKeyData ()
		{
			int LN = LVR_NUM;
			for ( int i = 0; i < LN; ++ i ) { Lvr[ i ] = false; }
			for ( int i = 0; i < LN; ++ i ) { PreLbr[ i ] = false; }

			int BN = BTN_NUM;
			for ( int i = 0; i < BN; ++ i ) { Btn[ i ] = false; }
			for ( int i = 0; i < BN; ++ i ) { PreBtn[ i ] = false; }
		}

		//コピーコンストラクタ
		public GameKeyData ( GameKeyData gk )
		{
			int DN = LVR_NUM;
			for ( int i = 0; i < DN; ++ i ) { this.Lvr[ i ] = gk.Lvr[ i ]; }
			for ( int i = 0; i < DN; ++ i ) { this.PreLbr[ i ] = gk.PreLbr[ i ]; }

			int BN = BTN_NUM;
			for ( int i = 0; i < BN; ++ i ) { this.Btn[ i ] = gk.Btn[ i ]; }
			for ( int i = 0; i < BN; ++ i ) { this.PreBtn[ i ] = gk.PreBtn[ i ]; }
		}
	}

}

