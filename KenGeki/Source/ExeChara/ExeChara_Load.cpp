//=================================================================================================
//
// �G�O�[�L�����@�\�[�X�t�@�C��
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// �w�b�_�t�@�C���̃C���N���[�h
//-------------------------------------------------------------------------------------------------
#include "ExeChara.h"


//-------------------------------------------------------------------------------------------------
// ��`
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//================================================
	//	���[�h�֘A �����֐�
	//================================================
	void ExeChara::LoadCharaData ()
	{
		//--------------------------------------------
		//m_pChara�̃f�[�^�Ǎ�

		//���O����X�N���v�g�t�@�C�����w�肵�ăL�����̃��[�h
		//��	D3DX�̃e�N�X�`����p���邽�߃t�H�[�J�X�ύX���ȂǂɍĐݒ�(Reset())���K�v
		//		tstring name ( _T ("testChara.dat") );
		//		tstring name ( _T ( "chara.dat" ) );
		//		tstring name ( _T ("chara_standBin.dat") );
		tstring name ( _T ( "charaBin.dat" ) );
#if 0
		//�p�����[�^�ɂ��L�����̑I��
		switch ( m_name )
		{
		case CHARA_RAKUNO: name.assign ( _T ( "Rakuno.dat" ) ); break;
		case CHARA_YUKINO: name.assign ( _T ( "Yukino.dat" ) ); break;
		default: break;
		}
#endif // 0

		//�o�C�i���f�[�^�Ǎ�
		LoadCharaBin loadCharaBin ( name.c_str (), *m_pChara );
	}

	void ExeChara::LoadInput ()
	{
		//�L��������(�v���C���[, CPU, Network)
		m_pPlayerInput = make_shared < PlayerInput > ();
		m_pPlayerInput->SetPlayer ( m_playerID );

		m_pCPUInput = make_shared < CPUInput > ( shared_from_this (), m_pOther );
		m_pCPUInput->SetPlayer ( m_playerID );
		m_pCPUInput->Load ();

		//�v���C�����[�h(���͎��)�ɂ�鏉����
		switch ( m_playerMode )
		{
		case MODE_PLAYER: m_pCharaInput = m_pPlayerInput; break;
		case MODE_CPU: m_pCharaInput = m_pCPUInput; break;
		case MODE_NETWORK: m_pCharaInput = m_pPlayerInput; break;	//(��)
		default: break;
		}
	}

}	//namespace GAME

