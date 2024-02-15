//=================================================================================================
//
//	Fighting ヘッダファイル
//
//=================================================================================================
#pragma once

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "Game.h"
#include "FtgConst.h"	
#include "MutualChara.h"
#include "../GameMain/Scene.h"
#include "FtgDemo.h"

//-------------------------------------------------------------------------------------------------
// 宣言
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	class Fighting : public TASK_LST
	{
		//背景
		P_GrpAcv		m_bg;
		P_GrpAcv		m_bg_blackout;

		//test カベ表示 背景
		P_GrpAcv		m_wall_L;
		P_GrpAcv		m_wall_R;

		//サブ背景位置差分
		float			m_subWall_dx { - GAME_WIDTH };


		//キャラ相互処理
		P_MutualChara	m_mutualChara;

		//画面固定システム表示
		//ゲージ枠
		P_GrpAcv		m_gauge_frame;

		//=====================================================
		//デモ
		P_FtgDemoActor	m_demoActor;
		bool			m_demoSkip;	//スキップ

		//=====================================================

		//暗転
		P_Timer			m_tmrBlackOut;

		//ポーズ
		P_GrpAcv		m_pause;

	public:
		Fighting ();
		Fighting ( const Fighting & rhs ) = delete;
		~Fighting ();

		void Init ();
		void Move ();

		void ParamInit ( P_Param pParam );

		bool IsEnd () const { return m_demoActor->IsEnd (); }

		//初期操作 プレイヤ/CPU 設定
		void Set_1P_vs_2P () { m_mutualChara->Set_1P_vs_2P (); }
		void Set_1P_vs_CPU () { m_mutualChara->Set_1P_vs_CPU (); }
		void Set_CPU_vs_CPU () { m_mutualChara->Set_CPU_vs_CPU (); }

		//初期デモスキップ設定
		void SetDemoOn () { m_demoSkip = F; }
		void SetDemoSkip () { m_demoSkip = T; }

		//トレーニング用リスタート
		void TrainingRestart ();

	private:
		void Pause ();

		//共通グラフィック処理
		void Grp ();
	};

	using P_FTG = std::shared_ptr < Fighting >;


}	//namespace GAME


