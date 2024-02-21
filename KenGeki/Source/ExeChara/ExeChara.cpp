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

	//■	毎フレーム スクリプト前処理
	void ExeChara::PreScriptMove () { m_actor.PreScriptMove (); }

	//■	両者の接触判定後に攻撃・相殺・当り判定枠を設定
	void ExeChara::RectMove () { m_actor.RectMove (); }

	//■	毎フレーム スクリプト後処理
	void ExeChara::PostScriptMove () { m_actor.PostScriptMove (); }


	//各処理で何を行い、何を飛ばすかはアクタがステートによって指定する
	
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

		//-----------------------------------------------------
		// のけぞり時間はコマンドによる行動不可
		if ( m_btlPrm.GetTmr_Lurch ()->IsActive () ) { return; }

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
			EndAction ();	//アクション終了処理
			ExeScript ();	//スクリプト通常処理
			return;			//終了
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
//		Landing ();	//着地
		m_btlPrm.Landing ();	//着地
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

		//-----------------------------------------------------
		//超必殺背景演出
		if ( IsNameAction ( _T ( "超必Bヒット後" ) ) )
		{
			if ( m_frame == 0 )
			{
				m_dispChara->TurnShadow ( T );
				m_btlPrm.SetWhiteOut ( T );
				m_pOther.lock()->m_dispChara->TurnShadow ( T );
			}
		}
		if ( IsNameAction ( _T ( "超必B終了" ) ) )
		{
			if ( m_frame == 0 )
			{
				m_dispChara->TurnShadow ( F );
				m_pOther.lock()->m_dispChara->TurnShadow ( F );
			}
		}

	}

	//スクリプトからパラメータを反映する
	void ExeChara::SetParamFromScript ()
	{
		//------------------------------------------------
		//アクションとスクリプトをパラメータに渡す
		m_btlPrm.Update ( m_pAction, m_pScript );

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

#if 0
	//落下・着地
	void ExeChara::Landing ()
	{
		m_btlPrm.Landing ();

		VEC2 pos = m_btlPrm.GetPos ();
		
		//位置が基準より下だったら
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
#endif // 0



	void ExeChara::BackMoveX ()
	{
		m_btlPrm.BackMoveX ();
#if 0
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
#endif // 0
	}

	void ExeChara::LookOther ()
	{
		m_btlPrm.LookOther ();
#if 0

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

#endif // 0
	}


	//-------------------------------------------------------------------------------------------------
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

