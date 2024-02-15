//=================================================================================================
//
// CharaInput �\�[�X�t�@�C��
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// �w�b�_�t�@�C���̃C���N���[�h
//-------------------------------------------------------------------------------------------------
#include "CharaInput.h"

//-------------------------------------------------------------------------------------------------
// ��`
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//static����
	const UINT CharaInput::m_vGameKeyNum = 60;		//�L�[���͂̕ۑ��t���[����

	//�R���X�g���N�^
	CharaInput::CharaInput ()
		: m_playerID ( PLAYER_ID_1 ), m_cpu ( F )
	{
		for ( UINT i = 0; i < m_vGameKeyNum; ++i )
		{
			_GameKey gameKey;
			m_vGameKey.push_back ( gameKey );
		}
	}

	//�f�X�g���N�^
	CharaInput::~CharaInput ()
	{
	}

	//�X�V
	void CharaInput::Update ( bool dirRight )
	{
		//����̓��͂��Q�[���L�[�ɒ����ĕۑ�
		_GameKey gameKey;

		//�㉺�O��
		bool bKey8 = F;	//��
		bool bKey2 = F;	//��
		bool bKey4 = F;	//��(�E������)
		bool bKey6 = F;	//�O(�E������)

		if ( PLAYER_ID_1 == m_playerID )
		{
			bKey8 = CFG_IS_KEY ( _P1_UP );
			bKey2 = CFG_IS_KEY ( _P1_DOWN );
			if ( dirRight )	//�E������
			{
				bKey4 = CFG_IS_KEY ( _P1_LEFT );
				bKey6 = CFG_IS_KEY ( _P1_RIGHT );
			}
			else	//��������
			{
				bKey4 = CFG_IS_KEY ( _P1_RIGHT );
				bKey6 = CFG_IS_KEY ( _P1_LEFT );
			}
		}
		else if ( PLAYER_ID_2 == m_playerID )
		{
			bKey8 = CFG_IS_KEY ( _P2_UP );
			bKey2 = CFG_IS_KEY ( _P2_DOWN );
			if ( dirRight )	//�E������
			{
				bKey4 = CFG_IS_KEY ( _P2_LEFT );
				bKey6 = CFG_IS_KEY ( _P2_RIGHT );
			}
			else	//��������
			{
				bKey4 = CFG_IS_KEY ( _P2_RIGHT );
				bKey6 = CFG_IS_KEY ( _P2_LEFT );
			}
		}

		//3�ȏ㓯�������͗D�揇�ŏ���

		// �R�}���h�w��ł�12369874��

		//�΂ߗD��
		if ( bKey4 && bKey2 )	{ gameKey.SetLvr ( _GameKey::LVR_1, T ); }
		if ( bKey2 )			{ gameKey.SetLvr ( _GameKey::LVR_2, T ); }
		if ( bKey6 && bKey2 )	{ gameKey.SetLvr ( _GameKey::LVR_3, T ); }
		if ( bKey6 )			{ gameKey.SetLvr ( _GameKey::LVR_6, T ); }
		if ( bKey8 && bKey6 )	{ gameKey.SetLvr ( _GameKey::LVR_9, T ); }
		if ( bKey8 )			{ gameKey.SetLvr ( _GameKey::LVR_8, T ); }
		if ( bKey8 && bKey4 )	{ gameKey.SetLvr ( _GameKey::LVR_7, T ); }
		if ( bKey4 )			{ gameKey.SetLvr ( _GameKey::LVR_4, T ); }


		//==============================================================================
		//�{�^��
		bool bBtn0 = F; bool bBtn1 = F; bool bBtn2 = F; bool bBtn3 = F;
		bool bBtn4 = F; bool bBtn5 = F; bool bBtn6 = F; bool bBtn7 = F;

		if ( PLAYER_ID_1 == m_playerID )
		{
			bBtn0 = CFG_IS_KEY ( _P1_BTN0 );
			bBtn1 = CFG_IS_KEY ( _P1_BTN1 );
			bBtn2 = CFG_IS_KEY ( _P1_BTN2 );
			bBtn3 = CFG_IS_KEY ( _P1_BTN3 );
			bBtn4 = CFG_IS_KEY ( _P1_BTN4 );
			bBtn5 = CFG_IS_KEY ( _P1_BTN5 );
			bBtn6 = CFG_IS_KEY ( _P1_BTN6 );
			bBtn7 = CFG_IS_KEY ( _P1_BTN7 );
		}
		else if ( PLAYER_ID_2 == m_playerID )
		{
			bBtn0 = CFG_IS_KEY ( _P2_BTN0 );
			bBtn1 = CFG_IS_KEY ( _P2_BTN1 );
			bBtn2 = CFG_IS_KEY ( _P2_BTN2 );
			bBtn3 = CFG_IS_KEY ( _P2_BTN3 );
			bBtn4 = CFG_IS_KEY ( _P2_BTN4 );
			bBtn5 = CFG_IS_KEY ( _P2_BTN5 );
			bBtn6 = CFG_IS_KEY ( _P2_BTN6 );
			bBtn7 = CFG_IS_KEY ( _P2_BTN7 );
		}


		gameKey.SetBtn ( 0, bBtn0 );
		gameKey.SetBtn ( 1, bBtn1 );
		gameKey.SetBtn ( 2, bBtn2 );
		gameKey.SetBtn ( 3, bBtn3 );
		gameKey.SetBtn ( 4, bBtn4 );
		gameKey.SetBtn ( 5, bBtn5 );
		gameKey.SetBtn ( 6, bBtn6 );
		gameKey.SetBtn ( 7, bBtn7 );


		//�����O��̃L�[�ɕۑ�����
		gameKey.ReservePrevious ( m_vGameKey[0] );


		//�Q�[�����͂��X�V���Ȃ��猻�݃t���[������ۑ�
		for ( int i = m_vGameKeyNum - 1; i >= 1; -- i )
		{
			m_vGameKey[i] = m_vGameKey[i - 1];
		}
		m_vGameKey[0] = gameKey;	//�Ō�ɐ擪�ɋL�^
	}


	//------------------------------------------------------------------------------------
	//���[�g���X�g���`�F�b�N���Ċe��u�����`�̃R�}���h���B������Ă�����
	//�J�ڐ�̃A�N�V����ID��Ԃ�
	//�ߒl�Fenum { NO_COMPLETE (0xFFFFFFFF) } �s����
	UINT CharaInput::GetTransitID ( Chara & ch, P_Script pScp, bool dirRight )
	{
		//�L�����̎����[�g,�u�����`,�R�}���h�̎Q��
		const VP_Route vpRoute = ch.GetvpRoute ();
		const VP_Branch vpBranch = ch.GetvpBranch ();
		const VP_Command vpCommand = ch.GetvpCommand ();
		
		//�X�N���v�g�̎����[�g���X�g
		for ( UINT indexRoute : pScp->GetcvRouteID () )
		{
			const V_UINT vBranchID = vpRoute[indexRoute]->GetcvIDBranch ();

			//�Ώۂ̃u�����`���X�g
			for ( UINT indexBranch : vBranchID )
			{
				//�R�}���h����ȊO�͔�΂�
				if ( BRC_CMD != vpBranch[indexBranch]->GetCondition () ) { continue; }

				//�R�}���h�擾
				UINT indexCommand = vpBranch[indexBranch]->GetIndexCommand ();
				P_Command pCmd = vpCommand[indexCommand];

				//�ΏۃR�}���h���������Ă�����
				if ( pCmd->Compare ( m_vGameKey, dirRight ) )
				{
					//�J�ڐ�A�N�V����ID��Ԃ�
					return vpBranch[indexBranch]->GetIndexSequence ();
				}
			}
		}
		//�s�����̂Ƃ�
		return NO_COMPLETE;
	}

	
	//�������X�g�𐶐�����
	void CharaInput::MakeTransitIDList ( Chara & ch, P_Script pScp, bool dirRight )
	{
		//���������P��ID�ł͂Ȃ��A��������ID��D�揇�ʂŕۑ��������X�g��Ԃ�
		m_vCompID.clear ();

		//�L�����̎����[�g,�u�����`,�R�}���h�̎Q��
		const VP_Route vpRoute = ch.GetvpRoute ();
		const VP_Branch vpBranch = ch.GetvpBranch ();
		const VP_Command vpCommand = ch.GetvpCommand ();

		//�X�N���v�g�̎����[�g���X�g
		for ( UINT indexRoute : pScp->GetcvRouteID () )
		{
			//���[�g�̎擾
			P_Route pRut = vpRoute [ indexRoute ];
			const V_UINT vBranchID = vpRoute [ indexRoute ]->GetcvIDBranch ();

			//�Ώۂ̃u�����`���X�g
			for ( UINT indexBranch : vBranchID )
			{
				//�u�����`�̎擾
				P_Branch pBrc = vpBranch [ indexBranch ];

				//�R�}���h����ȊO�͔�΂�
				if ( BRC_CMD != pBrc->GetCondition () ) { continue; }

				//�R�}���h�̎擾
				UINT indexCommand = vpBranch [ indexBranch ]->GetIndexCommand ();
				P_Command pCmd = vpCommand [ indexCommand ];

				//�ΏۃR�}���h���������Ă�����
				if ( pCmd->Compare ( m_vGameKey, dirRight ) )
				{
					//�J�ڐ�A�N�V����ID��o�^����
					UINT id = vpBranch [ indexBranch ]->GetIndexSequence ();
					m_vCompID.push_back ( id );
				}
			}
		}
	}

	//�D�惊�X�g�̐擪���擾����
	UINT CharaInput::GetCompID ()
	{
		if ( m_vCompID.size() > 0 )
		{
			return m_vCompID [ 0 ];
		}
		return NO_COMPLETE;
	}

	void CharaInput::SetGameKey ( V_GAME_KEY & vKey )
	{
		UINT size = m_vGameKey.size ();
		for ( UINT i = 0; i < size; ++ i )
		{
			m_vGameKey [ i ] = vKey [ i ];
		}
	}


	const bool CharaInput::IsInput4 () const 
	{
		return m_vGameKey[0].GetLvr ( _GameKey::LVR_4 );
	}

	const bool CharaInput::IsInput1 () const 
	{
		return m_vGameKey[0].GetLvr ( _GameKey::LVR_1 );
	}


}	//namespace GAME

