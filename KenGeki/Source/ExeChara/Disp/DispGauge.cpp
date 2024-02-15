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

		float x = m_base_x;
		float y = m_base_y;
		float w = m_base_w;
		float h = m_base_h;

		//初期位置
		if ( PLAYER_ID_1 == m_playerID )
		{
			m_Frame->SetRect ( x - 2, y - 2, w + 4, h + 4 );
			m_Value->SetRect ( x, y, w, h );
		}
		else if ( PLAYER_ID_2 == m_playerID )
		{
			float p2_bx_l = GAME_WINDOW_WIDTH - x - w;
			m_Frame->SetRect ( p2_bx_l - 2, y - 2, w + 4, h + 4 );
			m_Value->SetRect ( p2_bx_l, y, w, h );
		}
	}

	void DispGauge::Init ()
	{
		m_d = LIFE_MAX;
		m_Decrease->SetRect ( 0, 0, 0, 0 );
	}

	void DispGauge::Update ( UINT value )
	{
		const static float cfl = 1.f * LIFE_GAUGE_W / LIFE_MAX;		//1ライフあたりの表示長さ

		float ln = cfl * value;	//表示長さ
		float ln_d = cfl * m_d;	//ダメージ表示長さ

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


}	//namespace GAME

