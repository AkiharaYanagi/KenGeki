//=================================================================================================
//
//	EfClang �w�b�_�t�@�C��
//
//=================================================================================================
#pragma once

//-------------------------------------------------------------------------------------------------
// �w�b�_�t�@�C���̃C���N���[�h
//-------------------------------------------------------------------------------------------------
#include "Game.h"
#include "../../GameMain/GameConst.h"
#include "../../FtgMain/FtgConst.h"
#include "../G_Ftg.h"

//-------------------------------------------------------------------------------------------------
// �錾
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	class EfClang : public GrpEf
	{
	public:
		EfClang ();
		EfClang ( const EfClang & rhs ) = delete;
		~EfClang ();

		void Move ();

		void On ( VEC2 center );
	};

	using P_EfClang = std::shared_ptr < EfClang >;


}	//namespace GAME

