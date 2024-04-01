//=================================================================================================
//
// エグゼキャラ　ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "ExeChara.h"
#include "../GameMain/SoundConst.h"


//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//================================================
	//	特定アクション中の処理
	//================================================
	//	特殊アクション指定処理
	void ExeChara::SpecialAction ()
	{
		//-----------------------------------------------------
		if ( IsNameAction ( _T ( "立ち" ) ) )
		{
			//連続ヒット数のリセット
			m_pOther.lock()->m_btlPrm.SetChainHitNum ( 0 );
			if ( m_frame == 0 )
			{
			}

			//Test　バランス修正
			int b = m_btlPrm.GetBalance ();
			{
				if ( b < 1000 ) 
				{
					m_btlPrm.SetBalance ( 5000 );
				}
			}
		}

		//-----------------------------------------------------
		//ダッシュ開始
		if ( IsNameAction ( _T ( "FrontDashStart" ) ) )
		{
			if ( m_frame == 0 )
			{
				//SE
				SOUND->Play_SE ( SE_Btl_Dash );
			}
		}

		//-----------------------------------------------------
		//ジャンプ
		if ( IsNameAction ( _T ( "FrontJump" ) )
			|| IsNameAction ( _T ( "VerticalJump" ) )
			|| IsNameAction ( _T ( "BackJump" ) )
			)
		{
			if ( m_frame == 0 )
			{
				//SE
				SOUND->Play_SE ( SE_Btl_Jump );
			}
		}

		//-----------------------------------------------------
		//足払い追撃終了
		if ( IsNameAction ( _T ( "足払い追撃ヒット" ) ) )
		{
			if ( m_frame == 0 )
			{
				//				G_FTG->SetResetPos ( T );
			}
		}

		//-----------------------------------------------------
		//空中ダッシュ
		if ( IsNameAction ( _T ( "AirDash" ) ) )
		{
			if ( m_frame == 0 )
			{
				m_btlPrm.SetNAirDash ( 1 );
			}
			//回数リセットは "着地" 時
		}
#if 0
		//@info DBGOUT_WND_F は　ExeChara中で用いると２P側で上書きされる
		if ( m_playerID == PLAYER_ID_1 )
		{
			DBGOUT_WND_F ( 8, _T ( "AirDash = %d" ), m_btlPrm.GetNAirDash () );
		}
#endif // 0


#if 0
		//-----------------------------------------------------
		//バランスアウト
		if ( m_btlPrm.GetBalance () <= 0 )
		{
			//			SetAction ( _T ( "Dotty" ) );
			SetAction ( _T ( "立ち" ) );
		}
#endif // 0

		//=====================================================
		// キャラ別分岐
		
		//-----------------------------------------------------
		//必殺技A 壁割りテスト
		if ( IsNameAction ( _T ( "必殺技A" ) ) )
		{
			if ( m_frame == 0 )
			{
			}
		}

		//-----------------------------------------------------
		//超必殺背景演出
		if ( IsNameAction ( _T ( "超必Bヒット後" ) ) )
		{
			if ( m_frame == 0 )
			{
				//ライフ判定
				if ( m_pOther.lock()->GetLife () < 3000 )
				{
					//倒しきり時分岐
					SetAction ( _T ( "超必B_特殊勝利" ) );
				}

				//背景白転　キャラ影演出
				m_dispChara->TurnShadow ( T );
				m_btlPrm.SetWhiteOut ( T );
				m_pOther.lock()->m_dispChara->TurnShadow ( T );
			}
		}

		if (	IsNameAction ( _T ( "超必Bヒット終了" ) ) 
			||	IsNameAction ( _T ( "超必B_特殊勝利終了" ) ) 
		)
		{
			if ( m_frame == 0 )
			{
				//演出解除
				m_dispChara->TurnShadow ( F );
				m_pOther.lock()->m_dispChara->TurnShadow ( F );
			}
		}

	}



}	//namespace GAME

