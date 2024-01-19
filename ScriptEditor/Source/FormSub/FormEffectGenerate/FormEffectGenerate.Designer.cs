namespace ScriptEditor
{
	partial class FormEffectGenerate
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
			this.tbf_efZ = new ScriptEditor.TB_Float ();
			this.chb_efGnrt = new System.Windows.Forms.CheckBox ();
			this.chb_efLoop = new System.Windows.Forms.CheckBox ();
			this.chb_efSync = new System.Windows.Forms.CheckBox ();
			this.lbl_EfGnrtZ = new System.Windows.Forms.Label ();
			this.tBN_EfY = new ScriptEditor.TB_Number ();
			this.lbl_EfGn_Y = new System.Windows.Forms.Label ();
			this.tBN_EfX = new ScriptEditor.TB_Number ();
			this.lbl_EfGn_X = new System.Windows.Forms.Label ();
			this.lb_EfGnrt = new System.Windows.Forms.ListBox ();
			this.btnEfAdd = new System.Windows.Forms.Button ();
			this.btnEfDel = new System.Windows.Forms.Button ();
			this.CB_EffectList = new ScriptEditor.CB_SequenceList ();
			this.groupBox1.SuspendLayout ();
			this.SuspendLayout ();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add ( this.tbf_efZ );
			this.groupBox1.Controls.Add ( this.chb_efGnrt );
			this.groupBox1.Controls.Add ( this.chb_efLoop );
			this.groupBox1.Controls.Add ( this.chb_efSync );
			this.groupBox1.Controls.Add ( this.lbl_EfGnrtZ );
			this.groupBox1.Controls.Add ( this.tBN_EfY );
			this.groupBox1.Controls.Add ( this.lbl_EfGn_Y );
			this.groupBox1.Controls.Add ( this.tBN_EfX );
			this.groupBox1.Controls.Add ( this.lbl_EfGn_X );
			this.groupBox1.Controls.Add ( this.lb_EfGnrt );
			this.groupBox1.Controls.Add ( this.btnEfAdd );
			this.groupBox1.Controls.Add ( this.btnEfDel );
			this.groupBox1.Controls.Add ( this.CB_EffectList );
			this.groupBox1.Location = new System.Drawing.Point ( 9, 11 );
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size ( 346, 226 );
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Effect";
			// 
			// tbf_efZ
			// 
			this.tbf_efZ.dispCompend = null;
			this.tbf_efZ.editCompend = null;
			this.tbf_efZ.Location = new System.Drawing.Point ( 288, 25 );
			this.tbf_efZ.Name = "tbf_efZ";
			this.tbf_efZ.refFlaot = null;
			this.tbf_efZ.scriptAddress = null;
			this.tbf_efZ.Size = new System.Drawing.Size ( 36, 19 );
			this.tbf_efZ.TabIndex = 9;
			// 
			// chb_efGnrt
			// 
			this.chb_efGnrt.AutoSize = true;
			this.chb_efGnrt.Location = new System.Drawing.Point ( 163, 65 );
			this.chb_efGnrt.Name = "chb_efGnrt";
			this.chb_efGnrt.Size = new System.Drawing.Size ( 48, 16 );
			this.chb_efGnrt.TabIndex = 8;
			this.chb_efGnrt.Text = "生成";
			this.chb_efGnrt.UseVisualStyleBackColor = true;
			this.chb_efGnrt.CheckedChanged += new System.EventHandler ( this.chb_efGnrt_CheckedChanged );
			// 
			// chb_efLoop
			// 
			this.chb_efLoop.AutoSize = true;
			this.chb_efLoop.Location = new System.Drawing.Point ( 163, 87 );
			this.chb_efLoop.Name = "chb_efLoop";
			this.chb_efLoop.Size = new System.Drawing.Size ( 53, 16 );
			this.chb_efLoop.TabIndex = 8;
			this.chb_efLoop.Text = "ループ";
			this.chb_efLoop.UseVisualStyleBackColor = true;
			this.chb_efLoop.CheckedChanged += new System.EventHandler ( this.chb_efLoop_CheckedChanged );
			// 
			// chb_efSync
			// 
			this.chb_efSync.AutoSize = true;
			this.chb_efSync.Location = new System.Drawing.Point ( 163, 109 );
			this.chb_efSync.Name = "chb_efSync";
			this.chb_efSync.Size = new System.Drawing.Size ( 48, 16 );
			this.chb_efSync.TabIndex = 8;
			this.chb_efSync.Text = "同期";
			this.chb_efSync.UseVisualStyleBackColor = true;
			this.chb_efSync.CheckedChanged += new System.EventHandler ( this.chkEfSync_CheckedChanged );
			// 
			// lbl_EfGnrtZ
			// 
			this.lbl_EfGnrtZ.AutoSize = true;
			this.lbl_EfGnrtZ.Location = new System.Drawing.Point ( 270, 28 );
			this.lbl_EfGnrtZ.Name = "lbl_EfGnrtZ";
			this.lbl_EfGnrtZ.Size = new System.Drawing.Size ( 12, 12 );
			this.lbl_EfGnrtZ.TabIndex = 6;
			this.lbl_EfGnrtZ.Text = "Z";
			// 
			// tBN_EfY
			// 
			this.tBN_EfY.dispCompend = null;
			this.tBN_EfY.editCompend = null;
			this.tBN_EfY.Location = new System.Drawing.Point ( 222, 25 );
			this.tBN_EfY.Name = "tBN_EfY";
			this.tBN_EfY.refInt = null;
			this.tBN_EfY.scriptAddress = null;
			this.tBN_EfY.Size = new System.Drawing.Size ( 33, 19 );
			this.tBN_EfY.TabIndex = 7;
			// 
			// lbl_EfGn_Y
			// 
			this.lbl_EfGn_Y.AutoSize = true;
			this.lbl_EfGn_Y.Location = new System.Drawing.Point ( 204, 28 );
			this.lbl_EfGn_Y.Name = "lbl_EfGn_Y";
			this.lbl_EfGn_Y.Size = new System.Drawing.Size ( 12, 12 );
			this.lbl_EfGn_Y.TabIndex = 6;
			this.lbl_EfGn_Y.Text = "Y";
			// 
			// tBN_EfX
			// 
			this.tBN_EfX.dispCompend = null;
			this.tBN_EfX.editCompend = null;
			this.tBN_EfX.Location = new System.Drawing.Point ( 163, 25 );
			this.tBN_EfX.Name = "tBN_EfX";
			this.tBN_EfX.refInt = null;
			this.tBN_EfX.scriptAddress = null;
			this.tBN_EfX.Size = new System.Drawing.Size ( 35, 19 );
			this.tBN_EfX.TabIndex = 5;
			// 
			// lbl_EfGn_X
			// 
			this.lbl_EfGn_X.AutoSize = true;
			this.lbl_EfGn_X.Location = new System.Drawing.Point ( 148, 28 );
			this.lbl_EfGn_X.Name = "lbl_EfGn_X";
			this.lbl_EfGn_X.Size = new System.Drawing.Size ( 12, 12 );
			this.lbl_EfGn_X.TabIndex = 4;
			this.lbl_EfGn_X.Text = "X";
			// 
			// lb_EfGnrt
			// 
			this.lb_EfGnrt.FormattingEnabled = true;
			this.lb_EfGnrt.ItemHeight = 12;
			this.lb_EfGnrt.Location = new System.Drawing.Point ( 6, 18 );
			this.lb_EfGnrt.Name = "lb_EfGnrt";
			this.lb_EfGnrt.Size = new System.Drawing.Size ( 136, 136 );
			this.lb_EfGnrt.TabIndex = 3;
			this.lb_EfGnrt.SelectedIndexChanged += new System.EventHandler ( this.lb_EfGnrt_SelectedIndexChanged );
			// 
			// btnEfAdd
			// 
			this.btnEfAdd.Location = new System.Drawing.Point ( 72, 196 );
			this.btnEfAdd.Name = "btnEfAdd";
			this.btnEfAdd.Size = new System.Drawing.Size ( 70, 24 );
			this.btnEfAdd.TabIndex = 2;
			this.btnEfAdd.Text = "追加";
			this.btnEfAdd.UseVisualStyleBackColor = true;
			this.btnEfAdd.Click += new System.EventHandler ( this.btnEfAdd_Click );
			// 
			// btnEfDel
			// 
			this.btnEfDel.Location = new System.Drawing.Point ( 6, 196 );
			this.btnEfDel.Name = "btnEfDel";
			this.btnEfDel.Size = new System.Drawing.Size ( 60, 24 );
			this.btnEfDel.TabIndex = 2;
			this.btnEfDel.Text = "削除";
			this.btnEfDel.UseVisualStyleBackColor = true;
			this.btnEfDel.Click += new System.EventHandler ( this.btnEfDel_Click );
			// 
			// CB_EffectList
			// 
			this.CB_EffectList.dispCompend = null;
			this.CB_EffectList.FormattingEnabled = true;
			this.CB_EffectList.Location = new System.Drawing.Point ( 6, 170 );
			this.CB_EffectList.Name = "CB_EffectList";
			this.CB_EffectList.refInt = null;
			this.CB_EffectList.Size = new System.Drawing.Size ( 136, 20 );
			this.CB_EffectList.TabIndex = 1;
			this.CB_EffectList.SelectedIndexChanged += new System.EventHandler ( this.CB_EffectList_SelectedIndexChanged );
			// 
			// FormEffectGenerate
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 12F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size ( 369, 250 );
			this.Controls.Add ( this.groupBox1 );
			this.Name = "FormEffectGenerate";
			this.Text = "FormEffectGenerate";
			this.groupBox1.ResumeLayout ( false );
			this.groupBox1.PerformLayout ();
			this.ResumeLayout ( false );

		}

		#endregion

		private ScriptEditor.CB_SequenceList CB_EffectList;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnEfAdd;
		private System.Windows.Forms.Button btnEfDel;
		private System.Windows.Forms.ListBox lb_EfGnrt;
		private TB_Number tBN_EfY;
		private System.Windows.Forms.Label lbl_EfGn_Y;
		private TB_Number tBN_EfX;
		private System.Windows.Forms.Label lbl_EfGn_X;
		private System.Windows.Forms.CheckBox chb_efSync;
		private System.Windows.Forms.CheckBox chb_efLoop;
		private System.Windows.Forms.Label lbl_EfGnrtZ;
		private TB_Float tbf_efZ;
		private System.Windows.Forms.CheckBox chb_efGnrt;

	}
}