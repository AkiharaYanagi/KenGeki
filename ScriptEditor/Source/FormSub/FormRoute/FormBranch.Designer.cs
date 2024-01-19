namespace ScriptEditor
{
	partial class FormBranch
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
			this.Lb_Command = new System.Windows.Forms.ListBox();
			this.Lb_Action = new System.Windows.Forms.ListBox();
			this.Btn_Add = new System.Windows.Forms.Button();
			this.Btn_Del = new System.Windows.Forms.Button();
			this.Cb_Command = new System.Windows.Forms.ComboBox();
			this.Cb_Action = new System.Windows.Forms.ComboBox();
			this.Btn_Up = new System.Windows.Forms.Button();
			this.Btn_Down = new System.Windows.Forms.Button();
			this.Ckb_All = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// Lb_Command
			// 
			this.Lb_Command.FormattingEnabled = true;
			this.Lb_Command.ItemHeight = 12;
			this.Lb_Command.Location = new System.Drawing.Point(26, 45);
			this.Lb_Command.Name = "Lb_Command";
			this.Lb_Command.Size = new System.Drawing.Size(172, 268);
			this.Lb_Command.TabIndex = 0;
			this.Lb_Command.SelectedIndexChanged += new System.EventHandler(this.Lb_Command_SelectedIndexChanged);
			// 
			// Lb_Action
			// 
			this.Lb_Action.FormattingEnabled = true;
			this.Lb_Action.ItemHeight = 12;
			this.Lb_Action.Location = new System.Drawing.Point(241, 45);
			this.Lb_Action.Name = "Lb_Action";
			this.Lb_Action.Size = new System.Drawing.Size(179, 268);
			this.Lb_Action.TabIndex = 1;
			this.Lb_Action.SelectedIndexChanged += new System.EventHandler(this.Lb_Action_SelectedIndexChanged);
			// 
			// Btn_Add
			// 
			this.Btn_Add.Location = new System.Drawing.Point(26, 351);
			this.Btn_Add.Name = "Btn_Add";
			this.Btn_Add.Size = new System.Drawing.Size(79, 30);
			this.Btn_Add.TabIndex = 2;
			this.Btn_Add.Text = "追加";
			this.Btn_Add.UseVisualStyleBackColor = true;
			this.Btn_Add.Click += new System.EventHandler(this.Btn_Add_Click);
			// 
			// Btn_Del
			// 
			this.Btn_Del.Location = new System.Drawing.Point(119, 351);
			this.Btn_Del.Name = "Btn_Del";
			this.Btn_Del.Size = new System.Drawing.Size(79, 29);
			this.Btn_Del.TabIndex = 3;
			this.Btn_Del.Text = "削除";
			this.Btn_Del.UseVisualStyleBackColor = true;
			this.Btn_Del.Click += new System.EventHandler(this.Btn_Del_Click);
			// 
			// Cb_Command
			// 
			this.Cb_Command.FormattingEnabled = true;
			this.Cb_Command.Location = new System.Drawing.Point(26, 325);
			this.Cb_Command.Name = "Cb_Command";
			this.Cb_Command.Size = new System.Drawing.Size(172, 20);
			this.Cb_Command.TabIndex = 4;
			this.Cb_Command.DropDownClosed += new System.EventHandler(this.Cb_Command_DropDownClosed);
			// 
			// Cb_Action
			// 
			this.Cb_Action.FormattingEnabled = true;
			this.Cb_Action.Location = new System.Drawing.Point(241, 325);
			this.Cb_Action.Name = "Cb_Action";
			this.Cb_Action.Size = new System.Drawing.Size(178, 20);
			this.Cb_Action.TabIndex = 5;
			this.Cb_Action.DropDownClosed += new System.EventHandler(this.Cb_Action_DropDownClosed);
			// 
			// Btn_Up
			// 
			this.Btn_Up.Location = new System.Drawing.Point(241, 352);
			this.Btn_Up.Name = "Btn_Up";
			this.Btn_Up.Size = new System.Drawing.Size(82, 29);
			this.Btn_Up.TabIndex = 6;
			this.Btn_Up.Text = "↑";
			this.Btn_Up.UseVisualStyleBackColor = true;
			this.Btn_Up.Click += new System.EventHandler(this.Btn_Up_Click);
			// 
			// Btn_Down
			// 
			this.Btn_Down.Location = new System.Drawing.Point(329, 352);
			this.Btn_Down.Name = "Btn_Down";
			this.Btn_Down.Size = new System.Drawing.Size(90, 29);
			this.Btn_Down.TabIndex = 7;
			this.Btn_Down.Text = "↓";
			this.Btn_Down.UseVisualStyleBackColor = true;
			this.Btn_Down.Click += new System.EventHandler(this.Btn_Down_Click);
			// 
			// Ckb_All
			// 
			this.Ckb_All.AutoSize = true;
			this.Ckb_All.Checked = true;
			this.Ckb_All.CheckState = System.Windows.Forms.CheckState.Checked;
			this.Ckb_All.Location = new System.Drawing.Point(26, 20);
			this.Ckb_All.Name = "Ckb_All";
			this.Ckb_All.Size = new System.Drawing.Size(38, 16);
			this.Ckb_All.TabIndex = 8;
			this.Ckb_All.Text = "All";
			this.Ckb_All.UseVisualStyleBackColor = true;
			// 
			// FormBranch
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(442, 392);
			this.Controls.Add(this.Ckb_All);
			this.Controls.Add(this.Btn_Down);
			this.Controls.Add(this.Btn_Up);
			this.Controls.Add(this.Cb_Action);
			this.Controls.Add(this.Cb_Command);
			this.Controls.Add(this.Btn_Del);
			this.Controls.Add(this.Btn_Add);
			this.Controls.Add(this.Lb_Action);
			this.Controls.Add(this.Lb_Command);
			this.Name = "FormBranch";
			this.Text = "Branch";
			this.VisibleChanged += new System.EventHandler(this.FormBranch_VisibleChanged);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox Lb_Command;
		private System.Windows.Forms.ListBox Lb_Action;
		private System.Windows.Forms.Button Btn_Add;
		private System.Windows.Forms.Button Btn_Del;
		private System.Windows.Forms.ComboBox Cb_Command;
		private System.Windows.Forms.ComboBox Cb_Action;
		private System.Windows.Forms.Button Btn_Up;
		private System.Windows.Forms.Button Btn_Down;
		private System.Windows.Forms.CheckBox Ckb_All;
	}
}