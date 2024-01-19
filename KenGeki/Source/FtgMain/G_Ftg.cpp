//=================================================================================================
//
//	G_Ftg ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "G_Ftg.h"

//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{

	//------------------------------------------
	//	Static実体
	//------------------------------------------
	// シングルトンオブジェクト
	UP_G_Ftg	G_Ftg::m_inst;

	G_Ftg::G_Ftg ()
	:	  m_posMutualBase ( VEC2 ( 0, 0 ) )
	{
	}

	G_Ftg::~G_Ftg ()
	{
	}

	//キャラ位置による画面表示の基準位置
	void G_Ftg:: CulcPosMutualBase ( VEC2 pos1p, VEC2 pos2p ) const
	{
		//戻値
		float posMutualBase = 0;

		//位置
		float averagex = (pos1p.x + pos2p.x) * 0.5f;	//中心
		float window_center = GAME_WINDOW_WIDTH * 0.5f;
		float gw = GAME_WIDTH;

		//画面端表示処理

		//左寄
		if ( averagex < window_center )
		{
			posMutualBase = 0;
		}
		//右寄
		else if ( averagex > GAME_WIDTH - window_center )
		{
			posMutualBase = (float)GAME_WINDOW_WIDTH - GAME_WIDTH;
		}
		//中央
		else
		{
			posMutualBase = window_center - averagex;
		}

		G_Ftg::inst()->SetPosMutualBase ( VEC2 ( posMutualBase, 0 ) );
	}




}	//namespace GAME

