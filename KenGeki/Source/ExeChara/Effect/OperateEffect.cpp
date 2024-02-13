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

	void OperateEffect::Init ()
	{
		for ( P_ExEf pEx : * m_plpExeEffect )
		{
			pEx->Init ();
		}
		m_plpExeEffect->clear();
		TASK_VEC::Init ();
	}

	//解放
	void OperateEffect::Rele ()
	{
		m_plpExeEffect->clear();
		TASK_VEC::Rele ();
	}

	//エフェクト事前生成
	void OperateEffect::MakeEfList ( P_Chara p )
	{
		//キャラポインタを保存
		m_pChara = p;
		m_pvpEfTexture = p->GetpvpEfTexture ();
		m_vpBranch = p->GetvpBranch ();
		m_vpRoute = p->GetvpRoute ();

		//すべてのアクションとスクリプトを巡回
		PVP_Action pvpAction = m_pChara->GetpvpAction ();
		for ( P_Action pAction : ( * pvpAction ) )
		{
			PVP_Script pvpScript = pAction->GetpvpScript ();
			for ( P_Script pScript : ( * pvpScript ) )
			{
				PVP_EfGnrt pvpEfGnrt = pScript->GetpvpEfGnrt ();
				for ( P_EfGnrt pEfGnrt : ( * pvpEfGnrt ) )
				{
					//非生成なら初回に登録しておき、IDで稼働状態にする
					if ( ! pEfGnrt->GetGnrt () )
					{
						//エフェクトインデックスの取得
						UINT index = pEfGnrt->GetIndex ();

						//エフェクトの取得
						P_Effect pEf = m_pChara->GetpEffect ( index );

						//エフェクト管理に渡してIDを得る
//						UINT id = m_oprtEf.Addstd::vectorEffect ( pEf, pEfGnrt->GetZ () );

						//IDを記録
//						pEfGnrt->SetID ( id );
					}
				}
			}
		}
	}

	void OperateEffect::MoveEffect ( P_Script pScp, BtlParam & btlprm )
	{
		//エフェクト生成
		if ( btlprm.GetFirstEf () )	//ヒット後の初回のみは動作
		{
			GenerateEffect ( pScp, btlprm );
			btlprm.SetFirstEf ( F );
		}
		else
		{
			if ( ! btlprm.GetTmr_HitStop ()->IsActive () )	//ヒットストップ時は生成しない
			{
				GenerateEffect ( pScp, btlprm );
			}
		}

		//エフェクト動作
		PreScriptMove ();

		//エフェクト同期
		PostScriptMove ( btlprm.GetPos (), btlprm.GetDirRight () );
	}

	void OperateEffect::GenerateEffect ( P_Script pScp, const BtlParam & btlprm )
	{
		//発生
		PVP_EfGnrt  pvpEfGnrt = pScp->GetpvpEfGnrt ();
		for ( P_EfGnrt pEfGnrt : ( *pvpEfGnrt ) )
		{
			//生成用なら
			if ( pEfGnrt->GetGnrt () )
			{
				//エフェクトインデックスの取得
				UINT index = pEfGnrt->GetIndex ();
				//エフェクトの取得
				P_Effect pEf = m_pChara->GetpEffect ( index );
				//リストに追加
				AddListEffect ( pEf, pEfGnrt, btlprm.GetPos (), btlprm.GetDirRight () );
			}
			else //再利用なら
			{
				int i = 0;
#if false
				//エフェクトインデックスの取得
				UINT index = pEfGnrt->GetIndex ();
				//エフェクトの取得
				P_Effect pEf = m_pChara->GetpEffect ( index );
				//稼働中かどうか
				if ( !m_oprtEf.IsActive ( pEf ) )
				{
					m_oprtEf.DriveEffect ( pEf );
				}
#endif // false
			}
		}
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

