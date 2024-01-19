//=================================================================================================
//
// CharaInput ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "CharaInput.h"

//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//static実体
	const UINT CharaInput::m_vGameKeyNum = 60;		//キー入力の保存フレーム数

	//コンストラクタ
	CharaInput::CharaInput ()
		: m_playerID ( PLAYER_ID_1 )
	{
		for ( UINT i = 0; i < m_vGameKeyNum; ++i )
		{
			_GameKey gameKey;
			m_vGameKey.push_back ( gameKey );
		}
	}

	//デストラクタ
	CharaInput::~CharaInput ()
	{
	}

	//更新
	void CharaInput::Update ( bool dirRight )
	{
		//今回の入力をゲームキーに直して保存
		_GameKey gameKey;

		//上下前後
		bool bKey8 = false;	//上
		bool bKey2 = false;	//下
		bool bKey4 = false;	//後(右向き時)
		bool bKey6 = false;	//前(右向き時)

		if ( PLAYER_ID_1 == m_playerID )
		{
			bKey8 = CFG_IS_KEY ( _P1_UP );
			bKey2 = CFG_IS_KEY ( _P1_DOWN );
			if ( dirRight )	//右向き時
			{
				bKey4 = CFG_IS_KEY ( _P1_LEFT );
				bKey6 = CFG_IS_KEY ( _P1_RIGHT );
			}
			else	//左向き時
			{
				bKey4 = CFG_IS_KEY ( _P1_RIGHT );
				bKey6 = CFG_IS_KEY ( _P1_LEFT );
			}
		}
		else if ( PLAYER_ID_2 == m_playerID )
		{
			bKey8 = CFG_IS_KEY ( _P2_UP );
			bKey2 = CFG_IS_KEY ( _P2_DOWN );
			if ( dirRight )	//右向き時
			{
				bKey4 = CFG_IS_KEY ( _P2_LEFT );
				bKey6 = CFG_IS_KEY ( _P2_RIGHT );
			}
			else	//左向き時
			{
				bKey4 = CFG_IS_KEY ( _P2_RIGHT );
				bKey6 = CFG_IS_KEY ( _P2_LEFT );
			}
		}

		//3つ以上同時押しは優先順で処理

		// コマンド指定では12369874順

		//斜め優先
		if ( bKey4 && bKey2 )	{ gameKey.SetLvr ( _GameKey::LVR_1, true ); }
		if ( bKey2 )			{ gameKey.SetLvr ( _GameKey::LVR_2, true ); }
		if ( bKey6 && bKey2 )	{ gameKey.SetLvr ( _GameKey::LVR_3, true ); }
		if ( bKey6 )			{ gameKey.SetLvr ( _GameKey::LVR_6, true ); }
		if ( bKey8 && bKey6 )	{ gameKey.SetLvr ( _GameKey::LVR_9, true ); }
		if ( bKey8 )			{ gameKey.SetLvr ( _GameKey::LVR_8, true ); }
		if ( bKey8 && bKey4 )	{ gameKey.SetLvr ( _GameKey::LVR_7, true ); }
		if ( bKey4 )			{ gameKey.SetLvr ( _GameKey::LVR_4, true ); }


		//==============================================================================
		//ボタン
		bool bButton0 = false; bool bButton1 = false; bool bButton2 = false; bool bButton3 = false;
		bool bButton4 = false; bool bButton5 = false; bool bButton6 = false; bool bButton7 = false;

		if ( PLAYER_ID_1 == m_playerID )
		{
			bButton0 = CFG_IS_KEY ( _P1_BTN0 );
			bButton1 = CFG_IS_KEY ( _P1_BTN1 );
			bButton2 = CFG_IS_KEY ( _P1_BTN2 );
			bButton3 = CFG_IS_KEY ( _P1_BTN3 );
			bButton4 = CFG_IS_KEY ( _P1_BTN4 );
			bButton5 = CFG_IS_KEY ( _P1_BTN5 );
			bButton6 = CFG_IS_KEY ( _P1_BTN6 );
			bButton7 = CFG_IS_KEY ( _P1_BTN7 );
		}
		else if ( PLAYER_ID_2 == m_playerID )
		{
			bButton0 = CFG_IS_KEY ( _P2_BTN0 );
			bButton1 = CFG_IS_KEY ( _P2_BTN1 );
			bButton2 = CFG_IS_KEY ( _P2_BTN2 );
			bButton3 = CFG_IS_KEY ( _P2_BTN3 );
			bButton4 = CFG_IS_KEY ( _P2_BTN4 );
			bButton5 = CFG_IS_KEY ( _P2_BTN5 );
			bButton6 = CFG_IS_KEY ( _P2_BTN6 );
			bButton7 = CFG_IS_KEY ( _P2_BTN7 );
		}


		gameKey.SetBtn ( 0, bButton0 );
		gameKey.SetBtn ( 1, bButton1 );
		gameKey.SetBtn ( 2, bButton2 );
		gameKey.SetBtn ( 3, bButton3 );
		gameKey.SetBtn ( 4, bButton4 );
		gameKey.SetBtn ( 5, bButton5 );
		gameKey.SetBtn ( 6, bButton6 );
		gameKey.SetBtn ( 7, bButton7 );


		//前回のキーを今回にも保存する
		gameKey.ReservePrevious ( m_vGameKey[0] );


		//ゲーム入力を更新しながら現在フレーム分を保存
		for ( int i = m_vGameKeyNum - 1; i >= 1; -- i )
		{
			m_vGameKey[i] = m_vGameKey[i - 1];
		}
		m_vGameKey[0] = gameKey;
	}


	//------------------------------------------------------------------------------------
	//ルートリストをチェックして各種ブランチのコマンドが達成されていたら
	//遷移先のアクションIDを返す
	//戻値：enum { NO_COMPLETE (0xFFFFFFFF) } 不成立
	UINT CharaInput::GetTransitID ( Chara & ch, P_Script pScp, bool dirRight )
	{
		//キャラの持つルート,ブランチ,コマンドの参照
		const VP_Route vpRoute = ch.GetvpRoute ();
		const VP_Branch vpBranch = ch.GetvpBranch ();
		const VP_Command vpCommand = ch.GetvpCommand ();
		
		//スクリプトの持つルートリスト
		for ( UINT indexRoute : pScp->GetcvRouteID () )
		{
			const V_UINT vBranchID = vpRoute[indexRoute]->GetcvIDBranch ();

			//対象のブランチリスト
			for ( UINT indexBranch : vBranchID )
			{
				//コマンド分岐以外は飛ばす
				if ( BRC_CMD != vpBranch[indexBranch]->GetCondition () ) { continue; }

				//コマンド取得
				UINT indexCommand = vpBranch[indexBranch]->GetIndexCommand ();
				P_Command pCmd = vpCommand[indexCommand];

				//対象コマンドが成立していたら
				if ( pCmd->Compare ( m_vGameKey, dirRight ) )
				{
					//遷移先アクションIDを返す
					return vpBranch[indexBranch]->GetIndexSequence ();
				}
			}
		}
		//不成立のとき
		return NO_COMPLETE;
	}

	
	//成立リストを生成する
	void CharaInput::MakeTransitIDList ( Chara & ch, P_Script pScp, bool dirRight )
	{
		//成立した１つのIDではなく、成立したIDを優先順位で保存したリストを返す
		m_vCompID.clear ();

		//キャラの持つルート,ブランチ,コマンドの参照
		const VP_Route vpRoute = ch.GetvpRoute ();
		const VP_Branch vpBranch = ch.GetvpBranch ();
		const VP_Command vpCommand = ch.GetvpCommand ();

		//スクリプトの持つルートリスト
		for ( UINT indexRoute : pScp->GetcvRouteID () )
		{
			//ルートの取得
			P_Route pRut = vpRoute [ indexRoute ];
			const V_UINT vBranchID = vpRoute [ indexRoute ]->GetcvIDBranch ();

			//対象のブランチリスト
			for ( UINT indexBranch : vBranchID )
			{
				//ブランチの取得
				P_Branch pBrc = vpBranch [ indexBranch ];

				//コマンド分岐以外は飛ばす
				if ( BRC_CMD != pBrc->GetCondition () ) { continue; }

				//コマンドの取得
				UINT indexCommand = vpBranch [ indexBranch ]->GetIndexCommand ();
				P_Command pCmd = vpCommand [ indexCommand ];

				//対象コマンドが成立していたら
				if ( pCmd->Compare ( m_vGameKey, dirRight ) )
				{
					//遷移先アクションIDを登録する
					UINT id = vpBranch [ indexBranch ]->GetIndexSequence ();
					m_vCompID.push_back ( id );
				}
			}
		}
	}

	//優先リストの先頭を取得する
	UINT CharaInput::GetCompID ()
	{
		if ( m_vCompID.size() > 0 )
		{
			return m_vCompID [ 0 ];
		}
		return NO_COMPLETE;
	}

	void CharaInput::SetGameKey ( V_GAME_KEY & vKey )
	{
		UINT size = m_vGameKey.size ();
		for ( UINT i = 0; i < size; ++ i )
		{
			m_vGameKey [ i ] = vKey [ i ];
		}
	}


}	//namespace GAME

