using System;

using SlimDX.DirectInput;


namespace ScriptEditor
{
	//入力(キーボードとジョイスティック)をまとめたクラス
	public class DxInput
	{
		private readonly DirectInput dInput = new DirectInput ();

		private DxJoystick dxJoystick = new DxJoystick ();
		private DxKeyboard dxKeyboard = new DxKeyboard ();

		//コンストラクタ
		public DxInput ()
		{
		}

		//開始
		public void Load ()
		{
			dxJoystick.Load ( dInput );
			dxKeyboard.Load ( dInput );
		}

		//毎回の更新
		public void Update ()
		{
			dxJoystick.Update ();
			dxKeyboard.Update ();
		}

		//入力の取得
		public DeviceInput GetInput ()
		{
			DeviceInput di = new DeviceInput ( DeviceType.Other, Key.A );
			
			//ジョイスティック
			JoystickInput ji = dxJoystick.GetInput ();
			if ( ji.ObDvcType != ObjectDeviceType.NoData )
			{
				di.Type = DeviceType.Joystick;
				di.JoystickInput = ji;
				return di;
			}

			//キーボード
			Key k = dxKeyboard.GetKey ();
			if ( Key.Unknown != k )
			{
				di.Type = DeviceType.Keyboard;
				di.keyboardInput = k;
				return di;
			}

			//未入力はそのまま返す
			return di;
		}

		//押した瞬間の取得
		public DeviceInput PushInput ()
		{
			DeviceInput di = new DeviceInput ( DeviceType.Other, Key.A );

			//ジョイスティック
			JoystickInput ji = dxJoystick.PushInput ();
			if ( ji.ObDvcType != ObjectDeviceType.NoData )
			{
				di.Type = DeviceType.Joystick;
				di.JoystickInput = ji;
				return di;
			}

			//キーボード
			Key k = dxKeyboard.PushKey ();
			if ( Key.Unknown != k )
			{
				di.Type = DeviceType.Keyboard;
				di.keyboardInput = k;
				return di;
			}

			return di;
		}
	}
}
