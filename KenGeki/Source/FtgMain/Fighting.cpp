//=================================================================================================
//
//	Fighting ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "Fighting.h"

//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	Fighting::Fighting ()
	{
		//------------------------------------------------
		//背景
		m_bg = make_shared < GrpAcv > ();
//		m_bg->AddTexture ( _T ( "ftgmain_bg1.png" ) );
//		m_bg->AddTexture ( _T ( "test_bg.png" ) );
//		m_bg->AddTexture ( _T ( "BG0.png" ) );
		m_bg->AddTexture ( _T ( "training_bg.png" ) );
//		m_bg->AddTexture ( _T ( "training_bg_blackout.png" ) );
		m_bg->SetPos ( (float)BG_POS_X, (float)BG_POS_Y );
		m_bg->SetZ ( Z_BG );

		//オブジェクト追加
		m_bg->AddObject ();
		P_Object pOb = m_bg->GetpvpObject ()->at ( 1 );
		pOb->SetPos ( 200, 0 );

		AddpTask ( m_bg );
		GRPLST_INSERT_MAIN ( m_bg );

		m_bg_blackout = make_shared < GrpAcv > ();
//		m_bg_blackout->AddTexture ( _T ( "ftgmain_bg_blackout.png" ) );
//		m_bg_blackout->AddTexture ( _T ( "ftgmain_bg1.png" ) );
		m_bg_blackout->AddTexture ( _T ( "training_bg_blackout.png" ) );
		m_bg_blackout->SetPos ( (float)BG_POS_X, (float)BG_POS_Y );
		m_bg_blackout->SetZ ( Z_BG );
		m_bg_blackout->SetValid ( false );
		AddpTask ( m_bg_blackout );
		GRPLST_INSERT_MAIN ( m_bg_blackout );

		m_wall_L = make_shared < GrpAcv > ();
		m_wall_L->AddTexture ( _T ( "test_kabe.png" ) );
		m_wall_L->SetPos ( 0, 0 );
		m_wall_L->SetZ ( Z_BG );
		AddpTask ( m_wall_L );
		GRPLST_INSERT_MAIN ( m_wall_L );

		m_wall_R = make_shared < GrpAcv > ();
		m_wall_R->AddTexture ( _T ( "test_kabe.png" ) );
		m_wall_R->SetScaling ( -1.f, 1.f );
		m_wall_R->SetPos ( GAME_WIDTH - 512, 0 );
		m_wall_R->SetZ ( Z_BG );
		AddpTask ( m_wall_R );
		GRPLST_INSERT_MAIN ( m_wall_R );


		//test
		m_wall_L->SetValid ( F );
		m_wall_R->SetValid ( F );



		//------------------------------------------------
		//ゲージ枠
		m_gauge_frame = make_shared < GrpAcv > ();
		m_gauge_frame->AddTexture ( _T ( "gauge_frame.png" ) );
		m_gauge_frame->SetZ ( Z_SYS );
		AddpTask ( m_gauge_frame );
//		GRPLST_INSERT_MAIN ( m_gauge_frame );

		//BGタイマ
		m_tmrBlackOut = make_shared < Timer > ();
		AddpTask ( m_tmrBlackOut );

		//------------------------------------------------

		//=====================================================
		//キャラ相互処理
		//=====================================================
		m_mutualChara = make_shared < MutualChara > ();
		AddpTask ( m_mutualChara );

		//=====================================================
		//デモ
		m_demoActor = make_shared < FtgDemoActor > ();
//		m_demoActor->Load ();
		m_demoActor->SetpMutualChara ( m_mutualChara );
		AddpTask ( m_demoActor );

		//Debug用　開始デモをスキップ切替
#define DEMO_ON 0
#if DEMO_ON
		m_demoSkip = F;
#else
		m_demoSkip = T;
#endif // DEMO_ON

		//=====================================================

		//ポーズ
		m_pause = make_shared < GrpAcv > ();
		m_pause->AddTexture ( _T ( "Pause.png" ) );
		m_pause->SetPos ( (1280-512)*0.5, 480.f );
		m_pause->SetSpritePosition ( VEC3 ( 0, 0, Z_SYS ) );
		AddpTask ( m_pause );
		GRPLST_INSERT_MAIN ( m_pause );
		m_pause->SetValid ( false );
	}

	Fighting::~Fighting ()
	{
	}


	void Fighting::ParamInit ( P_Param pParam )
	{
		m_mutualChara->ParamInit ( pParam );
	}

	void Fighting::Init ()
	{
		m_pause->SetValid ( F );

		TASK_LST::Init ();

		//デモスキップ
		if ( m_demoSkip )
		{
			m_demoActor->StartFighting ();
		}
		else
		{
			m_demoActor->StartGreeting ();
		}
	}


	void Fighting::Move ()
	{
		//--------------------------
		//ポーズ
		Pause ();

		//--------------------------
		//暗転
		if ( m_tmrBlackOut->IsActive () )	//稼働時
		{
			//終了
			if ( m_tmrBlackOut->IsLast () )
			{
				m_tmrBlackOut->Clear ();

				m_bg->SetValid ( T );
				m_bg_blackout->SetValid ( F );
			}
		}
		//--------------------------
		//デモ分岐
		m_demoActor->Do ();

		//--------------------------
		//両者処理

		//暗転時は処理しない
		if ( ! m_tmrBlackOut->IsActive () )
		{
			m_mutualChara->Conduct ();
		}

		//--------------------------
		//共通グラフィック処理
		Grp ();

		//--------------------------
		TASK_LST::Move ();
	}

	void Fighting::TrainingRestart ()
	{
		m_demoActor->StartFighting ();
		m_mutualChara->TrainingInit ();
	}

	

	//=============================================================
	//	内部関数
	//=============================================================
	void Fighting::Pause ()
	{
		//F1でポーズ切替
		if ( WND_UTL::AscKey ( VK_F1 ) )
		{
			if ( m_pause->GetValid () )	//On->Off
			{
				m_pause->SetValid ( false );
//				m_mutualChara->Stop ( false );
			}
			else	//Off->On
			{
				m_pause->SetValid ( true );
//				m_mutualChara->Stop ( true );
			}
		}
	}

	//共通グラフィック処理
	void Fighting::Grp ()
	{
		//-------------------------------------------------------
		//暗転
		if ( ! m_tmrBlackOut->IsActive () )
		{
			UINT blackOut = m_mutualChara->GetBlackOut ();
			//初回
			if ( 0 < blackOut )
			{
				m_tmrBlackOut->SetTargetTime ( blackOut );
				m_tmrBlackOut->Start ();

				blackOut = 0;
				m_mutualChara->SetBlackOut ( 0 );

				m_bg->SetValid ( F );
				m_bg_blackout->SetValid ( T );
			}
		}
		//-------------------------------------------------------

		//背景位置補正
		int nx = (int) G_BASE_POS ().x;
		int disp_bg_x = nx % GAME_WIDTH;
		m_bg->SetPos ( (float)disp_bg_x, (float)BG_POS_Y );

		//サブ背景位置
		int lx = disp_bg_x - GAME_WIDTH;
		int rx = disp_bg_x + GAME_WIDTH;
		const int D = GAME_WINDOW_WIDTH - GAME_WIDTH;	//2048 - 1280 = 768
		int sub_x = ( - disp_bg_x < 0 )? lx : rx ;
		P_Object pOb = m_bg->GetpObject ( 1 );
		pOb->SetPos ( (float)sub_x, (float)BG_POS_Y );


		//カベ位置
		float wall_l = G_FTG->GetWallLeft ();
		m_wall_L->SetPos ( wall_l + G_BASE_POS ().x, 0 );

		float wall_r = G_FTG->GetWallRight ();
		m_wall_R->SetPos ( wall_r + G_BASE_POS ().x, 0 );
	}


}	//namespace GAME

