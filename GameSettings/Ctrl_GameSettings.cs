using System;
using System.Windows.Forms;
using ScriptEditor;


namespace GameSettings
{
	using STG_STRT = GameSettingsData.Stng_Start;
	using STG_OPRT = GameSettingsData.Stng_Operate;
	using STG_CHAR = GameSettingsData.Stng_Chara;


	public partial class Ctrl_GameSettings : UserControl
	{
		//-----------------------------------------------------
		//対象データ
		private GameSettingsData stgData = new GameSettingsData ();

		private string filename = "GameSettings.dat";

		//-----------------------------------------------------
		//コンストラクタ
		public Ctrl_GameSettings ()
		{
			InitializeComponent ();

			//コンボボックス初期化
			CB_Chara1.Items.Add ( STG_CHAR.testPlayer );
			CB_Chara1.Items.Add ( STG_CHAR.Rakuno  );
			CB_Chara1.Items.Add ( STG_CHAR.Yukino );
			CB_Chara1.Items.Add ( STG_CHAR.Random );
			CB_Chara1.SelectedIndex = 0;

			CB_Chara2.Items.Add ( STG_CHAR.testPlayer );
			CB_Chara2.Items.Add ( STG_CHAR.Rakuno  );
			CB_Chara2.Items.Add ( STG_CHAR.Yukino );
			CB_Chara2.Items.Add ( STG_CHAR.Random );
			CB_Chara2.SelectedIndex = 0;


			//@todo 一時使用不可
			CB_Chara1.Enabled = false;
			CB_Chara2.Enabled = false;

			RB_CPU1.Enabled = false;
			RB_CPU2.Enabled = false;
			RB_Player1.Enabled = false;
			RB_Player2.Enabled = false;


			//開始時に読込
			stgData.Load ( filename );

			//データをコントロールに反映
			InitCtrl ();
		}

		public void InitCtrl ()
		{
			//開始状態
			switch ( stgData.Start )
			{ 
			case STG_STRT.General : SelectRB_Start ( RB_General ); break;
			case STG_STRT.Battle : SelectRB_Start ( RB_Battle ); break;
			case STG_STRT.Traning : SelectRB_Start ( RB_Training ); break;
			}

			//デモ
			CHK_Demo.Checked = stgData.Demo;

			//操作
			switch ( stgData.Operate1p )
			{
			case STG_OPRT.Player : SelectRB_Operate1 ( RB_Player1 ); break;
			case STG_OPRT.CPU : SelectRB_Operate1 ( RB_CPU1 ); break;
			}
			switch ( stgData.Operate2p )
			{
			case STG_OPRT.Player : SelectRB_Operate2 ( RB_Player2 ); break;
			case STG_OPRT.CPU : SelectRB_Operate2 ( RB_CPU2 ); break;
			}

			//キャラ
			switch ( stgData.Chara1p )
			{ 
			case STG_CHAR.testPlayer : SelectCB_Chara1 ( STG_CHAR.testPlayer ); break;
			case STG_CHAR.Rakuno : SelectCB_Chara1 ( STG_CHAR.Rakuno ); break;
			case STG_CHAR.Yukino : SelectCB_Chara1 ( STG_CHAR.Yukino ); break;
			case STG_CHAR.Random : SelectCB_Chara1 ( STG_CHAR.Random ); break;
			}
		}

		private void SelectRB_Start ( RadioButton rb )
		{
			//全てOFF
			RB_General.Checked = false;
			RB_Battle.Checked = false;
			RB_Training.Checked = false;

			//対象をON
			rb.Checked = true;
		}

		private void SelectRB_Operate1 ( RadioButton rb )
		{
			//全てOFF
			RB_Player1.Checked = false;
			RB_CPU1.Checked = false;

			//対象をON
			rb.Checked = true;
		}

		private void SelectRB_Operate2 ( RadioButton rb )
		{
			//全てOFF
			RB_Player2.Checked = false;
			RB_CPU2.Checked = false;

			//対象をON
			rb.Checked = true;
		}

		private void SelectCB_Chara1 ( STG_CHAR ch )
		{
			CB_Chara1.SelectedItem = ch;
		}

		private void SelectCB_Chara2 ( STG_CHAR ch )
		{
			CB_Chara2.SelectedItem = ch;
		}


		//========================================================================
		//	コントロールイベント
		//========================================================================

		//----------------------------------------------------------------------
		//ボタン
		private void Btn_Folder_Click ( object sender, System.EventArgs e )
		{
			FormUtility.OpenCurrentDir ();
		}

		private void Btn_Decide_Click ( object sender, System.EventArgs e )
		{
			stgData.Save ( filename );
			Application.Exit ();
		}

		private void Btn_Cancel_Click ( object sender, System.EventArgs e )
		{
			Application.Exit ();
		}


		//----------------------------------------------------------------------
		//ラジオボタン選択：開始状態
		private void RB_General_CheckedChanged ( object sender, EventArgs e )
		{
			stgData.Start = STG_STRT.General;
		}

		private void RB_Battle_CheckedChanged ( object sender, EventArgs e )
		{
			stgData.Start = STG_STRT.Battle;
		}

		private void RB_Training_CheckedChanged ( object sender, EventArgs e )
		{
			stgData.Start = STG_STRT.Traning;
		}


		//----------------------------------------------------------------------
		//チェックボックス選択
		private void CHK_Demo_CheckedChanged ( object sender, EventArgs e )
		{
			stgData.Demo = CHK_Demo.Checked;
		}

		//----------------------------------------------------------------------
		//ラジオボタン選択：操作
		private void RB_Player1_CheckedChanged ( object sender, EventArgs e )
		{
			if ( RB_Player1.Checked )
			{
				stgData.Operate1p = STG_OPRT.Player;
			}
		}

		private void RB_CPU1_CheckedChanged ( object sender, EventArgs e )
		{
			if ( RB_CPU1.Checked )
			{
				stgData.Operate1p = STG_OPRT.CPU;
			}
		}

		private void RB_Player2_CheckedChanged ( object sender, EventArgs e )
		{
			if ( RB_Player2.Checked )
			{
				stgData.Operate2p = STG_OPRT.Player;
			}
		}

		private void RB_CPU2_CheckedChanged ( object sender, EventArgs e )
		{
			if ( RB_CPU2.Checked )
			{
				stgData.Operate2p = STG_OPRT.CPU;
			}
		}

		//----------------------------------------------------------------------
		//ドロップダウンリスト選択反映
		private void CB_Chara1_SelectionChangeCommitted ( object sender, EventArgs e )
		{
			stgData.Chara1p = (STG_CHAR)CB_Chara1.SelectedItem;
		}

		private void CB_Chara2_SelectionChangeCommitted ( object sender, EventArgs e )
		{
			stgData.Chara2p = (STG_CHAR)CB_Chara2.SelectedItem;
		}

	}
}
