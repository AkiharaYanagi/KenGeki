//=================================================================================================
//
// ƒGƒOƒ[ƒLƒƒƒ‰@ƒ\[ƒXƒtƒ@ƒCƒ‹
//
//=================================================================================================

//-------------------------------------------------------------------------------------------------
// ƒwƒbƒ_ƒtƒ@ƒCƒ‹‚ÌƒCƒ“ƒNƒ‹[ƒh
//-------------------------------------------------------------------------------------------------
#include "ExeChara.h"


//-------------------------------------------------------------------------------------------------
// ’è‹`
//-------------------------------------------------------------------------------------------------
namespace GAME
{
	//================================================
	//	”»’è˜gŠÖ˜A “à•”ŠÖ”
	//================================================

	//ÚG˜gÝ’è
	void ExeChara::SetCollisionRect ()
	{
		m_charaRect->SetCRect ( m_pScript->GetpvCRect (), m_btlPrm.GetDirRight (), m_btlPrm.GetPos () );
	}

	//‘ŠŽEEUŒ‚E“–‚èE˜gÝ’è
	void ExeChara::SetRect ()
	{
		SetOffsetRect ();
		SetAttackRect ();
		SetHitRect ();
	}

	//‘ŠŽE˜gÝ’è
	void ExeChara::SetOffsetRect ()
	{
		m_charaRect->SetORect ( m_pScript->GetpvORect (), m_btlPrm.GetDirRight (), m_btlPrm.GetPos () );
	}

	//UŒ‚˜gÝ’è
	void ExeChara::SetAttackRect ()
	{
		//======================================
		// ƒqƒbƒgŽž‚ÉŒã‚ÌUŒ‚˜g‚ðˆêŽž’âŽ~(‘½’i–hŽ~)
		//======================================

		//ƒqƒbƒgó‘Ô‚ðŠm”F (ƒqƒbƒgó‘ÔÝ’è‚Í OnHit(), OnClang() )
		bool bHit = m_btlPrm.GetHitEst () || m_btlPrm.GetClang ();

		//ƒfƒoƒbƒO•\Ž¦‚ ‚è
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
		//ƒfƒoƒbƒO•\Ž¦‚È‚µ
#endif // DEBUG_DISP

		//--------------------------------------------------
		//ƒqƒbƒg‚µ‚Ä‚¢‚È‚¢Žž(’ÊíŽž)AUŒ‚˜g‚ðÝ’è‚µ‚ÄI—¹
		if ( ! bHit )
		{
			//UŒ‚˜g‚ÌÝ’è
			m_charaRect->SetARect ( m_pScript->GetpvARect (), m_btlPrm.GetDirRight (), m_btlPrm.GetPos () );
			return;
		}

		//--------------------------------------------------
		//UŒ‚¬—§ŽžE‘Å‡Žž‚É“¯ˆêƒAƒNƒVƒ‡ƒ“’†‚Ì‚Ý˜g‚ðÁŽ¸‚³‚¹‚é

		//1ƒqƒbƒg‹Z‚ÍUŒ‚˜g‚ð‹ó‚É‚µ‚ÄI—¹
		if ( 1 >= m_pAction->GetHitNum () )
		{
			//UŒ‚˜g‚ð‹ó‚É‚·‚é
			m_charaRect->ResetARect ();
			return;
		}

		//--------------------------------------------------
		//ƒAƒNƒVƒ‡ƒ““àƒqƒbƒg”‚ªãŒÀ‚É’B‚µ‚½‚Æ‚«˜g‚ð‹ó‚É‚·‚é

		UINT hitmax = m_pAction->GetHitNum ();
		if ( hitmax <= m_btlPrm.GetHitNum () )
		{
			//UŒ‚˜g‚ð‹ó‚É‚·‚é
			m_charaRect->ResetARect ();
			return;
		}

		//--------------------------------------------------
		//‘½’i‰Â”\‚ÈƒAƒNƒVƒ‡ƒ“‚ÍAƒqƒbƒgŠÔŠu‚ðƒ`ƒFƒbƒN‚µ‚ÄUŒ‚˜g‚ðÄÝ’è‚·‚é
		P_Timer ptHitPitch = m_btlPrm.GetTmr_HitPitch ();
		UINT pitch = m_pAction->GetHitPitch ();
		if ( pitch < ptHitPitch->GetTime () )
		{
			//ÄƒXƒ^[ƒg
			m_btlPrm.HitPitchWaitStart ();

			//UŒ‚˜g‚ÌÝ’è
			m_charaRect->SetARect ( m_pScript->GetpvARect (), m_btlPrm.GetDirRight (), m_btlPrm.GetPos () );
			return;
		}

		//--------------------------------------------------
		//ŠY“–‚µ‚È‚¯‚ê‚ÎUŒ‚˜g‚ð‹ó‚É‚·‚é
		m_charaRect->ResetARect ();
	}

	//“–‚è˜gÝ’è
	void ExeChara::SetHitRect ()
	{
		m_charaRect->SetHRect ( m_pScript->GetpvHRect (), m_btlPrm.GetDirRight (), m_btlPrm.GetPos () );
	}

	//˜g•\Ž¦Ø‘Ö
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

	//˜g‚ð‚·‚×‚ÄƒŠƒZƒbƒg
	void ExeChara::ResetRect ()
	{
		m_charaRect->ResetAllRect ();
	}

}	//namespace GAME

