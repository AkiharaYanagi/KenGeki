namespace ScriptEditor
{
	partial class Ctrl_Image
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
			this.PB_Image = new System.Windows.Forms.PictureBox();
			this.rB_ToolHand = new ScriptEditor.RB_Tool();
			this.rB_ToolCRect = new ScriptEditor.RB_Tool();
			this.rB_ToolHRect = new ScriptEditor.RB_Tool();
			this.rB_ToolARect = new ScriptEditor.RB_Tool();
			this.rB_ToolORect = new ScriptEditor.RB_Tool();
			this.rB_ToolEfHand = new ScriptEditor.RB_Tool();
			((System.ComponentModel.ISupportInitialize)(this.PB_Image)).BeginInit();
			this.SuspendLayout();
			// 
			// PB_Image
			// 
			this.PB_Image.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PB_Image.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.PB_Image.Location = new System.Drawing.Point(0, 0);
			this.PB_Image.Name = "PB_Image";
			this.PB_Image.Size = new System.Drawing.Size(500, 400);
			this.PB_Image.TabIndex = 6;
			this.PB_Image.TabStop = false;
			this.PB_Image.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PB_Image_MouseDown);
			this.PB_Image.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PB_Image_MouseMove);
			this.PB_Image.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PB_Image_MouseUp);
			// 
			// rB_ToolHand
			// 
			this.rB_ToolHand.Appearance = System.Windows.Forms.Appearance.Button;
			this.rB_ToolHand.AutoSize = true;
			this.rB_ToolHand.Image = global::ScriptEditor.Properties.Resources.tool_hand;
			this.rB_ToolHand.Location = new System.Drawing.Point(-2, -2);
			this.rB_ToolHand.Name = "rB_ToolHand";
			this.rB_ToolHand.Size = new System.Drawing.Size(38, 38);
			this.rB_ToolHand.TabIndex = 7;
			this.rB_ToolHand.TabStop = true;
			this.rB_ToolHand.Tl_Img = null;
			this.rB_ToolHand.UseVisualStyleBackColor = true;
			this.rB_ToolHand.CheckedChanged += new System.EventHandler(this.rB_ToolHand_CheckedChanged);
			// 
			// rB_ToolCRect
			// 
			this.rB_ToolCRect.Appearance = System.Windows.Forms.Appearance.Button;
			this.rB_ToolCRect.AutoSize = true;
			this.rB_ToolCRect.Image = global::ScriptEditor.Properties.Resources.tool_CRect;
			this.rB_ToolCRect.Location = new System.Drawing.Point(-2, 34);
			this.rB_ToolCRect.Name = "rB_ToolCRect";
			this.rB_ToolCRect.Size = new System.Drawing.Size(38, 38);
			this.rB_ToolCRect.TabIndex = 7;
			this.rB_ToolCRect.TabStop = true;
			this.rB_ToolCRect.Tl_Img = null;
			this.rB_ToolCRect.UseVisualStyleBackColor = true;
			// 
			// rB_ToolHRect
			// 
			this.rB_ToolHRect.Appearance = System.Windows.Forms.Appearance.Button;
			this.rB_ToolHRect.AutoSize = true;
			this.rB_ToolHRect.Image = global::ScriptEditor.Properties.Resources.tool_HRect;
			this.rB_ToolHRect.Location = new System.Drawing.Point(-2, 68);
			this.rB_ToolHRect.Name = "rB_ToolHRect";
			this.rB_ToolHRect.Size = new System.Drawing.Size(38, 38);
			this.rB_ToolHRect.TabIndex = 7;
			this.rB_ToolHRect.TabStop = true;
			this.rB_ToolHRect.Tl_Img = null;
			this.rB_ToolHRect.UseVisualStyleBackColor = true;
			// 
			// rB_ToolARect
			// 
			this.rB_ToolARect.Appearance = System.Windows.Forms.Appearance.Button;
			this.rB_ToolARect.AutoSize = true;
			this.rB_ToolARect.Image = global::ScriptEditor.Properties.Resources.tool_ARect;
			this.rB_ToolARect.Location = new System.Drawing.Point(-2, 103);
			this.rB_ToolARect.Name = "rB_ToolARect";
			this.rB_ToolARect.Size = new System.Drawing.Size(38, 38);
			this.rB_ToolARect.TabIndex = 7;
			this.rB_ToolARect.TabStop = true;
			this.rB_ToolARect.Tl_Img = null;
			this.rB_ToolARect.UseVisualStyleBackColor = true;
			// 
			// rB_ToolORect
			// 
			this.rB_ToolORect.Appearance = System.Windows.Forms.Appearance.Button;
			this.rB_ToolORect.AutoSize = true;
			this.rB_ToolORect.Image = global::ScriptEditor.Properties.Resources.tool_ORect;
			this.rB_ToolORect.Location = new System.Drawing.Point(-2, 138);
			this.rB_ToolORect.Name = "rB_ToolORect";
			this.rB_ToolORect.Size = new System.Drawing.Size(38, 38);
			this.rB_ToolORect.TabIndex = 7;
			this.rB_ToolORect.TabStop = true;
			this.rB_ToolORect.Tl_Img = null;
			this.rB_ToolORect.UseVisualStyleBackColor = true;
			// 
			// rB_ToolEfHand
			// 
			this.rB_ToolEfHand.Appearance = System.Windows.Forms.Appearance.Button;
			this.rB_ToolEfHand.AutoSize = true;
			this.rB_ToolEfHand.Image = global::ScriptEditor.Properties.Resources.tool_efhand;
			this.rB_ToolEfHand.Location = new System.Drawing.Point(-2, 173);
			this.rB_ToolEfHand.Name = "rB_ToolEfHand";
			this.rB_ToolEfHand.Size = new System.Drawing.Size(38, 38);
			this.rB_ToolEfHand.TabIndex = 7;
			this.rB_ToolEfHand.TabStop = true;
			this.rB_ToolEfHand.Tl_Img = null;
			this.rB_ToolEfHand.UseVisualStyleBackColor = true;
			// 
			// Ctrl_Image
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.rB_ToolEfHand);
			this.Controls.Add(this.rB_ToolORect);
			this.Controls.Add(this.rB_ToolARect);
			this.Controls.Add(this.rB_ToolHRect);
			this.Controls.Add(this.rB_ToolCRect);
			this.Controls.Add(this.rB_ToolHand);
			this.Controls.Add(this.PB_Image);
			this.DoubleBuffered = true;
			this.Name = "Ctrl_Image";
			this.Size = new System.Drawing.Size(500, 400);
			((System.ComponentModel.ISupportInitialize)(this.PB_Image)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private RB_Tool rB_ToolHand;
		private RB_Tool rB_ToolCRect;
		private RB_Tool rB_ToolHRect;
		private RB_Tool rB_ToolARect;
		private RB_Tool rB_ToolORect;
		private RB_Tool rB_ToolEfHand;
		public System.Windows.Forms.PictureBox PB_Image;
	}
}
