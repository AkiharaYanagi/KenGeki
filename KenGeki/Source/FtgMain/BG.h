//=================================================================================================
//
//	BG ヘッダファイル
//
//=================================================================================================
#pragma once

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "Game.h"
#include "FtgConst.h"	


//-------------------------------------------------------------------------------------------------
// 宣言
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	class BG : public TASK_LST
	{
		//背景
		P_GrpAcv		m_bg;
		P_PrmRect		m_bg_black;
		P_PrmRect		m_bg_white;

		//test カベ表示 背景
		P_GrpAcv		m_wall_L;
		P_GrpAcv		m_wall_R;

		//サブ背景位置差分
		float			m_subWall_dx { - GAME_WIDTH };

		//タイマ
		P_Timer			m_tmrBlackOut;		//暗転
		P_Timer			m_tmrWhiteOut;		//白転

		//壁割り
		P_GrpAcv		m_wall_break;
		P_Timer			m_tmrWallBreak;
		UINT			m_indexTexture { 0 };

	public:
		BG ();
		BG ( const BG & rhs ) = delete;
		~BG ();

		void Init ();
		void Move ();

		//共通グラフィック処理
		void Grp ();


		//条件確認
		bool IsBlackOut () const { return m_tmrBlackOut->IsActive (); }

		//暗転
		void SetBlackOut ( UINT n );

		//白転
		void SetWhiteOut ( UINT n );

		//壁割り
		void StartWallBreak_L ();
		void StartWallBreak_R ();
	};

	using P_BG = std::shared_ptr < BG >;


}	//namespace GAME


