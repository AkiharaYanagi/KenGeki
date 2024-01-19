namespace ScriptEditor
{
	partial class FormRoute
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
#if false
			this.Cb_Route = new System.Windows.Forms.ComboBox();
			this.Btn_PasteGroup = new System.Windows.Forms.Button();
			this.Btn_PastAll = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.Lb_Copy = new System.Windows.Forms.ListBox();
			this.Btn_Copy = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
#endif
			this.SuspendLayout();
#if false
			// 
			// Cb_Route
			// 
			this.Cb_Route.FormattingEnabled = true;
			this.Cb_Route.Location = new System.Drawing.Point(237, 48);
			this.Cb_Route.Name = "Cb_Route";
			this.Cb_Route.Size = new System.Drawing.Size(202, 20);
			this.Cb_Route.TabIndex = 0;
			this.Cb_Route.SelectionChangeCommitted += new System.EventHandler(this.Cb_Route_SelectionChangeCommitted);
			// 
			// Btn_PasteGroup
			// 
			this.Btn_PasteGroup.Location = new System.Drawing.Point(149, 82);
			this.Btn_PasteGroup.Name = "Btn_PasteGroup";
			this.Btn_PasteGroup.Size = new System.Drawing.Size(128, 33);
			this.Btn_PasteGroup.TabIndex = 1;
			this.Btn_PasteGroup.Text = "グループにペースト ->";
			this.Btn_PasteGroup.UseVisualStyleBackColor = true;
			this.Btn_PasteGroup.Click += new System.EventHandler(this.Btn_PasteGroup_Click);
			// 
			// Btn_PastAll
			// 
			this.Btn_PastAll.Location = new System.Drawing.Point(149, 146);
			this.Btn_PastAll.Name = "Btn_PastAll";
			this.Btn_PastAll.Size = new System.Drawing.Size(128, 33);
			this.Btn_PastAll.TabIndex = 1;
			this.Btn_PastAll.Text = "全体にペースト ->";
			this.Btn_PastAll.UseVisualStyleBackColor = true;
			this.Btn_PastAll.Click += new System.EventHandler(this.Btn_PastAll_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(243, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "[ルート指定]";
			// 
			// Lb_Copy
			// 
			this.Lb_Copy.FormattingEnabled = true;
			this.Lb_Copy.ItemHeight = 12;
			this.Lb_Copy.Location = new System.Drawing.Point(6, 52);
			this.Lb_Copy.Name = "Lb_Copy";
			this.Lb_Copy.Size = new System.Drawing.Size(126, 232);
			this.Lb_Copy.TabIndex = 3;
			// 
			// Btn_Copy
			// 
			this.Btn_Copy.Location = new System.Drawing.Point(6, 21);
			this.Btn_Copy.Name = "Btn_Copy";
			this.Btn_Copy.Size = new System.Drawing.Size(126, 25);
			this.Btn_Copy.TabIndex = 4;
			this.Btn_Copy.Text = "->　コピー";
			this.Btn_Copy.UseVisualStyleBackColor = true;
			this.Btn_Copy.Click += new System.EventHandler(this.Btn_Copy_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.Btn_Copy);
			this.groupBox1.Controls.Add(this.Lb_Copy);
			this.groupBox1.Controls.Add(this.Btn_PastAll);
			this.groupBox1.Controls.Add(this.Btn_PasteGroup);
			this.groupBox1.Location = new System.Drawing.Point(237, 155);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(284, 309);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "複製";
#endif
			// 
			// FormRoute
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(522, 528);
#if false
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Cb_Route);
			this.groupBox1.ResumeLayout(false);
#endif
			this.Name = "FormRoute";
			this.Text = "FormRoute";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

#if false
		private System.Windows.Forms.ComboBox Cb_Route;
		private System.Windows.Forms.Button Btn_PasteGroup;
		private System.Windows.Forms.Button Btn_PastAll;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox Lb_Copy;
		private System.Windows.Forms.Button Btn_Copy;
		private System.Windows.Forms.GroupBox groupBox1;
#endif
	}
}