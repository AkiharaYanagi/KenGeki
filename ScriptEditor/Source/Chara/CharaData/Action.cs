namespace ScriptEditor
{
	//================================================================
	//	◆アクション		各フレームのスクリプトリストを持つ
	//		┣[]スクリプト
	//		┣次アクション
	//		┣アクション属性
	//		┣アクション体勢
	//		┣消費バランス値
	//================================================================

	//-------------------------------------------------------
	//	■ 基本状態アクション
	//		(アクションリストの先頭から固定の位置にあり、外部から変更される対象先のアクション)
	//		立ち, 構え, 打合, 避け, よろけ, ダメージ, 投げられ, ダウン, 勝利

	//-------------------------------------------------------
	//アクション属性 定義
	public enum ActionCategory
	{
		NEUTRAL, MOVE, DASH, ATTACK_L, ATTACK_M, ATTACK_H
		, CLANG, AVOID, DOTTY, DAMAGED
		, THROW, GUARD, DEMO, OTHER
	}

	//-------------------------------------------------------
	//アクション体勢 定義
	public enum ActionPosture
	{
		STAND, CROUCH, JUMP
	}

	//-------------------------------------------------------
	//アクション
	public class Action : Sequence 
	{
		//次アクションID
		public int NextIndex { get; set; } = 0;

		//次アクション名
		public string NextActionName { get; set; } = "Next";

		//次アクション(オブジェクトで指定)
		public Action NextAction { get; set; } = null;

		//アクション属性
		public ActionCategory Category { get; set; } = ActionCategory.NEUTRAL;

		//アクション体勢
		public ActionPosture Posture { get; set; } = ActionPosture.STAND;

		//消費バランス
//		public RefInt Balance { get; set; } = new RefInt ( 0 );
		public int _Balance { get; set; } = 0;


		//----------------------------------------------------------------------------
		//コンストラクタ
		public Action ()	//ロード時に空白が必要
		{
		}

		//引数付きコンストラクタ
		public Action ( string str ) : base ( str )
		{
			//baseのコンストラクタの後でbase.Clear()が呼ばれてしまうのでClear()を用いない
//			Clear ();
		}

		//コピーコンストラクタ
		public Action ( Action action )
		{
			base.Copy ( action );

			this.NextIndex = action.NextIndex;
			this.NextActionName = action.NextActionName;
			this.Category = action.Category;
			this.Posture = action.Posture;
			this._Balance = action._Balance;
	}

		//クリア
		public override void Clear ()
		{
			NextIndex = 0;
			NextActionName = "Next";
			Category = ActionCategory.NEUTRAL;
			Posture = ActionPosture.STAND;
			_Balance = 0;

			base.Clear ();
		}

		//コピー
		public void CopyAction ( Action action )
		{
			base.Copy ( action );

			this.NextIndex = action.NextIndex;
			this.NextActionName = action.NextActionName;
			this.Category = action.Category;
			this.Posture = action.Posture;
			this._Balance = action._Balance;
		}
	}

}
