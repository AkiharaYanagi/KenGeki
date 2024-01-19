using System.Windows.Forms;

using ScriptEditorUtility;


namespace ScriptEditor
{
	public partial class FormMain : Form
	{
		//-------------------------------------------------------
		//各コントロール 実体
		private Ctrl_SqcList ctrl_SqcList_Act = new Ctrl_SqcList ();	//シークエンスリスト・アクション
		private _Ctrl_Compend ctrl_cmpd_bhv = new _Ctrl_Compend ();		//コントロール・ビヘイビア
		private Ctrl_SubFormBtn ctrl_fmBtn_bhv = new Ctrl_SubFormBtn ();	//フォームボタン
		
		private Ctrl_SqcList ctrl_SqcList_Efc = new Ctrl_SqcList ();	//シークエンスリスト・エフェクト
		private _Ctrl_Compend ctrl_cmpd_gns = new _Ctrl_Compend ();		//コントロール・ガーニッシュ
		private Ctrl_SubFormBtn ctrl_fmBtn_gns = new Ctrl_SubFormBtn ();	//フォームボタン

		private Ctrl_CmdList ctrl_Cmd = new Ctrl_CmdList ();
		private Ctrl_Branch ctrl_Brc = new Ctrl_Branch ();
		private Ctrl_Route ctrl_Rut = new Ctrl_Route ();

		//-------------------------------------------------------
		//サブフォーム用コントロール 実体
		private _Ctrl_Script ctrl_Scp = new _Ctrl_Script ();
		private Ctrl_AllRect ctrl_Rct = new Ctrl_AllRect ();
		private Ctrl_Scp_Route ctrl_scpRut = new Ctrl_Scp_Route ();

		//-------------------------------------------------------


		//コントロール登録
		public void LoadCtrl ()
		{
			//コントロールの全体監理に渡す
			All_Ctrl.Inst.SqcList_Act = ctrl_SqcList_Act;
			All_Ctrl.Inst.Compend_Bhv = ctrl_cmpd_bhv;
			All_Ctrl.Inst.SqcList_Efc = ctrl_SqcList_Efc;
			All_Ctrl.Inst.Compend_Gns = ctrl_cmpd_gns;
			All_Ctrl.Inst.Cmd = ctrl_Cmd;
			All_Ctrl.Inst.Brc = ctrl_Brc;
			All_Ctrl.Inst.Rut = ctrl_Rut;

			All_Ctrl.Inst.Scp = ctrl_Scp;
			All_Ctrl.Inst.Rct = ctrl_Rct;
			All_Ctrl.Inst.ScpRut = ctrl_scpRut;
		}

		public void SetEnvironment ()
		{
			//-------------------------------------------------
			//環境設定

			//アクションリスト
			Ctrl_SqcList.CTRL_SQC act = Ctrl_SqcList.CTRL_SQC.ACTION;
			ctrl_SqcList_Act.SetEnviroment ( act, stgs );


			//編集の指定 ビヘイビア(:コンペンド)
			EditBehavior eb = EditChara.Inst.EditBehavior;
			ctrl_cmpd_bhv.SetEnviron ( eb );
			ctrl_scpRut.SetEnvironment ( eb );


			//effectリスト
			Ctrl_SqcList.CTRL_SQC efc = Ctrl_SqcList.CTRL_SQC.EFFECT;
			ctrl_SqcList_Efc.SetEnviroment ( efc, stgs );


			//編集の指定 ガーニッシュ(:コンペンド)
			EditGarnish eg = EditChara.Inst.EditGarnish;
			ctrl_cmpd_gns.SetEnviron ( eg );

			//-------------------------------------------------
			ctrl_Cmd.SetEnvironment ( stgs );
			ctrl_Brc.SetEnvironment ( stgs );
			ctrl_Rut.SetEnvironment ( stgs );

			//-------------------------------------------------
			//サブフォームにおける環境設定
			FormScript.Inst.SetCtrl ( ctrl_Scp );
			FormRect2.Inst.SetCtrl ( ctrl_Rct );
			FormRoute.Inst.SetCtrl ( ctrl_scpRut );
		}
	}
}
