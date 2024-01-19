namespace ScriptEditor
{
	partial class FormBranch0
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
			ScriptEditor.BindingDictionary<ScriptEditor.Branch> bindingDictionary_11 = new ScriptEditor.BindingDictionary<ScriptEditor.Branch>();
			ScriptEditor.BindingDictionary<ScriptEditor.Command> bindingDictionary_12 = new ScriptEditor.BindingDictionary<ScriptEditor.Command>();
			ScriptEditor.BindingDictionary<ScriptEditor.Sequence> bindingDictionary_13 = new ScriptEditor.BindingDictionary<ScriptEditor.Sequence>();
			this.ctrl_Branch1 = new ScriptEditor.Ctrl_Branch();
			this.SuspendLayout();
			// 
			// ctrl_Branch1
			// 
			this.ctrl_Branch1.BD_Branch = bindingDictionary_11;
			this.ctrl_Branch1.BD_Command = bindingDictionary_12;
			this.ctrl_Branch1.BD_Sequence = bindingDictionary_13;
			this.ctrl_Branch1.Location = new System.Drawing.Point(12, 12);
			this.ctrl_Branch1.Name = "ctrl_Branch1";
			this.ctrl_Branch1.Size = new System.Drawing.Size(501, 452);
			this.ctrl_Branch1.TabIndex = 0;
			// 
			// FormBranch0
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(547, 492);
			this.Controls.Add(this.ctrl_Branch1);
			this.Name = "FormBranch0";
			this.Text = "FormBranch0";
			this.ResumeLayout(false);

		}

		#endregion

		private Ctrl_Branch ctrl_Branch1;
	}
}