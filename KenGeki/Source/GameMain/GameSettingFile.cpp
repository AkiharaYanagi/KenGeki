//=================================================================================================
//
// 設定ファイル
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ヘッダファイルのインクルード
//-------------------------------------------------------------------------------------------------
#include "GameSettingFile.h"

//-------------------------------------------------------------------------------------------------
// 定義
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//コンストラクタ
	GameSettingFile::GameSettingFile ()
		: m_startMode ( START_BATTLE )
		, m_playerMode1p ( MODE_PLAYER ), m_playerMode2p ( MODE_PLAYER )
		, m_name1p ( CHARA_TEST ), m_name2p ( CHARA_TEST )
		, m_demo ( F )
	{
	}

	//コピーコンストラクタ
	GameSettingFile::GameSettingFile ( const GameSettingFile & rhs )
	{
		m_startMode		= rhs.m_startMode;
		m_playerMode1p	= rhs.m_playerMode1p;
		m_playerMode2p	= rhs.m_playerMode2p;
		m_name1p		= rhs.m_name1p;
		m_name2p		= rhs.m_name2p;
		m_demo			= rhs.m_demo;
	}

	void GameSettingFile::Load ()
	{
		try
		{
			//入力ストリームを生成
			ifstream ifstrm( _T( "GameSettings.dat" ), ios::in | ios::binary );

			//見つからないときデフォルトの値を設定して終了
			if (!ifstrm) { SetDefault(); return; }

			byte tempMode = 0;
			ifstrm.read( (char*)&tempMode, sizeof( byte ) );
			m_startMode = (START_MODE)tempMode;

			byte demo = 0;
			ifstrm.read( (char*)&demo, sizeof( bool ) );
			m_demo = (bool)demo;

			byte tempInput1p = 0;
			byte tempInput2p = 0;
			ifstrm.read( (char*)&tempInput1p, sizeof( byte ) );
			ifstrm.read( (char*)&tempInput2p, sizeof( byte ) );
			m_playerMode1p = (PLAYER_MODE)tempInput1p;
			m_playerMode2p = (PLAYER_MODE)tempInput2p;

			byte tempName1p = 0;
			byte tempName2p = 0;
			ifstrm.read( (char*)&tempName1p, sizeof( byte ) );
			ifstrm.read( (char*)&tempName2p, sizeof( byte ) );
			m_name1p = (CHARA_NAME)tempName1p;
			m_name2p = (CHARA_NAME)tempName2p;

			//終了
			ifstrm.close();

		}
		catch (...)
		{
			SetDefault ();
		}
	}

	GameSettingFile::~GameSettingFile ()
	{
	}

	void GameSettingFile::Save()
	{
		try
		{
			//入力ストリームを生成
			ofstream ofstrm ( _T("GameSettings.dat"), ios::out | ios::binary );
			
			ofstrm.write ( (char*)&m_startMode, sizeof(byte));
			ofstrm.write ( (char*)&m_demo, sizeof ( bool ) );
			ofstrm.write ( (char*)&m_playerMode1p, sizeof ( byte ) );
			ofstrm.write ( (char*)&m_playerMode2p, sizeof ( byte ) );
			ofstrm.write ( (char*)&m_name1p, sizeof ( byte ) );
			ofstrm.write ( (char*)&m_name2p, sizeof ( byte ) );

			//終了
			ofstrm.close ();
		}
		catch (...)
		{
			//エラー時何もしない
		}
	}
	
	void GameSettingFile::SetDefault ()
	{
		m_startMode = START_BATTLE;
		m_playerMode1p = MODE_PLAYER;
		m_playerMode2p = MODE_PLAYER;
		m_name1p = CHARA_TEST;
		m_name2p = CHARA_TEST;
		m_demo = F;
	}


}	//namespace GAME

