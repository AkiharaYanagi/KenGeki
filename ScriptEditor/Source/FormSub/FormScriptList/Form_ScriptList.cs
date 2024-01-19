using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace ScriptEditor
{
	using L_Scp = List < Script >;


	//-------------------------------------------------------------
	//	スクリプトリスト操作フォーム
	//-------------------------------------------------------------
	public sealed class Form_ScriptList : SubForm_Compend
	{
		private Button Btn_Copy;
		private Button Btn_Save;
		private TextBox textBox1;
		private GroupBox groupBox1;
		private Button Btn_Replace;
		private Button Btn_Load;

		//---------------------------------------------------------------------
		//シングルトン実体
		public static Form_ScriptList Inst { get; } = new Form_ScriptList ();

		//プライベートコンストラクタ
		private Form_ScriptList ()
		{
			InitializeComponent ();
			LoadObject ();

			textBox1.Text = "";
			Btn_Replace.Enabled = false;
		}
		//---------------------------------------------------------------------

		private void InitializeComponent ()
		{
			this.Btn_Copy = new System.Windows.Forms.Button();
			this.Btn_Save = new System.Windows.Forms.Button();
			this.Btn_Load = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.Btn_Replace = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// Btn_Copy
			// 
			this.Btn_Copy.Location = new System.Drawing.Point(16, 23);
			this.Btn_Copy.Name = "Btn_Copy";
			this.Btn_Copy.Size = new System.Drawing.Size(130, 31);
			this.Btn_Copy.TabIndex = 1;
			this.Btn_Copy.Text = "-> コピー";
			this.Btn_Copy.UseVisualStyleBackColor = true;
			this.Btn_Copy.Click += new System.EventHandler(this.Btn_Copy_Click);
			// 
			// Btn_Save
			// 
			this.Btn_Save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.Btn_Save.Location = new System.Drawing.Point(12, 12);
			this.Btn_Save.Name = "Btn_Save";
			this.Btn_Save.Size = new System.Drawing.Size(130, 31);
			this.Btn_Save.TabIndex = 1;
			this.Btn_Save.Text = "保存";
			this.Btn_Save.UseVisualStyleBackColor = false;
			this.Btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
			// 
			// Btn_Load
			// 
			this.Btn_Load.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.Btn_Load.Location = new System.Drawing.Point(161, 12);
			this.Btn_Load.Name = "Btn_Load";
			this.Btn_Load.Size = new System.Drawing.Size(130, 31);
			this.Btn_Load.TabIndex = 1;
			this.Btn_Load.Text = "読込";
			this.Btn_Load.UseVisualStyleBackColor = false;
			this.Btn_Load.Click += new System.EventHandler(this.Btn_Load_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(16, 76);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(130, 19);
			this.textBox1.TabIndex = 2;
			this.textBox1.Text = "Name[Num]";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.Btn_Replace);
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Controls.Add(this.Btn_Copy);
			this.groupBox1.Location = new System.Drawing.Point(12, 49);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(290, 276);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "複製";
			// 
			// Btn_Replace
			// 
			this.Btn_Replace.Location = new System.Drawing.Point(160, 76);
			this.Btn_Replace.Name = "Btn_Replace";
			this.Btn_Replace.Size = new System.Drawing.Size(119, 37);
			this.Btn_Replace.TabIndex = 3;
			this.Btn_Replace.Text = "リプレイス";
			this.Btn_Replace.UseVisualStyleBackColor = true;
			this.Btn_Replace.Click += new System.EventHandler(this.Btn_Replace_Click);
			// 
			// Form_ScriptList
			// 
			this.ClientSize = new System.Drawing.Size(318, 336);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.Btn_Load);
			this.Controls.Add(this.Btn_Save);
			this.Name = "Form_ScriptList";
			this.Text = "ScriptList";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		//---------------------------------------------------------------------

		public void SetEnvironment ( EditCompend ec )
		{
			EditCompend = ec;
		}

		//書出
		private void Btn_Save_Click ( object sender, EventArgs e )
		{
			using ( SaveFileDialog sfd = new SaveFileDialog () )
			{
				sfd.DefaultExt = "txt";
				sfd.InitialDirectory = Directory.GetCurrentDirectory ();
				sfd.FileName = "ScriptList.txt";

				if ( sfd.ShowDialog () == DialogResult.OK )
				{
					using ( StreamWriter sw = new StreamWriter ( sfd.FileName, false, Encoding.UTF8 ) )
					{
						Chara ch = EditChara.Inst.Chara;
						CharaToDoc ctod = new CharaToDoc ();
						ctod.WriteListScript ( ch, sw, EditCompend.Compend, EditCompend.SelectedSequence.ListScript ) ;
					}
				}
			}
			
		}

		//読込
		private void Btn_Load_Click ( object sender, EventArgs e )
		{
			using ( OpenFileDialog ofd = new OpenFileDialog () )
			{
				ofd.DefaultExt = "txt";
				ofd.InitialDirectory = Directory.GetCurrentDirectory ();
				ofd.FileName = "ScriptList.txt";

				if ( ofd.ShowDialog () == DialogResult.OK )
				{
					using ( StreamReader sr = new StreamReader ( ofd.FileName, Encoding.UTF8 ) )
					{
						Document doc = new Document ( sr.BaseStream );
						DocToChara dtoc = new DocToChara ();
						Sequence sqc = new Sequence ();
						dtoc.ReadScriptList ( sqc, doc.Root.Elements );

						//選択
						string name = EditCompend.SelectedSequence.Name;
						EditCompend.SelectedSequence.CopyScpList ( sqc );
						EditCompend.SelectSequence ( name );
						
						//表示の更新
						//DispChara.Inst.Disp ();
					}
				}
			}
		}

		//コピー
		private L_Scp L_Script = new L_Scp ();
		private void Btn_Copy_Click ( object sender, EventArgs e )
		{
			L_Scp ls = EditCompend.SelectedSequence.ListScript;
			L_Script = new L_Scp ();
			foreach ( Script scp in ls )
			{
				L_Script.Add ( new Script ( scp ) );
			}

			textBox1.Text = EditCompend.SelectedSequence.Name + "[" + ls.Count.ToString () + "]";
			Btn_Replace.Enabled = true;
		}

		//リプレイス
		private void Btn_Replace_Click ( object sender, EventArgs e )
		{
			if ( textBox1.Text == "" ) { return; }

			L_Scp ls = EditCompend.SelectedSequence.ListScript;
			ls.Clear ();
			foreach ( Script scp in L_Script )
			{
				ls.Add ( new Script ( scp ) );
			}

			//選択
			EditCompend.SelectSequence ( EditCompend.SelectedSequence.Name );
			
			//表示更新
			//DispChara.Inst.Disp ();
			All_Ctrl.Inst.UpdateData ();
		}
	}
}
