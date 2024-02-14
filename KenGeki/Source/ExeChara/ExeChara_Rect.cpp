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
	//	����g�֘A �����֐�
	//================================================

	//�ڐG�g�ݒ�
	void ExeChara::SetCollisionRect ()
	{
		m_charaRect->SetCRect ( m_pScript->GetpvCRect (), m_btlPrm.GetDirRight (), m_btlPrm.GetPos () );
	}

	//���E�E�U���E����E�g�ݒ�
	void ExeChara::SetRect ()
	{
		SetOffsetRect ();
		SetAttackRect ();
		SetHitRect ();
	}

	//���E�g�ݒ�
	void ExeChara::SetOffsetRect ()
	{
		m_charaRect->SetORect ( m_pScript->GetpvORect (), m_btlPrm.GetDirRight (), m_btlPrm.GetPos () );
	}

	//�U���g�ݒ�
	void ExeChara::SetAttackRect ()
	{
		//======================================
		// �q�b�g���Ɍ�̍U���g���ꎞ��~(���i�h�~)
		//======================================

		//�q�b�g��Ԃ��m�F (�q�b�g��Ԑݒ�� OnHit(), OnClang() )
		bool bHit = m_btlPrm.GetHitEst () || m_btlPrm.GetClang ();

		//�f�o�b�O�\������
		if ( m_playerID == PLAYER_ID_1 )
		{
			DBGOUT_WND_F ( 4, _T ( "bHit = %d" ), bHit ? 1 : 0 );
			P_Timer ptHitStop = m_btlPrm.GetTmr_HitStop ();
			DBGOUT_WND_F ( 5, _T ( "hitStop = %d" ), ptHitStop->GetTime () );
			UINT hitpitch = m_pAction->GetHitPitch ();
			P_Timer ptHitPitch = m_btlPrm.GetTmr_HitPitch ();
			DBGOUT_WND_F ( 6, _T ( "hitPitch = %d / %d" ), ptHitPitch->GetTime (), hitpitch );
			UINT hitmax = m_pAction->GetHitNum ();
			UINT hitnum = m_btlPrm.GetHitNum ();
			DBGOUT_WND_F ( 7, _T ( "hitnum = %d / %d" ), hitnum, hitmax );
		}
#if DEBUG_DISP
#else
		//�f�o�b�O�\���Ȃ�
#endif // DEBUG_DISP

		//--------------------------------------------------
		//�q�b�g���Ă��Ȃ���(�ʏ펞)�A�U���g��ݒ肵�ďI��
		if ( ! bHit )
		{
			//�U���g�̐ݒ�
			m_charaRect->SetARect ( m_pScript->GetpvARect (), m_btlPrm.GetDirRight (), m_btlPrm.GetPos () );
			return;
		}

		//--------------------------------------------------
		//�U���������E�ō����ɓ���A�N�V�������̂ݘg������������

		//1�q�b�g�Z�͍U���g����ɂ��ďI��
		if ( 1 >= m_pAction->GetHitNum () )
		{
			//�U���g����ɂ���
			m_charaRect->ResetARect ();
			return;
		}

		//--------------------------------------------------
		//�A�N�V�������q�b�g��������ɒB�����Ƃ��g����ɂ���

		UINT hitmax = m_pAction->GetHitNum ();
		if ( hitmax <= m_btlPrm.GetHitNum () )
		{
			//�U���g����ɂ���
			m_charaRect->ResetARect ();
			return;
		}

		//--------------------------------------------------
		//���i�\�ȃA�N�V�����́A�q�b�g�Ԋu���`�F�b�N���čU���g���Đݒ肷��
		P_Timer ptHitPitch = m_btlPrm.GetTmr_HitPitch ();
		UINT pitch = m_pAction->GetHitPitch ();
		if ( pitch < ptHitPitch->GetTime () )
		{
			//�ăX�^�[�g
			m_btlPrm.HitPitchWaitStart ();

			//�U���g�̐ݒ�
			m_charaRect->SetARect ( m_pScript->GetpvARect (), m_btlPrm.GetDirRight (), m_btlPrm.GetPos () );
			return;
		}

		//--------------------------------------------------
		//�Y�����Ȃ���΍U���g����ɂ���
		m_charaRect->ResetARect ();
	}

	//����g�ݒ�
	void ExeChara::SetHitRect ()
	{
		m_charaRect->SetHRect ( m_pScript->GetpvHRect (), m_btlPrm.GetDirRight (), m_btlPrm.GetPos () );
	}

	//�g�\���ؑ�
	void ExeChara::OnDispRect ()
	{
		m_dispChara->OnRect ();
		m_oprtEf->OnDispRect ();
	}
	void ExeChara::OffDispRect ()
	{
		m_dispChara->OffRect ();
		m_oprtEf->OffDispRect ();
	}

	//�g�����ׂă��Z�b�g
	void ExeChara::ResetRect ()
	{
		m_charaRect->ResetAllRect ();
	}

}	//namespace GAME

