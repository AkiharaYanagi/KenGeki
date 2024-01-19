namespace ScriptEditor.Source.FormSub.FormRect
{
	partial class Ctrl_Rects
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
			this.ctrl_ListCRect = new ScriptEditor.Ctrl_ListRect();
			this.ctrl_ListHRect = new ScriptEditor.Ctrl_ListRect();
			this.ctrl_ListARect = new ScriptEditor.Ctrl_ListRect();
			this.ctrl_ListORect = new ScriptEditor.Ctrl_ListRect();
			this.SuspendLayout();
			// 
			// ctrl_ListCRect
			// 
			this.ctrl_ListCRect.Location = new System.Drawing.Point(14, 12);
			this.ctrl_ListCRect.Name = "ctrl_ListCRect";
			this.ctrl_ListCRect.Size = new System.Drawing.Size(284, 58);
			this.ctrl_ListCRect.TabIndex = 0;
			// 
			// ctrl_ListHRect
			// 
			this.ctrl_ListHRect.Location = new System.Drawing.Point(14, 79);
			this.ctrl_ListHRect.Name = "ctrl_ListHRect";
			this.ctrl_ListHRect.Size = new System.Drawing.Size(284, 58);
			this.ctrl_ListHRect.TabIndex = 0;
			// 
			// ctrl_ListARect
			// 
			this.ctrl_ListARect.Location = new System.Drawing.Point(14, 146);
			this.ctrl_ListARect.Name = "ctrl_ListARect";
			this.ctrl_ListARect.Size = new System.Drawing.Size(284, 58);
			this.ctrl_ListARect.TabIndex = 0;
			// 
			// ctrl_ListORect
			// 
			this.ctrl_ListORect.Location = new System.Drawing.Point(14, 213);
			this.ctrl_ListORect.Name = "ctrl_ListORect";
			this.ctrl_ListORect.Size = new System.Drawing.Size(284, 58);
			this.ctrl_ListORect.TabIndex = 0;
			// 
			// Ctrl_Rects
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ctrl_ListORect);
			this.Controls.Add(this.ctrl_ListARect);
			this.Controls.Add(this.ctrl_ListHRect);
			this.Controls.Add(this.ctrl_ListCRect);
			this.Name = "Ctrl_Rects";
			this.Size = new System.Drawing.Size(312, 290);
			this.ResumeLayout(false);

		}

		#endregion

		private Ctrl_ListRect ctrl_ListCRect;
		private Ctrl_ListRect ctrl_ListHRect;
		private Ctrl_ListRect ctrl_ListARect;
		private Ctrl_ListRect ctrl_ListORect;
	}
}
