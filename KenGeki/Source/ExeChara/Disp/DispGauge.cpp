//=================================================================================================
//
// DispGauge ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "DispGauge.h"

//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	const float DispGauge::UNIT_LGS = 1.f * GAUGE_W / GAUGE_MAX;//1単位あたりの表示長さ
	const float DispGauge::LIFE_Y_REV = -20;
//	const float DispGauge::LIFE_Y_REV = -0;
//	const float DispGauge::LIFE_H_REV = +50;
	const float DispGauge::LIFE_H_REV = +0;



	DispGauge::DispGauge ()
	{
#if 0
		//枠
		m_Frame = make_shared < PrmRect > ();
		m_Frame->SetZ ( Z_SYS );
		AddpTask ( m_Frame );
		GRPLST_INSERT_MAIN ( m_Frame );

		//白ダメージ
		m_white = make_shared < PrmRect > ();
		m_white->SetZ ( Z_SYS );
		AddpTask ( m_white );
		GRPLST_INSERT_MAIN ( m_white );

		//減少分
		m_Decrease = make_shared < PrmRect > ();
		m_Decrease->SetZ ( Z_SYS );
		AddpTask ( m_Decrease );
		GRPLST_INSERT_MAIN ( m_Decrease );

		//現在値
		m_Value = make_shared < PrmRect > ();
		m_Value->SetZ ( Z_SYS );
		AddpTask ( m_Value );
		GRPLST_INSERT_MAIN ( m_Value );

		m_white->SetColorN ( 0, 0xffffffff );
		m_white->SetColorN ( 1, 0xffc0c0c0 );
		m_white->SetColorN ( 2, 0xffffffff );
		m_white->SetColorN ( 3, 0xffe0e0e0 );
		m_white->SetValid ( F );

#endif // 0

		//画像

		//枠
		m_grp_Frame = std::make_shared < GrpAcv > ();
		m_grp_Frame->AddTexture ( _T("gauge_frame_life_2p.png") );
		m_grp_Frame->AddTexture ( _T("gauge_frame_life_2p.png") );
		m_grp_Frame->SetZ ( Z_SYS + 0.05f );
		m_grp_Frame->SetScalingCenter ( VEC2 ( 0, 64.f ) );
		AddpTask ( m_grp_Frame );
		GRPLST_INSERT_MAIN ( m_grp_Frame );

		//値
		m_grp_Value = std::make_shared < GrpAcv > ();
		m_grp_Value->AddTexture ( _T("gauge_value_life_2p.png") );
		m_grp_Value->AddTexture ( _T("gauge_value_life_2p.png") );
		m_grp_Value->SetZ ( Z_SYS );
		m_grp_Value->SetScalingCenter ( VEC2 ( 0, 64.f ) );
		AddpTask ( m_grp_Value );
		GRPLST_INSERT_MAIN ( m_grp_Value );

		//減少分(赤)
		m_grp_Decrease = std::make_shared < GrpAcv > ();
		m_grp_Decrease->AddTexture ( _T("gauge_value_life_2p.png") );
		m_grp_Decrease->AddTexture ( _T("gauge_value_life_2p.png") );
		m_grp_Decrease->SetZ ( Z_SYS + 0.04f );
		m_grp_Decrease->SetScalingCenter ( VEC2 ( 0, 64.f ) );
		AddpTask ( m_grp_Decrease );
		GRPLST_INSERT_MAIN ( m_grp_Decrease );

		//回復分(白)
		m_grp_White = std::make_shared < GrpAcv > ();
		m_grp_White->AddTexture ( _T("gauge_white_life_2p.png") );
		m_grp_White->AddTexture ( _T("gauge_white_life_2p.png") );
		m_grp_White->SetZ ( Z_SYS + 0.01f );
		m_grp_White->SetScalingCenter ( VEC2 ( 0, 64.f ) );
		AddpTask ( m_grp_White );
		GRPLST_INSERT_MAIN ( m_grp_White );
	}

	DispGauge::~DispGauge ()
	{
	}

	void DispGauge::GrpOff ()
	{
		m_grp_Frame->SetValid ( F ); 
		m_grp_Value->SetValid ( F ); 
		m_grp_Decrease->SetValid ( F ); 
		m_grp_White->SetValid ( F ); 
	}

	void DispGauge::GrpOn ()
	{
		m_grp_Frame->SetValid ( T ); 
		m_grp_Value->SetValid ( T ); 
		m_grp_Decrease->SetValid ( T ); 
		m_grp_White->SetValid ( T ); 
	}


	void DispGauge::SetPosition ( float x, float y, float w, float h )
	{
		m_base_x = x;
		m_base_y = y;
		m_base_w = w;
		m_base_h = h;
	}

	void DispGauge::SetColor_Frame ( _CLR c )
	{
//		m_Frame->SetAllColor ( c );
	}

	void DispGauge::SetColor_Decrease ( _CLR c )
	{
//		m_Decrease->SetAllColor ( c );
		m_grp_Decrease->SetColor ( c );
	}

	void DispGauge::SetColor_Value ( _CLR c0, _CLR c1, _CLR c2, _CLR c3 )
	{
#if 0
		m_Value->SetColorN ( 0, c0 );
		m_Value->SetColorN ( 1, c1 );
		m_Value->SetColorN ( 2, c2 );
		m_Value->SetColorN ( 3, c3 );
#endif // 0

//		m_grp_Value->SetColor ( 0xffff0000 );
	}

	void DispGauge::LoadPlayer ( PLAYER_ID id )
	{
		m_playerID = id;
		Init ();
	}

	void DispGauge::Init ()
	{
		m_value = LIFE_MAX;
		m_dcr = LIFE_MAX;

		//計算要 初期位置
		float x = m_base_x;
		float y = m_base_y + LIFE_Y_REV;
		float w = m_base_w;
		float h = m_base_h;

		if ( PLAYER_ID_1 == m_playerID )
		{
			const float X = x + 512;
			m_grp_Frame->SetPos ( X, y );
			m_grp_Frame->SetScaling ( VEC2 ( -1.f, 1.f ) );
			m_grp_Value->SetPos ( X, y );
			m_grp_Value->SetScaling ( VEC2 ( -1.f, 1.f ) );
			m_grp_Decrease->SetPos ( X, y );
			m_grp_Decrease->SetScaling ( VEC2 ( -1.f, 1.f ) );
			m_grp_White->SetPos ( X, y );
			m_grp_White->SetScaling ( VEC2 ( -1.f, 1.f ) );
		}
		else if ( PLAYER_ID_2 == m_playerID )
		{
			float p2_bx_l = GAME_WINDOW_WIDTH - x - w;
			const float W = 512;
			float p2_bx = GAME_WINDOW_WIDTH - x - W;	//W = テクスチャサイズ
			m_grp_Frame->SetPos ( p2_bx, y );
			m_grp_Value->SetPos ( p2_bx, y );
			m_grp_Decrease->SetPos ( p2_bx, y );
			m_grp_White->SetPos ( p2_bx, y );

			//2P用テクスチャを指定
			m_grp_Frame->SetIndexTexture ( 1 );
			m_grp_Value->SetIndexTexture ( 1 );
			m_grp_Decrease->SetIndexTexture ( 1 );
			m_grp_White->SetIndexTexture ( 1 );
		}
	}


	//値の更新
	//◆毎フレーム 実行
	void DispGauge::Update ( UINT value )
	{
		m_value = value;
		float ln = UNIT_LGS * m_value;	//表示長さ
		float ln_d = UNIT_LGS * m_dcr;	//ダメージ表示長さ

		float x = m_base_x;
		float y = m_base_y;
		float w = m_base_w;
		float h = m_base_h;
		const float Y = y + LIFE_Y_REV;
		const LONG H = (LONG)( h + LIFE_H_REV );

		//毎フレーム ダメージ表示を減少させる
		if ( ln < ln_d )
		{
			m_dcr -= 10;
		}

		//表示
		if ( PLAYER_ID_1 == m_playerID )
		{
			//@info テクスチャレクトを変更するとき、テクスチャサイズは変更しない
			m_grp_Value->SetPos ( x + 6 + w, Y );
			m_grp_Value->SetRect ( 6, 0, (LONG)(ln + 6), H );

			m_grp_Decrease->SetPos ( x + 6 + w, Y );
			m_grp_Decrease->SetRect ( 6, 0, (LONG)(ln_d + 6), H );
		}
		else if ( PLAYER_ID_2 == m_playerID )
		{
			float p2_bx_0 = GAME_WINDOW_WIDTH - x;
			float p2_bx_l = GAME_WINDOW_WIDTH - x - w;

			const float W = 512;
			float p2_bx = GAME_WINDOW_WIDTH - x - W;	//W = テクスチャサイズ
			float d = w - ln;

			m_grp_Value->SetPos ( p2_bx, Y );
			m_grp_Decrease->SetPos ( p2_bx, Y );

//			m_grp->SetPos ( p2_bx_l, y - 50 );
//			m_grp->SetRect ( (LONG)d, 0, (LONG)ln, (LONG)h );
			m_grp_Value->SetRect ( 2, 0, (LONG)(w - d), H );
			m_grp_Decrease->SetRect ( 2, 0, (LONG)d, H );
		}

	}

	void DispGauge::UpdateWhite ( int white )
	{
		float ln = UNIT_LGS * m_value;	//表示長さ
		float wht = UNIT_LGS * white;	//表示長さ

		float x = m_base_x;
		float y = m_base_y + LIFE_Y_REV;
		float w = m_base_w;
		float h = m_base_h + LIFE_H_REV;

		//メイン値のサイズを取得
		float vx = m_grp_Value->GetPos().x;
		LONG vl = m_grp_Value->GetpObject(0)->GetlpRect()->left;
		LONG vr = m_grp_Value->GetpObject(0)->GetlpRect()->right;
		float vw = (float)( vr - vl );

		if ( PLAYER_ID_1 == m_playerID )
		{
//			m_white->SetRect ( x - ln, y + 1, ln, h - 2 );
//			m_grpWhite->SetPos ( 6 + x - wht, y + 100 );
			m_grp_White->SetPos ( x + 6 + w, y );
//			m_grp_White->SetPos ( x, y + 40 );
//			m_grpWhite->SetRect ( 6 + (LONG)(512 - w - wht), 0, (LONG)512, H );
			m_grp_White->SetRect ( 6, 0, (LONG)(ln + 6 + wht), (LONG)h );

//			DBGOUT_WND_F ( 4, _T("vx - vw = %f"), vx - vw );
		}
		else if ( PLAYER_ID_2 == m_playerID )
		{
//			m_white->SetRect ( x + w, y + 1, ln, h - 2 );
		}


	}


	//最大値変更
	void DispGauge::ChangeMax ( float percent )
	{
#if 0
		//初期位置
		float x = m_base_x;
		float y = m_base_y;
		float w = m_base_w * percent * 0.01f;
		float h = m_base_h;

		if ( PLAYER_ID_1 == m_playerID )
		{
			float x0 = x + ( m_base_w - w );
			m_Frame->SetRect ( x0 - 2, y - 2, w + 4, h + 4 );
		}
		else if ( PLAYER_ID_2 == m_playerID )
		{
			float p2_bx_l = GAME_WINDOW_WIDTH - x - m_base_w;
			m_Frame->SetRect ( p2_bx_l - 2, y - 2, w + 4, h + 4 );
		}
#endif // 0
	}


	void DispGauge::On ()
	{
#if 0
		m_Value->SetValid ( T );
		m_Frame->SetValid ( T );
		m_Decrease->SetValid ( T );
#endif // 0
		m_grp_Value->SetValid ( T ); 
		m_grp_Frame->SetValid ( T ); 
	}

	void DispGauge::Off ()
	{
#if 0
		m_Value->SetValid ( F );
		m_Frame->SetValid ( F );
		m_Decrease->SetValid ( F );
#endif // 0
		m_grp_Value->SetValid ( F ); 
		m_grp_Frame->SetValid ( F ); 
	}


}	//namespace GAME

