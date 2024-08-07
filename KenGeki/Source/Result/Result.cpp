//=================================================================================================
//
//	Result ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "Result.h"
#include "../GameMain/SoundConst.h"
#include "../Title/Title.h"

//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	const float Result::BX_TX = 10;
	const float Result::BX_1P = 300;
	const float Result::BX_2P = 900;
	const float Result::BY = 200;
	const float Result::PY = 40;
	const float Result::CH_DX = 300;
	const float Result::CH_X_1P = - CH_DX;
	const float Result::CH_X_2P = 1280 - 1024 + CH_DX;
	const float Result::CH_Y = 280;


	Result::Result ()
	{
		//-----------------------------------------------------
		//背景
		m_bg = make_shared < GrpAcv > ();
		m_bg->AddTexture ( _T ( "Result.png" ) );
		AddpTask ( m_bg );
		GRPLST_INSERT_MAIN ( m_bg );

		//-----------------------------------------------------
		//終了タイマ
		m_timer = make_shared < Timer >( 180 );
		AddpTask( m_timer );

		//-----------------------------------------------------
		//キャラ
		m_chara = make_shared < GrpAcv > ();
		m_chara->AddTexture ( _T ( "Stand_Sae.png" ) );
		m_chara->AddTexture ( _T ( "Stand_Sae.png" ) );
		
		m_chara->AddObject ();	//オブジェクト２個め
		m_chara->GetpObject ( 0 )->SetPos ( VEC2 ( CH_X_1P, CH_Y ) );
		m_chara->GetpObject ( 1 )->SetPos ( VEC2 ( CH_X_2P, CH_Y ) );

		AddpTask ( m_chara );
		GRPLST_INSERT_MAIN ( m_chara );


#if 0
		//-----------------------------------------------------
		m_affinity = MakeStr ( _T ( "Affinity" ), VEC2 ( 550, 50 ) );
		m_rank = make_shared < Rank > ();
		AddpTask ( m_rank );

		//-----------------------------------------------------

		//見出
		const float y_pl = BY + PY * 0;
		m_str1p = MakeStr ( _T ( "1p" ), VEC2 ( BX_1P + 50, y_pl ) );
		m_str2p = MakeStr ( _T ( "2p" ), VEC2 ( BX_2P + 50, y_pl ) );

		//残ライフ
		const float y_lf = BY + PY * 2;
		m_strNLife = MakeStr ( _T ( "残ライフ：" ), VEC2 ( BX_TX, y_lf ) );
		m_strNLife1p = MakeStr ( _T ( "1p" ), VEC2 ( BX_1P, y_lf ) );
		m_strNLife2p = MakeStr ( _T ( "2p" ), VEC2 ( BX_2P, y_lf ) );

		//アクション移行回数
		const float y_ac_tx = BY + PY * 3;
		const float y_ac = BY + PY * 4;
		m_strNAct = MakeStr ( _T ( "アクション移行回数：" ), VEC2 ( BX_TX, y_ac_tx ) );
		m_strNAct1p = MakeStr ( _T ( "1p" ), VEC2 ( BX_1P, y_ac ) );
		m_strNAct2p = MakeStr ( _T ( "2p" ), VEC2 ( BX_2P, y_ac ) );
#endif // 0

		//勝敗
		const float y_wl = BY + PY * 1;
		m_win_lose = MakeStr ( _T ( "勝敗" ), VEC2 ( BX_TX, y_wl ) );
		m_win_lose_1p = MakeStr ( _T ( "win_lose_1p" ), VEC2 ( BX_1P, y_wl ) );
		m_win_lose_2p = MakeStr ( _T ( "win_lose_2p" ), VEC2 ( BX_2P, y_wl ) );
	}

	Result::~Result ()
	{
	}

	P_GrpStr Result::MakeStr ( LPCTSTR str, VEC2 pos )
	{
		P_GrpStr p = make_shared < GrpStr > ();
		p->SetStr ( str );
		p->SetPos ( pos );
		AddpTask ( p );
		GRPLST_INSERT_MAIN ( p );
		return p;
	}

	//シーン共通パラメータから取得して初期化
	void Result::ParamInit ()
	{
#if 0

		//残ライフ(全 10,000 )
		int nLife1 = GetpParam ()->GetN_Life1p ();
//		nLife1 = 3980;
//		nLife1 = 1980;
		m_strNLife1p->SetStrF ( _T ( "%d" ), nLife1 );
		int nLife2 = GetpParam ()->GetN_Life2p ();
//		nLife2 = 0;
		m_strNLife2p->SetStrF ( _T ( "%d" ), nLife2 );
		int dLife = abs ( nLife1 - nLife2 );
		int rank = dLife * 7 / 10000;
		m_rank->SelectRank ( ( Rank::RANK ) rank );


		//行動回数
		int nAct1 = GetpParam ()->GetN_Act1p ();
		m_strNAct1p->SetStrF ( _T ( "%d" ), nAct1 );
		int nAct2 = GetpParam ()->GetN_Act2p ();
		m_strNAct2p->SetStrF ( _T ( "%d" ), nAct2 );

#endif // 0

		//勝敗
		PLAYER_ID winner = GetpParam ()->GetWinner ();
//		PLAYER_ID winner = PLAYER_ID_2;
		switch ( winner )
		{
		case PLAYER_ID_1:
			m_win_lose_1p->SetStr ( _T ( "Win" ) );
			m_win_lose_2p->SetStr ( _T ( "Lose" ) );
			m_chara->GetpObject ( 0 )->SetIndexTexture ( 0 );
			m_chara->GetpObject ( 1 )->SetIndexTexture ( 1 );
			break;
		case PLAYER_ID_2:
			m_win_lose_1p->SetStr ( _T ( "Lose" ) );
			m_win_lose_2p->SetStr ( _T ( "Win" ) );
			m_chara->GetpObject ( 0 )->SetIndexTexture ( 1 );
			m_chara->GetpObject ( 1 )->SetIndexTexture ( 0 );
			break;
		case _PLAYER_NUM:
			m_win_lose_1p->SetStr ( _T ( "Draw" ) );
			m_win_lose_2p->SetStr ( _T ( "Draw" ) );
			m_chara->GetpObject ( 0 )->SetIndexTexture ( 0 );
			m_chara->GetpObject ( 1 )->SetIndexTexture ( 0 );
			break;
		}

	}

	void Result::Load ()
	{
		//遷移先指定にthisを保存
		Scene::SetwpThis ( shared_from_this () );

#if 0
		//SOUND
		SOUND->Play_BGM ( BGM_Result );

		//終了タイマ
		m_timer->Start();
#endif // 0

		Scene::Load ();
	}

	void Result::Init ()
	{
		Scene::Init ();
	}

	void Result::Move ()
	{
		Scene::Move ();
	}

	P_GameScene Result::Transit ()
	{
		//===========================================================
		//戻る (P1, P2, ボタン0, ボタン1 受付)
		bool p12_b0 = CFG_PUSH_KEY ( _P1_BTN0 ) || CFG_PUSH_KEY ( _P2_BTN0 );
		bool p12_b1 = CFG_PUSH_KEY ( _P1_BTN1 ) || CFG_PUSH_KEY ( _P2_BTN1 );
		if ( p12_b0 || p12_b1 )
		{
			//SOUND
//			SOUND->Stop_BGM ( BGM_Result );

			//タイトルに戻る
			Scene::Transit_Title ();
		}
		//===========================================================

		//自動終了
		if (m_timer->IsLast())
		{
			//SOUND
//			SOUND->Stop_BGM( BGM_Result );

			//タイトルに戻る
			Scene::Transit_Title();
		}
		//===========================================================


		return Scene::Transit ();
	}


}	//namespace GAME

