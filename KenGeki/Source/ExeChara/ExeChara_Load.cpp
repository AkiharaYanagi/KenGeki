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
	//	ロード関連 内部関数
	//================================================

	//シーンパラメータ関連初期化
	void ExeChara::ParamInit ( P_Param pParam )
	{
		//ゲーム設定
		GameSettingFile stg = pParam->GetGameSetting ();

		//選択キャラ名前・モードを取得
//		m_name = stg.GetName ( m_playerID );
		m_name = pParam->GetCharaName ( m_btlPrm.GetPlayerID () );
		m_playerMode = stg.GetPlayerMode ( m_btlPrm.GetPlayerID () );

		//入力
		LoadInput ();

		//表示
		m_dispChara->ParamInit ( pParam );
	}

	//------------------------
	//読込
	void ExeChara::Load ()
	{
		//--------------------------------------------
		//m_pCharaのデータ読込
		LoadCharaData ();

		//--------------------------------------------
		//キャラ表示初期化
		m_dispChara->SetpChara ( m_pChara );
		m_dispChara->SetpCharaRect ( m_charaRect );

		//アクタ・ステートに用いる状態パラメータに登録
		m_actor.SetwpExeChara ( shared_from_this () );

		//バトルパラメータに設定
		m_btlPrm.SetpChara ( m_pChara );
		m_btlPrm.SetwpExeChara ( shared_from_this (), m_pOther );

		//エフェクト生成ベクタの生成
		m_oprtEf->MakeEfList ( m_pChara );

		TASK_VEC::Load ();
	}

	//------------------------
	//初期化(複数回実行可能)
	void ExeChara::Init ()
	{
		//アクション・スクリプト初期化
		m_actionID = 0;
		m_pAction = m_pChara->GetpAction ( m_actionID );
		m_frame = 0;
		m_pScript = m_pAction->GetpScript ( m_frame );

		//バトルパラメータ
		m_btlPrm.Init ();
		SetParamFromScript ();	//スクリプトからパラメータを反映する

		//枠
		m_charaRect->ResetAllRect ();

		//状態
		m_actor.Init ();

		//表示
		//@info Move()中のTransit()の後に遷移し、
		//	再度Move()は呼ばれず(Init()は呼ばれる)、
		//	Draw()が呼ばれるため、ここで手動の初期化が必要
		m_dispChara->Update ( m_pAction, m_pScript, m_btlPrm, m_pCharaInput );

		//スロウタイマ
		m_tmrSlow.Init ();

		m_nameChangeOther = _T("");

		TASK_VEC::Init ();
	}

	//再設定
	void ExeChara::Reset ()
	{
		_Reset ();	//復旧時

		TASK_VEC::Reset ();
	}

	//------------------------
	//復旧時の再設定
	void ExeChara::_Reset ()
	{
		//テクスチャメモリ確保関連は再設定
		Rele ();
		m_pChara = make_shared < Chara > ();
		Load ();
		m_dispChara->SetpChara ( m_pChara );
		m_oprtEf->MakeEfList ( m_pChara );

		//アクション・スクリプト再取得
		m_pAction = m_pChara->GetpAction ( m_actionID );
		m_pScript = m_pAction->GetpScript ( m_frame );

		//表示の更新
		m_dispChara->Update ( m_pAction, m_pScript, m_btlPrm, m_pCharaInput );

		//エフェクト 表示のみ
		m_oprtEf->Disp ();
	}

	void ExeChara::LoadCharaData ()
	{
		//--------------------------------------------
		//m_pCharaのデータ読込

		//名前からスクリプトファイルを指定してキャラのロード
		//※	D3DXのテクスチャを用いるためフォーカス変更時などに再設定(Reset())が必要
		tstring name ( _T ( "charaBin.dat" ) );

		PLAYER_ID id = m_btlPrm.GetPlayerID ();

		if ( PLAYER_ID_1 == id )
		{
//			name.assign ( _T ( "charaBin.dat" ) );
			name.assign ( _T ( "chara_Sae_Bin.dat" ) ); m_name = CHARA_SAE;
//			name.assign ( _T ( "chara_Retsudou_Bin.dat" ) ); m_name = CHARA_RETSUDOU;
//			name.assign ( _T ( "chara_E0_Bin.dat" ) );
		}
		else if ( PLAYER_ID_2 == id )
		{
//			name.assign ( _T ( "charaBin.dat" ) );
			name.assign ( _T ( "chara_Sae_Bin.dat" ) ); m_name = CHARA_SAE;
//			name.assign ( _T ( "chara_Retsudou_Bin.dat" ) ); m_name = CHARA_RETSUDOU;
//			name.assign ( _T ( "chara_E0_Bin.dat" ) );

			//@info　キャラが複数に渡るとき、リアクション技指定をIDでなく名前で指定しないと位置がずれる
		}
//
		//パラメータによるキャラの選択
		switch ( m_name )
		{
//		case CHARA_OUKA: name.assign ( _T ( "Ouka.dat" ) ); break;
//		case CHARA_SAE : name.assign ( _T ( "Sae.dat" ) ); break;
		case CHARA_OUKA: name.assign ( _T ( "charaBin.dat" ) ); break;
		case CHARA_SAE : name.assign ( _T ( "chara_Sae_Bin.dat" ) ); break;
		default: break;
		}


		if ( PLAYER_ID_1 == id )
		{		
			//バイナリデータ読込
			LoadCharaBin loadCharaBin ( name.c_str (), *m_pChara );
		}
		else if ( PLAYER_ID_2 == id )
		{
			//名前が同じ時、すでに読み込んであるキャラデータを参照する
			if ( this->m_name == m_pOther.lock()->m_name )
			{
				m_pChara = m_pOther.lock()->m_pChara;
			}
			else
			{
				//バイナリデータ読込
				LoadCharaBin loadCharaBin ( name.c_str (), *m_pChara );
			}
		}
	}

	void ExeChara::LoadInput ()
	{
		//キャラ入力(プレイヤー, CPU, Network)
		m_pPlayerInput = make_shared < PlayerInput > ();
		m_pPlayerInput->SetPlayer ( m_btlPrm.GetPlayerID () );

		m_pCPUInput = make_shared < CPUInput > ( shared_from_this (), m_pOther );
		m_pCPUInput->SetPlayer ( m_btlPrm.GetPlayerID () );
		m_pCPUInput->Load ();

		//プレイヤモード(入力種類)による初期化
		switch ( m_playerMode )
		{
		case MODE_PLAYER: m_pCharaInput = m_pPlayerInput; break;
		case MODE_CPU: m_pCharaInput = m_pCPUInput; break;
		case MODE_NETWORK: m_pCharaInput = m_pPlayerInput; break;	//(仮)
		default: break;
		}
	}

}	//namespace GAME

