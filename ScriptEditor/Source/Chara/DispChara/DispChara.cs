namespace ScriptEditor
{

	//@info Disp系をすべて廃止
	//	->	更新や関連付けなどの機能のみを
	//		ActionやFuncのデリゲートで呼び出す。

	// CtrlCompend   
	//	↓			
	// Ctrl_All(シングルトン)
	//	↓	
	//	↓			 // EditChara(シングルトン)
	//	↓				↓
	//	↓→ → AllDisp(), Assosiate() など
	//	↓				↓
	//	[------ Chara ------](編集対象キャラデータ)



	//==================================================================================
	//	キャラの表示
	//		アクションの集合ビヘイビア
	//		エフェクトの集合ガーニッシュを保持する
	//		シングルトンとしてグローバルからアクセス可能
	//	各所から表示の更新(Update)と描画(Disp)を行う
	//==================================================================================

	//	DispChara(シングルトン)
	//	↓				↓
	// CtrlCompend →  // EditChara(シングルトン)
	//	↓				↓
	//	[------ Chara ------]

	public sealed class DispChara
	{
		//---------------------------------------------------------------------
		//シングルトン実体
		public static DispChara Inst { get; } = new DispChara ();

		//プライベートコンストラクタ
		private DispChara ()
		{
			DispBehavior = new DispBehavior ();
			DispGarnish = new DispGarnish ();
		}
		//---------------------------------------------------------------------

		//コンペンドの表示 
		public DispBehavior DispBehavior { get; set; } = null;	//アクションの集合
		public DispGarnish DispGarnish { get; set; } = null;	//エフェクトの集合

		//読込時、コントールにキャラのデータを設定する
		public void SetCharaData ( Chara ch )
		{
			DispBehavior.SetCharaData ( ch );
			DispGarnish.SetCharaData ( ch );
		}

		//更新
		public void UpdateData ()
		{
			DispBehavior.UpdateData ();
			DispGarnish.UpdateData ();
		}

		//すべて描画
		public void Disp ()
		{
			DispBehavior.Disp ();
			DispGarnish.Disp ();
		}
	
	}


}
