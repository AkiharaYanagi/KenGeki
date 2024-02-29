//=================================================================================================
//
//	Fighting ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "Fighting.h"
#include "FtgConst.h"	


//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	Fighting::Fighting ()
	{
#if 0
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

		//------------------------------------------------
		//白背景
		m_bg_white = make_shared < PrmRect > ();
		m_bg_white->SetSize ( 1280, 960 );
		m_bg_white->SetAllColor ( 0xffffffff );
		m_bg_white->SetPos ( 0, 0 );
		m_bg_white->SetZ ( Z_BG );
		AddpTask ( m_bg_white );
		GRPLST_INSERT_MAIN ( m_bg_white );
		m_bg_white->SetValid ( F );

		//------------------------------------------------
		//test
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
		

		m_wall_L->SetValid ( F );
		m_wall_R->SetValid ( F );

#endif // 0
		m_bg = make_shared < BG > ();
		AddpTask ( m_bg );

		//------------------------------------------------
		//ゲージ枠
		m_gauge_frame = make_shared < GrpAcv > ();
		m_gauge_frame->AddTexture ( _T ( "gauge_frame.png" ) );
		m_gauge_frame->SetZ ( Z_SYS );
		AddpTask ( m_gauge_frame );
//		GRPLST_INSERT_MAIN ( m_gauge_frame );
		//------------------------------------------------

		//=====================================================
		//キャラ相互処理
		//=====================================================
		m_mutualChara = make_shared < MutualChara > ();
		AddpTask ( m_mutualChara );

		//=====================================================
		//デモ
		m_demoActor = make_shared < FtgDemoActor > ();		m_demoActor->Load ();
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
		//基準 初期化
		TASK_LST::Init ();

		//初期化後
	
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
		//デモ分岐
		m_demoActor->Do ();

		//--------------------------
		//両者処理


		//暗転以外のとき、通常処理
		if ( ! m_bg->IsBlackOut () )
		{
			m_mutualChara->Conduct ();
		}
		//暗転時は通常処理しない


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

	//共通グラフィック処理
	void Fighting::Grp ()
	{
		m_bg->Grp ();
#if 0
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
#endif // 0
	}


}	//namespace GAME

