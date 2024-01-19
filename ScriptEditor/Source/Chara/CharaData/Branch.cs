namespace ScriptEditor
{
	//各種条件によるアクション分岐
	public class Branch
	{
		//実行上は参照を用いるが、
		//スクリプトをテキストファイルに書き出すため、インデックスも保存する

		//条件　コマンドリストの添字 indexCommand
		public int IndexCommand { get; set; } = 0;

		//条件　コマンドの参照
		public Command Condition { get; set; } = null;

		//遷移先　アクションリストの添字 indexAction
		public int IndexAction { get; set; } = 0;

		//遷移先　アクションの参照
		public Action Transit { get; set; } = null;

		//遷移先　フレーム指定
//		public RefInt Frame { get; set; } = new RefInt ();
		public int Frame { get; set; } = 0;

		//コンストラクタ
		public Branch ()
		{
		}

		public Branch ( int indexCommand, Command command, int indexAction, Action action )
		{
			IndexCommand = indexCommand;
			Condition = command;
			IndexAction = indexAction;
			Transit = action;
		}
	}

}
