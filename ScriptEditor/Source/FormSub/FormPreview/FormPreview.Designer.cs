namespace ScriptEditor
{
	partial class FormPreview
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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.Btn_Start = new System.Windows.Forms.Button();
			this.Btn_Stop = new System.Windows.Forms.Button();
			this.Btn_Pause = new System.Windows.Forms.Button();
			this.Btn_Advance = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.pictureBox1.Location = new System.Drawing.Point(8, 57);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(587, 537);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.SizeChanged += new System.EventHandler(this.pictureBox1_SizeChanged);
			// 
			// Btn_Start
			// 
			this.Btn_Start.Location = new System.Drawing.Point(178, 12);
			this.Btn_Start.Name = "Btn_Start";
			this.Btn_Start.Size = new System.Drawing.Size(65, 39);
			this.Btn_Start.TabIndex = 1;
			this.Btn_Start.Text = "Start";
			this.Btn_Start.UseVisualStyleBackColor = true;
			this.Btn_Start.Click += new System.EventHandler(this.Btn_Start_Click);
			// 
			// Btn_Stop
			// 
			this.Btn_Stop.Location = new System.Drawing.Point(249, 12);
			this.Btn_Stop.Name = "Btn_Stop";
			this.Btn_Stop.Size = new System.Drawing.Size(70, 39);
			this.Btn_Stop.TabIndex = 2;
			this.Btn_Stop.Text = "Stop";
			this.Btn_Stop.UseVisualStyleBackColor = true;
			this.Btn_Stop.Click += new System.EventHandler(this.Btn_Stop_Click);
			// 
			// Btn_Pause
			// 
			this.Btn_Pause.Location = new System.Drawing.Point(12, 12);
			this.Btn_Pause.Name = "Btn_Pause";
			this.Btn_Pause.Size = new System.Drawing.Size(77, 31);
			this.Btn_Pause.TabIndex = 3;
			this.Btn_Pause.Text = "一時停止";
			this.Btn_Pause.UseVisualStyleBackColor = true;
			this.Btn_Pause.Click += new System.EventHandler(this.Btn_Pause_Click);
			// 
			// Btn_Advance
			// 
			this.Btn_Advance.Location = new System.Drawing.Point(95, 12);
			this.Btn_Advance.Name = "Btn_Advance";
			this.Btn_Advance.Size = new System.Drawing.Size(77, 31);
			this.Btn_Advance.TabIndex = 3;
			this.Btn_Advance.Text = "コマ送り";
			this.Btn_Advance.UseVisualStyleBackColor = true;
			this.Btn_Advance.Click += new System.EventHandler(this.Btn_Advance_Click);
			// 
			// FormPreview
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(607, 606);
			this.Controls.Add(this.Btn_Advance);
			this.Controls.Add(this.Btn_Pause);
			this.Controls.Add(this.Btn_Stop);
			this.Controls.Add(this.Btn_Start);
			this.Controls.Add(this.pictureBox1);
			this.Name = "FormPreview";
			this.Text = "FormPreview";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button Btn_Start;
		private System.Windows.Forms.Button Btn_Stop;
		private System.Windows.Forms.Button Btn_Pause;
		private System.Windows.Forms.Button Btn_Advance;
	}
}