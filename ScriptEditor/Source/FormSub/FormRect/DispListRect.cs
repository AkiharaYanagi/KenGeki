using System.Drawing;

namespace ScriptEditor
{

	//枠表示まとめ
	public class DispRects
	{
		private DispListRect DL_CRect = new DispListRect ();
		private DispListRect DL_HRect = new DispListRect ();
		private DispListRect DL_ARect = new DispListRect ();
		private DispListRect DL_ORect = new DispListRect ();

		public void Set ( Ctrl_ListRect cl )
		{
			DL_CRect.Set ( cl, Color.FromArgb ( 0x400000ff ) );	//接触枠リスト
			DL_HRect.Set ( cl, Color.FromArgb ( 0x40ff0000 ) );	//当り枠リスト
			DL_ARect.Set ( cl, Color.FromArgb ( 0x4000ff00 ) );	//攻撃枠リスト
			DL_ORect.Set ( cl, Color.FromArgb ( 0x40ffff00 ) );	//相殺枠リスト
		}
	}

	//種類ごと枠
	public class DispListRect
	{
		public Ctrl_ListRect Ctrl_ListRect { get; set; } = null;

		//表示枠色
		public Color Color { get; set; } = Color.White;

		public void Set ( Ctrl_ListRect cl, Color clr )
		{
			Ctrl_ListRect = cl;
			Color = clr;
		}
	}
}
