//=================================================================================================
//
//	DemoMenuItem ヘッダファイル
//
//=================================================================================================
#pragma once

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "Game.h"
//#include "DemoMain.h" // ->前方宣言に移行


//-------------------------------------------------------------------------------------------------
// 宣言
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//循環参照のため前方宣言
	class DemoMain;
	using P_DemoMain = std::shared_ptr < DemoMain >;


	class DemoMenuItem : public MenuItem
	{
	protected:
		P_DemoMain		m_demoMain;
		P_GrpAcv		m_grp;

	public:
		DemoMenuItem ();
		DemoMenuItem ( const MenuItem & rhs ) = delete;
		~DemoMenuItem ();

		virtual void Do () {}

		void SetPos ( VEC2 v ) { m_grp->SetPos ( v ); }

		void SetpDemoMain ( P_DemoMain p ) { m_demoMain = p; }
	};

	using P_DemoMenuItem = std::shared_ptr < DemoMenuItem >;
	using VP_DemoMenuItem = std::vector < P_DemoMenuItem >;


	//-------------------------------------------
	class Menu_1v2 : public DemoMenuItem { public: Menu_1v2 ();	void Do (); };
	class Menu_1vc : public DemoMenuItem { public: Menu_1vc ();	void Do (); };
	class Menu_cv2 : public DemoMenuItem { public: Menu_cv2 ();	void Do (); };
	class Menu_cvc : public DemoMenuItem { public: Menu_cvc ();	void Do (); };

	using P_Menu_1v2 = std::shared_ptr < Menu_1v2 >;
	using P_Menu_1vc = std::shared_ptr < Menu_1vc >;
	using P_Menu_cv2 = std::shared_ptr < Menu_cv2 >;
	using P_Menu_cvc = std::shared_ptr < Menu_cvc >;


}	//namespace GAME


