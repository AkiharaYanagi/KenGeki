using System.Windows.Forms;

namespace ScriptEditor.Source.FormSub.FormRect
{
	public partial class Ctrl_Rects : UserControl
	{
		public Ctrl_Rects ()
		{
			InitializeComponent ();

			ctrl_ListCRect.SetText ( "CRect" );
			ctrl_ListHRect.SetText ( "HRect" );
			ctrl_ListARect.SetText ( "ARect" );
			ctrl_ListORect.SetText ( "ORect" );
		}
	}
}
