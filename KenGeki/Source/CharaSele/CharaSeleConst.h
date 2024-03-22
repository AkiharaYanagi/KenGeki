//=================================================================================================
//
//	CharaSele定数 ヘッダファイル
//
//=================================================================================================
#pragma once

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------

//-------------------------------------------------------------------------------------------------
// 宣言
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//キャラ枠位置( 0 ~ 9 )
	enum CHARA_SELE_ID
	{
		CHSLID_0, CHSLID_1, CHSLID_2, CHSLID_3, CHSLID_4, CHSLID_5, CHSLID_6, CHSLID_7, CHSLID_8, CHSLID_9,
	};

	//表示位置定数
	enum CHARA_SELE
	{
		CHARA_SELE_NUM = 10,

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

		SELECT_1P_POS_X = 415 + 75 - 128,	//基準点 + 枠中心 + 画像半分
		SELECT_1P_POS_Y = 70 + 75 - 128,
		SELECT_2P_POS_X = 715 + 75 - 128,	//基準点 + 枠中心 + 画像半分
		SELECT_2P_POS_Y = 70 + 75 - 128,

		CUTIN_VX = 60,

		POS0_X = 0,
	};

	//キャラ枠と移動先指定
	struct ChSlct
	{
		CHARA_SELE_ID ID = CHSLID_0;
		float x = SELECT_1P_POS_X;
		float y = SELECT_1P_POS_Y;
		CHARA_SELE_ID UP	 = CHSLID_7;
		CHARA_SELE_ID DOWN	 = CHSLID_3;
		CHARA_SELE_ID LEFT	 = CHSLID_2;
		CHARA_SELE_ID RIGHT	 = CHSLID_1;
	};


}	//namespace GAME


