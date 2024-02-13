//=================================================================================================
//
// エグゼキャラ　ヘッダファイル
//		キャラのデータを受けてゲーム上で実行（フレーム毎動作、描画）する
//		操作時の位置などを保存する
//
//=================================================================================================
#pragma once

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "Game.h"
#include "CharaData.h"
#include "../GameMain/GameConst.h"
#include "../GameMain/Param.h"
#include "../FtgMain/G_Ftg.h"
#include "../FtgMain/FtgConst.h"
#include "Disp/DispChara.h"
#include "Input/PlayerInput.h"
#include "Input/CPUInput.h"
#include "Effect/OperateEffect.h"
#include "Rect/CharaRect.h"
#include "BtlParam.h"
#include "ExeChara_Actor.h"
#include "../FtgMain/Ef/EfPart.h"

//-------------------------------------------------------------------------------------------------
// 宣言
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//前方宣言
	class ExeChara;
	using P_ExeChara = std::shared_ptr < ExeChara >;
	using WP_ExeChara = std::weak_ptr < ExeChara >;

	//キャラ実行 クラス
	class ExeChara : public TASK_VEC, public enable_shared_from_this < ExeChara >
	{
		//------------------------------------------------
		//基本データ
		P_Chara			m_pChara;		//キャラデータ
		WP_ExeChara		m_pOther;		//相手キャラ(循環参照なのでweak_ptr)

		PLAYER_ID		m_playerID;		//プレイヤーID
		PLAYER_MODE		m_playerMode;	//プレイヤーモード(人,CPU,ネットワーク)
		CHARA_NAME		m_name;			//キャラ名

		//------------------------------------------------
		//表示
		P_DispChara		m_dispChara;	//キャラ全般表示

		//------------------------------------------------
		//入力
		P_CharaInput	m_pCharaInput;	//入力
		P_PlayerInput	m_pPlayerInput;		//プレイヤ
		P_CPUInput		m_pCPUInput;		//プレイヤ


		//------------------------------------------------
		//スクリプト実行
		UINT			m_actionID;		//実効現在アクションID
		P_Action		m_pAction;		//実効アクションポインタ
		UINT			m_frame;		//実効内部フレーム(スクリプトID)
		P_Script		m_pScript;		//実効スクリプトポインタ

		//------------------------------------------------
		//枠
		P_CharaRect		m_charaRect;	//枠セット

		//------------------------------------------------
		//パラメータ
		BtlParam		m_btlPrm;		//バトルパラメータ	

		//------------------------------------------------
		//エフェクト監理
		P_OprEf			m_oprtEf;

		//------------------------------------------------
		//ゲーム進行状態ステート
		ExeChara_Actor	m_actor;

		//------------------------------------------------
		//粒子エフェクト(参照)
		P_EfPart	m_efPart;

	public:
		ExeChara () = delete;
		ExeChara ( PLAYER_ID m_playerID );
		ExeChara ( const ExeChara & rhs ) = delete;
		~ExeChara ();

		void ParamInit ( P_Param pParam );
		void Load ();
		void _Load ();
		void Init ();
		void Reset ();
		void _Reset ();	//復旧時

		//===========================================================
		//******************************
		// 毎フレーム スクリプト処理一連
		//******************************
		void PreScriptMove ();			//	スクリプト前処理
		//MutualChara::Collision ();	//	相互判定 (接触枠)
		void RectMove ();			//	ぶつかり後、判定枠を設定
		//MutualChara::Decision ();		//	相互判定 (攻撃枠、ヒット枠)
		void PostScriptMove ();			//	スクリプト後処理
		//===========================================================

		//------------------------------------------------------------
		//相手を設定
		void SetpOther ( WP_ExeChara p ) { m_pOther = p; }

		//------------------------------------------------------------

		//パラメータ
		//@todo スクリプトの持つ　ScriptParam_Battle と ExeCharaの持つ実効値 BtlPrm の整理

		BtlParam GetBtlPrm () const { return m_btlPrm; }

		void SetPos ( VEC2 v ) { m_btlPrm.SetPos ( v ); }
		VEC2 GetPos () const { return m_btlPrm.GetPos (); }		//位置を取得
		bool GetDirRight () const { return m_btlPrm.GetDirRight (); }	//向きを取得
		void SetDirRight ( bool b ) { m_btlPrm.SetDirRight ( b ); }		//立ち状態で向きを設定

		void BackPt () { m_btlPrm.BackPt (); }
		void BackPtX () { m_btlPrm.BackPtX (); }
		void BackMoveX ();
		void LookOther ();	//相手の方向を向く

		//------------------------------------------------
		//粒子エフェクト
		void SetpParticle ( P_EfPart p ) { m_efPart = p; }

		//---------------------------------------------
		//入力処理
		void Input ();

		//ライフ０チェック
		bool IsZeroLife () const { return ( 0 >= m_btlPrm.GetLife () ); }

		//------------------------------------------------
		//エフェクト

		//エフェクトリスト取得
		PLP_ExEf GetplpExEf () { return m_oprtEf->GetplpExEf (); }

		//---------------------------------------------
		//枠
		P_CharaRect GetpCharaRect () const { return m_charaRect; }		//枠取得

		void SetCollisionRect ();	//[PreMove] 位置から接触枠設定
		void SetRect ();			//[PostMove] 相殺・攻撃・当り 枠設定
		void ResetRect ();			//リセット
	private:
		void SetOffsetRect ();	//相殺枠設定
		void SetAttackRect ();	//攻撃枠設定
		void SetHitRect ();		//当り枠設定
	public:

		//---------------------------------------------
		//各値取得
		P_Script GetpScript () { return m_pScript; }
		BtlParam GetBtlParam () const { return m_btlPrm; };

		CHARA_NAME GetCharaName () const { return m_name; }
		int GetLife () const { return m_btlPrm.GetLife (); }		//ライフ取得
		ACTION_POSTURE GetPosture () const { return m_pAction->GetPosture (); }

		//---------------------------------------------
		//外部からの状態変更

		//Demo用
		void StartGreeting ();
		void StartGetReady ();
		void StartFighting ();

		//一時停止
		void SetWait ( bool b ) { m_btlPrm.SetWait ( b ); }	//入力を停止
		void SetStop ( bool b ) { m_btlPrm.SetStop ( b ); }	//描画更新を停止
		void SetStopTimer ( UINT i ) {
			m_btlPrm.GetTmr_Stop ()->SetTargetTime ( i );
			m_btlPrm.GetTmr_Stop ()->Start ();
		}

		//ヒットストップ
		bool IsHitStop ()
		{ 
			if ( ! m_btlPrm.GetTmr_HitStop ()->IsLast () )
			{
				return m_btlPrm.GetTmr_HitStop ()->IsActive ();
			}
			return false;
		}

		//打合
		bool GetClang () const { return m_btlPrm.GetClang (); }
		void SetClang ( bool b ) { m_btlPrm.SetClang ( b ); }

		//引数：打合停止時間
		void OnClang ( UINT nLurch, CLANG_DECISION_WL clangDecision );

		//相手・攻撃 → 自分・くらい
		//くらい発生
		bool IsDamaged () const { return m_btlPrm.GetDamaged (); }
		void SetDamaged ( bool b ) { m_btlPrm.SetDamaged ( b ); }
		void OnDamaged ( int damage );
		void OnDamaged ();

		//自分・攻撃 -> 相手・くらい
		//ヒット発生
		bool IsHit () const { return m_btlPrm.GetHitEst (); }
		void SetHit ( bool b ) { m_btlPrm.SetHitEst ( b ); }
		void OnHit ();
		void OnEfHit ();

		//-------------------------------------------------
		//スクリプトからの変更
		//暗転
		UINT GetBlackOut () const { return m_btlPrm.GetBlackOut (); }
		void SetBlackOut ( UINT i ) { m_btlPrm.SetBlackOut ( i ); }

		//停止
		UINT GetScpStop () const { return m_btlPrm.GetScpStop (); }
		void SetScpStop ( UINT i ) { m_btlPrm.SetScpStop ( i ); }

		//-------------------------------------------------

		//外部からの状態確認
		//特殊アクション（名前指定）
		bool IsNameAction ( tstring nameAction ) const { return m_pAction->IsName ( nameAction ); }

		//ダッシュ分岐
		void OnDashBranch ();

		//-------------------------------------------------
		//システム

		//トレーニングモード初期化
		void TrainingInit ();

		//枠表示切替
		void OnDispRect ();
		void OffDispRect ();

		//CPU操作切替
		void ControlCPU ();
		void ControlPlayer ();

	//================================================
	//	内部関数
	//================================================
	private:
//		void MakeEfOprt ();		//エフェクト処理の生成

	public:
		//アクション指定
		void SetAction ( tstring action_name );
		void SetAction ( UINT action_id );

		void TransitAction ();	// アクション移項
		bool TranditAction_Command ();	//アクション移項（コマンドに関する処理）
		void TransitScript ();	//スクリプトを遷移させる

		void SetParamFromScript ();	//スクリプトからパラメータを反映する

		void EndAction ();	//アクション移項時、前アクションの最後の処理
		void CalcPos ();	// 位置計算		//ぶつかり後、位置の修正
		void Landing ();	//落下・着地

	private:

//		void TransitAction ( UINT actionID );		//アクションの移項
		void TransitAction_Condition_I ( BRANCH_CONDITION CONDITION, bool forced );	//条件をチェックして移行
		void TransitAction_Condition_E ( BRANCH_CONDITION CONDITION, bool forced );	//条件をチェックして移行
		UINT Check_TransitAction_Condition ( BRANCH_CONDITION CONDITION );	//アクション移行(条件チェック)

	public:
		//Stateから呼ばれる状態別処理
		void CheckLife ();			//ライフ判定
		void UpdateGraphic ();		//グラフィック更新
//		void EffectGenerate ();		//エフェクト生成
		void EffectMove ();
		void MoveTimer () { m_btlPrm.TimerMove (); }

		void OverEfPart ();	//EfPart重なり

	private:

		//------------------------------------------------
		//アクション体勢
		bool Is_AP_Stand () { return m_pAction->GetPosture () == AP_STAND; }
		bool Is_AP_Jump () { return m_pAction->GetPosture () == AP_JUMP; }
		bool Is_AP_Crouch () { return m_pAction->GetPosture () == AP_CROUCH; }

		//------------------------------------------------
		//アクションカテゴリ

		//現在アクションカテゴリ取得
		ACTION_CATEGORY ActCtg () const { return m_pAction->GetCategory (); }
		bool IsActCtg ( ACTION_CATEGORY ac ) const { return ac == ActCtg (); }
		//各アクションかどうか
		bool IsAttacking () const
		{
			return
				m_pAction->GetCategory ()  == AC_ATTACK_L 
			||	m_pAction->GetCategory ()  == AC_ATTACK_M
			||	m_pAction->GetCategory ()  == AC_ATTACK_H; 
		}
		bool IsStand ()		{ return m_pAction->GetCategory ()  == AC_NEUTRAL; }
		bool IsMove ()		{ return m_pAction->GetCategory ()  == AC_MOVE; }
		bool IsDash ()		{ return m_pAction->GetCategory ()  == AC_DASH; }
		bool IsPoised ()	{ return m_pAction->GetCategory ()  == AC_POISED; }
		bool IsClang ()		{ return m_pAction->GetCategory ()  == AC_CLANG; }
		bool IsAvoid ()		{ return m_pAction->GetCategory ()  == AC_AVOID; }
		bool IsDotty ()		{ return m_pAction->GetCategory ()  == AC_DOTTY; }
		bool IsDamaged ()	{ return m_pAction->GetCategory ()  == AC_DAMAGED; }

		//------------------------------------------------
#if 0
		//のけぞり時間
		void SetLurch ( UINT n ) { if ( 0 != n ) { m_lurch = n;  } }
		bool LurchTimer ();

#endif // 0

	};


}	//namespace GAME


