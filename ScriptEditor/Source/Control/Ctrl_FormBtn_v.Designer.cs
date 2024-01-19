namespace ScriptEditor
{
	partial class Ctrl_FormBtn_v
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
			if ( disposing && (components != null) )
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
			this.Btn_Preview = new System.Windows.Forms.Button();
			this.Btn_Route = new System.Windows.Forms.Button();
			this.Btn_EfGnrt = new System.Windows.Forms.Button();
			this.Btn_RctList = new System.Windows.Forms.Button();
			this.BtnImg = new System.Windows.Forms.Button();
			this.BtnScriptValue = new System.Windows.Forms.Button();
			this.BtnScriptList = new System.Windows.Forms.Button();
			this.BtnFmAction = new System.Windows.Forms.Button();
			this.Btn_HideAction = new System.Windows.Forms.Button();
			this.Btn_HideScriptList = new System.Windows.Forms.Button();
			this.Btn_HideScriptValue = new System.Windows.Forms.Button();
			this.Btn_HideImage = new System.Windows.Forms.Button();
			this.Btn_HideRectList = new System.Windows.Forms.Button();
			this.Btn_HideEfGnrt = new System.Windows.Forms.Button();
			this.Btn_HideRoute = new System.Windows.Forms.Button();
			this.Btn_HidePreview = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// Btn_Preview
			// 
			this.Btn_Preview.Enabled = false;
			this.Btn_Preview.Location = new System.Drawing.Point(30, 262);
			this.Btn_Preview.Name = "Btn_Preview";
			this.Btn_Preview.Size = new System.Drawing.Size(99, 31);
			this.Btn_Preview.TabIndex = 1;
			this.Btn_Preview.Text = "プレビュー";
			this.Btn_Preview.UseVisualStyleBackColor = true;
			this.Btn_Preview.Click += new System.EventHandler(this.Btn_Preview_Click);
			// 
			// Btn_Route
			// 
			this.Btn_Route.Location = new System.Drawing.Point(30, 225);
			this.Btn_Route.Name = "Btn_Route";
			this.Btn_Route.Size = new System.Drawing.Size(99, 31);
			this.Btn_Route.TabIndex = 2;
			this.Btn_Route.Text = "ルート";
			this.Btn_Route.UseVisualStyleBackColor = true;
			this.Btn_Route.Click += new System.EventHandler(this.Btn_Route_Click);
			// 
			// Btn_EfGnrt
			// 
			this.Btn_EfGnrt.Location = new System.Drawing.Point(30, 188);
			this.Btn_EfGnrt.Name = "Btn_EfGnrt";
			this.Btn_EfGnrt.Size = new System.Drawing.Size(99, 31);
			this.Btn_EfGnrt.TabIndex = 3;
			this.Btn_EfGnrt.Text = "Ef生成";
			this.Btn_EfGnrt.UseVisualStyleBackColor = true;
			this.Btn_EfGnrt.Click += new System.EventHandler(this.Btn_EfGnrt_Click);
			// 
			// Btn_RctList
			// 
			this.Btn_RctList.Location = new System.Drawing.Point(30, 151);
			this.Btn_RctList.Name = "Btn_RctList";
			this.Btn_RctList.Size = new System.Drawing.Size(99, 31);
			this.Btn_RctList.TabIndex = 4;
			this.Btn_RctList.Text = "枠リスト";
			this.Btn_RctList.UseVisualStyleBackColor = true;
			this.Btn_RctList.Click += new System.EventHandler(this.Btn_RctList_Click);
			// 
			// BtnImg
			// 
			this.BtnImg.Location = new System.Drawing.Point(30, 114);
			this.BtnImg.Name = "BtnImg";
			this.BtnImg.Size = new System.Drawing.Size(99, 31);
			this.BtnImg.TabIndex = 5;
			this.BtnImg.Text = "イメージ";
			this.BtnImg.UseVisualStyleBackColor = true;
			this.BtnImg.Click += new System.EventHandler(this.BtnImg_Click);
			// 
			// BtnScriptValue
			// 
			this.BtnScriptValue.Location = new System.Drawing.Point(30, 77);
			this.BtnScriptValue.Name = "BtnScriptValue";
			this.BtnScriptValue.Size = new System.Drawing.Size(99, 31);
			this.BtnScriptValue.TabIndex = 6;
			this.BtnScriptValue.Text = "スクリプト(値)";
			this.BtnScriptValue.UseVisualStyleBackColor = true;
			this.BtnScriptValue.Click += new System.EventHandler(this.BtnScriptValue_Click);
			// 
			// BtnScriptList
			// 
			this.BtnScriptList.Location = new System.Drawing.Point(30, 40);
			this.BtnScriptList.Name = "BtnScriptList";
			this.BtnScriptList.Size = new System.Drawing.Size(99, 31);
			this.BtnScriptList.TabIndex = 7;
			this.BtnScriptList.Text = "スクリプト(リスト)";
			this.BtnScriptList.UseVisualStyleBackColor = true;
			this.BtnScriptList.Click += new System.EventHandler(this.BtnScriptList_Click);
			// 
			// BtnFmAction
			// 
			this.BtnFmAction.Enabled = false;
			this.BtnFmAction.Location = new System.Drawing.Point(30, 3);
			this.BtnFmAction.Name = "BtnFmAction";
			this.BtnFmAction.Size = new System.Drawing.Size(99, 31);
			this.BtnFmAction.TabIndex = 8;
			this.BtnFmAction.Text = "アクション";
			this.BtnFmAction.UseVisualStyleBackColor = true;
			this.BtnFmAction.Click += new System.EventHandler(this.BtnFmAction_Click);
			// 
			// Btn_HideAction
			// 
			this.Btn_HideAction.Enabled = false;
			this.Btn_HideAction.Location = new System.Drawing.Point(3, 4);
			this.Btn_HideAction.Name = "Btn_HideAction";
			this.Btn_HideAction.Size = new System.Drawing.Size(21, 31);
			this.Btn_HideAction.TabIndex = 9;
			this.Btn_HideAction.Text = "▼";
			this.Btn_HideAction.UseVisualStyleBackColor = true;
			this.Btn_HideAction.Click += new System.EventHandler(this.Btn_HideAction_Click);
			// 
			// Btn_HideScriptList
			// 
			this.Btn_HideScriptList.Location = new System.Drawing.Point(3, 41);
			this.Btn_HideScriptList.Name = "Btn_HideScriptList";
			this.Btn_HideScriptList.Size = new System.Drawing.Size(21, 31);
			this.Btn_HideScriptList.TabIndex = 9;
			this.Btn_HideScriptList.Text = "▼";
			this.Btn_HideScriptList.UseVisualStyleBackColor = true;
			this.Btn_HideScriptList.Click += new System.EventHandler(this.Btn_HideScriptList_Click);
			// 
			// Btn_HideScriptValue
			// 
			this.Btn_HideScriptValue.Location = new System.Drawing.Point(3, 77);
			this.Btn_HideScriptValue.Name = "Btn_HideScriptValue";
			this.Btn_HideScriptValue.Size = new System.Drawing.Size(21, 31);
			this.Btn_HideScriptValue.TabIndex = 9;
			this.Btn_HideScriptValue.Text = "▼";
			this.Btn_HideScriptValue.UseVisualStyleBackColor = true;
			this.Btn_HideScriptValue.Click += new System.EventHandler(this.Btn_HideScriptValue_Click);
			// 
			// Btn_HideImage
			// 
			this.Btn_HideImage.Location = new System.Drawing.Point(3, 114);
			this.Btn_HideImage.Name = "Btn_HideImage";
			this.Btn_HideImage.Size = new System.Drawing.Size(21, 31);
			this.Btn_HideImage.TabIndex = 9;
			this.Btn_HideImage.Text = "▼";
			this.Btn_HideImage.UseVisualStyleBackColor = true;
			this.Btn_HideImage.Click += new System.EventHandler(this.Btn_HideImage_Click);
			// 
			// Btn_HideRectList
			// 
			this.Btn_HideRectList.Location = new System.Drawing.Point(3, 151);
			this.Btn_HideRectList.Name = "Btn_HideRectList";
			this.Btn_HideRectList.Size = new System.Drawing.Size(21, 31);
			this.Btn_HideRectList.TabIndex = 9;
			this.Btn_HideRectList.Text = "▼";
			this.Btn_HideRectList.UseVisualStyleBackColor = true;
			this.Btn_HideRectList.Click += new System.EventHandler(this.Btn_HideRectList_Click);
			// 
			// Btn_HideEfGnrt
			// 
			this.Btn_HideEfGnrt.Location = new System.Drawing.Point(3, 188);
			this.Btn_HideEfGnrt.Name = "Btn_HideEfGnrt";
			this.Btn_HideEfGnrt.Size = new System.Drawing.Size(21, 31);
			this.Btn_HideEfGnrt.TabIndex = 9;
			this.Btn_HideEfGnrt.Text = "▼";
			this.Btn_HideEfGnrt.UseVisualStyleBackColor = true;
			this.Btn_HideEfGnrt.Click += new System.EventHandler(this.Btn_HideEfGnrt_Click);
			// 
			// Btn_HideRoute
			// 
			this.Btn_HideRoute.Location = new System.Drawing.Point(3, 225);
			this.Btn_HideRoute.Name = "Btn_HideRoute";
			this.Btn_HideRoute.Size = new System.Drawing.Size(21, 31);
			this.Btn_HideRoute.TabIndex = 9;
			this.Btn_HideRoute.Text = "▼";
			this.Btn_HideRoute.UseVisualStyleBackColor = true;
			this.Btn_HideRoute.Click += new System.EventHandler(this.Btn_HideRoute_Click);
			// 
			// Btn_HidePreview
			// 
			this.Btn_HidePreview.Enabled = false;
			this.Btn_HidePreview.Location = new System.Drawing.Point(3, 262);
			this.Btn_HidePreview.Name = "Btn_HidePreview";
			this.Btn_HidePreview.Size = new System.Drawing.Size(21, 31);
			this.Btn_HidePreview.TabIndex = 9;
			this.Btn_HidePreview.Text = "▼";
			this.Btn_HidePreview.UseVisualStyleBackColor = true;
			this.Btn_HidePreview.Click += new System.EventHandler(this.Btn_HidePreview_Click);
			// 
			// Ctrl_FormBtn_v
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.Btn_HidePreview);
			this.Controls.Add(this.Btn_HideRoute);
			this.Controls.Add(this.Btn_HideEfGnrt);
			this.Controls.Add(this.Btn_HideRectList);
			this.Controls.Add(this.Btn_HideImage);
			this.Controls.Add(this.Btn_HideScriptValue);
			this.Controls.Add(this.Btn_HideScriptList);
			this.Controls.Add(this.Btn_HideAction);
			this.Controls.Add(this.Btn_Preview);
			this.Controls.Add(this.Btn_Route);
			this.Controls.Add(this.Btn_EfGnrt);
			this.Controls.Add(this.Btn_RctList);
			this.Controls.Add(this.BtnImg);
			this.Controls.Add(this.BtnScriptValue);
			this.Controls.Add(this.BtnScriptList);
			this.Controls.Add(this.BtnFmAction);
			this.Name = "Ctrl_FormBtn_v";
			this.Size = new System.Drawing.Size(135, 299);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button Btn_Preview;
		private System.Windows.Forms.Button Btn_Route;
		private System.Windows.Forms.Button Btn_EfGnrt;
		private System.Windows.Forms.Button Btn_RctList;
		private System.Windows.Forms.Button BtnImg;
		private System.Windows.Forms.Button BtnScriptValue;
		private System.Windows.Forms.Button BtnScriptList;
		private System.Windows.Forms.Button BtnFmAction;
		private System.Windows.Forms.Button Btn_HideAction;
		private System.Windows.Forms.Button Btn_HideScriptList;
		private System.Windows.Forms.Button Btn_HideScriptValue;
		private System.Windows.Forms.Button Btn_HideImage;
		private System.Windows.Forms.Button Btn_HideRectList;
		private System.Windows.Forms.Button Btn_HideEfGnrt;
		private System.Windows.Forms.Button Btn_HideRoute;
		private System.Windows.Forms.Button Btn_HidePreview;
	}
}
