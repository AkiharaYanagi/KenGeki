//=================================================================================================
//
// DispChara ヘッダファイル
//
//=================================================================================================
#pragma once

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "Game.h"
#include "Chara.h"
#include "../../GameMain/GameConst.h"
#include "../../FtgMain/G_Ftg.h"
#include "../../GameMain/Param.h"
#include "DispMainImage.h"
#include "DispRect.h"
#include "DispFrontEnd.h"
#include "DispInput.h"


//-------------------------------------------------------------------------------------------------
// 宣言
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	class DispChara : public TASK_VEC
	{
		P_DispMainImage	m_mainImage;	//メインイメージ
		P_DispRect		m_dispRect;		//枠
		P_GrpAcv		m_grpShadow;	//影
		P_DispFrontEnd	m_frontEnd;		//フロントエンド
		P_DispInput		m_dispInput;	//入力表示


	public:
		DispChara ();
		DispChara ( const DispChara & rhs ) = delete;
		~DispChara ();

		void ParamInit ( P_Param pParam );


		//キャラを設定する
		void SetpChara ( const P_Chara pChara );

		//メインイメージの更新
		void UpdateMainImage ( P_Script pScript, VEC2 ptChara, bool dirRight );

		//表示枠設定
		void SetpCharaRect ( P_CharaRect pCharaRect );


		//枠表示ON
		void OnRect () { m_dispRect->OnRect (); }
		//枠表示OFF
		void OffRect () { m_dispRect->OffRect (); }

		//PLAYER / CPU
		void SetControl_PLAYER () { m_frontEnd->SetPlayer (); }
		void SetControl_CPU () { m_frontEnd->SetCPU (); }


		//ゲージ類の表示部のみ初期化
		void LoadPlayer ( PLAYER_ID playerID );

		//ゲージ類更新
		void UpdateGauge ( BtlParam btlPrm );

		//入力更新
		void UpdateInput ( P_CharaInput p ) { m_dispInput->UpdateInput ( p ); }
	};


	using P_DispChara = std::shared_ptr < DispChara >;


}	//namespace GAME


