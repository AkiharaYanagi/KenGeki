using System;
using SlimDX.DirectInput;


namespace ScriptEditor
{
	public class DxKeyboard
	{
		private Keyboard keyboard;
		private KeyboardState kbState;

		//SlimDx.DirectInput の KeyboardStateは「押したキー」「離したキー」を１つずつ保存している
		//前状態を参照するため、Keyすべてにおいて記録する
		private bool[] pre_kbState;
		

		//コンストラクタ
		public DxKeyboard ()
		{
		}

		//開始
		public void Load ( DirectInput dInput )
		{
			keyboard = new Keyboard ( dInput );
			keyboard.Acquire ();
			kbState = keyboard.GetCurrentState ();
			pre_kbState = new bool [ Enum.GetValues ( typeof ( Key ) ).Length ];
		}

		//毎回の更新
		public void Update ()
		{
			//前回の記録
			foreach ( Key k in Enum.GetValues ( typeof ( Key )) )
			{
				pre_kbState [ (int)k ] = kbState.IsPressed ( k );
			}

			//今回の記録
			kbState = keyboard.GetCurrentState ();
		}


		//押された状態のキーを優先順に１つ返す
		//非対応キー ( Escape, Return, tab ) は Key.Unknown を返す
		public Key GetKey ()
		{
			//押された状態のキーが０のとき Key.Unknown を返す
			if ( 0 >= kbState.PressedKeys.Count ) { return Key.Unknown; }
			
			//押された状態のキーの先頭
			Key k = kbState.PressedKeys[0];
			
			//非対応キー ( Escape, Return, tab ) は Key.Unknown を返す
			if ( Key.Escape == k || Key.Tab == k || Key.Return == k ) { return Key.Unknown;}
			
			return k;
		}


		//押された瞬間のキーを優先順に１つ返す
		//非対応キー ( Escape, Return, tab ) は Key.Unknown を返す
		public Key PushKey ()
		{
			//押された状態のキーが０のとき Key.Unknown を返す
			if ( 0 >= kbState.PressedKeys.Count ) { return Key.Unknown; }
	
			//押された状態
			Key k = kbState.PressedKeys[0];
			
			//非対応キー ( Escape, Return, tab ) は Key.Unknown を返す
			if ( Key.Escape == k || Key.Tab == k || Key.Return == k ) { return Key.Unknown; }

			//１つ前の状態は押していない
			if ( pre_kbState [ (int)k ]  ) { return Key.Unknown; }
			
			return k;
		}
	}


}
