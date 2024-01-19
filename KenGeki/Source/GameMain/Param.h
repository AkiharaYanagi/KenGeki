//=================================================================================================
//
// パラメータヘッダ
//		ゲーム内でシーン間をまたぐ値
//
//=================================================================================================
#pragma once

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "Game.h"
#include "GameConst.h"
#include "GameSettingFile.h"
#include "SceneCommon.h"

//-------------------------------------------------------------------------------------------------
// 宣言
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	class Param	: public GameParam
	{
		//ゲーム設定 ( 外部ファイル読込 )
		GameSettingFile		m_setting;

		//内部設定
		MUTCH_MODE			m_mutchMode = MODE_PLAYER_PLAYER;

		//リザルト用
		PLAYER_ID		m_winner;		//勝者
		int				m_n_life_1p;	//残ライフ
		int				m_n_life_2p;	//
		int				m_n_input_1p;	//入力数
		int				m_n_input_2p;	//
		int				m_n_act_1p;		//アクション回数
		int				m_n_act_2p;		//
		int				m_n_offset;		//相殺数

	public:
		Param ();
		Param ( const Param & rhs );	//コピー可能
		~Param ();

		GameSettingFile GetGameSetting () const { return m_setting; }
		void SetSettingFile ( GameSettingFile stg ) { m_setting = stg; }

		void SetMutchMode ( MUTCH_MODE mode );
		void SetMutchMode ( PLAYER_MODE mode1p, PLAYER_MODE mode2p );
		MUTCH_MODE GetMutchMode () const { return m_mutchMode; }

		PLAYER_MODE GetPlayerMode ( PLAYER_ID id ) const;

		void SetRandomChara ();


		//リザルト用
		void SetWinner ( PLAYER_ID plr ) { m_winner = plr; }
		PLAYER_ID GetWinner () const { return m_winner; }

		void SetN_Life1p ( int n ) { m_n_life_1p = n; }
		int GetN_Life1p () const { return m_n_life_1p; }
		void SetN_Life2p ( int n ) { m_n_life_2p = n; }
		int GetN_Life2p () const { return m_n_life_2p; }

		void SetN_Input1p ( int n ) { m_n_input_1p = n; }
		int GetN_Input1p () const { return m_n_input_1p; }
		void SetN_Input2p ( int n ) { m_n_input_2p = n; }
		int GetN_Input2p () const { return m_n_input_2p; }

		void SetN_Act1p ( int n ) { m_n_act_1p = n; }
		int GetN_Act1p () const { return m_n_act_1p; }
		void SetN_Act2p ( int n ) { m_n_act_2p = n; }
		int GetN_Act2p () const { return m_n_act_2p; }
	};

	using P_Param = std::shared_ptr < Param >;


}	//namespace GAME

