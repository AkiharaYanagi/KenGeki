//=================================================================================================
//	ScriptParam_Battle ソースファイル
//=================================================================================================
#include "ScriptParam_Battle.h"

//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	ScriptParam_Battle::ScriptParam_Battle ()
		: Vel ( 0, 0 ), Acc ( 0, 0 ), Power ( 0 ), Warp ( 0 )
		, Recoil_I ( 0 ), Recoil_E ( 0 ), Balance_I ( 0 ), Balance_E ( 0 )
	{
	}

	ScriptParam_Battle::~ScriptParam_Battle ()
	{
	}


}	//namespace GAME

