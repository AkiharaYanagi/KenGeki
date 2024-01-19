using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace ScriptEditor
{
	public partial class FormMain : Form
	{
		//==================================================================================
		//タブ
		//==================================================================================
		//タブ名
		public enum TAB_NAME
		{
			TAB_ACTION,     // 0 : "アクション"
			TAB_SCRIPT_A,	// 1 : "スクリプト(A)"
			TAB_EFFECT,     // 2 : "エフェクト"
			TAB_SCRIPT_E,	// 3 : "スクリプト(E)"
			TAB_COMMAND,    // 4 : "コマンド" 
			TAB_BRANCH,		// 5 : "ブランチ" 
			TAB_ROUTE,      // 6 : "ルート"
		}

		//タブ変更時
		private void tabControl1_SelectedIndexChanged ( object sender, EventArgs e )
		{
			//タブ	(enumをswitchの条件に用いるには(int)キャストが必要)
			switch ( tabControl1.SelectedIndex )
			{
				case (int)TAB_NAME.TAB_ACTION: tabAction_Selected (); break;
				case (int)TAB_NAME.TAB_SCRIPT_A: tabScript_A_Selected (); break;
				case (int)TAB_NAME.TAB_EFFECT: tabEffect_Selected (); break;
				case (int)TAB_NAME.TAB_SCRIPT_E: tabScript_E_Selected (); break;
				case (int)TAB_NAME.TAB_COMMAND: tabCommand_Selected (); break;
				case (int)TAB_NAME.TAB_BRANCH: tabBranch_Selected (); break;
				case (int)TAB_NAME.TAB_ROUTE: tabRoute_Selected (); break;
				default: Debug.Assert ( false, "タブの選択失敗" ); break;
			}
		}

		//タブ離去時
		private void tabControl1_Deselected ( object sender, TabControlEventArgs e )
		{
			//タブ	(enumはintキャストが必要)
			switch ( tabControl1.SelectedIndex )
			{
				case ( int ) TAB_NAME.TAB_ACTION: tabAction_Deselected (); break;
				case ( int ) TAB_NAME.TAB_SCRIPT_A: tabScript_A_Deselected (); break;
				case ( int ) TAB_NAME.TAB_EFFECT: tabEffect_Deselected (); break;
				case ( int ) TAB_NAME.TAB_SCRIPT_E: tabScript_E_Deselected (); break;
				case ( int ) TAB_NAME.TAB_COMMAND: tabCommand_Deselected (); break;
				case ( int ) TAB_NAME.TAB_BRANCH: tabBranch_Deselected (); break;
				case ( int ) TAB_NAME.TAB_ROUTE: tabRoute_Deselected (); break;
				default: Debug.Assert ( false, "タブの離去失敗" ); break;
			}
		}

		//フォーム閉止時
		private void Form1_FormClosing ( object sender, FormClosingEventArgs e )
		{
		}



		//-----------------------------------------------------------------------
		//[アクション]タブ選択時
		public void tabAction_Selected ()
		{
#if false
			ctrl_SqcList_Act.UpdateData ();
			ctrl_SqcList_Act.UpdateImage ();
			ctrl_SqcList_Act.Refresh ();
#endif
		}

		//[アクション]タブ離去時
		public void tabAction_Deselected ()
		{
			ctrl_SqcList_Act.ApplyData_Action ();
			All_Ctrl.Inst.Compend_Bhv.UpdateSqcTree ();
			All_Ctrl.Inst.UpdateData ();
			FormImage.Inst.UpdateData ();
		}

		//-----------------------------------------------------------------------
		//[スクリプト(A)]タブ選択時
		public void tabScript_A_Selected ()
		{
#if false
			//共通フォームにビヘイビアを設定
			DispBehavior db = DispChara.Inst.DispBehavior;
			
			//他タブのデータで更新
			ctrl_cmpd_bhv.UpdateData ();
			ctrl_cmpd_bhv.SelectTop ();

			Assosiate ( eb.SelectedSequence, eb.SelectedScript );
			eb.SelectScript ( 0, 0 );


			//サブフォームにビヘイビアを設定 ( ビヘイビア / ガーニッシュ 切替 )
//			FormAction.Inst.SetCtrl ( eb.EditAction, db.DispAction, db );	//フォーム：アクション(ガーニッシュでは行わない)
			FormImage.Inst.SetData ( eb.Compend.BD_Image );
#endif
			//サブフォームすべてにコンペンド指定
			EditBehavior eb = EditChara.Inst.EditBehavior;
			SetEnvironment_SubForms ( eb );

			//イメージ指定
			FormImage.Inst.SetCharaData ( chara.behavior.BD_Image );

#if false
			//1回のみ
			//スクリプト内のImageNameからIDを外す
			BindingDictionary < Sequence > BD_Sqc = chara.behavior.BD_Sequence;
			foreach ( Sequence sqc in BD_Sqc.GetEnumerable () )
			{
				foreach ( Script scp in sqc.ListScript )
				{
					int len = scp.ImgName.Length;
					scp.ImgName = scp.ImgName.Substring ( 4, len - 4 );
				}
			}
			BindingDictionary < Sequence > BD_efc = chara.garnish.BD_Sequence;
			foreach ( Sequence sqc in BD_efc.GetEnumerable () )
			{
				foreach ( Script scp in sqc.ListScript )
				{
					int len = scp.ImgName.Length;
					scp.ImgName = scp.ImgName.Substring ( 4, len - 4 );
				}
			}

#endif
		}

		//----------------------
		//str_indexからheadを除き、Int.Parse()して返す
		private int GetIndex ( string str_index, string head )
		{
			int n = head.Length;
			int nextActionID = int.Parse ( str_index.Substring ( n, str_index.Length - n ) );
			return nextActionID;
		}

		
		//[スクリプト(A)]タブ離去時
		public void tabScript_A_Deselected ()
		{
			HideSubForms ();
		}

		//-----------------------------------------------------------------------
		//[エフェクト]タブ選択時
		public void tabEffect_Selected ()
		{
#if false
			ctrl_SqcList_Ef.UpdateData ();
			ctrl_SqcList_Ef.UpdateImage ();
#endif
		}

		//[エフェクト]タブ離去時
		public void tabEffect_Deselected ()
		{
			ctrl_SqcList_Efc.ApplyData_Effect ();
			All_Ctrl.Inst.Compend_Gns.UpdateSqcTree ();
			All_Ctrl.Inst.UpdateData ();
			FormImage.Inst.UpdateData ();
		}

		//-----------------------------------------------------------------------
		//[スクリプト(E)]
		public void tabScript_E_Selected ()
		{
#if false
			//共通フォームにガーニッシュを設定
			EditGarnish eg = EditChara.Inst.EditGarnish;
			DispGarnish dg = DispChara.Inst.DispGarnish;

			//cpd_Garnish.UpdateData ();
			//cpd_Garnish.SelectTop ();
			Assosiate ( eg.SelectedSequence, eg.SelectedScript );
			eg.SelectScript ( 0, 0 );

			//サブフォームにガーニッシュを設定
			Form_ScriptList.Inst.SetEnvironment ( eg );
			FormImage.Inst.SetEnviron ( eg );
			FormImage.Inst.SetData ( eg.Compend.BD_Image );
#endif
			//コンペンド指定
			EditGarnish eg = EditChara.Inst.EditGarnish;
			SetEnvironment_SubForms ( eg );

		//イメージ指定
			FormImage.Inst.SetCharaData ( chara.garnish.BD_Image );
		}

		//[スクリプト(E)]
		public void tabScript_E_Deselected ()
		{
			HideSubForms ();
		}


		//-----------------------------------------------------------------------
		public void tabCommand_Selected ()
		{
		}
		public void tabCommand_Deselected ()
		{
		}

		//-----------------------------------------------------------------------
		public void tabBranch_Selected ()
		{
			//ctrl_Branch1.Init ();
		}
		public void tabBranch_Deselected ()
		{
		}

		//-----------------------------------------------------------------------
		public void tabRoute_Selected ()
		{
			//ctrl_Route1.UpdateData ();
		}
		public void tabRoute_Deselected ()
		{
		}
	}
}
