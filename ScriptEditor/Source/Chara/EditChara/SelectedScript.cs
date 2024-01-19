using System.ComponentModel;

namespace ScriptEditor
{
	//---------------------------------------------------------------------
	//	スクリプト(シークエンス・フレーム)選択位置
	//		・存在しない部分は選択できない
	//---------------------------------------------------------------------
	public struct SELECTED_SCRIPT
	{
		public int sequence;
		public int frame;

		public SELECTED_SCRIPT ( int sequence, int frame )
		{
			this.sequence = sequence;
			this.frame = frame;
		}

		public void Copy ( SELECTED_SCRIPT ss )
		{
			this.sequence = ss.sequence;
			this.frame = ss.frame; 
		}

		public override string ToString ()
		{
			return "" + sequence.ToString () + ", " + frame.ToString ();
		}
	}


}
