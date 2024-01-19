namespace ScriptEditor
{
	//==================================================================================
	//	Compendを受けて表示する
	//		Behavior, Garcnishに派生する
	//==================================================================================
	public class DispCompend
	{
		//編集の参照
		public EditCompend EditCompend { get; set; } = null;

		//シークエンス表示の参照
		public DispSequence DispSequence { get; set; } = null;	//継承先で実体を持つ

		//スクリプトの表示 (表示されるスクリプトは常に１つなのでここで実体を持つ) 
//		public DispScript DispScript { get; set; } = new DispScript ();

		//コントロール：コンペンドの参照
		public _Ctrl_Compend CtrlCmpd { get; set; } = null;

		//シークエンスボードの参照
//		public SqcBoard SqcBoard { get; set; } = null;

		//コントロールの設定
		public virtual void SetCtrl ( EditCompend ec, _Ctrl_Compend cc )
		{
			EditCompend = ec;
//			DispScript.Set ( cc );
			CtrlCmpd = cc;
//			SqcBoard = cc.sqcBoard1;
		}

		//データの設定
		public void SetCharaData ( Chara ch )
		{
			DispSequence.SetCharaData ( ch );
//			DispScript.SetCharaData ( ch );
		}

		//更新
		public virtual void UpdateData ()
		{
			CtrlCmpd.UpdateData ();
//			DispScript.UpdateData ( EditCompend.SelectedScript );

			Disp ();
		}

		//表示
		public void Disp ()
		{
			CtrlCmpd.Invalidate ();
//			SqcBoard.Invalidate ();
			
			DispSequence.Disp ( EditCompend.SelectedSequence );		//シークエンス
//			DispScript.Disp ( EditCompend.SelectedScript );	//スクリプト
		}

		//アクションカテゴリの変更
		public void UpdateActionCategory ()
		{
			CtrlCmpd.Invalidate ();
		}
	}

	//==================================================================================

	//Behavior	ビヘイビア	(アクションの集合)
	public class DispBehavior : DispCompend
	{
		public DispAction DispAction { get; set; } = new DispAction ();

		public DispBehavior ()
		{
			//継承元に設定する
			base.DispSequence = DispAction;
		}

		public override void SetCtrl ( EditCompend ec, _Ctrl_Compend cc )
		{
			DispAction.SetCtrl ( cc );
			base.SetCtrl ( ec, cc );
		}

		public override void UpdateData ()
		{
//			DispAction.UpdateData ( (Action)EditCompend.SelectedSequence );
			base.UpdateData ();
		}
	}

	//Garnish	ガーニッシュ	(エフェクトの集合)
	public class DispGarnish : DispCompend
	{
		public DispEffect DispEffect { get; set; } = new DispEffect ();

		public DispGarnish ()
		{
			//継承元に設定する
			base.DispSequence = DispEffect;
		}

		public override void SetCtrl ( EditCompend ec, _Ctrl_Compend cc )
		{
			DispEffect.Load ( cc );
			base.SetCtrl ( ec, cc );
		}
	}

}

