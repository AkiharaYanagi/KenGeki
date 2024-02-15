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

//-------------------------------------------------------------------------------------------------
// 宣言
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	class CharaSele : public Scene
	{
		enum CHALA_SELE
		{
			LOGO_NUM = 12,
			LOGO_W = 128,
			LOGO_L_START_Y = 40,
			LOGO_L_VEL_X = -4,
			LOGO_R_START_Y = 880,
			LOGO_R_VEL_X = 4,

			CHARA_1P_POS_X0 = -300 - 300 ,
			CHARA_1P_POS_X = -300,
			CHARA_1P_POS_Y = 100,

			CHARA_2P_POS_X0 = 500 + 300,
			CHARA_2P_POS_X = 500,
			CHARA_2P_POS_Y = 100,

			SELECT_1P_POS_X = 420,
			SELECT_1P_POS_Y = 0,
			SELECT_2P_POS_X = 620,
			SELECT_2P_POS_Y = 0,

			CUTIN_VX = 60,
		};

		//BG
		P_GrpAcv		m_bg;

		//ロゴ
		P_GrpAcv		m_logo_l;
		P_GrpAcv		m_logo_r;

		//キャラ立絵
		P_GrpAcv		m_chara_1p;
		P_GrpAcv		m_chara_2p;

		P_GrpAcv		m_chara_light_1p;
		P_GrpAcv		m_chara_light_2p;

		P_Timer			m_tmrDecide1p;
		P_Timer			m_tmrDecide2p;

		//立絵カットイン
		float			m_x_1p;
		float			m_x_2p;

		//キャラ選択表示
		P_GrpAcv		m_select_Sonia;
		P_GrpAcv		m_select_Orfloat;

		//カーソル
		P_GrpBlink		m_cursor1p;
		P_GrpBlink		m_cursor2p;
		P_GrpBlink		m_cursorCPU1p;
		P_GrpBlink		m_cursorCPU2p;

		//選択キャラ名(パラメータ)
		CHARA_NAME		m_charaName1p;	//１Pキャラ名
		CHARA_NAME		m_charaName2p;	//２Pキャラ名

		bool m_bDecide1p;	//1P側決定(1Pコントローラで2P側を操作)
		bool m_bDecide2p;	//2P側決定

		P_FadeRect		m_fade;
		bool			m_endWait;

		//モード保存
		MUTCH_MODE		m_mode;


	public:
		CharaSele ();
		CharaSele ( const CharaSele & rhs ) = delete;
		~CharaSele ();

		void ParamInit ();
		void Init ();
		void Move ();

		P_GameScene Transit ();

	private:
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


