//=================================================================================================
//
//	Sequence ヘッダファイル
//
//=================================================================================================
#pragma once

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
//#include "Game.h"
#include "Script.h"

//-------------------------------------------------------------------------------------------------
// 宣言
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//-----------------------------------------------------------------
	//		スクリプトの一連、アクションとエフェクトに派生
	//-----------------------------------------------------------------
	class Sequence
	{
		tstring			m_name;			//名前
		PVP_Script		m_pvpScript;	//スクリプト配列
		UINT			m_next;			//次シークエンスID
		tstring			m_nextName;		//次シークエンス名

	public:
		Sequence ();
		Sequence ( const Sequence & rhs ) = delete;
		virtual ~Sequence ();

		void Rele ();

		//-----------------------------------------------------------------
		//名前
		void SetName ( tstring name ) { m_name.assign ( name ); }
		tstring GetName () const { return m_name; }
		bool IsName ( tstring name ) const { return m_name == name; }

		//-----------------------------------------------------------------
		//スクリプトの追加
		void AddpScript ( P_Script pScript ) { m_pvpScript->push_back ( pScript ); }

		//スクリプト配列にまとめて追加
		void AddaScript ( unique_ptr < P_Script[] > arypScript, rsize_t size );

		//スクリプト配列サイズの取得
		UINT SizeScript () const { return m_pvpScript->size(); }

		//スクリプト配列ポインタを取得
		PVP_Script GetpvpScript () { return m_pvpScript; }

		//スクリプトの取得
		P_Script GetpScript ( UINT index ) { return m_pvpScript->at ( index ); }

		//次スクリプトが存在するかどうか
		bool IsNextScript ( UINT index ) const { return ( index < m_pvpScript->size() - 1 ); }

		//最終スクリプトかどうか
		bool IsEndScript ( UINT index ) const { return ( index == m_pvpScript->size () - 1 ); }

		//オーバースクリプトかどうか
		bool IsOverScript ( UINT index ) const { return ( index > m_pvpScript->size () - 1 ); }

		//次シークエンスID
		UINT GetNextID () const { return m_next; }
		void SetNextID (UINT id) { m_next = id; }

		//次シークエンス名
		tstring GetNextName () const { return m_nextName; }
		void SetNextName ( tstring tstr ) { m_nextName = tstr; }
	};

	using WP_Sqc = std::weak_ptr < Sequence >;
	using P_Sqc = std::shared_ptr < Sequence >;
	using VP_Sqc = std::vector < P_Sqc >;
	using PVP_Sqc = std::shared_ptr < VP_Sqc >;


}	//namespace GAME


