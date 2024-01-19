using System.Windows.Forms;
using ScriptEditor;


namespace ScriptEditor
{
	public partial class Ctrl_EfGnrt : UserControl
	{
		private EditListbox < EffectGenerate > EL_EfGnrt = new EditListbox<EffectGenerate> ();
		
		
		public Ctrl_EfGnrt ()
		{
			InitializeComponent ();

			this.Controls.Add ( EL_EfGnrt );
		}
	}
}
