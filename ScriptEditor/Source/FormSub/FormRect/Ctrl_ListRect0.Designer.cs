namespace ScriptEditor
{
	partial class Ctrl_ListRect
	{
		/// <summary> 
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose ( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose ();
			}
			base.Dispose ( disposing );
		}

		#region コンポーネント デザイナーで生成されたコード

		/// <summary> 
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent ()
		{
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.Lbl_RectName = new System.Windows.Forms.Label();
			this.Lbl_X = new System.Windows.Forms.Label();
			this.Lbl_Y = new System.Windows.Forms.Label();
			this.Lbl_W = new System.Windows.Forms.Label();
			this.Lbl_H = new System.Windows.Forms.Label();
			this.Btn_Add = new System.Windows.Forms.Button();
			this.Btn_Del = new System.Windows.Forms.Button();
			this.Lbl_N = new System.Windows.Forms.Label();
			this.Tbn_H = new ScriptEditor.TB_Number();
			this.Tbn_Y = new ScriptEditor.TB_Number();
			this.Tbn_W = new ScriptEditor.TB_Number();
			this.Tbn_X = new ScriptEditor.TB_Number();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.SuspendLayout();
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.numericUpDown1.Location = new System.Drawing.Point(14, 23);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(39, 23);
			this.numericUpDown1.TabIndex = 0;
			this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
			// 
			// Lbl_RectName
			// 
			this.Lbl_RectName.AutoSize = true;
			this.Lbl_RectName.Location = new System.Drawing.Point(12, 3);
			this.Lbl_RectName.Name = "Lbl_RectName";
			this.Lbl_RectName.Size = new System.Drawing.Size(29, 12);
			this.Lbl_RectName.TabIndex = 1;
			this.Lbl_RectName.Text = "Rect";
			// 
			// Lbl_X
			// 
			this.Lbl_X.AutoSize = true;
			this.Lbl_X.Location = new System.Drawing.Point(165, 3);
			this.Lbl_X.Name = "Lbl_X";
			this.Lbl_X.Size = new System.Drawing.Size(12, 12);
			this.Lbl_X.TabIndex = 3;
			this.Lbl_X.Text = "X";
			// 
			// Lbl_Y
			// 
			this.Lbl_Y.AutoSize = true;
			this.Lbl_Y.Location = new System.Drawing.Point(165, 31);
			this.Lbl_Y.Name = "Lbl_Y";
			this.Lbl_Y.Size = new System.Drawing.Size(12, 12);
			this.Lbl_Y.TabIndex = 3;
			this.Lbl_Y.Text = "Y";
			// 
			// Lbl_W
			// 
			this.Lbl_W.AutoSize = true;
			this.Lbl_W.Location = new System.Drawing.Point(255, 3);
			this.Lbl_W.Name = "Lbl_W";
			this.Lbl_W.Size = new System.Drawing.Size(14, 12);
			this.Lbl_W.TabIndex = 3;
			this.Lbl_W.Text = "W";
			// 
			// Lbl_H
			// 
			this.Lbl_H.AutoSize = true;
			this.Lbl_H.Location = new System.Drawing.Point(255, 31);
			this.Lbl_H.Name = "Lbl_H";
			this.Lbl_H.Size = new System.Drawing.Size(13, 12);
			this.Lbl_H.TabIndex = 3;
			this.Lbl_H.Text = "H";
			// 
			// Btn_Add
			// 
			this.Btn_Add.Location = new System.Drawing.Point(102, 3);
			this.Btn_Add.Name = "Btn_Add";
			this.Btn_Add.Size = new System.Drawing.Size(44, 22);
			this.Btn_Add.TabIndex = 4;
			this.Btn_Add.Text = "追加";
			this.Btn_Add.UseVisualStyleBackColor = true;
			this.Btn_Add.Click += new System.EventHandler(this.Btn_Add_Click);
			// 
			// Btn_Del
			// 
			this.Btn_Del.Location = new System.Drawing.Point(102, 26);
			this.Btn_Del.Name = "Btn_Del";
			this.Btn_Del.Size = new System.Drawing.Size(44, 22);
			this.Btn_Del.TabIndex = 4;
			this.Btn_Del.Text = "削除";
			this.Btn_Del.UseVisualStyleBackColor = true;
			this.Btn_Del.Click += new System.EventHandler(this.Btn_Del_Click);
			// 
			// Lbl_N
			// 
			this.Lbl_N.AutoSize = true;
			this.Lbl_N.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Lbl_N.Location = new System.Drawing.Point(59, 27);
			this.Lbl_N.Name = "Lbl_N";
			this.Lbl_N.Size = new System.Drawing.Size(24, 16);
			this.Lbl_N.TabIndex = 5;
			this.Lbl_N.Text = "/n";
			// 
			// Tbn_H
			// 
			this.Tbn_H.Location = new System.Drawing.Point(275, 28);
			this.Tbn_H.Name = "Tbn_H";
			this.Tbn_H.Size = new System.Drawing.Size(49, 19);
			this.Tbn_H.TabIndex = 2;
			this.Tbn_H.Text = "0";
			// 
			// Tbn_Y
			// 
			this.Tbn_Y.Location = new System.Drawing.Point(183, 28);
			this.Tbn_Y.Name = "Tbn_Y";
			this.Tbn_Y.Size = new System.Drawing.Size(53, 19);
			this.Tbn_Y.TabIndex = 2;
			this.Tbn_Y.Text = "0";
			// 
			// Tbn_W
			// 
			this.Tbn_W.Location = new System.Drawing.Point(275, 0);
			this.Tbn_W.Name = "Tbn_W";
			this.Tbn_W.Size = new System.Drawing.Size(49, 19);
			this.Tbn_W.TabIndex = 2;
			this.Tbn_W.Text = "0";
			// 
			// Tbn_X
			// 
			this.Tbn_X.Location = new System.Drawing.Point(183, 0);
			this.Tbn_X.Name = "Tbn_X";
			this.Tbn_X.Size = new System.Drawing.Size(53, 19);
			this.Tbn_X.TabIndex = 2;
			this.Tbn_X.Text = "0";
			// 
			// Ctrl_ListRect0
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Lbl_N);
			this.Controls.Add(this.Btn_Del);
			this.Controls.Add(this.Btn_Add);
			this.Controls.Add(this.Lbl_H);
			this.Controls.Add(this.Lbl_W);
			this.Controls.Add(this.Lbl_Y);
			this.Controls.Add(this.Lbl_X);
			this.Controls.Add(this.Tbn_H);
			this.Controls.Add(this.Tbn_Y);
			this.Controls.Add(this.Tbn_W);
			this.Controls.Add(this.Tbn_X);
			this.Controls.Add(this.Lbl_RectName);
			this.Controls.Add(this.numericUpDown1);
			this.Name = "Ctrl_ListRect0";
			this.Size = new System.Drawing.Size(337, 52);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label Lbl_RectName;
		private TB_Number Tbn_X;
		private System.Windows.Forms.Label Lbl_X;
		private System.Windows.Forms.Label Lbl_Y;
		private System.Windows.Forms.Label Lbl_W;
		private System.Windows.Forms.Label Lbl_H;
		private TB_Number Tbn_Y;
		private TB_Number Tbn_W;
		private TB_Number Tbn_H;
		private System.Windows.Forms.Button Btn_Add;
		private System.Windows.Forms.Button Btn_Del;
		private System.Windows.Forms.Label Lbl_N;
	}
}
