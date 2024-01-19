using System.Windows.Forms;
using System.IO;
using System.Drawing;


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
			LoadCtrl ();		//コントロール初期化
			LoadTab ();			//タブ初期化
			LoadSubForm ();		//サブフォーム初期化
			
			SetEnvironment ();
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
			//タブページに手動追加
			TabPage tp = tabControl1.TabPages [ (int)TAB_NAME.TAB_ACTION ];
			tp.Controls.Add ( ctrl_SqcList_Act );
		}

		//タブ_スクリプト(Act)の初期化
		private void TabScript_A_Load ()
		{
			//タブページに手動追加
			TabPage tp = tabControl1.TabPages [ (int)TAB_NAME.TAB_SCRIPT_A ];
			tp.Controls.Add ( ctrl_cmpd_bhv );

			//コントロール位置指定
			tp.SuspendLayout ();

			ctrl_cmpd_bhv.Size = new Size ( tp.Size.Width - ctrl_fmBtn_bhv.Width, tp.Size.Height );
			ctrl_cmpd_bhv.Location = new Point ( 0, 0 );

			//ボタンの追加
			tp.Controls.Add ( ctrl_fmBtn_bhv );
			ctrl_fmBtn_bhv.Location = new Point ( ctrl_cmpd_bhv.Width, 0 );

			tp.ResumeLayout ( false );
		}


		//タブ_エフェクトの初期化
		private void TabEffect_Load ()
		{
			//タブページに手動追加
			TabPage tp = tabControl1.TabPages [ (int)TAB_NAME.TAB_EFFECT ];
			tp.Controls.Add ( ctrl_SqcList_Efc );
		}

		//タブ_スクリプト(Ef)の初期化
		private void TabScript_E_Load ()
		{
			//タブページに手動追加
			TabPage tp = tabControl1.TabPages [ (int)TAB_NAME.TAB_SCRIPT_E ];
			tp.Controls.Add ( ctrl_cmpd_gns );

			//コントロール位置指定
			tp.SuspendLayout ();

			ctrl_cmpd_gns.Size = new Size ( tp.Size.Width - ctrl_fmBtn_gns.Width, tp.Size.Height );
			ctrl_cmpd_gns.Location = new Point ( 0, 0 );

			//ボタンの追加
			tp.Controls.Add ( ctrl_fmBtn_gns );
			ctrl_fmBtn_gns.Location = new Point ( ctrl_cmpd_gns.Width, 0 );

			tp.ResumeLayout ( false );
		}


		//タブ_コマンドの初期化
		private void TabCommand_Load ()
		{
			//タブページに手動追加
			TabPage tp = tabControl1.TabPages [ (int)TAB_NAME.TAB_COMMAND ];
			tp.Controls.Add ( ctrl_Cmd );
		}

		//タブ_ブランチの初期化
		private void TabBranch_Load ()
		{
			//タブページに手動追加
			TabPage tp = tabControl1.TabPages [ (int)TAB_NAME.TAB_BRANCH ];
			tp.Controls.Add ( ctrl_Brc );
		}

		//タブ_ルートの初期化
		private void TabRoute_Load ()
		{
			//タブページに手動追加
			TabPage tp = tabControl1.TabPages [ (int)TAB_NAME.TAB_ROUTE ];
			tp.Controls.Add ( ctrl_Rut );
		}
	}
}
