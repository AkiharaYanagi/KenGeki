using System.Collections.Generic;

namespace ScriptEditor
{
	//------------------------------------------------------------------
	//	コマンド　クラス
	//		ゲーム入力キーの配列をフレーム毎における特定の組み合わせで保持する
	//		実際に入力されたものの記録と、スクリプト分岐の条件に用いる
	//------------------------------------------------------------------
	
	//コマンドの持続入力"→→","・→"は必要か？
	//フレーム毎にニュートラルでチェックできるのではないか
	// ↓
	//Command内で新たに比較関数を作る
	//・否定の条件
	//・キャラ左右向きの判定
	//・前要素
	//・WILD（押したか押さないか判定に関係しない）

	//------------------------------------------------------------------
	using GK_Cmd = GameKeyCommand;

	public class Command
	{
		//名前
		public string Name { set; get; } = "command name";
		public override string ToString () { return Name; }

		//ゲームキーコマンド配列
		public List < GK_Cmd > ListGameKeyCommand { get; } = new List < GK_Cmd > ();

		//入力受付時間
		public int LimitTime { get; set; }

		//コンストラクタ
		public Command ()
		{
			this.Name = "blank";
		}

		//引数付コンストラクタ
		public Command ( string str )
		{
			Name = str;
		}

		//コピーコンストラクタ
		public Command ( Command cmd )
		{
			Name = cmd.Name;
			ListGameKeyCommand.Clear ();
			foreach ( GK_Cmd gk in cmd.ListGameKeyCommand )
			{
				ListGameKeyCommand.Add ( gk );
			}
			LimitTime = cmd.LimitTime;
		}


		//比較
		//引数：現在フレームから遡って記録されたゲームキー配列
		public bool Compare ( GameKeyData[] aryGKData, bool dirRight )
		{
			//キー配列長さか、受付時間の短い方で検索
			int len = aryGKData.Length;
			int frame = ( LimitTime < len ) ? LimitTime : len;

			//入力完成数(目標値から減算し、０になったら完成)
			int comp = ListGameKeyCommand.Count - 1;
			
			//遡って検索
			for ( int i = 0; i < frame; ++i )
			{
				if ( ListGameKeyCommand[comp].CompareTarget ( aryGKData[i], dirRight ) )
				{
					-- comp;
					if ( 1 > comp )
					{
						int compi = 0;
						++compi;
					}
				}
				if ( 0 > comp )
				{
					return true; 
				}
			}
			return false;
		}
	}
}
