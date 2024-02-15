//=================================================================================================
//
//	FtgMain ヘッダファイル
//
//=================================================================================================
#pragma once

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "Game.h"
#include "../GameMain/Scene.h"
#include "Fighting.h"
#include "../Menu/PauseMenu.h"


//-------------------------------------------------------------------------------------------------
// 宣言
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	class FtgMain : public Scene, public enable_shared_from_this < FtgMain >
	{
#if 0
		//===========================================================
		//遷移先シーンポインタ
		//@info	thisを自身に保存すると循環参照となるのでweak_ptrを用いる
		WP_GameScene		mwp_This;
		P_GameScene			mp_Transit;		//遷移可能な次のシーン

		//===========================================================
#endif // 0

		//戦闘
		P_FTG			m_fighting;

		//ポーズメニュ
		P_PauseMenu		m_pauseMenu;

		//ロード中
		P_PrmRect		m_rectLoad;
		UINT			m_wait;
		P_GrpStr		m_NowLoading;

	public:
		FtgMain ();
		FtgMain ( const FtgMain & rhs ) = delete;
		~FtgMain ();

		void Load ();
		void ParamInit ();
		void Move ();

		//--------------------------
		//状態遷移
		P_GameScene Transit ();

		//--------------------------

		//初期操作 プレイヤ/CPU 設定
		void Set_1P_vs_2P () { m_fighting->Set_1P_vs_2P (); }
		void Set_1P_vs_CPU () { m_fighting->Set_1P_vs_CPU (); }
		void Set_CPU_vs_CPU () { m_fighting->Set_CPU_vs_CPU (); }
	};

	using P_FtgMain = std::shared_ptr < FtgMain >;


}	//namespace GAME


