//=================================================================================================
//
//	Demo ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "DemoMain.h"
#include "../FtgMain/FtgMain.h"

//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	DemoMain::DemoMain ()
	{
		//メニュ
		m_menu = make_shared < DemoMenu > ();
		AddpTask ( m_menu );

		//戦闘
		m_fighting = make_shared < Fighting > ();
		AddpTask ( m_fighting );
	}

	DemoMain::~DemoMain ()
	{
	}

	void DemoMain::ParamInit ()
	{
		//パラメータ手動指定
		P_Param pParam = GetpParam ();
		GameSettingFile stg = pParam->GetGameSetting ();
		stg.SetPlayerMode1p ( MODE_CPU );
		stg.SetPlayerMode2p ( MODE_CPU );
		pParam->SetSettingFile ( stg );

		m_fighting->ParamInit ( pParam );


		//コンストラクタ後 shared_from_this ()
		m_scene = shared_from_this ();
		m_menu->ParamInit ( shared_from_this () );
	}

	P_GameScene DemoMain::Transit ()
	{
		//@info MenuItemにthisを渡しておく
#if 0
		//ESCで戻る
		if ( ::GetAsyncKeyState ( VK_ESCAPE ) & 0x0001 )
		{
			SOUND->Stop ( BGM_Main );
			return make_shared < Title > ();
		}
#endif // 0

		//遷移先を返す（通常時は自身）
//		return shared_from_this ();
		return m_scene;
	}


	void DemoMain::Pause ()
	{
		//F1でポーズ切替
		if ( ::GetAsyncKeyState ( VK_F1 ) & 0x0001 )
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

	void DemoMain::To_1v2 ()
	{
		GRPLST_CLEAR ();
		m_scene = make_shared < FtgMain > ();
		GRPLST_LOAD ();
//		GRPLST_INIT ();
	}

	void DemoMain::To_1vC ()
	{
		m_scene = make_shared < FtgMain > ();
	}

	void DemoMain::To_Cv2 ()
	{
		m_scene = make_shared < FtgMain > ();
	}

	void DemoMain::To_CvC ()
	{
		m_scene = make_shared < FtgMain > ();
	}


}	//namespace GAME

