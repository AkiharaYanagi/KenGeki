//=================================================================================================
//
// DispInput ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "DispInput.h"

//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//キー入力表示個数
	//@todo CharaInputの入力保存数と同一なので統合する
	const UINT DispInput::NUM_DISP_INPUT = 20;
	const UINT DispInput::BASE_Y = 150;
	const UINT DispInput::CHIP_W = 10;
	const UINT DispInput::CHIP_H = 10;


	//コンストラクタ
	DispInput::DispInput ()
		: m_timer ( 0 ), m_vel ( 1.f ), m_base_x ( 10 )
	{
		//---------------------------------------------------------------
		//背景
		m_bg = make_shared < PrmRect > ();
		m_bg->SetRect ( m_base_x, BASE_Y, CHIP_W * INPUT_NUM, CHIP_H * NUM_DISP_INPUT );
		m_bg->SetZ ( Z_SYS );
		m_bg->SetAllColor ( 0x8080a080 );
		AddpTask ( m_bg );
		GRPLST_INSERT_MAIN ( m_bg );

		//---------------------------------------------------------------
		//見出
		m_index = make_shared < GrpAcv > ();
		m_index->AddTexture ( _T ( "KeyIndex.png" ) );
		m_index->SetPos ( VEC2 ( m_base_x, BASE_Y - CHIP_H ) );
		m_index->SetZ ( Z_SYS );
		AddpTask ( m_index );
		GRPLST_INSERT_MAIN ( m_index );

		//---------------------------------------------------------------
		//キー入力
		m_grp = make_shared < GrpAcv > ();
		m_grp->AddTexture ( _T ( "10_10_white.png" ) );
		m_grp->SetZ ( Z_SYS - 0.01f );
		m_grp->ClearObject ();
		AddpTask ( m_grp );
		GRPLST_INSERT_MAIN ( m_grp );

		//-----------------------------------------------------
		//矩形管理 ( 8種類 * 60[FPS] )
		//縦 60FPS
		for ( UINT frame = 0; frame < NUM_DISP_INPUT; ++ frame )
		{
			//横 8種類
			for ( UINT i = 0; i < INPUT_NUM; ++ i )
			{
				P_Object pOb = make_shared < GameObject > ();
				pOb->SetPos ( m_base_x + 10 * i, 10.f + 10 * frame );
				m_vpOb.push_back ( pOb );
				m_grp->AddpObject ( pOb );

				//初期値ランダム
				if ( 0 == rand () % 2 )
				{
					pOb->SetValid ( T );
				}
			}
		}
		//-----------------------------------------------------
	}

	DispInput::~DispInput ()
	{
	}

	//プレイヤIDによる表示初期化
	void DispInput::LoadPlayer ( PLAYER_ID playerID )
	{
		//位置
		if ( PLAYER_ID_1 == playerID )
		{
			m_base_x = 10;
		}
		else
		{
			m_base_x = 1280 - 10 - CHIP_W * INPUT_NUM;
		}
		m_bg->SetRect ( m_base_x, BASE_Y, CHIP_W * INPUT_NUM, CHIP_H * NUM_DISP_INPUT );
		m_index->SetPos ( VEC2 ( m_base_x, BASE_Y - CHIP_H ) );

		m_timer = 0;
	}

	//更新
	void DispInput::UpdateInput ( P_CharaInput pCharaInput )
	{
		//---------------------------------------------------------------
		//キー過去入力の表示
		++ m_timer;

		//1マス分移動したら上にシフト
		if ( 0 < m_timer )
		{
			//上書のため逆順ループ
			for ( UINT i = m_vpOb.size () - 1; i >= INPUT_NUM; -- i )
			{
				m_vpOb [ i ]->SetValid ( m_vpOb [ i - INPUT_NUM ]->GetValid () );
			}

			m_timer = 0;
		}

		//--------------------------
		//現入力の反映
		V_GAME_KEY vKey = pCharaInput->GetvGameKey ();

		//表示位置
		int i = 0;
		for ( P_Object pOb: m_vpOb )
		{
			int frame = i / INPUT_NUM;
			int input = i % INPUT_NUM;
			pOb->SetValid ( GetBoolInput ( pCharaInput, frame, input ) );
			pOb->SetPos ( m_base_x + 10 * input, BASE_Y + 10 * frame + m_vel * m_timer );
			++ i;
		}

		//---------------------------------------------------------------
	}

	//-------------------------------------
	//内部利用
	//n:キー種類, i:[F]
	bool DispInput::GetBoolInput ( P_CharaInput pCharaInput, int n, int i )
	{
		//現入力
		const V_GAME_KEY& vKey = pCharaInput->GetvGameKey ();

		bool ret = false;
		switch ( i )
		{
		case 0: ret = vKey[n].GetLvr ( _GameKey::LVR_4 ); break;
		case 1: ret = vKey[n].GetLvr ( _GameKey::LVR_2 ); break;
		case 2: ret = vKey[n].GetLvr ( _GameKey::LVR_6 ); break;
		case 3: ret = vKey[n].GetLvr ( _GameKey::LVR_8 ); break;
		case 4: ret = vKey [ n ].GetBtn ( _GameKey::BTN_0 ); break;
		case 5: ret = vKey [ n ].GetBtn ( _GameKey::BTN_1 ); break;
		case 6: ret = vKey [ n ].GetBtn ( _GameKey::BTN_2 ); break;
		case 7: ret = vKey [ n ].GetBtn ( _GameKey::BTN_3 ); break;
		case 8: ret = vKey [ n ].GetBtn ( _GameKey::BTN_4 ); break;
		case 9: ret = vKey [ n ].GetBtn ( _GameKey::BTN_5 ); break;
		case 10: ret = vKey [ n ].GetBtn ( _GameKey::BTN_6 ); break;
		case 11: ret = vKey [ n ].GetBtn ( _GameKey::BTN_7 ); break;
		default: break;
		}
		return ret;
	}


}	//namespace GAME

