//=================================================================================================
//
// OperateEffect �\�[�X�t�@�C��
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// �w�b�_�t�@�C���̃C���N���[�h
//-------------------------------------------------------------------------------------------------
#include "OperateEffect.h"

//-------------------------------------------------------------------------------------------------
// ��`
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//�R���X�g���N�^
	OperateEffect::OperateEffect ()
	{
		m_plpExeEffect = make_shared < LP_ExEf > ();
	}

	//�f�X�g���N�^
	OperateEffect::~OperateEffect ()
	{
		Rele ();
	}

	void OperateEffect::Init ()
	{
		for ( P_ExEf pEx : * m_plpExeEffect )
		{
			pEx->Init ();
		}
		m_plpExeEffect->clear();
		TASK_VEC::Init ();
	}

	//���
	void OperateEffect::Rele ()
	{
		m_plpExeEffect->clear();
		TASK_VEC::Rele ();
	}

	//�G�t�F�N�g���O����
	void OperateEffect::MakeEfList ( P_Chara p )
	{
		//�L�����|�C���^��ۑ�
		m_pChara = p;
		m_pvpEfTexture = p->GetpvpEfTexture ();
		m_vpBranch = p->GetvpBranch ();
		m_vpRoute = p->GetvpRoute ();

		//���ׂẴA�N�V�����ƃX�N���v�g������
		PVP_Action pvpAction = m_pChara->GetpvpAction ();
		for ( P_Action pAction : ( * pvpAction ) )
		{
			PVP_Script pvpScript = pAction->GetpvpScript ();
			for ( P_Script pScript : ( * pvpScript ) )
			{
				PVP_EfGnrt pvpEfGnrt = pScript->GetpvpEfGnrt ();
				for ( P_EfGnrt pEfGnrt : ( * pvpEfGnrt ) )
				{
					//�񐶐��Ȃ珉��ɓo�^���Ă����AID�ŉғ���Ԃɂ���
					if ( ! pEfGnrt->GetGnrt () )
					{
						//�G�t�F�N�g�C���f�b�N�X�̎擾
						UINT index = pEfGnrt->GetIndex ();

						//�G�t�F�N�g�̎擾
						P_Effect pEf = m_pChara->GetpEffect ( index );

						//�G�t�F�N�g�Ǘ��ɓn����ID�𓾂�
//						UINT id = m_oprtEf.Addstd::vectorEffect ( pEf, pEfGnrt->GetZ () );

						//ID���L�^
//						pEfGnrt->SetID ( id );
					}
				}
			}
		}
	}

	void OperateEffect::MoveEffect ( P_Script pScp, BtlParam & btlprm )
	{
		//�G�t�F�N�g����
		if ( btlprm.GetFirstEf () )	//�q�b�g��̏���݂͓̂���
		{
			GenerateEffect ( pScp, btlprm );
			btlprm.SetFirstEf ( F );
		}
		else
		{
			if ( ! btlprm.GetTmr_HitStop ()->IsActive () )	//�q�b�g�X�g�b�v���͐������Ȃ�
			{
				GenerateEffect ( pScp, btlprm );
			}
		}

		//�G�t�F�N�g����
		PreScriptMove ();

		//�G�t�F�N�g����
		PostScriptMove ( btlprm.GetPos (), btlprm.GetDirRight () );
	}

	void OperateEffect::GenerateEffect ( P_Script pScp, const BtlParam & btlprm )
	{
		//����
		PVP_EfGnrt  pvpEfGnrt = pScp->GetpvpEfGnrt ();
		for ( P_EfGnrt pEfGnrt : ( *pvpEfGnrt ) )
		{
			//�����p�Ȃ�
			if ( pEfGnrt->GetGnrt () )
			{
				//�G�t�F�N�g�C���f�b�N�X�̎擾
				UINT index = pEfGnrt->GetIndex ();
				//�G�t�F�N�g�̎擾
				P_Effect pEf = m_pChara->GetpEffect ( index );
				//���X�g�ɒǉ�
				AddListEffect ( pEf, pEfGnrt, btlprm.GetPos (), btlprm.GetDirRight () );
			}
			else //�ė��p�Ȃ�
			{
				int i = 0;
#if false
				//�G�t�F�N�g�C���f�b�N�X�̎擾
				UINT index = pEfGnrt->GetIndex ();
				//�G�t�F�N�g�̎擾
				P_Effect pEf = m_pChara->GetpEffect ( index );
				//�ғ������ǂ���
				if ( !m_oprtEf.IsActive ( pEf ) )
				{
					m_oprtEf.DriveEffect ( pEf );
				}
#endif // false
			}
		}
	}

	//�G�t�F�N�g���X�g�ɐV�K�ǉ�
	void OperateEffect::AddListEffect ( P_Effect pEffect, P_EfGnrt pEfGnrt, VEC2 ptChara, bool dirRight )
	{
		P_ExEf pExeEffect = make_shared < ExeEffect > ( pEffect, m_pChara, pEfGnrt, ptChara, dirRight );
		m_plpExeEffect->push_back ( pExeEffect );
		AddpTask ( pExeEffect );	//�^�X�N���X�g
	}

	//�I�u�W�F�N�g����ExeEf���擾
	P_ExEf OperateEffect::GetpExEf ( P_Effect p ) const
	{
		//�G�t�F�N�g���s���X�g���猟��
		for ( P_ExEf pExEf : (* m_plpExeEffect) )
		{
			if ( pExEf->Compare ( p ) ) { return pExEf; }
		}
		return nullptr;
	}

	//�X�N���v�g�O����
	void OperateEffect::PreScriptMove ()
	{
		//����
		for ( auto p : (*m_plpExeEffect) ) { p->PreScriptMove (); }
	}


	//�X�N���v�g�㏈��
	void OperateEffect::PostScriptMove ( VEC2 ptChara, bool dirRight )
	{
		//list
		for ( auto p : (*m_plpExeEffect) )
		{
			p->PostScriptMove ( ptChara, dirRight );
		}

		//�I������
		LP_ExEf::iterator it = begin ( * m_plpExeEffect );
		while ( it != end ( * m_plpExeEffect ) )
		{
			//�������A��u�C���N�������g�̓R�s�[��n���C�e���[�^���󂳂Ȃ�
			if ( (*it)->IsEnd () ) 
			{ 
				EraseTask ( *it );	//�^�X�N���X�g
				(*it).reset (); 
				m_plpExeEffect->erase ( it ++ ); 
			}
			else { ++ it; }
		}
	}

	//�X�N���v�g����
	void OperateEffect::SynchroScript ( VEC2 ptChara )
	{
		//list
		for ( P_ExEf p : (*m_plpExeEffect) )
		{
			p->SynchroScript ( ptChara );
		}
	}

	//�g�\���ؑ�
	void OperateEffect::OnDispRect ()
	{
		for ( auto p : (*m_plpExeEffect) ) { p->OnDispRect (); }
	}

	void OperateEffect::OffDispRect ()
	{
		for ( auto p : (*m_plpExeEffect) ) { p->OffDispRect (); }
	}



}	//namespace GAME

