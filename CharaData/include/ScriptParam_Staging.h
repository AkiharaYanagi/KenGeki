//=================================================================================================
//
//	ScriptParam_Staging ヘッダファイル
//
//=================================================================================================
#pragma once

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
//#include "Game.h"
#include "Define.h"
#include "DxDefine.h"

//-------------------------------------------------------------------------------------------------
// 宣言
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	struct ScriptParam_Staging
	{
	public:
		//演出(全体)
		UINT	BlackOut;		//暗転[F]
		UINT	Vibration;		//振動[F](全体)
		UINT	Stop;			//停止[F](全体)

		//------
		//演出(個別)
		int		Rotate;				//回転[rad]
		VEC2	Rotate_center;		//回転中心(x,y)
		UINT	AfterImage_N;		//残像[個]
		UINT	AfterImage_time;	//残像[F] 持続
		UINT	AfterImage_pitch;	//残像[F] pitch
		UINT	Vibration_S;		//振動[F](個別)
		_CLR	Color;				//色調変更
		UINT	Color_time;			//色調変更[F] 持続

		//------
		UINT	ID_SE;				//SEの指定

		//=======================================
		ScriptParam_Staging ();
		ScriptParam_Staging ( const ScriptParam_Staging & rhs ) = delete;
		~ScriptParam_Staging ();
		//=======================================


		//---------------------------------------------------------
		//暗転
		UINT GetBlackOut () const { return BlackOut; }
		void SetBlackOut ( UINT blackout ) { BlackOut = blackout; }

		//振動
		UINT GetVibration () const { return Vibration; }
		void SetVibration ( UINT viberation ) { Vibration = viberation; }

		//停止
		UINT GetStop () const { return Stop; }
		void SetStop ( UINT stop ) { Stop = stop; }

		//---------------------------------------------------------
		//回転
		int GetRadian () const { return Rotate; }
		void SetStop ( int rad ) { Rotate = rad; }

		//残像[F] pitch
		int GetAfterImage_pitch () const { return AfterImage_pitch; }
		void SetAfterImage_pitch ( int pitch ) { AfterImage_pitch = pitch; }

		//残像[個]
		int GetAfterImage_N () const { return AfterImage_N; }
		void SetAfterImage_N ( int n ) { AfterImage_N = n; }

		//残像[F] 持続
		int GetAfterImage_time () const { return AfterImage_time; }
		void SetAfterImage_time ( int time ) { AfterImage_time = time; }

		//振動[F](個別)
		int GetVibration_S () const { return Vibration_S; }
		void SetVibration_S ( int time ) { Vibration_S = time; }

		//色調
		_CLR GetColor () const { return Color; }
		void SetColor ( _CLR clr ) { Color = clr; }

		//色調変更[F] 持続
		int GetColor_time () const { return Color_time; }
		void SetColor_time ( int time ) { Color_time = time; }

		//---------------------------------------------------------
		//SE
		UINT GetID_SE () const { return ID_SE; }
		void SetID_SE ( UINT id ) { ID_SE = id; }
	};


}	//namespace GAME


