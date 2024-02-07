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
		VEC2	m_posMutualBase;

		float	m_chara_center_x { 0 };

		//立ち位置リセットフラグ
		//条件：画面端で "足払い" がヒット時
		//効果：背景を中央に移動する
		bool	m_bResetPos { F };

		//壁位置
		float	m_wall_L { 0 };	//左
		float	m_wall_R { 0 };	//右

	public:
		//画面端での表示基準位置
		void CulcPosMutualBase ( VEC2 pos1p, VEC2 pos2p );
//		void SetPosMutualBase ( VEC2 v ) { m_posMutualBase = v; }
		VEC2 GetPosMutualBase () const { return m_posMutualBase; }

		float GetCharaCenterX () const { return m_chara_center_x; }

		//立ち位置リセットフラグ
		bool GetResetPos () const { return m_bResetPos; }
		void SetResetPos ( bool b ) { m_bResetPos = b; }

		//壁位置
		float GetWallLeft () const { return m_wall_L; }
		float GetWallRight () const { return m_wall_R; }
	};

	using  UP_G_Ftg = unique_ptr < G_Ftg >;


#define G_FTG	G_Ftg::inst()
#define G_BASE_POS	G_Ftg::inst()->GetPosMutualBase


}	//namespace GAME


