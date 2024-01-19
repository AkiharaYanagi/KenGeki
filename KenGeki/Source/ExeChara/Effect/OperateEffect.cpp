//=================================================================================================
//
// OperateEffect ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "OperateEffect.h"

//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//コンストラクタ
	OperateEffect::OperateEffect ()
	{
		m_plpExeEffect = make_shared < LP_ExEf > ();
	}

	//デストラクタ
	OperateEffect::~OperateEffect ()
	{
		Rele ();
	}

	//解放
	void OperateEffect::Rele ()
	{
		m_plpExeEffect->clear();
	}

	void OperateEffect::SetpChara ( P_Chara p )
	{
		m_pChara = p;
		m_pvpEfTexture = p->GetpvpEfTexture ();
		m_vpBranch = p->GetvpBranch ();
		m_vpRoute = p->GetvpRoute ();
	}

	//エフェクトリストに新規追加
	void OperateEffect::AddListEffect ( P_Effect pEffect, P_EfGnrt pEfGnrt, VEC2 ptChara, bool dirRight )
	{
		P_ExEf pExeEffect = make_shared < ExeEffect > ( pEffect, m_pChara, pEfGnrt, ptChara, dirRight );
		m_plpExeEffect->push_back ( pExeEffect );
		AddpTask ( pExeEffect );	//タスクリスト
	}

	//オブジェクトからExeEfを取得
	P_ExEf OperateEffect::GetpExEf ( P_Effect p ) const
	{
		//エフェクト実行リストから検索
		for ( P_ExEf pExEf : (* m_plpExeEffect) )
		{
			if ( pExEf->Compare ( p ) ) { return pExEf; }
		}
		return nullptr;
	}

	//スクリプト前処理
	void OperateEffect::PreScriptMove ()
	{
		//動作
		for ( auto p : (*m_plpExeEffect) ) { p->PreScriptMove (); }
	}


	//スクリプト後処理
	void OperateEffect::PostScriptMove ( VEC2 ptChara, bool dirRight )
	{
		//list
		for ( auto p : (*m_plpExeEffect) )
		{
			p->PostScriptMove ( ptChara, dirRight );
		}

		//終了処理
		LP_ExEf::iterator it = begin ( * m_plpExeEffect );
		while ( it != end ( * m_plpExeEffect ) )
		{
			//消去時、後置インクリメントはコピーを渡しイテレータを壊さない
			if ( (*it)->IsEnd () ) 
			{ 
				EraseTask ( *it );	//タスクリスト
				(*it).reset (); 
				m_plpExeEffect->erase ( it ++ ); 
			}
			else { ++ it; }
		}
	}

	//スクリプト同期
	void OperateEffect::SynchroScript ( VEC2 ptChara )
	{
		//list
		for ( P_ExEf p : (*m_plpExeEffect) )
		{
			p->SynchroScript ( ptChara );
		}
	}

	//枠表示切替
	void OperateEffect::OnDispRect ()
	{
		for ( auto p : (*m_plpExeEffect) ) { p->OnDispRect (); }
	}

	void OperateEffect::OffDispRect ()
	{
		for ( auto p : (*m_plpExeEffect) ) { p->OffDispRect (); }
	}



}	//namespace GAME

