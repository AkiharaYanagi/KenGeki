//=================================================================================================
//
// DispChara_Const ヘッダファイル
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

	//ゲージ類 個別位置と色
	enum GAUGE
	{
		LIFE_GAUGE_X = 80,
		LIFE_GAUGE_Y = 30,
		LIFE_GAUGE_W = 500,		//値
		LIFE_GAUGE_H = 128,
		LIFE_GAUGE_P = 6,		//パディング

		LIFE_GAUGE_VALUE_CLR0 = 0xffd0f040,
		LIFE_GAUGE_VALUE_CLR1 = 0xffa0f0d0,
		LIFE_GAUGE_VALUE_CLR2 = 0xfff0f040,
		LIFE_GAUGE_VALUE_CLR3 = 0xffa0f0d0,
		LIFE_GAUGE_FRAME_CLR = 0xff606060,
		LIFE_GAUGE_DECREASE_CLR = 0xffff0000,


		BALANCE_GAUGE_X = 80 - 2,
		BALANCE_GAUGE_Y = 150 + 10,
		BALANCE_GAUGE_W = 512,	//画像幅
		BALANCE_GAUGE_H = 25,

		BALANCE_GAUGE_VALUE_CLR0 = 0xff40d0f0,
		BALANCE_GAUGE_VALUE_CLR1 = 0xffa0d0f0,
		BALANCE_GAUGE_VALUE_CLR2 = 0xff40f0f0,
		BALANCE_GAUGE_VALUE_CLR3 = 0xffa0d0f0,
		BALANCE_GAUGE_FRAME_CLR = 0xff808080,
		BALANCE_GAUGE_DECREASE_CLR = 0xffff9090,


		MANA_GAUGE_X = 30,
		MANA_GAUGE_Y = 900,
		MANA_GAUGE_W = 512,
		MANA_GAUGE_H = 32,
		MANA_GAUGE_P = 0,

		MANA_GAUGE_VALUE_CLR0 = 0xffd080f0,
		MANA_GAUGE_VALUE_CLR1 = 0xffa0d0f0,
		MANA_GAUGE_VALUE_CLR2 = 0xffa080f0,
		MANA_GAUGE_VALUE_CLR3 = 0xffa0d0f0,
		MANA_GAUGE_FRAME_CLR = 0xff606060,
		MANA_GAUGE_DECREASE_CLR = 0xffff9090,

	};

}	//namespace GAME


