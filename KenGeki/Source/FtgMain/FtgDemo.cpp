//=================================================================================================
//
//	Fighting デモ
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "FtgDemo.h"
#include <iomanip>	//std::setw(), std::setfill() など
#include "../GameMain/SoundConst.h"


//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//=====================================================
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

	//-------------
	const UINT FTG_DM_GetReady::COUNT = 90;
	const UINT FTG_DM_GetReady::COUNT_D = 60 - COUNT % 60;

	FTG_DM_GetReady::FTG_DM_GetReady ()
	{
		m_grpGetReady = MakeGrpValue ( _T ( "Demo_GetReady.png" ) );
		m_grpGetReady->SetEnd ( COUNT );

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

		m_timer = make_shared < Timer > ();
	}

	void FTG_DM_GetReady::Start ()
	{
		GetpMutualChara ()->StartGetReady ();

		m_grpGetReady->Start ();
		m_grpClock->SetValid ( T );
		m_timer->Start ();
	}

	void FTG_DM_GetReady::Final ()
	{
		m_grpClock->SetValid ( F );
	}

	void FTG_DM_GetReady::Do ()
	{
		m_timer->Move ();
		UINT t = m_timer->GetTime ();
		UINT index = ( t + COUNT_D ) % 60;
		m_grpClock->SetIndexTexture ( index );

		if ( ! m_grpGetReady->GetValid () )
		{
			GetwpFtgDemoActor ().lock ()->Change_GetReady_To_Attack ();
		}
	}

	//-------------

	FTG_DM_Attack::FTG_DM_Attack ()
	{
		m_grpAttack = MakeGrpValue ( _T ( "Demo_Attack.png" ) );
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

	//-------------
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

	//-------------

	FTG_DM_Down::FTG_DM_Down ()
	{
		m_grpDown = MakeGrpValue ( _T ( "Demo_Down.png" ) );
		m_grpDown->SetEnd ( 90 );
	}

	void FTG_DM_Down::Start ()
	{
		m_grpDown->Start ();
	}

	void FTG_DM_Down::Do ()
	{
		if ( ! m_grpDown->GetValid () )
		{
#if 0
			//新規開始
			GetpMutualChara ()->StartFighting ();
			GetwpFtgDemoActor ().lock ()->Change_Down_To_Greeting ();
#endif // 0
			//タイトルに戻る
			GetwpFtgDemoActor ().lock ()->End_Down_To_Result ();
		}
	}


	//-------------

	void FTG_DM_Winner::Do ()
	{
	}

	//=====================================================
	FtgDemoActor::FtgDemoActor ()
		: m_bEnd ( F )
	{
		//ステート
		m_Greeting = make_shared < FTG_DM_Greeting > ();
		m_GetReady = make_shared < FTG_DM_GetReady > ();
		m_Attack = make_shared < FTG_DM_Attack > ();
		m_Main = make_shared < FTG_DM_Main > ();
		m_Down = make_shared < FTG_DM_Down > ();
		m_Winner = make_shared < FTG_DM_Winner > ();

		mvp_FtgDemo.push_back ( m_Greeting );
		mvp_FtgDemo.push_back ( m_GetReady );
		mvp_FtgDemo.push_back ( m_Attack );
		mvp_FtgDemo.push_back ( m_Main );
		mvp_FtgDemo.push_back ( m_Down );
		mvp_FtgDemo.push_back ( m_Winner );

		for ( P_FtgDemo p : mvp_FtgDemo )
		{
			AddpTask ( p );
		}

		//初期ステート
		mp_FtgDemo = m_Greeting;

		//パラメータ
		mp_Param = make_shared < FtgDemoParam > ();
		for ( P_FtgDemo p : mvp_FtgDemo )
		{
			p->SetpPrm ( mp_Param );
		}
	}

	void FtgDemoActor::Load ()
	{
		mp_Param->SetwpFtgDemoActor ( shared_from_this () );
		TASK_VEC::Load ();
	}

	void FtgDemoActor::StartGreeting ()
	{
		mp_FtgDemo = m_Greeting;
		GetpMutualChara ()->StartGreeting ();
		m_Greeting->Start ();
	}

	void FtgDemoActor::StartFighting ()
	{
		mp_FtgDemo = m_Main;
		m_Main->Start ();
	}

	void FtgDemoActor::Do ()
	{
		mp_FtgDemo->Do ();
	}

	void FtgDemoActor::Change_Greeting_To_GetReady ()
	{
		m_GetReady->Start ();
		mp_FtgDemo = m_GetReady;

		//BGM
		SOUND->Play_Loop_BGM ( BGM_Main );
	}

	void FtgDemoActor::Change_GetReady_To_Attack ()
	{
		m_GetReady->Final ();
		m_Attack->Start ();

		mp_FtgDemo = m_Main;
		m_Main->Start ();
	}

	void FtgDemoActor::Change_Main_To_Down ()
	{
		m_Down->Start ();
		mp_FtgDemo = m_Down;
	}

	void FtgDemoActor::Change_Down_To_Greeting ()
	{
		GetpMutualChara ()->StartGreeting ();
		m_Greeting->Start ();
		mp_FtgDemo = m_Greeting;
	}

	void FtgDemoActor::End_Down_To_Result ()
	{
		m_bEnd = T;
	}






	//デモ分岐
#if 0

	switch ( G_FTG_STATE () )
	{
	case FS_GETREADY:
		if ( ! m_demo_GetReady->GetValid () )
		{
			m_demo_Attack->SetFadeOut ( 60 );
			m_demo_Attack->Init ();
			m_mutualChara->Wait ( false );
			m_mutualChara->SetMain ();
			G_FTG_STATE_SET ( FS_GAME_MAIN );
		}
		break;

	case FS_ATTACK:
		break;

	case FS_GAME_MAIN:
		// 格闘終了判定
		if ( m_mutualChara->CheckDown () )
		{
			StartGrpDemo ( m_demo_Down, 120 );
			m_mutualChara->Stop ( true );
			m_mutualChara->SetEndWait ();
			G_FTG_STATE_SET ( FS_DOWN_DISP );
		}

		break;

	case FS_DOWN_DISP:
		if ( ! m_demo_Down->GetValid () )
		{
			m_mutualChara->Stop ( false );
			m_mutualChara->CheckWinner ();

			//@info 終了時に挙動が不安定なため飛ばす
//				G_FTG_STATE_SET ( FS_DOWN_WAIT );
			StartGrpDemo ( m_demo_Winner, 180 );
			G_FTG_STATE_SET ( FS_POST_DEMO_START );
		}
		break;

	case FS_DOWN_WAIT:
		if ( m_mutualChara->CheckDownEnd () )
		{
			//				m_mutualChara->EndAct ();
			G_FTG_STATE_SET ( FS_POST_DEMO_START );
		}
		break;

	case FS_POST_DEMO_START:
		if ( m_mutualChara->CheckWinEnd () )
		{
			StartGrpDemo ( m_demo_Winner, 180 );
#if 0
			switch ( m_mutualChara->GetWinnerName () )
			{
			case CHARA_SONIA: StartGrpDemo ( m_demo_SONIA, 180 ); break;
			case CHARA_ORFLOAT: StartGrpDemo ( m_demo_ORFLOAT, 180 ); break;
			default: break;
			}
#endif // 0
			G_FTG_STATE_SET ( FS_WINNER );
			m_mutualChara->ForcedEnd ();
		}
		break;

	case FS_WINNER:
		if ( ! m_demo_Winner->GetValid () )
		{
			//G_FTG_STATE_SET ( FS_END );

			//test 初期化
			G_FTG_STATE_SET ( FS_GETREADY );
			m_mutualChara->Start ();
		}
		break;

	case FS_END:

		//FS_END時にはTransit()が分岐している

		break;

	default: break;
	}

#endif // 0

	//デモ関数
#if 0
	void Fighting::MakeGrpDemo ( P_GrpDemo & pGrp, LPCTSTR txName )
	{
		pGrp = make_shared < GrpDemo > ();
		pGrp->AddTexture ( txName );
		SetGrpDemo ( pGrp );
		GRPLST_INSERT_MAIN ( pGrp );
	}

	void Fighting::SetGrpDemo ( P_GrpDemo pGrp )
	{
		pGrp->SetPos ( VEC2 ( 128, 400 ) );
		pGrp->SetScalingCenter ( VEC2 ( 512, 128 ) );
		pGrp->SetStartScaling ( VEC2 ( 1.3f, 1.3f ) );
		pGrp->SetSecondVel ( VEC2 ( -0.001f, -0.001f ) );
		pGrp->SetZ ( Z_SYS );
		pGrp->SetValid ( false );
	}

	void Fighting::StartGrpDemo ( P_GrpDemo pGrp, UINT time )
	{
		//		pGrp->SetWait ( time );
		pGrp->Init ();
	}
#endif // 0


}	//namespace GAME

