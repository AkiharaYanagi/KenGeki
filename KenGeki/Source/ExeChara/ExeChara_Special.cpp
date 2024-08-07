//=================================================================================================
//
// エグゼキャラ　ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "ExeChara.h"
#include "../FtgMain/Fighting.h"
#include "../GameMain/SoundConst.h"


//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	const UINT32 ExeChara::CLR_LOOP [ 8 ] =
	{
		0xff0000ff,
		0xff2020ff,
		0xff4040ff,
		0xff6060ff,
		0xff8080ff,
		0xff6060ff,
		0xff4040ff,
		0xff2020ff,
	};


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

			//Test　バランス修正
			int b = m_btlPrm.GetBalance ();
			{
				if ( b < 1000 ) 
				{
					m_btlPrm.SetBalance ( BALANCE_START );
				}
			}
		}

		//-----------------------------------------------------
		//アクセル増減　 ：　アクション中のバランス値を利用(今作品ではバランス発生に用いない)
#if 0
		//-----------------------------------------------------
		if ( IsNameAction ( _T ( "前歩き" ) ) )
		{
			m_btlPrm.AddAccel ( 3 );
		}

		//-----------------------------------------------------
		if ( IsNameAction ( _T ( "後歩き" ) ) )
		{
			m_btlPrm.AddAccel ( -10 );
		}

		//-----------------------------------------------------
		//ダッシュ
		if ( IsNameAction ( _T ( "前ダッシュ" ) ) )
		{
			//アクセル
#if 0
			if ( m_playerID == PLAYER_ID_1 )
			{
				G_FTG->AddAccel ( + 3 );
			}
			else if ( m_playerID == PLAYER_ID_2 )
			{
				G_FTG->AddAccel ( - 3 );
			}
#endif // 0
			m_btlPrm.AddAccel ( 10 );
		}

		//-----------------------------------------------------
		//ジャンプ
		if ( IsNameAction ( _T ( "前方ジャンプ" ) ) )
		{
			m_btlPrm.AddAccel ( 10 );
		}

		if ( IsNameAction ( _T ( "垂直ジャンプ" ) ) )
		{
			m_btlPrm.AddAccel ( 0 );
		}
		
		if ( IsNameAction ( _T ( "後方ジャンプ" ) ) )
		{
			m_btlPrm.AddAccel ( -10 );
		}
#endif // 0
		int accel_value = m_pAction->GetBalance ();
		if ( 0 == accel_value )
		{
			//カテゴリ一括
			if ( IsAttacking () ) {	m_btlPrm.AddAccel ( 3 ); }	//通常攻撃
			else if ( IsSpecial () ) {	m_btlPrm.AddAccel ( 5 ); }	//必殺技
			else if ( IsOverdrive () ) {	m_btlPrm.AddAccel ( 10 ); }	//超必殺技
			else 
			{
				//０：基本状態は沈静化
				m_btlPrm.DirZeroAccel ( 3 );	//アクセル 沈静化
			}
		}
		else
		{
			m_btlPrm.AddAccel ( accel_value );
		}



		//-----------------------------------------------------
		//足払い追撃終了
//		if ( IsNameAction ( _T ( "足払い追撃ヒット" ) ) )
		if ( IsNameAction ( _T ( "ホーミング" ) ) )
//		if ( IsNameAction ( _T ( "テスト" ) ) )
		{
			if ( m_frame == 0 )
			{
				G_FTG->SetWallMove ( T );
			}

//			if ( m_pAction->IsOverScript ( m_frame ) )
			if ( m_pAction->IsEndScript ( m_pScript->GetFrame() ) )
			{
//				G_FTG->SetWallMove ( F );
			}
		}
		if ( IsNameAction ( _T ( "位置リセット着地" ) ) )
		{
			if ( m_frame == 0 )
			{
				G_FTG->SetWallMove ( F );
			}
		}


		//-----------------------------------------------------
		//空中ダッシュ
		if ( IsNameAction ( _T ( "空中ダッシュ" ) ) )
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

		if ( IsNameAction ( _T ( "空中ダッシュ小攻撃" ) ) 
			||  IsNameAction ( _T ( "空中ダッシュ中攻撃" ) ) 
			||  IsNameAction ( _T ( "空中ダッシュ大攻撃" ) ) 
			)
		{
			//攻撃成立時
			if ( m_btlPrm.GetHitEst () )
			{
				m_btlPrm.SetHitDrop ( 2.5 );
			}
		}

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
		//壁まで吹き飛び
		if ( IsNameAction ( _T ( "小攻撃" ) ) )
		{
			if ( m_frame == 0 )
			{
				int i = 0;
			}
		}

		//-----------------------------------------------------
		//壁まで吹き飛び
		if ( IsNameAction ( _T ( "壁まで吹き飛び" ) ) )
		{
			if ( m_frame == 0 )
			{
			}

			//カラー遷移
			static UINT32 index = 0;
			if ( index >= CLR_LOOP_NUM )
			{
				index = 0;
			}
			m_btlPrm.SetColor ( CLR_LOOP [ index ++ ] );
		}

		//-----------------------------------------------------
		//壁割り発生
		if ( IsNameAction ( _T ( "壁張り付き" ) ) )
		{
			if ( m_frame == 0 )
			{
#if 0
				if ( m_btlPrm.GetDirRight () )
				{
					mwp_fighting.lock()->WallBreak_L ();
				}
				else
				{
					mwp_fighting.lock()->WallBreak_R ();
				}

				//相手を強制的に"壁割り後の走り"に移行する
				
				G_FTG->SetNewWall ( T );
#endif // 0
				m_btlPrm.SetColor ( 0xffffffff );
			}
		}

		//-----------------------------------------------------
		//必殺技A
		if ( IsNameAction ( _T ( "必殺技A" ) ) )
		{
			//相手色変更
			m_pOther.lock ()->m_btlPrm.SetColor ( 0xff0000ff );
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

