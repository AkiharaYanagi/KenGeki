//=================================================================================================
//
// DispEffect ヘッダファイル
//
//=================================================================================================
#pragma once

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "Game.h"
#include "Chara.h"
#include "../../FtgMain/G_Ftg.h"
#include "../Disp/DispRect.h"

//-------------------------------------------------------------------------------------------------
// 宣言
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	class DispEffect : public TASK_VEC
	{
		P_GrpApTx		m_grp;			//グラフィック表示
		PVP_TxBs		m_pvpEfTx;		//エフェクトイメージのテクスチャリスト
		P_DispRect		m_dispRect;		//エフェクト枠表示

	public:
		DispEffect ( PVP_TxBs pvpEfTx, float z );
		DispEffect ( const DispEffect & rhs ) = delete;
		~DispEffect ();
		
		//有効フラグ設定
		void SetValid ( bool b ) { m_grp->SetValid ( b ); }

		//更新
		void Update ( P_Script pScript, VEC2 ptEf, bool dirRight );

		//表示枠設定
		void SetpCharaRect ( P_CharaRect pCharaRect );

		//枠表示
		void OnRect () { m_dispRect->OnRect (); }
		//枠非表示
		void OffRect () { m_dispRect->OffRect (); }
	};

	using P_DispEffect = std::shared_ptr < DispEffect >;


}	//namespace GAME


