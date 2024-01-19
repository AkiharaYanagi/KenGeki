//=================================================================================================
//
// FtgConst ファイル
//		Ftgメイン共通定義
//
//=================================================================================================
#pragma once

//-------------------------------------------------------------------------------------------------
//	インクルードファイル
//-------------------------------------------------------------------------------------------------
#include "Game.h"

//-------------------------------------------------------------------------------------------------
//	定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//------------------------------------------
	// 定数宣言
	//------------------------------------------

	//演出時間
	enum DEMO_TIME
	{
		WIN_WAIT = 360,
		WIN_POSE_WAIT = 180,
	};

	//勝者
	enum WINNER
	{
		WINNER_DRAW,
		WINNER_1P,
		WINNER_2P,
		WINNER_DP,
	};

	//文字定数
	const LPCTSTR ACT_DOWN = _T ( "Down" );
	//入力
	enum COMMAND
	{
		NO_COMPLETE = 0xFFFFFFFF
	};

	//キャラ定数
	enum CONST_CHARA
	{
		NUM_RECT = 8,	//判定枠配列の保持サイズ
//		NUM_RECT = 2,	//判定枠配列の保持サイズ
	};

	//枠表示(色)
	enum RECT_COLOR
	{
		RECT_COLOR_C = 0x400000ff,
		RECT_COLOR_A = 0x40ff0000,
		RECT_COLOR_H = 0x4000ff00,
		RECT_COLOR_O = 0x40ffff00,
	};

	//割合
	enum PERCENTAGE
	{
		CENT = 100,
	};

	//CPU_INPUT
	enum CPU_INPUT
	{
		CPU_INPUT_GAMEKEY_NUM = 60,
	};

	//打合結果 (左右)
	enum CLANG_DECISION_LR
	{
		CD_EQUAL,
		CD_LEFT,
		CD_RIGHT,
		CD_OFF,
	};

	//打合結果 (勝負)
	enum CLANG_DECISION_WL
	{
		CD_DRAW,
		CD_WIN,
		CD_LOSE,
	};

	//画面描画用 初期位置
	enum PLAYER_BASE_PT
	{
		GAME_WIDTH = 2048,		//ゲーム上で扱う幅
		GAME_HEIGHT = 1024,

		BG_SIZE_X = 2048,		//背景
		BG_SIZE_Y = 1024,
		BG_POS_X = 0 - ( 2048 - 1280 ) / 2,
		BG_POS_Y = 0 - ( 1024 - 960 ) / 2,

		GROUND_Y = 750,			//地面高さ
		PLAYER_BASE_Y = GROUND_Y,

		PLAYER_1_BASE_X = GAME_WIDTH / 2 - GAME_WINDOW_WIDTH / 2 + 200,
		PLAYER_1_BASE_Y = PLAYER_BASE_Y,
		PLAYER_2_BASE_X = GAME_WIDTH / 2 + GAME_WINDOW_WIDTH / 2 - 200,
		PLAYER_2_BASE_Y = PLAYER_BASE_Y,

		GRP_PLAYER1_X = 0,
		GRP_PLAYER1_Y = 14,
		GRP_PLAYER2_X = GAME_WINDOW_WIDTH - 64 - 0,
		GRP_PLAYER2_Y = 14,
	};

	//ゲージ類
	enum GAUGE
	{
		LIFE_GAUGE_X = 80,
		LIFE_GAUGE_Y = 60,
		LIFE_GAUGE_W = 500,
		LIFE_GAUGE_H = 40,

		LIFE_GAUGE_VALUE_CLR0 = 0xffd0f040,
		LIFE_GAUGE_VALUE_CLR1 = 0xffa0f0d0,
		LIFE_GAUGE_VALUE_CLR2 = 0xfff0f040,
		LIFE_GAUGE_VALUE_CLR3 = 0xffa0f0d0,
		LIFE_GAUGE_FRAME_CLR = 0xff606060,
		LIFE_GAUGE_DECREASE_CLR = 0xffff9090,


		BALANCE_GAUGE_X = 80,
		BALANCE_GAUGE_Y = 110,
		BALANCE_GAUGE_W = 500,
		BALANCE_GAUGE_H = 25,

		BALANCE_GAUGE_VALUE_CLR0 = 0xff40d0f0,
		BALANCE_GAUGE_VALUE_CLR1 = 0xffa0d0f0,
		BALANCE_GAUGE_VALUE_CLR2 = 0xff40f0f0,
		BALANCE_GAUGE_VALUE_CLR3 = 0xffa0d0f0,
		BALANCE_GAUGE_FRAME_CLR = 0xff808080,
		BALANCE_GAUGE_DECREASE_CLR = 0xffff9090,


		MANA_GAUGE_X = 80,
		MANA_GAUGE_Y = 850,
		MANA_GAUGE_W = 500,
		MANA_GAUGE_H = 25,

		MANA_GAUGE_VALUE_CLR0 = 0xffd080f0,
		MANA_GAUGE_VALUE_CLR1 = 0xffa0d0f0,
		MANA_GAUGE_VALUE_CLR2 = 0xffa080f0,
		MANA_GAUGE_VALUE_CLR3 = 0xffa0d0f0,
		MANA_GAUGE_FRAME_CLR = 0xff606060,
		MANA_GAUGE_DECREASE_CLR = 0xffff9090,

	};

	//バトル関連定数
	enum BATTLE_CONST
	{
		//フィールドサイズ
		FIELD_BASE_X = 0,
		FIELD_WIDTH = 2048,
		FIELD_EDGE = 64,		//画面端の補正

		//ゲージ類
		LIFE_MAX = 10000,
		LIFE_START = 10000,

		BALANCE_MAX = 10000,
		BALANCE_START = 8000,

		MANA_MAX = 10000,
		MANA_START = 1000,
		MANA_HALF = MANA_MAX / 2,

		//停止
		HITSTOP_TIME = 15,
		DOWN_TIME = 120 + 60,
		END_TIME = 60,
	};
}

