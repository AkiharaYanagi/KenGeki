//=================================================================================================
//
// エグゼキャラ　ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "ExeChara.h"
#include "Input/PlayerInput.h"
#include "Input/CPUInput.h"
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
		m_btlPrm.LoadPlayerID ( m_playerID );	//バトルパラメータ

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

		//プレイヤモード(入力種類)による初期化
		switch ( m_playerMode )
		{
		case MODE_PLAYER: m_pCharaInput = make_shared < PlayerInput > (); break;
		case MODE_CPU: m_pCharaInput = make_shared < CPUInput > ( shared_from_this (), m_pOther ); break;
		case MODE_NETWORK: m_pCharaInput = make_shared < PlayerInput > (); break;	//(仮)
		default: break;
		}
		m_pCharaInput->SetPlayer ( m_playerID );

		m_dispChara->ParamInit ( pParam );
	}

	//------------------------
	//読込
	void ExeChara::Load ()
	{
		//--------------------------------------------
		//m_pCharaのデータ読込
		_Load ();

		//--------------------------------------------
		//キャラ表示初期化
		m_dispChara->SetpChara ( m_pChara );
		m_dispChara->SetpCharaRect ( m_charaRect );

		//エフェクト生成ベクタの生成
		MakeEfOprt ();

		//アクタ・ステートに用いる状態パラメータに登録
		m_actor.SetwpExeChara ( shared_from_this () );

		TASK_VEC::Load ();
	}

	void ExeChara::_Load ()
	{
		//--------------------------------------------
		//m_pCharaのデータ読込

//名前からスクリプトファイルを指定してキャラのロード
//※	D3DXのテクスチャを用いるためフォーカス変更時などに再設定(Reset())が必要
//		tstring name (_T ("testChara.dat"));
//		tstring name ( _T ( "chara.dat" ) );
		tstring name ( _T ( "charaBin.dat" ) );
		//		tstring name (_T ("chara_standBin.dat"));
#if 0
		switch ( m_name )
		{
		case CHARA_RAKUNO: name.assign ( _T ( "Rakuno.dat" ) ); break;
		case CHARA_YUKINO: name.assign ( _T ( "Yukino.dat" ) ); break;
		default: break;
		}
#endif // 0

		//バイナリデータ読込
		LoadCharaBin loadCharaBin ( name.c_str (), *m_pChara );
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
		m_dispChara->UpdateMainImage ( m_pScript, GetPos (), GetDirRight () );
		//入力表示更新
		m_dispChara->UpdateInput ( m_pCharaInput );


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
		m_oprtEf->SetpChara ( m_pChara );

		//アクション・スクリプト再取得
		m_pAction = m_pChara->GetpAction ( m_actionID );
		m_pScript = m_pAction->GetpScript ( m_frame );

		//メイン イメージ
		m_dispChara->UpdateMainImage ( m_pScript, GetPos (), GetDirRight () );

		//エフェクト イメージ
		m_oprtEf->PostScriptMove ( GetPos (), GetDirRight () );
	}

	//==========================================================
	//MutualCharaから呼ばれる主な関数
	//==========================================================
	//	↓ 処理順番
	//	void ExeChara::PreScriptMove ();	//	スクリプト前処理
	//	void MutualChara::Collision ();		//	相互判定 (接触枠)
	//	void ExeChara::RectMove ();			//	ぶつかり後、判定枠を設定
	//	void MutualChara::Decision ();		//	相互判定 (攻撃枠、ヒット枠)
	//	void ExeChara::PostScriptMove ();	//	スクリプト後処理

	//■	毎フレーム スクリプト前処理
	void ExeChara::PreScriptMove () { m_actor.PreScriptMove (); }

	//■	両者の接触判定後に攻撃・相殺・当り判定枠を設定
	void ExeChara::RectMove () { m_actor.RectMove (); }

	//■	毎フレーム スクリプト後処理
	void ExeChara::PostScriptMove () { m_actor.PostScriptMove (); }


	//================================================
	//	外部からの状態変更
	//================================================
	void ExeChara::StartGreeting () { m_actor.StartGreeting (); }
	void ExeChara::StartGetReady () { m_actor.StartGetReady (); }
	void ExeChara::StartFighting () { m_actor.StartFighting (); }



	//================================================
	//	内部関数
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
		//バランスアウト
		if ( m_btlPrm.GetBalance () <= 0 )
		{
			SetAction ( _T ( "Dotty" ) );
		}


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

			//実効アクションm_pActionは次フレーム時のMove()でm_actionIDを使って取得される
			m_actionID = m_pAction->GetNextID ();
			TransitScript ();

			//終了
			return;
		}

		//-----------------------------------------------------
		// スクリプト通常処理
#if 0
		//m_frameは0から開始、Move()とDraw()で同一スクリプトを処理する
		//このフレームでスクリプトを処理するため、移行先アクションとスクリプトを保存
		m_pAction = m_pChara->GetpAction ( m_actionID );
		m_pScript = m_pAction->GetpScript ( m_frame );
#endif // 0
		TransitScript ();

		//通常処理：スクリプトを１つ進める
		++ m_frame;
	}


	//アクション移項（コマンドに関する処理）
	bool ExeChara::TranditAction_Command ()
	{
		//-----------------------------------------------------
		//テスト用
		//特定のアクション
		if ( IsNameAction ( _T ( "FrontDash" ) ) )
		{
			int i = 0;
		}

		//-----------------------------------------------------
		// コマンドによる分岐

//		UINT transitID = m_pCharaInput->GetTransitID ( *m_pChara, m_pScript, m_btlPrm.GetDirRight () );

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
			TransitScript ();

			//計測
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
#if 0
	void ExeChara::TransitAction ( UINT actionID )
	{
		m_actionID = actionID;		//遷移
		m_frame = 0;		//スクリプト初期化

		//一時アクションとスクリプトを再設定
		m_pAction = m_pChara->GetpAction ( m_actionID );
		m_pScript = m_pAction->GetpScript ( m_frame );
	}
#endif // 0
	void ExeChara::SetAction ( tstring action_name )
	{
		SetAction ( m_pChara->GetActionID ( action_name ) );
	}

	void ExeChara::SetAction ( UINT action_id )
	{
		m_actionID = action_id;
		m_pAction = m_pChara->GetpAction ( m_actionID );
		m_frame = 0;
		m_pScript = m_pAction->GetpScript ( m_frame );

		//スクリプトからのパラメータ反映
		SetParamFromScript ();
	}

	//スクリプトを遷移させる
	void ExeChara::TransitScript ()
	{
		//今回のフレーム中はm_pActionとm_pScriptを用い、
		//これ以降はm_actionIDとm_frameを用いない
		//このフレームでスクリプトを処理するため、移行先アクションとスクリプトを保存
		m_pAction = m_pChara->GetpAction ( m_actionID );
		m_pScript = m_pAction->GetpScript ( m_frame );

		//スクリプトからのパラメータ反映
		SetParamFromScript ();

		//特殊アクション指定

		//ダッシュ開始
		if ( IsNameAction ( _T ( "FrontDashStart" ) ) )
		{
			if ( m_frame == 0 )
			{
				//SE
				SOUND->Play_SE ( SE_Btl_Dash );
			}
		}
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

	}

	//スクリプトからパラメータを反映する
	void ExeChara::SetParamFromScript ()
	{
		//暗転
		m_btlPrm.SetBlackOut ( m_pScript->m_prmStaging.BlackOut );
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
			P_Script pScr = pAct->GetpScript ( 0 );

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


	//====================================================================================
	// 位置計算
	void ExeChara::CalcPos ()
	{
		//バランス処理
		int b = m_btlPrm.GetBalance ();
		int sb = m_pScript->m_prmBattle.Balance_I;
		b -= sb;
		if ( b < 0 ) { b = 0; }
		if ( b > BALANCE_MAX ) { b = BALANCE_MAX; }

		m_btlPrm.SetBalance ( b );

		//位置計算
		m_btlPrm.CalcPos ( m_pScript );

		Landing ();	//着地
	}

	//====================================================================================
	//エフェクト処理の生成
	void ExeChara::MakeEfOprt ()
	{
		//エフェクト処理にキャラポインタを設定
		m_oprtEf->SetpChara ( m_pChara );

		//すべてのアクションとスクリプトを巡回
		PVP_Action pvpAction = m_pChara->GetpvpAction ();
		for ( P_Action pAction : ( * pvpAction ) )
		{
			PVP_Script pvpScript = pAction->GetpvpScript ();
			for ( P_Script pScript : ( * pvpScript ) )
			{
				PVP_EfGnrt pvpEfGnrt = pScript->GetpvpEfGnrt ();
				for ( P_EfGnrt pEfGnrt : ( * pvpEfGnrt ) )
				{
					//非生成なら初回に登録しておき、IDで稼働状態にする
					if ( ! pEfGnrt->GetGnrt () )
					{
						//エフェクトインデックスの取得
						UINT index = pEfGnrt->GetIndex ();

						//エフェクトの取得
						P_Effect pEf = m_pChara->GetpEffect ( index );

						//エフェクト管理に渡してIDを得る
//						UINT id = m_oprtEf.Addstd::vectorEffect ( pEf, pEfGnrt->GetZ () );

						//IDを記録
//						pEfGnrt->SetID ( id );
					}
				}
			}
		}
	}

	//エフェクト生成
	void ExeChara::EffectGenerate ()
	{
		//発生
		PVP_EfGnrt  pvpEfGnrt = m_pScript->GetpvpEfGnrt ();
		for ( P_EfGnrt pEfGnrt : ( *pvpEfGnrt ) )
		{
			//生成用なら
			if ( pEfGnrt->GetGnrt () )
			{
				//エフェクトインデックスの取得
				UINT index = pEfGnrt->GetIndex ();
				//エフェクトの取得
				P_Effect pEf = m_pChara->GetpEffect ( index );
				//リストに追加
				m_oprtEf->AddListEffect ( pEf, pEfGnrt, m_btlPrm.GetPos (), m_btlPrm.GetDirRight () );
			}
			else //再利用なら
			{
				int i = 0;
#if false
				//エフェクトインデックスの取得
				UINT index = pEfGnrt->GetIndex ();
				//エフェクトの取得
				P_Effect pEf = m_pChara->GetpEffect ( index );
				//稼働中かどうか
				if ( !m_oprtEf.IsActive ( pEf ) )
				{
					m_oprtEf.DriveEffect ( pEf );
				}
#endif // false
			}
		}
	}

	void ExeChara::EffectMove ()
	{
		//エフェクト生成と動作
		if ( m_btlPrm.GetFirstEf () )	//ヒット後の初回のみは動作
		{
			EffectGenerate ();
			m_btlPrm.SetFirstEf ( F );
		}
		else
		{
			if ( ! m_btlPrm.GetTmr_HitStop ()->IsActive () )	//ヒットストップ時は生成しない
			{
				EffectGenerate ();
			}
		}

		//エフェクト動作
		m_oprtEf->PreScriptMove ();

		//エフェクト同期
		m_oprtEf->PostScriptMove ( m_btlPrm.GetPos (), m_btlPrm.GetDirRight () );
	}
	//====================================================================================


	//ライフ判定
	void ExeChara::CheckLife ()
	{
		//自分がライフ０
		if ( 0 >= m_btlPrm.GetLife () )
		{
			//ダウン状態に強制変更
			SetAction ( ACT_DOWN );
			m_btlPrm.GetTmr_Down ()->Start ();
		}
	}

	//====================================================================================
	//グラフィック更新
	void ExeChara::UpdateGraphic ()
	{
		//メインイメージ
		m_dispChara->UpdateMainImage ( m_pScript, m_btlPrm.GetPos (), m_btlPrm.GetDirRight () );

		//入力表示更新
		m_dispChara->UpdateInput ( m_pCharaInput );

		//共通グラフィック
		if ( ! m_btlPrm.GetTmr_Stop ()->IsActive () )
		{
			//スクリプトからの停止
			m_btlPrm.SetScpStop ( m_pScript->m_prmStaging.Stop );
		}

		//ゲージ更新
		m_dispChara->UpdateGauge ( m_btlPrm );
	}
	//====================================================================================

	//落下・着地

	//@todo ブランチ条件でスクリプト移項するようにする

	void ExeChara::Landing ()
	{
		VEC2 pos = m_btlPrm.GetPos ();
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

		float x = iPos.x + ( dirBack ? -1.f : 1.f );
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
				for ( RECT rect : ( *pvHRect ) )
				{
					//重なっていたとき
					if ( OverlapPoint ( prm.m_pos, rect ) )
					{
						vPrm [ i ].m_gotten = T;
						++count;
						break;	//RECTのループを抜け、EfPartの点を続行
					}
				}
			}

			++ i;
		}

		m_btlPrm.AddMana ( 100 * count );
	}


	//-------------------------------------------------------------------------------------------------
	//	枠設定
	//-------------------------------------------------------------------------------------------------
	//接触枠設定
	void ExeChara::SetCollisionRect ()
	{
		m_charaRect->SetCRect ( m_pScript->GetpvCRect (), m_btlPrm.GetDirRight (), m_btlPrm.GetPos () );
	}


	//相殺・攻撃・当り・枠設定
	void ExeChara::SetRect ()
	{
		SetOffsetRect ();
		SetAttackRect ();
		SetHitRect ();
	}

	//相殺枠設定
	void ExeChara::SetOffsetRect ()
	{
		m_charaRect->SetORect ( m_pScript->GetpvORect (), m_btlPrm.GetDirRight (), m_btlPrm.GetPos () );
	}

	//攻撃枠設定
	void ExeChara::SetAttackRect ()
	{
		//======================================
		// ヒット時に後の攻撃枠を一時停止(多段防止)
		//======================================

		//ヒット状態を確認 (ヒット状態設定は OnHit(), OnClang() )
		bool bHit = m_btlPrm.GetHitEst () || m_btlPrm.GetClang ();

#if DEBUG_DISP
		//デバッグ表示あり
		if ( m_playerID == PLAYER_ID_1 )
		{
			DBGOUT_WND_F ( 4, _T ( "bHit = %d" ), bHit ? 1 : 0 );
			P_Timer ptHitStop = m_btlPrm.GetTmr_HitStop ();
			DBGOUT_WND_F ( 5, _T ( "hitStop = %d" ), ptHitStop->GetTime () );
			UINT hitpitch = m_pAction->GetHitPitch ();
			P_Timer ptHitPitch = m_btlPrm.GetTmr_HitPitch ();
			DBGOUT_WND_F ( 6, _T ( "hitPitch = %d / %d" ), ptHitPitch->GetTime (), hitpitch );
			UINT hitmax = m_pAction->GetHitNum ();
			UINT hitnum = m_btlPrm.GetHitNum ();
			DBGOUT_WND_F ( 7, _T ( "hitnum = %d / %d" ), hitnum, hitmax );
		}
#else
		//デバッグ表示なし
#endif // DEBUG_DISP

		//--------------------------------------------------
		//ヒットしていない時(通常時)、攻撃枠を設定して終了
		if ( ! bHit )
		{
			//攻撃枠の設定
			m_charaRect->SetARect ( m_pScript->GetpvARect (), m_btlPrm.GetDirRight (), m_btlPrm.GetPos () );
			return;
		}

		//--------------------------------------------------
		//攻撃成立時・打合時に同一アクション中のみ枠を消失させる

		//1ヒット技は攻撃枠を空にして終了
		if ( 1 >= m_pAction->GetHitNum () )
		{
			//攻撃枠を空にする
			m_charaRect->ResetARect ();
			return;
		}

		//--------------------------------------------------
		//アクション内ヒット数が上限に達したとき枠を空にする

		UINT hitmax = m_pAction->GetHitNum ();
		if ( hitmax <= m_btlPrm.GetHitNum () )
		{
			//攻撃枠を空にする
			m_charaRect->ResetARect ();
			return;
		}

		//--------------------------------------------------
		//多段可能なアクションは、ヒット間隔をチェックして攻撃枠を再設定する
		P_Timer ptHitPitch = m_btlPrm.GetTmr_HitPitch ();
		UINT pitch = m_pAction->GetHitPitch ();
		if ( pitch < ptHitPitch->GetTime () )
		{
			//再スタート
			m_btlPrm.HitPitchWaitStart ();

			//攻撃枠の設定
			m_charaRect->SetARect ( m_pScript->GetpvARect (), m_btlPrm.GetDirRight (), m_btlPrm.GetPos () );
			return;
		}

		//--------------------------------------------------
		//該当しなければ攻撃枠を空にする
		m_charaRect->ResetARect ();
	}

	//当り枠設定
	void ExeChara::SetHitRect ()
	{
		m_charaRect->SetHRect ( m_pScript->GetpvHRect (), m_btlPrm.GetDirRight (), m_btlPrm.GetPos () );
	}

	//枠表示切替
	void ExeChara::OnDispRect ()
	{
		m_dispChara->OnRect ();
		m_oprtEf->OnDispRect ();
	}
	void ExeChara::OffDispRect ()
	{
		m_dispChara->OffRect ();
		m_oprtEf->OffDispRect ();
	}

	//枠をすべてリセット
	void ExeChara::ResetRect ()
	{
		m_charaRect->ResetAllRect ();
	}

	//-------------------------------------------------------------------------------------------------

	//アクション移項時、前アクションの最後の処理
	void ExeChara::EndAction ()
	{
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
	//入力処理
	void ExeChara::Input ()
	{
		//入力の更新
		m_pCharaInput->Update ( GetDirRight () );
	}


	//トレーニングモード初期化
	void ExeChara::TrainingInit ()
	{
		Init ();
		m_btlPrm.TrainingInit ();
	}



	//CPU操作切替
	void ExeChara::ControlCPU ()
	{
		m_pCharaInput = make_shared < CPUInput > ( shared_from_this (), m_pOther );
		m_pCharaInput->SetPlayer ( m_playerID );

		m_dispChara->SetControl_CPU ();
	}

	void ExeChara::ControlPlayer ()
	{
		m_pCharaInput = make_shared < PlayerInput > ();
		m_pCharaInput->SetPlayer ( m_playerID );

		m_dispChara->SetControl_PLAYER ();
	}

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
			SetAction ( _T ( "Dotty" ) );
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

