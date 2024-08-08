//-------------------------------------------------------------------
//	デバイス入力保存  
//		ジョイスティック、キーボードの値を１つ、または未入力状態を持つ。
//-------------------------------------------------------------------
using System;

using SlimDX.DirectInput;


namespace ScriptEditor
{
	public class DeviceInput
	{
		//デバイスタイプ
		public DeviceType Type { get; set; } = DeviceType.Other;

		//各デバイス入力
		public JoystickInput JoystickInput { get; set; } = new JoystickInput ();
		public Key keyboardInput { get; set; } = Key.Unknown;

		//コンストラクタ
		public DeviceInput ()
		{
		}

		public DeviceInput ( DeviceType t, Key k )
		{
			Type = t;
			keyboardInput = k;
		}

		//文字列
		public override string ToString ()
		{
			string str = "";
			switch ( Type )
			{
			case DeviceType.Joystick: str = JoystickInput.ToString (); break;
			case DeviceType.Keyboard: str = keyboardInput.ToString (); break;
			default: break;
			}
			return str;
		}
	}

	//ジョイスティック入力
	public class JoystickInput
	{
		public int DeviceID = 0;

		//入力の種類
		public ObjectDeviceType ObDvcType = ObjectDeviceType.NoData;

		//ボタン
		public int ButtonID = 0;

		//レバー
		public LEVER lvr = LEVER.UP;

		public JoystickInput ()
		{

		}

		public JoystickInput ( int device, int button )
		{
			DeviceID = device;
			ButtonID = button;
		}

		public override string ToString ()
		{
			string ret = "";

			if ( ObDvcType == ObjectDeviceType.Button )
			{
				ret = "Joy" + DeviceID.ToString () + "_Btn" + ButtonID;
			}

			if ( ObDvcType == ObjectDeviceType.PointOfViewController )
			{
				ret = "Joy" + DeviceID.ToString () + "_POV_" + lvr.ToString ();
			}

			if ( ObDvcType == ObjectDeviceType.Axis )
			{
				ret = "Joy" + DeviceID.ToString () + "_Axis_" + lvr.ToString ();
			}

			return ret;
		}
	}
}
