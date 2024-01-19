namespace ScriptEditor
{
	partial class Ctrl_ListBox<T>
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
			this.ListBox0 = new System.Windows.Forms.ListBox();
			this.BTN_Add = new System.Windows.Forms.Button();
			this.BTN_Del = new System.Windows.Forms.Button();
			this.Btn_Up = new System.Windows.Forms.Button();
			this.Btn_Down = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// ListBox0
			// 
			this.ListBox0.FormattingEnabled = true;
			this.ListBox0.ItemHeight = 12;
			this.ListBox0.Location = new System.Drawing.Point(0, 0);
			this.ListBox0.Name = "ListBox0";
			this.ListBox0.ScrollAlwaysVisible = true;
			this.ListBox0.Size = new System.Drawing.Size(137, 508);
			this.ListBox0.TabIndex = 0;
			this.ListBox0.SelectedIndexChanged += new System.EventHandler(this.ListBox_SelectedIndexChanged);
			// 
			// BTN_Add
			// 
			this.BTN_Add.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.BTN_Add.Location = new System.Drawing.Point(143, 190);
			this.BTN_Add.Name = "BTN_Add";
			this.BTN_Add.Size = new System.Drawing.Size(38, 83);
			this.BTN_Add.TabIndex = 1;
			this.BTN_Add.Text = "追加";
			this.BTN_Add.UseVisualStyleBackColor = false;
			this.BTN_Add.Click += new System.EventHandler(this.BTN_Add_Click);
			// 
			// BTN_Del
			// 
			this.BTN_Del.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.BTN_Del.Location = new System.Drawing.Point(143, 279);
			this.BTN_Del.Name = "BTN_Del";
			this.BTN_Del.Size = new System.Drawing.Size(38, 37);
			this.BTN_Del.TabIndex = 2;
			this.BTN_Del.Text = "削除";
			this.BTN_Del.UseVisualStyleBackColor = false;
			this.BTN_Del.Click += new System.EventHandler(this.BTN_Del_Click);
			// 
			// Btn_Up
			// 
			this.Btn_Up.Location = new System.Drawing.Point(143, 0);
			this.Btn_Up.Name = "Btn_Up";
			this.Btn_Up.Size = new System.Drawing.Size(38, 72);
			this.Btn_Up.TabIndex = 3;
			this.Btn_Up.Text = "↑";
			this.Btn_Up.UseVisualStyleBackColor = true;
			this.Btn_Up.Click += new System.EventHandler(this.Btn_Up_Click);
			// 
			// Btn_Down
			// 
			this.Btn_Down.Location = new System.Drawing.Point(143, 78);
			this.Btn_Down.Name = "Btn_Down";
			this.Btn_Down.Size = new System.Drawing.Size(38, 74);
			this.Btn_Down.TabIndex = 3;
			this.Btn_Down.Text = "↓";
			this.Btn_Down.UseVisualStyleBackColor = true;
			this.Btn_Down.Click += new System.EventHandler(this.Btn_Down_Click);
			// 
			// Ctrl_ListBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Btn_Down);
			this.Controls.Add(this.Btn_Up);
			this.Controls.Add(this.BTN_Del);
			this.Controls.Add(this.BTN_Add);
			this.Controls.Add(this.ListBox0);
			this.Name = "Ctrl_ListBox";
			this.Size = new System.Drawing.Size(186, 511);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox ListBox0;
		private System.Windows.Forms.Button BTN_Add;
		private System.Windows.Forms.Button BTN_Del;
		private System.Windows.Forms.Button Btn_Up;
		private System.Windows.Forms.Button Btn_Down;
	}
}
