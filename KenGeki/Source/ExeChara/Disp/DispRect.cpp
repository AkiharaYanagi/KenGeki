//=================================================================================================
//
// DispRect ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "DispRect.h"

//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	DispRect::DispRect () 
	{
		//枠表示を初期化
		m_pvpGrpCRect = make_shared < VP_PrmRect > ();
		m_pvpGrpARect = make_shared < VP_PrmRect > ();
		m_pvpGrpHRect = make_shared < VP_PrmRect > ();
		m_pvpGrpORect = make_shared < VP_PrmRect > ();

		for ( UINT i = 0; i < NUM_RECT; ++i )
		{
#if 0
			P_PrmRect pCRect = make_shared < PrmRect > ();
			pCRect->SetAllColor ( RECT_COLOR_C );
			pCRect->SetZ ( Z_SYS );
			pCRect->Load ();
			m_pvpGrpCRect->push_back ( pCRect );
			AddpTask ( pCRect );
			GRPLST_INSERT_MAIN ( pCRect );

			P_PrmRect pARect = make_shared < PrmRect > ();
			pARect->SetAllColor ( RECT_COLOR_A );
			pARect->SetZ ( Z_SYS );
			pARect->Load ();
			pARect->Move ();
			m_pvpGrpARect->push_back ( pARect );
			AddpTask ( pARect );
			GRPLST_INSERT_MAIN ( pARect );

			P_PrmRect pHRect = make_shared < PrmRect > ();
			pHRect->SetAllColor ( RECT_COLOR_H );
			pHRect->SetZ ( Z_SYS );
			pHRect->Load ();
			pHRect->Move ();
			m_pvpGrpHRect->push_back ( pHRect );
			AddpTask ( pHRect );
			GRPLST_INSERT_MAIN ( pHRect );

			P_PrmRect pORect = make_shared < PrmRect > ();
			pORect->SetAllColor ( RECT_COLOR_O );
			pORect->SetZ ( Z_SYS );
			pORect->Load ();
			pORect->Move ();
			m_pvpGrpORect->push_back ( pORect );
			AddpTask ( pORect );
			GRPLST_INSERT_MAIN ( pORect );
#endif // 0
			m_pvpGrpCRect->push_back ( Make ( RECT_COLOR_C ) );
			m_pvpGrpARect->push_back ( Make ( RECT_COLOR_A ) );
			m_pvpGrpHRect->push_back ( Make ( RECT_COLOR_H ) );
			m_pvpGrpORect->push_back ( Make ( RECT_COLOR_O ) );

		}
		InitRect ();

		//----------------------------------------------------
		//@info		GRPLSTに動的に追加するときは既に全体がLoad()されているので、
		//			明示的にLoad()が必要
		//----------------------------------------------------

		OnRect ();
	}

	P_PrmRect DispRect::Make ( DWORD clr )
	{
		P_PrmRect p = make_shared < PrmRect > ();
		p->SetAllColor ( clr );
		p->SetSize ( VEC2 ( 0, 0 ) );
		p->SetZ ( Z_SYS );
		p->Load ();
		p->Move ();
		AddpTask ( p );
		GRPLST_INSERT_MAIN ( p );
		return p;
	}

	DispRect::~DispRect ()
	{
		//終了時にグラフィックタスクを外す
		for ( UINT i = 0; i < NUM_RECT; ++i )
		{
			GRPLST_REMOVE_MAIN ( (*m_pvpGrpCRect)[i] );
			GRPLST_REMOVE_MAIN ( (*m_pvpGrpARect)[i] );
			GRPLST_REMOVE_MAIN ( (*m_pvpGrpHRect)[i] );
			GRPLST_REMOVE_MAIN ( (*m_pvpGrpORect)[i] );
		}
		//解放
		for ( UINT i = 0; i < NUM_RECT; ++i )
		{
			m_pvpGrpCRect->clear ();
			m_pvpGrpARect->clear ();
			m_pvpGrpHRect->clear ();
			m_pvpGrpORect->clear ();
		}
	}

	//実効枠設定
	void DispRect::SetCharaRect ( P_CharaRect pCharaRect )
	{
		mp_CharaRect = pCharaRect;
	}

	//更新
	void DispRect::Update ()
	{
		SetCRect ( mp_CharaRect->GetpvCRect () );
		SetARect ( mp_CharaRect->GetpvARect () );
		SetHRect ( mp_CharaRect->GetpvHRect () );
		SetORect ( mp_CharaRect->GetpvORect () );
	}

	//表示枠の初期化
	void DispRect::InitRect ()
	{
		for ( P_PrmRect p : * m_pvpGrpCRect ) { p->SetZero (); }
		for ( P_PrmRect p : * m_pvpGrpARect ) { p->SetZero (); }
		for ( P_PrmRect p : * m_pvpGrpHRect ) { p->SetZero (); }
		for ( P_PrmRect p : * m_pvpGrpORect ) { p->SetZero (); }
	}

	//表示
	void DispRect::OnRect ()
	{
		for ( P_PrmRect p : * m_pvpGrpCRect ) { p->SetValid ( true ); }
		for ( P_PrmRect p : * m_pvpGrpARect ) { p->SetValid ( true ); }
		for ( P_PrmRect p : * m_pvpGrpHRect ) { p->SetValid ( true ); }
		for ( P_PrmRect p : * m_pvpGrpORect ) { p->SetValid ( true ); }
	}

	//非表示
	void DispRect::OffRect ()
	{
		for ( P_PrmRect p : * m_pvpGrpCRect ) { p->SetValid ( false ); }
		for ( P_PrmRect p : * m_pvpGrpARect ) { p->SetValid ( false ); }
		for ( P_PrmRect p : * m_pvpGrpHRect ) { p->SetValid ( false ); }
		for ( P_PrmRect p : * m_pvpGrpORect ) { p->SetValid ( false ); }
	}

	//実効枠から表示枠へ設置
	void DispRect::SetGrpRect ( PVP_PrmRect pvpGrpRect, PV_RECT pvRect )
	{
		float bx = G_BASE_POS().x;	//基準位置

		UINT i = 0;
		for ( P_PrmRect p : * pvpGrpRect )
		{
			RECT r = pvRect->at ( i );
			float x = bx + r.left;
			float y = (float)r.top;
			float w = (float)( r.right - r.left );
			float h = (float)( r.bottom - r.top );
			p->SetRect ( x, y, w, h );
			++ i;
		}
	}

}	//namespace GAME

