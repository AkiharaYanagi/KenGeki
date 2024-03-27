//=================================================================================================
//
// エグゼキャラ　ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "ExeChara.h"


//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//================================================
	//	アクション遷移
	//================================================
	// 
	//アクション移項（コマンドに関する処理）
	bool ExeChara::TranditAction_Command ()
	{
		//-----------------------------------------------------
		// コマンドによる分岐
		//コマンドが完成したIDを優先順に保存したリスト
		m_pCharaInput->MakeTransitIDList ( *m_pChara, m_pScript, m_btlPrm.GetDirRight () );
		const std::vector < UINT > & vCompID = m_pCharaInput->GetvCompID ();

		UINT transitID = NO_COMPLETE;
		for ( UINT id : vCompID )
		{
			//遷移先チェック
			P_Action pAct = m_pChara->GetpAction ( id );

			//------------------------------------------
			//対象IDがバランス消費で移項可能なら移動処理へ

			//空中ダッシュ回数による遷移不可処理
			if ( pAct->IsName ( _T ( "AirDash" ) ) )
			{
				UINT n = m_btlPrm.GetNAirDash ();
				if ( 0 < n )
				{ continue; }
			}

			//超必　特定処理
			if ( pAct->IsName ( _T ( "超必殺発動" ) ) || pAct->IsName ( _T ( "超必B発動" ) ) )
			{
				//マナ消費
				if ( m_btlPrm.GetMana () >= MANA_HALF )
				{
					m_btlPrm.AddMana ( - MANA_MAX / 2 );
				}
				else //足りないとき遷移しない
				{
					continue;
				}
			}

			//不可能なら次をチェック
			//------------------------------------------

			//可能なら遷移先に指定して終了
			transitID = id;
			break;
		}

		//コマンドが完成していたら
		if ( NO_COMPLETE != transitID )
		{
			//現在アクションとして最後の処理
			EndAction ();

			//遷移先チェック
			P_Action pact = m_pChara->GetpAction ( transitID );
			P_Script pscr = pact->GetpScript ( 0 );


			//バランス処理
			int bl_a = pact->GetBalance ();		//アクション消費バランス
			int bl_p = m_btlPrm.GetBalance ();	//パラメータ現在バランス

			int d = bl_p - bl_a;
			if ( d < 0 ) { d = 0; }
			m_btlPrm.SetBalance ( d );


			//アクション遷移
			m_actionID = transitID;

			//スクリプト処理
			ExeScript ();

			//アクション移項を計測
			m_btlPrm.AddNActTrs ( 1 );

			//次フレームのスクリプトを１つ進める
			//今回フレームは取得済みのm_pScriptを用いる
			++ m_frame;

			//終了
			return T;
		}

		return F;
	}


	//アクションの移項(直接指定)
	void ExeChara::SetAction ( UINT action_id )
	{
		m_actionID = action_id;
		m_pAction = m_pChara->GetpAction ( m_actionID );
		m_frame = 0;
		m_pScript = m_pAction->GetpScript ( m_frame );

		//スクリプトからのパラメータ反映
		SetParamFromScript ();
	}

	void ExeChara::SetAction ( tstring action_name )
	{
		SetAction ( m_pChara->GetActionID ( action_name ) );
	}


	//アクション移行(自身)
	void  ExeChara::TransitAction_Condition_I ( BRANCH_CONDITION CONDITION, bool forced )
	{
		//ヒット・相手
		UINT indexAction = Check_TransitAction_Condition ( CONDITION );
		if ( NO_COMPLETE != indexAction )
		{
			//遷移先チェック
			P_Action pAct = m_pChara->GetpAction ( indexAction );
			P_Script pScr = pAct->GetpScript ( 0 );

			//自身を変更
			SetAction ( indexAction );	//遷移
			m_btlPrm.SetForcedChange ( forced );
		}
	}

	//アクション移行(相手)
	void  ExeChara::TransitAction_Condition_E ( BRANCH_CONDITION CONDITION, bool forced )
	{
		//ヒット・相手
		UINT indexAction = Check_TransitAction_Condition ( CONDITION );
		if ( NO_COMPLETE != indexAction )
		{
			//遷移先チェック
			P_Action pAct = m_pChara->GetpAction ( indexAction );

			//やられ状態のとき空中チェック
			bool bDai = m_pChara->GetActionID ( _T("ダメージ大") ) == indexAction ;
			bool bSyou = m_pChara->GetActionID ( _T("ダメージ小") ) == indexAction ;

			if ( bDai || bSyou )
			{
				ACTION_POSTURE ap = m_pOther.lock()->GetPosture ();
				if ( ap == ACTION_POSTURE::AP_JUMP )
				{
					indexAction = m_pChara->GetActionID ( _T("空中やられ") );
				}
			}

			//スクリプト
			P_Script pScr = pAct->GetpScript ( 0 );

			//@todo のけぞり時間を指定してある場合、相手に適用
			if ( m_pScript->m_prmBattle.Warp != 0 )
			{
			}

			//相手を変更
			m_pOther.lock ()->SetAction ( indexAction );	//遷移
			m_pOther.lock ()->m_btlPrm.SetForcedChange ( forced );
		}
	}

	//アクション移行(条件チェック)
	UINT ExeChara::Check_TransitAction_Condition ( BRANCH_CONDITION BRC_CND )
	{
		//キャラの持つルート,ブランチ,コマンドの参照
		const VP_Route& vpRoute = m_pChara->GetvpRoute ();
		const VP_Branch& vpBranch = m_pChara->GetvpBranch ();

		//スクリプトの持つルートリスト
		for ( UINT indexRut : m_pScript->GetcvRouteID () )
		{
			const V_UINT& vBrcID = vpRoute [ indexRut ]->GetcvIDBranch ();

			//対象のブランチリスト
			for ( UINT id : vBrcID )
			{
				//条件成立
				if ( BRC_CND != vpBranch [ id ]->GetCondition () ) { continue; }

				return vpBranch [ id ]->GetIndexSequence ();
			}
		}
		return NO_COMPLETE;
	}


	//-------------------------------------------------------------------------------------------------

	//アクション移項時、前アクションの最後の処理
	void ExeChara::EndAction ()
	{
		//次アクションを指定
		//実効アクションm_pActionは次フレーム時のMove()でm_actionIDを使って取得される
		m_actionID = m_pAction->GetNextID ();

		//------------------------------------------------
		//◆移項限定処理
		//ダッシュから次アクションに移項するとき、慣性を残す
		if ( m_pChara->GetActionID ( _T ( "FrontDashStart" ) ) == m_actionID )
		{
			m_btlPrm.SetDashInertial ( VEC2 ( 10.f, 0 ) );
		}
		if ( m_pChara->GetActionID ( _T ( "FrontDash" ) ) == m_actionID )
		{
			m_btlPrm.SetDashInertial ( VEC2 ( 10.f, 0 ) );
		}
		if ( m_pChara->GetActionID ( _T ( "BackDash" ) ) == m_actionID )
		{
			m_btlPrm.SetDashInertial ( VEC2 ( -8.f, 0 ) );
		}
		//------------------------------------------------

		//各種状態の終了
		m_btlPrm.EndAction ();
		m_frame = 0;
	}


}	//namespace GAME

