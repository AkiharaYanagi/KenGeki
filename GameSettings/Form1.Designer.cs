namespace GameSettings
{
	partial class Form1
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
			if ( disposing && (components != null) )
			{
				components.Dispose ();
			}
			base.Dispose ( disposing );
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent ()
		{
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage_System = new System.Windows.Forms.TabPage();
			this.tabPage_KeyConfig = new System.Windows.Forms.TabPage();
			this.tabPage_Game = new System.Windows.Forms.TabPage();
			this.tabControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage_System);
			this.tabControl1.Controls.Add(this.tabPage_KeyConfig);
			this.tabControl1.Controls.Add(this.tabPage_Game);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(549, 516);
			this.tabControl1.TabIndex = 0;
			this.tabControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabControl1_KeyDown);
			this.tabControl1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tabControl1_PreviewKeyDown);
			// 
			// tabPage_System
			// 
			this.tabPage_System.Location = new System.Drawing.Point(4, 22);
			this.tabPage_System.Name = "tabPage_System";
			this.tabPage_System.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage_System.Size = new System.Drawing.Size(541, 490);
			this.tabPage_System.TabIndex = 1;
			this.tabPage_System.Text = "システム設定";
			this.tabPage_System.UseVisualStyleBackColor = true;
			// 
			// tabPage_KeyConfig
			// 
			this.tabPage_KeyConfig.Location = new System.Drawing.Point(4, 22);
			this.tabPage_KeyConfig.Name = "tabPage_KeyConfig";
			this.tabPage_KeyConfig.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage_KeyConfig.Size = new System.Drawing.Size(541, 490);
			this.tabPage_KeyConfig.TabIndex = 0;
			this.tabPage_KeyConfig.Text = "キーコンフィグ";
			this.tabPage_KeyConfig.UseVisualStyleBackColor = true;
			// 
			// tabPage_Game
			// 
			this.tabPage_Game.Location = new System.Drawing.Point(4, 22);
			this.tabPage_Game.Name = "tabPage_Game";
			this.tabPage_Game.Size = new System.Drawing.Size(541, 490);
			this.tabPage_Game.TabIndex = 2;
			this.tabPage_Game.Text = "ゲーム設定";
			this.tabPage_Game.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(549, 516);
			this.Controls.Add(this.tabControl1);
			this.Name = "Form1";
			this.Text = "設定";
			this.tabControl1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage_System;
		private System.Windows.Forms.TabPage tabPage_KeyConfig;
		private System.Windows.Forms.TabPage tabPage_Game;
	}
}

