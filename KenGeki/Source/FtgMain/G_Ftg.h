//=================================================================================================
//
//	G_Ftg ヘッダファイル
//		Fighting共通グローバル変数群
//
//=================================================================================================
#pragma once

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "Game.h"
#include "../GameMain/GameConst.h"
#include "../FtgMain/FtgConst.h"

//-------------------------------------------------------------------------------------------------
// 宣言
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	class G_Ftg
	{
	//---------------------------------------------------------------------
	//シングルトンパターン
	private:
		using  _P_G_Ftg = unique_ptr < G_Ftg >;
		static _P_G_Ftg m_inst;		//シングルトンインスタンス
		G_Ftg ();		//private コンストラクタで通常の実体化は禁止
	public:
		~G_Ftg ();		//デストラクタはunique_ptrのためpublic
		static void Create() { if ( ! m_inst ) { m_inst = _P_G_Ftg ( new G_Ftg () ); } }
		static _P_G_Ftg & inst () { return m_inst; }	//インスタンス取得
	//---------------------------------------------------------------------

	private:
		//キャラ位置による画面補正量
		VEC2	m_posMutualBase { 0, 0 };

		//立ち位置リセットフラグ
		bool	m_bResetPos { F };

	public:
		//画面端での表示基準位置
		void CulcPosMutualBase ( VEC2 pos1p, VEC2 pos2p ) const;
		void SetPosMutualBase ( VEC2 v ) { m_posMutualBase = v; }
		VEC2 GetPosMutualBase () const { return m_posMutualBase; }

		//立ち位置リセットフラグ
		bool GetResetPos () const { return m_bResetPos; }
		void SetResetPos ( bool b ) { m_bResetPos = b; }
	};

	using  UP_G_Ftg = unique_ptr < G_Ftg >;
//#define G_FTG_STATE	G_Ftg::inst()->GetFightingState
//#define G_FTG_STATE_SET	G_Ftg::inst()->SetFightingState
#define G_BASE_POS	G_Ftg::inst()->GetPosMutualBase

}	//namespace GAME


