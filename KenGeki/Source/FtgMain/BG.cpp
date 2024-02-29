//=================================================================================================
//
//	BG ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "BG.h"
#include "FtgConst.h"	


//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	BG::BG ()
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


		//------------------------------------------------
		//BGタイマ
		//暗転
		m_tmrBlackOut = make_shared < Timer > ();
		AddpTask ( m_tmrBlackOut );
		//白転
		m_tmrWhiteOut = make_shared < Timer > ();
		AddpTask ( m_tmrWhiteOut );

		//------------------------------------------------
	}

	BG::~BG ()
	{
	}


	void BG::Init ()
	{
		TASK_LST::Init ();
	}


	void BG::Move ()
	{
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

		//白転
		if ( m_tmrWhiteOut->IsActive () )	//稼働時
		{
			//終了
			if ( m_tmrWhiteOut->IsLast () )
			{
				m_tmrWhiteOut->Clear ();

				m_bg_white->SetValid ( F );
			}
		}

		//--------------------------
		//共通グラフィック処理
		Grp ();

		//--------------------------
		TASK_LST::Move ();
	}

	//=============================================================
	//	内部関数
	//=============================================================
	//共通グラフィック処理
	void BG::Grp ()
	{
		//-------------------------------------------------------
		//暗転
		if ( ! m_tmrBlackOut->IsActive () )
		{
			UINT blackOut = m_mutualChara->GetBlackOut ();
			//初回
			if ( 0 < blackOut )
			{
				m_tmrBlackOut->Start ( blackOut );

				blackOut = 0;
				m_mutualChara->SetBlackOut ( 0 );

				m_bg->SetValid ( F );
				m_bg_blackout->SetValid ( T );
			}
		}

		//-------------------------------------------------------
		//白転
		if ( ! m_tmrWhiteOut->IsActive () )
		{
			//初回
			if ( m_mutualChara->CheckWhiteOut () )
			{
				m_tmrWhiteOut->Start ( 30 );

				m_bg_white->SetValid ( T );
				m_mutualChara->SetWhiteOut ( F );
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

