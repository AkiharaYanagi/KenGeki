//=================================================================================================
//
// DispMainImage ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "DispMainImage.h"

//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{

	DispMainImage::DispMainImage ()
	{
		//メイングラフィック
		m_mainGraphic = make_shared < GrpApTx > ();
		AddpTask ( m_mainGraphic );
		GRPLST_INSERT_MAIN ( m_mainGraphic );
	}

	DispMainImage::~DispMainImage ()
	{
	}

	void DispMainImage::SetpChara ( P_Chara pChara )
	{
		m_pvpMainTexture = pChara->GetpvpMainTexture ();
	}

	void DispMainImage::UpdateMainImage ( P_Script pScript, VEC2 ptChara, bool dirRight )
	{
		//位置
		VEC2 posScript = pScript->GetPos ();
		float fDir = dirRight ? ( 1.f ) : ( -1.f );		//向き
		float bx = G_Ftg::inst ()->GetPosMutualBase ().x;	//基準位置
		float x = bx + ptChara.x + fDir * posScript.x;
		float y = 0 + ptChara.y + 1.f * posScript.y;
		VEC2 vecImg = VEC2 ( x, y );

		//表示に反映
		m_mainGraphic->SetPos ( vecImg );
		m_mainGraphic->SetScaling ( 1.f * fDir, 1.f );

		UINT index = pScript->GetImageIndex ();

		//@todo イメージ未定義や画像削除後のIDなどインデックスが無い場合がある
		if ( m_pvpMainTexture->size () < index ) { return; }

		P_TxBs pTexture = m_pvpMainTexture->at ( index );
		m_mainGraphic->SetpTexture ( pTexture );
	}

}	//namespace GAME

