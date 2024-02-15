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
	{
		m_chara_center_x = GAME_WIDTH * 0.5f;
		m_wall_L = 0;
		m_wall_R = GAME_WIDTH;
	}

	G_Ftg::~G_Ftg ()
	{
	}

	//キャラ位置による画面表示の基準位置
	void G_Ftg:: CulcPosMutualBase ( VEC2 pos1p, VEC2 pos2p )
	{
		//戻値
		float posMutualBase = 0;

		//位置
		float averagex = (pos1p.x + pos2p.x) * 0.5f;	//中心
		float window_half = GAME_WINDOW_WIDTH * 0.5f;	//表示ウィンドウの中心(半分)
		m_chara_center_x = averagex;

		//画面端表示処理

		//特殊演出

		//壁リセット
		if ( m_bResetPos )
		{
			posMutualBase = window_half - averagex;
			m_posMutualBase = VEC2 ( posMutualBase, 0 );

			//壁位置
			m_wall_L = m_chara_center_x - GAME_WIDTH * 0.5f;
			m_wall_R = m_chara_center_x + GAME_WIDTH * 0.5f;

			m_bResetPos = F;
			return;
		}

		//壁の中心
		float wall_center = ( m_wall_L + m_wall_R ) * 0.5f;




		posMutualBase = window_half - averagex;

#if 1
		//左寄
		//画面左端から表示半分左側のとき
		if ( averagex < m_wall_L + window_half )
		{
			posMutualBase = 0 - m_wall_L;
		}
		//右寄
		//画面右端から表示半分右側のとき
		else if ( averagex > m_wall_R - window_half )
		{
			posMutualBase = GAME_WINDOW_WIDTH - m_wall_R;
		}
		//中央
		else
		{
			posMutualBase = window_half - averagex;
		}
#endif // 0




#if 0
		//左寄
		if ( averagex < 0 + window_half )
		{
			posMutualBase = 0;
		}
		//右寄
		else if ( averagex > GAME_WIDTH - window_half )
		{
			posMutualBase = (float)GAME_WINDOW_WIDTH - GAME_WIDTH;
		}
		//中央
		else
		{
			posMutualBase = window_center - averagex;
		}

#endif // 0

		//画面表示補正位置
		m_posMutualBase = VEC2 ( posMutualBase, 0 );
	}




}	//namespace GAME

