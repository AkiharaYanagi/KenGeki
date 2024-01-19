using System;

namespace ScriptEditor
{
	//-------------------------------------------------------------
	//	アクション内容 設定フォーム
	//-------------------------------------------------------------

	public sealed partial class FormAction : SubForm_Compend
	{
		//---------------------------------------------------------------------
		//シングルトン実体
		public static FormAction Inst { get; } = new FormAction ();

		//プライベートコンストラクタ
		private FormAction ()
		{
			InitializeComponent ();
			base.LoadObject ();

			//定数：アクション属性コンボボックス
			foreach ( ActionCategory ac in Enum.GetValues ( typeof ( ActionCategory ) ) )
			{
				CB_Category.Items.Add ( ac );
			}
			//定数：アクション態勢コンボボックス
			foreach ( ActionPosture ac in Enum.GetValues ( typeof ( ActionPosture ) ) )
			{
				CB_Posture.Items.Add ( ac );
			}
		}
		//---------------------------------------------------------------------
		//編集対象アクション
		private Action action = new Action ( "New_Action" );
		
		//シークエンスツリー更新
		public System.Action RemakeTree = ()=>{};


		//キャラデータ
		public void SetCharaData ( Chara ch )
		{
			CBSL_Next.SetCharaData ( ch.behavior.BD_Sequence );
		}

		//関連付け
		public void Assosiate ( Action act )
		{
			action = act;

			//表示部
			TB_Name.Text = act.Name;
			CBSL_Next.SelectName ( act.NextActionName );
			CB_Category.SelectedItem = act.Category;
			CB_Posture.SelectedItem = act.Posture;
			TBN_HitNum.Text = act.HitNum.ToString();
			Tbn_HitPitch.Text = act.HitPitch.ToString();
			Tbn_Balance.Text = act.Balance.ToString();

			//各コントロールに設定用のデリゲートを渡す
			CBSL_Next.SetFunc = a=>act.NextActionName = a.Name;
			TBN_HitNum.SetFunc = i=>act.HitNum = i;
			Tbn_HitPitch.SetFunc = i=>act.HitPitch = i;
			Tbn_Balance.SetFunc = i=>act.Balance = i;
		}

		//-----------------------------------------------------------
		//アクション：名前の変更
		private void TB_Name_TextChanged ( object sender, EventArgs e )
		{
			action.Name = TB_Name.Text;
			//Ctrl_All.Inst.AllDisp ();
		}


		//-----------------------------------------------------------
		//カテゴリの変更

		//インデックスが変更されたとき
		private void CB_Category_SelectedIndexChanged ( object sender, EventArgs e )
		{
			//@info ◆外部から直接 SelectedIndex が変更されたときもイベントが発生する
			// ->SelectionChangeCommitted()を用いる
		}

		//ドロップダウンリストから変更されて閉じたとき
		private void CB_Category_SelectionChangeCommitted ( object sender, EventArgs e )
		{
			//@info ◆現ドロップダウンリストを閉じたときのみイベントが発生する

			//シークエンスツリーの再構築が必要
			action.Category = (ActionCategory)CB_Category.SelectedItem;
			//ツリー再構築
			RemakeTree ();
			
			//Ctrl_All.Inst.AllDisp ();
		}

		//-----------------------------------------------------------
		//体勢
		//		値のみで表示の更新はない
		private void CB_Posture_SelectedIndexChanged ( object sender, EventArgs e )
		{
			action.Posture = (ActionPosture)CB_Posture.SelectedItem;
		}

	}
}
