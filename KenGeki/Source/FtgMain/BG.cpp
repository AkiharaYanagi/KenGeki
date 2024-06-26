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
#include "G_Ftg.h"	


//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{

	const float BG::BG_SIZE_W = 2048;
	const float BG::BG_SIZE_H = 1024;
	const float BG::BG_POS_X = 0 - ( 2048 - 1280 ) / 2;
	const float BG::BG_POS_Y = 0 - ( 1024 - 960 );
	const float BG::BG_POS_MIDDLE_Y = 380;
	const float BG::BG_POS_FRONT_Y = 960 - 256;


	BG::BG ()
	{
		//------------------------------------------------
		//背景
		m_bg = make_shared < GrpAcv > ( _T ( "BG_evening_back_1.png" ) );
		m_bg->SetPos ( BG_POS_X, BG_POS_Y );
		m_bg->SetZ ( Z_BG );

		//オブジェクト追加
		m_bg->AddObject ();
		P_Object pOb = m_bg->GetpvpObject ()->at ( 1 );
		pOb->SetPos ( 200, BG_POS_Y );

		AddpTask ( m_bg );
		GRPLST_INSERT_MAIN ( m_bg );


		//雲
		m_bg_cloud = make_shared < GrpAcv > ( _T("BG_evening_cloud_0.png") );
		m_bg_cloud->SetPos ( BG_POS_X, BG_POS_Y );
		m_bg_cloud->SetZ ( Z_BG );
//		m_bg_cloud->SetColor ( 0xa0ffffff );

		m_bg_cloud->AddObject ();
//		m_bg_cloud->GetpObject(1)->SetColor ( 0xff0000ff );

		AddpTask ( m_bg_cloud );
		GRPLST_INSERT_MAIN ( m_bg_cloud );
		m_cloud_delta = 0.05f;

		//後ろ草
		m_bg_middle = make_shared < GrpAcv > ();

#if 0
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0000.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0001.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0002.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0003.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0004.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0005.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0006.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0007.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0008.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0009.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0010.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0011.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0012.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0013.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0014.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0015.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0016.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0017.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0018.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0019.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0020.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0021.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0022.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0023.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0024.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0025.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0026.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0027.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0028.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0029.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0030.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0031.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0032.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0033.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0034.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0035.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0036.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0037.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0038.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0039.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0040.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0041.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0042.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0043.png") );
		m_bg_middle->AddTexture ( _T("BG_Middle\\image_058_0044.png") );

#endif // 0
		m_bg_middle->SetZ ( Z_BG -0.01f );
		m_bg_middle->ClearObject ();
		m_bg_middle->AddObject ();
		m_bg_middle->AddObject ();
//		AddpTask ( m_bg_middle );
//		GRPLST_INSERT_MAIN ( m_bg_middle );

		//基準
		m_bg_base = make_shared < GrpAcv > ( _T("BG_evening_base_1.png") );
		m_bg_base->SetPos ( BG_POS_X, BG_POS_Y );
		m_bg_base->SetZ ( Z_BG );
		AddpTask ( m_bg_base );
		GRPLST_INSERT_MAIN ( m_bg_base );

		//手前
		m_bg_front = make_shared < GrpAcv > ();
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0000.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0001.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0002.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0003.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0004.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0005.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0006.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0007.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0008.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0009.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0010.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0011.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0012.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0013.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0014.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0015.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0016.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0017.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0018.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0019.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0020.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0021.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0022.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0023.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0024.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0025.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0026.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0027.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0028.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0029.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0030.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0031.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0032.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0033.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0034.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0035.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0036.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0037.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0038.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0039.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0040.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0041.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0042.png") );
		m_bg_front->AddTexture ( _T("front_loop\\image_059_0043.png") );

#if 0

#endif // 0
		m_bg_front->SetZ ( Z_EFF );

		m_bg_front->ClearObject ();
		m_bg_front->AddObject ();
		m_bg_front->AddObject ();
		m_bg_front->AddObject ();
		m_bg_front->AddObject ();
		m_bg_front->AddObject ();
		m_bg_front->AddObject ();

//		AddpTask ( m_bg_front );
//		GRPLST_INSERT_MAIN ( m_bg_front );


		//手前2
		m_bg_front2 = make_shared < GrpAcv > ();
		m_bg_front2->AddTexture ( _T("front22\\image_059_0000.png") );
		m_bg_front2->SetZ ( Z_EFF + 0.01f );

//		AddpTask ( m_bg_front2 );
//		GRPLST_INSERT_MAIN ( m_bg_front2 );

		//手前3
		m_bg_front3 = make_shared < GrpAcv > ();
		m_bg_front3->AddTexture ( _T("front3\\image_059_0000.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0001.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0002.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0003.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0004.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0005.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0006.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0007.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0008.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0009.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0010.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0011.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0012.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0013.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0014.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0015.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0016.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0017.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0018.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0019.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0020.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0021.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0022.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0023.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0024.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0025.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0026.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0027.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0028.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0029.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0030.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0031.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0032.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0033.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0034.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0035.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0036.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0037.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0038.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0039.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0040.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0041.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0042.png") );
		m_bg_front3->AddTexture ( _T("front3\\image_059_0043.png") );
		m_bg_front3->SetZ ( Z_EFF );

		m_bg_front3->ClearObject ();
		m_bg_front3->AddObject ();
		m_bg_front3->AddObject ();
		m_bg_front3->AddObject ();
		m_bg_front3->AddObject ();
		m_bg_front3->AddObject ();
		m_bg_front3->AddObject ();

//		AddpTask ( m_bg_front3 );
//		GRPLST_INSERT_MAIN ( m_bg_front3 );


		//手前　岩
		m_bg_front_rock = make_shared < GrpAcv > ();
		m_bg_front_rock->AddTexture ( _T("BG_rock.png") );
		m_bg_front_rock->SetZ ( Z_EFF + 0.01f );
		AddpTask ( m_bg_front_rock );
		GRPLST_INSERT_MAIN ( m_bg_front_rock );

		//手前　岩2
		m_bg_front_rock2 = make_shared < GrpAcv > ();
		m_bg_front_rock2->AddTexture ( _T("BG_rock2.png") );
		m_bg_front_rock2->SetZ ( Z_EFF + 0.01f );
		AddpTask ( m_bg_front_rock2 );
		GRPLST_INSERT_MAIN ( m_bg_front_rock2 );


#if 0

//多重スクロールテスト
		m_bg_back = make_shared < GrpAcv > ();
		m_bg_back->AddTexture ( _T ( "BG0_Back.png" ) );
		m_bg_back->SetPos ( (float)BG_POS_X, (float)BG_POS_Y );
		m_bg_back->SetZ ( Z_BG + 0.02f );

		m_bg_middle = make_shared < GrpAcv > ();
		m_bg_middle->AddTexture ( _T ( "BG0_Middle.png" ) );
		m_bg_middle->SetPos ( (float)BG_POS_X, (float)BG_POS_Y );
		m_bg_middle->SetZ ( Z_BG + 0.01f );

		m_bg_front = make_shared < GrpAcv > ();
		m_bg_front->AddTexture ( _T ( "BG0_Front.png" ) );
		m_bg_front->SetPos ( (float)BG_POS_X, (float)BG_POS_Y );
		m_bg_front->SetZ ( Z_BG );

		AddpTask ( m_bg_back );
		GRPLST_INSERT_MAIN ( m_bg_back );
		AddpTask ( m_bg_middle );
		GRPLST_INSERT_MAIN ( m_bg_middle );
		AddpTask ( m_bg_front );
		GRPLST_INSERT_MAIN ( m_bg_front );

#endif // 0

#if 0
		m_bg_line = make_shared < GrpAcv > ();
		m_bg_line->AddTexture ( _T ( "BG_Line0.png" ) );
		m_bg_line->AddTexture ( _T ( "BG_Line1.png" ) );
		m_bg_line->AddTexture ( _T ( "BG_Line2.png" ) );
		m_bg_line->SetZ ( Z_BG );
		AddpTask ( m_bg_line );
		GRPLST_INSERT_MAIN ( m_bg_line );

		m_bg_concent_base = make_shared < GrpAcv > ();
		m_bg_concent_base->AddTexture ( _T ( "BG_concent_base.png" ) );
		m_bg_concent_base->SetZ ( Z_BG );
		m_bg_concent_base->SetPos ( (float)BG_POS_X, (float)BG_POS_Y );
		AddpTask ( m_bg_concent_base );
		GRPLST_INSERT_MAIN ( m_bg_concent_base );

		m_bg_concent = make_shared < GrpAcv > ();
		m_bg_concent->AddTexture ( _T ( "BG_concent.png" ) );
		m_bg_concent->SetZ ( Z_BG );
		m_bg_concent->SetRotationCenter ( VEC2 ( 1024, 1024 ) );
		m_bg_concent->SetPos ( (float)BG_POS_X, (float)BG_POS_Y - 512 );
		m_bg_concent->SetColor ( _CLR ( 0x40ffffff ) );
		AddpTask ( m_bg_concent );
		GRPLST_INSERT_MAIN ( m_bg_concent );
#endif // 0


		//------------------------------------------------
		//暗転
		m_bg_black = make_shared < PrmRect > ();
		m_bg_black->SetSize ( 1280, 960 );
		m_bg_black->SetAllColor ( 0x80000000 );
		m_bg_black->SetPos ( 0, 0 );
		m_bg_black->SetZ ( Z_BG );
		AddpTask ( m_bg_black );
		GRPLST_INSERT_MAIN ( m_bg_black );
		m_bg_black->SetValid ( F );

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

		//壁割り
		m_wall_break = make_shared < GrpAcv > ();
		m_wall_break->AddTexture ( _T("glass.png") );
		m_wall_break->AddTexture ( _T("break0.png") );
		m_wall_break->AddTexture ( _T("break1.png") );
		m_wall_break->AddTexture ( _T("break2.png") );
		m_wall_break->AddTexture ( _T("break3.png") );
		m_wall_break->SetScaling ( 0.5f, 1.f );
		AddpTask ( m_wall_break );
		GRPLST_INSERT_MAIN ( m_wall_break );
		m_wall_break->SetValid ( F );

		m_tmrWallBreak = make_shared < Timer > ();
		AddpTask ( m_tmrWallBreak );
	}

	BG::~BG ()
	{
	}


	void BG::Init ()
	{
		m_bg_black->SetValid ( F );
		m_bg_white->SetValid ( F );

		m_tmrBlackOut->Clear ();
		m_tmrWhiteOut->Clear ();
		m_tmrWallBreak->Clear ();

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
				m_bg_black->SetValid ( F );
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

		//壁割り
		if ( m_tmrWallBreak->IsActive () )
		{
			//終了
			if ( m_tmrWallBreak->IsLast () )
			{
				m_wall_break->SetIndexTexture ( m_indexTexture );
				if ( ++ m_indexTexture > 3 )
				{
					m_indexTexture = 0; 
					m_wall_break->SetValid ( F );
				} 
				m_tmrWallBreak->Start ( 6 );
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
#if 0
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

#endif // 0
		//-------------------------------------------------------
		//背景位置補正
		int nx = (int) G_BASE_POS ().x;
		int disp_bg_x = nx % GAME_WIDTH;
		

		//m_bg->SetPos ( (float)disp_bg_x, (float)BG_POS_Y );

		m_bg_base->SetPos ( (float)disp_bg_x, (float)BG_POS_Y );


		//雲
		VEC2 cloud_pos = m_bg_cloud->GetPos ();

		m_cloud_delta += 0.050f;
//		m_cloud_delta += 50.f;
		if ( m_cloud_delta > GAME_WIDTH ) { m_cloud_delta = -GAME_WIDTH; }

		float cloud_x = 0.5f * disp_bg_x + m_cloud_delta;
		float cloud_y = (float)BG_POS_Y ;
		m_bg_cloud->SetPos ( cloud_x, cloud_y );
		m_bg_cloud->GetpObject ( 1 )->SetPos ( cloud_x - 2048 - 512, cloud_y );


		//アニメーション
		PVP_Object pvpOb = m_bg_front->GetpvpObject ();
		PVP_Object pvpOb3 = m_bg_front3->GetpvpObject ();
		if ( m_indexFront > 43 ) { m_indexFront = 0; }
		for ( UINT i = 0; i < 6; ++ i )
		{
			pvpOb->at ( i )->SetIndexTexture ( m_indexFront );
			pvpOb3->at ( i )->SetIndexTexture ( m_indexFront );
		}
		pvpOb->at ( 0 )->SetPos ( -100 + 1.1f * disp_bg_x, BG_POS_FRONT_Y );
		pvpOb->at ( 1 )->SetPos ( 180 + 1.1f * disp_bg_x, BG_POS_FRONT_Y );
		pvpOb->at ( 2 )->SetPos ( 450 + 1.1f * disp_bg_x, BG_POS_FRONT_Y );
		pvpOb->at ( 3 )->SetPos ( 750 + 1.1f * disp_bg_x, BG_POS_FRONT_Y );
		pvpOb->at ( 4 )->SetPos ( 1210 + 1.1f * disp_bg_x, BG_POS_FRONT_Y );
		pvpOb->at ( 5 )->SetPos ( 1500 + 1.1f * disp_bg_x, BG_POS_FRONT_Y );
		pvpOb3->at ( 0 )->SetPos ( 0 + 1.1f * disp_bg_x, 960 - 256 );
		pvpOb3->at ( 1 )->SetPos ( 400 + 1.1f * disp_bg_x, 960 - 256 );
		pvpOb3->at ( 2 )->SetPos ( 750 + 1.1f * disp_bg_x, 960 - 256 );
		pvpOb3->at ( 3 )->SetPos ( 1000 + 1.1f * disp_bg_x, 960 - 256 );
		pvpOb3->at ( 4 )->SetPos ( 1480 + 1.1f * disp_bg_x, 960 - 256 );
		pvpOb3->at ( 5 )->SetPos ( 1800 + 1.1f * disp_bg_x, 960 - 256 );
//		m_bg_front ->SetColor ( 0xff0000ff );
//		m_bg_front3->SetColor ( 0xffff0000 );

		PVP_Object pvpOb_middle = m_bg_middle->GetpvpObject ();
		pvpOb_middle->at(0)->SetPos ( -100.f + disp_bg_x, BG_POS_MIDDLE_Y );
		pvpOb_middle->at(0)->SetIndexTexture ( m_indexFront );
		pvpOb_middle->at(1)->SetPos ( 1200.f + disp_bg_x, BG_POS_MIDDLE_Y );
		pvpOb_middle->at(1)->SetIndexTexture ( m_indexFront );


		m_bg_front2->SetPos ( 980 + 1.1f * disp_bg_x, 960 - 256 );
		m_bg_front_rock->SetPos ( 1600 + 1.1f * disp_bg_x, 960 - 256 );
		m_bg_front_rock2->SetPos ( -200 + 1.1f * disp_bg_x, 960 - 256 );



		if ( ++ m_indexFront_pitch > 3 ) 
		{
			++ m_indexFront;
			m_indexFront_pitch = 0;
		}

#if 0

		m_bg_back->SetPos ( BG_POS_X, (float)BG_POS_Y );
		m_bg_middle->SetPos ( 0.5f * (float)disp_bg_x, (float)BG_POS_Y );
		m_bg_front->SetPos ( (float)disp_bg_x, (float)BG_POS_Y );
#endif // 0

#if 0
		static int index = 0;
		m_bg_line->SetIndexTexture ( index >= 3 ? index = 0: ++ index );

		static float rad = 0;
		m_bg_concent->SetRadian ( rad += 0.2f );
#endif // 0


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


	void BG::SetBlackOut ( UINT n )
	{
		m_tmrBlackOut->Start ( n );
//		m_bg->SetValid ( F );
//		m_bg_blackout->SetValid ( T );
		m_bg_black->SetValid ( T );
	}

	void BG::OffBlackOut ()
	{
		m_tmrBlackOut->Clear ();
		m_bg_black->SetValid ( F );
	}

	void BG::SetWhiteOut ( UINT n )
	{
		m_tmrWhiteOut->Start ( n );
		m_bg_white->SetValid ( T );
	}

	void BG::StartWallBreak_L ()
	{
		m_tmrWallBreak->Start ( 4 );
		m_wall_break->SetPos ( 0, 0 );
		m_wall_break->SetValid ( T );
	}
	void BG::StartWallBreak_R ()
	{
		m_tmrWallBreak->Start ( 4 );
		m_wall_break->SetValid ( T );
		m_wall_break->SetPos ( GAME_WINDOW_WIDTH / 2, 0 );
	}


}	//namespace GAME

