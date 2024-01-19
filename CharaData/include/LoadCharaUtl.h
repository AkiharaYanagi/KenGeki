//=================================================================================================
//
// LoadCharaUtl ヘッダファイル
//
//=================================================================================================
#pragma once
#include "Define.h"
#include "Document.h"

#include "IOCharaDefine.h"

//-------------------------------------------------------------------------------------------------
// 宣言
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//------------------------------------------------------------
	//	LoadCharaで用いるUtility関数群
	//------------------------------------------------------------
	class LoadCharaUtl
	{
	public:
		LoadCharaUtl () = default;
		LoadCharaUtl ( const LoadCharaUtl & rhs ) = delete;
		~LoadCharaUtl () = default;


		//------------------------------------------------------------
		//エレメントポインタからindex番目のアトリビュートのValueをUINTに直して返す
		UINT _ElemToUINT ( const P_Element pElem, UINT indexAttr );

		//名前の保存
		void ElemToNameArray ( const P_Element pElem, V_STR & vec );

		//名前からインデックスを取得
		UINT IndexOf ( const V_STR& vstr, tstring name ) const;

		//------------------------------------------------------------
		//アトリビュート変換 (文字列はAttributeのGetValue()を用いる)
		bool _AttrToBool ( const P_Attribute pAttr );
		UINT _AttrToUINT ( const P_Attribute pAttr );
		int  _AttrToInt ( const P_Attribute pAttr );
		float  _AttrToFloat ( const P_Attribute pAttr );
		VEC2 _AttrToVec2 ( const P_Attribute pa0, const P_Attribute pa1 );
		RECT _AttrToRect
		( 
			const P_Attribute pa0, const P_Attribute pa1,  
			const P_Attribute pa2, const P_Attribute pa3  
		);

		CLC_ST _AttrToCLC_ST ( const P_Attribute pAttr );
		ACTION_POSTURE _AttrToACTION_POSTURE ( const P_Attribute pAttr );

	};


}	//namespace GAME


