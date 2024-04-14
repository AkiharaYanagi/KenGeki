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
		//------------------------------------------------
		//背景
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
#define DEMO_ON		0
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
		//暗転
		UINT blackOut = m_mutualChara->GetBlackOut ();
		if ( 0 != blackOut )
		{
			m_bg->SetBlackOut ( blackOut );
			m_mutualChara->SetBlackOut ( 0 );
		}

		//白転
		UINT whiteOut = m_mutualChara->CheckWhiteOut () ? 60 : 0;
		if ( 0 != whiteOut )
		{
			m_bg->SetWhiteOut ( whiteOut );
			m_mutualChara->SetWhiteOut ( F );
		}

		//背景通常処理
		m_bg->Grp ();
	}


	void Fighting::AssignWp ( WP_FTG wp ) const
	{
		m_mutualChara->AssignWp ( wp );
	}

}	//namespace GAME

