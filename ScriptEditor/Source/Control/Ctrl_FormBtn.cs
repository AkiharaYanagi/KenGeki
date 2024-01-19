using System;
using System.Windows.Forms;


namespace ScriptEditor
{
	public class Ctrl_FormBtn : UserControl
	{
		private Button BtnScriptList;
		private Button BtnScriptValue;
		private Button BtnImg;
		private Button Btn_RctList;
		private Button Btn_EfGnrt;
		private Button Btn_Route;
		private Button Btn_Preview;
		private Button BtnFmAction;

		public Ctrl_FormBtn ()
		{
			InitializeComponent ();
		}

		private void InitializeComponent ()
		{
			this.BtnFmAction = new System.Windows.Forms.Button();
			this.BtnScriptList = new System.Windows.Forms.Button();
			this.BtnScriptValue = new System.Windows.Forms.Button();
			this.BtnImg = new System.Windows.Forms.Button();
			this.Btn_RctList = new System.Windows.Forms.Button();
			this.Btn_EfGnrt = new System.Windows.Forms.Button();
			this.Btn_Route = new System.Windows.Forms.Button();
			this.Btn_Preview = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// BtnFmAction
			// 
			this.BtnFmAction.Location = new System.Drawing.Point(0, 0);
			this.BtnFmAction.Name = "BtnFmAction";
			this.BtnFmAction.Size = new System.Drawing.Size(64, 31);
			this.BtnFmAction.TabIndex = 0;
			this.BtnFmAction.Text = "アクション";
			this.BtnFmAction.UseVisualStyleBackColor = true;
			this.BtnFmAction.Click += new System.EventHandler(this.BtnFmAction_Click);
			// 
			// BtnScriptList
			// 
			this.BtnScriptList.Location = new System.Drawing.Point(70, 0);
			this.BtnScriptList.Name = "BtnScriptList";
			this.BtnScriptList.Size = new System.Drawing.Size(99, 31);
			this.BtnScriptList.TabIndex = 0;
			this.BtnScriptList.Text = "スクリプト(リスト)";
			this.BtnScriptList.UseVisualStyleBackColor = true;
			this.BtnScriptList.Click += new System.EventHandler(this.BtnScriptList_Click);
			// 
			// BtnScriptValue
			// 
			this.BtnScriptValue.Location = new System.Drawing.Point(175, 0);
			this.BtnScriptValue.Name = "BtnScriptValue";
			this.BtnScriptValue.Size = new System.Drawing.Size(99, 31);
			this.BtnScriptValue.TabIndex = 0;
			this.BtnScriptValue.Text = "スクリプト(値)";
			this.BtnScriptValue.UseVisualStyleBackColor = true;
			this.BtnScriptValue.Click += new System.EventHandler(this.BtnScriptValue_Click);
			// 
			// BtnImg
			// 
			this.BtnImg.Location = new System.Drawing.Point(280, 0);
			this.BtnImg.Name = "BtnImg";
			this.BtnImg.Size = new System.Drawing.Size(70, 31);
			this.BtnImg.TabIndex = 0;
			this.BtnImg.Text = "イメージ";
			this.BtnImg.UseVisualStyleBackColor = true;
			this.BtnImg.Click += new System.EventHandler(this.BtnImg_Click);
			// 
			// Btn_RctList
			// 
			this.Btn_RctList.Location = new System.Drawing.Point(356, 0);
			this.Btn_RctList.Name = "Btn_RctList";
			this.Btn_RctList.Size = new System.Drawing.Size(70, 31);
			this.Btn_RctList.TabIndex = 0;
			this.Btn_RctList.Text = "枠リスト";
			this.Btn_RctList.UseVisualStyleBackColor = true;
			this.Btn_RctList.Click += new System.EventHandler(this.Btn_RctList_Click);
			// 
			// Btn_EfGnrt
			// 
			this.Btn_EfGnrt.Location = new System.Drawing.Point(432, 0);
			this.Btn_EfGnrt.Name = "Btn_EfGnrt";
			this.Btn_EfGnrt.Size = new System.Drawing.Size(70, 31);
			this.Btn_EfGnrt.TabIndex = 0;
			this.Btn_EfGnrt.Text = "Ef生成";
			this.Btn_EfGnrt.UseVisualStyleBackColor = true;
			this.Btn_EfGnrt.Click += new System.EventHandler(this.Btn_EfGnrt_Click);
			// 
			// Btn_Route
			// 
			this.Btn_Route.Location = new System.Drawing.Point(508, 0);
			this.Btn_Route.Name = "Btn_Route";
			this.Btn_Route.Size = new System.Drawing.Size(70, 31);
			this.Btn_Route.TabIndex = 0;
			this.Btn_Route.Text = "ルート";
			this.Btn_Route.UseVisualStyleBackColor = true;
			this.Btn_Route.Click += new System.EventHandler(this.Btn_Route_Click);
			// 
			// Btn_Preview
			// 
			this.Btn_Preview.Location = new System.Drawing.Point(584, 0);
			this.Btn_Preview.Name = "Btn_Preview";
			this.Btn_Preview.Size = new System.Drawing.Size(70, 31);
			this.Btn_Preview.TabIndex = 0;
			this.Btn_Preview.Text = "プレビュー";
			this.Btn_Preview.UseVisualStyleBackColor = true;
			this.Btn_Preview.Click += new System.EventHandler(this.Btn_Preview_Click);
			// 
			// Ctrl_FormBtn
			// 
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.Btn_Preview);
			this.Controls.Add(this.Btn_Route);
			this.Controls.Add(this.Btn_EfGnrt);
			this.Controls.Add(this.Btn_RctList);
			this.Controls.Add(this.BtnImg);
			this.Controls.Add(this.BtnScriptValue);
			this.Controls.Add(this.BtnScriptList);
			this.Controls.Add(this.BtnFmAction);
			this.Name = "Ctrl_FormBtn";
			this.Size = new System.Drawing.Size(662, 31);
			this.ResumeLayout(false);

		}

		private void BtnFmAction_Click ( object sender, EventArgs e )
		{
			FormAction.Inst.Active ();
		}

		private void BtnScriptList_Click ( object sender, EventArgs e )
		{
			Form_ScriptList.Inst.Active ();
		}

		private void BtnScriptValue_Click ( object sender, EventArgs e )
		{

		}

		private void BtnImg_Click ( object sender, EventArgs e )
		{

		}

		private void Btn_RctList_Click ( object sender, EventArgs e )
		{

		}

		private void Btn_EfGnrt_Click ( object sender, EventArgs e )
		{

		}

		private void Btn_Route_Click ( object sender, EventArgs e )
		{

		}

		private void Btn_Preview_Click ( object sender, EventArgs e )
		{

		}
	}
}
