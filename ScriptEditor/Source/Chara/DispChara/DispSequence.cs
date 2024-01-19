using System.Windows.Forms;

namespace ScriptEditor
{
	public class DispSequence
	{
		//名前
//		protected TextBox TbSeqName { get; set; } = null;

		//表示
		public virtual void Disp ( Sequence sqc )
		{
//			TbSeqName.Text = ( null == sqc.Name ) ? "" : sqc.Name;
		}

		//キャラデータの設定
		public virtual void SetCharaData ( Chara ch )
		{
		}
	}


}

