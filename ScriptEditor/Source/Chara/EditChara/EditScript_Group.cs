using System.Collections.Generic;

namespace ScriptEditor
{
	using LScp = List < Script >;

	public partial class EditScript
	{
		//---------------------------------------------------------------------
		//グループ（スクリプトの参照リスト）
		//ScriptがIDを持ち、Sequenceが変更されるたびに
		//編集用リストを再構築する
		//---------------------------------------------------------------------
		//グループリスト
		List < LScp > L_ScriptGroup = new List<LScp> ();

		//選択中グループ
		LScp SelectedGroup = null;

		//再構築
		public void Restruct ( LScp lsScp, int frame )
		{
			//グループ個数確認
			int g = 0;	//グループ種類数
			List < int > group = new List<int> ();
			foreach ( Script s in lsScp )
			{
				if ( ! group.Exists ( i=>i==s.Group ) )
				{
					group .Add ( s.Group );
					++ g;
				}
			}

			//グループ種類数で初期化(最大はスクリプト数)
			L_ScriptGroup = new List<LScp> ( lsScp.Count );
			for ( int i = 0; i < lsScp.Count + 1; ++ i ) { L_ScriptGroup.Add ( new LScp () ); }

			//スクリプトの値から各グループに追加する
			foreach ( Script s in lsScp )
			{				
				L_ScriptGroup[ s.Group ].Add ( s );
			}

			//選択
			SelectGroup ( lsScp, frame );
		}


		//選択範囲のスクリプトをグループにする
		public void MakeGroup ( LScp lsScp, int start, int end )
		{
			//チェック
			int count = 0;
			foreach ( Script s in lsScp )
			{
				//選択範囲のスクリプトをチェック
				if ( start <= count && count <= end )
				{
					//すでにグループになっているスクリプトが１つでもあれば中止
					if ( 0 != s.Group ) { return; }
				}
				++ count;
			}

			//未使用グループ値を検索
			int unusedGroup = GetUnusedIndex ( lsScp );

			//選択範囲のスクリプトをグループにする
			count = 0;
			foreach ( Script s in lsScp )
			{
				if ( start <= count && count <= end )
				{
					s.Group = unusedGroup;
				}
				++ count;
			}

			//再構築
			Restruct ( lsScp, start );
		}

		//０以外の未使用グループ値を検索
		public int GetUnusedIndex ( LScp lsScp )
		{
			int unusedGroup = 1;
			for ( int i = 0; i < lsScp.Count; ++ i )
			{
				Script s = lsScp [ i ];
				if ( unusedGroup == s.Group ) 
				{
					++ unusedGroup;
					i = 0;
					continue;
				}
			}
			return unusedGroup;
		}

		//グループの選択
		public void SelectGroup ( LScp lsScp, int frame )
		{
			SelectedGroup = L_ScriptGroup [ lsScp [ frame ].Group ];
		}

		//グループに選択スクリプトの内容を複製
		public void PasteGroup ( Script scp )
		{
			//０のときは何もしない
			if ( 0 == scp.Group ) { return; }

			foreach ( Script s in SelectedGroup )
			{
				s.Copy ( scp );
			}
		}
	}
}
