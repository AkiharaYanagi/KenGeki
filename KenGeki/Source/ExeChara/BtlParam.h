//=================================================================================================
//
//	バトルパラメータ
//
//=================================================================================================
#pragma once

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "Game.h"
#include "Script.h"
#include "../GameMain/GameConst.h"
#include "../FtgMain/FtgConst.h"


//-------------------------------------------------------------------------------------------------
// 宣言
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//前方宣言
	class ExeChara;
	using P_ExeChara = std::shared_ptr < ExeChara >;
	using WP_ExeChara = std::weak_ptr < ExeChara >;
	class Chara;
	using P_Chara = std::shared_ptr < Chara >;
	class Action;
	using P_Action = std::shared_ptr < Action >;
	class Script;
	using P_Script = std::shared_ptr < Script >;


	//単純フラグboolに対する get;set;マクロ
#define GET(type,Getter,prm)		type Getter () const { return prm; }
#define SET(type,Setter,prm)		void Setter ( type t ){ prm = t; }
#define GT_ST(type,Getter,Setter,b)		GET(type,Getter,b) SET(type,Setter,b)


	//クラス宣言
	class BtlParam
	{
		//------------------------------------------------
		//データ
		P_Chara			m_pChara;		//キャラデータ
		WP_ExeChara		m_pExeChara;	//自エグゼキャラ(循環参照なのでweak_ptr)
		WP_ExeChara		m_pOther;		//相手エグゼキャラ(循環参照なのでweak_ptr)
		P_Action		m_pAction;		//現在アクション
		P_Script		m_pScript;		//現在スクリプト

		//------------------------------------------------
		//パラメータ
		PLAYER_ID	m_playerID;			//プレイヤID
		VEC2	m_posChara;		//キャラ位置
		VEC2	m_tempPt;		//一時保存
		bool	m_dirRight;		//向き(右：正, 左：逆)
		VEC2	m_inertial;		//慣性
		VEC2	m_dashInertial;	//ダッシュ慣性
		VEC2	m_vel;	//速度
		VEC2	m_acc;	//加速度
		float	m_g;	//重力
		float	m_vg;	//重力(計算用)

		int		m_life;			//ライフ
		int		m_balance;		//バランス
		int		m_mana;			//マナ

		int		m_power;		//実効攻撃値
		bool	m_damaged;		//くらいフラグ
		bool	m_hitEst;		//攻撃成立フラグ
		bool	m_FirstEf;		//初回Efフラグ
		bool	m_FirstSE;		//初回SEフラグ
		bool	m_ForcedChange;	//強制変更
		bool	m_clang;		//打合発生フラグ
		bool	m_transit;		//スクリプト遷移したフレーム

		UINT	m_lurch;		//のけぞり(キャンセル不可)時間追加
		bool	m_wait;			//入力を一時停止
		bool	m_stop;			//入力、スクリプト処理を一時停止


		VP_Timer	m_timers;	//タイマ監理

		P_Timer	m_tmrStop;		//全体ストップタイマ
		P_Timer	m_tmrHitstop;	//ヒットストップタイマ
		P_Timer	m_tmrDown;		//ダウンタイマ
		P_Timer	m_tmrEnd;		//終了状態タイマ
		P_Timer	m_tmrLurch;		//のけぞりタイマ

		UINT	m_blackOut;		//暗転
		UINT	m_scpStop;		//スクリプトからの停止
		bool	m_whiteOut { F };	//白転

		//連続ヒット
		P_Timer	m_tmrHitPitch;		//ヒット間隔タイマ
		UINT	m_hitNum;			//同一アクション内ヒット数

		UINT	m_chainHitNum;		//連続ヒット数　(別アクション)

		//Result用
		int		m_nActTransit;		//アクション移行回数

	public:
		BtlParam ();
		BtlParam ( const BtlParam & rhs );	// ※ コピー可能
		~BtlParam ();

		//--------------------------------------------------------------------
		void SetpChara ( P_Chara p );
		void SetwpExeChara ( WP_ExeChara pExeChara, WP_ExeChara pOther );

		void Update ( P_Action pAct, P_Script pScp ) { m_pAction = pAct; m_pScript = pScp; }

		void SetPlayerID ( PLAYER_ID playerID );
		void LoadTimer ();
		void Init ();
		void TimerMove ();

		//--------------------------------------------------------------------
		//モード別初期化
		//トレーニング初期化
		void TrainingInit ();

		//--------------------------------------------------------------------
		//アクション終了時
		void EndAction ();

		//位置計算
		void CalcPos ( P_Script pScp );

		void PosInit ();

		//--------------------------------------------------------------------
		//各パラメータ
		GT_ST ( VEC2, GetPos, SetPos, m_posChara )			//キャラ位置
		GT_ST ( bool, GetDirRight, SetDirRight, m_dirRight )		//右正の向き
		GT_ST ( VEC2, GetInertial, SetInertial, m_inertial )			//慣性
		GT_ST ( VEC2, GetDashInertial, SetDashInertial, m_dashInertial )	//ダッシュ慣性
		GT_ST ( VEC2, GetVel, SetVel, m_vel )			//速度
		GT_ST ( VEC2, GetAcc, SetAcc, m_acc )			//加速度
		GT_ST ( float, GetG, SetG, m_g )			//重力
		GT_ST ( float, GetVg, SetVg, m_vg )			//重力(計算用)

		GT_ST ( int, GetLife, SetLife, m_life	)			//ライフ
		GT_ST ( int, GetBalance, SetBalance, m_balance )	//バランス
		GT_ST ( int, GetMana, SetMana, m_mana )				//マナ
		GT_ST ( int, GetPower, SetPower, m_power )			//実効攻撃値
		
		//--------------------------------------------------------------------
		//単純フラグ { get; set; }
		GT_ST ( bool, GetDamaged, SetDamaged, m_damaged )		//くらい
		GT_ST ( bool, GetHitEst, SetHitEst, m_hitEst )			//ヒット成立
		GT_ST ( bool, GetForcedChange, SetForcedChange, m_ForcedChange )		//強制変更
		GT_ST ( bool, GetClang, SetClang, m_clang )				//打合
		GT_ST ( bool, GetTrangit, SetTrangit, m_transit )		//スクリプト遷移
		GT_ST ( bool, GetFirstEf, SetFirstEf, m_FirstEf )		//エフェクト発生初回
		GT_ST ( bool, GetFirstSE, SetFirstSE, m_FirstSE )		//SE発生初回
		GT_ST ( bool, GetWait, SetWait, m_wait )				//待機(入力を停止)
		GT_ST ( bool, GetStop, SetStop, m_stop )				//停止(入力、スクリプト処理を停止)

		//--------------------------------------------------------------------
		GT_ST ( UINT, GetBlackOut, SetBlackOut, m_blackOut )	//暗転
		GT_ST ( UINT, GetScpStop, SetScpStop, m_scpStop )	//スクリプトからの停止
		GT_ST ( UINT, GetHitNum, SetHitNum, m_hitNum )		//同一アクション内ヒット数
		GT_ST ( UINT, GetChainHitNum, SetChainHitNum, m_chainHitNum )		//同一アクション内ヒット数
		GT_ST ( bool, GetWhiteOut, SetWhiteOut, m_whiteOut )		//白転

		//--------------------------------------------------------------------
		GT_ST ( int, GetNActTrs, SetNActTrs, m_nActTransit )			//アクション移行回数

		//--------------------------------------------------------------------
		//タイマ
		P_Timer GetTmr_Stop () { return m_tmrStop; }
		P_Timer GetTmr_HitStop () { return m_tmrHitstop; }
		P_Timer GetTmr_HitPitch () { return m_tmrHitPitch; }
		P_Timer GetTmr_Down () { return m_tmrDown; }
		P_Timer GetTmr_End () { return m_tmrEnd; }
		P_Timer GetTmr_Lurch () { return m_tmrLurch; }
		//--------------------------------------------------------------------
		bool IsHitStop () const 
		{ 
			//最後だけ外す
			if ( m_tmrHitstop->IsLast () ) { return F; }
			return m_tmrHitstop->IsActive ();
		}


		//====
		//増減用
		void AddLife ( int n );
		void AddBalance ( int n );
		void AddMana ( int n );

		void AddNActTrs ( int n ) { m_nActTransit += n; }

		//----
		//処理まとめ
		void OnHit ();	//ヒット時
		void HitPitchWaitStart ();	//ヒット間隔再スタート
		void CalcBalance ( P_Script pScp );

		void BackPt () { m_posChara = m_tempPt; }
		void BackPtX () { m_posChara.x = m_tempPt.x; }
		void Ground_Y ();	//Yリセット
		void Landing ();	//着地
		void BackMoveX ();	//重なり時後ろずらし
		void LookOther ();	//相手の方向を向く

	private:
		// 慣性の減少
		void DecreaseInertial ( float d, VEC2 & inertial );
	};


}	//namespace GAME

