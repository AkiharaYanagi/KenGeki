namespace ScriptEditor
{
	using GK_L = GameKeyCommand.LeverCommand;
	using GKC_ST = GameKeyCommand.GameKeyCommandState;

	//選択されているコマンド中のキー
	public class SelectKey
	{
		//未選択状態
		public bool Selecting { get; set; } = true;

		//対象データ
		public Command Cmd { get; set; } = new Command ();

		//選択中のフレーム数
		public int Frame { get; set; } = 0;

		//レバー選択位置
		public int SelectedIndex { get; set; } = 0;

		//キー種類
		public enum KeyKind
		{
			ARROW,
			KEY_L,
			KEY_Ma,
			KEY_Mb,
			KEY_H
		}

		public KeyKind Kind { get; set; } = KeyKind.ARROW;


		public void Set ( Command cmd, int frame, KeyKind keyKind )
		{
			Cmd = cmd;
			Frame = frame;
			Kind = keyKind;
			Selecting = true;
		}

		public GameKeyCommand GetGKC ( Command cmd )
		{
			if ( cmd.ListGameKeyCommand.Count <= Frame ) { return null; }
			return cmd.ListGameKeyCommand [ Frame ];
		}

		public GKC_ST GetSt ( Command cmd )
		{
			GameKeyCommand gkc = cmd.ListGameKeyCommand [ Frame ];

			GKC_ST ret = GKC_ST.KEY_OFF;
			switch ( Kind )
			{
			case KeyKind.ARROW: ret = gkc.GetLvrSt (); break;
			case KeyKind.KEY_L: ret = gkc.Btn [ 0 ]; break;
			case KeyKind.KEY_Ma: ret = gkc.Btn [ 1 ]; break;
			case KeyKind.KEY_Mb: ret = gkc.Btn [ 2 ]; break;
			case KeyKind.KEY_H: ret = gkc.Btn [ 3 ]; break;
			}

			return ret; 
		}

		public void SetSt ( Command cmd, GKC_ST gkcst )
		{
			GameKeyCommand gkc = cmd.ListGameKeyCommand [ Frame ];

			//表示インデックスからレバーインデックスに変換
			// 0 1 2		6 5 4	(	7 8 9	)
			// 3 4 5	→	7   3	(	4   6	)
			// 6 7 8		0 1 2	(	1 2 3	)

			int[] ary = { 6, 5, 4, 7, 8, 3, 0, 1, 2 };
			int indexLvr = ary [ SelectedIndex ];

			switch ( Kind )
			{
			case KeyKind.ARROW: gkc.SetLvrSt ( gkcst, (GK_L)indexLvr ); break;
			case KeyKind.KEY_L: gkc.Btn [ 0 ] = gkcst; break;
			case KeyKind.KEY_Ma: gkc.Btn [ 1 ] = gkcst; break;
			case KeyKind.KEY_Mb: gkc.Btn [ 2 ] = gkcst; break;
			case KeyKind.KEY_H: gkc.Btn [ 3 ] = gkcst; break;
			}
		}
	}
}
