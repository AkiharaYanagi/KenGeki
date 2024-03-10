//=================================================================================================
//
//	Fighting デモState
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "FtgDemo_State.h"
#include <iomanip>	//std::setw(), std::setfill() など
#include "../GameMain/SoundConst.h"
#include "FtgDemo.h"


//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//=====================================================
	//デモ表示　グラフィック基準
	P_GrpDemo FtgDemoState::MakeGrpValue ( LPCTSTR txName )
	{
		P_GrpDemo pGrp = make_shared < GrpDemo > ();
		pGrp->AddTexture ( txName );

		pGrp->SetPos ( VEC2 ( 128, 400 ) );
		pGrp->SetScalingCenter ( VEC2 ( 512, 128 ) );
		pGrp->SetStartScaling ( VEC2 ( 1.3f, 1.3f ) );
		pGrp->SetSecondVel ( VEC2 ( -0.001f, -0.001f ) );
		pGrp->SetZ ( Z_SYS );	//@info SetZはGRPLST_INSERT()の後では行わない(リストが崩れる)
		pGrp->SetValid ( F );

		AddpTask ( pGrp );
		GRPLST_INSERT_MAIN ( pGrp );
		return pGrp;
	}
	//=====================================================


	//------------------------------------------------
	//開幕挨拶
	FTG_DM_Greeting::FTG_DM_Greeting ()
	{
		m_timer = make_shared < Timer > ();
		m_timer->SetTargetTime ( 90 );
	}

	void FTG_DM_Greeting::Start ()
	{
		m_timer->Start ();
	}

	void FTG_DM_Greeting::Do ()
	{
		m_timer->Move ();
		if ( m_timer->IsLast () )
		{
			GetwpFtgDemoActor ().lock ()->Change_Greeting_To_GetReady ();
		}
	}

	//------------------------------------------------
	//準備

	const UINT FTG_DM_GetReady::COUNT = 90;
	const UINT FTG_DM_GetReady::COUNT_D = 60 - COUNT % 60;

	FTG_DM_GetReady::FTG_DM_GetReady ()
	{
		m_grpGetReady = MakeGrpValue ( _T ( "Demo_GetReady.png" ) );
		m_grpGetReady->SetEnd ( COUNT );

#if 0
		m_grpClock = make_shared < GrpAcv > ();
		m_grpClock->SetPos ( VEC2 ( 640 - 256, 300 ) );
		m_grpClock->SetValid ( F );
		m_grpClock->SetZ ( Z_EFF );
		AddpTask ( m_grpClock );
		GRPLST_INSERT_MAIN ( m_grpClock );

		tostringstream toss;
		tstring filename_base = _T ( "clock\\clock_" );
		tstring ext = _T ( ".png" );

		for ( UINT i = 1; i < 61; ++ i )
		{
			toss << filename_base << std::setw ( 2 ) << std::setfill ( _T ( '0' ) ) << i << ext;
			m_grpClock->AddTexture ( toss.str () );
			toss.str ( _T ( "" ) );
		}
#endif // 0

		m_timer = make_shared < Timer > ();
	}

	void FTG_DM_GetReady::Start ()
	{
		GetpMutualChara ()->StartGetReady ();

		m_grpGetReady->Start ();
//		m_grpClock->SetValid ( T );
		m_timer->Start ();
	}

	void FTG_DM_GetReady::Final ()
	{
//		m_grpClock->SetValid ( F );
	}

	void FTG_DM_GetReady::Do ()
	{
		m_timer->Move ();
		UINT t = m_timer->GetTime ();
		UINT index = ( t + COUNT_D ) % 60;
//		m_grpClock->SetIndexTexture ( index );

		if ( ! m_grpGetReady->GetValid () )
		{
			GetwpFtgDemoActor ().lock ()->Change_GetReady_To_Attack ();
		}
	}

	//------------------------------------------------
	//開始
	FTG_DM_Attack::FTG_DM_Attack ()
	{
//		m_grpAttack = MakeGrpValue ( _T ( "Demo_Attack.png" ) );
		m_grpAttack = MakeGrpValue ( _T ( "Demo_Fight.png" ) );
		m_grpAttack->SetEnd ( 90 );
	}

	void FTG_DM_Attack::Start ()
	{
		m_grpAttack->Start ();
	}

	void FTG_DM_Attack::Do ()
	{
		m_grpAttack->Move ();
	}

	//------------------------------------------------
	//メイン
	void FTG_DM_Main::Start ()
	{
		GetpMutualChara ()->StartFighting ();
	}

	void FTG_DM_Main::Do ()
	{
		// 格闘終了判定
		if ( GetpMutualChara()->CheckZeroLife () )
		{
			//GetpMutualChara ()->Stop ( true );
			//GetpMutualChara ()->SetEndWait ();
			GetwpFtgDemoActor ().lock ()->Change_Main_To_Down ();
		}
	}

	//------------------------------------------------
	//特殊演出
	void FTG_DM_WallBreak::Start ()
	{
	}

	void FTG_DM_WallBreak::Do ()
	{
	}

	//------------------------------------------------
	//ダウン
	FTG_DM_Down::FTG_DM_Down ()
	{
		m_grpDown = MakeGrpValue ( _T ( "Demo_Down.png" ) );
		m_grpDown->SetEnd ( 120 );
	}

	void FTG_DM_Down::Start ()
	{
		m_grpDown->Start ();
	}

	void FTG_DM_Down::Do ()
	{
		if ( ! m_grpDown->GetValid () )
		{
			//新規開始
			GetpMutualChara ()->StartFighting ();
			GetwpFtgDemoActor ().lock ()->Change_Down_To_Greeting ();
#if 0
			//タイトルに戻る
			GetwpFtgDemoActor ().lock ()->End_Down_To_Result ();
#endif // 0
		}
	}


	//------------------------------------------------
	//勝者表示

	void FTG_DM_Winner::Do ()
	{
	}


}	//namespace GAME

