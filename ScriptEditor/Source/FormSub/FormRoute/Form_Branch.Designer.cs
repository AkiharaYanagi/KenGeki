namespace ScriptEditor
{
	partial class Form_Branch
	{
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose ( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose ();
			}
			base.Dispose ( disposing );
		}

		#region Windows フォーム デザイナで生成されたコード

		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent ()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox ();
			this.tbNextAction = new System.Windows.Forms.TextBox ();
			this.label6 = new System.Windows.Forms.Label ();
			this.label5 = new System.Windows.Forms.Label ();
			this.label2 = new System.Windows.Forms.Label ();
			this.label8 = new System.Windows.Forms.Label ();
			this.btnDown = new System.Windows.Forms.Button ();
			this.btnUp = new System.Windows.Forms.Button ();
			this.cbTransitAction = new System.Windows.Forms.ComboBox ();
			this.cbConditionCommand = new System.Windows.Forms.ComboBox ();
			this.label7 = new System.Windows.Forms.Label ();
			this.btnDelete = new System.Windows.Forms.Button ();
			this.groupBox2 = new System.Windows.Forms.GroupBox ();
			this.lblAllBranch = new System.Windows.Forms.Label ();
			this.btnAddCondition = new System.Windows.Forms.Button ();
			this.lbTransit = new System.Windows.Forms.ListBox ();
			this.label3 = new System.Windows.Forms.Label ();
			this.label1 = new System.Windows.Forms.Label ();
			this.lbCondition = new System.Windows.Forms.ListBox ();
			this.tba_BranchFrame = new ScriptEditor.TB_Number ();
			this.lbl_F = new System.Windows.Forms.Label ();
			this.groupBox1.SuspendLayout ();
			this.groupBox2.SuspendLayout ();
			this.SuspendLayout ();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add ( this.tbNextAction );
			this.groupBox1.Controls.Add ( this.label6 );
			this.groupBox1.Controls.Add ( this.label5 );
			this.groupBox1.Controls.Add ( this.label2 );
			this.groupBox1.Controls.Add ( this.label8 );
			this.groupBox1.Location = new System.Drawing.Point ( 12, 12 );
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size ( 390, 63 );
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "ｱｸｼｮﾝ終了時";
			// 
			// tbNextAction
			// 
			this.tbNextAction.Location = new System.Drawing.Point ( 221, 33 );
			this.tbNextAction.Name = "tbNextAction";
			this.tbNextAction.ReadOnly = true;
			this.tbNextAction.Size = new System.Drawing.Size ( 139, 19 );
			this.tbNextAction.TabIndex = 6;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point ( 182, 36 );
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size ( 17, 12 );
			this.label6.TabIndex = 5;
			this.label6.Text = "→";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font ( "MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 128 ) ) );
			this.label5.Location = new System.Drawing.Point ( 54, 35 );
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size ( 82, 13 );
			this.label5.TabIndex = 4;
			this.label5.Text = "ｱｸｼｮﾝ終了時";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font ( "MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 128 ) ) );
			this.label2.Location = new System.Drawing.Point ( 252, 15 );
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size ( 55, 15 );
			this.label2.TabIndex = 3;
			this.label2.Text = "遷移先";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font ( "MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 128 ) ) );
			this.label8.Location = new System.Drawing.Point ( 70, 15 );
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size ( 39, 15 );
			this.label8.TabIndex = 1;
			this.label8.Text = "条件";
			// 
			// btnDown
			// 
			this.btnDown.Location = new System.Drawing.Point ( 297, 286 );
			this.btnDown.Name = "btnDown";
			this.btnDown.Size = new System.Drawing.Size ( 66, 28 );
			this.btnDown.TabIndex = 11;
			this.btnDown.Text = "↓";
			this.btnDown.UseVisualStyleBackColor = true;
			this.btnDown.Click += new System.EventHandler ( this.btnDown_Click );
			// 
			// btnUp
			// 
			this.btnUp.Location = new System.Drawing.Point ( 221, 287 );
			this.btnUp.Name = "btnUp";
			this.btnUp.Size = new System.Drawing.Size ( 67, 27 );
			this.btnUp.TabIndex = 10;
			this.btnUp.Text = "↑";
			this.btnUp.UseVisualStyleBackColor = true;
			this.btnUp.Click += new System.EventHandler ( this.btnUp_Click );
			// 
			// cbTransitAction
			// 
			this.cbTransitAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTransitAction.FormattingEnabled = true;
			this.cbTransitAction.Location = new System.Drawing.Point ( 221, 238 );
			this.cbTransitAction.Name = "cbTransitAction";
			this.cbTransitAction.Size = new System.Drawing.Size ( 139, 20 );
			this.cbTransitAction.TabIndex = 9;
			this.cbTransitAction.SelectedIndexChanged += new System.EventHandler ( this.cbTransitAction_SelectedIndexChanged );
			this.cbTransitAction.DropDownClosed += new System.EventHandler ( this.cbTransitAction_DropDownClosed );
			// 
			// cbConditionCommand
			// 
			this.cbConditionCommand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbConditionCommand.FormattingEnabled = true;
			this.cbConditionCommand.Location = new System.Drawing.Point ( 21, 238 );
			this.cbConditionCommand.Name = "cbConditionCommand";
			this.cbConditionCommand.Size = new System.Drawing.Size ( 140, 20 );
			this.cbConditionCommand.TabIndex = 8;
			this.cbConditionCommand.SelectedIndexChanged += new System.EventHandler ( this.cbConditionCommand_SelectedIndexChanged );
			this.cbConditionCommand.DropDownClosed += new System.EventHandler ( this.cbConditionCommand_DropDownClosed );
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point ( 182, 133 );
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size ( 17, 12 );
			this.label7.TabIndex = 5;
			this.label7.Text = "→";
			// 
			// btnDelete
			// 
			this.btnDelete.Location = new System.Drawing.Point ( 116, 287 );
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size ( 83, 28 );
			this.btnDelete.TabIndex = 5;
			this.btnDelete.Text = "削除";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler ( this.btnDelete_Click );
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add ( this.lbl_F );
			this.groupBox2.Controls.Add ( this.tba_BranchFrame );
			this.groupBox2.Controls.Add ( this.lblAllBranch );
			this.groupBox2.Controls.Add ( this.btnDown );
			this.groupBox2.Controls.Add ( this.btnUp );
			this.groupBox2.Controls.Add ( this.cbTransitAction );
			this.groupBox2.Controls.Add ( this.cbConditionCommand );
			this.groupBox2.Controls.Add ( this.label7 );
			this.groupBox2.Controls.Add ( this.btnDelete );
			this.groupBox2.Controls.Add ( this.btnAddCondition );
			this.groupBox2.Controls.Add ( this.lbTransit );
			this.groupBox2.Controls.Add ( this.label3 );
			this.groupBox2.Controls.Add ( this.label1 );
			this.groupBox2.Controls.Add ( this.lbCondition );
			this.groupBox2.Location = new System.Drawing.Point ( 12, 81 );
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size ( 390, 321 );
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "ｺﾏﾝﾄﾞ入力時";
			// 
			// lblAllBranch
			// 
			this.lblAllBranch.AutoSize = true;
			this.lblAllBranch.BackColor = System.Drawing.SystemColors.ControlLight;
			this.lblAllBranch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblAllBranch.Location = new System.Drawing.Point ( 19, 271 );
			this.lblAllBranch.Name = "lblAllBranch";
			this.lblAllBranch.Size = new System.Drawing.Size ( 21, 14 );
			this.lblAllBranch.TabIndex = 12;
			this.lblAllBranch.Text = "All";
			// 
			// btnAddCondition
			// 
			this.btnAddCondition.Location = new System.Drawing.Point ( 19, 288 );
			this.btnAddCondition.Name = "btnAddCondition";
			this.btnAddCondition.Size = new System.Drawing.Size ( 88, 27 );
			this.btnAddCondition.TabIndex = 4;
			this.btnAddCondition.Text = "追加";
			this.btnAddCondition.UseVisualStyleBackColor = true;
			this.btnAddCondition.Click += new System.EventHandler ( this.btnAddCondition_Click );
			// 
			// lbTransit
			// 
			this.lbTransit.FormattingEnabled = true;
			this.lbTransit.ItemHeight = 12;
			this.lbTransit.Location = new System.Drawing.Point ( 221, 36 );
			this.lbTransit.Name = "lbTransit";
			this.lbTransit.Size = new System.Drawing.Size ( 139, 196 );
			this.lbTransit.TabIndex = 3;
			this.lbTransit.SelectedIndexChanged += new System.EventHandler ( this.lbTransit_SelectedIndexChanged );
			this.lbTransit.MouseDown += new System.Windows.Forms.MouseEventHandler ( this.lbTransit_MouseDown );
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font ( "MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 128 ) ) );
			this.label3.Location = new System.Drawing.Point ( 218, 15 );
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size ( 55, 15 );
			this.label3.TabIndex = 2;
			this.label3.Text = "遷移先";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font ( "MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 128 ) ) );
			this.label1.Location = new System.Drawing.Point ( 18, 14 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size ( 39, 15 );
			this.label1.TabIndex = 1;
			this.label1.Text = "条件";
			// 
			// lbCondition
			// 
			this.lbCondition.FormattingEnabled = true;
			this.lbCondition.ItemHeight = 12;
			this.lbCondition.Location = new System.Drawing.Point ( 21, 36 );
			this.lbCondition.Name = "lbCondition";
			this.lbCondition.Size = new System.Drawing.Size ( 140, 196 );
			this.lbCondition.TabIndex = 0;
			this.lbCondition.SelectedIndexChanged += new System.EventHandler ( this.lbCondition_SelectedIndexChanged );
			this.lbCondition.MouseDown += new System.Windows.Forms.MouseEventHandler ( this.lbCondition_MouseDown );
			// 
			// tba_BranchFrame
			// 
			this.tba_BranchFrame.Location = new System.Drawing.Point ( 294, 14 );
			this.tba_BranchFrame.Name = "tba_BranchFrame";
			this.tba_BranchFrame.refInt = null;
			this.tba_BranchFrame.scriptAddress = null;
			this.tba_BranchFrame.Size = new System.Drawing.Size ( 43, 19 );
			this.tba_BranchFrame.TabIndex = 13;
			// 
			// lbl_F
			// 
			this.lbl_F.AutoSize = true;
			this.lbl_F.Location = new System.Drawing.Point ( 343, 17 );
			this.lbl_F.Name = "lbl_F";
			this.lbl_F.Size = new System.Drawing.Size ( 20, 12 );
			this.lbl_F.TabIndex = 14;
			this.lbl_F.Text = "[F]";
			// 
			// Form_Branch
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 12F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size ( 415, 415 );
			this.Controls.Add ( this.groupBox1 );
			this.Controls.Add ( this.groupBox2 );
			this.Name = "Form_Branch";
			this.Text = "Form_Branch";
			this.TopMost = true;
			this.groupBox1.ResumeLayout ( false );
			this.groupBox1.PerformLayout ();
			this.groupBox2.ResumeLayout ( false );
			this.groupBox2.PerformLayout ();
			this.ResumeLayout ( false );

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button btnDown;
		private System.Windows.Forms.Button btnUp;
		private System.Windows.Forms.ComboBox cbTransitAction;
		private System.Windows.Forms.ComboBox cbConditionCommand;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnAddCondition;
		private System.Windows.Forms.ListBox lbTransit;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox lbCondition;
		private System.Windows.Forms.TextBox tbNextAction;
		private System.Windows.Forms.Label lblAllBranch;
		private System.Windows.Forms.Label lbl_F;
		private TB_Number tba_BranchFrame;
	}
}