//=================================================================================================
//
//	BattleTime ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "BattleTime.h"
#include "FtgConst.h"	
#include "G_Ftg.h"	


//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	BattleTime::BattleTime ()
	{
		//------------------------------------------------
		//背景
		m_time_bg = make_shared < GrpAcv > ();
//		m_time_bg->AddTexture ( _T("time\\time_bg.png") );
		m_time_bg->AddTexture ( _T("time\\time_bg0.png") );
		m_time_bg->SetPos ( VEC2 ( (1280 / 2) - (256 / 2), 12 ) );
		m_time_bg->SetZ ( Z_BG - 0.01f );
		AddpTask ( m_time_bg );
		GRPLST_INSERT_MAIN ( m_time_bg );

		//数値
		m_battle_time = make_shared < GrpAcv > ();
		m_battle_time->AddTexture ( _T("time\\time_0.png") );
		m_battle_time->AddTexture ( _T("time\\time_1.png") );
		m_battle_time->AddTexture ( _T("time\\time_2.png") );
		m_battle_time->AddTexture ( _T("time\\time_3.png") );
		m_battle_time->AddTexture ( _T("time\\time_4.png") );
		m_battle_time->AddTexture ( _T("time\\time_5.png") );
		m_battle_time->AddTexture ( _T("time\\time_6.png") );
		m_battle_time->AddTexture ( _T("time\\time_7.png") );
		m_battle_time->AddTexture ( _T("time\\time_8.png") );
		m_battle_time->AddTexture ( _T("time\\time_9.png") );

		AddpTask ( m_battle_time );
		GRPLST_INSERT_MAIN ( m_battle_time );

		m_battle_time_01 = make_shared < GameObject > ();
		m_battle_time_02 = make_shared < GameObject > ();
		m_battle_time->ClearObject ();
		m_battle_time->AddpObject ( m_battle_time_01 );
		m_battle_time->AddpObject ( m_battle_time_02 );

		float bx = 16 + (1280 * 0.5f) - (128 / 2);
		float by = 50;
		m_battle_time_01->SetPos ( VEC2 ( bx + 34,	by ) );
		m_battle_time_02->SetPos ( VEC2 ( bx,		by ) );
	}

	BattleTime::~BattleTime ()
	{
	}

	void BattleTime::Init ()
	{
		m_time = 3660;

		TASK_LST::Init ();
	}

	void BattleTime::Move ()
	{
		//--------------------------
		// バトルタイム
		int second = m_time / 60;			//[F] to [Sec]
		int index_01 = second % 10;			//１桁目
		int index_02 = second / 10 % 10;	//2桁目

		m_battle_time_01->SetIndexTexture ( index_01 );
		m_battle_time_02->SetIndexTexture ( index_02 );


		//範囲
		if ( m_time <= 0 ) { m_time = 3660; }

		-- m_time;

		TASK_LST::Move ();
	}

}	//namespace GAME

