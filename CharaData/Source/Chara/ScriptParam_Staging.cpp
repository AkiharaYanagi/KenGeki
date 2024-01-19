//=================================================================================================
//	ScriptParam_Staging ソースファイル
//=================================================================================================
#include "ScriptParam_Staging.h"

//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	ScriptParam_Staging::ScriptParam_Staging ()
		: BlackOut ( 0 ), Vibration ( 0 ), Stop ( 0 )
		, Rotate ( 0 ), AfterImage_N ( 0 ), AfterImage_time ( 0 ), AfterImage_pitch ( 0 )
		, Vibration_S ( 0 ), Color ( 0xffffffff ), Color_time ( 0 )
	{
	}

	ScriptParam_Staging::~ScriptParam_Staging ()
	{
	}


}	//namespace GAME

