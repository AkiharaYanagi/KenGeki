//=================================================================================================
//
// エグゼキャラ　ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "ExeChara.h"
#include "../GameMain/SoundConst.h"


//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//================================================
	//	判定枠関連 内部関数
	//================================================

	//---------------------------------------------
	//イベント

	void ExeChara::OnDashBranch ()
	{
		m_btlPrm.AddBalance ( -1000 );
		TransitAction_Condition_I ( BRC_DASH, F );	//ダッシュ相殺・自分
	}

	//打合発生
	void ExeChara::OnClang ( UINT nLurch, CLANG_DECISION_WL clangDecision )
	{
#if 0
		//		if ( CD_LOSE == clangDecision )
		{
			//状態の変更
			m_actionID = m_pChara->GetBsAction ( BA_CLANG );
			m_frame = 0;
		}
#endif // 0

		//相手のパラメータで増減
		P_Script scp = m_pOther.lock ()->GetpScript ();
		int balance_e = scp->m_prmBattle.Balance_E;
		m_btlPrm.AddBalance ( balance_e );

		//バランスアウト
		if ( m_btlPrm.GetBalance () <= 0 )
		{
			//			SetAction ( _T ( "Dotty" ) );
			SetAction ( _T ( "立ち" ) );
		}

		//自身の状態変更
		m_btlPrm.SetClang ( true );		//打合状態
		//		m_lurch = nLurch;		//のけぞり時間の設定
		m_btlPrm.GetTmr_HitStop ()->Start ();		//ヒットストップの設定
	}


	//相手・攻撃 → 自分・くらい
	//くらい状態・ダメージ処理
#if 0
	void ExeChara::OnDamaged ( int damage )
	{
		bool hit = true;
		bool guard = false;

		//回避判定
#if 0
		//攻撃中でなく、下要素が入力されているとき
		//ダッシュ中、よろけ中なども除外する (歩きは可能)
		if ( !IsAttacking () && !( GetActionName () == Chara::DOTTY ) )
		{
			if ( IsInput2E () )
			{
				//避けに移項
				TransitAction ( m_pChara->GetBsAction ( Chara::AVOID ) );

				//ヒットストップの設定
				m_hitstop = 15;

				//エフェクトの発生
				//				float dispGameBaseX = GetDispGameBaseX ();	//画面端による表示基準位置
				m_dispChara.OnAvoid ( m_ptChara, m_dirRight );

				//SE
				SoundArchiver::instance ()->Play ( 2 );

				//自分のバランス減少・追加分
				int balanceDamage0 = 200 - damage;		//ダメージの逆数(L>M>H)
				if ( m_balance < balanceDamage0 ) { balanceDamage0 = m_balance; }	//現在値以上は表示制限
				m_balance -= balanceDamage0;

				//相手のバランス減少
				int balanceDamage = damage;		//ダメージ分バランス減少(L<M<H)
				int balance = m_pOther->GetBalance ();
				if ( balance < balanceDamage ) { balanceDamage = balance; }	//現在値以上は表示制限
				m_pOther->AddbBalance ( -1 * balanceDamage );

				return;
			}
		}
#endif // 0

		//ガード発生
#if 0
		//攻撃中でないとき
		//ダッシュ中、よろけ中なども除外する (歩きは可能)
		if ( !IsAttacking () && !IsDamaged () )
		{
			//上中下段　分岐処理
			ACTION_POSTURE ap = m_pOther.lock ()->GetPosture ();

			//後方向が入力されているとき
			if ( m_pCharaInput->IsInput4 () )
			{
				//相手の状態で分岐
				switch ( ap )
				{
				case AP_STAND:	hit = false; guard = true; break;
				case AP_CROUCH: hit = true; guard = false; break;
				case AP_JUMP:	hit = false; guard = true; break;
				}
			}

			//後下方向が入力されているとき
			if ( m_pCharaInput->IsInput1 () )
			{
				//相手の状態で分岐
				switch ( ap )
				{
				case AP_STAND:	hit = false; guard = true; break;
				case AP_CROUCH: hit = false; guard = true; break;
				case AP_JUMP:	hit = true; guard = false; break;
				}
			}

		}

		//--------------------------------------------------------

		//ガード成立
		if ( guard )
		{
			tstring act;
			switch ( m_pAction->GetPosture () )
			{
			case AP_STAND:	act.assign ( _T ( "S_Guard" ) ); break;
			case AP_CROUCH: act.assign ( _T ( "C_Guard" ) ); break;
			case AP_JUMP:	act.assign ( _T ( "J_Guard" ) ); break;
			}
			TransitAction ( m_pChara->GetActionID ( act ) );

			m_FirstEf = true;				//初回のみエフェクト発生
			m_tmrHitstop->Start ();				//ヒットストップの設定

			//SE
			SOUND->Play ( SE_Guard );
		}
#endif // 0

		//くらい時 ( ガードをしていない ) && ( 強制変更されていない )
		if ( hit && ! m_btlPrm.GetForcedChange () )
		{
			int lf = m_btlPrm.GetLife ();
			//ダメージをライフによって補正(根性値)
			if ( lf < LIFE_MAX * 0.5f )
			{
				damage = (int)( damage * ( 0.001f * ( 0.5f * LIFE_MAX + lf ) ) );
			}

			//ダメージ処理
			if ( lf < damage ) { m_btlPrm.SetDamage ( lf ); }	//ライフ以上は表示制限
			else { m_btlPrm.SetDamage ( damage ); }		//表示用

			m_btlPrm.SetLife ( lf - damage );

			//状態の変更
#if 0
			tstring act;
			switch ( m_pAction->GetPosture () )
			{
			case AP_STAND:	act.assign ( _T ( "Damaged_L" ) ); break;
			case AP_CROUCH: act.assign ( _T ( "C_DamagedL" ) ); break;
			case AP_JUMP:	act.assign ( _T ( "J_DamagedL" ) ); break;
			}
			TransitAction ( m_pChara->GetActionID ( act ) );
#endif // 0

			//その他　効果
			m_btlPrm.GetTmr_HitStop ()->Start ();			//ヒットストップの設定
			m_btlPrm.SetFirstEf ( true );			//初回のみエフェクト発生
			m_btlPrm.SetFirstSE ( true );			//初回のみSE発生

		}
	}
#endif // 0


	//相手・攻撃 → 自分・くらい
	//くらい状態・ダメージ処理
	void ExeChara::OnDamaged ()
	{
		//相手
		P_Script pScp = m_pOther.lock ()->m_pScript;

		//-------------------------------------------------
		//ダメージ処理
		int damage = pScp->m_prmBattle.Power;

#if 0
		//ダメージをライフによって補正(根性値)
		int lf = m_btlPrm.GetLife ();
		if ( lf < LIFE_MAX * 0.5f )
		{
			damage = (int)( damage * ( 0.001f * ( 0.5f * LIFE_MAX + lf ) ) );
		}
#endif // 0

#if 0
		if ( lf < damage ) { m_btlPrm.SetDamage ( lf ); }	//ライフ以上は表示制限
		else { m_btlPrm.SetDamage ( damage ); }		//表示用
		m_btlPrm.SetLife ( lf - damage );
#endif // 0
		m_btlPrm.AddLife ( - damage );

		//-------------------------------------------------
		//バランス処理
		int b_e = pScp->m_prmBattle.Balance_E;
		int bl = m_btlPrm.GetBalance ();
		m_btlPrm.SetBalance ( bl - b_e );

		//-------------------------------------------------
		//その他　効果
		m_btlPrm.GetTmr_HitStop ()->Start ();			//ヒットストップの設定
		m_btlPrm.SetFirstEf ( true );			//初回のみエフェクト発生
		m_btlPrm.SetFirstSE ( true );			//初回のみSE発生
	}


	//自分・攻撃 -> 相手・くらい
	//ヒット発生(攻撃成立側)
	void ExeChara::OnHit ()
	{
		//-----------------------------------------------------
		//条件分岐 (相手→自分でないとスクリプトが変わってしまう)
		TransitAction_Condition_E ( BRC_THR_E, T );	//投げ・相手
		TransitAction_Condition_I ( BRC_THR_I, F );	//投げ・自分
		TransitAction_Condition_E ( BRC_HIT_E, T );	//ヒット・相手
		TransitAction_Condition_I ( BRC_HIT_I, F );	//ヒット・自分

		//-----------------------------------------------------

		//m_btlPrm.SetHitEst ( true );		//攻撃成立フラグ
		//m_btlPrm.GetTmr_HitStop ()->Start ();		//ヒットストップの設定
		m_btlPrm.OnHit ();
	}

	//エフェクトヒット発生(攻撃成立側)
	void ExeChara::OnEfHit ()
	{
		m_btlPrm.SetHitEst ( true );		//攻撃成立フラグ
		//		m_tmrHitstop->Start ();		//エフェクトはヒットストップしない

		m_btlPrm.GetTmr_HitPitch ()->Start ();
	}

#if 0
	//終了演出
	void ExeChara::OnEndAct ()
	{
		m_charaState = CHST_WIN_WAIT;
	}

	//強制終了状態
	void ExeChara::ForcedEnd ()
	{
		if ( m_btlPrm.GetLife () <= 0 )
		{
			m_charaState = CHST_DOWN_END;
			//			m_actionID = m_pChara->GetBsAction ( BA_DOWN );
			TransitAction ( m_actionID );
		}
		else
		{
			m_charaState = CHST_WIN_END;
			//			m_actionID = m_pChara->GetBsAction ( BA_WIN );
			TransitAction ( m_actionID );
		}
	}
#endif // 0


}	//namespace GAME

