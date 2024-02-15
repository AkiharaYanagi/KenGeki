//=================================================================================================
//
//	Fighting デモ
//
//=================================================================================================
#pragma once

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "Game.h"
#include "FtgConst.h"	
#include "MutualChara.h"

//-------------------------------------------------------------------------------------------------
// 宣言
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//=====================================================
	class FtgDemoActor; using WP_FtgDemoActor = weak_ptr < FtgDemoActor >;

	//=====================================================
	//param
	class FtgDemoParam
	{
		P_MutualChara			m_mutualChara;
		WP_FtgDemoActor			m_ftgDemoActor;

	public:
		
		void SetpMutualChara ( P_MutualChara p ) { m_mutualChara = p; }
		P_MutualChara GetpMutualChara () const { return m_mutualChara; }

		void SetwpFtgDemoActor ( WP_FtgDemoActor p ) { m_ftgDemoActor = p; }
		WP_FtgDemoActor GetwpFtgDemoActor () const { return m_ftgDemoActor; }
	};
	using P_PRM_FTG_DEMO = std::shared_ptr < FtgDemoParam >;

	//=====================================================
	//State
	class FtgDemoState : public TASK_VEC
	{
	protected:
		P_PRM_FTG_DEMO		m_prmFtgDemo;
	public:
		FtgDemoState () {}
		FtgDemoState ( const FtgDemoState & rhs ) = delete;
		~FtgDemoState () {}

		virtual void Do () {}
		void SetpPrm ( P_PRM_FTG_DEMO p ) { m_prmFtgDemo = p; }

		P_GrpDemo MakeGrpValue ( LPCTSTR txName );
		P_MutualChara GetpMutualChara () const { return m_prmFtgDemo->GetpMutualChara (); }
		WP_FtgDemoActor GetwpFtgDemoActor () const { return m_prmFtgDemo->GetwpFtgDemoActor (); }

	};
	using P_FtgDemo = std::shared_ptr < FtgDemoState >;

	//=====================================================

	//State_Derivered
	//------------------------------------------------

	class FTG_DM_Greeting : public FtgDemoState
	{
		//タイマ
		P_Timer		m_timer;

	public:
		FTG_DM_Greeting ();
		FTG_DM_Greeting ( const FTG_DM_Greeting & rhs ) = delete;
		~FTG_DM_Greeting () {}
		void Start ();
		void Do ();
	};
	using P_FTG_DM_Greeting = std::shared_ptr < FTG_DM_Greeting >;

	//------------------------------------------------

	class FTG_DM_GetReady : public FtgDemoState
	{
		P_GrpDemo		m_grpGetReady;
		P_GrpAcv		m_grpClock;
		P_Timer			m_timer;
		static const UINT		COUNT;
		static const UINT		COUNT_D;
	public:
		FTG_DM_GetReady ();
		FTG_DM_GetReady ( const FTG_DM_GetReady & rhs ) = delete;
		~FTG_DM_GetReady () {}
		
		void Start ();
		void Do ();
		void Final ();
	};
	using P_FTG_DM_GetReady = std::shared_ptr < FTG_DM_GetReady >;


	//------------------------------------------------
	class FTG_DM_Attack : public FtgDemoState
	{
		P_GrpDemo		m_grpAttack;
	public:
		FTG_DM_Attack ();
		FTG_DM_Attack ( const FTG_DM_Attack & rhs ) = delete;
		~FTG_DM_Attack () {}

		void Start ();
		void Do ();
	};
	using P_FTG_DM_Attack = std::shared_ptr < FTG_DM_Attack >;


	//------------------------------------------------
	class FTG_DM_Main : public FtgDemoState
	{
	public:
		void Start ();
		void Do ();
	};
	using P_FTG_DM_Main = std::shared_ptr < FTG_DM_Main >;


	//------------------------------------------------
	class FTG_DM_Down : public FtgDemoState
	{
		P_GrpDemo		m_grpDown;
	public:
		FTG_DM_Down ();
		FTG_DM_Down ( const FTG_DM_Down & rhs ) = delete;
		~FTG_DM_Down () {}

		void Start ();
		void Do ();
	};
	using P_FTG_DM_Down = std::shared_ptr < FTG_DM_Down >;


	//------------------------------------------------
	class FTG_DM_Winner : public FtgDemoState
	{
	public:
		void Do ();
	};
	using P_FTG_DM_Winner = std::shared_ptr < FTG_DM_Winner >;


	//=====================================================

	using VP_FtgDemo = std::vector < P_FtgDemo >;

	//Actor
	class FtgDemoActor : public TASK_VEC, public enable_shared_from_this < FtgDemoActor >
	{
		//ステート
		P_FTG_DM_Greeting	m_Greeting;
		P_FTG_DM_GetReady	m_GetReady;
		P_FTG_DM_Attack		m_Attack;
		P_FTG_DM_Main		m_Main;
		P_FTG_DM_Down		m_Down;
		P_FTG_DM_Winner		m_Winner;

		//ステート監理
		VP_FtgDemo			mvp_FtgDemo;

		//現在ステート
		P_FtgDemo			mp_FtgDemo;

		//共通パラメータ
		P_PRM_FTG_DEMO		mp_Param;

		//終了フラグ
		bool				m_bEnd;

	public:
		FtgDemoActor ();

		void StartGreeting ();
		void StartFighting ();
		void Load ();
		void Do ();

		void SetpMutualChara ( P_MutualChara p ) { mp_Param->SetpMutualChara ( p ); }
//		void SetpMutualChara_Demo ( P_MutualChara_Demo p ) { mp_Param->SetpMutualChara_Demo ( p ); }

		void Change_Greeting_To_GetReady ();
		void Change_GetReady_To_Attack ();
		void Change_Main_To_Down ();
		void Change_Down_To_Greeting ();
		
		void End_Down_To_Result ();
		bool IsEnd () const { return m_bEnd; }

	private:
		P_MutualChara GetpMutualChara () const { return mp_Param->GetpMutualChara (); }
	};

	using P_FtgDemoActor = std::shared_ptr < FtgDemoActor >;


}	//namespace GAME


