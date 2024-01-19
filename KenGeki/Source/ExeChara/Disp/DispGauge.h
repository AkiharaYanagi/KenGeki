//=================================================================================================
//
// DispGauge ヘッダファイル
//
//=================================================================================================
#pragma once

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "Game.h"
#include "Chara.h"
#include "../../FtgMain/G_Ftg.h"
#include "../BtlParam.h"

//-------------------------------------------------------------------------------------------------
// 宣言
//-------------------------------------------------------------------------------------------------
namespace GAME
{

	class DispGauge : public TASK_VEC
	{
		PLAYER_ID	m_playerID;		//プレイヤ表示側

		float		m_base_x;		//基準位置x
		float		m_base_y;		//基準位置y
		float		m_base_w;		//基準位置w
		float		m_base_h;		//基準位置h

		P_PrmRect	m_Value;		//表示
		P_PrmRect	m_Frame;		//枠
		P_PrmRect	m_Decrease;		//減少分表示
		float		m_d;		//減少分値


	public:
		DispGauge ();
		DispGauge ( const DispGauge & rhs ) = delete;
		~DispGauge ();

		//基準位置設定
		void SetPosition ( VEC2 xy, VEC2 wh ) { SetPosition ( xy.x, xy.y, wh.x, wh.y ); }
		void SetPosition ( float x, float y, float w, float h );

		//カラーの設定
		void SetColor_Frame ( _CLR c );
		void SetColor_Decrease ( _CLR c );
		void SetColor_Value ( _CLR c0, _CLR c1, _CLR c2, _CLR c3 );

		//ゲージ類の表示部のみ初期化
		void LoadPlayer ( PLAYER_ID id );

		//初期化
		void Init ();

		//減少分表示
		void OffDecrease () { m_Decrease->SetValid ( F ); }

		//値の更新
		void Update ( UINT value );
	};

	using P_DispGauge = std::shared_ptr < DispGauge >;


}	//namespace GAME

