//=================================================================================================
//
// DispRect ヘッダファイル
//
//=================================================================================================
#pragma once

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "Game.h"
#include "../../FtgMain/G_Ftg.h"
#include "../../GameMain/GameConst.h"
#include "../Rect/CharaRect.h"

//-------------------------------------------------------------------------------------------------
// 宣言
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	using VP_PrmRect = std::vector < P_PrmRect >;
	using PVP_PrmRect = std::shared_ptr < VP_PrmRect >;

	class DispRect : public TASK_VEC
	{
		//参照実効枠
		P_CharaRect		mp_CharaRect;

		//表示枠
		PVP_PrmRect		m_pvpGrpCRect;
		PVP_PrmRect		m_pvpGrpARect;
		PVP_PrmRect		m_pvpGrpHRect;
		PVP_PrmRect		m_pvpGrpORect;

	public:
		DispRect ();
		DispRect ( const DispRect & rhs ) = delete;
		~DispRect ();

		//実効枠設定
		void SetCharaRect ( P_CharaRect pCharaRect );
	
		//表示枠の初期化
		void InitRect ();

		//更新
		void Update ();

		//------------------
		//表示状態に変更
		void OnRect ();

		//非表示状態に変更
		void OffRect ();

		//-------------------

		//表示枠設定
		void SetGrpRect ( PVP_PrmRect pvpGrpRect, PV_RECT pvRect );
		void SetCRect ( PV_RECT pvRect ) { SetGrpRect ( m_pvpGrpCRect, pvRect ); }
		void SetARect ( PV_RECT pvRect ) { SetGrpRect ( m_pvpGrpARect, pvRect ); }
		void SetHRect ( PV_RECT pvRect ) { SetGrpRect ( m_pvpGrpHRect, pvRect ); }
		void SetORect ( PV_RECT pvRect ) { SetGrpRect ( m_pvpGrpORect, pvRect ); }

	private:
		//初期化用
		P_PrmRect Make ( DWORD clr );
	};

	using P_DispRect = std::shared_ptr < DispRect >;


}	//namespace GAME


