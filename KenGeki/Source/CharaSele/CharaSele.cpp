//=================================================================================================
//
//	CharaSele ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "CharaSele.h"
#include "../Title/Title.h"
#include "../FtgMain/Fighting.h"
//#include "../Training/Training.h"

//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	CharaSele::CharaSele ()
	{
		//BG
		m_bg = make_shared < GrpAcv > ();
		m_bg->AddTexture ( _T ( "CharaSele_BG.png" ) );
		AddpTask ( m_bg );
		GRPLST_INSERT_MAIN ( m_bg );

#if 0
		//キャラ枠位置
		mv_pos.push_back ( ChSlct { CHSLID_0, 362,  17, CHSLID_7, CHSLID_3, CHSLID_2, CHSLID_1 } );
		mv_pos.push_back ( ChSlct { CHSLID_1, 512,  17, CHSLID_8, CHSLID_4, CHSLID_0, CHSLID_2 } );
		mv_pos.push_back ( ChSlct { CHSLID_2, 662,  17, CHSLID_9, CHSLID_4, CHSLID_1, CHSLID_0 } );
		mv_pos.push_back ( ChSlct { CHSLID_3, 437, 167, CHSLID_0, CHSLID_5, CHSLID_4, CHSLID_4 } );
		mv_pos.push_back ( ChSlct { CHSLID_4, 587, 167, CHSLID_1, CHSLID_6, CHSLID_3, CHSLID_3 } );
		mv_pos.push_back ( ChSlct { CHSLID_5, 437, 577, CHSLID_3, CHSLID_7, CHSLID_6, CHSLID_6 } );
		mv_pos.push_back ( ChSlct { CHSLID_6, 587, 577, CHSLID_4, CHSLID_8, CHSLID_5, CHSLID_5 } );
		mv_pos.push_back ( ChSlct { CHSLID_7, 362, 727, CHSLID_5, CHSLID_0, CHSLID_9, CHSLID_8 } );
		mv_pos.push_back ( ChSlct { CHSLID_8, 512, 727, CHSLID_6, CHSLID_1, CHSLID_7, CHSLID_9 } );
		mv_pos.push_back ( ChSlct { CHSLID_9, 662, 727, CHSLID_6, CHSLID_2, CHSLID_8, CHSLID_7 } );

		//カーソル
		m_cursor1p = make_shared < GrpBlink > ();
		m_cursor1p->AddTexture ( _T ( "CharaSele_Cursor_1p.png" ) );
		m_cursor1p->SetPos ( SELECT_1P_POS_X, SELECT_1P_POS_Y );
		m_cursor1p->SetTimer ( 15 );
		AddpTask ( m_cursor1p );
		GRPLST_INSERT_MAIN ( m_cursor1p );

		m_cursor2p = make_shared < GrpBlink > ();
		m_cursor2p->AddTexture ( _T ( "CharaSele_Cursor_2p.png" ) );
		m_cursor2p->SetPos ( SELECT_2P_POS_X, SELECT_2P_POS_Y );
		AddpTask ( m_cursor2p );
		GRPLST_INSERT_MAIN ( m_cursor2p );

		//選択位置
		m_chslct_id_1p = CHSLID_0;
		m_chslct_id_2p = CHSLID_2;


		//キャラ立絵
		m_chara_1p = make_shared < GrpAcv > ();
		m_chara_1p->AddTexture ( _T("Stand_Ouka.png") );
		m_chara_1p->AddTexture ( _T("Stand_Sae.png") );
		m_chara_1p->SetPos ( CHARA_1P_POS_X, CHARA_1P_POS_Y );
		AddpTask ( m_chara_1p );
		GRPLST_INSERT_MAIN ( m_chara_1p );

		m_chara_2p = make_shared < GrpAcv > ();
		m_chara_2p->AddTexture ( _T("Stand_Ouka.png") );
		m_chara_2p->AddTexture ( _T("Stand_Sae.png") );
		m_chara_2p->SetPos ( CHARA_2P_POS_X, CHARA_2P_POS_Y );
		AddpTask ( m_chara_2p );
		GRPLST_INSERT_MAIN ( m_chara_2p );

#endif // 0

		//選択プレイヤ
		m_chsl_pl_1p = make_shared < CharaSele_Player > ();
		m_chsl_pl_1p->PlayerInit ( PLAYER_ID_1 );
		AddpTask ( m_chsl_pl_1p );

		m_chsl_pl_2p = make_shared < CharaSele_Player > ();
		m_chsl_pl_2p->PlayerInit ( PLAYER_ID_2 );
		AddpTask ( m_chsl_pl_2p );

	}

	CharaSele::~CharaSele ()
	{
	}

	void CharaSele::Load ()
	{
		//==================================================
		//	Scene共通
		//==================================================
		//遷移先を自身に設定
		//	(コンストラクタでは shared_from_this() が使えないため、Load() で呼び出す)
		Scene::SetwpThis ( shared_from_this () );
		//==================================================
		
		Scene::Load ();
	}

	P_GameScene CharaSele::Transit ()
	{
		//ESCで戻る
//		if ( ::GetAsyncKeyState ( VK_ESCAPE ) & 0x0001 )
		if ( WND_UTL::AscKey ( VK_ESCAPE ) )
		{
			return make_shared < Title > ();
		}

		return Scene::Transit (); 
	}


	void CharaSele::ParamInit ()
	{
	}
	

	void CharaSele::Init ()
	{
		Scene::Init (); 
	}
	
	void CharaSele::Move ()
	{
#if 0
		//入力
		if ( CFG_PUSH_KEY ( _P1_UP ) ) { SetCursorUp ( m_cursor1p, m_chslct_id_1p ); }
		if ( CFG_PUSH_KEY ( _P1_DOWN ) ) { SetCursorDown ( m_cursor1p, m_chslct_id_1p ); }
		if ( CFG_PUSH_KEY ( _P1_LEFT ) ) { SetCursorLeft ( m_cursor1p, m_chslct_id_1p ); }
		if ( CFG_PUSH_KEY ( _P1_RIGHT ) ) { SetCursorRight ( m_cursor1p, m_chslct_id_1p ); }
		
		if ( CFG_PUSH_KEY ( _P2_UP ) ) { SetCursorUp ( m_cursor2p, m_chslct_id_2p ); }
		if ( CFG_PUSH_KEY ( _P2_DOWN ) ) { SetCursorDown ( m_cursor2p, m_chslct_id_2p ); }
		if ( CFG_PUSH_KEY ( _P2_LEFT ) ) { SetCursorLeft ( m_cursor2p, m_chslct_id_2p ); }
		if ( CFG_PUSH_KEY ( _P2_RIGHT ) ) { SetCursorRight ( m_cursor2p, m_chslct_id_2p ); }


		//キャラ表示
		if ( m_chslct_id_1p == CHSLID_0 ) { m_chara_1p->SetIndexTexture ( CHSLID_0 ); }
		if ( m_chslct_id_1p == CHSLID_2 ) { m_chara_1p->SetIndexTexture ( 1 ); }
		if ( m_chslct_id_2p == CHSLID_0 ) { m_chara_2p->SetIndexTexture ( CHSLID_0 ); }
		if ( m_chslct_id_2p == CHSLID_2 ) { m_chara_2p->SetIndexTexture ( 1 ); }

#endif // 0

		Scene::Move (); 
	}

	bool CharaSele::KeyLR1p () { return F; }
	bool CharaSele::KeyLR2p () { return F; }

	void CharaSele::Move1p () {}
	void CharaSele::Move2p () {}
	void CharaSele::Select1p () {}
	void CharaSele::Select2p () {}
	void CharaSele::SelectCPU1p () {}
	void CharaSele::SelectCPU2p () {}
	void CharaSele::Decision1p () {}
	void CharaSele::Decision2p () {}
	void CharaSele::DecisionCPU1p () {}
	void CharaSele::DecisionCPU2p () {}


#if 0
	CharaSele::CharaSele ()
	{
		SOUND->PlayLoop ( 0 );

		//---------------------------------------------
		//Sceneの最初一回のみ、GrpLstをゲームタスクに設定
		AddpTask ( GRPLST_NEW () );
		//---------------------------------------------

		//BG
		m_bg = make_shared < GrpAcv > ();
		m_bg->AddTexture ( _T ( "BG_CharaSelect.png" ) );
		GRPLST_INSERT_MAIN ( m_bg );

		//ロゴ
		m_logo_l = make_shared < GrpAcv > ();
		m_logo_l->AddTexture ( _T ( "CharaSelectLogo.png" ) );
		m_logo_l->ResetObjectNum ( LOGO_NUM );
		PVP_Object pvpObject_l = m_logo_l->GetpvpObject ();
		int il = 0;
		for ( P_Object p : *pvpObject_l )
		{
			p->SetPos ( 1.f * LOGO_W * il, 1.f * LOGO_L_START_Y );
			++il;
		}
		GRPLST_INSERT_MAIN ( m_logo_l );

		m_logo_r = make_shared < GrpAcv > ();
		m_logo_r->AddTexture ( _T ( "CharaSelectLogo.png" ) );
		m_logo_r->ResetObjectNum ( LOGO_NUM );
		PVP_Object pvpObject_r = m_logo_r->GetpvpObject ();
		int ir = 0;
		for ( P_Object p : *pvpObject_r )
		{
			p->SetPos ( 1.f * LOGO_W * ir, 1.f * LOGO_R_START_Y );
			++ir;
		}
		GRPLST_INSERT_MAIN ( m_logo_r );

		//キャラ立絵カットイン
		m_chara_1p = make_shared < GrpAcv > ();
		m_chara_1p->AddTexture ( _T ( "Chara_Sonia.png" ) );
		m_chara_1p->AddTexture ( _T ( "Chara_Orfloat.png" ) );
		m_chara_1p->SetPos ( CHARA_1P_POS_X0, CHARA_1P_POS_Y );
		GRPLST_INSERT_MAIN ( m_chara_1p );

		m_chara_light_1p = make_shared < GrpAcv > ();
		m_chara_light_1p->AddTexture ( _T ( "Chara_Sonia_light.png" ) );
		m_chara_light_1p->AddTexture ( _T ( "Chara_Orfloat_light.png" ) );
		m_chara_light_1p->SetPos ( CHARA_1P_POS_X0, CHARA_1P_POS_Y );
		GRPLST_INSERT_MAIN ( m_chara_light_1p );
		m_chara_light_1p->SetValid ( false );


		m_chara_2p = make_shared < GrpAcv > ();
		m_chara_2p->AddTexture ( _T ( "Chara_Sonia.png" ) );
		m_chara_2p->AddTexture ( _T ( "Chara_Orfloat.png" ) );
		m_chara_2p->SetPos ( CHARA_2P_POS_X0, CHARA_2P_POS_Y );
		GRPLST_INSERT_MAIN ( m_chara_2p );

		m_chara_light_2p = make_shared < GrpAcv > ();
		m_chara_light_2p->AddTexture ( _T ( "Chara_Sonia_light.png" ) );
		m_chara_light_2p->AddTexture ( _T ( "Chara_Orfloat_light.png" ) );
		m_chara_light_2p->SetPos ( CHARA_2P_POS_X0, CHARA_2P_POS_Y );
		GRPLST_INSERT_MAIN ( m_chara_light_2p );
		m_chara_light_2p->SetValid ( false );


		m_x_1p = CHARA_1P_POS_X0;
		m_x_2p = CHARA_2P_POS_X0;


		//キャラ選択表示
		m_select_Sonia = make_shared < GrpAcv > ();
		m_select_Sonia->AddTexture ( _T ( "CharaSelect_Sonia.png" ) );
		m_select_Sonia->SetPos ( SELECT_1P_POS_X, SELECT_1P_POS_Y );
		GRPLST_INSERT_MAIN ( m_select_Sonia );

		m_select_Orfloat = make_shared < GrpAcv > ();
		m_select_Orfloat->AddTexture ( _T ( "CharaSelect_Orfloat.png" ) );
		m_select_Orfloat->SetPos ( SELECT_2P_POS_X, SELECT_2P_POS_Y );
		GRPLST_INSERT_MAIN ( m_select_Orfloat );

		//カーソル
		m_cursor1p = make_shared < GrpBlink > ();
		m_cursor1p->AddTexture ( _T ( "CharaSelect_Cursor1p.png" ) );
		m_cursor1p->SetPos ( SELECT_1P_POS_X, SELECT_1P_POS_Y );
		m_cursor1p->SetTimer ( 15 );
		GRPLST_INSERT_MAIN ( m_cursor1p );

		m_cursor2p = make_shared < GrpBlink > ();
		m_cursor2p->AddTexture ( _T ( "CharaSelect_Cursor2p.png" ) );
		m_cursor2p->SetPos ( SELECT_2P_POS_X, SELECT_2P_POS_Y );
		GRPLST_INSERT_MAIN ( m_cursor2p );

		m_cursorCPU1p = make_shared < GrpBlink > ();
		m_cursorCPU1p->AddTexture ( _T ( "CharaSelect_CursorCPU1p.png" ) );
		m_cursorCPU1p->SetPos ( SELECT_1P_POS_X, SELECT_1P_POS_Y );
		GRPLST_INSERT_MAIN ( m_cursorCPU1p );

		m_cursorCPU2p = make_shared < GrpBlink > ();
		m_cursorCPU2p->AddTexture ( _T ( "CharaSelect_CursorCPU2p.png" ) );
		m_cursorCPU2p->SetPos ( SELECT_2P_POS_X, SELECT_2P_POS_Y );
		GRPLST_INSERT_MAIN ( m_cursorCPU2p );

		//選択キャラ
#if 0
		m_charaName1p = CHARA_SONIA;
		m_chara_1p->SetIndexTexture ( CHARA_SONIA );
		m_chara_light_1p->SetIndexTexture ( CHARA_SONIA );
		m_charaName2p = CHARA_ORFLOAT;
		m_chara_2p->SetIndexTexture ( CHARA_ORFLOAT );
		m_chara_light_2p->SetIndexTexture ( CHARA_ORFLOAT );
#endif // 0

		m_bDecide1p = false;	//1P側決定(1Pコントローラで2P側を操作)
		m_bDecide2p = false;	//2P側決定

		m_fade = make_shared < Fade > ();
		GRPLST_INSERT_MAIN ( m_fade );

		m_endWait = false;

		//タイマー
		m_tmrDecide1p = make_shared < Timer > ();
		m_tmrDecide1p->SetTargetTime ( 5 );
		AddpTask ( m_tmrDecide1p );
		m_tmrDecide2p = make_shared < Timer > ();
		m_tmrDecide2p->SetTargetTime ( 5 );
		AddpTask ( m_tmrDecide2p );
	}

	CharaSele::~CharaSele ()
	{
	}

	void CharaSele::ParamInit ()
	{
		//パラメータの保存
		P_Param pParam = GetpParam ();
		pParam->SetCharaName1p ( m_charaName1p );
		pParam->SetCharaName2p ( m_charaName2p );

		m_mode = pParam->GetMutchMode ();
		switch ( m_mode )
		{
		case MODE_PLAYER_PLAYER: 
			m_cursorCPU1p->SetValid ( false );
			m_cursorCPU2p->SetValid ( false );
			break;
		case MODE_PLAYER_CPU:
			m_cursorCPU1p->SetValid ( false );
			m_cursor2p->SetValid ( false );
			break;
		case MODE_CPU_PLAYER:
			m_cursor1p->SetValid ( false );
			m_cursorCPU2p->SetValid ( false );
			break;
		case MODE_CPU_CPU:
			m_cursor1p->SetValid ( false );
			m_cursor2p->SetValid ( false );
			break;
		}
	}

	void CharaSele::Init ()
	{
		Scene::Init ();
	}

	void CharaSele::Move ()
	{
		//---------------------------------------------------------------------------
		//ロゴ動作
		PVP_Object pvpObject_l = m_logo_l->GetpvpObject ();
		for ( P_Object p : *pvpObject_l )
		{
			p->AddPosX ( 1.f * LOGO_L_VEL_X );
			if ( p->GetPos ().x < 0.f - LOGO_W ) { p->SetPosX ( 1.f * LOGO_W * (LOGO_NUM - 1) ); }
		}
		PVP_Object pvpObject_r = m_logo_r->GetpvpObject ();
		for ( P_Object p : *pvpObject_r )
		{
			p->AddPosX ( 1.f * LOGO_R_VEL_X );
			if ( p->GetPos ().x > 0.f + WINDOW_WIDTH ) { p->SetPosX ( 1.f * WINDOW_WIDTH - LOGO_W * (LOGO_NUM - 1) ); }
		}
		//---------------------------------------------------------------------------
		//カットイン
		m_x_1p += CUTIN_VX;
		if ( m_x_1p > CHARA_1P_POS_X ) { m_x_1p = CHARA_1P_POS_X; }
		m_chara_1p->SetPos ( m_x_1p, CHARA_1P_POS_Y );
		m_chara_light_1p->SetPos ( m_x_1p, CHARA_1P_POS_Y );
		if ( ! m_tmrDecide1p->IsActive () ) { m_chara_light_1p->SetValid ( false ); }

		m_x_2p += - CUTIN_VX;
		if ( m_x_2p < CHARA_2P_POS_X ) { m_x_2p = CHARA_2P_POS_X; }
		m_chara_2p->SetPos ( m_x_2p, CHARA_2P_POS_Y );
		m_chara_light_2p->SetPos ( m_x_2p, CHARA_2P_POS_Y );
		if ( !m_tmrDecide2p->IsActive () ) { m_chara_light_2p->SetValid ( false ); }

		//---------------------------------------------------------------------------

		//終了待機状態はタスク動作のみ
		if ( m_endWait ) 
		{
			Scene::Move ();
			return; 
		}

		//モードで分岐
		switch ( m_mode )
		{
		case MODE_PLAYER_PLAYER:
			Move1p ();
			Move2p ();
			break;
		case MODE_PLAYER_CPU:
			Move1p ();
			break;
		case MODE_CPU_PLAYER:
			Move2p ();
			break;
		case MODE_CPU_CPU:
			Move1p ();
			Move2p ();
			break;
		}


		Scene::Move ();
	}

	void CharaSele::Move1p ()
	{
		//1p移動
		if ( KeyLR1p () )
		{
			//1p未選択
			if ( ! m_bDecide1p ) { Select1p (); }
			//1p決定済み
			else
			{
				switch ( m_mode )	//CPU2Pの選択
				{
				case MODE_PLAYER_PLAYER:				break;
				case MODE_PLAYER_CPU:	SelectCPU2p (); break;
				case MODE_CPU_PLAYER:					break;
				case MODE_CPU_CPU:		SelectCPU2p (); break;
				}
			}
		}

		//1p決定
		if ( PUSH_KEY ( P1_BUTTON1 ) )
		{
			//1p未決定
			if ( !m_bDecide1p )
			{
				Decision1p ();
			}
			//1p決定済み
			else
			{
				switch ( m_mode )			//CPU2Pの決定
				{
				case MODE_PLAYER_PLAYER:					break;
				case MODE_PLAYER_CPU:	DecisionCPU2p ();	break;
				case MODE_CPU_PLAYER:						break;
				case MODE_CPU_CPU:		DecisionCPU2p ();	break;
				}
			}
		}

		//1p解除
		if ( PUSH_KEY ( P1_BUTTON2 ) )
		{
			//1p決定済み
			if ( m_bDecide1p )
			{
				SOUND->Play ( SE_Cancel );
				m_bDecide1p = false;
				m_cursor1p->Start ();
			}
		}
	}

	void CharaSele::Move2p ()
	{
		//2p移動
		if ( KeyLR2p () )
		{
			//2p未選択
			if ( ! m_bDecide2p ) { Select2p (); }
			//2p決定済み
			else
			{
				switch ( m_mode )	//CPU1Pの選択
				{
				case MODE_PLAYER_PLAYER:				break;
				case MODE_PLAYER_CPU:					break;
				case MODE_CPU_PLAYER:	SelectCPU1p (); break;
				case MODE_CPU_CPU:		SelectCPU1p (); break;
				}
			}
		}

		//2p決定
		if ( PUSH_KEY ( P2_BUTTON1 ) )
		{
			//2p未決定
			if ( ! m_bDecide2p )
			{
				Decision2p ();
			}
			//2p決定済み
			else
			{
				switch ( m_mode )			//CPU1Pの決定
				{
				case MODE_PLAYER_PLAYER:					break;
				case MODE_PLAYER_CPU:						break;
				case MODE_CPU_PLAYER:	DecisionCPU1p ();	break;
				case MODE_CPU_CPU:		DecisionCPU1p ();	break;
				}
			}
		}

		//2p解除
		if ( PUSH_KEY ( P2_BUTTON2 ) )
		{
			//2p決定済み
			if ( m_bDecide2p )
			{
				SOUND->Play ( SE_Cancel );
				m_bDecide2p = false;
				m_cursor2p->Start ();
			}
		}
	}

	void CharaSele::Select1p ()
	{
		if ( CHARA_SONIA == m_charaName1p )
		{
			m_cursor1p->SetPos ( SELECT_2P_POS_X, SELECT_2P_POS_Y );
			m_charaName1p = CHARA_ORFLOAT;
			m_chara_1p->SetIndexTexture ( CHARA_ORFLOAT );
			m_chara_light_1p->SetIndexTexture ( CHARA_ORFLOAT );
		}
		else
		{
			m_cursor1p->SetPos ( SELECT_1P_POS_X, SELECT_1P_POS_Y );
			m_charaName1p = CHARA_SONIA;
			m_chara_1p->SetIndexTexture ( CHARA_SONIA );
			m_chara_light_1p->SetIndexTexture ( CHARA_SONIA );
		}
		SOUND->Play ( SE_cursor_move );
		m_x_1p = CHARA_1P_POS_X0;
	}

	void CharaSele::Select2p ()
	{
		if ( CHARA_SONIA == m_charaName2p )
		{
			m_cursor2p->SetPos ( SELECT_2P_POS_X, SELECT_2P_POS_Y );
			m_charaName2p = CHARA_ORFLOAT;
			m_chara_2p->SetIndexTexture ( CHARA_ORFLOAT );
			m_chara_light_2p->SetIndexTexture ( CHARA_ORFLOAT );
		}
		else
		{
			m_cursor2p->SetPos ( SELECT_1P_POS_X, SELECT_1P_POS_Y );
			m_charaName2p = CHARA_SONIA;
			m_chara_2p->SetIndexTexture ( CHARA_SONIA );
			m_chara_light_2p->SetIndexTexture ( CHARA_SONIA );
		}
		SOUND->Play ( SE_cursor_move );
		m_x_2p = CHARA_2P_POS_X0;
	}


	void CharaSele::SelectCPU1p ()
	{
		if ( CHARA_SONIA == m_charaName1p )
		{
			m_cursorCPU1p->SetPos ( SELECT_2P_POS_X, SELECT_2P_POS_Y );
			m_charaName1p = CHARA_ORFLOAT;
			m_chara_1p->SetIndexTexture ( CHARA_ORFLOAT );
		}
		else
		{
			m_cursorCPU1p->SetPos ( SELECT_1P_POS_X, SELECT_1P_POS_Y );
			m_charaName1p = CHARA_SONIA;
			m_chara_1p->SetIndexTexture ( CHARA_SONIA );
		}
		m_x_1p = CHARA_1P_POS_X0;
	}

	void CharaSele::SelectCPU2p ()
	{
		if ( CHARA_SONIA == m_charaName2p )
		{
			m_cursorCPU2p->SetPos ( SELECT_2P_POS_X, SELECT_2P_POS_Y );
			m_charaName2p = CHARA_ORFLOAT;
			m_chara_2p->SetIndexTexture ( CHARA_ORFLOAT );
		}
		else
		{
			m_cursorCPU2p->SetPos ( SELECT_1P_POS_X, SELECT_1P_POS_Y );
			m_charaName2p = CHARA_SONIA;
			m_chara_2p->SetIndexTexture ( CHARA_SONIA );
		}
		m_x_2p = CHARA_2P_POS_X0;
	}

	void CharaSele::Decision1p ()
	{
		m_bDecide1p = true;
		m_cursor1p->Stop ();
		SOUND->Play ( SE_Clang );
		m_chara_light_1p->SetValid ( true );
		m_tmrDecide1p->Start ();
	}

	void CharaSele::Decision2p ()
	{
		m_bDecide2p = true;
		m_cursor2p->Stop ();
		SOUND->Play ( SE_Clang );
		m_chara_light_2p->SetValid ( true );
		m_tmrDecide2p->Start ();
	}

	void CharaSele::DecisionCPU1p ()
	{
		m_bDecide1p = true;
		m_cursor1p->Stop ();
		SOUND->Play ( SE_Clang );
		m_chara_light_1p->SetValid ( true );
		m_tmrDecide1p->Start ();
	}

	void CharaSele::DecisionCPU2p ()
	{
		m_bDecide2p = true;
		m_cursor2p->Stop ();
		SOUND->Play ( SE_Clang );
		m_chara_light_2p->SetValid ( true );
		m_tmrDecide2p->Start ();
	}


	//■================================================================
	P_GameScene CharaSele::Transit ()
	{
		//ESCで戻る
		if ( ::GetAsyncKeyState ( VK_ESCAPE ) & 0x0001 )
		{
			SOUND->Stop ( 0 );
			return make_shared < Title > ();
		}

		//パラメータ
		P_Param pParam = GetpParam ();

		//両者決定でシーンを進める
		if ( m_endWait )
		{
			if ( ! m_fade->IsActive () )
			{
				SOUND->Stop ( 0 );
				//パラメータで分岐
				if ( pParam->GetTraining () )
				{
					return make_shared < Training > ();
				}
				else
				{
					return make_shared < Fighting > ();
				}
			}
		}
		else
		{
			if ( m_bDecide1p && m_bDecide2p )
			{
				//パラメータに記録
				pParam->SetCharaName1p ( m_charaName1p );
				pParam->SetCharaName2p ( m_charaName2p );
				m_fade->SetDarkOut ( 15 );
				m_endWait = true;
			}
		}


		//通常時
		return shared_from_this ();
	}


	bool CharaSele::KeyLR1p ()
	{
		return (PUSH_KEY ( P1_LEFT ) || PUSH_KEY ( P1_RIGHT ));
	}

	bool CharaSele::KeyLR2p ()
	{
		return (PUSH_KEY ( P2_LEFT ) || PUSH_KEY ( P2_RIGHT ));
	}

#endif // 0

}	//namespace GAME

