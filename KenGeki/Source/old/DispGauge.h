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

		P_PrmRect	m_Value;		//値
		P_PrmRect	m_Frame;		//枠
		P_PrmRect	m_Decrease;		//減少分表示
		float		m_d;			//減少分値
		P_PrmRect	m_white;		//一時値		//白ダメージ

		//画像
		P_GrpAcv	m_grp_Value;	//値
		P_GrpAcv	m_grp_Frame;	//枠
		P_GrpAcv	m_grp_Decrease;	//減少分
		P_GrpAcv	m_grp_White;	//一時値		//白ダメージ


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
		void UpdateWhite ( int white );

		//最大値変更(値のみ、内部数値はBrlParamで制限)
		// 初期値 GAUGE_WIDTH ("FtgConst.h") のパーセントで変更 (0%~100%)
		void ChangeMax ( float percent );

		//表示切替
		void On ();
		void Off ();


		//白ゲージ切替
		void OnWhite () { m_white->SetValid ( T ); }
		void OffWhite () { m_white->SetValid ( F ); }


		//画像
		void GrpOn ();
		void GrpOff ();

	private:
		static const float UNIT_LGS;	//1単位あたりの長さ
		static const float LIFE_Y_REV;	//ライフゲージ補正
		static const float LIFE_H_REV;	//ライフゲージ補正
	};

	using P_DispGauge = std::shared_ptr < DispGauge >;


}	//namespace GAME

