namespace ScriptEditor
{
	//------------------------------------------------------------------
	//	ゲーム入力キー
	//------------------------------------------------------------------
	// ゲームメインにおける1フレーム中のキー入力状態(レバー, ボタン)
	// 8方向レバー(Lever)、ボタン４つ(Button)を表す
	// レバーも同時押しに対応する
	//
	//	Lever
	// [7] [8] [9]
	//
	// [4] [_] [6]
	//				Button
	// [1] [2] [3]	,[0], [1], [2], [3]
	//
	// データとして今フレーム、前フレーム情報を持つ [**]
	// 判定は5種類
	//	(押した状態)[__]
	//	(離した状態)[--]
	//	(押した瞬間)[-_]
	//	(離した瞬間)[_-]
	//	(どの状態でも)[**]
	// 
	//------------------------------------------------------------------
	//
	// レバーアイコン表示順は12369874(テンキー表示で１から反時計回り)
	// レバーにも瞬間の状態を記述
	//
	// コマンドとして「否定」フラグ→「指定のゲームキー状態でなくなったとき」
	// 『入力状態を持つクラス』と『コマンドの１フレーム条件のクラス』を分割する

	//-------------------------------------------------------------------

	using GK_L = GameKeyCommand.LeverCommand;
	using GKC_ST = GameKeyCommand.GameKeyCommandState;

	//====================================================================
	//ゲーム入力定義
	//	1[F]における入力の種類
	public static class _GameKey
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
	}
	//====================================================================

	//====================================================================
	//入力保存用
	//1[f]と、その前の[F]における入力の状態
	public class GameKeyData
	{
		//方向キー状態
		public bool [] Lvr { set; get; } = new bool [ _GameKey.LVR_NUM ];
		public bool [] PreLbr { set; get; } = new bool [ _GameKey.LVR_NUM ];
 
		//ボタン状態
		public bool [] Btn { set; get; } = new bool [ _GameKey.BTN_NUM ];
		public bool [] PreBtn { set; get; } = new bool [ _GameKey.BTN_NUM ];

		//--------------------------------------------------------------------
		//コンストラクタ
		public GameKeyData ()
		{
			int LN = _GameKey.LVR_NUM;
			for ( int i = 0; i < LN; ++ i ) { Lvr[ i ] = false; }
			for ( int i = 0; i < LN; ++ i ) { PreLbr[ i ] = false; }

			int BN = _GameKey.BTN_NUM;
			for ( int i = 0; i < BN; ++ i ) { Btn[ i ] = false; }
			for ( int i = 0; i < BN; ++ i ) { PreBtn[ i ] = false; }
		}

		//コピーコンストラクタ
		public GameKeyData ( GameKeyData gk )
		{
			int DN = _GameKey.LVR_NUM;
			for ( int i = 0; i < DN; ++ i ) { this.Lvr[ i ] = gk.Lvr[ i ]; }
			for ( int i = 0; i < DN; ++ i ) { this.PreLbr[ i ] = gk.PreLbr[ i ]; }

			int BN = _GameKey.BTN_NUM;
			for ( int i = 0; i < BN; ++ i ) { this.Btn[ i ] = gk.Btn[ i ]; }
			for ( int i = 0; i < BN; ++ i ) { this.PreBtn[ i ] = gk.PreBtn[ i ]; }
		}
	}


	//====================================================================
	//1[F]のコマンド条件に用いるクラス

	//データはレバー全方向にデータを持つが、判定上はレバー１種類だけ取得/設定する

	//前方向要素(6E)
	//	* * 9
	//	* * 6
	//	* * 3

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
			KEY_WILD = 4		// どの状態でも
		}
		//@define	using GKC_ST = GameKeyCommand.GameKeyCommandState;

		//---------------------------------------------------
		//レバー入力種類
		public enum LeverCommand
		{
			//方向
			NEUTRAL = 0,	//入力無
			DIR_1 = 1,
			DIR_2 = 2,
			DIR_3 = 3,
			DIR_6 = 4,
			DIR_9 = 5,
			DIR_8 = 6,
			DIR_7 = 7,
			DIR_4 = 8,

			//方向要素(Element)(いずれか)
			DIR_2E = 9,
			DIR_6E = 10,
			DIR_8E = 11,
			DIR_4E = 12,
		}
		public const int LeverCommandNum = 13;

		//判定状態
		public GKC_ST [] Lvr { set; get; } = new GKC_ST [ LeverCommandNum ];

		//現在インデックス
		public int IdLvr { get; set; } = 0;


		//---------------------------------------------------
		//ボタン状態
		private const int BtnNum = 4;
		public GKC_ST [] Btn { set; get; } = new GKC_ST [ BtnNum ];

		//否定のフラグ
		public bool Not { get; set; } = false;


		//---------------------------------------------------
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
			return GK_L.NEUTRAL;
		}

		//レバー設定
		public void SetLever ( GK_L gkl )
		{
			switch ( gkl )
			{
			case GK_L.DIR_1: 
				ClearLever ();
				Lvr [ (int) GK_L.DIR_4 ] = GKC_ST.KEY_OFF;
				Lvr [ (int) GK_L.DIR_1 ] = GKC_ST.KEY_ON;
				Lvr [ (int) GK_L.DIR_2 ] = GKC_ST.KEY_OFF;
			break;
			}
		}

		//レバー状態指定を取得
		public GKC_ST GetLvrSt ()
		{
			return Lvr [ (int)GetLever() ];
		}

		//レバー状態指定を設定
		public void SetLvrSt ( GKC_ST st )
		{
			Lvr [ (int)GetLever() ] = st;
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
			bool[] bWildBtn = new bool[_GameKey.BTN_NUM] { false, false, false, false };

			//比較結果
			bool[] b_Lvr = new bool [ LeverCommandNum ];
			bool[] b_Btn = new bool[_GameKey.BTN_NUM] { false, false, false, false };

			//-----------------------------------------------------------------
			//左向きのとき左右を入れ替え
			FlipData ( gk, dirRight );

			//-----------------------------------------------------------------
#if false
			if ( this.Dir != GKD_ST.DIR_WILD )
			{
				bCheck_i = true;

				//要素判定
				switch ( this.Dir )
				{
				case GKD_ST.DIR_2E:	//下要素
					if (	this.Dir == GK_D.DIR_1
						||	this.Dir == GK_D.DIR_2
						||	this.Dir == GK_D.DIR_3 ) { b_i = true; }
					break;
				case GKD_ST.DIR_6E:	//右要素
					if (	this.Dir == GK_D.DIR_9
						||	this.Dir == GK_D.DIR_6
						||	this.Dir == GK_D.DIR_3 ) { b_i = true; }
					break;
				case GKD_ST.DIR_8E:	//上要素
					if (	this.Dir == GK_D.DIR_7
						||	this.Dir == GK_D.DIR_8
						||	this.Dir == GK_D.DIR_9 ) { b_i = true; }
					break;
				case GKD_ST.DIR_4E:	//左要素
					if (	this.Dir == GK_D.DIR_7
						||	this.Dir == GK_D.DIR_4
						||	this.Dir == GK_D.DIR_1 ) { b_i = true; }
					break;
				default:
					if ( gameKey.Dir == this.Dir ) { b_i = true; }
					break;
				}
			}
#endif

#if false
			//レバー

			for ( int i = 0; i < LeverCommandNum; ++ i )
			{
				//条件がワイルドの場合は比較しない
				if ( GKC_ST.KEY_WILD == Lvr[i] )
				{
					bWildLvr[i] = true;
				}
				else 
				{
					bool b = gameKeyData.Lvr[i];
					bool pb = gameKeyData.PreLbr[i];

					switch ( Lvr [ i ] )
					{
					case GKC_ST.KEY_ON  : b_Lvr[i] = b; break;
					case GKC_ST.KEY_OFF : b_Lvr[i] = ! b; break;
					case GKC_ST.KEY_PUSH: b_Lvr[i] = b && ! pb; break;
					case GKC_ST.KEY_RELE: b_Lvr[i] = ! b && pb; break;
					}
				}
			}

			//-----------------------------------------------------------------
			//ボタン
			for ( int i = 0; i < _GameKey.BTN_NUM; ++ i )
			{
				//条件がワイルドの場合は比較しない
				if ( GKC_ST.KEY_WILD == Btn[i] )
				{
					bWildBtn [ i ] = true;
				}
				else
				{
					bool b = gameKeyData.Btn[i];
					bool pb = gameKeyData.PreBtn[i];

					switch ( Btn [ i ] )
					{
					case GKC_ST.KEY_ON  : b_Btn[i] = b; break;
					case GKC_ST.KEY_OFF : b_Btn[i] = ! b; break;
					case GKC_ST.KEY_PUSH: b_Btn[i] = b && ! pb; break;
					case GKC_ST.KEY_RELE: b_Btn[i] = ! b && pb; break;
					}
				}
			}
#endif
			//レバー
			CompareKey ( LeverCommandNum, Lvr, bWildLvr, b_Lvr, gameKeyData.Lvr, gameKeyData.PreLbr );
			//ボタン
			CompareKey ( _GameKey.BTN_NUM, Btn, bWildBtn, b_Btn, gameKeyData.Btn, gameKeyData.PreBtn );

			//-----------------------------------------------------------------
			//まとめ
#if false
			//ひとつもチェックしない場合はfalseを返す(否定の場合もfalse)
			if ( ! bCheck_i && ! bCheck[0] && ! bCheck[1] && ! bCheck[2] && ! bCheck[3] ) { return false; }
#endif
			//すべてワイルドの場合true
			if ( AllTrue ( bWildLvr ) && AllTrue ( bWildBtn ) ) { return true; }

			//いずれかを返す場合
			bool ret = true;
#if false
			if ( bCheck_i ) { ret &= b_i; }
			if ( bCheck[0] ) { ret &= b_Btn[0]; }
			if ( bCheck[1] ) { ret &= b_Btn[1]; }
			if ( bCheck[2] ) { ret &= b_Btn[2]; }
			if ( bCheck[3] ) { ret &= b_Btn[3]; }
#endif
			//レバー
			ret &= Check ( LeverCommandNum, bWildLvr, b_Lvr );

			//ボタン
			ret &= Check ( _GameKey.BTN_NUM, bWildBtn, b_Btn );

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
				bool b;

				b = gk.Lvr [ (int)GK_L.DIR_1 ];
				gk.Lvr [ (int)GK_L.DIR_1 ] = gk.Lvr [ (int)GK_L.DIR_3 ];
				gk.Lvr [ (int)GK_L.DIR_3 ] = b;

				b = gk.Lvr [ (int)GK_L.DIR_4 ];
				gk.Lvr [ (int)GK_L.DIR_4 ] = gk.Lvr [ (int)GK_L.DIR_6 ];
				gk.Lvr [ (int)GK_L.DIR_6 ] = b;

				b = gk.Lvr [ (int)GK_L.DIR_7 ];
				gk.Lvr [ (int)GK_L.DIR_7 ] = gk.Lvr [ (int)GK_L.DIR_9 ];
				gk.Lvr [ (int)GK_L.DIR_9 ] = b;
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

