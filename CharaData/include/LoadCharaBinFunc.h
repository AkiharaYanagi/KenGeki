//=================================================================================================
//
// LoadCharaBinFunc ヘッダファイル
//
//=================================================================================================
#pragma once
//#include "Game.h"
#include "Chara.h"
#include "LoadCharaBinUtl.h"

//-------------------------------------------------------------------------------------------------
// 宣言
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	using P_CH = std::shared_ptr < char [] >;
	using L_UINT = unique_ptr < UINT [] >;

	//------------------------------------------------------------
	//	LoadCharaBinで用いる関数群
	//------------------------------------------------------------
	class LoadCharaBinFunc
	{
		LoadCharaBinUtl		m_utl;


	public:
		LoadCharaBinFunc ();
		LoadCharaBinFunc ( const LoadCharaBinFunc & rhs ) = delete;
		~LoadCharaBinFunc () = default;

		void LoadChara ( P_CH buf, UINT & pos, Chara & ch );
		void LoadCharaScript ( P_CH buf, UINT & pos, Chara & ch );
		void LoadCharaImage ( P_CH buf, UINT & pos, Chara & ch );

		void LoadBehavior ( P_CH buf, UINT & pos, Chara & ch );
		void LoadGarnish ( P_CH buf, UINT & pos, Chara & ch );

		void LoadCommand ( P_CH buf, UINT & pos, Chara & ch );
		void LoadBranch ( P_CH buf, UINT & pos, Chara & ch );
		void LoadRoute ( P_CH buf, UINT & pos, Chara & ch );

		void LoadScript ( P_CH buf, UINT & pos, Script & scp );
		void LoadScpPrm_Btl ( P_CH buf, UINT & pos, Script & scp );
		void LoadScpPrm_Stg ( P_CH buf, UINT & pos, Script & scp );

		void LoadImg ( P_CH buf, UINT & pos, PVP_TxBs pvp_txBs );

	};


}	//namespace GAME


