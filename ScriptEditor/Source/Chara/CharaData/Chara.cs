using System.ComponentModel;

namespace ScriptEditor
{
	//==================================================================================
	//	キャラ 実行上データ ver106
	//==================================================================================

	//==================================================================================
	//	◆キャラ	
	//		┣ビヘイビア ( []アクション )
	//		┣ガーニッシュ ( []エフェクト )
	//		┣[]コマンド
	//
	//	イメージリストはコンペンド(キャラのビヘイビアとガーニッシュ)が持つように変更
	//==================================================================================
	public class Chara
	{
		//---------------------------------------------------------------------
		//Compend(一覧)継承のアクションとエフェクト
		public Behavior behavior = new Behavior ();		//アクションの集合
		public Garnish garnish = new Garnish ();        //エフェクトの集合

		//---------------------------------------------------------------------
		//コマンドリスト
		public BindingList<Command> ListCommand { get; } = new BindingList<Command> ();

#if false
		//@todo 基本状態アクションIDをアクションリストの先頭から固定番号配置にする
		//		-> 基本状態アクションID項目の削除
		//		別位置で順番を定義する必要がある
		//---------------------------------------------------------------------
		//基本状態アクションID
		public enum BasicAction
		{
			STAND, POISED, CLANG, AVOID, DOTTY, DAMAGED, THROWN, DOWN, WIN
		};
		public RefInt[] BsAction { get; set; }
#endif

		//---------------------------------------------------------------------
		//コンストラクタ
		public Chara ()
		{
			//ダミー
			//必ずアクション、エフェクト、コマンドが各一つ以上存在する
			// 引数(名前)付きコンストラクタはScriptも生成される
			//（空）コンストラクタはScriptを生成しない
			// コピー等で必要なClear()のあとは手動で追加する
			behavior.Bldct_sqc.Add ( new Action ( "testAction0" ) );
			garnish.Bldct_sqc.Add ( new Effect ( "testEffect0" ) );
			ListCommand.Add ( new Command ( "testCommand0" ) );

#if false
			//基本状態アクションID
			BsAction = new RefInt[ Enum.GetNames ( typeof ( BasicAction ) ).Length ];
			int index = 0;
			foreach ( RefInt refi in BsAction )
			{
				BsAction [ index ++ ] = new RefInt ( 0 );
			}
#endif
		}

		//コピーコンストラクタ
		public Chara ( Chara srcChara )
		{
			this.Clear ();

			this.behavior.Copy ( srcChara.behavior );
			this.garnish.Copy ( srcChara.garnish );

			foreach ( Command cmd in srcChara.ListCommand )
			{
				this.ListCommand.Add ( new Command ( cmd ) );
			}
		}

		//クリア
		//	すべてのデータについてメモリを解放し、個数を０にする
		public void Clear ()
		{
			behavior.Clear ();
			garnish.Clear ();
			ListCommand.Clear ();

#if false
			//個数は不変かつ、RefIntは即値のみなのでメモリは解放しない
			foreach ( RefInt refi in BsAction )
			{
				refi.i = 0;
			}
#endif
		}

		//イメージリスト(メイン,Ef)を残しスクリプトだけクリア
		public void ClearScript ()
		{
			behavior.Clear ();
			garnish.Clear ();
			ListCommand.Clear ();
		}

		//コピー
		public void Copy ( Chara chara )
		{
			this.Clear ();

			this.behavior.Copy ( chara.behavior );
			this.garnish.Copy ( chara.garnish );

			foreach ( Command cmd in chara.ListCommand )
			{
				this.ListCommand.Add ( new Command ( cmd ) );
			}

#if false
			//個数は不変、RefIntの即値だけコピー
			int index = 0;
			foreach ( RefInt refi in BsAction )
			{
				refi.i = chara.BsAction[ index++ ].i;
			}
#endif
		}


	}
}
