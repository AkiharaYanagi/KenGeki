//=================================================================================================
//
//	CharaSele Player ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "CharaSele_Player.h"


//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//枠位置定数
	const ChSlct CharaSele_Player::m_pos [ CHARA_SELE_NUM ] = 
	{
		{ CHSLID_0, 362,  17, CHSLID_7, CHSLID_3, CHSLID_2, CHSLID_1 }, 
		{ CHSLID_1, 512,  17, CHSLID_8, CHSLID_4, CHSLID_0, CHSLID_2 }, 
		{ CHSLID_2, 662,  17, CHSLID_9, CHSLID_4, CHSLID_1, CHSLID_0 }, 
		{ CHSLID_3, 437, 167, CHSLID_0, CHSLID_5, CHSLID_4, CHSLID_4 }, 
		{ CHSLID_4, 587, 167, CHSLID_1, CHSLID_6, CHSLID_3, CHSLID_3 }, 
		{ CHSLID_5, 437, 577, CHSLID_3, CHSLID_7, CHSLID_6, CHSLID_6 },  
		{ CHSLID_6, 587, 577, CHSLID_4, CHSLID_8, CHSLID_5, CHSLID_5 }, 
		{ CHSLID_7, 362, 727, CHSLID_5, CHSLID_0, CHSLID_9, CHSLID_8 }, 
		{ CHSLID_8, 512, 727, CHSLID_6, CHSLID_1, CHSLID_7, CHSLID_9 }, 
		{ CHSLID_9, 662, 727, CHSLID_6, CHSLID_2, CHSLID_8, CHSLID_7 }, 
	};


	CharaSele_Player::CharaSele_Player ()
	{
		//キャラ立絵
		m_chara_stand = make_shared < GrpAcv > ();
		m_chara_stand->AddTexture ( _T("Stand_Ouka.png") );
		m_chara_stand->AddTexture ( _T("0.png") );
		m_chara_stand->AddTexture ( _T("Stand_Sae.png") );
		AddpTask ( m_chara_stand );
		GRPLST_INSERT_MAIN ( m_chara_stand );

		//カーソル
		m_cursor = make_shared < GrpBlink > ();
		m_cursor->AddTexture ( _T ( "CharaSele_Cursor_1p.png" ) );
		m_cursor->AddTexture ( _T ( "CharaSele_Cursor_2p.png" ) );
		m_cursor->SetTimer ( 15 );
		AddpTask ( m_cursor );
		GRPLST_INSERT_MAIN ( m_cursor );

	}

	CharaSele_Player::~CharaSele_Player ()
	{
	}

	void CharaSele_Player::PlayerInit ( PLAYER_ID id )
	{
		m_player_id = id;

		if ( PLAYER_ID_1 == id )
		{
			m_chslct_id = CHSLID_0;
			m_chara_stand->SetPos ( CHARA_1P_POS_X, CHARA_1P_POS_Y );
			m_chara_stand->SetIndexTexture ( CHSLID_0 );
			m_cursor->SetPos ( SELECT_1P_POS_X, SELECT_1P_POS_Y );
			m_cursor->SetIndexTexture ( 0 );
		}
		else if ( PLAYER_ID_2 == id )
		{
			m_chslct_id = CHSLID_2;
			m_chara_stand->SetPos ( CHARA_2P_POS_X, CHARA_2P_POS_Y );
			m_chara_stand->SetIndexTexture ( CHSLID_2 );
			m_cursor->SetPos ( SELECT_2P_POS_X, SELECT_2P_POS_Y );
			m_cursor->SetIndexTexture ( 1 );
		}
	}

	void CharaSele_Player::Move ()
	{
		//入力
		if ( PLAYER_ID_1 == m_player_id )
		{
			if ( CFG_PUSH_KEY ( _P1_UP ) ) { SetCursorUp ( m_cursor, m_chslct_id ); }
			if ( CFG_PUSH_KEY ( _P1_DOWN ) ) { SetCursorDown ( m_cursor, m_chslct_id ); }
			if ( CFG_PUSH_KEY ( _P1_LEFT ) ) { SetCursorLeft ( m_cursor, m_chslct_id ); }
			if ( CFG_PUSH_KEY ( _P1_RIGHT ) ) { SetCursorRight ( m_cursor, m_chslct_id ); }
		}
		else if ( PLAYER_ID_2 == m_player_id )
		{
			if ( CFG_PUSH_KEY ( _P2_UP ) ) { SetCursorUp ( m_cursor, m_chslct_id ); }
			if ( CFG_PUSH_KEY ( _P2_DOWN ) ) { SetCursorDown ( m_cursor, m_chslct_id ); }
			if ( CFG_PUSH_KEY ( _P2_LEFT ) ) { SetCursorLeft ( m_cursor, m_chslct_id ); }
			if ( CFG_PUSH_KEY ( _P2_RIGHT ) ) { SetCursorRight ( m_cursor, m_chslct_id ); }
		}

		TASK_VEC::Move ();
	}



	void CharaSele_Player::SetCursorUp ( P_GrpBlink pCrs, CHARA_SELE_ID & id )
	{
		id = m_pos [ id ].UP;
		pCrs->SetPos ( VEC2 ( m_pos [ id ].x, m_pos [ id ].y ) );
	}
	void CharaSele_Player::SetCursorDown ( P_GrpBlink pCrs, CHARA_SELE_ID & id )
	{
		id = m_pos [ id ].DOWN;
		pCrs->SetPos ( VEC2 ( m_pos [ id ].x, m_pos [ id ].y ) );
	}
	void CharaSele_Player::SetCursorLeft ( P_GrpBlink pCrs, CHARA_SELE_ID & id )
	{
		id = m_pos [ id ].LEFT;
		pCrs->SetPos ( VEC2 ( m_pos [ id ].x, m_pos [ id ].y ) );
	}
	void CharaSele_Player::SetCursorRight ( P_GrpBlink pCrs, CHARA_SELE_ID & id )
	{
		id = m_pos [ id ].RIGHT;
		pCrs->SetPos ( VEC2 ( m_pos [ id ].x, m_pos [ id ].y ) );
	}


}	//namespace GAME

