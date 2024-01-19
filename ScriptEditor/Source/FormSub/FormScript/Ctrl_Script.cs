using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ScriptEditor
{
	using PrmInt = ScriptParam < int >;

	public partial class Ctrl_Script : UserControl
	{
		//編集環境
		public EditCompend EditCompend { get; set; } = null;
		public EditScript EditScript { get; set; } = null;

		//現在スクリプト
		private Script script = null;

		//コントロール集合
		private List < TB_Number > ls_tbn = new List<TB_Number> ();
		private List < TB_ScpNumber > ls_tbsn = new List<TB_ScpNumber> ();

		//計算状態反映グループセッタ
		private System.Action < Script, CLC_ST > GrouptSetterCLC_ST;


		//@todo グループを越えて、設定項目を全体に反映する切り替えボタン
		//グループ編集
		// 各値が異なるとき空白表示、同一のとき値表示


		//コンストラクタ
		public Ctrl_Script ()
		{
			InitializeComponent ();

			//計算状態 プルダウンメニュー初期化
			foreach ( CLC_ST clcst in Enum.GetValues ( typeof ( CLC_ST ) ) )
			{
				cB_ClcSt.Items.Add ( clcst );
			}
			GrouptSetterCLC_ST = (s,c) => s.CalcState = c;

			//スクリプトの数値設定用デリゲートを指定
#if false
			TBSN_PosX.PrmInt = new PrmInt ( (s,i)=>s.SetPosX(i), s=>s.Pos.X );
			TBSN_PosY.PrmInt = new PrmInt ( (s,i)=>s.SetPosY(i), s=>s.Pos.Y );
			TBSN_VelX.PrmInt = new PrmInt ( (s,i)=>s.Param_Btl.SetVelX(i), s=>s.Param_Btl.Vel.X );
			TBSN_VelY.PrmInt = new PrmInt ( (s,i)=>s.Param_Btl.SetVelY(i), s=>s.Param_Btl.Vel.Y );
			TBSN_AccX.PrmInt = new PrmInt ( (s,i)=>s.Param_Btl.SetAccX(i), s=>s.Param_Btl.Acc.X );
			TBSN_AccY.PrmInt = new PrmInt ( (s,i)=>s.Param_Btl.SetAccY(i), s=>s.Param_Btl.Acc.Y );
#endif
//			TBSN_PosX.Setter = (s,i)=>s.SetPosX(i);

			//登録
			ls_tbsn.Add ( TBSN_PosX );
			ls_tbsn.Add ( TBSN_PosY );
			ls_tbsn.Add ( TBSN_VelX );
			ls_tbsn.Add ( TBSN_VelY );
			ls_tbsn.Add ( TBSN_AccX );
			ls_tbsn.Add ( TBSN_AccY );


			//登録
			ls_tbn.Add ( tBN_Power );
			ls_tbn.Add ( tBN_BlackOut );
			ls_tbn.Add ( tBN_Vibration );
			ls_tbn.Add ( tBN_Stop );
		}

		//コントロール設定
		public void SetEnvironment ( EditCompend ec )
		{
			EditCompend = ec;
			EditScript = ec.EditScript;

			TBSN_PosX.SetEnvironment ( ec, new PrmInt ( (s,i)=>s.SetPosX(i), s=>s.Pos.X ) );
		}

		//更新(対象に変更があったとき)
		public void UpdateData ()
		{
			tB_Frame.Text = script.Frame.ToString ();
			cB_ClcSt.SelectedItem = script.CalcState;
			Tb_Img.Text = script.ImgName;

			foreach ( TB_Number tbn in ls_tbn )
			{
				tbn.UpdateData ();
			}
		}

		//関連付け(対象が変更になったとき)
		public void Assosiate ( Script scp, Sequence sqc )
		{
			script = scp;
//			sequence = sqc;

			//スクリプトから表示を設定
			tB_Frame.Text = script.Frame.ToString ();
			cB_ClcSt.SelectedItem = script.CalcState;
			Tb_Img.Text = script.ImgName;

			//各コントロールにスクリプト参照を設定
			foreach ( TB_ScpNumber tbsn in ls_tbsn )
			{
				tbsn.Assosiate ( scp );
			}

			//ラムダ式で単体設定デリゲートを指定
			tBN_Power.Assosiate ( i => scp.Param_Btl.Power = i, ()=> scp.Param_Btl.Power );
			tBN_BlackOut.Assosiate ( i => scp.Param_Ef.BlackOut = i, ()=> scp.Param_Ef.BlackOut );
			tBN_Vibration.Assosiate ( i => scp.Param_Ef.Vibration = i, ()=> scp.Param_Ef.Vibration );
			tBN_Stop.Assosiate ( i => scp.Param_Ef.Stop = i, ()=> scp.Param_Ef.Stop );

#if false
			//グループセッタを登録
			tBN_PosX.GroupSetter = EditScript.GroupSetterPosX;
			tBN_PosY.GroupSetter = EditScript.GroupSetterPosY;
			tBN_VelX.GroupSetter = EditScript.GroupSetterVelX;
			tBN_VelY.GroupSetter = EditScript.GroupSetterVelY;
			tBN_AccX.GroupSetter = EditScript.GroupSetterAccX;
			tBN_AccY.GroupSetter = EditScript.GroupSetterAccY;
			tBN_Power.GroupSetter = EditScript.GroupSetterPower;
#endif
		}

		//計算状態
		//インデックスが変更されたときのイベントでは初期化やスクリプト選択で発生してしまう
		private void cB_ClcSt_SelectedIndexChanged ( object sender, EventArgs e )
		{
		}
		//ドロップダウンリストを変更して閉じたときに発生するイベント
		private void cB_ClcSt_SelectionChangeCommitted ( object sender, EventArgs e )
		{
			script.CalcState = (CLC_ST)cB_ClcSt.SelectedItem;
			EditScript.DoSetterInGroup_T ( GrouptSetterCLC_ST, script.CalcState );
		}

		//編集範囲を設定
		private void RB_TRG_ALL_CheckedChanged ( object sender, EventArgs e )
		{
			//foreach ( TB_Number tbn in l_tbn ) { tbn.SetAll (); }
		}

		private void RB_TRG_GRP_CheckedChanged ( object sender, EventArgs e )
		{
			//foreach ( TB_Number tbn in l_tbn ) { tbn.SetGroup (); }
		}

		private void RB_TRG_SGL_CheckedChanged ( object sender, EventArgs e )
		{
			//foreach ( TB_Number tbn in l_tbn ) { tbn.SetSingle (); }
		}
	}

}
