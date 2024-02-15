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
	//コンストラクタ
	ExeChara::ExeChara ( PLAYER_ID m_playerID )
		: m_pChara ( nullptr )
		, m_playerID ( m_playerID ), m_name ( CHARA_TEST ), m_playerMode ( MODE_PLAYER )
		, m_actionID ( 0 ), m_frame ( 0 )
	{
		//キャラデータ生成
		m_pChara = make_shared < Chara > ();	//キャラデータ実体
		m_charaRect = make_shared < CharaRect > ();	//実効枠
		m_btlPrm.SetPlayerID ( m_playerID );	//バトルパラメータ

		//表示
		m_dispChara = make_shared < DispChara > ();
		m_dispChara->LoadPlayer ( m_playerID );	//表示(1P/2P側による位置)
		AddpTask ( m_dispChara );

		//エフェクト監理
		m_oprtEf = make_shared < OperateEffect > ();
		AddpTask ( m_oprtEf );
	}

	//デストラクタ
	ExeChara::~ExeChara ()
	{
	}

	//------------------------
	//シーンパラメータ関連初期化
	void ExeChara::ParamInit ( P_Param pParam )
	{
		//ゲーム設定
		GameSettingFile stg = pParam->GetGameSetting ();

		//選択キャラ名前・モードを取得
		m_name = stg.GetName ( m_playerID );
		m_playerMode = stg.GetPlayerMode ( m_playerID );

		//入力
#if 0
		m_pPlayerInput = make_shared < PlayerInput > ();
		m_pPlayerInput->SetPlayer ( m_playerID );

		m_pCPUInput = make_shared < CPUInput > ( shared_from_this (), m_pOther );
		m_pCPUInput->SetPlayer ( m_playerID );
		m_pCPUInput->Load ();


		//プレイヤモード(入力種類)による初期化
		switch ( m_playerMode )
		{
		case MODE_PLAYER: m_pCharaInput = m_pPlayerInput; break;
		case MODE_CPU: m_pCharaInput = m_pCPUInput; break;
		case MODE_NETWORK: m_pCharaInput = m_pPlayerInput; break;	//(仮)
		default: break;
		}
#endif // 0
		LoadInput ();

		//表示
		m_dispChara->ParamInit ( pParam );
	}

	//------------------------
	//読込
	void ExeChara::Load ()
	{
		//--------------------------------------------
		//m_pCharaのデータ読込
		LoadCharaData ();

		//--------------------------------------------
		//キャラ表示初期化
		m_dispChara->SetpChara ( m_pChara );
		m_dispChara->SetpCharaRect ( m_charaRect );

		//アクタ・ステートに用いる状態パラメータに登録
		m_actor.SetwpExeChara ( shared_from_this () );

		//バトルパラメータに設定
		m_btlPrm.SetpChara ( m_pChara, m_pOther );

		//エフェクト生成ベクタの生成
		m_oprtEf->MakeEfList ( m_pChara );

		TASK_VEC::Load ();
	}

	//------------------------
	//初期化(複数回実行可能)
	void ExeChara::Init ()
	{
		//アクション・スクリプト初期化
		m_actionID = 0;
		m_pAction = m_pChara->GetpAction ( m_actionID );
		m_frame = 0;
		m_pScript = m_pAction->GetpScript ( m_frame );

		//バトルパラメータ
		m_btlPrm.Init ();
		SetParamFromScript ();	//スクリプトからパラメータを反映する

		//枠
		m_charaRect->ResetAllRect ();

		//状態
		m_actor.Init ();

		//表示
		//@info Move()中のTransit()の後に遷移し、
		//		再度Move()は呼ばれずDraw()が呼ばれるため、ここで手動の初期化が必要(Init()は呼ばれる)
#if 0
		m_dispChara->UpdateMainImage ( m_pScript, GetPos (), GetDirRight () );
		//入力表示更新
		m_dispChara->UpdateInput ( m_pCharaInput );
#endif // 0
		m_dispChara->Update ( m_pAction, m_pScript, m_btlPrm, m_pCharaInput );

		TASK_VEC::Init ();
	}

	//再設定
	void ExeChara::Reset ()
	{
		_Reset ();	//復旧時

		TASK_VEC::Reset ();
	}

	//------------------------
	//復旧時の再設定
	void ExeChara::_Reset ()
	{
		//テクスチャメモリ確保関連は再設定
		Rele ();
		m_pChara = make_shared < Chara > ();
		Load ();
		m_dispChara->SetpChara ( m_pChara );
		m_oprtEf->MakeEfList ( m_pChara );

		//アクション・スクリプト再取得
		m_pAction = m_pChara->GetpAction ( m_actionID );
		m_pScript = m_pAction->GetpScript ( m_frame );

		//表示の更新
#if 0
		//メイン イメージ
		m_dispChara->UpdateMainImage ( m_pScript, GetPos (), GetDirRight () );
#endif // 0
		m_dispChara->Update ( m_pAction, m_pScript, m_btlPrm, m_pCharaInput );

		//エフェクト イメージ
		m_oprtEf->PostScriptMove ( GetPos (), GetDirRight () );
	}

	//===========================================================
	//***********************************************************
	//	毎フレームMutualCharaから呼ばれる主なスクリプト処理関数の一連
	//***********************************************************
	//	↓ 処理順番
	//	void ExeChara::PreScriptMove ();	//	スクリプト前処理
	//	//void MutualChara::Collision ();	//	相互判定 (接触枠)
	//	void ExeChara::RectMove ();			//	ぶつかり後、判定枠を設定
	//	//void MutualChara::Decision ();	//	相互判定 (攻撃枠、ヒット枠)
	//	void ExeChara::PostScriptMove ();	//	スクリプト後処理
	//===========================================================

	//各処理で何を行い、何を飛ばすかはアクタが指定する
	
	//■	毎フレーム スクリプト前処理
	void ExeChara::PreScriptMove () { m_actor.PreScriptMove (); }

	//■	両者の接触判定後に攻撃・相殺・当り判定枠を設定
	void ExeChara::RectMove () { m_actor.RectMove (); }

	//■	毎フレーム スクリプト後処理
	void ExeChara::PostScriptMove () { m_actor.PostScriptMove (); }


	//===========================================================
	//***********************************************************
	//	Stateから呼ばれる状態別処理
	//***********************************************************
	//	void Input ();			//入力処理
	//	void TransitAction ();	//アクション移項
	//	void CalcPos ();		// 位置計算		//ぶつかり後、位置の修正
	//	void CheckLife ();		//ライフ判定
	//	void UpdateGraphic ();	//グラフィック更新
	//	void EffectMove ();		//エフェクト動作
	//	void MoveTimer () { m_btlPrm.TimerMove (); }		//タイマ稼働
	//===========================================================

	//================================================
	//入力処理
	void ExeChara::Input ()
	{
		//入力の更新
		m_pCharaInput->Update ( GetDirRight () );
	}

	//================================================
	// アクション移項(条件:コマンド, アクション終了)
	void ExeChara::TransitAction ()
	{
		assert ( nullptr != m_pAction && nullptr != m_pScript );

		// のけぞり時間はコマンドによる行動不可
#if 0
		if ( LurchTimer () ) { return; }
#endif // 0

		//-----------------------------------------------------
		// コマンドによる分岐
		if ( TranditAction_Command () )
		{
			//分岐が成立していたら以降はチェックしない
			return;
		}

		//-----------------------------------------------------
		//現在スクリプトが現在アクションにおける最終フレーム ならば
		if ( m_pAction->IsOverScript ( m_frame ) )
		{
			//アクション終了処理
			EndAction ();

			// スクリプト通常処理
			ExeScript ();

			//終了
			return;
		}

		//-----------------------------------------------------
		// スクリプト通常処理
		ExeScript ();

		//通常処理：スクリプトを１つ進める
		++ m_frame;
	}

	//================================================
	// 位置計算
	void ExeChara::CalcPos ()
	{
		m_btlPrm.CalcBalance ( m_pScript );	//バランス処理
		m_btlPrm.CalcPos ( m_pScript );		//位置計算
		Landing ();	//着地
	}

	//================================================
	//エフェクト
	void ExeChara::EffectMove ()
	{
		m_oprtEf->MoveEffect ( m_pScript, m_btlPrm );
	}

	//================================================
	//ライフ判定
	void ExeChara::CheckLife ()
	{
		//自分がライフ０
		if ( 0 >= m_btlPrm.GetLife () )
		{
			//test
#if 0
			//ダウン状態に強制変更
			SetAction ( ACT_DOWN );
			m_btlPrm.GetTmr_Down ()->Start ();
#endif // 0
			m_btlPrm.SetLife ( 10000 );
		}
	}

	//====================================================================================
	//グラフィック更新
	void ExeChara::UpdateGraphic ()
	{
		m_dispChara->Update ( m_pAction, m_pScript, m_btlPrm, m_pCharaInput );
	}


	//================================================
	//	外部からの状態(State)変更
	//================================================


	//================================================
	//	内部関数
	//================================================

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
			P_Action pact = m_pChara->GetpAction ( id );

			//対象IDがバランス消費で移項可能なら移動処理へ


			//不可能なら次をチェック
			transitID = id;
			break;
		}

		//コマンドが完成していたら
		if ( NO_COMPLETE != transitID )
		{
			//超必　特定処理
			if ( transitID == m_pChara->GetActionID ( _T ( "OD0_L" ) ) )
			{
				//マナ消費
				if ( m_btlPrm.GetMana () >= MANA_HALF )
				{
					m_btlPrm.AddMana ( - MANA_MAX / 2 );
				}
				else //足りないとき遷移しない
				{
					return F;
				}
			}

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


	//==================================================
	//スクリプト通常処理
	//==================================================
	void ExeChara::ExeScript ()
	{
		//------------------------------------------------
		//このフレームでスクリプトを処理するため、移行先アクションとスクリプトを保存
		//今回のフレーム中はm_pActionとm_pScriptを用い、
		//これ以降はm_actionIDとm_frameを用いない
		m_pAction = m_pChara->GetpAction ( m_actionID );
		m_pScript = m_pAction->GetpScript ( m_frame );

		//------------------------------------------------
		//	特殊アクション指定
		SpecialAction ();

		//------------------------------------------------
		//スクリプトからのパラメータ反映
		SetParamFromScript ();
	}

	//	特殊アクション指定処理
	void ExeChara::SpecialAction ()
	{
		//-----------------------------------------------------
		if ( IsNameAction ( _T ( "立ち" ) ) )
		{
			//連続ヒット数のリセット
			m_pOther.lock()->m_btlPrm.SetChainHitNum ( 0 );
			if ( m_frame == 0 )
			{
			}

			//Test　バランス修正
			int b = m_btlPrm.GetBalance ();
			{
				if ( b < 1000 ) 
				{
					m_btlPrm.SetBalance ( 5000 );
				}
			}
		}

		//-----------------------------------------------------
		//ダッシュ開始
		if ( IsNameAction ( _T ( "FrontDashStart" ) ) )
		{
			if ( m_frame == 0 )
			{
				//SE
				SOUND->Play_SE ( SE_Btl_Dash );
			}
		}

		//-----------------------------------------------------
		//ジャンプ
		if ( IsNameAction ( _T ( "FrontJump" ) )
			|| IsNameAction ( _T ( "VerticalJump" ) )
			|| IsNameAction ( _T ( "BackJump" ) )
			)
		{
			if ( m_frame == 0 )
			{
				//SE
				SOUND->Play_SE ( SE_Btl_Jump );
			}
		}

		//-----------------------------------------------------
		//足払い追撃終了
		if ( IsNameAction ( _T ( "足払い追撃ヒット" ) ) )
		{
			if ( m_frame == 0 )
			{
				//				G_FTG->SetResetPos ( T );
			}
		}

		//-----------------------------------------------------
		//バランスアウト
		if ( m_btlPrm.GetBalance () <= 0 )
		{
			//			SetAction ( _T ( "Dotty" ) );
			SetAction ( _T ( "立ち" ) );
		}
	}

	//スクリプトからパラメータを反映する
	void ExeChara::SetParamFromScript ()
	{
		//------------------------------------------------
		//暗転
		m_btlPrm.SetBlackOut ( m_pScript->m_prmStaging.BlackOut );

		//------------------------------------------------
		//スクリプト停止
		if ( ! m_btlPrm.GetTmr_Stop ()->IsActive () )
		{
			//スクリプトからの停止
			m_btlPrm.SetScpStop ( m_pScript->m_prmStaging.Stop );
		}
	}

	//====================================================================================

	//落下・着地
	void ExeChara::Landing ()
	{
		//位置が基準より下だったら
		VEC2 pos = m_btlPrm.GetPos ();
		
		if ( PLAYER_BASE_Y < pos.y )
		{
			//着地条件のブランチを取得
			UINT indexAction = Check_TransitAction_Condition ( BRANCH_CONDITION::BRC_GRD );
			if ( NO_COMPLETE != indexAction )
			{
				//y位置リセット
				float x = pos.x;
				float y = PLAYER_BASE_Y;
				m_btlPrm.SetPos ( VEC2 ( x, y ) );
				m_btlPrm.SetVg ( 0 );
				m_btlPrm.SetG ( 0 );

				//遷移先チェック
				P_Action pAct = m_pChara->GetpAction ( indexAction );
				P_Script pScr = pAct->GetpScript ( 0 );

				//自身を変更
				SetAction ( indexAction );	//遷移
			}
		}

		//位置が基準より上で立ち状態だったら
		if (PLAYER_BASE_Y > pos.y)
		{
			if ( IsNameAction ( _T ( "Drop" ) ) )
			{
				//落下
				m_btlPrm.SetG ( 5 );
			}
		}

		//位置が基準より下で立ち状態だったら
		if ( pos.y < PLAYER_BASE_Y )
		{
			if ( IsNameAction ( _T ( "立ち" ) ) )
			{
				//y位置リセット
				float x = pos.x;
				float y = PLAYER_BASE_Y;
				m_btlPrm.SetPos ( VEC2 ( x, y ) );
				m_btlPrm.SetVg ( 0 );
				m_btlPrm.SetG ( 0 );

			}
		}

#if 0
		if ( PLAYER_BASE_Y < pos.y )
		{
			//デモ時は何もしない
			if ( IsActCtg ( AC_DEMO ) )
			{
				return;
			}

			float x = pos.x;
			float y = PLAYER_BASE_Y;
			m_btlPrm.SetPos ( VEC2 ( x, y ) );
			m_btlPrm.SetVg ( 0 );
			m_btlPrm.SetG ( 0 );

			//SE
			SOUND->Play_SE ( SE_Btl_Landing );

			//実効アクションm_pActionは次フレーム時のMove()でm_actionIDを使って取得される
			m_actionID = 0;	//standの指定
			m_frame = 0;
		}

		//落下
		if ( IsStand () && PLAYER_BASE_Y > m_btlPrm.GetPos ().y )
		{
			m_btlPrm.SetG ( 5 );
		}
#endif // 0
	}


	void ExeChara::BackMoveX ()
	{
		//向きによらず、相手から離れる方向
		VEC2 iPos = m_btlPrm.GetPos ();
		VEC2 ePos = m_pOther.lock ()->GetPos ();
		bool dirBack = true;

		//同値の場合は1P2Pで選別
		if ( iPos.x == ePos.x )
		{
			dirBack = ( m_playerID == PLAYER_ID_1 );
		}
		else
		{
			//互いの位置で補正方向を決定
			dirBack = ( iPos.x < ePos.x );
		}

		float x = iPos.x + 10 * ( dirBack ? -1.f : 1.f );
		float y = iPos.y;
		m_btlPrm.SetPos ( VEC2 ( x, y ) );
	}

	void ExeChara::LookOther ()
	{
#if 0
		//ジャンプ中(アクション体勢/空中状態)は持続
		if ( Is_AP_Jump () )
		{
			return;
		}
#endif // 0
		//ニュートラルのみ振り返り
		if ( ! IsStand () )
		{
			return;
		}

		//位置xが同じ場合は持続
		VEC2 iPos = m_btlPrm.GetPos ();
		VEC2 ePos = m_pOther.lock ()->GetPos ();
		if ( iPos.x == ePos.x )
		{
			return;
		}

		//互いの位置で方向を決定
		m_btlPrm.SetDirRight ( iPos.x < ePos.x );
	}

	//EfPart重なり
	void ExeChara::OverEfPart ()
	{
		//1p2p同時重なりは両方が取得

		//当り枠を取得
		PV_RECT pvHRect = GetpCharaRect ()->GetpvHRect ();

		//EF側の点集合を取得
		std::vector < PrmEfPart > & vPrm = m_efPart->Getrv_Prm ();

		//重なり判定
		UINT count = 0;
		UINT i = 0;
		for ( PrmEfPart prm : vPrm )
		{
			if ( 0 != prm.m_count ) { continue; }

			if ( prm.m_pOb->GetValid () )
			{
				for ( const RECT & rect : ( *pvHRect ) )
				{
					//重なっていたとき
					if ( OverlapPoint ( prm.m_pos, rect ) )
					{
						vPrm [ i ].m_gotten = T;
						++ count;
						break;	//RECTのループを抜け、EfPartの点を続行
					}
				}
			}

			++ i;
		}

		m_btlPrm.AddMana ( 100 * count );
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

	//のけぞりタイマー
#if 0
	bool ExeChara::LurchTimer ()
	{
		// のけぞり時間はコマンドによる行動不可
		if ( 0 < m_lurch )
		{
			if ( m_lurchTimer == m_lurch )	//のけぞり終了
			{
				m_lurch = 0;
				m_lurchTimer = 0;
			}
			else
			{
				++m_lurchTimer;
				return true;
			}
		}
		return false;
	}
#endif // 0


	//-------------------------------------------------------------------------------------------------
	//トレーニングモード初期化
	void ExeChara::TrainingInit ()
	{
		Init ();
		m_btlPrm.TrainingInit ();
	}

	//CPU操作切替
	void ExeChara::ControlCPU ()
	{
		m_pCharaInput = m_pCPUInput;
		m_dispChara->SetControl_CPU ();
	}

	void ExeChara::ControlPlayer ()
	{
		m_pCharaInput = m_pPlayerInput;
		m_dispChara->SetControl_PLAYER ();
	}

}	//namespace GAME

