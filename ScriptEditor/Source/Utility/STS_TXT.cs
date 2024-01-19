using System.Windows.Forms;

namespace ScriptEditor
{
	public static class STS_TXT
	{
		public static ToolStripStatusLabel Tssl { get; set; } = null;

		public static void Trace ( string str )
		{
			Tssl.Text = str;
		}
	}
}
