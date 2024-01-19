namespace ScriptEditor
{
	partial class FormRect
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.ctrl_ListORect = new ScriptEditor.Ctrl_ListRect();
			this.ctrl_ListARect = new ScriptEditor.Ctrl_ListRect();
			this.ctrl_ListHRect = new ScriptEditor.Ctrl_ListRect();
			this.ctrl_ListCRect = new ScriptEditor.Ctrl_ListRect();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label1.Location = new System.Drawing.Point(3, 65);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(342, 2);
			this.label1.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label2.Location = new System.Drawing.Point(3, 138);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(342, 2);
			this.label2.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label3.Location = new System.Drawing.Point(3, 207);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(342, 2);
			this.label3.TabIndex = 1;
			// 
			// ctrl_ListORect
			// 
			this.ctrl_ListORect.GroupSetter = null;
			this.ctrl_ListORect.Location = new System.Drawing.Point(3, 223);
			this.ctrl_ListORect.Name = "ctrl_ListORect";
			this.ctrl_ListORect.Size = new System.Drawing.Size(353, 52);
			this.ctrl_ListORect.TabIndex = 0;
			// 
			// ctrl_ListARect
			// 
			this.ctrl_ListARect.GroupSetter = null;
			this.ctrl_ListARect.Location = new System.Drawing.Point(3, 152);
			this.ctrl_ListARect.Name = "ctrl_ListARect";
			this.ctrl_ListARect.Size = new System.Drawing.Size(353, 52);
			this.ctrl_ListARect.TabIndex = 0;
			// 
			// ctrl_ListHRect
			// 
			this.ctrl_ListHRect.GroupSetter = null;
			this.ctrl_ListHRect.Location = new System.Drawing.Point(3, 83);
			this.ctrl_ListHRect.Name = "ctrl_ListHRect";
			this.ctrl_ListHRect.Size = new System.Drawing.Size(353, 52);
			this.ctrl_ListHRect.TabIndex = 0;
			// 
			// ctrl_ListCRect
			// 
			this.ctrl_ListCRect.GroupSetter = null;
			this.ctrl_ListCRect.Location = new System.Drawing.Point(3, 10);
			this.ctrl_ListCRect.Name = "ctrl_ListCRect";
			this.ctrl_ListCRect.Size = new System.Drawing.Size(353, 52);
			this.ctrl_ListCRect.TabIndex = 0;
			// 
			// FormRect
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(347, 286);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.ctrl_ListORect);
			this.Controls.Add(this.ctrl_ListARect);
			this.Controls.Add(this.ctrl_ListHRect);
			this.Controls.Add(this.ctrl_ListCRect);
			this.Name = "FormRect";
			this.Text = "Rect";
			this.ResumeLayout(false);

		}

		#endregion

		public Ctrl_ListRect ctrl_ListCRect;
		public Ctrl_ListRect ctrl_ListHRect;
		public Ctrl_ListRect ctrl_ListARect;
		public Ctrl_ListRect ctrl_ListORect;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
	}
}