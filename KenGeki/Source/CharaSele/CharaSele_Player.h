//=================================================================================================
//
//	CharaSele Player ヘッダファイル
//
//=================================================================================================
#pragma once

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "Game.h"
#include "CharaSeleConst.h"


//-------------------------------------------------------------------------------------------------
// 宣言
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	class CharaSele_Player : public TASK_VEC
	{
		//プレイヤ識別
		PLAYER_ID		m_player_id { PLAYER_ID_1 };

		//現在位置
		CHARA_SELE_ID	m_chslct_id { CHSLID_0 };

		//キャラ立絵
		P_GrpAcv		m_chara_stand;

		//カーソル
		P_GrpBlink		m_cursor;

		//枠位置
		static const ChSlct		m_pos [ CHARA_SELE_NUM ];

	public:
		CharaSele_Player ();
		CharaSele_Player ( const CharaSele_Player & rhs ) = delete;
		~CharaSele_Player ();

		void PlayerInit ( PLAYER_ID id );

		void Move ();

	private:
		void SetCursorUp ( P_GrpBlink pCrs, CHARA_SELE_ID & id );
		void SetCursorDown ( P_GrpBlink pCrs, CHARA_SELE_ID & id );
		void SetCursorLeft ( P_GrpBlink pCrs, CHARA_SELE_ID & id );
		void SetCursorRight ( P_GrpBlink pCrs, CHARA_SELE_ID & id );
	};


	using P_ChSele_Pl = shared_ptr < CharaSele_Player >;


}	//namespace GAME


