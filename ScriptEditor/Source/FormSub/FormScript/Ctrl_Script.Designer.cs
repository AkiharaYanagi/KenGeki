namespace ScriptEditor
{
	partial class Ctrl_Script
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
			this.cB_ClcSt = new System.Windows.Forms.ComboBox();
			this.tB_Frame = new System.Windows.Forms.TextBox();
			this.lbl_clc = new System.Windows.Forms.Label();
			this.lbl_Vel = new System.Windows.Forms.Label();
			this.lbl_Frame = new System.Windows.Forms.Label();
			this.lbl_AP = new System.Windows.Forms.Label();
			this.lbl_Pos = new System.Windows.Forms.Label();
			this.lbl_Acc = new System.Windows.Forms.Label();
			this.tBN_Power = new ScriptEditor.TB_Number();
			this.Lbl_Img = new System.Windows.Forms.Label();
			this.Tb_Img = new System.Windows.Forms.TextBox();
			this.Lbl_BlackOut = new System.Windows.Forms.Label();
			this.tBN_BlackOut = new ScriptEditor.TB_Number();
			this.Lbl_Vibration = new System.Windows.Forms.Label();
			this.tBN_Vibration = new ScriptEditor.TB_Number();
			this.Lbl_Stop = new System.Windows.Forms.Label();
			this.tBN_Stop = new ScriptEditor.TB_Number();
			this.RB_TRG_ALL = new System.Windows.Forms.RadioButton();
			this.RB_TRG_GRP = new System.Windows.Forms.RadioButton();
			this.RB_TRG_SGL = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.Lbl_Rad = new System.Windows.Forms.Label();
			this.tB_Number1 = new ScriptEditor.TB_Number();
			this.tB_Number2 = new ScriptEditor.TB_Number();
			this.Lbl_AI_Time = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.tB_Number4 = new ScriptEditor.TB_Number();
			this.tB_Number3 = new ScriptEditor.TB_Number();
			this.Lbl_AI_N = new System.Windows.Forms.Label();
			this.Lbl_AI_Pitch = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.tB_Number5 = new ScriptEditor.TB_Number();
			this.tB_Number7 = new ScriptEditor.TB_Number();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.Btn_PasteSqc = new System.Windows.Forms.Button();
			this.Btn_PasteGroup = new System.Windows.Forms.Button();
			this.Btn_Copy = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.TBSN_AccY = new ScriptEditor.TB_ScpNumber();
			this.TBSN_AccX = new ScriptEditor.TB_ScpNumber();
			this.TBSN_VelY = new ScriptEditor.TB_ScpNumber();
			this.TBSN_VelX = new ScriptEditor.TB_ScpNumber();
			this.TBSN_PosY = new ScriptEditor.TB_ScpNumber();
			this.TBSN_PosX = new ScriptEditor.TB_ScpNumber();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// cB_ClcSt
			// 
			this.cB_ClcSt.FormattingEnabled = true;
			this.cB_ClcSt.Location = new System.Drawing.Point(75, 87);
			this.cB_ClcSt.Margin = new System.Windows.Forms.Padding(2);
			this.cB_ClcSt.Name = "cB_ClcSt";
			this.cB_ClcSt.Size = new System.Drawing.Size(74, 20);
			this.cB_ClcSt.TabIndex = 15;
			this.cB_ClcSt.SelectedIndexChanged += new System.EventHandler(this.cB_ClcSt_SelectedIndexChanged);
			this.cB_ClcSt.SelectionChangeCommitted += new System.EventHandler(this.cB_ClcSt_SelectionChangeCommitted);
			// 
			// tB_Frame
			// 
			this.tB_Frame.BackColor = System.Drawing.SystemColors.Control;
			this.tB_Frame.Cursor = System.Windows.Forms.Cursors.Default;
			this.tB_Frame.Location = new System.Drawing.Point(75, 62);
			this.tB_Frame.Margin = new System.Windows.Forms.Padding(2);
			this.tB_Frame.Name = "tB_Frame";
			this.tB_Frame.ReadOnly = true;
			this.tB_Frame.Size = new System.Drawing.Size(30, 19);
			this.tB_Frame.TabIndex = 14;
			// 
			// lbl_clc
			// 
			this.lbl_clc.AutoSize = true;
			this.lbl_clc.Location = new System.Drawing.Point(27, 87);
			this.lbl_clc.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lbl_clc.Name = "lbl_clc";
			this.lbl_clc.Size = new System.Drawing.Size(33, 12);
			this.lbl_clc.TabIndex = 7;
			this.lbl_clc.Text = "ClcSt";
			// 
			// lbl_Vel
			// 
			this.lbl_Vel.AutoSize = true;
			this.lbl_Vel.Location = new System.Drawing.Point(18, 145);
			this.lbl_Vel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lbl_Vel.Name = "lbl_Vel";
			this.lbl_Vel.Size = new System.Drawing.Size(46, 12);
			this.lbl_Vel.TabIndex = 8;
			this.lbl_Vel.Text = "Vel(X,Y)";
			// 
			// lbl_Frame
			// 
			this.lbl_Frame.AutoSize = true;
			this.lbl_Frame.Location = new System.Drawing.Point(23, 65);
			this.lbl_Frame.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lbl_Frame.Name = "lbl_Frame";
			this.lbl_Frame.Size = new System.Drawing.Size(37, 12);
			this.lbl_Frame.TabIndex = 9;
			this.lbl_Frame.Text = "Frame";
			// 
			// lbl_AP
			// 
			this.lbl_AP.AutoSize = true;
			this.lbl_AP.Location = new System.Drawing.Point(173, 91);
			this.lbl_AP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lbl_AP.Name = "lbl_AP";
			this.lbl_AP.Size = new System.Drawing.Size(70, 12);
			this.lbl_AP.TabIndex = 10;
			this.lbl_AP.Text = "AttackPower";
			// 
			// lbl_Pos
			// 
			this.lbl_Pos.AutoSize = true;
			this.lbl_Pos.Location = new System.Drawing.Point(16, 123);
			this.lbl_Pos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lbl_Pos.Name = "lbl_Pos";
			this.lbl_Pos.Size = new System.Drawing.Size(48, 12);
			this.lbl_Pos.TabIndex = 8;
			this.lbl_Pos.Text = "Pos(X,Y)";
			// 
			// lbl_Acc
			// 
			this.lbl_Acc.AutoSize = true;
			this.lbl_Acc.Location = new System.Drawing.Point(15, 170);
			this.lbl_Acc.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lbl_Acc.Name = "lbl_Acc";
			this.lbl_Acc.Size = new System.Drawing.Size(49, 12);
			this.lbl_Acc.TabIndex = 8;
			this.lbl_Acc.Text = "Acc(X,Y)";
			// 
			// tBN_Power
			// 
			this.tBN_Power.GetFunc = null;
			this.tBN_Power.Location = new System.Drawing.Point(247, 88);
			this.tBN_Power.Margin = new System.Windows.Forms.Padding(2);
			this.tBN_Power.Name = "tBN_Power";
			this.tBN_Power.SetFunc = null;
			this.tBN_Power.Size = new System.Drawing.Size(54, 19);
			this.tBN_Power.TabIndex = 18;
			this.tBN_Power.Text = "0";
			// 
			// Lbl_Img
			// 
			this.Lbl_Img.AutoSize = true;
			this.Lbl_Img.Location = new System.Drawing.Point(176, 65);
			this.Lbl_Img.Name = "Lbl_Img";
			this.Lbl_Img.Size = new System.Drawing.Size(23, 12);
			this.Lbl_Img.TabIndex = 21;
			this.Lbl_Img.Text = "Img";
			// 
			// Tb_Img
			// 
			this.Tb_Img.Location = new System.Drawing.Point(213, 62);
			this.Tb_Img.Name = "Tb_Img";
			this.Tb_Img.ReadOnly = true;
			this.Tb_Img.Size = new System.Drawing.Size(89, 19);
			this.Tb_Img.TabIndex = 22;
			// 
			// Lbl_BlackOut
			// 
			this.Lbl_BlackOut.AutoSize = true;
			this.Lbl_BlackOut.Location = new System.Drawing.Point(13, 231);
			this.Lbl_BlackOut.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.Lbl_BlackOut.Name = "Lbl_BlackOut";
			this.Lbl_BlackOut.Size = new System.Drawing.Size(29, 12);
			this.Lbl_BlackOut.TabIndex = 10;
			this.Lbl_BlackOut.Text = "暗転";
			// 
			// tBN_BlackOut
			// 
			this.tBN_BlackOut.GetFunc = null;
			this.tBN_BlackOut.Location = new System.Drawing.Point(75, 228);
			this.tBN_BlackOut.Margin = new System.Windows.Forms.Padding(2);
			this.tBN_BlackOut.Name = "tBN_BlackOut";
			this.tBN_BlackOut.SetFunc = null;
			this.tBN_BlackOut.Size = new System.Drawing.Size(42, 19);
			this.tBN_BlackOut.TabIndex = 18;
			this.tBN_BlackOut.Text = "0";
			// 
			// Lbl_Vibration
			// 
			this.Lbl_Vibration.AutoSize = true;
			this.Lbl_Vibration.Location = new System.Drawing.Point(14, 254);
			this.Lbl_Vibration.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.Lbl_Vibration.Name = "Lbl_Vibration";
			this.Lbl_Vibration.Size = new System.Drawing.Size(29, 12);
			this.Lbl_Vibration.TabIndex = 10;
			this.Lbl_Vibration.Text = "振動";
			// 
			// tBN_Vibration
			// 
			this.tBN_Vibration.GetFunc = null;
			this.tBN_Vibration.Location = new System.Drawing.Point(75, 251);
			this.tBN_Vibration.Margin = new System.Windows.Forms.Padding(2);
			this.tBN_Vibration.Name = "tBN_Vibration";
			this.tBN_Vibration.SetFunc = null;
			this.tBN_Vibration.Size = new System.Drawing.Size(42, 19);
			this.tBN_Vibration.TabIndex = 18;
			this.tBN_Vibration.Text = "0";
			// 
			// Lbl_Stop
			// 
			this.Lbl_Stop.AutoSize = true;
			this.Lbl_Stop.Location = new System.Drawing.Point(13, 67);
			this.Lbl_Stop.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.Lbl_Stop.Name = "Lbl_Stop";
			this.Lbl_Stop.Size = new System.Drawing.Size(29, 12);
			this.Lbl_Stop.TabIndex = 10;
			this.Lbl_Stop.Text = "停止";
			// 
			// tBN_Stop
			// 
			this.tBN_Stop.GetFunc = null;
			this.tBN_Stop.Location = new System.Drawing.Point(75, 274);
			this.tBN_Stop.Margin = new System.Windows.Forms.Padding(2);
			this.tBN_Stop.Name = "tBN_Stop";
			this.tBN_Stop.SetFunc = null;
			this.tBN_Stop.Size = new System.Drawing.Size(42, 19);
			this.tBN_Stop.TabIndex = 18;
			this.tBN_Stop.Text = "0";
			// 
			// RB_TRG_ALL
			// 
			this.RB_TRG_ALL.AutoSize = true;
			this.RB_TRG_ALL.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.RB_TRG_ALL.Location = new System.Drawing.Point(26, 13);
			this.RB_TRG_ALL.Name = "RB_TRG_ALL";
			this.RB_TRG_ALL.Size = new System.Drawing.Size(42, 20);
			this.RB_TRG_ALL.TabIndex = 23;
			this.RB_TRG_ALL.Text = "All";
			this.RB_TRG_ALL.UseVisualStyleBackColor = true;
			this.RB_TRG_ALL.CheckedChanged += new System.EventHandler(this.RB_TRG_ALL_CheckedChanged);
			// 
			// RB_TRG_GRP
			// 
			this.RB_TRG_GRP.AutoSize = true;
			this.RB_TRG_GRP.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.RB_TRG_GRP.Location = new System.Drawing.Point(87, 13);
			this.RB_TRG_GRP.Name = "RB_TRG_GRP";
			this.RB_TRG_GRP.Size = new System.Drawing.Size(67, 20);
			this.RB_TRG_GRP.TabIndex = 24;
			this.RB_TRG_GRP.Text = "Group";
			this.RB_TRG_GRP.UseVisualStyleBackColor = true;
			this.RB_TRG_GRP.CheckedChanged += new System.EventHandler(this.RB_TRG_GRP_CheckedChanged);
			// 
			// RB_TRG_SGL
			// 
			this.RB_TRG_SGL.AutoSize = true;
			this.RB_TRG_SGL.Checked = true;
			this.RB_TRG_SGL.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.RB_TRG_SGL.Location = new System.Drawing.Point(170, 13);
			this.RB_TRG_SGL.Name = "RB_TRG_SGL";
			this.RB_TRG_SGL.Size = new System.Drawing.Size(65, 20);
			this.RB_TRG_SGL.TabIndex = 25;
			this.RB_TRG_SGL.TabStop = true;
			this.RB_TRG_SGL.Text = "Single";
			this.RB_TRG_SGL.UseVisualStyleBackColor = true;
			this.RB_TRG_SGL.CheckedChanged += new System.EventHandler(this.RB_TRG_SGL_CheckedChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.RB_TRG_SGL);
			this.groupBox1.Controls.Add(this.RB_TRG_GRP);
			this.groupBox1.Controls.Add(this.RB_TRG_ALL);
			this.groupBox1.Location = new System.Drawing.Point(8, 7);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(246, 43);
			this.groupBox1.TabIndex = 26;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Target";
			// 
			// Lbl_Rad
			// 
			this.Lbl_Rad.AutoSize = true;
			this.Lbl_Rad.Location = new System.Drawing.Point(198, 126);
			this.Lbl_Rad.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.Lbl_Rad.Name = "Lbl_Rad";
			this.Lbl_Rad.Size = new System.Drawing.Size(40, 12);
			this.Lbl_Rad.TabIndex = 10;
			this.Lbl_Rad.Text = "Radian";
			// 
			// tB_Number1
			// 
			this.tB_Number1.GetFunc = null;
			this.tB_Number1.Location = new System.Drawing.Point(260, 123);
			this.tB_Number1.Margin = new System.Windows.Forms.Padding(2);
			this.tB_Number1.Name = "tB_Number1";
			this.tB_Number1.SetFunc = null;
			this.tB_Number1.Size = new System.Drawing.Size(42, 19);
			this.tB_Number1.TabIndex = 18;
			this.tB_Number1.Text = "0";
			// 
			// tB_Number2
			// 
			this.tB_Number2.GetFunc = null;
			this.tB_Number2.Location = new System.Drawing.Point(68, 18);
			this.tB_Number2.Margin = new System.Windows.Forms.Padding(2);
			this.tB_Number2.Name = "tB_Number2";
			this.tB_Number2.SetFunc = null;
			this.tB_Number2.Size = new System.Drawing.Size(42, 19);
			this.tB_Number2.TabIndex = 18;
			this.tB_Number2.Text = "0";
			// 
			// Lbl_AI_Time
			// 
			this.Lbl_AI_Time.AutoSize = true;
			this.Lbl_AI_Time.Location = new System.Drawing.Point(19, 21);
			this.Lbl_AI_Time.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.Lbl_AI_Time.Name = "Lbl_AI_Time";
			this.Lbl_AI_Time.Size = new System.Drawing.Size(29, 12);
			this.Lbl_AI_Time.TabIndex = 10;
			this.Lbl_AI_Time.Text = "時間";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.tB_Number4);
			this.groupBox2.Controls.Add(this.tB_Number3);
			this.groupBox2.Controls.Add(this.tB_Number2);
			this.groupBox2.Controls.Add(this.Lbl_AI_N);
			this.groupBox2.Controls.Add(this.Lbl_AI_Pitch);
			this.groupBox2.Controls.Add(this.Lbl_AI_Time);
			this.groupBox2.Location = new System.Drawing.Point(192, 210);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(126, 102);
			this.groupBox2.TabIndex = 28;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "残像";
			// 
			// tB_Number4
			// 
			this.tB_Number4.GetFunc = null;
			this.tB_Number4.Location = new System.Drawing.Point(68, 64);
			this.tB_Number4.Margin = new System.Windows.Forms.Padding(2);
			this.tB_Number4.Name = "tB_Number4";
			this.tB_Number4.SetFunc = null;
			this.tB_Number4.Size = new System.Drawing.Size(42, 19);
			this.tB_Number4.TabIndex = 18;
			this.tB_Number4.Text = "0";
			// 
			// tB_Number3
			// 
			this.tB_Number3.GetFunc = null;
			this.tB_Number3.Location = new System.Drawing.Point(68, 41);
			this.tB_Number3.Margin = new System.Windows.Forms.Padding(2);
			this.tB_Number3.Name = "tB_Number3";
			this.tB_Number3.SetFunc = null;
			this.tB_Number3.Size = new System.Drawing.Size(42, 19);
			this.tB_Number3.TabIndex = 18;
			this.tB_Number3.Text = "0";
			// 
			// Lbl_AI_N
			// 
			this.Lbl_AI_N.AutoSize = true;
			this.Lbl_AI_N.Location = new System.Drawing.Point(19, 67);
			this.Lbl_AI_N.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.Lbl_AI_N.Name = "Lbl_AI_N";
			this.Lbl_AI_N.Size = new System.Drawing.Size(29, 12);
			this.Lbl_AI_N.TabIndex = 10;
			this.Lbl_AI_N.Text = "個数";
			// 
			// Lbl_AI_Pitch
			// 
			this.Lbl_AI_Pitch.AutoSize = true;
			this.Lbl_AI_Pitch.Location = new System.Drawing.Point(19, 44);
			this.Lbl_AI_Pitch.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.Lbl_AI_Pitch.Name = "Lbl_AI_Pitch";
			this.Lbl_AI_Pitch.Size = new System.Drawing.Size(29, 12);
			this.Lbl_AI_Pitch.TabIndex = 10;
			this.Lbl_AI_Pitch.Text = "間隔";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.Lbl_Stop);
			this.groupBox3.Location = new System.Drawing.Point(0, 210);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(149, 102);
			this.groupBox3.TabIndex = 29;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "全体";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 344);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 10;
			this.label1.Text = "個別振動";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(26, 367);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(17, 12);
			this.label2.TabIndex = 10;
			this.label2.Text = "色";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(11, 390);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 12);
			this.label3.TabIndex = 10;
			this.label3.Text = "色時間";
			// 
			// tB_Number5
			// 
			this.tB_Number5.GetFunc = null;
			this.tB_Number5.Location = new System.Drawing.Point(75, 341);
			this.tB_Number5.Margin = new System.Windows.Forms.Padding(2);
			this.tB_Number5.Name = "tB_Number5";
			this.tB_Number5.SetFunc = null;
			this.tB_Number5.Size = new System.Drawing.Size(42, 19);
			this.tB_Number5.TabIndex = 18;
			this.tB_Number5.Text = "0";
			// 
			// tB_Number7
			// 
			this.tB_Number7.GetFunc = null;
			this.tB_Number7.Location = new System.Drawing.Point(75, 387);
			this.tB_Number7.Margin = new System.Windows.Forms.Padding(2);
			this.tB_Number7.Name = "tB_Number7";
			this.tB_Number7.SetFunc = null;
			this.tB_Number7.Size = new System.Drawing.Size(42, 19);
			this.tB_Number7.TabIndex = 18;
			this.tB_Number7.Text = "0";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.Btn_PasteSqc);
			this.groupBox4.Controls.Add(this.Btn_PasteGroup);
			this.groupBox4.Controls.Add(this.Btn_Copy);
			this.groupBox4.Controls.Add(this.textBox1);
			this.groupBox4.Location = new System.Drawing.Point(355, 17);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(180, 167);
			this.groupBox4.TabIndex = 31;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "複製";
			// 
			// Btn_PasteSqc
			// 
			this.Btn_PasteSqc.Location = new System.Drawing.Point(22, 134);
			this.Btn_PasteSqc.Name = "Btn_PasteSqc";
			this.Btn_PasteSqc.Size = new System.Drawing.Size(136, 25);
			this.Btn_PasteSqc.TabIndex = 2;
			this.Btn_PasteSqc.Text = "シークエンスにペースト";
			this.Btn_PasteSqc.UseVisualStyleBackColor = true;
			// 
			// Btn_PasteGroup
			// 
			this.Btn_PasteGroup.Location = new System.Drawing.Point(22, 103);
			this.Btn_PasteGroup.Name = "Btn_PasteGroup";
			this.Btn_PasteGroup.Size = new System.Drawing.Size(136, 25);
			this.Btn_PasteGroup.TabIndex = 2;
			this.Btn_PasteGroup.Text = "グループにペースト";
			this.Btn_PasteGroup.UseVisualStyleBackColor = true;
			// 
			// Btn_Copy
			// 
			this.Btn_Copy.Location = new System.Drawing.Point(16, 19);
			this.Btn_Copy.Name = "Btn_Copy";
			this.Btn_Copy.Size = new System.Drawing.Size(108, 23);
			this.Btn_Copy.TabIndex = 1;
			this.Btn_Copy.Text = "コピー";
			this.Btn_Copy.UseVisualStyleBackColor = true;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(16, 48);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(143, 19);
			this.textBox1.TabIndex = 0;
			// 
			// TBSN_AccY
			// 
			this.TBSN_AccY.Location = new System.Drawing.Point(126, 166);
			this.TBSN_AccY.Name = "TBSN_AccY";
			this.TBSN_AccY.Scp = null;
			this.TBSN_AccY.Size = new System.Drawing.Size(36, 19);
			this.TBSN_AccY.TabIndex = 27;
			this.TBSN_AccY.Text = "0";
			// 
			// TBSN_AccX
			// 
			this.TBSN_AccX.Location = new System.Drawing.Point(75, 166);
			this.TBSN_AccX.Name = "TBSN_AccX";
			this.TBSN_AccX.Scp = null;
			this.TBSN_AccX.Size = new System.Drawing.Size(36, 19);
			this.TBSN_AccX.TabIndex = 27;
			this.TBSN_AccX.Text = "0";
			// 
			// TBSN_VelY
			// 
			this.TBSN_VelY.Location = new System.Drawing.Point(126, 143);
			this.TBSN_VelY.Name = "TBSN_VelY";
			this.TBSN_VelY.Scp = null;
			this.TBSN_VelY.Size = new System.Drawing.Size(36, 19);
			this.TBSN_VelY.TabIndex = 27;
			this.TBSN_VelY.Text = "0";
			// 
			// TBSN_VelX
			// 
			this.TBSN_VelX.Location = new System.Drawing.Point(75, 143);
			this.TBSN_VelX.Name = "TBSN_VelX";
			this.TBSN_VelX.Scp = null;
			this.TBSN_VelX.Size = new System.Drawing.Size(36, 19);
			this.TBSN_VelX.TabIndex = 27;
			this.TBSN_VelX.Text = "0";
			// 
			// TBSN_PosY
			// 
			this.TBSN_PosY.Location = new System.Drawing.Point(126, 120);
			this.TBSN_PosY.Name = "TBSN_PosY";
			this.TBSN_PosY.Scp = null;
			this.TBSN_PosY.Size = new System.Drawing.Size(36, 19);
			this.TBSN_PosY.TabIndex = 27;
			this.TBSN_PosY.Text = "0";
			// 
			// TBSN_PosX
			// 
			this.TBSN_PosX.Location = new System.Drawing.Point(75, 120);
			this.TBSN_PosX.Name = "TBSN_PosX";
			this.TBSN_PosX.Scp = null;
			this.TBSN_PosX.Size = new System.Drawing.Size(36, 19);
			this.TBSN_PosX.TabIndex = 27;
			this.TBSN_PosX.Text = "0";
			// 
			// Ctrl_Script
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.tB_Number7);
			this.Controls.Add(this.TBSN_AccY);
			this.Controls.Add(this.tB_Number5);
			this.Controls.Add(this.TBSN_AccX);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.TBSN_VelY);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.TBSN_VelX);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.TBSN_PosY);
			this.Controls.Add(this.TBSN_PosX);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.Tb_Img);
			this.Controls.Add(this.Lbl_Img);
			this.Controls.Add(this.tBN_Stop);
			this.Controls.Add(this.tBN_Vibration);
			this.Controls.Add(this.tB_Number1);
			this.Controls.Add(this.tBN_BlackOut);
			this.Controls.Add(this.tBN_Power);
			this.Controls.Add(this.cB_ClcSt);
			this.Controls.Add(this.tB_Frame);
			this.Controls.Add(this.lbl_clc);
			this.Controls.Add(this.lbl_Pos);
			this.Controls.Add(this.lbl_Acc);
			this.Controls.Add(this.Lbl_Vibration);
			this.Controls.Add(this.Lbl_Rad);
			this.Controls.Add(this.lbl_Vel);
			this.Controls.Add(this.Lbl_BlackOut);
			this.Controls.Add(this.lbl_Frame);
			this.Controls.Add(this.lbl_AP);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox3);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "Ctrl_Script";
			this.Size = new System.Drawing.Size(561, 437);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public TB_Number tBN_Power;
		public System.Windows.Forms.ComboBox cB_ClcSt;
		public System.Windows.Forms.TextBox tB_Frame;
		private System.Windows.Forms.Label lbl_clc;
		private System.Windows.Forms.Label lbl_Vel;
		private System.Windows.Forms.Label lbl_Frame;
		private System.Windows.Forms.Label lbl_AP;
		private System.Windows.Forms.Label lbl_Pos;
		private System.Windows.Forms.Label lbl_Acc;
		private System.Windows.Forms.Label Lbl_Img;
		private System.Windows.Forms.TextBox Tb_Img;
		private System.Windows.Forms.Label Lbl_BlackOut;
		public TB_Number tBN_BlackOut;
		private System.Windows.Forms.Label Lbl_Vibration;
		public TB_Number tBN_Vibration;
		private System.Windows.Forms.Label Lbl_Stop;
		public TB_Number tBN_Stop;
		private System.Windows.Forms.RadioButton RB_TRG_ALL;
		private System.Windows.Forms.RadioButton RB_TRG_GRP;
		private System.Windows.Forms.RadioButton RB_TRG_SGL;
		private System.Windows.Forms.GroupBox groupBox1;
		private TB_ScpNumber TBSN_PosX;
		private TB_ScpNumber TBSN_PosY;
		private TB_ScpNumber TBSN_VelX;
		private TB_ScpNumber TBSN_VelY;
		private TB_ScpNumber TBSN_AccX;
		private TB_ScpNumber TBSN_AccY;
		private System.Windows.Forms.Label Lbl_Rad;
		public TB_Number tB_Number1;
		public TB_Number tB_Number2;
		private System.Windows.Forms.Label Lbl_AI_Time;
		private System.Windows.Forms.GroupBox groupBox2;
		public TB_Number tB_Number4;
		public TB_Number tB_Number3;
		private System.Windows.Forms.Label Lbl_AI_N;
		private System.Windows.Forms.Label Lbl_AI_Pitch;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		public TB_Number tB_Number5;
		public TB_Number tB_Number7;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Button Btn_PasteSqc;
		private System.Windows.Forms.Button Btn_PasteGroup;
		private System.Windows.Forms.Button Btn_Copy;
		private System.Windows.Forms.TextBox textBox1;
	}
}
