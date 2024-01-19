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

	//Managed DirectX を用いてキーボードとジョイスティックを扱う

	public class DxInput : IDisposable
	{
		private Keyboard keyboard = new Keyboard ();
		private Joystick joystick = new Joystick ();

		//コンストラクタ
		public DxInput ()
		{
		}

		//==================================================
		// ◆ IDisposable 継承
		//==================================================
		//開放
		private bool disposed = false;
		public void Dispose ()
		{
			Dispose ( true );
			GC.SuppressFinalize ( this );
		}

		protected virtual void Dispose ( bool bDisposing )
		{
			if ( ! disposed )
			{
				if ( bDisposing )
				{
					keyboard.Dispose ();
				}
				disposed = true;
			}
		}

		~DxInput ()
		{
			Dispose ( false );
		}
		//==================================================
		//更新
		public void Update ()
		{
			keyboard.Update ();
			joystick.Update ();
		}

		//すべての中で押された状態のものの先頭を返す
		public DeviceInput IsInput ()
		{
			DeviceInput di = new DeviceInput ();
			
			//なければそのままを返す
			return di;
		}

		//---------------------------------------------------------------------------
		//	キーボード
		//---------------------------------------------------------------------------
		//	キーボードで押されている状態かどうかを取得する
		//	引数：key キーボードの配列定数
		public bool IsKeyboard ( Key key ) { return keyboard.IsKey ( key ); }

		//	キーボードで押された瞬間かどうかを取得する
		//	引数：key キーボードの配列定数
		public bool PushKeyboard ( Key key ) { return keyboard.PushKey ( key ); }

		//	キーボードで押し続けている状態かどうかを取得する
		//	引数：key キーボードの配列定数
		public bool KeepKeyboard ( Key key ) { return keyboard.KeepKey ( key ); }

		//	キーボードで離された瞬間かどうかを取得する
		//	引数：key キーボードの配列定数
		public bool ReleaseKeyboard ( Key key ) { return keyboard.ReleaseKey ( key ); }


		//押されている状態のキーの中で先頭から返す
		//Key.Escapeは該当なしとする
		public Key GetKey ()
		{
			for ( Key k = Key.Escape; k < Key.MediaSelect; ++k )
			{
				if ( keyboard.IsKey ( k ) ) { return k; }
			}
			return Key.Escape;
		}

		//押された瞬間のキーの中で先頭から返す
		//Key.Escapeは該当なしとする
		public Key PushKey ()
		{
			for ( Key k = Key.Escape; k < Key.MediaSelect; ++k )
			{
				if ( keyboard.PushKey ( k ) ) { return k; }
			}
			return Key.Escape;
		}


		//---------------------------------------------------------------------------
		//	ジョイスティック
		//---------------------------------------------------------------------------
		//	ジョイスティックで押されている状態かどうかを取得する
		//	引数：device デバイスID, button ボタンID
		public bool IsJoyButton ( int device, int button ) { return joystick.IsButton ( device, button ); }
		public bool IsJoyUp ( int device ) { return joystick.IsUp ( device ); }
		public bool IsJoyDown ( int device ) { return joystick.IsDown ( device ); }
		public bool IsJoyLeft ( int device ) { return joystick.IsLeft ( device ); }
		public bool IsJoyRight ( int device ) { return joystick.IsRight ( device ); }

		//	ジョイスティックで押された瞬間かどうかを取得する
		//	引数：num デバイス番号, key キー番号
		public bool PushJoyButton ( int device, int button ) { return joystick.PushButton ( device, button ); }
		public bool PushJoyUp ( int device ) { return joystick.PushUp ( device ); }
		public bool PushJoyDown ( int device ) { return joystick.PushDown ( device ); }
		public bool PushJoyLeft ( int device ) { return joystick.PushLeft ( device ); }
		public bool PushJoyRight ( int device ) { return joystick.PushRight ( device ); }

		//	ジョイスティックで押し続けている状態かどうかを取得する
		//	引数：num デバイス番号, key キー番号
		public bool KeepJoyButton ( int device, int button ) { return joystick.KeepButton ( device, button ); }
		public bool KeepJoyUp ( int device ) { return joystick.KeepUp ( device ); }
		public bool KeepJoyDown ( int device ) { return joystick.KeepDown ( device ); }
		public bool KeepJoyLeft ( int device ) { return joystick.KeepLeft ( device ); }
		public bool KeepJoyRight ( int device ) { return joystick.KeepRight ( device ); }

		//	ジョイスティックで押された瞬間かどうかを取得する
		//	引数：num デバイス番号, key キー番号
		public bool ReleaseJoyButton ( int device, int key ) { return false; }
		public bool ReleaseJoyUp ( int device ) { return false; }
		public bool ReleaseJoyDown ( int device ) { return false; }
		public bool ReleaseJoyLeft ( int device ) { return false; }
		public bool ReleaseJoyRight ( int device ) { return false; }


		//押された状態のデバイスがあったら先頭を返す
		public int GetDeviceId ()
		{
			for ( int device = 0; device < joystick.GetJoystickNum (); ++device )
			{
				for ( int button = 0; button < Joystick.NUM_JOYSTICK_BUTTON; ++button )
				{
					if ( joystick.IsButton ( device, button ) ) { return device; }
				}
				if ( joystick.IsUp ( device ) ) { return device; }
				if ( joystick.IsDown ( device ) ) { return device; }
				if ( joystick.IsLeft ( device ) ) { return device; }
				if ( joystick.IsRight ( device ) ) { return device; }
			}

			return Joystick.NO_DEVICE;
		}

		//指定デバイスの中で押された状態のジョイスティックキーがあったら先頭を返す
		public int GetJoyKey ( int device )
		{
			//すべてのキーをループして該当したらbreak
			for ( int button = 0; button < Joystick.NUM_JOYSTICK_BUTTON; ++button )
			{
				if ( joystick.IsButton ( device, button ) )
				{
					return button;
				}
			}
			//方向キー
			if ( joystick.IsUp ( device ) ) { return Joystick.JOYSTICK_UP; }
			if ( joystick.IsDown ( device ) ) { return Joystick.JOYSTICK_DOWN; }
			if ( joystick.IsLeft ( device ) ) { return Joystick.JOYSTICK_LEFT; }
			if ( joystick.IsRight ( device ) ) { return Joystick.JOYSTICK_RIGHT; }

			return Joystick.JOYSTICK_NEUTRAL;
		}

		//押された瞬間のデバイスがあったら先頭を返す
		public int PushDeviceId ()
		{
			for ( int device = 0; device < joystick.GetJoystickNum (); ++device )
			{
				for ( int button = 0; button < Joystick.NUM_JOYSTICK_BUTTON; ++button )
				{
					if ( joystick.IsButton ( device, button ) ) { return device; }
				}
				if ( joystick.PushUp ( device ) ) { return device; }
				if ( joystick.PushDown ( device ) ) { return device; }
				if ( joystick.PushLeft ( device ) ) { return device; }
				if ( joystick.PushRight ( device ) ) { return device; }
			}

			return Joystick.NO_DEVICE;
		}

		//指定デバイスの中で押された状態のジョイスティックキーがあったら先頭を返す
		public int PushJoyKey ( int device )
		{
			//すべてのキーをループして該当したらbreak
			for ( int button = 0; button < Joystick.NUM_JOYSTICK_BUTTON; ++button )
			{
				if ( joystick.PushButton ( device, button ) )
				{
					return button;
				}
			}
			//方向キー
			if ( joystick.PushUp ( device ) ) { return Joystick.JOYSTICK_UP; }
			if ( joystick.PushDown ( device ) ) { return Joystick.JOYSTICK_DOWN; }
			if ( joystick.PushLeft ( device ) ) { return Joystick.JOYSTICK_LEFT; }
			if ( joystick.PushRight ( device ) ) { return Joystick.JOYSTICK_RIGHT; }

			return Joystick.JOYSTICK_NEUTRAL;
		}

		//Joystrickの個数を返す
		public int GetJoystickNum ()
		{
			return joystick.GetJoystickNum ();
		}

#if false
		//軸の状態を返す
		int GetJoyAxisX( int nDevice )
		{
			return 0;
		}

		int GetJoyAxisY ( int nDevice )
		{
			return 0;
		}
#endif

	}
}
