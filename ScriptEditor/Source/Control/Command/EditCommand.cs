using ScriptEditor;

namespace ScriptEditor
{
	using GKC_ST = GameKeyCommand.GameKeyCommandState;

	public class EditCommand
	{
		//対象データ
		public Command Cmd { get; set; } = new Command ();

		//コマンド中のキーの位置
		public SelectKey SlctKey { get; set; } = new SelectKey ();


		//キー追加
		public void AddKey ()
		{
			Cmd.ListGameKeyCommand.Add ( new GameKeyCommand () );
		}

		//キー削除
		public void DelKey ()
		{
			if ( Cmd.ListGameKeyCommand.Count <= 1 ) { return; }
			Cmd.ListGameKeyCommand.RemoveAt ( Cmd.ListGameKeyCommand.Count - 1 );
		}

		//キー変更
		public void UpKey ()
		{
			GKC_ST gkcst = SlctKey.GetSt ( Cmd );
			if ( GKC_ST.KEY_WILD == gkcst )
			{
				SlctKey.SetSt ( Cmd, GKC_ST.KEY_OFF );
			}
			else
			{
				SlctKey.SetSt ( Cmd, gkcst.Next () );
			}
		}

		public void DownKey ()
		{
			GKC_ST gkcst = SlctKey.GetSt ( Cmd );
			if ( GKC_ST.KEY_OFF == gkcst )
			{
				SlctKey.SetSt ( Cmd, GKC_ST.KEY_WILD );
			}
			else
			{
				SlctKey.SetSt ( Cmd, gkcst.Prev () );
			}
		}

		//タイミング変更
		public void Timing ( GKC_ST gkcst )
		{
			SlctKey.SetSt ( Cmd, gkcst );
		}

		//レバー変更
		public void LvrSelect_L ()
		{
			if ( SelectKey.KeyKind.ARROW == SlctKey.Kind )
			{
				GameKeyCommand gkc = SlctKey.GetGKC ( Cmd );
				gkc.Lever_L ();
			}
		}

		//レバー変更
		public void LvrSelect_R ()
		{
			if ( SelectKey.KeyKind.ARROW == SlctKey.Kind )
			{
				GameKeyCommand gkc = SlctKey.GetGKC ( Cmd );
				gkc.Lever_R ();
			}
		}

		//否定のフラグ
		public void TurnNot ( bool b )
		{
			GameKeyCommand gkc = SlctKey.GetGKC ( Cmd );
			if ( gkc != null )
			{
				gkc.Not = b;
			}
		}
	}
}
