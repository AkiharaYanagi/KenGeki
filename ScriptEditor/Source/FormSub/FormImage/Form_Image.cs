using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;


namespace ScriptEditor
{
	using BD_ImgDt = BindingDictionary < ImageData >;

	//-------------------------------------------------------------
	//	イメージ選択フォーム
	//-------------------------------------------------------------
	public sealed partial class FormImage : SubForm_Compend
	{
		//---------------------------------------------------------------------
		//シングルトン実体
		public static FormImage Inst { get; } = new FormImage ();

		//プライベートコンストラクタ
		private FormImage ()
		{
			this.Location = Cursor.Position;

			InitializeComponent ();
			base.LoadObject ();

			Lb_Image.DisplayMember = "Name";
			Lb_Image.ValueMember = "Image";
		}
		//---------------------------------------------------------------------

		// イメージリスト参照
		private BD_ImgDt BD_ImageData { get; set; } = new BD_ImgDt ();

		// グループ編集用
//		public ScriptParam < int > ScriptSetter { get; set; } = null;

		//編集参照
//		public EditCompend EditCompend { get; set; } = null;
//		public DispCompend DispCompend { get; set; } = null;

		//---------------------------------------------------------------------
		//選択中イメージのインデックス
//		public int GetImageIndex () { return Lb_Image.SelectedIndex; }
		//選択中イメージの名前
		public string GetImageName () { return ( (ImageData)Lb_Image.SelectedItem ).Name; }

#if false
		//環境を設定
		public void SetEnviron ( EditCompend ec )
		{
//			EditCompend = ec;
//			DispCompend = dc;
		}
#endif

		//対象を設定
		public void SetCharaData ( BD_ImgDt bd_imgDt )
		{
			BD_ImageData = bd_imgDt;

			//リストボックスに反映
			Lb_Image.DataSource = BD_ImageData.GetBindingList ();
		}

		public void UpdateData ()
		{
			BD_ImageData.ResetItems ();
		}

		//リストボックスの選択位置変更時
		private void listBox1_SelectedIndexChanged ( object sender, EventArgs e )
		{
			//選択されていないとき何もしない
			if ( null == Lb_Image.SelectedItem ) { return; }

			//画像の更新
			pbArchiveImage.Image = ( ( ImageData ) Lb_Image.SelectedItem ).Img;
		}

		//キャンセル
		private void Btn_Cancel_Click ( object sender, EventArgs e )
		{
			this.Close ();			//何もせずに終了
		}

		//OK
		private void Btn_ArchiveOK_Click ( object sender, EventArgs e )
		{
			//選択中スクリプトにイメージの設定
			Script scp = EditCompend.SelectedScript;
			scp.ImgName = GetImageName ();
			
			//グループにも変更を反映
//			EditCompend.EditScript.GroupSetterImageIndex ( scp.ImgIndex );

			//全体描画
			//DispCompend.Disp ();
			//Ctrl_All.Inst.AllDisp ();
			//All_Ctrl.Inst.Disp ();
			All_Ctrl.Inst.UpdateData ();

			this.Hide ();
		}

		private void 読込ToolStripMenuItem_Click ( object sender, EventArgs e )
		{
			//複数のファイルを選択できるようにする
			openFileDialog1.Multiselect = true;
			openFileDialog1.FileName = "";

			if ( openFileDialog1.ShowDialog () == DialogResult.OK )
			{
				LoadFiles ( openFileDialog1.FileNames );
				//DispCompend.Disp ();
				//Ctrl_All.Inst.AllDisp ();
				All_Ctrl.Inst.Disp ();
			}
		}

		private void 全削除ToolStripMenuItem_Click ( object sender, EventArgs e )
		{
			//個数が０のときは何もしない
			if ( Lb_Image.Items.Count == 0 ) { return; }

			//未選択時は何もしない
//			if ( Lb_Image.SelectedItem == null ) { return; }

			//全イメージを削除
			BD_ImageData.Clear ();

			//表示をクリア
			pbArchiveImage.Image = null;
		}

		private void FormImage_VisibleChanged ( object sender, EventArgs e )
		{
			//フォーム位置を親フォームの右端にする
			int x = FormMain.Location.X + FormMain.Width;
			int y = FormMain.Location.Y;
			this.Location = new Point ( x, y );
		}

		private void フォルダ読込ToolStripMenuItem_Click ( object sender, EventArgs e )
		{
			OpenFolder_CodePack opf = new OpenFolder_CodePack ();
			string crDir = Directory.GetCurrentDirectory ();
			string upDir = crDir.Substring ( 0, crDir.LastIndexOf ( @"\" ) + 1 );
			opf.SetInitDir ( crDir );
			opf.SetDefaultFilename ( "out" );

			if ( opf.OpenFolder () )
			{
				string[] files = Directory.GetFiles ( opf.GetPath () );
				LoadFiles ( files );
			}
		}

		private void LoadFiles ( string[] files )
		{
			foreach ( string filename in files )
			{
				ImageData imageData = null;	//参照
				try
				{
					string f = Path.GetFileName ( filename );
					//string fn = f.Substring ( 4 );	//先頭のインデックス("ddd_")を除く

					//画像からImageData型を作成
					imageData = new ImageData ( f, Image.FromFile ( filename ) );

					//L_ImageData.Add ( imageData );			//内部データに保存
					BD_ImageData.Add ( imageData );
					
					pbArchiveImage.Image = imageData.Img;			//プレビューにImageを表示
					Lb_Image.SelectedIndex = Lb_Image.Items.Count - 1;		//末尾を選択
				}
				catch ( System.OutOfMemoryException ex )
				{
					//ファイル形式が有効でないとき
					MessageBox.Show ( "ファイル形式が無効です。\n" + ex.Message );
					return;
				}
			}
		}
	}
}
