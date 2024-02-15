//=================================================================================================
//
//	タイトル カーソル
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
	//==============================================================
	//メニュ　初期状態
	class Title_Cursor : public GrpAcv
	{
		int			m_count;
		static const float		OMEGA;
		float		m_dir;

	public:
		Title_Cursor ();
		Title_Cursor ( const Title_Cursor & rhs ) = delete;
		~Title_Cursor ();

		void Move ();

		void Off ();
		void On ();

		void Reverse () { m_dir = -1.f; }
	};
	using P_Title_Cursor = std::shared_ptr < Title_Cursor >;


}	//namespace GAME

