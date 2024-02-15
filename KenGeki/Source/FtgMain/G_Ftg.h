//=================================================================================================
//
//	G_Ftg �w�b�_�t�@�C��
//		Fighting���ʃO���[�o���ϐ��Q
//
//=================================================================================================
#pragma once

//-------------------------------------------------------------------------------------------------
// �w�b�_�t�@�C���̃C���N���[�h
//-------------------------------------------------------------------------------------------------
#include "Game.h"
#include "../GameMain/GameConst.h"
#include "../FtgMain/FtgConst.h"

//-------------------------------------------------------------------------------------------------
// �錾
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	class G_Ftg
	{
	//---------------------------------------------------------------------
	//�V���O���g���p�^�[��
	private:
		using  _P_G_Ftg = unique_ptr < G_Ftg >;
		static _P_G_Ftg m_inst;		//�V���O���g���C���X�^���X
		G_Ftg ();		//private �R���X�g���N�^�Œʏ�̎��̉��͋֎~
	public:
		~G_Ftg ();		//�f�X�g���N�^��unique_ptr�̂���public
		static void Create() { if ( ! m_inst ) { m_inst = _P_G_Ftg ( new G_Ftg () ); } }
		static _P_G_Ftg & inst () { return m_inst; }	//�C���X�^���X�擾
	//---------------------------------------------------------------------

	private:
		//�L�����ʒu�ɂ���ʕ␳��
		VEC2	m_posMutualBase;

		float	m_chara_center_x { 0 };

		//�����ʒu���Z�b�g�t���O
		//�����F��ʒ[�� "������" ���q�b�g��
		//���ʁF�w�i�𒆉��Ɉړ�����
		bool	m_bResetPos { F };

		//�ǈʒu
		float	m_wall_L { 0 };	//��
		float	m_wall_R { 0 };	//�E

	public:
		//��ʒ[�ł̕\����ʒu
		void CulcPosMutualBase ( VEC2 pos1p, VEC2 pos2p );
//		void SetPosMutualBase ( VEC2 v ) { m_posMutualBase = v; }
		VEC2 GetPosMutualBase () const { return m_posMutualBase; }

		float GetCharaCenterX () const { return m_chara_center_x; }

		//�����ʒu���Z�b�g�t���O
		bool GetResetPos () const { return m_bResetPos; }
		void SetResetPos ( bool b ) { m_bResetPos = b; }

		//�ǈʒu
		float GetWallLeft () const { return m_wall_L; }
		float GetWallRight () const { return m_wall_R; }
	};

	using  UP_G_Ftg = unique_ptr < G_Ftg >;


#define G_FTG	G_Ftg::inst()
#define G_BASE_POS	G_Ftg::inst()->GetPosMutualBase


}	//namespace GAME


