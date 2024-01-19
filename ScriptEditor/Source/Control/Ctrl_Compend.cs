using System.Windows.Forms;
using System.ComponentModel;

namespace ScriptEditor
{

#if false

	using BD_Sqc = BindingDictionary < Sequence >;
	using BL_Sqc = BindingList < Sequence >;

	//==================================================================================
	//	Compendを扱うコントロールの集合
	//		アクションもしくはエフェクトの編集と表示を参照する
	//		外部からSetCtrl()でビヘイビアとガーニッシュのいずれかを指定する
	//==================================================================================
	public partial class Ctrl_Compend : UserControl
	{
		public EditCompend EditCompend { get; set; } = null;

		//アクションのときtrue, エフェクトのときfalse
		public bool BoolAction { get; set; } = false;

		//-------------------------------------------------------------------------
		//コンストラクタ
		public Ctrl_Compend ()
		{
			InitializeComponent ();

			sequenceTree1.CtrlCompend = this;
		}

		//初期化
		public void SetEnviron ( EditCompend ec, DispCompend dc )
		{
			EditCompend = ec;
			sqcBoard1.SetEnviron ( ec, dc );
			Ctrl_Img.SetEnviron ( ec, dc );
			sequenceTree1.SetCtrl ( ec, dc );
		}

		public void SetBehavior ()
		{
			BoolAction = true;
			Btn_Sqc.Text = "アクション";
			Btn_Sqc.Enabled = true;
		}

		public void SetGarnish ()
		{
			BoolAction = false;
			Btn_Sqc.Text = "エフェクト";
			Btn_Sqc.Enabled = false;
		}

		public SequenceTree GetSequenceTree ()
		{
			return sequenceTree1;
		}

		//読込時キャラデータの設定
		public void SetCharaData ( Chara ch )
		{
			Behavior b = ch.behavior;
			Garnish g = ch.garnish;
			if ( BoolAction )
			{
				Ctrl_Img.SetCharaData ( b.BD_Image, g.BD_Sequence, g.BD_Image );
				sequenceTree1.SetCharaData ( b.BD_Sequence );
			}
			else
			{
				Ctrl_Img.SetCharaData ( g.BD_Image, g.BD_Sequence, g.BD_Image );
				sequenceTree1.SetCharaData ( g.BD_Sequence );
			}

			FormAction.Inst.SetCharaData ( ch );

			SelectTarget ();
		}


		public void SelectTarget ()
		{
			//選択
			Sequence sqc = EditCompend.SelectedSequence;
			sqcBoard1.SetData ( sqc );

			Assosiate ();
		}

		//先頭を選択
		public void SelectTop ()
		{
			if ( 0 < EditCompend.Compend.BD_Sequence.Count () )
			{
				sequenceTree1.SelectTop ();
				sequenceTree1.Focus ();
			}
		}

		//関連付け
		public void Assosiate ()
		{
			EditCompend.Assosiate ();

			//--------------------------------------------------------
			//シークエンス
			Sequence sqc = EditCompend.SelectedSequence;
			sqcBoard1.Assosiate ( sqc );

			//アクションとエフェクトで分岐
			if ( BoolAction )
			{
				FormAction.Inst.Assosiate ( ( Action ) sqc );
			}

			//--------------------------------------------------------
			//スクリプト
			Script scp = EditCompend.SelectedScript;
			FormScript.Inst.Assosiate ( scp, sqc );	//各値
			FormRect2.Inst.Assosiate ( scp );	//枠
			FormEfGnrt.Inst.Assosiate ( scp );	//Ef生成
			FormRoute.Inst.Assosiate ( scp );	//ルート
			//--------------------------------------------------------
		}

		//更新
		public void UpdateData ()
		{
			BD_Sqc bdsqc = EditCompend.Compend.BD_Sequence;
			bdsqc.ResetItems ();
			sequenceTree1.UpdateCategory ( bdsqc.GetBindingList () );

			Assosiate ();
		}

		//-------------------------------------------------------------------------
		//[アクション/エフェクト] (シークエンス) ボタン
		private void Btn_Sqc_Click ( object sender, System.EventArgs e )
		{
			FormAction.Inst.Active ();
		}
		private void Btn_HideSqc_Click ( object sender, System.EventArgs e )
		{
			FormAction.Inst.Hidden ();
		}

		//-------------------------------------------------------------------------
		//スクリプト(リスト)　ボタン
		private void Btn_ScriptList_Click ( object sender, System.EventArgs e )
		{
			Form_ScriptList.Inst.Active ();
		}
		private void Btn_HideScpLs_Click ( object sender, System.EventArgs e )
		{
			Form_ScriptList.Inst.Hidden ();
		}
		//-------------------------------------------------------------------------
		//スクリプト(値) ボタン
		private void Btn_Scp_Click ( object sender, System.EventArgs e )
		{
			FormScript.Inst.Active ();
		}
		private void Btn_HideScpPrm_Click ( object sender, System.EventArgs e )
		{
			FormScript.Inst.Hidden ();
		}

		//-------------------------------------------------------------------------
		//イメージ
		private void Btn_Image_Click ( object sender, System.EventArgs e )
		{
			FormImage.Inst.Active ();
		}
		private void Btn_HideImage_Click ( object sender, System.EventArgs e )
		{
			FormImage.Inst.Hidden ();
		}

		//-------------------------------------------------------------------------
		//判定枠
		private void Btn_Rect_Click ( object sender, System.EventArgs e )
		{
//			FormRect.Inst.Active ();
			FormRect2.Inst.Active ();
		}
		private void Btn_HideRect_Click ( object sender, System.EventArgs e )
		{
//			FormRect.Inst.Hidden ();
			FormRect2.Inst.Hidden ();
		}

		//-------------------------------------------------------------------------
		//Ef生成
		private void Btn_EfGnrt_Click ( object sender, System.EventArgs e )
		{
			FormEfGnrt.Inst.Active ();
		}
		private void Btn_HideEfGnrt_Click ( object sender, System.EventArgs e )
		{
			FormEfGnrt.Inst.Hidden ();
		}

		//-------------------------------------------------------------------------
		//ルート
		private void Btn_Route_Click ( object sender, System.EventArgs e )
		{
			FormRoute.Inst.Active ();
		}
		private void Btn_HideRut_Click ( object sender, System.EventArgs e )
		{
			FormRoute.Inst.Hidden ();
		}

		//-------------------------------------------------------------------------
		//プレビュー
		private void Btn_Preview_Click ( object sender, System.EventArgs e )
		{
			FormPreview.Inst.VisFlip ();
			FormPreview.Inst.Start ();
		}

	}




#endif
}
