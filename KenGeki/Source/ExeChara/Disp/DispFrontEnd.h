//=================================================================================================
//
// DispFrontEnd ヘッダファイル
//
//=================================================================================================
#pragma once

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "Game.h"
#include "Chara.h"
#include "../../GameMain/Param.h"
#include "../../FtgMain/G_Ftg.h"
#include "../BtlParam.h"
#include "DispGauge.h"

//-------------------------------------------------------------------------------------------------
// 宣言
//-------------------------------------------------------------------------------------------------
namespace GAME
{

	class DispFrontEnd : public TASK_VEC
	{
		PLAYER_ID	m_playerID;			//プレイヤ側

		P_DispGauge	m_gaugeLife;		//ライフゲージ
		P_DispGauge	m_gaugeBalance;		//バランスゲージ
		P_DispGauge	m_gaugeMana;		//マナゲージ

#if	0
		PrmRect		m_gaugeHitStop;			//ヒットストップ時間表示
		PrmRect		m_gaugeLurch;			//のけぞり時間表示

											//エフェクト
		GrpEf		m_grpHitEf;			//ヒットエフェクト
		GrpEf		m_grpAvoidEf;		//アヴォイドエフェクト
		GrpEf		m_grpPoisedEf;		//構えエフェクト

		P_GrpApTx		m_efGraphic;		//エフェクトグラフィック表示
		OperateEffect*		m_pOprtEf;
#endif	//0
		//-----------------------------------------------------

		P_GrpAcv	m_grp_Cst_Player1P2P;	//プレイヤ側 固定表示"1P""2P"
		P_GrpAcv	m_grp_Cst_InputPlayerCOM;	//入力者 固定表示"CPU""Player"

		P_GrpAcv	m_grp_CH_Player1P2P;	//キャラ近傍 プレイヤ表示"1P""2P"
		P_GrpAcv	m_grp_CH_InputCOMPLayer;	//キャラ近傍 入力者表示"CPU""Player"

		static VEC2 POS_PL_CP_1P;
		static VEC2 POS_PL_CP_2P;
		//-----------------------------------------------------

		//ヒット数
		P_GrpAcv	m_grpHitNum;		//"0"~"9"
		P_GrpAcv	m_grpStrHit;		//"Hit"

		//-----------------------------------------------------
		enum DISP_FE_CONST
		{
			SIDE_1P = 0,
			SIDE_2P = 1,

			INPUT_PLAYER = 0,
			INPUT_CPU = 1,
		};

	public:
		DispFrontEnd ();
		DispFrontEnd ( const DispFrontEnd & rhs ) = delete;
		~DispFrontEnd ();

		void ParamInit ( P_Param pParam );


		//ゲージ類の表示部のみ初期化
		void LoadPlayer ( PLAYER_ID playerID );

		//メインイメージ更新
		void UpdateMainImage ( VEC2 posChara );

		//ゲージ類更新
		void UpdateGauge ( BtlParam btlPrm );

#if 0
		//ヒットストップ時間表示の更新
		void UpdateHitStop ( VEC2 ptChara, bool dirRight, UINT lurch, UINT lurchTimer );

		//のけぞり時間表示の更新
		void UpdateLurch ( VEC2 ptChara, bool dirRight, UINT lurch, UINT lurchTimer );

		//ヒット
		void OnHit ( VEC2 ptChara, bool dirRight );

		//アヴォイド
		void OnAvoid ( VEC2 ptChara, bool dirRight );

		//ポイズド
		void OnPoised ( VEC2 ptChara, bool dirRight );
#endif // 0

		void SetPlayer ();
		void SetCPU ();

		void UpdateHitNum ( UINT n );

	private:
		P_GrpAcv MakepGrpPlyr ( LPCTSTR pstr );

	};

	using P_DispFrontEnd = std::shared_ptr < DispFrontEnd >;


}	//namespace GAME

