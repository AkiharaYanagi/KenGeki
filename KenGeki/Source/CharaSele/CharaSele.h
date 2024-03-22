//=================================================================================================
//
//	CharaSele ヘッダファイル
//
//=================================================================================================
#pragma once

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "Game.h"
#include "../FtgMain/FtgConst.h"	
#include "../GameMain/Scene.h"
#include "CharaSeleConst.h"
#include "CharaSele_Player.h"


//-------------------------------------------------------------------------------------------------
// 宣言
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	class CharaSele : public Scene, public enable_shared_from_this < CharaSele >
	{
		//BG
		P_GrpAcv		m_bg;

		//カーソル
		P_GrpBlink		m_cursor1p;
		P_GrpBlink		m_cursor2p;

		CHARA_SELE_ID	m_chslct_id_1p { CHSLID_0 };	//現在位置1p
		CHARA_SELE_ID	m_chslct_id_2p { CHSLID_2 };	//現在位置2p

#if 0
		//キャラ枠位置
		std::vector < ChSlct >		mv_pos;


		//キャラ立絵
		P_GrpAcv		m_chara_1p;
		P_GrpAcv		m_chara_2p;

		//選択キャラ名(パラメータ)
		CHARA_NAME		m_charaName1p { CHARA_OUKA };	//１Pキャラ名
		CHARA_NAME		m_charaName2p { CHARA_OUKA };	//２Pキャラ名

		bool m_bDecide1p { F };	//1P側決定(1Pコントローラで2P側を操作)
		bool m_bDecide2p { F };	//2P側決定

		P_FadeRect		m_fade;
		bool			m_endWait { F };

		//モード保存
		MUTCH_MODE		m_mode { MODE_PLAYER_PLAYER };

#endif // 0

		P_ChSele_Pl		m_chsl_pl_1p;
		P_ChSele_Pl		m_chsl_pl_2p;

	public:
		CharaSele ();
		CharaSele ( const CharaSele & rhs ) = delete;
		~CharaSele ();

		void ParamInit ();
		void Load ();
		void Init ();
		void Move ();

		P_GameScene Transit ();

	private:
		void SetCursorUp ( P_GrpBlink pCrs, CHARA_SELE_ID & id );
		void SetCursorDown ( P_GrpBlink pCrs, CHARA_SELE_ID & id );
		void SetCursorLeft ( P_GrpBlink pCrs, CHARA_SELE_ID & id );
		void SetCursorRight ( P_GrpBlink pCrs, CHARA_SELE_ID & id );

		bool KeyLR1p ();
		bool KeyLR2p ();

		void Move1p ();
		void Move2p ();

		void Select1p ();
		void Select2p ();

		void SelectCPU1p ();
		void SelectCPU2p ();

		void Decision1p ();
		void Decision2p ();

		void DecisionCPU1p ();
		void DecisionCPU2p ();

	};


}	//namespace GAME


