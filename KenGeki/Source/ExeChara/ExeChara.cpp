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
	//�R���X�g���N�^
	ExeChara::ExeChara ( PLAYER_ID m_playerID )
		: m_pChara ( nullptr )
		, m_playerID ( m_playerID ), m_name ( CHARA_TEST ), m_playerMode ( MODE_PLAYER )
		, m_actionID ( 0 ), m_frame ( 0 )
	{
		//�L�����f�[�^����
		m_pChara = make_shared < Chara > ();	//�L�����f�[�^����
		m_charaRect = make_shared < CharaRect > ();	//�����g
		m_btlPrm.SetPlayerID ( m_playerID );	//�o�g���p�����[�^

		//�\��
		m_dispChara = make_shared < DispChara > ();
		m_dispChara->LoadPlayer ( m_playerID );	//�\��(1P/2P���ɂ��ʒu)
		AddpTask ( m_dispChara );

		//�G�t�F�N�g�ė�
		m_oprtEf = make_shared < OperateEffect > ();
		AddpTask ( m_oprtEf );
	}

	//�f�X�g���N�^
	ExeChara::~ExeChara ()
	{
	}

	//------------------------
	//�V�[���p�����[�^�֘A������
	void ExeChara::ParamInit ( P_Param pParam )
	{
		//�Q�[���ݒ�
		GameSettingFile stg = pParam->GetGameSetting ();

		//�I���L�������O�E���[�h���擾
		m_name = stg.GetName ( m_playerID );
		m_playerMode = stg.GetPlayerMode ( m_playerID );

		//����
#if 0
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
#endif // 0
		LoadInput ();

		//�\��
		m_dispChara->ParamInit ( pParam );
	}

	//------------------------
	//�Ǎ�
	void ExeChara::Load ()
	{
		//--------------------------------------------
		//m_pChara�̃f�[�^�Ǎ�
		LoadCharaData ();

		//--------------------------------------------
		//�L�����\��������
		m_dispChara->SetpChara ( m_pChara );
		m_dispChara->SetpCharaRect ( m_charaRect );

		//�A�N�^�E�X�e�[�g�ɗp�����ԃp�����[�^�ɓo�^
		m_actor.SetwpExeChara ( shared_from_this () );

		//�o�g���p�����[�^�ɐݒ�
		m_btlPrm.SetpChara ( m_pChara, m_pOther );

		//�G�t�F�N�g�����x�N�^�̐���
		m_oprtEf->MakeEfList ( m_pChara );

		TASK_VEC::Load ();
	}

	//------------------------
	//������(��������s�\)
	void ExeChara::Init ()
	{
		//�A�N�V�����E�X�N���v�g������
		m_actionID = 0;
		m_pAction = m_pChara->GetpAction ( m_actionID );
		m_frame = 0;
		m_pScript = m_pAction->GetpScript ( m_frame );

		//�o�g���p�����[�^
		m_btlPrm.Init ();
		SetParamFromScript ();	//�X�N���v�g����p�����[�^�𔽉f����

		//�g
		m_charaRect->ResetAllRect ();

		//���
		m_actor.Init ();

		//�\��
		//@info Move()����Transit()�̌�ɑJ�ڂ��A
		//		�ēxMove()�͌Ă΂ꂸDraw()���Ă΂�邽�߁A�����Ŏ蓮�̏��������K�v(Init()�͌Ă΂��)
#if 0
		m_dispChara->UpdateMainImage ( m_pScript, GetPos (), GetDirRight () );
		//���͕\���X�V
		m_dispChara->UpdateInput ( m_pCharaInput );
#endif // 0
		m_dispChara->Update ( m_pAction, m_pScript, m_btlPrm, m_pCharaInput );

		TASK_VEC::Init ();
	}

	//�Đݒ�
	void ExeChara::Reset ()
	{
		_Reset ();	//������

		TASK_VEC::Reset ();
	}

	//------------------------
	//�������̍Đݒ�
	void ExeChara::_Reset ()
	{
		//�e�N�X�`���������m�ۊ֘A�͍Đݒ�
		Rele ();
		m_pChara = make_shared < Chara > ();
		Load ();
		m_dispChara->SetpChara ( m_pChara );
		m_oprtEf->MakeEfList ( m_pChara );

		//�A�N�V�����E�X�N���v�g�Ď擾
		m_pAction = m_pChara->GetpAction ( m_actionID );
		m_pScript = m_pAction->GetpScript ( m_frame );

		//�\���̍X�V
#if 0
		//���C�� �C���[�W
		m_dispChara->UpdateMainImage ( m_pScript, GetPos (), GetDirRight () );
#endif // 0
		m_dispChara->Update ( m_pAction, m_pScript, m_btlPrm, m_pCharaInput );

		//�G�t�F�N�g �C���[�W
		m_oprtEf->PostScriptMove ( GetPos (), GetDirRight () );
	}

	//===========================================================
	//***********************************************************
	//	���t���[��MutualChara����Ă΂���ȃX�N���v�g�����֐��̈�A
	//***********************************************************
	//	�� ��������
	//	void ExeChara::PreScriptMove ();	//	�X�N���v�g�O����
	//	//void MutualChara::Collision ();	//	���ݔ��� (�ڐG�g)
	//	void ExeChara::RectMove ();			//	�Ԃ����A����g��ݒ�
	//	//void MutualChara::Decision ();	//	���ݔ��� (�U���g�A�q�b�g�g)
	//	void ExeChara::PostScriptMove ();	//	�X�N���v�g�㏈��
	//===========================================================

	//�e�����ŉ����s���A�����΂����̓A�N�^���w�肷��
	
	//��	���t���[�� �X�N���v�g�O����
	void ExeChara::PreScriptMove () { m_actor.PreScriptMove (); }

	//��	���҂̐ڐG�����ɍU���E���E�E���蔻��g��ݒ�
	void ExeChara::RectMove () { m_actor.RectMove (); }

	//��	���t���[�� �X�N���v�g�㏈��
	void ExeChara::PostScriptMove () { m_actor.PostScriptMove (); }


	//===========================================================
	//***********************************************************
	//	State����Ă΂���ԕʏ���
	//***********************************************************
	//	void Input ();			//���͏���
	//	void TransitAction ();	//�A�N�V�����ڍ�
	//	void CalcPos ();		// �ʒu�v�Z		//�Ԃ����A�ʒu�̏C��
	//	void CheckLife ();		//���C�t����
	//	void UpdateGraphic ();	//�O���t�B�b�N�X�V
	//	void EffectMove ();		//�G�t�F�N�g����
	//	void MoveTimer () { m_btlPrm.TimerMove (); }		//�^�C�}�ғ�
	//===========================================================

	//================================================
	//���͏���
	void ExeChara::Input ()
	{
		//���͂̍X�V
		m_pCharaInput->Update ( GetDirRight () );
	}

	//================================================
	// �A�N�V�����ڍ�(����:�R�}���h, �A�N�V�����I��)
	void ExeChara::TransitAction ()
	{
		assert ( nullptr != m_pAction && nullptr != m_pScript );

		// �̂����莞�Ԃ̓R�}���h�ɂ��s���s��
#if 0
		if ( LurchTimer () ) { return; }
#endif // 0

		//-----------------------------------------------------
		// �R�}���h�ɂ�镪��
		if ( TranditAction_Command () )
		{
			//���򂪐������Ă�����ȍ~�̓`�F�b�N���Ȃ�
			return;
		}

		//-----------------------------------------------------
		//���݃X�N���v�g�����݃A�N�V�����ɂ�����ŏI�t���[�� �Ȃ��
		if ( m_pAction->IsOverScript ( m_frame ) )
		{
			//�A�N�V�����I������
			EndAction ();

			// �X�N���v�g�ʏ폈��
			ExeScript ();

			//�I��
			return;
		}

		//-----------------------------------------------------
		// �X�N���v�g�ʏ폈��
		ExeScript ();

		//�ʏ폈���F�X�N���v�g���P�i�߂�
		++ m_frame;
	}

	//================================================
	// �ʒu�v�Z
	void ExeChara::CalcPos ()
	{
		m_btlPrm.CalcBalance ( m_pScript );	//�o�����X����
		m_btlPrm.CalcPos ( m_pScript );		//�ʒu�v�Z
		Landing ();	//���n
	}

	//================================================
	//�G�t�F�N�g
	void ExeChara::EffectMove ()
	{
		m_oprtEf->MoveEffect ( m_pScript, m_btlPrm );
	}

	//================================================
	//���C�t����
	void ExeChara::CheckLife ()
	{
		//���������C�t�O
		if ( 0 >= m_btlPrm.GetLife () )
		{
			//test
#if 0
			//�_�E����Ԃɋ����ύX
			SetAction ( ACT_DOWN );
			m_btlPrm.GetTmr_Down ()->Start ();
#endif // 0
			m_btlPrm.SetLife ( 10000 );
		}
	}

	//====================================================================================
	//�O���t�B�b�N�X�V
	void ExeChara::UpdateGraphic ()
	{
		m_dispChara->Update ( m_pAction, m_pScript, m_btlPrm, m_pCharaInput );
	}


	//================================================
	//	�O������̏��(State)�ύX
	//================================================


	//================================================
	//	�����֐�
	//================================================

	//�A�N�V�����ڍ��i�R�}���h�Ɋւ��鏈���j
	bool ExeChara::TranditAction_Command ()
	{
		//-----------------------------------------------------
		// �R�}���h�ɂ�镪��
		//�R�}���h����������ID��D�揇�ɕۑ��������X�g
		m_pCharaInput->MakeTransitIDList ( *m_pChara, m_pScript, m_btlPrm.GetDirRight () );
		const std::vector < UINT > & vCompID = m_pCharaInput->GetvCompID ();

		UINT transitID = NO_COMPLETE;
		for ( UINT id : vCompID )
		{
			//�J�ڐ�`�F�b�N
			P_Action pact = m_pChara->GetpAction ( id );

			//�Ώ�ID���o�����X����ňڍ��\�Ȃ�ړ�������


			//�s�\�Ȃ玟���`�F�b�N
			transitID = id;
			break;
		}

		//�R�}���h���������Ă�����
		if ( NO_COMPLETE != transitID )
		{
			//���K�@���菈��
			if ( transitID == m_pChara->GetActionID ( _T ( "OD0_L" ) ) )
			{
				//�}�i����
				if ( m_btlPrm.GetMana () >= MANA_HALF )
				{
					m_btlPrm.AddMana ( - MANA_MAX / 2 );
				}
				else //����Ȃ��Ƃ��J�ڂ��Ȃ�
				{
					return F;
				}
			}

			//���݃A�N�V�����Ƃ��čŌ�̏���
			EndAction ();

			//�J�ڐ�`�F�b�N
			P_Action pact = m_pChara->GetpAction ( transitID );
			P_Script pscr = pact->GetpScript ( 0 );


			//�o�����X����
			int bl_a = pact->GetBalance ();		//�A�N�V��������o�����X
			int bl_p = m_btlPrm.GetBalance ();	//�p�����[�^���݃o�����X

			int d = bl_p - bl_a;
			if ( d < 0 ) { d = 0; }
			m_btlPrm.SetBalance ( d );


			//�A�N�V�����J��
			m_actionID = transitID;

			//�X�N���v�g����
			ExeScript ();

			//�A�N�V�����ڍ����v��
			m_btlPrm.AddNActTrs ( 1 );

			//���t���[���̃X�N���v�g���P�i�߂�
			//����t���[���͎擾�ς݂�m_pScript��p����
			++ m_frame;

			//�I��
			return T;
		}

		return F;
	}


	//�A�N�V�����̈ڍ�(���ڎw��)
	void ExeChara::SetAction ( UINT action_id )
	{
		m_actionID = action_id;
		m_pAction = m_pChara->GetpAction ( m_actionID );
		m_frame = 0;
		m_pScript = m_pAction->GetpScript ( m_frame );

		//�X�N���v�g����̃p�����[�^���f
		SetParamFromScript ();
	}

	void ExeChara::SetAction ( tstring action_name )
	{
		SetAction ( m_pChara->GetActionID ( action_name ) );
	}


	//�A�N�V�����ڍs(���g)
	void  ExeChara::TransitAction_Condition_I ( BRANCH_CONDITION CONDITION, bool forced )
	{
		//�q�b�g�E����
		UINT indexAction = Check_TransitAction_Condition ( CONDITION );
		if ( NO_COMPLETE != indexAction )
		{
			//�J�ڐ�`�F�b�N
			P_Action pAct = m_pChara->GetpAction ( indexAction );
			P_Script pScr = pAct->GetpScript ( 0 );

			//���g��ύX
			SetAction ( indexAction );	//�J��
			m_btlPrm.SetForcedChange ( forced );
		}
	}

	//�A�N�V�����ڍs(����)
	void  ExeChara::TransitAction_Condition_E ( BRANCH_CONDITION CONDITION, bool forced )
	{
		//�q�b�g�E����
		UINT indexAction = Check_TransitAction_Condition ( CONDITION );
		if ( NO_COMPLETE != indexAction )
		{
			//�J�ڐ�`�F�b�N
			P_Action pAct = m_pChara->GetpAction ( indexAction );

			//�����Ԃ̂Ƃ��󒆃`�F�b�N
			bool bDai = m_pChara->GetActionID ( _T("�_���[�W��") ) == indexAction ;
			bool bSyou = m_pChara->GetActionID ( _T("�_���[�W��") ) == indexAction ;

			if ( bDai || bSyou )
			{
				ACTION_POSTURE ap = m_pOther.lock()->GetPosture ();
				if ( ap == ACTION_POSTURE::AP_JUMP )
				{
					indexAction = m_pChara->GetActionID ( _T("�󒆂���") );
				}
			}

			//�X�N���v�g
			P_Script pScr = pAct->GetpScript ( 0 );

			//@todo �̂����莞�Ԃ��w�肵�Ă���ꍇ�A����ɓK�p
			if ( m_pScript->m_prmBattle.Warp != 0 )
			{
			}

			//�����ύX
			m_pOther.lock ()->SetAction ( indexAction );	//�J��
			m_pOther.lock ()->m_btlPrm.SetForcedChange ( forced );
		}
	}

	//�A�N�V�����ڍs(�����`�F�b�N)
	UINT ExeChara::Check_TransitAction_Condition ( BRANCH_CONDITION BRC_CND )
	{
		//�L�����̎����[�g,�u�����`,�R�}���h�̎Q��
		const VP_Route& vpRoute = m_pChara->GetvpRoute ();
		const VP_Branch& vpBranch = m_pChara->GetvpBranch ();

		//�X�N���v�g�̎����[�g���X�g
		for ( UINT indexRut : m_pScript->GetcvRouteID () )
		{
			const V_UINT& vBrcID = vpRoute [ indexRut ]->GetcvIDBranch ();

			//�Ώۂ̃u�����`���X�g
			for ( UINT id : vBrcID )
			{
				//��������
				if ( BRC_CND != vpBranch [ id ]->GetCondition () ) { continue; }

				return vpBranch [ id ]->GetIndexSequence ();
			}
		}
		return NO_COMPLETE;
	}


	//==================================================
	//�X�N���v�g�ʏ폈��
	//==================================================
	void ExeChara::ExeScript ()
	{
		//------------------------------------------------
		//���̃t���[���ŃX�N���v�g���������邽�߁A�ڍs��A�N�V�����ƃX�N���v�g��ۑ�
		//����̃t���[������m_pAction��m_pScript��p���A
		//����ȍ~��m_actionID��m_frame��p���Ȃ�
		m_pAction = m_pChara->GetpAction ( m_actionID );
		m_pScript = m_pAction->GetpScript ( m_frame );

		//------------------------------------------------
		//	����A�N�V�����w��
		SpecialAction ();

		//------------------------------------------------
		//�X�N���v�g����̃p�����[�^���f
		SetParamFromScript ();
	}

	//	����A�N�V�����w�菈��
	void ExeChara::SpecialAction ()
	{
		//-----------------------------------------------------
		if ( IsNameAction ( _T ( "����" ) ) )
		{
			//�A���q�b�g���̃��Z�b�g
			m_pOther.lock()->m_btlPrm.SetChainHitNum ( 0 );
			if ( m_frame == 0 )
			{
			}

			//Test�@�o�����X�C��
			int b = m_btlPrm.GetBalance ();
			{
				if ( b < 1000 ) 
				{
					m_btlPrm.SetBalance ( 5000 );
				}
			}
		}

		//-----------------------------------------------------
		//�_�b�V���J�n
		if ( IsNameAction ( _T ( "FrontDashStart" ) ) )
		{
			if ( m_frame == 0 )
			{
				//SE
				SOUND->Play_SE ( SE_Btl_Dash );
			}
		}

		//-----------------------------------------------------
		//�W�����v
		if ( IsNameAction ( _T ( "FrontJump" ) )
			|| IsNameAction ( _T ( "VerticalJump" ) )
			|| IsNameAction ( _T ( "BackJump" ) )
			)
		{
			if ( m_frame == 0 )
			{
				//SE
				SOUND->Play_SE ( SE_Btl_Jump );
			}
		}

		//-----------------------------------------------------
		//�������ǌ��I��
		if ( IsNameAction ( _T ( "�������ǌ��q�b�g" ) ) )
		{
			if ( m_frame == 0 )
			{
				//				G_FTG->SetResetPos ( T );
			}
		}

		//-----------------------------------------------------
		//�o�����X�A�E�g
		if ( m_btlPrm.GetBalance () <= 0 )
		{
			//			SetAction ( _T ( "Dotty" ) );
			SetAction ( _T ( "����" ) );
		}
	}

	//�X�N���v�g����p�����[�^�𔽉f����
	void ExeChara::SetParamFromScript ()
	{
		//------------------------------------------------
		//�Ó]
		m_btlPrm.SetBlackOut ( m_pScript->m_prmStaging.BlackOut );

		//------------------------------------------------
		//�X�N���v�g��~
		if ( ! m_btlPrm.GetTmr_Stop ()->IsActive () )
		{
			//�X�N���v�g����̒�~
			m_btlPrm.SetScpStop ( m_pScript->m_prmStaging.Stop );
		}
	}

	//====================================================================================

	//�����E���n
	void ExeChara::Landing ()
	{
		//�ʒu�����艺��������
		VEC2 pos = m_btlPrm.GetPos ();
		
		if ( PLAYER_BASE_Y < pos.y )
		{
			//���n�����̃u�����`���擾
			UINT indexAction = Check_TransitAction_Condition ( BRANCH_CONDITION::BRC_GRD );
			if ( NO_COMPLETE != indexAction )
			{
				//y�ʒu���Z�b�g
				float x = pos.x;
				float y = PLAYER_BASE_Y;
				m_btlPrm.SetPos ( VEC2 ( x, y ) );
				m_btlPrm.SetVg ( 0 );
				m_btlPrm.SetG ( 0 );

				//�J�ڐ�`�F�b�N
				P_Action pAct = m_pChara->GetpAction ( indexAction );
				P_Script pScr = pAct->GetpScript ( 0 );

				//���g��ύX
				SetAction ( indexAction );	//�J��
			}
		}

		//�ʒu�������ŗ�����Ԃ�������
		if (PLAYER_BASE_Y > pos.y)
		{
			if ( IsNameAction ( _T ( "Drop" ) ) )
			{
				//����
				m_btlPrm.SetG ( 5 );
			}
		}

		//�ʒu�����艺�ŗ�����Ԃ�������
		if ( pos.y < PLAYER_BASE_Y )
		{
			if ( IsNameAction ( _T ( "����" ) ) )
			{
				//y�ʒu���Z�b�g
				float x = pos.x;
				float y = PLAYER_BASE_Y;
				m_btlPrm.SetPos ( VEC2 ( x, y ) );
				m_btlPrm.SetVg ( 0 );
				m_btlPrm.SetG ( 0 );

			}
		}

#if 0
		if ( PLAYER_BASE_Y < pos.y )
		{
			//�f�����͉������Ȃ�
			if ( IsActCtg ( AC_DEMO ) )
			{
				return;
			}

			float x = pos.x;
			float y = PLAYER_BASE_Y;
			m_btlPrm.SetPos ( VEC2 ( x, y ) );
			m_btlPrm.SetVg ( 0 );
			m_btlPrm.SetG ( 0 );

			//SE
			SOUND->Play_SE ( SE_Btl_Landing );

			//�����A�N�V����m_pAction�͎��t���[������Move()��m_actionID���g���Ď擾�����
			m_actionID = 0;	//stand�̎w��
			m_frame = 0;
		}

		//����
		if ( IsStand () && PLAYER_BASE_Y > m_btlPrm.GetPos ().y )
		{
			m_btlPrm.SetG ( 5 );
		}
#endif // 0
	}


	void ExeChara::BackMoveX ()
	{
		//�����ɂ�炸�A���肩�痣������
		VEC2 iPos = m_btlPrm.GetPos ();
		VEC2 ePos = m_pOther.lock ()->GetPos ();
		bool dirBack = true;

		//���l�̏ꍇ��1P2P�őI��
		if ( iPos.x == ePos.x )
		{
			dirBack = ( m_playerID == PLAYER_ID_1 );
		}
		else
		{
			//�݂��̈ʒu�ŕ␳����������
			dirBack = ( iPos.x < ePos.x );
		}

		float x = iPos.x + 10 * ( dirBack ? -1.f : 1.f );
		float y = iPos.y;
		m_btlPrm.SetPos ( VEC2 ( x, y ) );
	}

	void ExeChara::LookOther ()
	{
#if 0
		//�W�����v��(�A�N�V�����̐�/�󒆏��)�͎���
		if ( Is_AP_Jump () )
		{
			return;
		}
#endif // 0
		//�j���[�g�����̂ݐU��Ԃ�
		if ( ! IsStand () )
		{
			return;
		}

		//�ʒux�������ꍇ�͎���
		VEC2 iPos = m_btlPrm.GetPos ();
		VEC2 ePos = m_pOther.lock ()->GetPos ();
		if ( iPos.x == ePos.x )
		{
			return;
		}

		//�݂��̈ʒu�ŕ���������
		m_btlPrm.SetDirRight ( iPos.x < ePos.x );
	}

	//EfPart�d�Ȃ�
	void ExeChara::OverEfPart ()
	{
		//1p2p�����d�Ȃ�͗������擾

		//����g���擾
		PV_RECT pvHRect = GetpCharaRect ()->GetpvHRect ();

		//EF���̓_�W�����擾
		std::vector < PrmEfPart > & vPrm = m_efPart->Getrv_Prm ();

		//�d�Ȃ蔻��
		UINT count = 0;
		UINT i = 0;
		for ( PrmEfPart prm : vPrm )
		{
			if ( 0 != prm.m_count ) { continue; }

			if ( prm.m_pOb->GetValid () )
			{
				for ( const RECT & rect : ( *pvHRect ) )
				{
					//�d�Ȃ��Ă����Ƃ�
					if ( OverlapPoint ( prm.m_pos, rect ) )
					{
						vPrm [ i ].m_gotten = T;
						++ count;
						break;	//RECT�̃��[�v�𔲂��AEfPart�̓_�𑱍s
					}
				}
			}

			++ i;
		}

		m_btlPrm.AddMana ( 100 * count );
	}


	//-------------------------------------------------------------------------------------------------

	//�A�N�V�����ڍ����A�O�A�N�V�����̍Ō�̏���
	void ExeChara::EndAction ()
	{
		//���A�N�V�������w��
		//�����A�N�V����m_pAction�͎��t���[������Move()��m_actionID���g���Ď擾�����
		m_actionID = m_pAction->GetNextID ();

		//------------------------------------------------
		//���ڍ����菈��
		//�_�b�V�����玟�A�N�V�����Ɉڍ�����Ƃ��A�������c��
		if ( m_pChara->GetActionID ( _T ( "FrontDashStart" ) ) == m_actionID )
		{
			m_btlPrm.SetDashInertial ( VEC2 ( 10.f, 0 ) );
		}
		if ( m_pChara->GetActionID ( _T ( "FrontDash" ) ) == m_actionID )
		{
			m_btlPrm.SetDashInertial ( VEC2 ( 10.f, 0 ) );
		}
		if ( m_pChara->GetActionID ( _T ( "BackDash" ) ) == m_actionID )
		{
			m_btlPrm.SetDashInertial ( VEC2 ( -8.f, 0 ) );
		}
		//------------------------------------------------

		//�e���Ԃ̏I��
		m_btlPrm.EndAction ();
		m_frame = 0;
	}

	//�̂�����^�C�}�[
#if 0
	bool ExeChara::LurchTimer ()
	{
		// �̂����莞�Ԃ̓R�}���h�ɂ��s���s��
		if ( 0 < m_lurch )
		{
			if ( m_lurchTimer == m_lurch )	//�̂�����I��
			{
				m_lurch = 0;
				m_lurchTimer = 0;
			}
			else
			{
				++m_lurchTimer;
				return true;
			}
		}
		return false;
	}
#endif // 0


	//-------------------------------------------------------------------------------------------------
	//�g���[�j���O���[�h������
	void ExeChara::TrainingInit ()
	{
		Init ();
		m_btlPrm.TrainingInit ();
	}

	//CPU����ؑ�
	void ExeChara::ControlCPU ()
	{
		m_pCharaInput = m_pCPUInput;
		m_dispChara->SetControl_CPU ();
	}

	void ExeChara::ControlPlayer ()
	{
		m_pCharaInput = m_pPlayerInput;
		m_dispChara->SetControl_PLAYER ();
	}

}	//namespace GAME

