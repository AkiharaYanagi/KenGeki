//=================================================================================================
//
// パラメータ
//		シーン間をまたぐ値
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "Param.h"

//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	Param::Param ()
		: m_winner ( _PLAYER_NUM )
		, m_n_life_1p ( 0 ), m_n_life_2p ( 0 ), m_n_act_1p ( 0 ), m_n_act_2p ( 0 )
		, m_n_input_1p ( 0 ), m_n_input_2p ( 0 )
		, m_n_offset ( 0 )
	{
		//ゲーム設定ファイル開始
		m_setting.Load ();
	}

	//コピーコンストラクタ
	Param::Param ( const Param & rhs )
		: m_winner ( _PLAYER_NUM )
		, m_n_life_1p ( 0 ), m_n_life_2p ( 0 ), m_n_act_1p ( 0 ), m_n_act_2p ( 0 )
		, m_n_input_1p ( 0 ), m_n_input_2p ( 0 )
		, m_n_offset ( 0 )
	{
		m_setting = rhs.m_setting;
	}

	Param::~Param ()
	{
	}

	void Param::SetMutchMode ( MUTCH_MODE mode )
	{
		m_mutchMode = mode; 

		switch ( mode )
		{
		case MODE_PLAYER_PLAYER:	m_setting.SetPlayerMode ( MODE_PLAYER, MODE_PLAYER );	break;
		case MODE_PLAYER_CPU:		m_setting.SetPlayerMode ( MODE_PLAYER, MODE_CPU );	break;
		case MODE_CPU_PLAYER:		m_setting.SetPlayerMode ( MODE_CPU, MODE_PLAYER );	break;
		case MODE_CPU_CPU:			m_setting.SetPlayerMode ( MODE_CPU, MODE_CPU );	break;
		case MODE_PLAYER_NETWORK:	m_setting.SetPlayerMode ( MODE_PLAYER, MODE_NETWORK );	break;
		}
	}

	void Param::SetMutchMode ( PLAYER_MODE mode1p, PLAYER_MODE mode2p )
	{
		if ( MODE_PLAYER == mode1p )
		{
			if ( MODE_PLAYER == mode2p ) { m_mutchMode = MODE_PLAYER_PLAYER; }
			if ( MODE_CPU == mode2p ) { m_mutchMode = MODE_PLAYER_CPU; }
			if ( MODE_NETWORK == mode2p ) { m_mutchMode = MODE_PLAYER_NETWORK; }
		}
		else if ( MODE_CPU == mode1p )
		{
			if ( MODE_PLAYER == mode2p ) { m_mutchMode = MODE_CPU_PLAYER; }
			if ( MODE_CPU == mode2p ) { m_mutchMode = MODE_CPU_CPU; }
		}
	}

	PLAYER_MODE Param::GetPlayerMode ( PLAYER_ID id ) const
	{
		PLAYER_MODE pm = MODE_PLAYER;
		if ( id == PLAYER_ID_1 )
		{
			switch ( m_mutchMode )
			{
			case MODE_PLAYER_PLAYER:	pm = MODE_PLAYER;	break;
			case MODE_PLAYER_CPU:		pm = MODE_PLAYER; 	break;
			case MODE_CPU_PLAYER:		pm = MODE_CPU;	  	break;
			case MODE_CPU_CPU:			pm = MODE_CPU;	  	break;
			case MODE_PLAYER_NETWORK:	pm = MODE_PLAYER; 	break;
			default: break;
			}
		}
		if ( id == PLAYER_ID_2 )
		{
			switch ( m_mutchMode )
			{
			case MODE_PLAYER_PLAYER:	pm = MODE_PLAYER;	break;
			case MODE_PLAYER_CPU:		pm = MODE_CPU;	  	break;
			case MODE_CPU_PLAYER:		pm = MODE_PLAYER; 	break;
			case MODE_CPU_CPU:			pm = MODE_CPU;	  	break;
			case MODE_PLAYER_NETWORK:	pm = MODE_NETWORK; 	break;
			default: break;
			}
		}
		return pm;
	}

	void Param::SetRandomChara ()
	{

	}


}	//namespace GAME

