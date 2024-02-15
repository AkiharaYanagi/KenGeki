//=================================================================================================
//
//	G_Ftg �\�[�X�t�@�C��
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// �w�b�_�t�@�C���̃C���N���[�h
//-------------------------------------------------------------------------------------------------
#include "G_Ftg.h"

//-------------------------------------------------------------------------------------------------
// ��`
//-------------------------------------------------------------------------------------------------
namespace GAME
{

	//------------------------------------------
	//	Static����
	//------------------------------------------
	// �V���O���g���I�u�W�F�N�g
	UP_G_Ftg	G_Ftg::m_inst;

	G_Ftg::G_Ftg ()
	{
		m_chara_center_x = GAME_WIDTH * 0.5f;
		m_wall_L = 0;
		m_wall_R = GAME_WIDTH;
	}

	G_Ftg::~G_Ftg ()
	{
	}

	//�L�����ʒu�ɂ���ʕ\���̊�ʒu
	void G_Ftg:: CulcPosMutualBase ( VEC2 pos1p, VEC2 pos2p )
	{
		//�ߒl
		float posMutualBase = 0;

		//�ʒu
		float averagex = (pos1p.x + pos2p.x) * 0.5f;	//���S
		float window_half = GAME_WINDOW_WIDTH * 0.5f;	//�\���E�B���h�E�̒��S(����)
		m_chara_center_x = averagex;

		//��ʒ[�\������

		//���ꉉ�o

		//�ǃ��Z�b�g
		if ( m_bResetPos )
		{
			posMutualBase = window_half - averagex;
			m_posMutualBase = VEC2 ( posMutualBase, 0 );

			//�ǈʒu
			m_wall_L = m_chara_center_x - GAME_WIDTH * 0.5f;
			m_wall_R = m_chara_center_x + GAME_WIDTH * 0.5f;

			m_bResetPos = F;
			return;
		}

		//�ǂ̒��S
		float wall_center = ( m_wall_L + m_wall_R ) * 0.5f;




		posMutualBase = window_half - averagex;

#if 1
		//����
		//��ʍ��[����\�����������̂Ƃ�
		if ( averagex < m_wall_L + window_half )
		{
			posMutualBase = 0 - m_wall_L;
		}
		//�E��
		//��ʉE�[����\�������E���̂Ƃ�
		else if ( averagex > m_wall_R - window_half )
		{
			posMutualBase = GAME_WINDOW_WIDTH - m_wall_R;
		}
		//����
		else
		{
			posMutualBase = window_half - averagex;
		}
#endif // 0




#if 0
		//����
		if ( averagex < 0 + window_half )
		{
			posMutualBase = 0;
		}
		//�E��
		else if ( averagex > GAME_WIDTH - window_half )
		{
			posMutualBase = (float)GAME_WINDOW_WIDTH - GAME_WIDTH;
		}
		//����
		else
		{
			posMutualBase = window_center - averagex;
		}

#endif // 0

		//��ʕ\���␳�ʒu
		m_posMutualBase = VEC2 ( posMutualBase, 0 );
	}




}	//namespace GAME

