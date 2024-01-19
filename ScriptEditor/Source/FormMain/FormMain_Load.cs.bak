using System.Windows.Forms;
using System.IO;


namespace ScriptEditor
{
	public partial class FormMain : Form
	{
		//==================================================================================
		//	環境初期化
		//==================================================================================
		private void LoadEnvironment ()
		{
			LoadSetting ();		//設定ファイル初期化
			LoadSubForm ();		//サブフォーム初期化
			LoadTab ();			//タブ初期化
		}

		//---------------------------------------------------------------------
		//	設定ファイル初期化
		//---------------------------------------------------------------------
		private void LoadSetting ()
		{
			//設定ファイル読込
			XML_IO.SettingFilepath = Directory.GetCurrentDirectory () + "\\setting.xml";
			stgs = (Ctrl_Settings) XML_IO.Load ( stgs.GetType () );
			SetFormText ( stgs.LastFilepath );
		}

		//---------------------------------------------------------------------
		//	タブ初期化関連
		//---------------------------------------------------------------------

		//各タブの初期化
		private void LoadTab ()
		{
			TabAction_Load ();
			TabScript_A_Load ();
			TabEffect_Load ();
			TabScript_E_Load ();
			TabCommand_Load ();
			TabBranch_Load ();
			TabRoute_Load ();
		}
		//--------------------------------------------------------------------------

		//タブ_アクションの初期化
		private void TabAction_Load ()
		{
			Ctrl_SqcList.CTRL_SQC ac = Ctrl_SqcList.CTRL_SQC.ACTION;
			ctrl_SqcList_Act.SetEnviroment ( ac, ()=>new Action(), stgs );
			ctrl_SqcList_Act.SetCharaData ( chara.behavior );
			ctrl_SqcList_Act.LoadCtrl ();
		}

		//タブ_スクリプト(A)の初期化
		private void TabScript_A_Load ()
		{
			//ビヘイビア(:コンペンド)の指定
			EditBehavior eb = EditChara.Inst.EditBehavior;
			DispBehavior db = DispChara.Inst.DispBehavior;

			db.SetCtrl ( eb, cpd_Behavior );    //表示

			cpd_Behavior.SetEnviron ( eb, db );	//ビヘイビア(:コンペンド)初期化
			cpd_Behavior.SetBehavior ();

			//サブフォームにおける環境設定
			SetEnvironmentSubForms ( eb, db );

			//アクションのみ
			FormAction.Inst.SetCtrl ( eb.EditAction, db.DispAction, db );
		}

		//タブ_エフェクトの初期化
		private void TabEffect_Load ()
		{
			Ctrl_SqcList.CTRL_SQC ef = Ctrl_SqcList.CTRL_SQC.EFFECT;
			ctrl_SqcList_Ef.SetEnviroment ( ef, ()=>new Effect(), stgs );
			ctrl_SqcList_Ef.SetCharaData ( chara.garnish );
			ctrl_SqcList_Ef.LoadCtrl ();
		}

		//タブ_スクリプト(E)の初期化
		private void TabScript_E_Load ()
		{
			//ガーニッシュ(:コンペンド)の指定
			EditGarnish eg = EditChara.Inst.EditGarnish;
			DispGarnish dg = DispChara.Inst.DispGarnish;

			dg.SetCtrl ( eg, cpd_Garnish );	//表示

			cpd_Garnish.SetEnviron ( eg, dg );	//ガーニッシュ(:コンペンド)初期化
			cpd_Garnish.SetGarnish ();
	
			//サブフォームの初期化
			Form_ScriptList.Inst.SetEnvironment ( eg, chara );
			FormImage.Inst.SetEnviron ( eg, dg );
			FormRect2.Inst.SetEnvironment ( eg, dg );
		}

		//タブ_コマンドの初期化
		private void TabCommand_Load ()
		{
			ctrl_CmdList1.SetEnvironment ( stgs );
		}

		//タブ_ブランチの初期化
		private void TabBranch_Load ()
		{
			ctrl_Branch1.SetEnvironment ( stgs );
			ctrl_Branch1.SetCharaData ( chara );
		}

		//タブ_ルートの初期化
		private void TabRoute_Load ()
		{
			ctrl_Route1.SetEnvironment ( stgs );
		}
<<<<<<< HEAD




=======
>>>>>>> 6eb90c5221393b3b859870b2a900cc77a833ba38
	}
}
