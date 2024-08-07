//=================================================================================================
//
//	ExeChara アクタ
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "ExCh_Actor.h"

//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{

	ExeChara_Actor::ExeChara_Actor ()
	{
		//ステート
		m_Start = make_shared < CHST_Start > ();
		m_Greeting = make_shared < CHST_Greeting > ();
		m_GetReady = make_shared < CHST_GetReady > ();
		m_Main = make_shared < CHST_Main > ();
		m_Slow_Skip = make_shared < CHST_Slow_Skip > ();
		m_EndWait = make_shared < CHST_EndWait > ();
		m_Down = make_shared < CHST_Down > ();
		m_DownWait = make_shared < CHST_DownWait > ();
		m_WinWait = make_shared < CHST_WinWait > ();
		m_Win = make_shared < CHST_Win > ();
		m_WinEnd = make_shared < CHST_WinEnd > ();
		m_TimeUp = make_shared < CHST_TimeUp > ();

		//ステート監理
		mvp_state.push_back ( m_Start );
		mvp_state.push_back ( m_Greeting );
		mvp_state.push_back ( m_GetReady );
		mvp_state.push_back ( m_Main );
		mvp_state.push_back ( m_Slow_Skip );
		mvp_state.push_back ( m_EndWait );
		mvp_state.push_back ( m_Down );
		mvp_state.push_back ( m_DownWait );
		mvp_state.push_back ( m_WinWait );
		mvp_state.push_back ( m_Win );
		mvp_state.push_back ( m_WinEnd );
		mvp_state.push_back ( m_TimeUp );

		//初期ステート
		mp_state = m_Start;
//		mp_state = m_Main;

		//パラメータ
		mp_param = make_shared < ExeChara_Param > ();
		for ( P_ExeChara_State p : mvp_state )
		{
			p->SetpPrm ( mp_param );
		}
	}

	void ExeChara_Actor::Init ()
	{
//		mp_state = m_Start;
		mp_state = m_Main;

		//test
//		mp_state = m_Slow_Skip;
	}


	//==========================================================
	//状態変更	(Start()を実行する)
	//==========================================================
	void ExeChara_Actor::StartGreeting ()
	{
		mp_state = m_Greeting;
		mp_state->Start ();
	}

	void ExeChara_Actor::StartGetReady ()
	{
		mp_state = m_GetReady;
		mp_state->Start ();
	}

	void ExeChara_Actor::StartFighting ()
	{
		mp_state = m_Main;
		mp_state->Start ();
	}

	void ExeChara_Actor::StartTimeUp ()
	{
		mp_state = m_TimeUp;
		mp_state->Start ();
	}

	void ExeChara_Actor::StartEndWait ()
	{
		mp_state = m_EndWait;
		mp_state->Start ();
	}


	//==========================================================
	//メイン中一時遷移		(Start()を実行しないでメインに戻る)
	//==========================================================
	void ExeChara_Actor::ShiftFighting ()
	{
		mp_state = m_Main;
	}

	void ExeChara_Actor::ShiftSlowSkip ()
	{
		mp_state = m_Slow_Skip;
	}

	//==========================================================
	//MutualCharaから呼ばれる主な関数
	//==========================================================
	void ExeChara_Actor::PreScriptMove ()
	{
		mp_state->PreScriptMove ();
	}

	void ExeChara_Actor::RectMove ()
	{
		mp_state->RectMove ();
	}

	void ExeChara_Actor::PostScriptMove ()
	{
		mp_state->PostScriptMove ();
	}



}	//namespace GAME

