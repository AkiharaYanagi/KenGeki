//=================================================================================================
//
//	EfSpark ソースファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "EfSpark.h"

//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	EfSpark::EfSpark ()
	{
		AddTexture ( _T ( "Ef_Spark.png" ) );

		//グラフィックオブジェクトを初期化
		ResetObjectNum ( SPARK_NUM );

		//パラメータを初期化
		PVP_Object pvpObject = GetpvpObject ();
		UINT i = 0;
		for ( P_Object pOb : * pvpObject )
		{
			PrmEfSpark prm;
			
			//位置
			prm.m_pos.x = 0;
			prm.m_pos.y = 0;

			//角度
			float rad = D3DX_2PI / SPARK_NUM;
			float rad_i = rad * i;
			prm.m_angle = rad_i;

			//cos((近似)π/2)を制御
			float c = cosf ( rad_i );
			if ( D3DX_PI_HALF - 0.01 < rad_i && rad_i < D3DX_PI_HALF + 0.01 ) { c = 0; }

			//sin((近似)π)を制御
			float s = sinf ( rad_i );
			if ( 3.14f < rad_i && rad_i < 3.15f ) { s = 0; }

			float rndx = 1.f * (rand () % 20);
			float rndy = 1.f * (rand () % 20);

			prm.m_startVel = VEC2 ( (rndx + 5.f) * c, (rndy + 5.f) * s );
			prm.m_vel.x = 0;
			prm.m_vel.y = 0;
			prm.m_G = VEC2 ( 0, 0.3f );

			pOb->SetRadian ( D3DX_PI_HALF + rad_i );
			pOb->SetValid ( false );

			m_vPrm.push_back ( prm );

			++ i;
		}
	}

	EfSpark::~EfSpark ()
	{
	}

	void EfSpark::Move ()
	{
		PVP_Object pvpObject = GetpvpObject ();
		UINT i = 0;
		for ( P_Object pOb : *pvpObject )
		{
			if ( ! pOb->GetValid () ) { continue; }

			//位置計算
			VEC2 prePos = m_vPrm[i].m_pos;
			m_vPrm[i].m_vel += -0.03f * m_vPrm[i].m_vel;	//減衰
			m_vPrm[i].m_vel += m_vPrm[i].m_G;
			m_vPrm[i].m_pos += m_vPrm[i].m_vel;

			//基準位置 + 補正位置 + 外部補正位置 + 個別位置
			VEC2 prePosMatrix = GetCalcPos ( i );
			SetPosMatrix ( i, m_vPrm[i].m_pos );

			//バウンド
			VEC2 pos = GetCalcPos ( i );
			if ( PLAYER_BASE_Y < pos.y )
			{
				m_vPrm[i].m_vel.y *= -0.8f;
				m_vPrm[i].m_vel.x *= 0.8f;
				SetPosMatrix ( i, prePosMatrix );
			}

			//回転
//			float rad = D3DX_2PI / SPARK_NUM;
//			float rad = atan2f ( m_sparkPos[i].y - prePos.y, m_sparkPos[i].x - prePos.x );
			float rad = atan2f ( m_vPrm[i].m_pos.y - prePos.y, m_vPrm[i].m_pos.x - prePos.x );
//			(*pvpMatrix)[i]->SetRadian ( D3DX_PI_HALF + rad * i );
			pOb->SetRadian ( D3DX_PI_HALF + rad );

			//インデックス
			++ i;
		}
		SetDispBase ( G_BASE_POS () );

		GrpEf::Move ();
	}

	void EfSpark::On ( VEC2 center )
	{
		//初期位置と初速をリセット
		PVP_Object pvpObject = GetpvpObject ();
		UINT i = 0;
		for ( P_Object pOb : *pvpObject )
		{
			pOb->SetValid ( T );
			pOb->SetFadeOut ( 60 );
			m_vPrm[i].m_pos = VEC2 ( 0, 0 );
			m_vPrm[i].m_vel = m_vPrm[i].m_startVel;
			//インデックス
			++i;
		}

		SetRevised ( center );
//		SetWait ( 60 );
//		SetFadeOut ( 60 );
		SetValid ( true );
		GrpEf::On ();
	}
}
