namespace ScriptEditor
{
	partial class ListCommand
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
			this.Lb_LCmd = new System.Windows.Forms.ListBox();
			this.Btn_Add = new System.Windows.Forms.Button();
			this.Btn_Del = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// Lb_LCmd
			// 
			this.Lb_LCmd.FormattingEnabled = true;
			this.Lb_LCmd.ItemHeight = 12;
			this.Lb_LCmd.Location = new System.Drawing.Point(21, 20);
			this.Lb_LCmd.Name = "Lb_LCmd";
			this.Lb_LCmd.Size = new System.Drawing.Size(124, 364);
			this.Lb_LCmd.TabIndex = 0;
			this.Lb_LCmd.SelectedIndexChanged += new System.EventHandler(this.Lb_LCmd_SelectedIndexChanged);
			// 
			// Btn_Add
			// 
			this.Btn_Add.BackColor = System.Drawing.Color.LightSkyBlue;
			this.Btn_Add.Location = new System.Drawing.Point(21, 390);
			this.Btn_Add.Name = "Btn_Add";
			this.Btn_Add.Size = new System.Drawing.Size(124, 30);
			this.Btn_Add.TabIndex = 1;
			this.Btn_Add.Text = "コマンド追加";
			this.Btn_Add.UseVisualStyleBackColor = false;
			this.Btn_Add.Click += new System.EventHandler(this.Btn_Add_Click);
			// 
			// Btn_Del
			// 
			this.Btn_Del.BackColor = System.Drawing.Color.Pink;
			this.Btn_Del.Location = new System.Drawing.Point(63, 426);
			this.Btn_Del.Name = "Btn_Del";
			this.Btn_Del.Size = new System.Drawing.Size(82, 23);
			this.Btn_Del.TabIndex = 2;
			this.Btn_Del.Text = "コマンド削除";
			this.Btn_Del.UseVisualStyleBackColor = false;
			this.Btn_Del.Click += new System.EventHandler(this.Btn_Del_Click);
			// 
			// ListCommand
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Btn_Del);
			this.Controls.Add(this.Btn_Add);
			this.Controls.Add(this.Lb_LCmd);
			this.Name = "ListCommand";
			this.Size = new System.Drawing.Size(643, 489);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox Lb_LCmd;
		private System.Windows.Forms.Button Btn_Add;
		private System.Windows.Forms.Button Btn_Del;
	}
}
