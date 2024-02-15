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
	void ExeChara::LoadCharaData ()
	{
		//--------------------------------------------
		//m_pCharaのデータ読込

		//名前からスクリプトファイルを指定してキャラのロード
		//※	D3DXのテクスチャを用いるためフォーカス変更時などに再設定(Reset())が必要
		//		tstring name ( _T ("testChara.dat") );
		//		tstring name ( _T ( "chara.dat" ) );
		//		tstring name ( _T ("chara_standBin.dat") );
		tstring name ( _T ( "charaBin.dat" ) );
#if 0
		//パラメータによるキャラの選択
		switch ( m_name )
		{
		case CHARA_RAKUNO: name.assign ( _T ( "Rakuno.dat" ) ); break;
		case CHARA_YUKINO: name.assign ( _T ( "Yukino.dat" ) ); break;
		default: break;
		}
#endif // 0

		//バイナリデータ読込
		LoadCharaBin loadCharaBin ( name.c_str (), *m_pChara );
	}

	void ExeChara::LoadInput ()
	{
		//キャラ入力(プレイヤー, CPU, Network)
		m_pPlayerInput = make_shared < PlayerInput > ();
		m_pPlayerInput->SetPlayer ( m_playerID );

		m_pCPUInput = make_shared < CPUInput > ( shared_from_this (), m_pOther );
		m_pCPUInput->SetPlayer ( m_playerID );
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

