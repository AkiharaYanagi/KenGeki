using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using System.Diagnostics;


namespace ScriptEditor
{

	//エフェクト生成フォーム
	public sealed partial class FormEffectGenerate : Form
	{

		//----------------------------------------------------------------
		//シングルトンパターン
		//sealed : 継承不可
		//インスタンス
		private static readonly FormEffectGenerate _instance = new FormEffectGenerate ();
		public static FormEffectGenerate Instance { get { return _instance; } }

		//プライベートコンストラクタ
		private FormEffectGenerate ()
		{
			//フォームのロケーションを手動に切り替え
			this.StartPosition = FormStartPosition.Manual;

			//コンポーネント初期化
			InitializeComponent ();

			//その他初期化
			ctrlEfGnrt = new ControlsEfGnrt ();
		}

		//閉じたときに破棄しない
		protected override void OnFormClosing ( FormClosingEventArgs e )
		{
			e.Cancel = true;
			this.Hide ();
		}
		//----------------------------------------------------------------

		//参照
		public DispCompend dispCompend { get; set; }
		public EditChara editChara { get; set; }
		public EditCompend editCompendAction { get; set; }	//アクションから指定する

		public DispEffectGenerate dispEfGnrt { get; set; }


		//@todo コントロールをまとめたクラスを定義する
		public ControlsEfGnrt ctrlEfGnrt { get; set; }


		//ロード(コントロールなどの初期化)
		public void LoadForm ( DispCompend dc, EditChara ec, EditCompend ecAction, DispEffectGenerate deg )
		{
			dispCompend = dc;
			editCompendAction = ecAction;
			editChara = ec;

			//Controls
			ctrlEfGnrt.Load
			( 
				ecAction, dc, 
				lb_EfGnrt, CB_EffectList, btnEfAdd, btnEfDel,
				tBN_EfX, tBN_EfY, tbf_efZ,
				chb_efGnrt, chb_efLoop, chb_efSync
			);

			//キャラ表示
			dispEfGnrt = deg;

			//キャラ表示にセット
			dispEfGnrt.Load ( ec, ctrlEfGnrt );
		}

		//設定(キャラのデータが変更されたときにする初期化)
		public void Set ( BindingList<Sequence> bl_effect )
		{
			CB_EffectList.DataSource = bl_effect;
			CB_EffectList.DisplayMember = "Name";
		}

		//スクリプトの更新
		public void UpdateData ( Script script )
		{
			dispEfGnrt.Disp ( script );
		}

		//追加
		private void btnEfAdd_Click ( object sender, EventArgs e )
		{
			//追加用エフェクト生成を生成
			EffectGenerate efGnrt = new EffectGenerate ();

			//現在スクリプトを取得
			Script script = editCompendAction.GetScript ();

			//エフェクトリストを取得
			BindingList<Sequence> listEf = editChara.chara.garnish.ListSequence;

			//生成先エフェクト
			Effect effect = ( Effect ) listEf [ efGnrt.id.i ];

			//エフェクトから名前の取得
			efGnrt.name = effect.Name;

			//スクリプトに追加
			script.ListGenerateEf.Add ( efGnrt );

			//バインディングリストの内容更新
			for ( int i = 0; i < script.ListGenerateEf.Count; ++i )
			{
				script.ListGenerateEf.ResetItem ( i );
			}

			//コントロールの表示
			ctrlEfGnrt.On ();
		}

		//削除
		private void btnEfDel_Click ( object sender, EventArgs e )
		{
			Script script = editChara.EditAction.GetScript ();
			script.ListGenerateEf.RemoveAt ( lb_EfGnrt.SelectedIndex );

			if ( script.ListGenerateEf.Count > 0 )
			{
				//バインディングリストの内容更新
				for ( int i = 0; i < script.ListGenerateEf.Count; ++i )
				{
					script.ListGenerateEf.ResetItem ( i );
				}
			}
			else
			{
				ctrlEfGnrt.Off ();
			}
		}

		private void lb_EfGnrt_SelectedIndexChanged ( object sender, EventArgs e )
		{
			if ( null == editCompendAction ) { return; }
			editCompendAction.selectedIndexEfGnrt = lb_EfGnrt.SelectedIndex;
		}

		//チェックボックス：生成
		private void chb_efGnrt_CheckedChanged ( object sender, EventArgs e )
		{
			//エフェクトジェネレートが０なら何もしない
			Script script = editChara.EditAction.GetScript ();
			if ( 0 == script.ListGenerateEf.Count ) { return; }

			//現在エフェクトジェネレートを取得
			EffectGenerate eg = ( EffectGenerate ) ctrlEfGnrt.lb_EfGnrt.SelectedItem;
			if ( null != eg ) { eg.gnrt = chb_efGnrt.Checked; }
		}

		//チェックボックス：繰返
		private void chb_efLoop_CheckedChanged ( object sender, EventArgs e )
		{
			//エフェクトジェネレートが０なら何もしない
			Script script = editChara.EditAction.GetScript ();
			if ( 0 == script.ListGenerateEf.Count ) { return; }

			EffectGenerate eg = ( EffectGenerate ) ctrlEfGnrt.lb_EfGnrt.SelectedItem;
			if ( null != eg ) { eg.loop = chb_efLoop.Checked; }
		}

		//チェックボックス：同期
		private void chkEfSync_CheckedChanged ( object sender, EventArgs e )
		{
			//エフェクトジェネレートが０なら何もしない
			Script script = editChara.EditAction.GetScript ();
			if ( 0 == script.ListGenerateEf.Count ) { return; }

			EffectGenerate eg = ( EffectGenerate ) ctrlEfGnrt.lb_EfGnrt.SelectedItem;
			if ( null != eg ) { eg.sync = chb_efSync.Checked; }
		}

		//コンボボックス：変更時
		private void CB_EffectList_SelectedIndexChanged ( object sender, EventArgs e )
		{
			EffectGenerate eg = ( EffectGenerate ) ctrlEfGnrt.lb_EfGnrt.SelectedItem;
			if ( null == eg ) { return; }
			eg.id.i = CB_EffectList.SelectedIndex;

			//バインディングリストの内容更新
			if ( null == editCompendAction ) { return; }
			Script script = editChara.EditAction.GetScript ();
			for ( int i = 0; i < script.ListGenerateEf.Count; ++i )
			{
				script.ListGenerateEf.ResetItem ( i );
			}
		}
	}
}
