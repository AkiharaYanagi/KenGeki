namespace ScriptEditor
{
	partial class FormAction
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
			this.Lbl_Name = new System.Windows.Forms.Label();
			this.Lbl_Next = new System.Windows.Forms.Label();
			this.Lbl_Category = new System.Windows.Forms.Label();
			this.Lbl_HitNum = new System.Windows.Forms.Label();
			this.Lbl_Posture = new System.Windows.Forms.Label();
			this.TB_Name = new System.Windows.Forms.TextBox();
			this.CB_Category = new System.Windows.Forms.ComboBox();
			this.CB_Posture = new System.Windows.Forms.ComboBox();
			this.CBSL_Next = new ScriptEditor.CB_SequenceList();
			this.TBN_HitNum = new ScriptEditor.TB_Number();
			this.Lbl_HitPitch = new System.Windows.Forms.Label();
			this.Tbn_HitPitch = new ScriptEditor.TB_Number();
			this.label1 = new System.Windows.Forms.Label();
			this.Tbn_Balance = new ScriptEditor.TB_Number();
			this.SuspendLayout();
			// 
			// Lbl_Name
			// 
			this.Lbl_Name.AutoSize = true;
			this.Lbl_Name.Location = new System.Drawing.Point(12, 12);
			this.Lbl_Name.Name = "Lbl_Name";
			this.Lbl_Name.Size = new System.Drawing.Size(17, 12);
			this.Lbl_Name.TabIndex = 1;
			this.Lbl_Name.Text = "名";
			// 
			// Lbl_Next
			// 
			this.Lbl_Next.AutoSize = true;
			this.Lbl_Next.Location = new System.Drawing.Point(12, 49);
			this.Lbl_Next.Name = "Lbl_Next";
			this.Lbl_Next.Size = new System.Drawing.Size(17, 12);
			this.Lbl_Next.TabIndex = 2;
			this.Lbl_Next.Text = "次";
			// 
			// Lbl_Category
			// 
			this.Lbl_Category.AutoSize = true;
			this.Lbl_Category.Location = new System.Drawing.Point(12, 86);
			this.Lbl_Category.Name = "Lbl_Category";
			this.Lbl_Category.Size = new System.Drawing.Size(39, 12);
			this.Lbl_Category.TabIndex = 3;
			this.Lbl_Category.Text = "カテゴリ";
			// 
			// Lbl_HitNum
			// 
			this.Lbl_HitNum.AutoSize = true;
			this.Lbl_HitNum.Location = new System.Drawing.Point(12, 160);
			this.Lbl_HitNum.Name = "Lbl_HitNum";
			this.Lbl_HitNum.Size = new System.Drawing.Size(40, 12);
			this.Lbl_HitNum.TabIndex = 4;
			this.Lbl_HitNum.Text = "ヒット数";
			// 
			// Lbl_Posture
			// 
			this.Lbl_Posture.AutoSize = true;
			this.Lbl_Posture.Location = new System.Drawing.Point(12, 123);
			this.Lbl_Posture.Name = "Lbl_Posture";
			this.Lbl_Posture.Size = new System.Drawing.Size(29, 12);
			this.Lbl_Posture.TabIndex = 5;
			this.Lbl_Posture.Text = "体勢";
			// 
			// TB_Name
			// 
			this.TB_Name.Location = new System.Drawing.Point(69, 9);
			this.TB_Name.Name = "TB_Name";
			this.TB_Name.Size = new System.Drawing.Size(219, 19);
			this.TB_Name.TabIndex = 6;
			this.TB_Name.TextChanged += new System.EventHandler(this.TB_Name_TextChanged);
			// 
			// CB_Category
			// 
			this.CB_Category.FormattingEnabled = true;
			this.CB_Category.Location = new System.Drawing.Point(69, 83);
			this.CB_Category.Name = "CB_Category";
			this.CB_Category.Size = new System.Drawing.Size(220, 20);
			this.CB_Category.TabIndex = 8;
			this.CB_Category.SelectedIndexChanged += new System.EventHandler(this.CB_Category_SelectedIndexChanged);
			this.CB_Category.SelectionChangeCommitted += new System.EventHandler(this.CB_Category_SelectionChangeCommitted);
			// 
			// CB_Posture
			// 
			this.CB_Posture.FormattingEnabled = true;
			this.CB_Posture.Location = new System.Drawing.Point(69, 120);
			this.CB_Posture.Name = "CB_Posture";
			this.CB_Posture.Size = new System.Drawing.Size(219, 20);
			this.CB_Posture.TabIndex = 9;
			this.CB_Posture.SelectedIndexChanged += new System.EventHandler(this.CB_Posture_SelectedIndexChanged);
			// 
			// CBSL_Next
			// 
			this.CBSL_Next.FormattingEnabled = true;
			this.CBSL_Next.Location = new System.Drawing.Point(69, 44);
			this.CBSL_Next.Name = "CBSL_Next";
			this.CBSL_Next.SetFunc = s=>{};
			this.CBSL_Next.Size = new System.Drawing.Size(218, 20);
			this.CBSL_Next.TabIndex = 10;
			// 
			// TBN_HitNum
			// 
			this.TBN_HitNum.GetFunc = null;
			this.TBN_HitNum.Location = new System.Drawing.Point(69, 157);
			this.TBN_HitNum.Name = "TBN_HitNum";
			this.TBN_HitNum.SetFunc = null;
			this.TBN_HitNum.Size = new System.Drawing.Size(100, 19);
			this.TBN_HitNum.TabIndex = 11;
			this.TBN_HitNum.Text = "0";
			// 
			// Lbl_HitPitch
			// 
			this.Lbl_HitPitch.AutoSize = true;
			this.Lbl_HitPitch.Location = new System.Drawing.Point(12, 197);
			this.Lbl_HitPitch.Name = "Lbl_HitPitch";
			this.Lbl_HitPitch.Size = new System.Drawing.Size(52, 12);
			this.Lbl_HitPitch.TabIndex = 4;
			this.Lbl_HitPitch.Text = "ヒット間隔";
			// 
			// Tbn_HitPitch
			// 
			this.Tbn_HitPitch.GetFunc = null;
			this.Tbn_HitPitch.Location = new System.Drawing.Point(69, 194);
			this.Tbn_HitPitch.Name = "Tbn_HitPitch";
			this.Tbn_HitPitch.SetFunc = null;
			this.Tbn_HitPitch.Size = new System.Drawing.Size(100, 19);
			this.Tbn_HitPitch.TabIndex = 11;
			this.Tbn_HitPitch.Text = "0";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(17, 232);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 12;
			this.label1.Text = "バランス";
			// 
			// Tbn_Balance
			// 
			this.Tbn_Balance.GetFunc = null;
			this.Tbn_Balance.Location = new System.Drawing.Point(69, 229);
			this.Tbn_Balance.Name = "Tbn_Balance";
			this.Tbn_Balance.SetFunc = null;
			this.Tbn_Balance.Size = new System.Drawing.Size(100, 19);
			this.Tbn_Balance.TabIndex = 11;
			this.Tbn_Balance.Text = "0";
			// 
			// FormAction
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(338, 267);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Tbn_Balance);
			this.Controls.Add(this.Tbn_HitPitch);
			this.Controls.Add(this.TBN_HitNum);
			this.Controls.Add(this.CBSL_Next);
			this.Controls.Add(this.CB_Posture);
			this.Controls.Add(this.CB_Category);
			this.Controls.Add(this.TB_Name);
			this.Controls.Add(this.Lbl_Posture);
			this.Controls.Add(this.Lbl_HitPitch);
			this.Controls.Add(this.Lbl_HitNum);
			this.Controls.Add(this.Lbl_Category);
			this.Controls.Add(this.Lbl_Next);
			this.Controls.Add(this.Lbl_Name);
			this.Name = "FormAction";
			this.Text = "Action";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label Lbl_Name;
		private System.Windows.Forms.Label Lbl_Next;
		private System.Windows.Forms.Label Lbl_Category;
		private System.Windows.Forms.Label Lbl_HitNum;
		private System.Windows.Forms.Label Lbl_Posture;
		public System.Windows.Forms.TextBox TB_Name;
		public System.Windows.Forms.ComboBox CB_Category;
		public System.Windows.Forms.ComboBox CB_Posture;
		public CB_SequenceList CBSL_Next;
		private TB_Number TBN_HitNum;
		private System.Windows.Forms.Label Lbl_HitPitch;
		private TB_Number Tbn_HitPitch;
		private System.Windows.Forms.Label label1;
		private TB_Number Tbn_Balance;
	}
}