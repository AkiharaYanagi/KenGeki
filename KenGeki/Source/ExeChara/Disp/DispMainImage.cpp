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

	void DispMainImage::Init ()
	{
		TASK_VEC::Init ();
	}

	void DispMainImage::SetpChara ( P_Chara pChara )
	{
		m_pvpMainTexture = pChara->GetpvpMainTexture ();
	}

	void DispMainImage::UpdateMainImage ( P_Script pScript, VEC2 ptChara, bool dirRight )
	{
		//------------------------------------------------
		// テクスチャインデックス
		//IDチェックはスクリプト作成時、ロード時に行う
		UINT index = pScript->GetImageIndex ();
		P_TxBs pTexture = m_pvpMainTexture->at ( index );
		m_mainGraphic->SetpTexture ( pTexture );

		USIZE tx_size = pTexture->GetSize ();

		//------------------------------------------------
		//位置
		VEC2 posScript = pScript->GetPos ();
		float fDir = dirRight ? ( 1.f ) : ( -1.f );		//向き
		float bx = G_Ftg::inst ()->GetPosMutualBase ().x;	//基準位置
		float x = bx + ptChara.x + posScript.x;	//->ScalingCenterの指定により位置補正は無し
		float y =  0 + ptChara.y + posScript.y;

		//向き
		//ゲーム画面のカメラ補正位置　＋　キャラゲーム位置　＋　スクリプト画像表示位置
		if ( dirRight )
		{
			x = bx + ptChara.x + posScript.x;
//			DBGOUT_WND_F ( 1, _T("bx = %f, ptx = %f, scpx = %f"), bx, ptChara.x, posScript.x );
		}
		else
		{
			//反対向きのとき、表示位置がテクスチャサイズの補数
			x = bx + ptChara.x + ( - posScript.x - tx_size.w );
		}
		VEC2 vecImg = VEC2 ( x, y );


		//回転
		float rad = D3DX_PI * 0.01f * pScript->m_prmStaging.Rotate;
		m_mainGraphic->SetRadian ( rad * fDir );

		//スクリプトからの指定がなければテクスチャの中心
		VEC2 center { 0, 0 };
		if ( center != pScript->m_prmStaging.Rotate_center )
		{
			center = pScript->m_prmStaging.Rotate_center;
		}
		else
		{
			center = pTexture->GetCenter ();
		}

		m_mainGraphic->SetRotationCenter ( center );

		//左右反転
		m_mainGraphic->SetScalingCenter ( center );
//		m_mainGraphic->SetScalingCenter ( VEC2 ( 256, 0 ) );
		m_mainGraphic->SetScaling ( 1.f * fDir, 1.f );


		//表示に反映
		m_mainGraphic->SetPos ( vecImg );


	}

	void DispMainImage::TurnShade ( bool b )
	{
		m_mainGraphic->SetColor ( b ? 0xff000000 : 0xffffffff );
	}


}	//namespace GAME

