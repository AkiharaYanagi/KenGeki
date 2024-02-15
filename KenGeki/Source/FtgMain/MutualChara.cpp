//=================================================================================================
//
//	MutualChara ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "MutualChara.h"
#include "../GameMain/SoundConst.h"


//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	MutualChara::MutualChara ()
		: m_winner ( WINNER::WINNER_DRAW )
	{
		m_decision = make_shared < Decision > ();

		//キャラ
		m_exeChara1 = make_shared < ExeChara > ( PLAYER_ID_1 );
		m_exeChara2 = make_shared < ExeChara > ( PLAYER_ID_2 );

		m_exeChara1->SetpOther ( m_exeChara2 );
		m_exeChara2->SetpOther ( m_exeChara1 );

		m_exeChara1->SetpParticle ( m_decision->GetpEfPart () );
		m_exeChara2->SetpParticle ( m_decision->GetpEfPart () );

		AddpTask ( m_exeChara1 );
		AddpTask ( m_exeChara2 );

		//判定
		m_decision->SetpChara (m_exeChara1, m_exeChara2);
		AddpTask ( m_decision );

		//ヒットストップタイマー
		m_tmrHitstop = make_shared < Timer > ();
		m_tmrHitstop->SetTargetTime ( HITSTOP_TIME );
		AddpTask ( m_tmrHitstop );
		m_decision->SetpHitStop ( m_tmrHitstop );

		//デバッグ用枠表示
#define DISP_RECT	1
#if DISP_RECT
		m_exeChara1->OnDispRect ();
		m_exeChara2->OnDispRect ();
#else
		m_exeChara1->OffDispRect ();
		m_exeChara2->OffDispRect ();
#endif // DISP_RECT

	}

	MutualChara::~MutualChara ()
	{
	}

	void MutualChara::ParamInit ( P_Param pParam )
	{
		m_pParam = pParam;

		m_exeChara1->ParamInit ( pParam );
		m_exeChara2->ParamInit ( pParam );
	}

	void MutualChara::Init ()
	{
		m_pParam->SetN_Act1p ( 0 );
		m_pParam->SetN_Act2p ( 0 );

		TASK_VEC::Init ();
	}



	//■#########################################################
	//■
	//■	スクリプトの毎フレーム処理
	//■
	//■#########################################################
	void MutualChara::Conduct ()
	{
		//---------------------------------------------------
		//システム変更
		SwitchRect ();	//枠表示切替
		SwithcCPU ();	//CPU操作切替
		ResetMatch ();	//試合初期化
		//---------------------------------------------------

		//◆スクリプト前処理(入力、移動など)
		m_exeChara1->PreScriptMove ();
		m_exeChara2->PreScriptMove ();

		//◆相互判定(ぶつかり枠)
		Collision ();

		//◆ぶつかり後、攻撃・ヒット判定枠を設定
		m_exeChara1->RectMove ();
		m_exeChara2->RectMove ();

		//◆相互判定(攻撃・ヒット枠)
		_Decision ();

		//◆スクリプト後処理(グラフィック位置など)
		m_exeChara1->PostScriptMove ();
		m_exeChara2->PostScriptMove ();

		//グラフィック共通
		Grp ();

		//シーン共通パラメータ記録
		m_pParam->SetN_Life1p ( m_exeChara1->GetLife () );
		m_pParam->SetN_Life2p ( m_exeChara2->GetLife () );
		m_pParam->SetN_Act1p ( m_exeChara1->GetBtlParam ().GetNActTrs () );
		m_pParam->SetN_Act2p ( m_exeChara2->GetBtlParam ().GetNActTrs () );
	}
	//■#########################################################



	//◆================================
	//◆		相互判定(ぶつかり枠)
	//◆================================
	void MutualChara::Collision ()
	{
		//両者の接触枠を取得
		P_CharaRect pCharaRect1p = m_exeChara1->GetpCharaRect ();
		P_CharaRect pCharaRect2p = m_exeChara2->GetpCharaRect ();
		PV_RECT pvRect1p = pCharaRect1p->GetpvCRect ();
		PV_RECT pvRect2p = pCharaRect2p->GetpvCRect ();

		//接触判定
		//重なっているとき
		if ( OverlapAryRect ( pvRect1p, pvRect2p ) )
		{
			bool dir1 = m_exeChara1->GetDirRight ();
			bool dir2 = m_exeChara2->GetDirRight ();
			VEC2 pos1_0 = m_exeChara1->GetPos ();
			VEC2 pos2_0 = m_exeChara2->GetPos ();

			m_exeChara1->BackPtX ();	//互いにx方向のみ位置を戻す
			m_exeChara2->BackPtX ();

			VEC2 pos1_1 = m_exeChara1->GetPos ();
			VEC2 pos2_1 = m_exeChara2->GetPos ();

			//さらに重なっているとき
			if ( OverlapAryRect ( pvRect1p, pvRect2p ) )
			{
				//位置をさらに戻して(動作した後の位置)から補正する
				if ( LeaveDir ( dir1, pos1_0.x, pos1_1.x ) )
				{
					m_exeChara1->SetPos ( pos1_0 );
				}
				if ( LeaveDir ( dir2, pos2_0.x, pos2_1.x ) )
				{
					m_exeChara2->SetPos ( pos2_0 );
				}
			}


			int i = 0;
			while ( OverlapAryRect ( pvRect1p, pvRect2p ) )
			{
#if 0
#endif // 0
				m_exeChara1->BackMoveX ();	//重なり微調整
				m_exeChara2->BackMoveX ();

				m_exeChara1->SetCollisionRect ();	//当り枠再設定
				m_exeChara2->SetCollisionRect ();

				pvRect1p = pCharaRect1p->GetpvCRect ();
				pvRect2p = pCharaRect2p->GetpvCRect ();

				if ( ++ i > 10 ) { break; }
			}
		}

		//振向
		m_exeChara1->LookOther ();
		m_exeChara2->LookOther ();

		//距離制限(画面端同士)
		float p1x = m_exeChara1->GetPos ().x;
		float p2x = m_exeChara2->GetPos ().x;
		
		// P1 << P2
		if ( p2x - p1x > GAME_WINDOW_WIDTH - FIELD_EDGE * 2 )
		{
			m_exeChara1->BackPtX ();	//互いに位置を戻す
			m_exeChara2->BackPtX ();
		}
		// P2 << P1
		else if ( p1x - p2x > GAME_WINDOW_WIDTH - FIELD_EDGE * 2 )
		{
			m_exeChara1->BackPtX ();	//互いに位置を戻す
			m_exeChara2->BackPtX ();
		}
	}

	bool MutualChara::LeaveDir ( bool dirRight, float pos0, float pos1 )
	{
		//離れる方向のとき
		bool leave = F;
		if ( dirRight )	//右向き左移動
		{
			if ( pos1 < pos0 ) leave = T;
		}
		else//左向き右移動
		{
			if ( pos0 < pos1 ) leave = T;
		}
		return leave;
	}

	//◆================================
	//◆		相互判定 (攻撃・ヒット枠)
	//◆================================
	void MutualChara::_Decision ()
	{
		m_decision->Do ();
	}

	//◆================================
	//◆		共通グラフィック
	//◆================================
	void MutualChara::Grp ()
	{
		//---------------------------------------------------
		//暗転
		UINT bo1 = m_exeChara1->GetBlackOut ();
		UINT bo2 = m_exeChara2->GetBlackOut ();

		//どちらかが発生したとき
		if ( 0 < bo1 || 0 < bo2 )
		{
			SOUND->Play_SE ( SE_Sys_EnterFighting );

			//大きい方で上書
			m_blackOut = ( bo2 < bo1 ) ? bo1 : bo2;
			m_exeChara1->SetBlackOut ( 0 );
			m_exeChara2->SetBlackOut ( 0 );
		}

		//---------------------------------------------------
		//スクリプトからの停止
		UINT scpStop1P = m_exeChara1->GetScpStop ();
		UINT scpStop2P = m_exeChara2->GetScpStop ();
		if ( 0 < scpStop1P )
		{
			m_scpStop = scpStop1P;
			m_exeChara1->SetScpStop ( 0 );

			m_exeChara1->SetStopTimer ( m_scpStop );
			m_exeChara2->SetStopTimer ( m_scpStop );
		}

		//---------------------------------------------------
		//画面表示の基準位置を決定
		VEC2 pos1p = m_exeChara1->GetPos ();
		VEC2 pos2p = m_exeChara2->GetPos ();
		G_Ftg::inst ()->CulcPosMutualBase ( pos1p, pos2p );
	}



	//------------------------------------------------------
	//	終了判定
	//------------------------------------------------------
	bool MutualChara::CheckZeroLife ()
	{
		//終了判定
		bool finish1p = m_exeChara1->IsZeroLife ();
		bool finish2p = m_exeChara2->IsZeroLife ();

		//どちらか、または両方ライフ０なら終了
		if ( finish1p || finish2p )
		{
			PLAYER_ID plr = _PLAYER_NUM;
			if ( finish1p && finish2p )
			{
				plr = _PLAYER_NUM;
			}
			else if ( ! finish1p && finish2p )
			{
				plr = PLAYER_ID_1;
			}
			else if ( finish1p && ! finish2p )
			{
				plr = PLAYER_ID_2;
			}
			//シーン共通パラメータ記録
			m_pParam->SetWinner ( plr );
			return T;
		}

		return F;
	}

#if 0
	bool MutualChara::CheckDown ()
	{
		//ダウン判定
		bool down1p = m_exeChara1->IsDown ();
		bool down2p = m_exeChara2->IsDown ();
		return (down1p || down2p);
	}

	bool MutualChara::CheckDownEnd ()
	{
		//ダウン判定
		bool downEnd1p = m_exeChara1->IsDownEnd ();
		bool downEnd2p = m_exeChara2->IsDownEnd ();
		return (downEnd1p || downEnd2p);
	}

	bool MutualChara::CheckWin ()
	{
		//勝利状態判定
		bool win1p = m_exeChara1->IsWin ();
		bool win2p = m_exeChara2->IsWin ();
		return (win1p || win2p);
	}

	bool MutualChara::CheckWinEnd ()
	{
		//勝利状態判定
		bool winEnd1p = m_exeChara1->IsWinEnd ();
		bool winEnd2p = m_exeChara2->IsWinEnd ();
		return (winEnd1p || winEnd2p);
	}

	bool MutualChara::CheckWinner ()
	{
		bool bRet = false;

		//ライフで勝利者を決定する
		int life1p = m_exeChara1->GetLife ();
		int life2p = m_exeChara2->GetLife ();

		//Double K.O.
		if ( life1p <= 0 && life2p <= 0 ) { m_winner = WINNER_DP; }
		//2P Win
		else if ( life1p <= 0 ) { m_winner = WINNER_2P; }
		//1P Win
		else if ( life2p <= 0 ) { m_winner = WINNER_1P; }
		//Draw
		else if ( life1p == life2p ) { m_winner = WINNER_DRAW; }

		switch ( m_winner )
		{
		case WINNER_1P: 
			m_exeChara1->SetCharaState ( CHST_WIN_END );
			m_exeChara2->SetCharaState ( CHST_DOWN_END );
			break;
		case WINNER_2P: 
			m_exeChara1->SetCharaState ( CHST_DOWN_END );
			m_exeChara2->SetCharaState ( CHST_WIN_END );
			break;
		case WINNER_DP: break;
		default: break;
		}

		return bRet;
	}

	void MutualChara::ForcedEnd ()
	{
		m_exeChara1->ForcedEnd ();
		m_exeChara2->ForcedEnd ();
	}
#endif // 0


	CHARA_NAME MutualChara::GetWinnerName () const
	{
		switch ( m_winner )
		{
		case WINNER_1P: return m_exeChara1->GetCharaName (); break;
		case WINNER_2P: return m_exeChara2->GetCharaName (); break;
		case WINNER_DP: break;
		default: break;
		}
		return CHARA_NAME ();
	}


	//------------------------------------------------------
	//	内部関数
	//------------------------------------------------------

	//------------------------------------------------------
	//枠表示切替 
	//@info ExeCharaで呼ぶと1P2Pで２回呼ばれてしまう
	void MutualChara::SwitchRect ()
	{
		static bool bDispRect = false;		//状態
		static bool pre_bDispRect = false;	//前回押しているか
		static bool is_bDispRect = false;	//今回押しているか
		
		
//		is_bDispRect = ( ::GetAsyncKeyState ( '1' ) & 0x0001 );
		is_bDispRect = ( WND_UTL::AscKey ( '1' ) );

		//@info キーボード入力は押しっぱなしで一定時間後連打状態になる
		//TRACE_F ( _T ( "b = %d, pre = %d, is = %d\n" ), bDispRect ? 1 : 0, pre_bDispRect ? 1 : 0, is_bDispRect ? 1 : 0  );
		
		//今回押した瞬間ならば、1回のみ切替
		if ( ! pre_bDispRect && is_bDispRect )	// false -> true
		{
			if ( ! bDispRect )
			{
				m_exeChara1->OnDispRect ();
				m_exeChara2->OnDispRect ();
				bDispRect = true;
			}
			else
			{
				m_exeChara1->OffDispRect ();
				m_exeChara2->OffDispRect ();
				bDispRect = false;
			}
		}

		pre_bDispRect = is_bDispRect;
	}

	//------------------------------------------------------
	//CPU操作切替
	void MutualChara::SwithcCPU ()
	{
		static bool cpu1 = F;
		if ( WND_UTL::AscKey ( '6' ) )
		{
			cpu1 ^= T;
			if ( cpu1 )
			{
				m_exeChara1->ControlCPU ();
			}
			else
			{
				m_exeChara1->ControlPlayer ();
			}
		}

		static bool cpu2 = F;
		if ( WND_UTL::AscKey ( '7' ) )
		{
			cpu2 ^= T;
			if ( cpu2 )
			{
				m_exeChara2->ControlCPU ();
			}
			else
			{
				m_exeChara2->ControlPlayer ();
			}
		}
	}

	void MutualChara::Set_1P_vs_2P ()
	{
		m_exeChara1->ControlPlayer ();
		m_exeChara2->ControlPlayer ();
	}

	void MutualChara::Set_1P_vs_CPU ()
	{
		m_exeChara1->ControlPlayer ();
		m_exeChara2->ControlCPU ();
	}

	void MutualChara::Set_CPU_vs_CPU ()
	{
		m_exeChara1->ControlCPU ();
		m_exeChara2->ControlCPU ();
	}


	//------------------------------------------------------

	//試合初期化
	void MutualChara::ResetMatch ()
	{
		if ( ::GetAsyncKeyState ( '0' ) & 0x0001 )
		{
			m_exeChara1->Init ();
			m_exeChara2->Init ();
		}
	}

	void MutualChara::TrainingInit ()
	{
		m_exeChara1->TrainingInit ();
		m_exeChara2->TrainingInit ();
	}

	//------------------------------------------------------
	//	ExeChara両者操作
	//------------------------------------------------------
	//開始デモ
	void MutualChara::StartGreeting ()
	{
		m_exeChara1->StartGreeting ();
		m_exeChara2->StartGreeting ();
	}

	//開始準備
	void MutualChara::StartGetReady ()
	{
		m_exeChara1->StartGetReady ();
		m_exeChara2->StartGetReady ();
	}

	//戦闘開始
	void MutualChara::StartFighting ()
	{
		m_exeChara1->StartFighting ();
		m_exeChara2->StartFighting ();
	}


#if 0
	void MutualChara::SetReady ()
	{
		//準備
		m_exeChara1->SetCharaState ( CHST_START );
		m_exeChara2->SetCharaState ( CHST_START );
	}
	void MutualChara::SetMain ()
	{
		//開始
		m_exeChara1->SetMain ();
		m_exeChara2->SetMain ();
	}
	void MutualChara::Wait ( bool b )
	{
		//入力停止
		m_exeChara1->SetWait ( b );
		m_exeChara2->SetWait ( b );
	}
	void MutualChara::Stop ( bool b )
	{
		//一時停止
		m_exeChara1->SetStop ( b );
		m_exeChara2->SetStop ( b );
	}
	void MutualChara::SetEndWait ()
	{
		//終了待機
		m_exeChara1->SetEndWait ();
		m_exeChara2->SetEndWait ();
	}
	void MutualChara::EndAct ()
	{
		//終了
		m_exeChara1->OnEndAct ();
		m_exeChara2->OnEndAct ();
	}
	void MutualChara::SetCharaState ( CHARA_STATE chst )
	{
		//状態の設定
		m_exeChara1->SetCharaState ( chst );
		m_exeChara2->SetCharaState ( chst );
	}
#endif // 0

}	//namespace GAME

