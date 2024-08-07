//=================================================================================================
//
//	Result ヘッダファイル
//
//=================================================================================================
#pragma once

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "Game.h"
#include "../GameMain/Scene.h"
//#include "Rank.h"


//-------------------------------------------------------------------------------------------------
// 宣言
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//public 継承 enable_shared_from_this　＜ T >

	class Result : public Scene, public enable_shared_from_this < Result >
	{
		P_GrpAcv		m_bg;		//背景
		P_Timer			m_timer;	//終了タイマ
		P_GrpAcv		m_chara;	//キャラ

#if 0
		//===========================================================
		P_GrpStr		m_affinity;		//アフィニティ(相性)
		P_Rank			m_rank;

		//-----------------------------------------------------
		P_GrpStr		m_str1p;		//見出	
		P_GrpStr		m_str2p;

		P_GrpStr		m_strNLife;		//残ライフ
		P_GrpStr		m_strNLife1p;	
		P_GrpStr		m_strNLife2p;

		P_GrpStr		m_strNAct;		//アクション移行回数
		P_GrpStr		m_strNAct1p;	
		P_GrpStr		m_strNAct2p;
#endif // 0

		P_GrpStr		m_win_lose;		//勝敗
		P_GrpStr		m_win_lose_1p;
		P_GrpStr		m_win_lose_2p;

		//===========================================================

		static const float	BX_TX;
		static const float	BX_1P;
		static const float	BX_2P;
		static const float	BY;
		static const float	PY;
		static const float	CH_DX;
		static const float	CH_X_1P;
		static const float	CH_X_2P;
		static const float	CH_Y;

	public:
		Result ();
		Result ( const Result& rhs ) = delete;
		~Result ();

		void ParamInit ();
		void Load ();
		void Init ();
		void Move ();

		P_GameScene Transit ();

	private:

		P_GrpStr	MakeStr ( LPCTSTR str, VEC2 pos );
	};


}	//namespace GAME


