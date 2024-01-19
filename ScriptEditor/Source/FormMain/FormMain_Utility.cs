using System.Windows.Forms;

namespace ScriptEditor
{
	public partial class FormMain : Form
	{
		//--------------------------------------------------------------------------
		//タイトルバー
		private readonly string formText = "ScriptEditor";		//フォームテキスト(タイトル)

		private void InitFormText ()
		{
			this.Text = formText;
		}
		private void SetFormText ( string fileName )
		{
			this.Text = fileName + " - " + formText;
		}
		private void NoSaveFormText ( string fileName )
		{
			this.Text = fileName + "*" + " - " + formText;
		}
		//--------------------------------------------------------------------------
	}
}
