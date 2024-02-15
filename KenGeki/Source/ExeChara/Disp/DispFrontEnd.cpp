//=================================================================================================
//
// DispFrontEnd ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "DispFrontEnd.h"

//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	VEC2 DispFrontEnd::POS_PL_CP_1P ( 5, LIFE_GAUGE_Y );
	VEC2 DispFrontEnd::POS_PL_CP_2P ( 1280 - 64 - 5, LIFE_GAUGE_Y );


	DispFrontEnd::DispFrontEnd ()
	{
		//ゲージ類

		//ライフゲージ
		m_gaugeLife = make_shared < DispGauge > ();
		m_gaugeLife->SetPosition ( LIFE_GAUGE_X, LIFE_GAUGE_Y, LIFE_GAUGE_W, LIFE_GAUGE_H );
		const _CLR c0L = LIFE_GAUGE_VALUE_CLR0;
		const _CLR c1L = LIFE_GAUGE_VALUE_CLR1;
		const _CLR c2L = LIFE_GAUGE_VALUE_CLR2;
		const _CLR c3L = LIFE_GAUGE_VALUE_CLR3;
		m_gaugeLife->SetColor_Value ( c0L, c1L, c2L, c3L );
		m_gaugeLife->SetColor_Frame ( LIFE_GAUGE_FRAME_CLR );
		m_gaugeLife->SetColor_Decrease ( LIFE_GAUGE_DECREASE_CLR );
		AddpTask ( m_gaugeLife );

		//バランスゲージ
		m_gaugeBalance = make_shared < DispGauge > ();
		m_gaugeBalance->SetPosition ( BALANCE_GAUGE_X, BALANCE_GAUGE_Y, BALANCE_GAUGE_W, BALANCE_GAUGE_H );
		const _CLR c0B = BALANCE_GAUGE_VALUE_CLR0;
		const _CLR c1B = BALANCE_GAUGE_VALUE_CLR1;
		const _CLR c2B = BALANCE_GAUGE_VALUE_CLR2;
		const _CLR c3B = BALANCE_GAUGE_VALUE_CLR3;
		m_gaugeBalance->SetColor_Value ( c0B, c1B, c2B, c3B );
		m_gaugeBalance->SetColor_Frame ( BALANCE_GAUGE_FRAME_CLR );
		m_gaugeBalance->SetColor_Decrease ( BALANCE_GAUGE_DECREASE_CLR );
		m_gaugeBalance->OffDecrease ();
		AddpTask ( m_gaugeBalance );

		//マナゲージ
		m_gaugeMana = make_shared < DispGauge > ();
		m_gaugeMana->SetPosition ( MANA_GAUGE_X, MANA_GAUGE_Y, MANA_GAUGE_W, MANA_GAUGE_H );
		const _CLR c0M = MANA_GAUGE_VALUE_CLR0;
		const _CLR c1M = MANA_GAUGE_VALUE_CLR1;
		const _CLR c2M = MANA_GAUGE_VALUE_CLR2;
		const _CLR c3M = MANA_GAUGE_VALUE_CLR3;
		m_gaugeMana->SetColor_Value ( c0M, c1M, c2M, c3M );
		m_gaugeMana->SetColor_Frame ( BALANCE_GAUGE_FRAME_CLR );
		m_gaugeMana->SetColor_Decrease ( BALANCE_GAUGE_DECREASE_CLR );
		m_gaugeMana->OffDecrease ();
		AddpTask ( m_gaugeMana );


		//-----------------------------------------------------
		//プレイヤー表示
		//-----------------------------------------------------
		m_grp_Cst_Player1P2P = MakepGrpPlyr ( _T ( "Player_1P.png" ) );
		m_grp_Cst_Player1P2P->AddTexture ( _T ( "Player_2P.png" ) );

		m_grp_Cst_InputPlayerCOM = MakepGrpPlyr ( _T ( "INPUT_PLAYER.png" ) );
		m_grp_Cst_InputPlayerCOM->AddTexture ( _T ( "INPUT_CPU.png" ) );

		m_grp_CH_Player1P2P = MakepGrpPlyr ( _T ( "Player_1P.png" ) );
		m_grp_CH_Player1P2P->AddTexture ( _T ( "Player_2P.png" ) );

		m_grp_CH_InputCOMPLayer = MakepGrpPlyr ( _T ( "INPUT_PLAYER.png" ) );
		m_grp_CH_InputCOMPLayer->AddTexture ( _T ( "INPUT_CPU.png" ) );


#if 0
		//ヒットストップ時間表示
		m_gaugeHitStop.SetAllColor ( _CLR ( 0xffa0a0ff ) );
		m_pGrpAry->InsertTask ( & m_gaugeHitStop );

		//のけぞり時間表示
		m_gaugeLurch.SetAllColor ( _CLR ( 0xffa0ffa0 ) );
		m_pGrpAry->InsertTask ( & m_gaugeLurch );
#endif	//0

		//ヒット数
		m_grpHitNum = make_shared < GrpAcv > ();
		m_grpHitNum->AddTexture ( _T("0.png") );
		m_grpHitNum->AddTexture ( _T("1.png") );
		m_grpHitNum->AddTexture ( _T("2.png") );
		m_grpHitNum->AddTexture ( _T("3.png") );
		m_grpHitNum->AddTexture ( _T("4.png") );
		m_grpHitNum->AddTexture ( _T("5.png") );
		m_grpHitNum->AddTexture ( _T("6.png") );
		m_grpHitNum->AddTexture ( _T("7.png") );
		m_grpHitNum->AddTexture ( _T("8.png") );
		m_grpHitNum->AddTexture ( _T("9.png") );
		m_grpHitNum->SetZ ( Z_EFB + 0.01f );
		GRPLST_INSERT_MAIN ( m_grpHitNum );
		AddpTask ( m_grpHitNum );

		//ヒット数２桁目
		m_grpHitNum->AddObject ();

		m_grpStrHit = make_shared < GrpAcv > ();
		m_grpStrHit->AddTexture ( _T("Hit.png") );
		m_grpStrHit->SetZ ( Z_EFB + 0.01f );
		GRPLST_INSERT_MAIN ( m_grpStrHit );
		AddpTask ( m_grpStrHit );


		//アクション名
		m_strAction = make_shared < GrpStr > ();
		m_strAction->SetStr ( _T("Action") );
		m_strAction->SetPos ( VEC2 () );
		m_strAction->SetZ ( Z_MENU );
		m_strAction->SetFontColor ( 0xff0000ff, 0xffffffff );
		GRPLST_INSERT_MAIN ( m_strAction );
		AddpTask ( m_strAction );
	}

	P_GrpAcv DispFrontEnd::MakepGrpPlyr ( LPCTSTR pstr )
	{
		P_GrpAcv p = make_shared < GrpAcv > ();
		p->SetValid ( T );
		p->AddTexture ( pstr );
		p->SetZ ( Z_SYS - 0.001f );
		AddpTask ( p );
		GRPLST_INSERT_MAIN ( p );
		return p;
	}

	DispFrontEnd::~DispFrontEnd ()
	{
	}

	void DispFrontEnd::LoadPlayer ( PLAYER_ID playerID )
	{
		m_playerID = playerID;

		m_gaugeLife->LoadPlayer ( playerID );
		m_gaugeBalance->LoadPlayer ( playerID );
		m_gaugeMana->LoadPlayer ( playerID );


			//プレイヤにより表示を指定
		if ( PLAYER_ID_1 == playerID )
		{
			m_grp_Cst_Player1P2P->SetPos ( POS_PL_CP_1P );
			m_grp_Cst_Player1P2P->SetIndexTexture ( SIDE_1P );

			m_grp_Cst_InputPlayerCOM->SetPos ( POS_PL_CP_1P + VEC2 ( 0, 33 ) );
			m_grp_CH_Player1P2P->SetIndexTexture ( SIDE_1P );
		}
		else if ( PLAYER_ID_2 == playerID )
		{
			m_grp_Cst_Player1P2P->SetPos ( POS_PL_CP_2P );
			m_grp_Cst_Player1P2P->SetIndexTexture ( SIDE_2P );

			m_grp_Cst_InputPlayerCOM->SetPos ( POS_PL_CP_2P + VEC2 ( 0, 33 ) );
			m_grp_CH_Player1P2P->SetIndexTexture ( SIDE_2P );
		}

#if 0
		//ヒットストップ時間表示
		m_gaugeHitStop.SetRect ( 0, 0, 0, 0 );

		//のけぞり時間表示
		m_gaugeLurch.SetRect ( 0, 0, 0, 0 );
#endif // 0

		//ヒット数
		P_Object pOb = m_grpHitNum->GetpObject ( 1 );
		if ( PLAYER_ID_1 == playerID )
		{
			m_grpHitNum->SetPos ( VEC2 ( 100, 200 ) );
			m_grpStrHit->SetPos ( VEC2 ( 100 + 128, 200 ) );
			
			pOb->SetPos ( VEC2 ( 0, 200 ) );
		}
		else if ( PLAYER_ID_2 == playerID )
		{
			m_grpHitNum->SetPos ( VEC2 ( 1280 - 384 -100, 200 ) );
			m_grpStrHit->SetPos ( VEC2 ( 1280 - 256 -100, 200 ) );

			pOb->SetPos ( VEC2 (  1280 - 384 - 0, 200 ) );
		}


		//アクション名
		if ( PLAYER_ID_1 == playerID )
		{
			m_strAction->SetPos ( VEC2 ( 400, 150 ) );
		}
		else if ( PLAYER_ID_2 == playerID )
		{
			m_strAction->SetPos ( VEC2 ( 640 + 150, 150 ) );
		}

	}

	//------------------------
	//シーンパラメータ関連初期化
	void DispFrontEnd::ParamInit ( P_Param pParam )
	{
		//ゲーム設定
		GameSettingFile stg = pParam->GetGameSetting ();

		//選択キャラ名前・モードを取得
		PLAYER_MODE playerMode = stg.GetPlayerMode ( m_playerID );

		//プレイヤモード(入力種類)による初期化
		switch ( playerMode )
		{
		case MODE_PLAYER: SetPlayer (); break;
		case MODE_CPU: SetCPU (); break;
		case MODE_NETWORK: break;
		default: break;
		}
	}

	void DispFrontEnd::SetPlayer ()
	{
		m_grp_Cst_InputPlayerCOM->SetIndexTexture ( INPUT_PLAYER );
		m_grp_CH_InputCOMPLayer->SetIndexTexture ( INPUT_PLAYER );
	}

	void DispFrontEnd::SetCPU ()
	{
		m_grp_Cst_InputPlayerCOM->SetIndexTexture ( INPUT_CPU );
		m_grp_CH_InputCOMPLayer->SetIndexTexture ( INPUT_CPU );
	}


	//------------------------
	void DispFrontEnd::UpdateGauge ( BtlParam btlPrm )
	{
		m_gaugeLife->Update ( btlPrm.GetLife () );
		m_gaugeBalance->Update ( btlPrm.GetBalance () );
		m_gaugeMana->Update ( btlPrm.GetMana () );

	}

	void DispFrontEnd::UpdateMainImage ( VEC2 posChara )
	{
		//プレイヤー表示
		float bx = G_Ftg::inst ()->GetPosMutualBase ().x;	//基準位置
		VEC2 vecImgPlayer = VEC2 ( bx, 0 ) + posChara + VEC2 ( -32.f, 0 );
		vecImgPlayer.y = 32.f + 1.f * PLAYER_BASE_Y;	//y方向のみ指定

		m_grp_CH_Player1P2P->SetPos ( vecImgPlayer );
		m_grp_CH_InputCOMPLayer->SetPos ( vecImgPlayer + VEC2 ( 0, 33 ) );

		//硬直時間表示
#if 0
		static bool b2 = true;
		if ( ::GetAsyncKeyState ( '2' ) & 0x0001 ) { b2 ^= 1; }
		if ( b2 )
		{
			//ヒットストップ時間
			m_dispChara.UpdateHitStop ( m_ptChara, m_dirRight, m_hitstop, m_hitstopTimer );

			//のけぞり時間
			m_dispChara.UpdateLurch ( m_ptChara, m_dirRight, m_lurch, m_lurchTimer );
		}
#endif // 0

	}

	void DispFrontEnd::UpdateHitNum ( UINT n )
	{
		if ( n < 0 || 100 <= n ) { return; }


		int n1 = n % 10;	//1桁目
		int n2 = (n / 10) % 10;	//2桁目
		P_Object pOb = m_grpHitNum->GetpObject ( 1 );

		m_grpHitNum->SetIndexTexture ( n1 );

		if ( n == 0 )
		{
			m_grpHitNum->SetValid ( F );
			m_grpStrHit->SetValid ( F );
		}
		else
		{
			m_grpHitNum->SetValid ( T );
			m_grpStrHit->SetValid ( T );
			if ( n < 10 )
			{
				pOb->SetValid ( F );
			}
			else
			{
				pOb->SetIndexTexture ( n2 );
				pOb->SetValid ( T );
			}
		}
	}


	void DispFrontEnd::UpdateActionName ( LPCTSTR actionName )
	{
		m_strAction->SetStr ( actionName );
	}


#if 0
	void DispChara::UpdateHitStop ( VEC2 ptChara, bool dirRight, UINT hitstop, UINT hitstopTimer )
	{
		//のけぞりフレーム時間表示
		float fLurch = 10 * ( (float)hitstop - hitstopTimer );
		float x = ptChara.x - ( dirRight ? 10 + 64 + 10 : 0 - 64 - 10 );
		float y = ptChara.y - fLurch;
		float w = 10.f;
		float h = fLurch;
		m_gaugeHitStop.SetRect ( ( *m_pDispGameBase ).x + ptChara.x - ( dirRight ? 10 + 64 + 10 : 0 - 64 - 10 ), ptChara.y - fLurch, 10.f, fLurch );
	}

	void DispChara::UpdateLurch ( VEC2 ptChara, bool dirRight, UINT lurch, UINT lurchTimer )
	{
		//のけぞりフレーム時間表示
		float fLurch = 10 * ( (float)lurch - lurchTimer );
		float x = ptChara.x - ( dirRight ? 10 + 64 : 0 - 64 );
		float y = ptChara.y - fLurch;
		float w = 10.f;
		float h = fLurch;
		m_gaugeLurch.SetRect ( ( *m_pDispGameBase ).x + ptChara.x - ( dirRight ? 10 + 64 : 0 - 64 ), ptChara.y - fLurch, 10.f, fLurch );
	}
#endif	//0

#if 0

	//ヒット
	void DispChara::OnHit ( VEC2 ptChara, bool dirRight )
	{
		//エフェクトの設定
		m_grpHitEf.SetWait ( 15 );
		m_grpHitEf.SetFadeOut ( 15 );

		//向きによる位置の計算
//		VEC2 img_vec = VEC2 ( dispGameBaseX, 0 ) + ptChara + VEC2 ( 0, -128 );
//		VEC2 img_vec = VEC2 ( dispGameBaseX, 0 ) + ptChara;
//		m_grpHitEf.GetpMatrix()->SetPos ( img_vec );
		m_grpHitEf.SetPos ( ptChara + VEC2 ( 0, -128 ) );

		//向きは毎回更新する
		float dir = dirRight ? 1.f : -1.f;
		m_grpHitEf.SetStartScaling ( VEC2 ( dir * 1.f, 1.f ) );
		m_grpHitEf.SetTargetScaling ( VEC2 ( dir * 1.3f, 1.3f ) );
		m_grpHitEf.SetAcc ( VEC2 ( dir * 0.005f, 0.005f ) );
		m_grpHitEf.On ();
	}

	//アヴォイド
	void DispChara::OnAvoid ( VEC2 ptChara, bool dirRight )
	{
		//エフェクトの設定
		m_grpAvoidEf.SetWait ( 15 );
		m_grpAvoidEf.SetFadeIn ( 15 );

		//向きによる位置の計算
//		VEC2 img_vec = VEC2 ( dispGameBaseX, 0 ) + ptChara + VEC2 ( -128, -256 );
//		VEC2 img_vec = VEC2 ( dispGameBaseX, 0 ) + ptChara + VEC2 ( 0, -64 );
//		m_grpAvoidEf.GetpMatrix()->SetPos ( img_vec );
//		m_grpAvoidEf.SetPos ( img_vec );
		m_grpAvoidEf.SetPos ( ptChara + VEC2 ( 0, -64 ) );

		//向きは毎回更新する
		float dir = dirRight ? 1.f : -1.f;
		m_grpAvoidEf.SetStartScaling ( VEC2 ( dir * 1.f, 1.f ) );
		m_grpAvoidEf.SetTargetScaling ( VEC2 ( dir * 1.3f, 1.3f ) );
		m_grpAvoidEf.SetAcc ( VEC2 ( dir * 0.005f, 0.005f ) );
		m_grpAvoidEf.On ();
	}

	//ポイズド
	void DispChara::OnPoised ( VEC2 ptChara, bool dirRight )
	{
		//エフェクトの設定
		m_grpPoisedEf.SetWait ( 15 );
		m_grpPoisedEf.SetFadeIn ( 15 );

		//位置の計算
		m_grpPoisedEf.SetPos ( ptChara + VEC2 ( 0, -96 ) );

		//向きは毎回更新する
		float dir = dirRight ? 1.f : -1.f;
		m_grpPoisedEf.SetStartScaling ( VEC2 ( dir * 1.f, 1.f ) );
		m_grpPoisedEf.SetTargetScaling ( VEC2 ( dir * 1.5f, 1.5f ) );
		m_grpPoisedEf.SetAcc ( VEC2 ( dir * 0.005f, 0.005f ) );
		m_grpPoisedEf.On ();
	}


#endif // 0


}	//namespace GAME

