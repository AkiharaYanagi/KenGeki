using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ScriptEditor
{

	public partial class EditChara 
	{
		//---------------------------------------------------------------------
		//コマンド

		//コマンドを追加
		public void AddCommand ()
		{
			Chara.ListCommand.Add ( new Command ( "newCommand" ) );
		}

		//指定コマンドを削除
		public void RemoveCommand ( Command command )
		{
			Chara.ListCommand.Remove ( command );
		}

		//末尾のコマンドを削除
		public void RemoveCommand ()
		{
			Chara.ListCommand.RemoveAt ( Chara.ListCommand.Count - 1 );
		}

		//変更の通知
		public void ResetCommand ()
		{
			for ( int i = 0; i < Chara.ListCommand.Count; ++i )
			{
				Chara.ListCommand.ResetItem ( i );
			}
		}

		//---------------------------------------------------------------------

	}
}
