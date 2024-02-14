//=================================================================================================
//
// �G�O�[�L�����@�\�[�X�t�@�C��
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// �w�b�_�t�@�C���̃C���N���[�h
//-------------------------------------------------------------------------------------------------
#include "ExeChara.h"
#include "../GameMain/SoundConst.h"


//-------------------------------------------------------------------------------------------------
// ��`
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//================================================
	//	����g�֘A �����֐�
	//================================================

	//---------------------------------------------
	//�C�x���g

	void ExeChara::OnDashBranch ()
	{
		m_btlPrm.AddBalance ( -1000 );
		TransitAction_Condition_I ( BRC_DASH, F );	//�_�b�V�����E�E����
	}

	//�ō�����
	void ExeChara::OnClang ( UINT nLurch, CLANG_DECISION_WL clangDecision )
	{
#if 0
		//		if ( CD_LOSE == clangDecision )
		{
			//��Ԃ̕ύX
			m_actionID = m_pChara->GetBsAction ( BA_CLANG );
			m_frame = 0;
		}
#endif // 0

		//����̃p�����[�^�ő���
		P_Script scp = m_pOther.lock ()->GetpScript ();
		int balance_e = scp->m_prmBattle.Balance_E;
		m_btlPrm.AddBalance ( balance_e );

		//�o�����X�A�E�g
		if ( m_btlPrm.GetBalance () <= 0 )
		{
			//			SetAction ( _T ( "Dotty" ) );
			SetAction ( _T ( "����" ) );
		}

		//���g�̏�ԕύX
		m_btlPrm.SetClang ( true );		//�ō����
		//		m_lurch = nLurch;		//�̂����莞�Ԃ̐ݒ�
		m_btlPrm.GetTmr_HitStop ()->Start ();		//�q�b�g�X�g�b�v�̐ݒ�
	}


	//����E�U�� �� �����E���炢
	//���炢��ԁE�_���[�W����
#if 0
	void ExeChara::OnDamaged ( int damage )
	{
		bool hit = true;
		bool guard = false;

		//��𔻒�
#if 0
		//�U�����łȂ��A���v�f�����͂���Ă���Ƃ�
		//�_�b�V�����A��낯���Ȃǂ����O���� (�����͉\)
		if ( !IsAttacking () && !( GetActionName () == Chara::DOTTY ) )
		{
			if ( IsInput2E () )
			{
				//�����Ɉڍ�
				TransitAction ( m_pChara->GetBsAction ( Chara::AVOID ) );

				//�q�b�g�X�g�b�v�̐ݒ�
				m_hitstop = 15;

				//�G�t�F�N�g�̔���
				//				float dispGameBaseX = GetDispGameBaseX ();	//��ʒ[�ɂ��\����ʒu
				m_dispChara.OnAvoid ( m_ptChara, m_dirRight );

				//SE
				SoundArchiver::instance ()->Play ( 2 );

				//�����̃o�����X�����E�ǉ���
				int balanceDamage0 = 200 - damage;		//�_���[�W�̋t��(L>M>H)
				if ( m_balance < balanceDamage0 ) { balanceDamage0 = m_balance; }	//���ݒl�ȏ�͕\������
				m_balance -= balanceDamage0;

				//����̃o�����X����
				int balanceDamage = damage;		//�_���[�W���o�����X����(L<M<H)
				int balance = m_pOther->GetBalance ();
				if ( balance < balanceDamage ) { balanceDamage = balance; }	//���ݒl�ȏ�͕\������
				m_pOther->AddbBalance ( -1 * balanceDamage );

				return;
			}
		}
#endif // 0

		//�K�[�h����
#if 0
		//�U�����łȂ��Ƃ�
		//�_�b�V�����A��낯���Ȃǂ����O���� (�����͉\)
		if ( !IsAttacking () && !IsDamaged () )
		{
			//�㒆���i�@���򏈗�
			ACTION_POSTURE ap = m_pOther.lock ()->GetPosture ();

			//����������͂���Ă���Ƃ�
			if ( m_pCharaInput->IsInput4 () )
			{
				//����̏�Ԃŕ���
				switch ( ap )
				{
				case AP_STAND:	hit = false; guard = true; break;
				case AP_CROUCH: hit = true; guard = false; break;
				case AP_JUMP:	hit = false; guard = true; break;
				}
			}

			//�㉺���������͂���Ă���Ƃ�
			if ( m_pCharaInput->IsInput1 () )
			{
				//����̏�Ԃŕ���
				switch ( ap )
				{
				case AP_STAND:	hit = false; guard = true; break;
				case AP_CROUCH: hit = false; guard = true; break;
				case AP_JUMP:	hit = true; guard = false; break;
				}
			}

		}

		//--------------------------------------------------------

		//�K�[�h����
		if ( guard )
		{
			tstring act;
			switch ( m_pAction->GetPosture () )
			{
			case AP_STAND:	act.assign ( _T ( "S_Guard" ) ); break;
			case AP_CROUCH: act.assign ( _T ( "C_Guard" ) ); break;
			case AP_JUMP:	act.assign ( _T ( "J_Guard" ) ); break;
			}
			TransitAction ( m_pChara->GetActionID ( act ) );

			m_FirstEf = true;				//����̂݃G�t�F�N�g����
			m_tmrHitstop->Start ();				//�q�b�g�X�g�b�v�̐ݒ�

			//SE
			SOUND->Play ( SE_Guard );
		}
#endif // 0

		//���炢�� ( �K�[�h�����Ă��Ȃ� ) && ( �����ύX����Ă��Ȃ� )
		if ( hit && ! m_btlPrm.GetForcedChange () )
		{
			int lf = m_btlPrm.GetLife ();
			//�_���[�W�����C�t�ɂ���ĕ␳(�����l)
			if ( lf < LIFE_MAX * 0.5f )
			{
				damage = (int)( damage * ( 0.001f * ( 0.5f * LIFE_MAX + lf ) ) );
			}

			//�_���[�W����
			if ( lf < damage ) { m_btlPrm.SetDamage ( lf ); }	//���C�t�ȏ�͕\������
			else { m_btlPrm.SetDamage ( damage ); }		//�\���p

			m_btlPrm.SetLife ( lf - damage );

			//��Ԃ̕ύX
#if 0
			tstring act;
			switch ( m_pAction->GetPosture () )
			{
			case AP_STAND:	act.assign ( _T ( "Damaged_L" ) ); break;
			case AP_CROUCH: act.assign ( _T ( "C_DamagedL" ) ); break;
			case AP_JUMP:	act.assign ( _T ( "J_DamagedL" ) ); break;
			}
			TransitAction ( m_pChara->GetActionID ( act ) );
#endif // 0

			//���̑��@����
			m_btlPrm.GetTmr_HitStop ()->Start ();			//�q�b�g�X�g�b�v�̐ݒ�
			m_btlPrm.SetFirstEf ( true );			//����̂݃G�t�F�N�g����
			m_btlPrm.SetFirstSE ( true );			//����̂�SE����

		}
	}
#endif // 0


	//����E�U�� �� �����E���炢
	//���炢��ԁE�_���[�W����
	void ExeChara::OnDamaged ()
	{
		//����
		P_Script pScp = m_pOther.lock ()->m_pScript;

		//-------------------------------------------------
		//�_���[�W����
		int damage = pScp->m_prmBattle.Power;

#if 0
		//�_���[�W�����C�t�ɂ���ĕ␳(�����l)
		int lf = m_btlPrm.GetLife ();
		if ( lf < LIFE_MAX * 0.5f )
		{
			damage = (int)( damage * ( 0.001f * ( 0.5f * LIFE_MAX + lf ) ) );
		}
#endif // 0

#if 0
		if ( lf < damage ) { m_btlPrm.SetDamage ( lf ); }	//���C�t�ȏ�͕\������
		else { m_btlPrm.SetDamage ( damage ); }		//�\���p
		m_btlPrm.SetLife ( lf - damage );
#endif // 0
		m_btlPrm.AddLife ( - damage );

		//-------------------------------------------------
		//�o�����X����
		int b_e = pScp->m_prmBattle.Balance_E;
		int bl = m_btlPrm.GetBalance ();
		m_btlPrm.SetBalance ( bl - b_e );

		//-------------------------------------------------
		//���̑��@����
		m_btlPrm.GetTmr_HitStop ()->Start ();			//�q�b�g�X�g�b�v�̐ݒ�
		m_btlPrm.SetFirstEf ( true );			//����̂݃G�t�F�N�g����
		m_btlPrm.SetFirstSE ( true );			//����̂�SE����
	}


	//�����E�U�� -> ����E���炢
	//�q�b�g����(�U��������)
	void ExeChara::OnHit ()
	{
		//-----------------------------------------------------
		//�������� (���聨�����łȂ��ƃX�N���v�g���ς���Ă��܂�)
		TransitAction_Condition_E ( BRC_THR_E, T );	//�����E����
		TransitAction_Condition_I ( BRC_THR_I, F );	//�����E����
		TransitAction_Condition_E ( BRC_HIT_E, T );	//�q�b�g�E����
		TransitAction_Condition_I ( BRC_HIT_I, F );	//�q�b�g�E����

		//-----------------------------------------------------

		//m_btlPrm.SetHitEst ( true );		//�U�������t���O
		//m_btlPrm.GetTmr_HitStop ()->Start ();		//�q�b�g�X�g�b�v�̐ݒ�
		m_btlPrm.OnHit ();
	}

	//�G�t�F�N�g�q�b�g����(�U��������)
	void ExeChara::OnEfHit ()
	{
		m_btlPrm.SetHitEst ( true );		//�U�������t���O
		//		m_tmrHitstop->Start ();		//�G�t�F�N�g�̓q�b�g�X�g�b�v���Ȃ�

		m_btlPrm.GetTmr_HitPitch ()->Start ();
	}

#if 0
	//�I�����o
	void ExeChara::OnEndAct ()
	{
		m_charaState = CHST_WIN_WAIT;
	}

	//�����I�����
	void ExeChara::ForcedEnd ()
	{
		if ( m_btlPrm.GetLife () <= 0 )
		{
			m_charaState = CHST_DOWN_END;
			//			m_actionID = m_pChara->GetBsAction ( BA_DOWN );
			TransitAction ( m_actionID );
		}
		else
		{
			m_charaState = CHST_WIN_END;
			//			m_actionID = m_pChara->GetBsAction ( BA_WIN );
			TransitAction ( m_actionID );
		}
	}
#endif // 0


}	//namespace GAME

