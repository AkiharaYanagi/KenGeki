using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace ScriptEditor
{
	using LsRect = List < Rectangle >;
	using GS_LsRect = System.Action < List < Rectangle > >;

	//---------------------------------------------------------------------
	//	枠リストに対するコントロールをまとめるクラス
	//---------------------------------------------------------------------
	public partial class Ctrl_ListRect : UserControl
	{
		//対象枠リスト
		private LsRect LsRect = null;

		//編集
		public EditScript EditScript = null;

		//コンストラクタ
		public Ctrl_ListRect ()
		{
			InitializeComponent ();
		}

		//関連付け
		public void Assosiate ( LsRect lsRect )
		{
			LsRect = lsRect;

			//個数のチェック
			SetCount ( lsRect.Count );
			if ( 0 >= lsRect.Count ) { return; }

			int selectedIndex = (int)numericUpDown1.Value;
			SetValue ( lsRect [ selectedIndex ] );
		}

		//更新
		public void UpdateData ()
		{
			if ( null == LsRect ) { return; }
			SetCount ( LsRect.Count );
			if ( 1 > LsRect.Count ) { return; }

			int selectedIndex = (int)numericUpDown1.Value;
			Rectangle r = LsRect [ selectedIndex ];
			SetValue ( r );
		}

		public void SetName ( string name )
		{
			Lbl_RectName.Text = name;
		}

		//個数によって表示非表示を切替
		public void SetCount ( int count )
		{
			string str = (count < 1) ? "-" : (count - 1).ToString ();
			Lbl_N.Text = "/" + str;
			numericUpDown1.Maximum = (count < 1) ? 0 : count - 1;
			numericUpDown1.Enabled = ( 0 < count );
			Btn_Del.Enabled = ( 0 < count );
			if ( 0 == count ) { SetZero (); }
		}

		public void SetValue ( Rectangle r )
		{
			Tbn_X.Text = r.X.ToString ();
			Tbn_Y.Text = r.Y.ToString ();
			Tbn_W.Text = r.Width.ToString ();
			Tbn_H.Text = r.Height.ToString ();
		}

		public void SetZero ()
		{
			SetValue ( new Rectangle ( 0, 0, 0, 0 ) );
		}

		public Rectangle GetRect ()
		{
			if ( null == LsRect ) { return new Rectangle (); }
			if ( 1 > LsRect.Count ) { return new Rectangle (); }

			int selectedIndex = (int)numericUpDown1.Value;
			if ( selectedIndex < 0 || LsRect.Count < selectedIndex ) { return new Rectangle (); }

			return LsRect [ selectedIndex ];
		}

		public void SetRect ( Rectangle r )
		{
			if ( null == LsRect ) { return; }
			if ( 1 > LsRect.Count ) { return; }

			int selectedIndex = (int)numericUpDown1.Value;
			if ( selectedIndex < 0 || LsRect.Count < selectedIndex ) { return; }

			LsRect [ selectedIndex ] = r;

			GroupSetter?.Invoke ( LsRect );
		}

		private void numericUpDown1_ValueChanged ( object sender, System.EventArgs e )
		{
			UpdateData ();
			DispChara.Inst.Disp ();
		}

		private void Btn_Add_Click ( object sender, System.EventArgs e )
		{
			LsRect.Add ( new Rectangle () );
			SetCount ( LsRect.Count );
			DispChara.Inst.Disp ();
		}

		private void Btn_Del_Click ( object sender, System.EventArgs e )
		{
			if ( 1 > LsRect.Count ) { return; }

			LsRect.RemoveAt ( LsRect.Count - 1 );
			SetCount ( LsRect.Count );
			DispChara.Inst.Disp ();
		}

		//-------------------------------------------------------------------------------
		//グループへの変更はリストのコピーを行う
		public GS_LsRect GroupSetter { get; set; } = null;

	}
}
