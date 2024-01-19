using System;
using System.Windows.Forms;

namespace ScriptEditor
{
	public partial class FormMain : Form
	{
		//==================================================================================
		//	キャラ初期設定(の選択)
		//==================================================================================
		private void EstablishChara ()
		{
			//-------------------------------
			//@info 下記いずれかを環境で選ぶ
			//-------------------------------

#if false
			//キャラ：空データ
			SetCharaData ( chara );

			//キャラ：テストデータ
			TestCharaData ();
#endif
#if false

			//個別データから作成
			SetCharaData ( chara );
			LoadPreData ();
#endif

			//自動で既存のキャラデータを読込
			_LoadChara ( stgs.LastFilepath );
			//イメージのみ追加読込
			All_Ctrl.Inst.LoadImage ();
		}

		//プレデータテキストファイルから既存データの読込
		public void LoadPreData ()
		{
			All_Ctrl.Inst.LoadPreData ();
		}

		//==================================================================================
		//各コントロールのデータソースにキャラデータを設定する (読込時に１回のみ)
		//==================================================================================
		public void SetCharaData ( Chara ch )
		{
			//----------------------------------------------
			EditChara.Inst.SetCharaDara ( ch );			//編集
			All_Ctrl.Inst.SetCharaData ( ch );

			//----------------------------------------------
			_FormEfGnrt.Inst.SetCharaData ( ch );	// [エフェクトジェネレートフォーム]
			FormRoute.Inst.SetCharaData ( ch );		//フォーム ルート

			//----------------------------------------------
			UpdateData ();			// [メインフォーム] キャラ内容表示
		}

	}
}
