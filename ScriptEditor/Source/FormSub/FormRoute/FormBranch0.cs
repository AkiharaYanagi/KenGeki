using System;

namespace ScriptEditor
{
	//=================================================
	//	フォーム・ブランチ
	//=================================================
	public sealed partial class FormBranch0 :EditorForm
	{
		//---------------------------------------------------------------------
		//シングルトン実体
		public static FormBranch0 Inst { get; } = new FormBranch0 ();

		//プライベートコンストラクタ
		private FormBranch0 ()
		{
			this.InitPt = new System.Drawing.Point ( 0, 200 );
			InitializeComponent ();
		}

		//---------------------------------------------------------------------

		//---------------------------------------------------------------------

		//データ設定
		public void SetCharaData ( Chara ch )
		{
			ctrl_Branch1.SetCharaData ( ch );
		}

		//関連付け
		public void Assosiate ( Script scp )
		{

		}
	}
}
