using System;
using System.Windows.Forms;

namespace ScriptEditor
{
	//==================================================================================
	//	Actionを受けて表示する
	//==================================================================================
	public class DispAction : DispSequence
	{
		//アクション名(メインフォーム・編集)
		public TextBox TbName { get; set; } = null;
		//アクション名(専用フォーム・表示のみ)
		public TextBox TbActionName { get; set; } = null;
		//次の遷移先
		public ComboBox CbNext { get; set; } = null;
		//アクション属性
		public ComboBox CbCategory { get; set; } = null;
		//アクション体勢
		public ComboBox CbPosture { get; set; } = null;
		//バランス値
		public TB_Number TbaBalance { get; set; } = null;


		//コンストラクタ
		public DispAction ()
		{
		}


		//コントロールコンペンドからの設定
		public void SetCtrl ( _Ctrl_Compend cc )
		{
		}

		public void SetCtrl 
		(
			TextBox tbNm, CB_SequenceList cbNx, ComboBox cbCtg, ComboBox cbPst, TB_Number tbaBlc
		)
		{
			this.TbActionName = tbNm;
			this.CbNext = cbNx;
			this.CbCategory = cbCtg;
			this.CbPosture = cbPst;
			this.TbaBalance = tbaBlc;
		}

		//キャラデータの設定
		public override void SetCharaData ( Chara ch )
		{
			// [アクション] 次アクションリスト
			CbNext.DataSource = ch.behavior.BD_Sequence.GetBindingList();

			//１以上のときは先頭を選択、0のときは飛ばす
			if ( CbNext.Items.Count > 0 ) { CbNext.SelectedIndex = 0; }

			base.SetCharaData ( ch );
		}

		//更新
		public void UpdateData ( Action action )
		{
			Disp ( action );
		}

		//表示
		public void Disp ( Action action )
		{
			//次アクション
			CbNext.SelectedItem = action;

			//バランス
//			TbaBalance.Text = action._Balance.ToString();
//			TbaBalance.refInt = action.Balance;

			//アクションカテゴリ
			CbCategory.SelectedIndex = (int)action.Category;

			base.Disp ( action );
		}
	}
}
