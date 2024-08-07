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
	//	外部からの状況起因関数
	//================================================

	//---------------------------------------------
	//イベント

	//ダッシュ相殺
	void ExeChara::OnDashBranch ()
	{
		m_btlPrm.AddBalance ( -1000 );
		TransitAction_Condition_I ( BRC_DASH, F );	//ダッシュ相殺・自分
	}

	//----------------------------------------------
	//打合発生//自分：Attack, 相手：Offset
	void ExeChara::OnClang_AO ()
	{

		//パラメータ
		m_btlPrm.OnClang_AO ();
	}

	//打合発生	//自分：Offset, 相手：Attack
	void ExeChara::OnClang_OA ()
	{

		//パラメータ
		m_btlPrm.OnClang_OA ();
	}

	//打合発生	//自分：Attack, 相手：Attack
	void ExeChara::OnClang_AA ()
	{
		//相手のパラメータで増減
		P_Script scp = m_pOther.lock ()->GetpScript ();
		int balance_e = scp->m_prmBattle.Balance_E;
		m_btlPrm.AddBalance ( balance_e );

#if 0
		//バランスアウト
		if ( m_btlPrm.GetBalance () <= 0 )
		{
			//			SetAction ( _T ( "Dotty" ) );
			SetAction ( _T ( "立ち" ) );
		}
#endif // 0

		//パラメータ
		m_btlPrm.OnClang_AA ();
	}
	//----------------------------------------------


	//自分・攻撃 -> 相手・くらい
	//ヒット発生(攻撃成立側)
	void ExeChara::OnHit ()
	{
		//-----------------------------------------------------
		//避けを直前成立させるとスロウ
		if ( m_pOther.lock()->IsNameAction ( _T ( "避け" ) ) )
		{
			if ( m_pOther.lock()->m_frame < 8 )
			{
				m_pFtgGrp->StartSlow ();

				//相手のアクション変更
				m_pOther.lock()->SetAction ( _T("避け直前成立") );

				//自身のスロウ開始
				m_actor.ShiftSlowSkip ();
				m_tmrSlow.SetValid ( T );

				//以降の処理(スクリプト変更など)を飛ばす
				return;
			}
		}

		//-----------------------------------------------------
		//条件分岐 (相手→自分でないとスクリプトが変わってしまう)

		//投げのとき、相手の状態によっては移項しない　（投げ不能状態）
		TransitAction_Condition_E ( BRC_THR_E, T );	//投げ・相手
		TransitAction_Condition_I ( BRC_THR_I, F );	//投げ・自分

		TransitAction_Condition_E ( BRC_HIT_E, T );	//ヒット・相手
		TransitAction_Condition_I ( BRC_HIT_I, F );	//ヒット・自分

		//-------------------------------------------------
		//ノックバック処理		// 値は (float) = (int)1/10
		float recoil_i = 0.1f * m_pScript->m_prmBattle.Recoil_I;
		m_btlPrm.SetAccRecoil ( recoil_i );

		//-----------------------------------------------------
		//ゲージ増減
		int p = m_pScript->m_prmBattle.Power;
		m_btlPrm.AddMana ( p );

		//-----------------------------------------------------
		//パラメータ
		m_btlPrm.OnHit ();
	}


	//投げられ判定
	bool ExeChara::CanBeThrown () const 
	{
		P_Action pAct = m_pAction;
#if 0
		//指定アクションのとき 「可能」
		if ( pAct->IsName ( _T("ダメージ小") ) ) { return T; }
		if ( pAct->IsName ( _T("ダメージ大") ) ) { return T; }
#endif // 0

		//指定アクションのとき　「不可」
		if ( pAct->IsName ( _T("ガード") ) ) { return F; }
		if ( pAct->IsName ( _T("投げやられ") ) ) { return F; }


		//アクション態勢が"AP_JUMP"は「不可」
		ACTION_POSTURE ap = GetPosture ();
		if ( ap == ACTION_POSTURE::AP_JUMP ) { return F; }

		//アクション属性が"AC_DAMAGED"は「不可」
		ACTION_CATEGORY ac = ActCtg ();
		if ( ac == ACTION_CATEGORY::AC_DAMAGED ) { return F; }

		//それ以外は「可能」
		return T;
	}

	//投げ判定
	bool ExeChara::IsThrowAction () const
	{
		//状態遷移に「投げ」を条件に持つものがあるとき true を返す
		UINT i = Check_TransitAction_Condition ( BRC_THR_I );
		UINT e = Check_TransitAction_Condition ( BRC_THR_E );

		return ( i != NO_COMPLETE ) || ( e != NO_COMPLETE );
	}
	

	//相殺しない判定
	bool ExeChara::IsNotOffset () const
	{
		//投げ
		if ( IsThrowAction () ) { return T; }
#if 0
		//超必殺
		P_Action pAct = m_pAction;
		if ( pAct->IsName ( _T("超必殺発動") ) ) { return T; }
		if ( pAct->IsName ( _T("超必B発動") ) ) { return T; }
#endif // 0

		return F;
	}


	//ガードできる状態かどうか
	bool ExeChara::CanGaurd () const
	{
		if ( IsStand () ) { return T; }
		return F;
	}


	//エフェクトヒット発生(攻撃成立側)
	void ExeChara::OnEfHit ()
	{
		m_btlPrm.SetHitEst ( true );		//攻撃成立フラグ
//		m_tmrHitstop->Start ();		//エフェクトはヒットストップしない
		m_btlPrm.GetTmr_HitPitch ()->Start ();
	}

	//相手・攻撃 → 自分・くらい
	//くらい状態・ダメージ処理
#if 0
	void ExeChara::OnDamaged ( int damage )
	{
		bool hit = true;
		bool guard = false;
#endif
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
#if 0
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
		P_Script pScpOther = m_pOther.lock ()->m_pScript;

		//-------------------------------------------------
		//ガード判定
		

#if 0
		//test
		//強制すべてガード状態
		if ( IsPlayerID ( PLAYER_ID_2 ) )
		{
			SetAction ( _T("ガード弱") );

			if ( m_pOther.lock()->IsNameAction ( _T("万雷中派生") ) )
			{
				m_btlPrm.SetAccRecoil_e ( 100 );
			}
			else 
			{
				m_btlPrm.SetAccRecoil_e ( 10 );
			}

			return;
		}
#endif // 0
		

		//レバー1(後下)入力されているとき
		if ( m_pCharaInput->IsLvr1 () )
		{
			//アクション変更
			SetAction ( _T("しゃがみガード") );
			m_btlPrm.SetAccRecoil_e ( 10 );
			return;
		}
		 
		//後方向が入力されているとき
		if ( m_pCharaInput->IsLvr4 () )
		{
			//----------------------------
			//OnHit()
			//OnDamaaged()
			// の順番なので後で変更した方が優先
			//----------------------------

			//アクション変更

			if ( m_pOther.lock()->IsNameAction ( _T("大攻撃") ) )
			{
				SetAction ( _T("ガード強") );
			}
			else if ( m_pOther.lock()->IsNameAction ( _T("中攻撃") ) )
			{
				SetAction ( _T("ガード中") );
			}
			else
			{
				SetAction ( _T("ガード弱") );
			}
			
			
			
			
			m_btlPrm.SetAccRecoil_e ( 10 );
			return;
		}

		//-------------------------------------------------
		//ダメージ処理
		int damage = pScpOther->m_prmBattle.Power;
//		m_btlPrm.AddLife ( - damage );
		m_btlPrm.OnDamage ( - damage );

		//-------------------------------------------------
		//ノックバック処理
		float recoil_i = 0.1f * pScpOther->m_prmBattle.Recoil_I;		// 値は (float) = (int)1/10
//		m_btlPrm.SetAccRecoil ( recoil_i );

		//-------------------------------------------------
		//バランス処理
		int b_e = pScpOther->m_prmBattle.Balance_E;
		int bl = m_btlPrm.GetBalance ();
		m_btlPrm.SetBalance ( bl - b_e );

		//-------------------------------------------------
		//その他　効果
		m_btlPrm.GetTmr_HitStop ()->Start ();			//ヒットストップの設定
//		m_btlPrm.SetFirstEf ( true );			//初回のみエフェクト発生
		m_btlPrm.SetFirstSE ( true );			//初回のみSE発生
	}

	//相手ダメージ処理の後
	void ExeChara::OnDamaged_After ()
	{
		//-----------------------------------------------------
		//特殊アクションカテゴリ指定
		if ( IsSpecial () || IsOverdrive () )
		{
			//必殺・超必殺時に相手の白ダメージ確定
			m_pOther.lock()->m_btlPrm.DecisionWhiteDamage ();
		}

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

