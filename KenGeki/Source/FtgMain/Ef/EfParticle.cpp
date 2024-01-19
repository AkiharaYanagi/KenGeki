//=================================================================================================
//
//	EfParticle ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "EfParticle.h"

//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//初速は方向・速度ともにランダム
	//一定の減衰でその場に揺れながら微速落下

	EfParticle::EfParticle ()
	{
		AddTexture ( _T ( "Ef_Particle.png" ) );

		//グラフィックオブジェクトを初期化
		ResetObjectNum ( SPARK_NUM );

		//-----------------------------------------------------
		//角度初期化用ランダマイザ
		std::vector < UINT > v_rnd_ui;
		v_rnd_ui.reserve ( SPARK_NUM );
		for ( UINT i = 0; i < SPARK_NUM; ++ i )
		{
			v_rnd_ui.push_back ( i );
		}
		for ( UINT i = 0; i < SPARK_NUM; ++ i )
		{
			int j = rand () % (i + 1);
			UINT temp = v_rnd_ui [ i ];
			v_rnd_ui [ i ] = v_rnd_ui [ j ];
			v_rnd_ui [ j ] = temp;
		}
		//-----------------------------------------------------

		//パラメータを初期化
		UINT i = 0;
		PVP_Object pvpOb = GetpvpObject ();
		for ( P_Object pOb : * GetpvpObject () )
		{
			PrmEfParticle prm;
			
			//位置
			prm.m_pos.x = 0;
			prm.m_pos.y = 0;

			//角度
			float rad = D3DX_2PI / SPARK_NUM;
//			float rad_i = rad * i;
			float rad_i = rad * v_rnd_ui [ i ];
			prm.m_angle = rad_i;
			pOb->SetRadian ( D3DX_PI_HALF + rad_i );

			//cos((近似)π/2)を制御
			float c = cosf ( rad_i );
			if ( D3DX_PI_HALF - 0.01 < rad_i && rad_i < D3DX_PI_HALF + 0.01 ) { c = 0; }

			//sin((近似)π)を制御
			float s = sinf ( rad_i );
			if ( 3.14f < rad_i && rad_i < 3.15f ) { s = 0; }

			int rndx = rand () % 20;
			int rndy = rand () % 20;

			prm.m_startVel = VEC2 ( (rndx + 5.f) * c, (rndy + 5.f) * s );
			prm.m_vel.x = 0;
			prm.m_vel.y = 0;
			prm.m_G = VEC2 ( 0, 0.01f );

			pOb->SetValid ( false );
//			pOb->SetValid ( T );

			m_vPrm.push_back ( prm );

			++ i;
		}

		SetValid ( true );
	}

	EfParticle::~EfParticle ()
	{
	}

	void EfParticle::Move ()
	{
		PVP_Object pvpObject = GetpvpObject ();
		UINT i = 0;
		for ( P_Object pOb : *pvpObject )
		{
			if ( ! pOb->GetValid () ) { continue; }

			//位置計算
//			VEC2 prePos = m_vPrm[i].m_pos;

			m_vPrm[i].m_vel += -0.03f * m_vPrm[i].m_vel;	//減衰
			m_vPrm[i].m_vel += m_vPrm[i].m_G;
			m_vPrm[i].m_pos += m_vPrm[i].m_vel;

			if ( m_vPrm [ i ].m_pos.y >= GROUND_Y )
			{
				pOb->SetColor ( 0xff00ff00 );
				pOb->SetValid ( F );
				m_vPrm [ i ].m_flag = F;
			}

			//基準位置 + 補正位置 + 外部補正位置 + 個別位置
//			VEC2 prePosMatrix = GetCalcPos ( i );

			SetPosMatrix ( i, m_vPrm[i].m_pos );

			//インデックス
			++ i;
		}

		for ( UINT i = 0; i < SPARK_NUM; ++ i )
		{
//			( *pvpObject ) [ i ]->SetValid ( m_vPrm [ i ].m_flag );
		}
		
		//相互キャラクタ位置による画面補正位置
		SetDispBase ( G_BASE_POS () );

		GrpEf::Move ();
	}

	void EfParticle::On ( VEC2 center )
	{
		//TRACE_F ( _T("EfParticle::On\n") );

		//初期位置と初速をリセット
		UINT i = 0;
		UINT count = 0;
		const UINT COUNT_MAX = 30;

		PVP_Object pvpObject = GetpvpObject ();
		for ( P_Object pOb : *pvpObject )
		{
			//非稼働状態のものを再稼働
//			if ( ! pOb->GetValid () )
			if ( ! m_vPrm [ i ].m_flag )
			{
				pOb->SetValid ( T );
				m_vPrm [ i ].m_flag = T;
				m_vPrm [ i ].m_pos = center;
				m_vPrm [ i ].m_startPos = center;
				m_vPrm[i].m_vel = m_vPrm[i].m_startVel;

				//稼働インデックス
				if ( ++ count >= COUNT_MAX ) { break; }
			}

			//全体インデックス
			++i;
		}

		GrpEf::On ();
	}

	void EfParticle::Draw ()
	{
		PVP_Object pvpObject = GetpvpObject ();
		GrpEf::Draw ();
	}


}
