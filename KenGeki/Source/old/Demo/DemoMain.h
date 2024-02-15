//=================================================================================================
//
//	Demo ヘッダファイル
//		タイトルに代わるシーンとしてデモ状態を監理する
//
//=================================================================================================
#pragma once

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "Game.h"
#include "../FtgMain/FtgMain.h"	
#include "../GameMain/Scene.h"
#include "DemoMenu.h"


//-------------------------------------------------------------------------------------------------
// 宣言
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	class DemoMain : public Scene, public enable_shared_from_this < DemoMain >
	{
		//メニュ
		P_DemoMenu	m_menu;

		//戦闘
		P_FTG		m_fighting;

		//ポーズ
		P_GrpAcv	m_pause;

		//遷移シーン
		P_GameScene		m_scene;

	public:
		DemoMain ();
		DemoMain ( const DemoMain & rhs ) = delete;
		~DemoMain ();

		void ParamInit ();

		P_GameScene Transit ();

		//シーン変更
		void To_1v2 ();
		void To_1vC ();
		void To_Cv2 ();
		void To_CvC ();

	private:
		void Pause ();
	};


	using P_DemoMain = std::shared_ptr < DemoMain >;


}	//namespace GAME


