using System.Windows.Forms;

namespace ScriptEditor
{
	//Ef生成フォームのコントロールをまとめるクラス
	public class ControlsEfGnrt
	{
		//エフェクト生成リスト表示
		public ListBox lb_EfGnrt { get; set; }

		//選択用ガーニッシュコンボボックス (エフェクトの集合)
		public CB_SequenceList cb_garnish;

		Button btn_EfGnrtAdd { get; set; }		//EfGnrt追加
		Button btn_EfGnrtRem { get; set; }		//EfGnrt削除

		//生成位置(X,Y)
		public TB_Number tbn_EfX { get; set; }
		public TB_Number tbn_EfY { get; set; }
		public TB_Number tbf_EfZ { get; set; }

		public CheckBox ckb_Gnrt { get; set; }		//生成
		public CheckBox ckb_Loop { get; set; }		//ループ
		public CheckBox ckb_Sync { get; set; }      //同期

#if false
		//初期化
		public void Load
		(
			EditCompend ec, DispCompend dc,
			ListBox lb_eg, CB_SequenceList cb_gns,
			Button btn_ega, Button btn_egr,
			TB_Number tbn_efx, TB_Number tbn_efy, TB_Number tbf_efz,
			CheckBox ckb_g, CheckBox ckb_l, CheckBox ckb_s
		)
		{
			lb_EfGnrt = lb_eg;

			cb_garnish = cb_gns;
			cb_garnish.DispCompend = dc;

			btn_EfGnrtAdd = btn_ega;
			btn_EfGnrtRem = btn_egr;

			tbn_EfX = tbn_efx;
			tbn_EfY = tbn_efy;
			tbf_EfZ = tbf_efz;

			ckb_Gnrt = ckb_g;
			ckb_Loop = ckb_l;
			ckb_Sync = ckb_s;
		}
#endif
		//関連付け
		public void Assosiate ( Script scp )
		{
//			tbf_EfZ.SetFunc ( scp.BD_EfGnrt);
		}

		//オン
		public void On ()
		{
			lb_EfGnrt.Enabled = true;
			cb_garnish.Enabled = true;
			btn_EfGnrtAdd.Enabled = true;
			btn_EfGnrtRem.Enabled = true;
			tbn_EfX.Enabled = true;
			tbn_EfY.Enabled = true;
			tbf_EfZ.Enabled = true;
			ckb_Gnrt.Enabled = true;
			ckb_Loop.Enabled = true;
			ckb_Sync.Enabled = true;
		}

		//オフ
		public void Off ()
		{
			//クリア
			tbn_EfX.Clear ();
			tbn_EfY.Clear ();
			tbf_EfZ.Clear ();
			ckb_Gnrt.Checked = false;
			ckb_Loop.Checked = false;
			ckb_Sync.Checked = false;

			//無効化
			lb_EfGnrt.Enabled = false;
			cb_garnish.Enabled = false;
			/* 追加ボタンだけはOn */
			btn_EfGnrtRem.Enabled = false;
			tbn_EfX.Enabled = false;
			tbn_EfY.Enabled = false;
			tbf_EfZ.Enabled = false;
			ckb_Gnrt.Enabled = false;
			ckb_Loop.Enabled = false;
			ckb_Sync.Enabled = false;
		}

	}
}
