//=================================================================================================
//
// LoadChara ソースファイル
//
//=================================================================================================
#include "LoadCharaBin.h"

//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//==================================================================================
	//	LoadChara
	//		スクリプトとイメージリストを保存したキャラデータ ".dat"ファイル から、Charaを読み込む
	//==================================================================================
	LoadCharaBin::LoadCharaBin ( const tstring & filename, Chara & chara )
	{
		try
		{
			_Load ( filename, chara );
		}
		catch ( LPCTSTR lpctstr )
		{
			TRACE_F ( lpctstr );
		}
		catch (...)
		{
			TRACE_F ( _T("Error LoadCharaBin.\n") );
		}
	}

	//----------------------------------
	//	デストラクタ
	//----------------------------------
	LoadCharaBin::~LoadCharaBin ()
	{
	}


	//------------------------------------------
	//	try用実行関数
	//------------------------------------------
	void LoadCharaBin::_Load ( const tstring & filename, Chara & chara )
	{


		DWORD startTime = ::timeGetTime ();




		//---------------------------------------------------------------------
		//ファイルストリーム開始
		ifstream ifstrm ( filename.c_str (), ios::binary );

		//ファイルが存在しないとき何もしない
		if ( ifstrm.fail () ) { throw _T ( "ファイルが存在しません。" ); }

		//キャラアーカイブファイル形式のバージョン
		UINT version = 0;	//読込用
		ifstrm.read ( (char*)& version, sizeof ( UINT ) );
		const UINT CHARA_VERSION = 110;
		if ( CHARA_VERSION != version ) { throw _T ( "キャラアーカイブのバージョンが異なります。" ); }

		//---------------------------------------------------------------------
		//全体のサイズ
		UINT scriptSize = 0;
		ifstrm.read ( (char*)& scriptSize, sizeof ( UINT ) );

		//一時読込
		P_CH scriptBuf = make_unique < char [] > ( scriptSize );
		ifstrm.read ( scriptBuf.get (), scriptSize );
		UINT pos = 0;	//メモリポインタ



		DWORD scriptRead_Time = ::timeGetTime ();



		//------------------------------------------

		//キャラ読込

		//スクリプト
		m_func.LoadCharaScript ( scriptBuf, pos, chara );



		DWORD LoadScript_Time = ::timeGetTime ();



		//イメージ
		m_func.LoadCharaImage ( scriptBuf, pos, chara );

		//---------------------------------------------------------------------
		//ファイルストリーム終了
		ifstrm.close ();


		DWORD LoadImage_Time = ::timeGetTime ();


		DWORD t0 = scriptRead_Time - startTime;
		DWORD t2 = LoadScript_Time - scriptRead_Time;
		DWORD t3 = LoadImage_Time - LoadScript_Time;
//		DBGOUT_WND_F ( 6, _T ( "scriptRead_Time = %d, LoadScript_Time = %d, LoadImage_Time = %d" ), t0, t2, t3 );
	}


}	//namespace GAME

