using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;


namespace ScriptEditor
{
	public partial class FormRoute : SubForm_Compend
	{
#if false
		//コントロール(ルート)
		EditListbox < TName > EL_Route = new EditListbox < TName > ();

		//チェック用ルートリスト
		private BindingDictionary < Route > BD_Route = new BindingDictionary<Route> ();
#endif


		//---------------------------------------------------------------------
		//シングルトン実体
		public static FormRoute Inst { get; } = new FormRoute ();

		//プライベートコンストラクタ
		private FormRoute ()
		{
			InitializeComponent ();
			base.InitPt = new Point ( 0, 0 );
			base.LoadObject ();


#if false

			//==============================================================
			//エディットリストボックスの設定

			//----------------------------------
			//コントロール(ルート)
			EL_Route.Location = new Point ( 3, -70 );
			this.Controls.Add ( EL_Route );
			EL_Route.IO_Visible ( false );
			EL_Route.TbName_Off ();

			//追加時
			EL_Route.Listbox_Add = ()=>
			{
#if false
				//対象の先頭を指定
				if ( 0 < BD_Route.Count() )
				{
					Route rut = BD_Route.Get ( 0 );
					//追加しているので必ず１個以上存在する
					EL_Route.BD_T.Get(0).Name = rut.Name;
					EL_Route.ResetItems ();
				}
#endif

			};
			//選択変更時
			EL_Route.SelectedIndexChanged = ()=>
			{
				if ( BD_Route.ContainsKey ( EL_Route.Get ().Name ) )
				{
					Cb_Route.SelectedValue = EL_Route.Get ().Name;
				}
				else
				{
					Cb_Route.SelectedIndex = -1;
				}
			};
			EL_Route.Func_color_check = (ob)=>
			{
				//ルート名が存在するかどうか
				return ! BD_Route.ContainsKey ( ((TName)ob).Name );
			};
			//==============================================================

			Cb_Route.DisplayMember = "Name";
			Lb_Copy.DisplayMember = "Name";
#endif
		}

		//---------------------------------------------------------------------
		//コントロール
		private Ctrl_Scp_Route Ctrl_Scp_Route = new Ctrl_Scp_Route ();	//仮オブジェクト

		//FormMainで実体を確保し、設置する
		public void SetCtrl ( Ctrl_Scp_Route ctrl )
		{
			Ctrl_Scp_Route = ctrl;
			this.Controls.Add ( Ctrl_Scp_Route );
		}


		//データ設定
		public void SetCharaData ( Chara ch )
		{
#if false
			//コンボボックスに登録
			Cb_Route.DataSource = ch.BD_Route.GetBindingList ();
			Cb_Route.DisplayMember = "Name";
			Cb_Route.ValueMember = "Name";

			//チェック用保存
			BD_Route = ch.BD_Route;
#endif
			Ctrl_Scp_Route.SetCharaData ( ch );
		}

		//環境設定
#if false
		public void SetEnvironment ( EditCompend ec )
		{
			EditCompend = ec;
		}
#endif

		//コンペンド編集の切り替え
		public override void SetEditCompend ( EditCompend ec )
		{
			Ctrl_Scp_Route.SetEnvironment ( ec );
			base.SetEditCompend ( ec );
		}

#if false
		//関連付け
		public Script Script { get; set; } = new Script ();
		public void Assosiate ( Script scp )
		{
			Script = scp;
			EL_Route.SetData ( scp.BD_RutName );
		}

		//コンボボックスで選択
		private void Cb_Route_SelectionChangeCommitted ( object sender, EventArgs e )
		{
			Route rut = (Route)Cb_Route.SelectedItem;
			EL_Route.Get ().Name = rut.Name;
			EL_Route.ResetItems ();
		}

		//一時コピー
		private void Btn_Copy_Click ( object sender, EventArgs e )
		{
			ListBox lb = EL_Route.GetListBox ();
			Lb_Copy.Items.Clear ();
			foreach ( object ob in lb.Items )
			{
				Lb_Copy.Items.Add ( (TName)ob );
			}
		}

		//グループにペースト
		private void Btn_PasteGroup_Click ( object sender, EventArgs e )
		{
			EditScript es = EditCompend.EditScript;
			es.DoGroup ( scp => SetBD_Route ( scp ) );
		}

		//スクリプトにコピーしているルートリストを設定
		private void SetBD_Route ( Script scp )
		{
			scp.BD_RutName.Clear ();
			foreach ( TName tn in Lb_Copy.Items )
			{
				scp.BD_RutName.Add ( tn );
			}
		}

		//全体にペースト
		private void Btn_PastAll_Click ( object sender, EventArgs e )
		{
			EditCompend.DoAllScript ( scp=>SetBD_Route ( scp ) );
		}
#endif
	}
}
