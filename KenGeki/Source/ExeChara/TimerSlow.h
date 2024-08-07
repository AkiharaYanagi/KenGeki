//=================================================================================================
//
//	タイマ：スロウ
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

	class TimerSlow
	{
		bool	m_valid { F };		//稼働状態

		int		m_count { 0 };		//	1/100
		int		m_pitch { 1000 };	//	1/100
		int		m_vel { 500 };		//	1/100
		bool	m_next { F };		//次へ

		static const int VEL;
		static const int PITCH;

	public:
		TimerSlow ();
		TimerSlow ( const TimerSlow & rhs ) = delete;
		~TimerSlow ();

		void Init ();
		void Move ();

		bool GetValid () const { return m_valid; }
		void SetValid ( bool b ) { m_valid = b; }

		int GetCount () const { return m_count; }

		//間隔 ( 1 / 100 )
		void SetPitch ( int pitch ) { m_pitch = pitch; }

		//速度 ( 1 / 100 )
		void SetVel ( int vel ) { m_vel = vel; }

		//スロウ倍率(magnification)
		float GetMagni () const { return (float)m_vel / m_pitch; }

		//次へ
		bool Next ();
	};


}	//namespace GAME

