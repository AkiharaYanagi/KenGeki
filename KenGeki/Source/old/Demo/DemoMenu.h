//=================================================================================================
//
//	DemoMenu ヘッダファイル
//
//=================================================================================================
#pragma once

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "Game.h"
#include "DemoMenuItem.h"


//-------------------------------------------------------------------------------------------------
// 宣言
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	class DemoMenu : public TASK_VEC
	{
		P_PrmRect		m_BG;			//背景
		P_Str			m_strDemo;		//デモ表示
		P_GrpAcv		m_arrow;		//選択矢印

		//デモ項目表示
		P_GrpAcv		m_1p_vs_2p;
		P_GrpAcv		m_1p_vs_cpu;
		P_GrpAcv		m_cpu_vs_2p;
		P_GrpAcv		m_cpu_vs_cpu;

		//項目オブジェクト
		P_Menu_1v2		m_menu_1v2;
		P_Menu_1vc		m_menu_1vc;
		P_Menu_cv2		m_menu_cv2;
		P_Menu_cvc		m_menu_cvc;

		//選択
		VP_DemoMenuItem		mv_menu;
		VP_DemoMenuItem::const_iterator		m_itMenu;

	public:
		DemoMenu ();
		DemoMenu ( const DemoMenu & rhs ) = delete;
		~DemoMenu ();

		void Init ();
		void Move ();

		void ParamInit ( P_DemoMain pDemoMain );
	};

	using P_DemoMenu = std::shared_ptr < DemoMenu >;


}	//namespace GAME


