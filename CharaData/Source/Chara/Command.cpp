//=================================================================================================
//
// Command ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "Command.h"

//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{

	Command::Command () :
		m_name (_T("command name")), m_limitTime ( 0 )
	{
	}

	Command::~Command ()
	{
		m_vecGameKey.clear ();
	}

	void Command::SetaGameKey ( _GameKeyCommand gkca [], UINT size )
	{
		m_vecGameKey.clear ();
		m_vecGameKey.resize ( size );
		for ( UINT i = 0; i < size; ++ i )
		{
			m_vecGameKey [ i ] = gkca [ i ];
		}
	}

	bool Command::Compare ( const V_GAME_KEY & vecGameKey, bool dirRight )
	{
		//キー配列長さか、コマンド受付時間の短い方で検索
		UINT len = vecGameKey.size ();
		UINT frame = ( m_limitTime < len ) ? m_limitTime : len;
		
		//完成入力数（カウントダウン）(判定は-1まで用いる)
		int comp = (int)m_vecGameKey.size() - 1;

		//フレームを遡って検索
		for ( UINT i = 0; i < frame; ++i )
		{
			if ( m_vecGameKey[comp].CompareTarget ( vecGameKey[i], dirRight ) )
			{
				--comp;
			}
			if ( 0 > comp ) { return true; }
		}
		return false;
	}


}	//namespace GAME

