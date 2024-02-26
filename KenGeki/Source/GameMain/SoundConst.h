//=================================================================================================
//
// SoundConst ファイル
//		音声ファイル指定用 定数
//
//=================================================================================================
#pragma once

//-------------------------------------------------------------------------------------------------
//	インクルードファイル
//-------------------------------------------------------------------------------------------------

//-------------------------------------------------------------------------------------------------
//	定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//------------------------------------------
	// 定数宣言
	//------------------------------------------
	//Soundフォルダ、
	//SEフォルダ内の読込ファイル順(ABC)にIDが振られる

	//BGM
	enum BGM_ID
	{
//		BGM_Title,
//		BGM_Intro,
		BGM_Main,
//		BGM_Result,
	};

	//SE
	enum SE_ID
	{
		SE_Sys_Select,
		SE_Sys_Enter,
		SE_Sys_Cancel,
		SE_Sys_EnterFighting,
		SE_Btl_Clang,
		SE_Btl_Hit,
		SE_Btl_Landing,
		SE_Btl_Dash,
		SE_Btl_Jump,
	};

}

