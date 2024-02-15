//=================================================================================================
//
// LoadCharaFunc ソースファイル
//
//=================================================================================================
#include "LoadCharaFunc.h"
using namespace std;
using namespace std::placeholders;

//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
using LCF = LoadCharaFunc;

	//========================================================
	//ドキュメントからスクリプトに変換する
	//========================================================
	void LoadCharaFunc::_DocumentToChara ( const Document & document, Chara & ch )
	{
		//ルート下の操作用一時エレメント配列
		const PVP_Element pvpElemRt = document.Root()->GetpvpElement ();

		//---------------------------------------------------------------
		//ルート -> キャラ
		P_Element pElemChara = pvpElemRt->at ( 0 );
		PVP_Element pvpeCh = pElemChara->GetpvpElement ();

		//---------------------------------------------------------------
		//バージョン
		P_Element pElemVer = pvpeCh->at ( ELEM_CHARA_VER );

		//---------------------------------------------------------------
		//メインイメージリストのエレメント
		P_Element pElemMainImage = pvpeCh->at ( EL_MAIN_IMAGE_LIST );
		UINT sizeMainTexture = m_utl._ElemToUINT ( pElemMainImage, 0 );
		ch.GetpvpMainTexture()->resize ( sizeMainTexture );
		m_utl.ElemToNameArray ( pElemMainImage, m_vMainImageName );		//名前の保存

		//Efイメージリストのエレメント
		P_Element pElemEfImage = pvpeCh->at ( EL_EF_IMAGE_LIST );
		UINT sizeEfTexture = m_utl._ElemToUINT ( pElemEfImage, 0 );
		ch.GetpvpEfTexture()->resize ( sizeEfTexture );
		m_utl.ElemToNameArray ( pElemEfImage, m_vEfImageName );		//名前の保存

		//---------------------------------------------------------------
		//アクション配列のエレメント
		ElemAry ( pvpeCh->at ( EL_ACTION ), ch, bind ( & LCF::_ElemToActionC, this, _1, _2 ) );
		m_utl.ElemToNameArray ( pvpeCh->at ( EL_ACTION ), m_vActionName );

		//---------------------------------------------------------------
		//エフェクト配列のエレメント
		ElemAry ( pvpeCh->at ( EL_EFFECT ), ch, bind ( & LCF::_ElemToEffect, this, _1, _2 ) );
		m_utl.ElemToNameArray ( pvpeCh->at ( EL_EFFECT ), m_vEffectName );

		//---------------------------------------------------------------
		//コマンド配列のエレメント
		ElemAry ( pvpeCh->at ( EL_COMMAND ), ch, bind ( & LCF::_ElemToCommand, this, _1, _2 ) );
		m_utl.ElemToNameArray ( pvpeCh->at ( EL_COMMAND ), m_vCommandName );

		//ブランチ配列のエレメント
		ElemAry ( pvpeCh->at ( EL_BRANCH ), ch, bind ( & LCF::_ElemToBranch, this, _1, _2 ) );
		m_utl.ElemToNameArray ( pvpeCh->at ( EL_BRANCH ), m_vBranchName );

		//ルート配列のエレメント
		ElemAry ( pvpeCh->at ( EL_ROUTE ), ch, bind ( & LCF::_ElemToRoute, this, _1, _2 ) );
		m_utl.ElemToNameArray ( pvpeCh->at ( EL_ROUTE ), m_vRouteName );

		//---------------------------------------------------------------
		//スクリプト内のルート (アクションとコマンドの読込後、indexから実効オブジェクトのポインタを取得して設定する)
		_LoadRouteInScript ( ch );
	}


	//-------------------------------------------------------------------
	//	内部使用関数
	//-------------------------------------------------------------------

	//エレメントが持つ子エレメント配列に各読込関数を実行する
	void LoadCharaFunc::ElemAry ( P_Element pElem, Chara & ch, FP_ElemToCharaParam fp )
	{
		const PVP_Element pvpElem = pElem->GetpvpElement ();
		for ( P_Element pe : * pvpElem )
		{
			fp ( pe, ch );
		}
	}

	//エレメントポインタからアクションを読み込み、キャラに追加する
	void LoadCharaFunc::_ElemToActionC ( const P_Element pElem, Chara & ch )
	{
		P_Action pAction = make_shared < Action > ();	//代入用の新規アクション

		//---------------------------
		//attribute配列ポインタ
		const PVP_Attribute pvpAttrAction = pElem->GetpvpAttribute ();

		//アクション "名前"
		pAction->SetName ( pvpAttrAction->at ( ATTR_ACTION_NAME )->GetValue () );


		//UINT 次アクションID (終了時における次アクションのリスト内インデックス)
		pAction->SetNextID (m_utl._AttrToUINT (pvpAttrAction->at (ATTR_ACTION_ID)));

		//tstring 次アクション名 (終了時における次アクションの名前)
		pAction->SetNextName ( pvpAttrAction->at (ATTR_ACTION_ID)->GetValue () );


		//アクション属性
		UINT indexCategory = m_utl._AttrToUINT ( (*pvpAttrAction)[ATTR_ACTION_CATEGORY] );
		ACTION_CATEGORY category = static_cast <ACTION_CATEGORY> (indexCategory);
		pAction->SetCategory ( category );

		//アクション体勢
		ACTION_POSTURE ap = m_utl._AttrToACTION_POSTURE ( (*pvpAttrAction)[ATTR_ACTION_POSTURE] );
		pAction->SetPosture ( ap );

		//int 消費バランス値
//		pAction->SetBalance ( _AttrToInt ( (*pvpAttrAction)[ATTR_ACTION_BALANCE] ) );

		//---------------------------

		//<Action>内のElement
		//	<Script>　...不定数
		const PVP_Element pvpElemScript = pElem->GetpvpElement ();
		UINT _frame = 0;
		for ( P_Element pe : *pvpElemScript )
		{
			P_Script pScript = make_shared < Script > ();		//設定用スクリプトを生成
			_ElemToScript ( pe, pScript, _frame ++ );		//エレメントからスクリプトを読込
			pAction->AddpScript ( pScript );			//アクションにスクリプトを追加
		}

		//---------------------------

		ch.AddpAction ( pAction );			//キャラにアクションを追加
	}

	//エレメントポインタからエフェクトを読み込み、キャラに追加する
	void LoadCharaFunc::_ElemToEffect ( const P_Element pElem, Chara & ch )
	{
		P_Effect pEffect = make_shared < Effect > ();	//代入用の新規エフェクト
		//---------------------------

		//attribute配列ポインタ
		const PVP_Attribute pvpAttrAction = pElem->GetpvpAttribute ();

		//<Effect>内のElement
		//	<Script>　...不定数
		const PVP_Element pvpElemScript = pElem->GetpvpElement ();
		UINT _frame = 0;
		for ( P_Element pe : * pvpElemScript )
		{
			P_Script pScript = make_shared < Script > ();		//設定用スクリプトを生成
			_ElemToScript ( pe, pScript, _frame ++ );		//エレメントからスクリプトを読込
			pEffect->AddpScript ( pScript );			//エフェクトにスクリプトを追加
		}
		//---------------------------

		ch.AddpEffect ( pEffect );			//キャラにエフェクトを追加
	}

	void LoadCharaFunc::_ElemToScript ( const P_Element pElem, const P_Script pScript, UINT frame )
	{
		//-----------------------------------------------------------------------------
		//作業用アトリビュート配列ポインタ
		const PVP_Attribute pAttr = pElem->GetpvpAttribute ();

		//フレーム数
		pScript->SetFrame ( frame );

		//グループ(実効では用いない)

		//イメージID
//		//名前をIDに変換
//		tstring tstrImageName = pAttr->at ( ATTR_SCRIPT_IMAGE_ID )->GetValue ();
//		pScript->SetImageIndex ( m_utl.IndexOf ( m_vMainImageName, tstrImageName ) );
		pScript->SetImageIndex ( m_utl._AttrToUINT ( pAttr->at ( ATTR_SCRIPT_IMAGE_ID ) ) );

		//Pos
		VEC2 pos = m_utl._AttrToVec2 ( pAttr->at ( ATTR_SCRIPT_POS_X ), pAttr->at ( ATTR_SCRIPT_POS_Y ) );
		pScript->SetPos ( pos );

		//Vel
		VEC2 vel = m_utl._AttrToVec2 ( pAttr->at ( ATTR_SCRIPT_VEL_X ), pAttr->at ( ATTR_SCRIPT_VEL_Y ) );
//		pScript->SetVel ( vel );
		pScript->m_prmBattle.Vel = vel;

		//Acc
		VEC2 acc = m_utl._AttrToVec2 ( pAttr->at ( ATTR_SCRIPT_ACC_X ), pAttr->at ( ATTR_SCRIPT_ACC_Y ) );
//		pScript->SetAcc ( acc );
		pScript->m_prmBattle.Acc = acc;

		//計算状態
		CLC_ST clcSt = m_utl._AttrToCLC_ST ( pAttr->at ( ATTR_SCRIPT_CLCST ) );
		pScript->m_prmBattle.CalcState = clcSt;

		//----------------------------------------------------
		//パラメータ

		//power
		int power = m_utl._AttrToInt ( pAttr->at ( ATTR_SCRIPT_POWER ) );
		pScript->m_prmBattle.Power = power;

		//blackout
		UINT blackout = m_utl._AttrToUINT ( pAttr->at ( ATTR_SCRIPT_BLACKOUT ) );
		pScript->m_prmStaging.BlackOut = blackout;

		//viberation
		UINT viberation = m_utl._AttrToUINT ( pAttr->at ( ATTR_SCRIPT_VIBRATION ) );
		pScript->m_prmStaging.Vibration = viberation;

		//stop
		UINT stop = m_utl._AttrToUINT ( pAttr->at ( ATTR_SCRIPT_STOP ) );
		pScript->m_prmStaging.Stop = stop;

		//-----------------------------------------------------------------------------
		//スクリプト内部のエレメント
		const PVP_Element pvpElemInScript = pElem->GetpvpElement ();

		//ルート配列のエレメント
		P_Element pElemVecRoute = pvpElemInScript->at ( ELEM_SCRIPT_ROUTE );
		PVP_Element pvpElemRouteName = pElemVecRoute->GetpvpElement ();
		for ( P_Element pe : * pvpElemRouteName )
		{
			PVP_Attribute pa = pe->GetpvpAttribute ();
			UINT i = m_utl._AttrToUINT ( pa->at ( ATTR_SCRIPT_ROUTE_ID ) );
			pScript->AddRouteID ( i );
		}

		//EFジェネレート配列のエレメント
		P_Element pElemVecEfGnrt = pvpElemInScript->at ( ELEM_SCRIPT_EFGNRT );
		_ElemToEfGnrtArray ( pElemVecEfGnrt, pScript );

		//枠読込
		_LoadRectAll ( pvpElemInScript, pScript );
	}

	//エレメントポインタからコマンドを読み込む
	void LoadCharaFunc::_ElemToCommand ( const P_Element pElem, Chara & ch )
	{
		P_Command pCommand = make_shared < Command > ();	//代入用の新規作成
	
		//------------------------------------------------------------
		//アトリビュート配列
		const PVP_Attribute pvpAttrCommand = pElem->GetpvpAttribute ();

		//Name
		const P_Attribute pAttrCommand = pvpAttrCommand->at ( ATTR_COMMAND_NAME );
		tstring strName = pAttrCommand->GetValue ();
		pCommand->SetName ( strName );

		//Limit
		UINT limitTime = m_utl._AttrToUINT ( pvpAttrCommand->at ( ATTR_COMMAND_LIMIT ) );
		pCommand->SetLimitTime ( limitTime );

		//------------------------------------------------------------
		//エレメント

		//子Elementは_GameKeyを不定数保持
		const PVP_Element pvpElemGameKey = pElem->GetpvpElement ();
		for ( P_Element pe : * pvpElemGameKey )
		{
			_GameKeyCommand gameKeyCmd;	//コピー可
			_ElemToGameKeyCmd ( pe, gameKeyCmd );
			pCommand->AddGameKey ( gameKeyCmd );
		}
		//------------------------------------------------------------

		ch.AddpCommand ( pCommand );
	}

	//エレメントポインタからゲームキーコマンドを読み込む
	void LoadCharaFunc::_ElemToGameKeyCmd ( const P_Element pElem, _GameKeyCommand & gameKeyCmd )
	{
		//作業用
		const PVP_Attribute pvpAttrGameKey = pElem->GetpvpAttribute ();

		//否定フラグ
		gameKeyCmd.SetNot ( m_utl._AttrToBool ( pvpAttrGameKey->at ( ATTR_GAMEKEY_NOT ) ) );

		//文字列を読み込んでENUMに変換する
		int index = ATTR_GAMEKEY_LVR_ST;
		//・Lvr
		for ( UINT i = 0; i < (UINT)_GameKey::LVR_NUM; ++ i )
		{
			tstring strGameLvr = pvpAttrGameKey->at ( index + i )->GetValue ();
			gameKeyCmd.SetLvr ( i, StrToKeyState ( strGameLvr ) );
		}
		index += _GameKey::LVR_NUM;

		//・Btn
		for ( UINT i = 0; i < (UINT)_GameKey::BTN_NUM; ++ i )
		{
			tstring strGameKey = pvpAttrGameKey->at ( index + i )->GetValue();
			gameKeyCmd.SetBtn ( i, StrToKeyState ( strGameKey ) );
		}
	}

	_GameKeyCommand::GAME_KEY_STATE LoadCharaFunc::StrToKeyState ( tstring str )
	{
		if ( 0 == str.compare ( _T ( "KEY_OFF" ) ) ){ return _GameKeyCommand::GAME_KEY_OFF; }
		else	if ( 0 == str.compare ( _T ( "KEY_ON" ) ) ) { return _GameKeyCommand::GAME_KEY_ON; }
		else	if ( 0 == str.compare ( _T ( "KEY_PUSH" ) ) ) { return _GameKeyCommand::GAME_KEY_PUSH; }
		else	if ( 0 == str.compare ( _T ( "KEY_RELE" ) ) ) { return _GameKeyCommand::GAME_KEY_RELE; }
		else	if ( 0 == str.compare ( _T ( "KEY_WILD" ) ) ) { return _GameKeyCommand::GAME_KEY_WILD; }
		else	if ( 0 == str.compare ( _T ( "KEY_IS" ) ) ) { return _GameKeyCommand::GAME_KEY_IS; }
		else	if ( 0 == str.compare ( _T ( "KEY_NIS" ) ) ) { return _GameKeyCommand::GAME_KEY_NIS; }
		return _GameKeyCommand::GAME_KEY_STATE ();
	}
	
	void LoadCharaFunc::_ElemToBranch ( const P_Element pElem, Chara & ch )
	{
		P_Branch pBranch = make_shared < Branch > ();	//代入用の新規作成

		//------------------------------------------------------------
		//ブランチのアトリビュート
		const PVP_Attribute pa = pElem->GetpvpAttribute ();

		//分岐条件
		UINT indexCondition = m_utl._AttrToUINT ( pa->at ( ATTR_BRANCH_CONDITION ) );
		pBranch->SetCondition ( (BRANCH_CONDITION)indexCondition );

		//条件コマンドインデックス
		UINT indexCommand = m_utl._AttrToUINT ( pa->at ( ATTR_BRANCH_COMMAND_ID ) );
		pBranch->SetIndexCommand ( indexCommand );

		//遷移先シークエンスインデックス
		UINT indexAction = m_utl._AttrToUINT ( pa->at ( ATTR_BRANCH_SEQUENCE_ID ) );
		pBranch->SetIndexSequence ( indexAction );

		//遷移先スクリプトインデックス
		UINT indexFrame = m_utl._AttrToUINT ( pa->at ( ATTR_BRANCH_FRAME_INDEX ) );
		pBranch->SetIndexFrame ( indexFrame );

		//------------------------------------------------------------
		ch.AddpBranch ( pBranch );			//キャラに追加
	}

	void LoadCharaFunc::_ElemToRoute ( const P_Element pElem, Chara & ch )
	{
		P_Route pRoute = make_shared < Route > ();	//代入用の新規作成
		//------------------------------------------------------------
		//ルートのエレメント
		P_Element pElemBlanchNameListNum = pElem->GetpvpElement ()->at ( 0 );
		const PVP_Element pvpElemBlanchNameList = pElemBlanchNameListNum->GetpvpElement ();
		for ( P_Element pe : * pvpElemBlanchNameList )
		{
			P_Attribute pa = pe->GetpvpAttribute ()->at ( ATTR_NAME_0 );
			UINT i = m_utl.IndexOf ( m_vBranchName, pa->GetValue () );
			pRoute->AddBranchID ( i );
		}
		//------------------------------------------------------------
		ch.AddpRoute ( pRoute );			//キャラに追加
	}

	//エレメントポインタからEFジェネレートを読み込む
	void LoadCharaFunc::_ElemToEfGnrtArray ( const P_Element pElem, const P_Script & pScript )
	{
		//EFジェネレート配列のエレメント配列
		const PVP_Element pvpElemEfGnrt = pElem->GetpvpElement ();
		for ( P_Element pe : * pvpElemEfGnrt )
		{
			P_EfGnrt pEfGnrt = make_shared < EffectGenerate > ();			//代入用に新規作成
			_ElemToEfGnrt ( pe, pEfGnrt );
			pScript->AddpEfGnrt ( pEfGnrt );			//スクリプトに追加
		}
	}

	void LoadCharaFunc::_ElemToEfGnrt ( const P_Element pElem, const P_EfGnrt & pEfGnrt )
	{
		///EFジェネレートのアトリビュート
		const PVP_Attribute pa = pElem->GetpvpAttribute();

		//アトリビュート列挙
		enum { NAME, INDEX, POS_X, POS_Y, POS_Z, GNRT, LOOP, SYNC, };

		//指定Efインデックス
		pEfGnrt->SetIndex ( m_utl._AttrToUINT ( pa->at ( INDEX ) ) );

		//生成位置
		pEfGnrt->SetPos ( m_utl._AttrToVec2 ( pa->at ( POS_X ), pa->at ( POS_Y ) ) );

		//z位置
		pEfGnrt->SetZ ( m_utl._AttrToFloat ( pa->at ( POS_Z ) ) );

		//生成
		pEfGnrt->SetGnrt ( m_utl._AttrToBool ( pa->at ( GNRT ) ) );

		//繰返
		pEfGnrt->SetLoop ( m_utl._AttrToBool ( pa->at ( LOOP ) ) );

		//同期
		pEfGnrt->SetSync ( m_utl._AttrToBool ( pa->at ( SYNC ) ) );
	}

	
	//------------------------------------------------------------


	//枠読込
#if 0
	//関数ポインタ
	void LoadCharaUtl::_LoadRect ( const P_Element pElemRect, P_Script pScript, void (Script::* const pFuncAddRect)(RECT) )
	{
		//アトリビュートはサイズのみ
		const PVP_Attribute pvecpAttrVecRect = pElemRect->GetpvpAttribute();
		UINT sizeRect = _AttrToUINT ( (*pvecpAttrVecRect)[0] );

		const PVP_Element pvecpElemRect = pElemRect->GetpvpElement();

		//枠
		for ( UINT i = 0; i < sizeRect; ++i )
		{
			//エレメント
			P_Element pElem = (*pvecpElemRect)[i];

			//アトリビュート
			const PVP_Attribute pa = pElem->GetpvpAttribute();
			RECT rect = _AttrToRect ( (*pa)[0], (*pa)[1], (*pa)[2], (*pa)[3] );
			
			//関数ポインタで各枠に設定
			(pScript->*pFuncAddRect)( rect );
		}
	}
#endif // 0
	//引数：エレメントポインタ, スクリプトポインタ, 枠を設定するScript内の関数オブジェクト
	//設定用関数をstd::functionで指定
	void LoadCharaFunc::_LoadRect ( const P_Element pElemRect, P_Script pScript, FP_SetRect fp_SetRect )
	{
		//アトリビュートはサイズのみ
		const PVP_Attribute pvpAttrVecRect = pElemRect->GetpvpAttribute ();
		UINT sizeRect = m_utl._AttrToUINT ( (*pvpAttrVecRect)[0] );

		//エレメント
		const PVP_Element pvpElemRect = pElemRect->GetpvpElement ();

		//枠
		for ( UINT i = 0; i < sizeRect; ++i )
		{
			//エレメント
			P_Element pElem = (*pvpElemRect)[i];

			//アトリビュート
			const PVP_Attribute pa = pElem->GetpvpAttribute();
			RECT rect = m_utl._AttrToRect ( (*pa)[0], (*pa)[1], (*pa)[2], (*pa)[3] );
			
			//FP_SetRectはpScriptと関連付けてあるのでそのまま呼び出す
			fp_SetRect ( rect );
		}
	}

	//引数：エレメントポインタ, 枠を設定するScript内の関数オブジェクト
	void LoadCharaFunc::_LoadRect ( const P_Element pElemRect, FP_SetRect fp_SetRect )
	{
		const PVP_Element pvpElemRect = pElemRect->GetpvpElement ();
		for ( P_Element pElem : * pvpElemRect )
		{
			const PVP_Attribute pa = pElem->GetpvpAttribute();
			RECT rect = m_utl._AttrToRect ( pa->at(0), pa->at(1), pa->at(2), pa->at(3) );
			fp_SetRect ( rect );	//FP_SetRectはpScriptと関連付けてあるのでそのまま呼び出す
		}
	}

	void LoadCharaFunc::_LoadRectAll ( const PVP_Element pvpElem, const P_Script & pScript )
	{
#if 0
		//関数ポインタで指定する
		_LoadRect ( (*cpvpElemInScript)[CRECT], pScript, &Script::AddCRect );
		_LoadRect ( (*cpvpElemInScript)[ARECT], pScript, &Script::AddARect );
		_LoadRect ( (*cpvpElemInScript)[HRECT], pScript, &Script::AddHRect );
		_LoadRect ( (*cpvpElemInScript)[ORECT], pScript, &Script::AddORect );
#endif // 0
#if 0
		//bind
		FP_SetRect func_c = std::bind ( & Script::AddCRect, pScript, std::placeholders::_1 );
		FP_SetRect func_a = std::bind ( & Script::AddARect, pScript, std::placeholders::_1 );
		FP_SetRect func_h = std::bind ( & Script::AddHRect, pScript, std::placeholders::_1 );
		FP_SetRect func_o = std::bind ( & Script::AddORect, pScript, std::placeholders::_1 );
#endif // 0

		//ラムダ式
		FP_SetRect func_c = [ & pScript ] ( RECT r ) { pScript->AddCRect ( r ); };
		_LoadRect ( pvpElem->at ( ELEM_SCRIPT_CRECT ), pScript, func_c );

		FP_SetRect func_a = [ & pScript ] ( RECT r ) { pScript->AddARect ( r ); };
		_LoadRect ( pvpElem->at ( ELEM_SCRIPT_ARECT ), pScript, func_a );

		FP_SetRect func_h = [ & pScript ] ( RECT r ) { pScript->AddHRect ( r ); };
		_LoadRect ( pvpElem->at ( ELEM_SCRIPT_HRECT ), pScript, func_h );

		FP_SetRect func_o = [ & pScript ] ( RECT r ) { pScript->AddORect ( r ); };
		_LoadRect ( pvpElem->at ( ELEM_SCRIPT_ORECT ), pScript, func_o );
	}


	//------------------------------------------------------------
	//スクリプト内ルート (アクションとコマンドの読込後、indexから実効オブジェクトのポインタを取得して設定する)
	void LoadCharaFunc::_LoadRouteInScript ( Chara & ch )
	{
#if 0
		PVP_Action pvpAction = ch.GetpvpAction ();
		for ( P_Action pAc : * pvpAction )
		{
			PVP_Script pvpScript = pAc->GetpvpScript ();
			for ( P_Script pSc : * pvpScript )
			{
				PVP_Branch pvpBranch = pSc->GetpvpBranch ();
				for ( P_Branch pBr : * pvpBranch )
				{
					pBr->SetpSequence ( ch.GetpAction ( pBr->GetIndexAction () ) );
					pBr->SetpCommand ( ch.GetpCommand ( pBr->GetIndexCommand () ) );
				}
			}
		}
#endif // 0
	}

#if 0
	//基本状態アクションID
	void LoadCharaFunc::_ElemToBasicActionID ( const P_Element pElem, Chara & ch )
	{
		//基本状態アクションID (nameは飛ばす)
		const PVP_Element pvpElemBasicAction = pElem->GetpvpElement ();
//		P_Element pElemBasicAction = pvpElemBasicAction->at ( 0 );
		for ( UINT i = 0; i < BASIC_ACTION_NUM; ++ i )
		{
			UINT n = m_utl._ElemToUINT ( pvpElemBasicAction->at ( i ), 0 );
			ch.SetBsAction ( static_cast < BASIC_ACTION > ( i ), n );
		}
	}
#endif // 0

	//------------------------------------------------------------
	//イメージアーカイブ読込
	void LoadCharaFunc::_LoadImage ( ifstream & ifstrm, PVP_TxBs pvpTexture )
	{
		//イメージの個数でループ
		UINT size = pvpTexture->size ();
		for ( UINT i = 0; i < size; ++ i )
		{
			//イメージの個別サイズを読込
			UINT imageSize = 0;
			ifstrm.read ( (char*)&imageSize, sizeof ( UINT ) );

			//一時領域の確保から、イメージ本体の読込
			char* imageBuf = new char [ imageSize ];
			ifstrm.read ( imageBuf, imageSize );

			//メモリ上のデータからゲームテクスチャに変換
			P_TxMem pGameTexture = make_shared < TxMem > ( (LPCVOID)imageBuf, imageSize );

			//一時領域は解放する
			delete[] imageBuf;

			//キャラ内部のテクスチャリストに加える
			(*pvpTexture) [ i ] = pGameTexture;
		}
	}

}	//namespace GAME

