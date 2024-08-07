//=================================================================================================
//
//	PauseMenu
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "PauseMenu.h"
#include "PauseMenu_Const.h"


//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	PauseMenu::PauseMenu ()
		: m_bMenu ( F )
	{
		//--------------------------------------------
		m_bg = make_shared < PrmRect > ();
		m_bg->SetSize ( GAME_WINDOW_WIDTH - 200, GAME_WINDOW_HEIGHT - 200 );
		m_bg->SetAllColor ( 0xa0000000 );
		m_bg->SetPos ( 100, 100 );
		m_bg->SetZ ( Z_MENU_BG );
		AddpTask ( m_bg );
		GRPLST_INSERT_MAIN ( m_bg );

		//--------------------------------------------
		m_grpStr_pause = make_shared < MenuString > ();
		m_grpStr_pause->SetStr ( _T ( "- PAUSE -" ) );
		m_grpStr_pause->SetPos ( 500, 120 );
		m_grpStr_pause->SetZ ( Z_MENU_STR );

		//@todo Z値が小さい(手前の)とき文字列が描画されないことの調査

		AddpTask ( m_grpStr_pause );
		GRPLST_INSERT_MAIN ( m_grpStr_pause );

		//--------------------------------------------
		//カーソル
		m_cursor = make_shared < GrpAcv > ();
		m_cursor->AddTexture ( _T ( "title_cursor.png" ) );
		m_cursor->SetPos ( 100, 300 );
		m_cursor->SetZ ( Z_MENU_STR );
		AddpTask ( m_cursor );
		GRPLST_INSERT_MAIN ( m_cursor );

		//--------------------------------------------
		//メニュー項目
		m_mi_title = make_shared < PMI_To_Title > ();
		AddpTask ( m_mi_title );

		m_mi_resume = make_shared < PMI_ResumeGame > ();
		AddpTask ( m_mi_resume );

		//--------------------------------------------
		//Y/Nメニュ
		m_yesnoMenu = make_shared < YesNo_Menu > ();
		AddpTask ( m_yesnoMenu );

		//--------------------------------------------
		//初期状態はOff
		Off ();
	}

	PauseMenu::~PauseMenu ()
	{
	}

	void PauseMenu::Load ()
	{
		//--------------------------------------------
		//メニュー項目
		Menu::SetpMenuItem ( m_mi_title );
		Menu::SetpMenuItem ( m_mi_resume );

		Menu::Load ();
	}

	void PauseMenu::Do ()
	{
	}

	void PauseMenu::Move ()
	{
		//Y/Nメニュ稼働時は何もしない
		if ( m_yesnoMenu->GetActive () ) { Menu::Move (); return; }

		//非アクティブ時は何もしない
		if ( ! GetActive () ) { Menu::Move (); return; }
		if ( ! m_bMenu )
		{
			Menu::Move (); return; 
		}

#if 0
		//@info 解除を同一ボタンにすると同[F]で解除されてしまう

		//メニュポーズ解除
		if ( CFG_PUSH_KEY ( _P1_BTN5 ) || CFG_PUSH_KEY ( _P2_BTN5 ) )
		{
			Off ();
		}
#endif // 0
		
		Input ();

		//Do()は選択されたメニュ項目について常に行う
		Menu::Do ();

		//カーソル位置
		m_cursor->SetPos ( 100.f, 300.f + 100 * Menu::GetIdItem () );

		Menu::Move ();
	}


	bool PauseMenu::MenuCheck ()
	{
		//メニュポーズ中
		if ( m_bMenu )
		{
			//メニュポーズ解除
			bool bEsc = ( WND_UTL::AscKey ( VK_ESCAPE ) );
			bool bMenuBtn = ( CFG_PUSH_KEY ( _P1_BTN5 ) || CFG_PUSH_KEY ( _P2_BTN5 ) );
			if ( bEsc || bMenuBtn )
			{
				Off ();
				return F;
			}
			else
			{
				Move ();
				return T;
			}
		}

		//メニュポーズ開始
		bool bEsc = ( WND_UTL::AscKey ( VK_ESCAPE ) );
		bool bMenuBtn = ( CFG_PUSH_KEY ( _P1_BTN5 ) || CFG_PUSH_KEY ( _P2_BTN5 ) );
		if ( bEsc || bMenuBtn )
		{
			On ();
		}

		return F;
	}

	void PauseMenu::Input ()
	{
		//=============================================================
		// 操作
		//=============================================================
		//選択
		if ( CFG_PUSH_KEY ( _P1_DOWN ) || CFG_PUSH_KEY ( _P2_DOWN ) )
		{
			Menu::Next ();
		}
		if ( CFG_PUSH_KEY ( _P1_UP ) || CFG_PUSH_KEY ( _P2_UP ) )
		{
			Menu::Prev ();
		}

		//決定
		if ( CFG_PUSH_KEY ( _P1_BTN0 ) || CFG_PUSH_KEY ( _P2_BTN0 ) )
		{
			Menu::Decide ();
		}
		//=============================================================
	}


	void PauseMenu::Off ()
	{
		m_bg->SetValid ( F );
		m_grpStr_pause->SetValid ( F );
		m_cursor->SetValid ( F );

		m_mi_title->Off ();
		m_mi_resume->Off ();

		m_yesnoMenu->Off ();

		m_bMenu = F;
		SetActive ( F );
	}

	void PauseMenu::UnDisp ()
	{
		m_bg->SetValid ( F );
		m_grpStr_pause->SetValid ( F );
		m_cursor->SetValid ( F );

		m_mi_title->Off ();
		m_mi_resume->Off ();

		m_yesnoMenu->Off ();

		//全体稼働フラグは残す
		//m_bMenu = F;
		SetActive ( F );
	}

	void PauseMenu::On ()
	{
		m_bg->SetValid ( T );
		m_grpStr_pause->SetValid ( T );
		m_cursor->SetValid ( T );

		m_mi_title->On ();
		m_mi_resume->On ();

		m_bMenu = T;
		SetActive ( T );
	}

#if 0
	void PauseMenu::SetwpParent ( WP_FtgMain p )
	{
		m_mi_title->SetwpParent ( shared_from_this () );
		m_mi_resume->SetwpParent ( shared_from_this () );

		m_yesnoMenu->SetwpParent ( p );
	}
#endif // 0

	void PauseMenu::SetwpParentScene ( WP_Scene wp )
	{
		m_mi_title->SetwpParent ( shared_from_this () );
		m_mi_resume->SetwpParent ( shared_from_this () );

		m_yesnoMenu->SetwpParentScene ( wp );
	}

}	//namespace GAME

