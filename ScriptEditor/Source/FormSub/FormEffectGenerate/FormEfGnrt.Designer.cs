namespace ScriptEditor
{
	partial class FormEfGnrt
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose ( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose ();
			}
			base.Dispose ( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ()
		{
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.Btn_Del = new System.Windows.Forms.Button();
			this.Btn_Add = new System.Windows.Forms.Button();
			this.Cb_Gnrt = new System.Windows.Forms.CheckBox();
			this.Cb_Loop = new System.Windows.Forms.CheckBox();
			this.Cb_Sync = new System.Windows.Forms.CheckBox();
			this.Lbl_X = new System.Windows.Forms.Label();
			this.Lbl_Y = new System.Windows.Forms.Label();
			this.Lbl_Z = new System.Windows.Forms.Label();
			this.Tbn_Z = new ScriptEditor.TB_Number();
			this.Tbn_Y = new ScriptEditor.TB_Number();
			this.Tbn_X = new ScriptEditor.TB_Number();
			this.cB_SequenceList1 = new ScriptEditor.CB_SequenceList();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			this.listBox1.FormattingEnabled = true;
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new System.Drawing.Point(12, 12);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(121, 172);
			this.listBox1.TabIndex = 0;
			// 
			// Btn_Del
			// 
			this.Btn_Del.Location = new System.Drawing.Point(12, 216);
			this.Btn_Del.Name = "Btn_Del";
			this.Btn_Del.Size = new System.Drawing.Size(55, 26);
			this.Btn_Del.TabIndex = 2;
			this.Btn_Del.Text = "削除";
			this.Btn_Del.UseVisualStyleBackColor = true;
			this.Btn_Del.Click += new System.EventHandler(this.Btn_Del_Click);
			// 
			// Btn_Add
			// 
			this.Btn_Add.Location = new System.Drawing.Point(73, 216);
			this.Btn_Add.Name = "Btn_Add";
			this.Btn_Add.Size = new System.Drawing.Size(60, 26);
			this.Btn_Add.TabIndex = 3;
			this.Btn_Add.Text = "追加";
			this.Btn_Add.UseVisualStyleBackColor = true;
			this.Btn_Add.Click += new System.EventHandler(this.Btn_Add_Click);
			// 
			// Cb_Gnrt
			// 
			this.Cb_Gnrt.AutoSize = true;
			this.Cb_Gnrt.Location = new System.Drawing.Point(146, 81);
			this.Cb_Gnrt.Name = "Cb_Gnrt";
			this.Cb_Gnrt.Size = new System.Drawing.Size(48, 16);
			this.Cb_Gnrt.TabIndex = 4;
			this.Cb_Gnrt.Text = "生成";
			this.Cb_Gnrt.UseVisualStyleBackColor = true;
			this.Cb_Gnrt.CheckedChanged += new System.EventHandler(this.Cb_Gnrt_CheckedChanged);
			// 
			// Cb_Loop
			// 
			this.Cb_Loop.AutoSize = true;
			this.Cb_Loop.Location = new System.Drawing.Point(146, 103);
			this.Cb_Loop.Name = "Cb_Loop";
			this.Cb_Loop.Size = new System.Drawing.Size(53, 16);
			this.Cb_Loop.TabIndex = 5;
			this.Cb_Loop.Text = "ループ";
			this.Cb_Loop.UseVisualStyleBackColor = true;
			this.Cb_Loop.CheckedChanged += new System.EventHandler(this.Cb_Loop_CheckedChanged);
			// 
			// Cb_Sync
			// 
			this.Cb_Sync.AutoSize = true;
			this.Cb_Sync.Location = new System.Drawing.Point(146, 125);
			this.Cb_Sync.Name = "Cb_Sync";
			this.Cb_Sync.Size = new System.Drawing.Size(48, 16);
			this.Cb_Sync.TabIndex = 6;
			this.Cb_Sync.Text = "同期";
			this.Cb_Sync.UseVisualStyleBackColor = true;
			this.Cb_Sync.CheckedChanged += new System.EventHandler(this.Cb_Sync_CheckedChanged);
			// 
			// Lbl_X
			// 
			this.Lbl_X.AutoSize = true;
			this.Lbl_X.Location = new System.Drawing.Point(143, 28);
			this.Lbl_X.Name = "Lbl_X";
			this.Lbl_X.Size = new System.Drawing.Size(12, 12);
			this.Lbl_X.TabIndex = 11;
			this.Lbl_X.Text = "X";
			// 
			// Lbl_Y
			// 
			this.Lbl_Y.AutoSize = true;
			this.Lbl_Y.Location = new System.Drawing.Point(201, 28);
			this.Lbl_Y.Name = "Lbl_Y";
			this.Lbl_Y.Size = new System.Drawing.Size(12, 12);
			this.Lbl_Y.TabIndex = 11;
			this.Lbl_Y.Text = "Y";
			// 
			// Lbl_Z
			// 
			this.Lbl_Z.AutoSize = true;
			this.Lbl_Z.Location = new System.Drawing.Point(277, 28);
			this.Lbl_Z.Name = "Lbl_Z";
			this.Lbl_Z.Size = new System.Drawing.Size(12, 12);
			this.Lbl_Z.TabIndex = 11;
			this.Lbl_Z.Text = "Z";
			// 
			// Tbn_Z
			// 
			this.Tbn_Z.Location = new System.Drawing.Point(295, 25);
			this.Tbn_Z.Name = "Tbn_Z";
			this.Tbn_Z.Size = new System.Drawing.Size(33, 19);
			this.Tbn_Z.TabIndex = 10;
			this.Tbn_Z.Text = "0";
			// 
			// Tbn_Y
			// 
			this.Tbn_Y.Location = new System.Drawing.Point(219, 25);
			this.Tbn_Y.Name = "Tbn_Y";
			this.Tbn_Y.Size = new System.Drawing.Size(35, 19);
			this.Tbn_Y.TabIndex = 8;
			this.Tbn_Y.Text = "0";
			// 
			// Tbn_X
			// 
			this.Tbn_X.Location = new System.Drawing.Point(159, 25);
			this.Tbn_X.Name = "Tbn_X";
			this.Tbn_X.Size = new System.Drawing.Size(35, 19);
			this.Tbn_X.TabIndex = 7;
			this.Tbn_X.Text = "0";
			// 
			// cB_SequenceList1
			// 
			this.cB_SequenceList1.FormattingEnabled = true;
			this.cB_SequenceList1.Location = new System.Drawing.Point(12, 190);
			this.cB_SequenceList1.Name = "cB_SequenceList1";
			this.cB_SequenceList1.Size = new System.Drawing.Size(121, 20);
			this.cB_SequenceList1.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(246, 57);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 12);
			this.label1.TabIndex = 12;
			this.label1.Text = "実効Z値は/100";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(224, 81);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104, 12);
			this.label2.TabIndex = 12;
			this.label2.Text = "0.10<-前 | 後->0.90";
			// 
			// FormEfGnrt
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(340, 248);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Lbl_Z);
			this.Controls.Add(this.Lbl_Y);
			this.Controls.Add(this.Lbl_X);
			this.Controls.Add(this.Tbn_Z);
			this.Controls.Add(this.Tbn_Y);
			this.Controls.Add(this.Tbn_X);
			this.Controls.Add(this.Cb_Sync);
			this.Controls.Add(this.Cb_Loop);
			this.Controls.Add(this.Cb_Gnrt);
			this.Controls.Add(this.Btn_Add);
			this.Controls.Add(this.Btn_Del);
			this.Controls.Add(this.cB_SequenceList1);
			this.Controls.Add(this.listBox1);
			this.Name = "FormEfGnrt";
			this.Text = "EfGnrtcs";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox listBox1;
		private CB_SequenceList cB_SequenceList1;
		private System.Windows.Forms.Button Btn_Del;
		private System.Windows.Forms.Button Btn_Add;
		private System.Windows.Forms.CheckBox Cb_Gnrt;
		private System.Windows.Forms.CheckBox Cb_Loop;
		private System.Windows.Forms.CheckBox Cb_Sync;
		private TB_Number Tbn_X;
		private TB_Number Tbn_Y;
		private TB_Number Tbn_Z;
		private System.Windows.Forms.Label Lbl_X;
		private System.Windows.Forms.Label Lbl_Y;
		private System.Windows.Forms.Label Lbl_Z;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}