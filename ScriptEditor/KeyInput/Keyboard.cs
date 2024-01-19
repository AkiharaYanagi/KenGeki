using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;


using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;
using DirectInput = Microsoft.DirectX.DirectInput;


namespace ScriptEditor
{

	//一つ前のキーボード状態を保存するため保存用クラスを作る
	//DirectInput.KeyboardStateは読取専用
	public class PreKeyboardState
	{
		private bool[] b = new bool[( int ) Key.MediaSelect];
		public bool this[Key index] { get { return b[( int ) index]; } set { b[( int ) index] = value; } }
	}


	//キーボードの管理

	public class Keyboard
	{
		//扱うキーボード
		private Device keyboard;

		//キーボードの状態
		private KeyboardState keyState;

		//キーボードの一つ前の状態
		private PreKeyboardState preKeyState = new PreKeyboardState();

		//コンストラクタ
		public Keyboard ()
		{
			keyboard = new Device ( SystemGuid.Keyboard );
			keyboard.Acquire ();
			keyState = keyboard.GetCurrentKeyboardState ();
		}

		//更新
		public void Update ()
		{
			//一つ前を保存
			//DirectInput.KeyはEscape(==1)からMediaSelect(==237)までのEnum
			for ( Key key = Key.Escape - 1; key < Key.MediaSelect; ++key )
			{
				preKeyState[key] = keyState[key];
			}
			keyState = keyboard.GetCurrentKeyboardState ();
		}

		//キーが押された状態かどうか
		public bool IsKey ( DirectInput.Key key )
		{
			return ( keyState[key] );
		}

		//押した瞬間かどうか(前回off 今回on)
		public bool PushKey ( DirectInput.Key key )
		{
			return ( keyState[key] && ! preKeyState[key] );
		}

		//押し続けている状態かどうか(前回on 今回on)
		public bool KeepKey ( DirectInput.Key key )
		{
			return ( keyState[key] && preKeyState[key] );
		}

		//離した瞬間かどうか(前回on 今回off)
		public bool ReleaseKey ( DirectInput.Key key )
		{
			return ( ! keyState[key] && preKeyState[key] );
		}

	}
}
