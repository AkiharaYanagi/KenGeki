//=================================================================================================
//
//	BattleTime ヘッダファイル
//
//=================================================================================================
#pragma once

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "Game.h"


//-------------------------------------------------------------------------------------------------
// 宣言
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	class BattleTime : public TASK_LST
	{
		bool		m_active { T };		//稼働状態

		P_GrpAcv	m_time_bg;			//背景
		P_GrpAcv	m_battle_time;		//共通テクスチャ
		P_Object	m_battle_time_01;	//1の桁
		P_Object	m_battle_time_02;	//10の桁
		int			m_time { START_TIME };	// 3600 ～ 0 [F] ( 60 ～ 0[Sec] )

	public:
		BattleTime ();
		BattleTime ( const BattleTime & rhs ) = delete;
		~BattleTime ();

		void Init ();
		void Move ();

		void Set ();
		void Start () { m_active = T; }
		void Stop () { m_active = F; }

		bool IsTimeUp ();

	private:
		static const int START_TIME;

	};

	using P_BattleTime = std::shared_ptr < BattleTime >;


}	//namespace GAME


