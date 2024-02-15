//=================================================================================================
//
// LoadCharaBinUtl ソースファイル
//
//=================================================================================================
#include "LoadCharaBinUtl.h"
#include <codecvt>

//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	LoadCharaBinUtl::LoadCharaBinUtl ()
	{
		::setlocale ( LC_ALL, "japanese" );
	}


	//-----------------------------------------------------------------------
	tstring LoadCharaBinUtl::LoadText ( P_CH buf, UINT & pos )
	{
		//名前のサイズ
		byte length = buf [ pos ++ ];	//Encoding.UTF8

//		unique_ptr < char [] > arypChar = make_unique < char [] > ( length + 1 );
//		memcpy_s ( arypChar.get(), length ,buf, length );
		
//		unique_ptr < TCHAR [] >  tbuf = make_unique < TCHAR [] > ( length + 1 );
//		size_t _PptNumOfCharConverted = 0;
//		errno_t err = ::mbstowcs_s ( &_PptNumOfCharConverted, tbuf.get (), length + 1, arypChar.get (), _TRUNCATE );

		//UTF8 -> wstring
		string str ( buf.get() + pos, length );
		std::wstring_convert < std::codecvt_utf8_utf16 < wchar_t > > converter;
		tstring tstr = converter.from_bytes ( str );

		//位置を更新
		pos += length;

		//取得した名前を返す
		return tstr ;
	}


	byte LoadCharaBinUtl::LoadByte ( P_CH buf, UINT & pos )
	{
		//1バイト読み込んでポインタを進め、値を返す
		byte b = buf [ pos ++ ];
		return b;
	}

	int LoadCharaBinUtl::LoadInt ( P_CH buf, UINT & pos )
	{
		//リトルエンディアン読込 (byte[])0x67 0x45 0x23 0x01 -> (int)0x01234567
		int i = 0;
		rsize_t size = sizeof ( int );
		::memcpy_s ( &i, size, buf.get () + pos, size );
		pos += size;
		return i;
	}

	UINT LoadCharaBinUtl::LoadUInt ( P_CH buf, UINT & pos )
	{
		//リトルエンディアン読込 (byte[])0x67 0x45 0x23 0x01 -> (UINT)0x01234567
		UINT i = 0;
		rsize_t size = sizeof ( UINT );
		::memcpy_s ( &i, size, buf.get () + pos, size );
		pos += size;
		return i;
	}


	VEC2 LoadCharaBinUtl::LoadVec2 ( P_CH buf, UINT & pos )
	{
		int pos_x = LoadInt ( buf, pos );
		int pos_y = LoadInt ( buf, pos );
		return VEC2 ( (float)pos_x, (float)pos_y );
	}


	RECT LoadCharaBinUtl::LoadRect ( P_CH buf, UINT & pos )
	{
		//リトルエンディアン読込 (byte[])0x67 0x45 0x23 0x01 -> (int)0x01234567
		RECT rect = { 0 };
		rsize_t size = sizeof ( RECT );
		::memcpy_s ( &rect, size, buf.get () + pos, size );
		pos += size;
		return rect;
	}

	
	void LoadCharaBinUtl::LoadListRect ( P_CH buf, UINT & pos, PV_RECT pvRect )
	{
		UINT n = buf [ pos ++ ];
		pvRect->clear ();
		pvRect->resize ( n );
		for ( UINT i = 0; i < n; ++ i )
		{
			( *pvRect ) [ i ] = LoadRect ( buf, pos );
		}
	}


	L_UINT LoadCharaBinUtl::LoadAryUint ( P_CH buf, UINT & pos, UINT & refLength )
	{
		refLength = buf [ pos ++ ];

		unique_ptr < UINT [] > ary_uint = make_unique < UINT [] > ( refLength );
		for ( UINT i = 0; i < refLength; ++ i )
		{
			ary_uint [ i ] = (UINT)buf [ pos ++ ];
		}

		return ::move ( ary_uint );
	}


	void LoadCharaBinUtl::LoadAryUint ( P_CH buf, UINT & pos, V_UINT & refAryUint )
	{
		UINT size = LoadUInt ( buf, pos );
		refAryUint.resize ( size );
		for ( UINT i = 0; i < size; ++ i )
		{
			refAryUint [ i ] = LoadUInt ( buf, pos );
		}
	}


}	//namespace GAME

