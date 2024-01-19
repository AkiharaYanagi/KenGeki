using System;


namespace ScriptEditor
{
	//@todo コントロールの管理範囲をチェック

	//=====================================================
	//
	// CtrlCompend   
	//	↓			
	// Ctrl_All(シングルトン)
	//	↓	
	//	↓			 // EditChara(シングルトン)
	//	↓				↓
	//	↓→ → UpdateData(), Assosiate() など
	//	↓				↓
	//	[------ Chara ------](編集対象キャラデータ)
	//
	//=====================================================


	//@todo	各コントロールにScripControlとしてインターフェースを継承させる
	//	↓→ → UpdateData(), Assosiate() など


#if false
	//各コントロールの統合処理
	public sealed class Ctrl_All
	{
		//---------------------------------------------------------------------
		//シングルトン実体
		public static Ctrl_All Inst { get; } = new Ctrl_All ();

		//プライベートコンストラクタ
		private Ctrl_All ()
		{
		}
		//---------------------------------------------------------------------

		//コントロール参照
		//ビヘイビア（アクション）
		private Ctrl_SqcList Ctrl_SqcList_Act { get; set; } = new Ctrl_SqcList ();
		private _Ctrl_Compend Ctrl_Cmpd_Bhv { get; set; } = new _Ctrl_Compend ();
		//ガーニッシュ（エフェクト）
		private Ctrl_SqcList Ctrl_SqcList_Efc { get; set; } = new Ctrl_SqcList ();
		private _Ctrl_Compend Ctrl_Cmpd_Gns { get; set; } = new _Ctrl_Compend ();


		public void SetCtrl ( Ctrl_SqcList act, _Ctrl_Compend ctrl_bhv, Ctrl_SqcList efc, _Ctrl_Compend ctrl_gns )
		{
			Ctrl_Cmpd_Bhv = ctrl_bhv;
			Ctrl_SqcList_Act = act;
			Ctrl_Cmpd_Gns = ctrl_gns;
			Ctrl_SqcList_Efc = efc;
		}

		public void SetCharaData ( Chara ch )
		{
			Ctrl_Cmpd_Bhv.SetCharaData ( ch );
			Ctrl_Cmpd_Gns.SetCharaData ( ch );
		}

		public void UpdateData ()
		{
			Ctrl_Cmpd_Bhv.UpdateData ();
			Ctrl_Cmpd_Gns.UpdateData ();
		}

		public void AllDisp ()
		{
			
		}

		public void AllAssosiate ()
		{
			Ctrl_Cmpd_Bhv.Assosiate ();
			Ctrl_Cmpd_Gns.Assosiate ();
		}
	}
#endif
}
