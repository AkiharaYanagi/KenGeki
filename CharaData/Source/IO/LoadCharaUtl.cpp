//=================================================================================================
//
// LoadCharaUtl ソースファイル
//
//=================================================================================================
#include "LoadCharaUtl.h"

//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//-------------------------------------------------------------------
	//	内部使用関数
	//-------------------------------------------------------------------
	//エレメントポインタからindex番目のアトリビュートのValueをUINTに直して返す
	UINT LoadCharaUtl::_ElemToUINT ( const P_Element pElem, UINT indexAttr )
	{
		UINT ui = 0;
		try
		{
			const PVP_Attribute pvpAttr = pElem->GetpvpAttribute();
			tistringstream tiss ( (*pvpAttr)[ indexAttr ]->GetValue() );
			tiss >> ui;
		}
		catch (...) { TRACE_F ( _T("Error _ElemToUINT.\n") ); }
		return ui;
	}
	
	//------------------------------------------------------------
	//名前の保存
	void LoadCharaUtl::ElemToNameArray ( const P_Element pElem, V_STR & vec )
	{
		PVP_Element pvp_elem = pElem->GetpvpElement ();

#if 0
		for ( P_Element pe : * pvp_elem )
		{
			PVP_Attribute pvp_attr = pe->GetpvpAttribute ();
			vec.push_back ( (*pvp_attr)[0]->GetValue () );		//名前のアトリビュートは０の位置
		}
#endif // 0

		//要素数を数え上げ先にリサイズで確保する
		vec.resize ( pvp_elem->size () );

		//値の代入
		UINT i = 0;
		for ( P_Element pe : * pvp_elem )
		{
			PVP_Attribute pvp_attr = pe->GetpvpAttribute ();
			vec[i] = ( *pvp_attr ) [ 0 ]->GetValue ();		//名前のアトリビュートは０の位置
			++ i;
		}
	}

	UINT LoadCharaUtl::IndexOf ( const V_STR& vstr, tstring name ) const
	{
		UINT ret = 0;
		for ( tstring tstr : vstr )
		{
			if ( 0 == tstr.compare ( name ) ) { break; }
			++ ret;
		}
		return ret;
	}

	//------------------------------------------------------------
	//アトリビュートポインタからValueをboolに直して返す
	bool LoadCharaUtl::_AttrToBool ( const P_Attribute pAttr )
	{
		bool b = false;
		try
		{
			b = pAttr->GetValue().compare ( _T("True") ) == 0;
		}
		catch (...) { TRACE_F ( _T("Error _AttrToBool.\n") ); }
		return b;
	}

	//アトリビュートポインタからValueをUINTに直して返す
	UINT LoadCharaUtl::_AttrToUINT ( const P_Attribute pAttr )
	{
		UINT ui = 0;
		try
		{
			tistringstream tiss ( pAttr->GetValue() );
			tiss >> ui;
		}
		catch (...){ TRACE_F ( _T("Error _AttrToUINT.\n") ); }
		return ui;
	}

	//アトリビュートポインタからValueをintに直して返す
	int LoadCharaUtl::_AttrToInt ( const P_Attribute pAttr )
	{
		int i = 0;
		try
		{
			tistringstream tiss ( pAttr->GetValue() );
			tiss >> i;
		}
		catch (...)	{	TRACE_F ( _T("Error _AttrToInt.\n") );	}
		return i;
	}

	//アトリビュートポインタからValueをfloatに直して返す
	float LoadCharaUtl::_AttrToFloat ( const P_Attribute pAttr )
	{
		float f = 0;
		try
		{
			tistringstream tiss ( pAttr->GetValue() );
			tiss >> f;
		}
		catch (...)	{	TRACE_F ( _T("Error _AttrToFloat.\n") );	}
		return f;
	}

	//アトリビュートポインタ２つからValueをVEC2に直して返す
	VEC2 LoadCharaUtl::_AttrToVec2 ( const P_Attribute pa0, const P_Attribute pa1 )
	{
		return VEC2 ( _AttrToFloat ( pa0 ), _AttrToFloat ( pa1 ) );
	}

	//アトリビュートポインタ４つからValueをRECTに直して返す
	//引数：x, y, w, h
	RECT LoadCharaUtl::_AttrToRect ( const P_Attribute pa0, const P_Attribute pa1, const P_Attribute pa2, const P_Attribute pa3 )
	{
		//x0, y0, x1, y1に直す
		int x = _AttrToInt ( pa0 );
		int y = _AttrToInt ( pa1 );
		int w = _AttrToInt ( pa2 );
		int h = _AttrToInt ( pa3 );

		RECT rect = { x, y, x + w, y + h };
		return rect;
	}

	//アトリビュートポインタからValueをCLC_STに直して返す
	CLC_ST LoadCharaUtl::_AttrToCLC_ST ( const P_Attribute pAttr )
	{
		CLC_ST clcSt = (CLC_ST)_AttrToInt ( pAttr );
#if 0
		tstring str = pAttr->GetValue ();
		if ( 0 == str.compare ( _T ( "CLC_MAINTAIN" ) ) ) { clcSt = CLC_MAINTAIN; }
		else if ( 0 == str.compare ( _T ( "CLC_SUBSTITUDE" ) ) ) { clcSt = CLC_SUBSTITUDE; }
		else if ( 0 == str.compare ( _T ( "CLC_ADD" ) ) ) { clcSt = CLC_ADD; }
#endif // 0
		return clcSt;
	}

	//アトリビュートポインタからValueをACTION_POSTUREに直して返す
	ACTION_POSTURE LoadCharaUtl::_AttrToACTION_POSTURE ( const P_Attribute pAttr )
	{
		ACTION_POSTURE ap = (ACTION_POSTURE)_AttrToInt ( pAttr );
#if 0
		tstring str = pAttr->GetValue ();
		if ( 0 == str.compare ( _T ( "AP_STAND" ) ) ) { ap = AP_STAND; }
		else if ( 0 == str.compare ( _T ( "AP_CROUCH" ) ) ) { ap = AP_CROUCH; }
		else if ( 0 == str.compare ( _T ( "AP_JUMP" ) ) ) { ap = AP_JUMP; }
#endif // 0
		return ap;
	}





}	//namespace GAME

