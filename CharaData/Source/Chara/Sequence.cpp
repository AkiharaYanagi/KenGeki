//=================================================================================================
//
//	Sequence ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "Sequence.h"

//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{

	Sequence::Sequence ()
		: m_name ( _T("new_sequence") )
		, m_next ( 0 ), m_nextName ( _T("next_sequence") )
	{
		m_pvpScript = make_shared < VP_Script > ();
	}

	Sequence::~Sequence ()
	{
	}

	void Sequence::Rele ()
	{
		for ( auto p : * m_pvpScript ) { p->Rele (); } m_pvpScript->clear (); m_pvpScript.reset ();
	}

	void Sequence::AddaScript ( unique_ptr < P_Script [] > aryScript, rsize_t size )
	{
		m_pvpScript->clear ();
		m_pvpScript->resize ( size );
		for ( UINT i = 0; i < size; ++ i )
		{
			( *m_pvpScript ) [ i ] = aryScript [ i ];
		}
	}


}	//namespace GAME

