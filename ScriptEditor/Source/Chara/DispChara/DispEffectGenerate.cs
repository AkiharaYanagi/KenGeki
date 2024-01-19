using System.Windows.Forms;
using System.Diagnostics;
using System.ComponentModel;

namespace ScriptEditor
{
	using BL_EFGN = BindingList < EffectGenerate >;


	public class DispEffectGenerate
	{
		//エフェクトジェネレートフォームのコントロールのまとめ
		ControlsEfGnrt ctrlEfGnrt { get; set; }

		//初期化
		public void Load ( EditChara ec, ControlsEfGnrt ceg )
		{
			ctrlEfGnrt = ceg;
		}

		//表示
		public void Disp ( Script script )
		{
			//エラーチェック
			Debug.Assert ( null != script );

			if ( null == ctrlEfGnrt ) { return; }
			
			//作業用
			Chara ch = EditChara.Inst.Chara;
			ControlsEfGnrt c = ctrlEfGnrt;
			BL_EFGN bl_efgn = script.BD_EfGnrt.GetBindingList ();

			//Ef生成が存在しないとき
			if ( bl_efgn.Count == 0 )
			{
				c.Off ();
				return;
			}

			//１つ以上存在したら
			c.On ();

			//リストボックスの更新
			ListBox lb = c.lb_EfGnrt;
			lb.DataSource = bl_efgn;
			lb.DisplayMember = "name";

			//リストボックスの選択
			if ( null == lb.SelectedItem ) { lb.SelectedIndex = 0; }

			//エフェクト生成を取得
			EffectGenerate efGnrt = ( EffectGenerate ) lb.SelectedValue;

			//エフェクトの取得
#if false
			int indexEf = efGnrt.Id;
			BindingList<Sequence> listEf = ch.garnish.BD_Sequence.GetBindingList ();
			Debug.Assert ( indexEf < listEf.Count );
			Effect ef = ( Effect ) listEf[ indexEf ];
#endif
			Effect ef = (Effect)ch.garnish.BD_Sequence.Get ( efGnrt.EfName );

			//コンボボックスの更新
//			c.cb_garnish.SelectedIndex = indexEf;
//			c.cb_garnish.refInt = efGnrt.id;

			//名前の取得
			efGnrt.Name = ef.Name;

			//バインディングリストの内容更新
			for ( int i = 0; i < bl_efgn.Count; ++i )
			{
				bl_efgn.ResetItem ( i );
			}

			//X,Y,Z
			c.tbn_EfX.UpdateData ();
			c.tbn_EfY.UpdateData ();
			c.tbf_EfZ.UpdateData ();

			//生成
			c.ckb_Gnrt.Checked = efGnrt.Gnrt;

			//繰返
			c.ckb_Loop.Checked = efGnrt.Loop;
			
			//同期
			c.ckb_Sync.Checked = efGnrt.Sync;
		}
	}
}
