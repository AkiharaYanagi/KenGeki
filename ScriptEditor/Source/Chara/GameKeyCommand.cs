namespace ScriptEditor
{
	//------------------------------------------------------------------
	//	ゲームキー　コマンド
	//------------------------------------------------------------------
	// ◆格闘ゲーム入力におけるキャラの入力
	// ◆レバーは内部的に全方位別で記録されるが、外部からのコマンド的には１方向を指定する
	// ◆方向要素入力(2E, 6E, 8E, 4E)も判定する
	// ◆bool 方向(右正)(左時は左右のみ反転)
	// ◆bool 否定
	//  コマンドとして「否定」フラグ→「指定のゲームキー状態でなくなったとき」
	//------------------------------------------------------------------
	//下方向要素(2E)	前方向要素(6E)	上方向要素(8E)	後方向要素(4E)
	//  * * *		  * * 9			  7 8 9			  7 * *
	//  * * *		  * * 6			  * * *			  4 * *
	//  1 2 3		  * * 3			  * * *			  1 * *
	//------------------------------------------------------------------
	//
	// ◆判定は7種類
	//	[__](押した状態)		
	//	[--](離した状態)		
	//	[-_](押した瞬間)		
	//	[_-](離した瞬間)		
	//	[**](どの状態でも)	
	//	[*_] 現在のみ押した状態
	//	[*-] 現在のみ離した状態
	// 
	//-------------------------------------------------------------------

	using GK_L = GameKeyCommand.LeverCommand;
	using GKC_ST = GameKeyCommand.GameKeyCommandState;

	//====================================================================
	//1[F]のコマンド条件に用いるクラス
	//データはレバー全方向にデータを持つが、判定上はレバー１種類だけ取得/設定する
	//キャラを受けて向きの前後の判定
	//～でない（否定）の条件
	//====================================================================
	public class GameKeyCommand
	{
		//---------------------------------------------------
		//キー入力状態
		public enum GameKeyCommandState
		{
			KEY_OFF = 0,		// 入力無
			KEY_ON = 1,			// 入力有
			KEY_PUSH = 2,		// 押した瞬間
			KEY_RELE = 3,		// 離した瞬間
			KEY_WILD = 4,		// どの状態でも
			KEY_IS = 5,			// 現在のみ押した状態
			KEY_NIS = 6,		// 現在のみ離した状態
		}
		//@define	using GKC_ST = GameKeyCommand.GameKeyCommandState;

		//---------------------------------------------------
		//レバー入力種類
		public enum LeverCommand
		{
			//方向
			LVR_CMD_N = 0,	//入力無
			LVR_CMD_1 = 1,
			LVR_CMD_2 = 2,
			LVR_CMD_3 = 3,
			LVR_CMD_6 = 4,
			LVR_CMD_9 = 5,
			LVR_CMD_8 = 6,
			LVR_CMD_7 = 7,
			LVR_CMD_4 = 8,

			//方向要素(Element)(いずれか)
			LVR_CMD_2E = 9,
			LVR_CMD_6E = 10,
			LVR_CMD_8E = 11,
			LVR_CMD_4E = 12,
		}
		//@define	using GK_L = GameKeyCommand.LeverCommand;
		public const int LeverCommandNum = 13;

		//---------------------------------------------------
		//判定状態
		public GKC_ST [] Lvr { set; get; } = new GKC_ST [ LeverCommandNum ];

		//現在レバーインデックス
		public int IdLvr { get; set; } = 0;


		//---------------------------------------------------
		//ボタン状態
		public const int BtnNum = 4;
		public GKC_ST [] Btn { set; get; } = new GKC_ST [ BtnNum ];

		//---------------------------------------------------
		//否定のフラグ
		public bool Not { get; set; } = false;


		//======================================================================================
		//コンストラクタ
		public GameKeyCommand ()
		{
			for ( int i = 0; i < LeverCommandNum; ++ i )
			{
				Lvr [ i ] = GKC_ST.KEY_WILD;
			}
			for ( int i = 0; i < BtnNum; ++ i )
			{
				Btn [ i ] = GKC_ST.KEY_WILD;
			}
		}


		//---------------------------------------------------
#if false

		//現在の対象レバーインデックスを取得
		public int GetIndexLever ()
		{
			for ( int i = 0; i < LeverCommandNum; ++ i )
			{
				if ( GKC_ST.KEY_WILD != Lvr[i] ) { return i; }
			}
			return 0;
		}

		public void SetIndexLever ( int index )
		{
			if ( index < 0 || LeverCommandNum <= index ) { return; }
			Lvr [ index ] = GKC_ST.KEY_PUSH;
		}
#endif

		//現在の対象レバー種類を取得
		public GK_L GetLever ()
		{

			for ( int i = 0; i < LeverCommandNum; ++ i )
			{
//				if ( GKC_ST.KEY_WILD != Lvr[i] ) { return (GK_L)i; }
				if ( GKC_ST.KEY_ON == Lvr[i] ) { return (GK_L)i; }
				if ( GKC_ST.KEY_PUSH == Lvr[i] ) { return (GK_L)i; }
			}
			return GK_L.LVR_CMD_N;
#if false
			return Lvr [ IdLvr ];
#endif
		}

		//レバー設定
		public void SetLever ( GK_L gkl )
		{
			switch ( gkl )
			{
			case GK_L.LVR_CMD_1: 
				ClearLever ();
				Lvr [ (int) GK_L.LVR_CMD_4 ] = GKC_ST.KEY_OFF;
				Lvr [ (int) GK_L.LVR_CMD_1 ] = GKC_ST.KEY_ON;
				Lvr [ (int) GK_L.LVR_CMD_2 ] = GKC_ST.KEY_OFF;
			break;
			}
		}

		//レバー状態指定を取得
		public GKC_ST GetLvrSt ()
		{
			return Lvr [ IdLvr ];
		}

		//レバー状態指定を設定
		public void SetLvrSt ( GKC_ST st )
		{
			Lvr [ IdLvr ] = st;
		}

		//レバー状態初期化
		private void ClearLever ()
		{
			for ( int i = 0; i < Lvr.Length; ++ i )
			{
				Lvr[i] = GKC_ST.KEY_WILD;
			}
		}

		//レバー回転
		public void Lever_L ()
		{
			GKC_ST st = GetLvrSt ();
			ClearLever ();
			
			++ IdLvr;
			if ( LeverCommandNum - 1 == IdLvr ) { IdLvr = 0; }
			SetLvrSt ( st );
		}

		public void Lever_R ()
		{
			GKC_ST st = GetLvrSt ();
			ClearLever ();
			
			-- IdLvr;
			if ( 0 > IdLvr ) { IdLvr = LeverCommandNum - 2; }
			SetLvrSt ( st );
		}


		//比較
		//thisの状態がチェックするコマンド条件、引数がプレイヤ入力
		//引数：コマンド成立条件となるゲームキー状態, キャラクタ向き(右正)
		//戻値：適合したらtrue、それ以外はfalse
		public bool CompareTarget ( GameKeyData gameKeyData, bool dirRight )
		{
			//チェック用(反転などの操作をするためにディープコピーとする)
			GameKeyData gk = new GameKeyData ( gameKeyData );

			//比較するかどうか(条件がワイルドの時は比較しない)
			bool[] bWildLvr = new bool [ LeverCommandNum ];
			bool[] bWildBtn = new bool[ GameKeyData.BTN_NUM ] { false, false, false, false };

			//比較結果
			bool[] b_Lvr = new bool [ LeverCommandNum ];
			bool[] b_Btn = new bool[ GameKeyData.BTN_NUM ] { false, false, false, false };

			//-----------------------------------------------------------------
			//左向きのとき左右を入れ替え
			FlipData ( gk, dirRight );

			//-----------------------------------------------------------------
			//比較
			//レバー
			CompareKey ( LeverCommandNum, Lvr, bWildLvr, b_Lvr, gameKeyData.Lvr, gameKeyData.PreLbr );
			//ボタン
			CompareKey ( GameKeyData.BTN_NUM, Btn, bWildBtn, b_Btn, gameKeyData.Btn, gameKeyData.PreBtn );

			//-----------------------------------------------------------------
			//まとめ

			//すべてワイルドの場合true
			if ( AllTrue ( bWildLvr ) && AllTrue ( bWildBtn ) ) { return true; }

			//いずれかを返す場合
			bool ret = true;

			//レバー
			ret &= Check ( LeverCommandNum, bWildLvr, b_Lvr );

			//ボタン
			ret &= Check ( GameKeyData.BTN_NUM, bWildBtn, b_Btn );

			//否定の場合は反転して返す (排他的論理和)
			return ret ^ this.Not;
		}


		//キーデータ左右入替
		private void FlipData ( GameKeyData gk, bool dirRight )
		{
			//左向きのとき左右を入れ替え
			//7<-->9
			//4<-->6
			//1<-->3
			if ( ! dirRight )
			{
				bool tempBool;

				tempBool = gk.Lvr [ (int)GK_L.LVR_CMD_1 ];
				gk.Lvr [ (int)GK_L.LVR_CMD_1 ] = gk.Lvr [ (int)GK_L.LVR_CMD_3 ];
				gk.Lvr [ (int)GK_L.LVR_CMD_3 ] = tempBool;

				tempBool = gk.Lvr [ (int)GK_L.LVR_CMD_4 ];
				gk.Lvr [ (int)GK_L.LVR_CMD_4 ] = gk.Lvr [ (int)GK_L.LVR_CMD_6 ];
				gk.Lvr [ (int)GK_L.LVR_CMD_6 ] = tempBool;

				tempBool = gk.Lvr [ (int)GK_L.LVR_CMD_7 ];
				gk.Lvr [ (int)GK_L.LVR_CMD_7 ] = gk.Lvr [ (int)GK_L.LVR_CMD_9 ];
				gk.Lvr [ (int)GK_L.LVR_CMD_9 ] = tempBool;
			}
		}

		//すべてtrueかどうか
		private bool AllTrue ( bool [] bAry )
		{
			foreach ( bool b in bAry )
			{
				if ( ! b ) { return false; }
			}
			return true;
		}

		//比較
		private void CompareKey ( int num,  GKC_ST [] stAry, bool[] bWildAry, bool[] bAry, bool[] bDataAry, bool[] bPreAry )
		{
			for ( int i = 0; i < num; ++ i )
			{
				bool b = bDataAry[i];
				bool pb = bPreAry[i];

				switch ( stAry [ i ] )
				{
				//条件がワイルドの場合は比較しない
				case GKC_ST.KEY_WILD: bWildAry[i] = true; break;
				case GKC_ST.KEY_ON  : bAry[i] = b; break;
				case GKC_ST.KEY_OFF : bAry[i] = ! b; break;
				case GKC_ST.KEY_PUSH: bAry[i] = b && ! pb; break;
				case GKC_ST.KEY_RELE: bAry[i] = ! b && pb; break;
				}
			}
		}

		//すべて調査対象かつ適合かどうか
		private bool Check ( int num, bool [] bWildAry, bool [] bCheckAry )
		{
			for ( int i = 0; i < num; ++ i )
			{
				//一つでも調査対象が不適であったらその時点でfalse
				if ( bWildAry[i] ) { if ( bCheckAry[i] ) { return false; } }
			}			
			//すべて適合だったらtrue
			return true;
		}

	}
}

