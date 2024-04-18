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

	//表示のみ
	void OperateEffect::Disp ()
	{
		for ( P_ExEf pEx : * m_plpExeEffect ) { pEx->Disp (); }
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
//						UINT id = m_oprtEf.AddEffect ( pEf, pEfGnrt->GetZ () );

						//IDを記録
//						pEfGnrt->SetID ( id );
					}
				}
			}
		}
	}


	//スクリプト処理 前 エフェクト全体の動作
	void OperateEffect::PreMove ( P_Script pScp, BtlParam & btlprm )
	{
#if 0
		//	初回チェックはスクリプト側からは必要ないので、エフェクト内で処理する

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
#endif // 0
		
		GenerateEffect ( pScp, btlprm );

		//各エフェクトの動作
		for ( auto p : * m_plpExeEffect ) { p->PreScriptMove (); }
	}


	//スクリプト処理 後 エフェクト全体の動作
	void OperateEffect::PostMove ( BtlParam & btlprm )
	{
		//DBGOUT_WND_F は　ExeChara中で用いると２P側で上書きされる
		//DBGOUT_WND_F ( 8, _T ( "GRPLST->size() = %d" ), GrpLst::Inst()->GetNumList() );


		//各エフェクトの動作
		for ( auto p : * m_plpExeEffect ) { p->PostScriptMove ( btlprm ); }


		//終了処理
		

		//削除時イテレータ処理に問題あり
		
		
		LP_ExEf::iterator it = std::begin ( * m_plpExeEffect );
		while ( it != std::end ( * m_plpExeEffect ) )
		{
			//消去時、後置インクリメントはコピーを渡しイテレータを壊さない
			if ( (*it)->IsEnd () ) 
			{ 
				EraseTask ( *it );	//タスクリスト
				(*it).reset (); 
				it = m_plpExeEffect->erase ( it ); 
			}
			else { ++ it; }
		}
	}


	//エフェクト生成
	void OperateEffect::GenerateEffect ( P_Script pScp, const BtlParam & btlprm )
	{
		UINT NUM_GRP = GrpLst::Inst()->GetNumList();

		//発生
		PVP_EfGnrt  pvpEfGnrt = pScp->GetpvpEfGnrt ();
		for ( P_EfGnrt pEfGnrt : ( *pvpEfGnrt ) )
		{
			//エフェクトインデックスの取得
			UINT index = pEfGnrt->GetIndex ();
			//エフェクトの取得
			P_Effect pEf = m_pChara->GetpEffect ( index );

			//----------------------------------
			//	Ef特殊指定
			//----------------------------------
			if ( pEf->GetName () == _T("Inazuma") )
			{
			}
			 
			//----------------------------------
			//生成用なら
			if ( pEfGnrt->GetGnrt () )
			{
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


		UINT NUM_GRP0 = GrpLst::Inst()->GetNumList();


		P_ExEf pExeEffect = make_shared < ExeEffect > ( pEffect, m_pChara, pEfGnrt, ptChara, dirRight );


		UINT NUM_GRP1 = GrpLst::Inst()->GetNumList();


		m_plpExeEffect->push_back ( pExeEffect );
		AddpTask ( pExeEffect );	//タスクリスト
	}

	//オブジェクトからExeEfを取得
	P_ExEf OperateEffect::GetpExEf ( P_Effect p ) const
	{
		//エフェクト実行リストから検索
		for ( P_ExEf pExEf : * m_plpExeEffect )
		{
			if ( pExEf->Compare ( p ) ) { return pExEf; }
		}
		return nullptr;
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


	//---------------------------------------------------------------
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

