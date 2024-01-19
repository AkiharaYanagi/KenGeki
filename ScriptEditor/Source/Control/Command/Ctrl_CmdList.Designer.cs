namespace ScriptEditor
{
	partial class Ctrl_CmdList
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
			this.ctrl_Command1 = new ScriptEditor.Ctrl_Command();
			this.SuspendLayout();
			// 
			// ctrl_Command1
			// 
			this.ctrl_Command1.Location = new System.Drawing.Point(231, 19);
			this.ctrl_Command1.Name = "ctrl_Command1";
			this.ctrl_Command1.Size = new System.Drawing.Size(688, 462);
			this.ctrl_Command1.TabIndex = 0;
			// 
			// Ctrl_CmdList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ctrl_Command1);
			this.Name = "Ctrl_CmdList";
			this.Size = new System.Drawing.Size(899, 541);
			this.ResumeLayout(false);

		}

		#endregion

		private Ctrl_Command ctrl_Command1;
	}
}
