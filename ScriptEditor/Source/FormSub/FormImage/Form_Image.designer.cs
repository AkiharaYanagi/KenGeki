namespace ScriptEditor
{
	partial class FormImage
	{
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose ( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose ();
			}
			base.Dispose ( disposing );
		}

		#region Windows フォーム デザイナで生成されたコード

		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent ()
		{
			this.Lb_Image = new System.Windows.Forms.ListBox();
			this.btn_ArchiveOK = new System.Windows.Forms.Button();
			this.pbArchiveImage = new System.Windows.Forms.PictureBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.読込ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.全削除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.btn_Cancel = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.フォルダ読込ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			((System.ComponentModel.ISupportInitialize)(this.pbArchiveImage)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// Lb_Image
			// 
			this.Lb_Image.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.Lb_Image.FormattingEnabled = true;
			this.Lb_Image.ItemHeight = 12;
			this.Lb_Image.Location = new System.Drawing.Point(12, 33);
			this.Lb_Image.Name = "Lb_Image";
			this.Lb_Image.Size = new System.Drawing.Size(180, 520);
			this.Lb_Image.TabIndex = 1;
			this.Lb_Image.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
			// 
			// btn_ArchiveOK
			// 
			this.btn_ArchiveOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btn_ArchiveOK.Location = new System.Drawing.Point(409, 32);
			this.btn_ArchiveOK.Name = "btn_ArchiveOK";
			this.btn_ArchiveOK.Size = new System.Drawing.Size(180, 27);
			this.btn_ArchiveOK.TabIndex = 3;
			this.btn_ArchiveOK.Text = "OK";
			this.btn_ArchiveOK.UseVisualStyleBackColor = true;
			this.btn_ArchiveOK.Click += new System.EventHandler(this.Btn_ArchiveOK_Click);
			// 
			// pbArchiveImage
			// 
			this.pbArchiveImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pbArchiveImage.BackColor = System.Drawing.SystemColors.ControlDark;
			this.pbArchiveImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pbArchiveImage.Location = new System.Drawing.Point(203, 66);
			this.pbArchiveImage.Name = "pbArchiveImage";
			this.pbArchiveImage.Size = new System.Drawing.Size(528, 497);
			this.pbArchiveImage.TabIndex = 4;
			this.pbArchiveImage.TabStop = false;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.読込ToolStripMenuItem,
            this.全削除ToolStripMenuItem,
            this.フォルダ読込ToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(743, 24);
			this.menuStrip1.TabIndex = 5;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// 読込ToolStripMenuItem
			// 
			this.読込ToolStripMenuItem.Name = "読込ToolStripMenuItem";
			this.読込ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
			this.読込ToolStripMenuItem.Text = "読込";
			this.読込ToolStripMenuItem.Click += new System.EventHandler(this.読込ToolStripMenuItem_Click);
			// 
			// 全削除ToolStripMenuItem
			// 
			this.全削除ToolStripMenuItem.Name = "全削除ToolStripMenuItem";
			this.全削除ToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
			this.全削除ToolStripMenuItem.Text = "全削除";
			this.全削除ToolStripMenuItem.Click += new System.EventHandler(this.全削除ToolStripMenuItem_Click);
			// 
			// btn_Cancel
			// 
			this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn_Cancel.Location = new System.Drawing.Point(203, 33);
			this.btn_Cancel.Name = "btn_Cancel";
			this.btn_Cancel.Size = new System.Drawing.Size(189, 25);
			this.btn_Cancel.TabIndex = 6;
			this.btn_Cancel.Text = "Cancel";
			this.btn_Cancel.UseVisualStyleBackColor = true;
			this.btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// フォルダ読込ToolStripMenuItem
			// 
			this.フォルダ読込ToolStripMenuItem.Name = "フォルダ読込ToolStripMenuItem";
			this.フォルダ読込ToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
			this.フォルダ読込ToolStripMenuItem.Text = "フォルダ読込";
			this.フォルダ読込ToolStripMenuItem.Click += new System.EventHandler(this.フォルダ読込ToolStripMenuItem_Click);
			// 
			// FormImage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(743, 575);
			this.Controls.Add(this.btn_ArchiveOK);
			this.Controls.Add(this.btn_Cancel);
			this.Controls.Add(this.pbArchiveImage);
			this.Controls.Add(this.Lb_Image);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "FormImage";
			this.Text = "Form_ImagePreview";
			this.VisibleChanged += new System.EventHandler(this.FormImage_VisibleChanged);
			((System.ComponentModel.ISupportInitialize)(this.pbArchiveImage)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox Lb_Image;
		private System.Windows.Forms.Button btn_ArchiveOK;
		private System.Windows.Forms.PictureBox pbArchiveImage;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem 読込ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 全削除ToolStripMenuItem;
		private System.Windows.Forms.Button btn_Cancel;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.ToolStripMenuItem フォルダ読込ToolStripMenuItem;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
	}
}