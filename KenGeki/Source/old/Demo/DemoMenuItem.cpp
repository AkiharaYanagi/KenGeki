//=================================================================================================
//
//	MenuItem ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "DemoMenuItem.h"
#include "DemoMain.h"


//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//-------------------------------------------
	DemoMenuItem::DemoMenuItem ()
	{
		m_grp = make_shared < GrpAcv > ();
	}

	DemoMenuItem::~DemoMenuItem ()
	{

	}

	//-------------------------------------------


	Menu_1v2::Menu_1v2 ()
	{
		m_grp->AddTexture ( _T ( "menu\\1P_VS_2P.png" ) );
		GRPLST_INSERT_MAIN ( m_grp );
	}

	void Menu_1v2::Do ()
	{
		m_demoMain->To_1v2 ();
	}

	Menu_1vc::Menu_1vc ()
	{
		m_grp->AddTexture ( _T ( "menu\\1P_VS_CPU.png" ) );
		GRPLST_INSERT_MAIN ( m_grp );
	}

	void Menu_1vc::Do ()
	{
		m_demoMain->To_1vC ();
	}

	Menu_cv2::Menu_cv2 ()
	{
		m_grp->AddTexture ( _T ( "menu\\CPU_VS_2P.png" ) );
		GRPLST_INSERT_MAIN ( m_grp );
	}

	void Menu_cv2::Do ()
	{
		m_demoMain->To_Cv2 ();
	}

	Menu_cvc::Menu_cvc ()
	{
		m_grp->AddTexture ( _T ( "menu\\CPU_VS_CPU.png" ) );
		GRPLST_INSERT_MAIN ( m_grp );
	}

	void Menu_cvc::Do ()
	{
		m_demoMain->To_CvC ();
	}


}	//namespace GAME

