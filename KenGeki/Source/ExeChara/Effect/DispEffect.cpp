//=================================================================================================
//
// DispEffect ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "DispEffect.h"

//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	DispEffect::DispEffect ( PVP_TxBs pvpEfTx , float z ) 
		: m_pvpEfTx ( pvpEfTx )
	{
		//メイングラフィック
		m_grp = make_shared < GrpApTx > ();
		m_grp->SetZ ( z );	//初期位置
		AddpTask ( m_grp );
		GRPLST_INSERT_MAIN ( m_grp );

		//枠表示
		m_dispRect = make_shared < DispRect > ();
		AddpTask ( m_dispRect );
	}

	DispEffect::~DispEffect ()
	{
		//終了時にグラフィックタスクを外す
		EraseTask ( m_dispRect );
		EraseTask ( m_grp );
		GRPLST_REMOVE_MAIN ( m_grp );
	}


	void DispEffect::Update ( P_Script pScript, VEC2 ptEf, bool dirRight )
	{
		//Disp：スクリプトのイメージ表示位置

		//テクスチャの取得
		P_TxBs pEfTx = m_pvpEfTx->at ( pScript->GetImageIndex() );

		//位置 (エフェクトのゲーム位置＋スクリプトの表示位置(*向き)＋キャラによる画面補正位置)
		float fDir = dirRight ? (1.f) : (-1.f);
		VEC2 tempImgPos = pScript->GetPos ();
		VEC2 imgPos = VEC2( fDir * tempImgPos.x, tempImgPos.y );
		VEC2 vecEfImg = ptEf + imgPos + G_BASE_POS ();

//		DBGOUT_WND_F ( 2, _T ( "DispEfPos = (%d,%d)" ), vecEfImg.x, vecEfImg.y );

		//表示に反映
		m_grp->SetPos ( vecEfImg );
		m_grp->SetScaling ( 1.f * fDir, 1.f );
		m_grp->SetpTexture ( pEfTx );
	}

	void DispEffect::SetpCharaRect ( P_CharaRect pCharaRect )
	{
		m_dispRect->SetCharaRect ( pCharaRect );
	}


}	//namespace GAME

