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

	DispGauge::DispGauge ()
		: m_playerID ( PLAYER_ID_1 )
		, m_base_x ( 0 ), m_base_y ( 0 ), m_base_w ( 0 ), m_base_h ( 0 )
		, m_d ( LIFE_MAX )
	{
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

		//初期化
		Init ();
	}

	DispGauge::~DispGauge ()
	{
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
		m_Frame->SetAllColor ( c );
	}

	void DispGauge::SetColor_Decrease ( _CLR c )
	{
		m_Decrease->SetAllColor ( c );
	}

	void DispGauge::SetColor_Value ( _CLR c0, _CLR c1, _CLR c2, _CLR c3 )
	{
		m_Value->SetColorN ( 0, c0 );
		m_Value->SetColorN ( 1, c1 );
		m_Value->SetColorN ( 2, c2 );
		m_Value->SetColorN ( 3, c3 );
	}

	void DispGauge::LoadPlayer ( PLAYER_ID id )
	{
		m_playerID = id;
		Init ();
	}

	void DispGauge::Init ()
	{
		m_d = LIFE_MAX;
		m_Decrease->SetRect ( 0, 0, 0, 0 );

		//初期位置
		float x = m_base_x;
		float y = m_base_y;
		float w = m_base_w;
		float h = m_base_h;

		if ( PLAYER_ID_1 == m_playerID )
		{
			m_Frame->SetRect ( x - 2, y - 2, w + 4, h + 4 );
			m_Value->SetRect ( x, y, w, h );
			m_white->SetRect ( x, y, w, h );
		}
		else if ( PLAYER_ID_2 == m_playerID )
		{
			float p2_bx_l = GAME_WINDOW_WIDTH - x - w;
			m_Frame->SetRect ( p2_bx_l - 2, y - 2, w + 4, h + 4 );
			m_Value->SetRect ( p2_bx_l, y, w, h );
			m_white->SetRect ( p2_bx_l, y, w, h );
		}
	}

	void DispGauge::Update ( UINT value )
	{
		float ln = UNIT_LGS * value;	//表示長さ
		float ln_d = UNIT_LGS * m_d;	//ダメージ表示長さ

		float x = m_base_x;
		float y = m_base_y;
		float w = m_base_w;
		float h = m_base_h;

		if ( ln < ln_d )
		{
			m_d -= 10;
		}

		if ( PLAYER_ID_1 == m_playerID )
		{
			m_Decrease->SetRect ( x + w - ln_d, y, ln_d, h );
			m_Value->SetRect ( x + w - ln, y, ln, h );
		}
		else if ( PLAYER_ID_2 == m_playerID )
		{
			float p2_bx_l = GAME_WINDOW_WIDTH - x - w;
			m_Decrease->SetRect ( p2_bx_l, y, ln_d, h );
			m_Value->SetRect ( p2_bx_l, y, ln, h );
		}
	}

	void DispGauge::UpdateWhite ( int white )
	{
		float ln = UNIT_LGS * white;	//表示長さ
		VEC2 pos = m_Value->GetPos ();	//メイン値の位置を取得
		VEC2 size = m_Value->GetSize ();	//メイン値のサイズを取得
		float x = pos.x;
		float y = m_base_y;
		float w = size.x;
		float h = m_base_h;

		if ( PLAYER_ID_1 == m_playerID )
		{
			m_white->SetRect ( x - ln, y + 1, ln, h - 2 );
		}
		else if ( PLAYER_ID_2 == m_playerID )
		{
			m_white->SetRect ( x + w, y + 1, ln, h - 2 );
		}
	}


	//最大値変更
	void DispGauge::ChangeMax ( float percent )
	{
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
	}


	void DispGauge::On ()
	{
		m_Value->SetValid ( T );
		m_Frame->SetValid ( T );
		m_Decrease->SetValid ( T );
	}

	void DispGauge::Off ()
	{
		m_Value->SetValid ( F );
		m_Frame->SetValid ( F );
		m_Decrease->SetValid ( F );
	}


}	//namespace GAME

