//=================================================================================================
//
// エグゼキャラ　ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "ExeChara.h"


//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//================================================
	//	アクション遷移中、特定アクション
	// 戻値　T : 遷移可能なのでその値で先に進む
	//		F : 遷移不可なので戻り先ループ内でconinue;して飛ばす
	//================================================
	// 
	//アクション移項（コマンドに関する処理）
	bool ExeChara::TranditAction_Special ( P_Action pAct )
	{
		//------------------------------------------
		//対象IDがバランス消費で移項不可能なら次へ

		//------------------------------------------
		//空中ダッシュ回数による遷移不可処理
		if ( pAct->IsName ( _T ( "空中ダッシュ" ) ) )
		{
			UINT n = m_btlPrm.GetNAirDash ();
			if ( 0 < n )
			{ return F; }
		}

		//超必　遷移チェック
		
		if ( pAct->IsName ( _T("超雷電蹴_発生") ) )
		{
			int i = 0;
		}
		// 
//		if ( pAct->IsName ( _T ( "超必殺発動" ) ) || pAct->IsName ( _T ( "超必B発動" ) ) )
		if ( pAct->GetCategory () == AC_OVERDRIVE )
		{
			//マナ消費
			if ( m_btlPrm.GetMana () >= MANA_HALF )
			{
				m_btlPrm.AddMana ( - MANA_HALF );
			}
			else //足りないとき遷移しない
			{
				return F;
			}
		}

		//不可能なら次をチェック
		//------------------------------------------

		return T;
	}



	//アクション移項（コマンドに関する処理）限定
	bool ExeChara::TranditAction_Command_Special ()
	{
		//-----------------------------------------------------
		// コマンドによる分岐
		//コマンドが完成したIDを優先順に保存したリスト
		m_pCharaInput->MakeTransitIDList ( *m_pChara, m_pScript, m_btlPrm.GetDirRight () );
		const std::vector < UINT > & vCompID = m_pCharaInput->GetvCompID ();

		UINT transitID = NO_COMPLETE;
		for ( UINT id : vCompID )
		{
			//遷移先チェック
			P_Action pAct = m_pChara->GetpAction ( id );

			if ( ! pAct->IsName ( _T ( "雷電蹴_0" ) ) )
			{
				continue;
			}
			
			//特殊アクション 除外 指定　：　不可能なら次をチェック
			if ( ! TranditAction_Special ( pAct ) )
			{
				continue;
			}

			//------------------------------------------

			//可能なら遷移先に指定して終了
			transitID = id;
			break;
		}

		//コマンドが完成していたら
		if ( NO_COMPLETE != transitID )
		{
			//現在アクションとして最後の処理
			EndAction ();

			//遷移先チェック
			P_Action pact = m_pChara->GetpAction ( transitID );
			P_Script pscr = pact->GetpScript ( 0 );

			//バランス処理
			int bl_a = pact->GetBalance ();		//アクション消費バランス
			m_btlPrm.AddBalance ( bl_a );

			//アクション遷移
			m_actionID = transitID;

			//スクリプト処理
			ExeScript ();

			//アクション移項を計測
			m_btlPrm.AddNActTrs ( 1 );

			//次フレームのスクリプトを１つ進める
			//今回フレームは取得済みのm_pScriptを用いる
			++ m_frame;

			//終了
			return T;
		}

		return F;
	}



}	//namespace GAME

