using SlimDX.DirectInput;


namespace ScriptEditor
{
    //--------------------------------------------
    //キーボード定数
    // (設定保存ファイル)(C++)DirectInput から (C#)SlimDx 定義の定数に変換
    //--------------------------------------------
	public class ConvertDxToSlim
	{
		public Key Keyboard ( DxKey dxkey )
		{
			Key k = Key.Unknown;

            switch ( dxkey )
            {
			case DxKey.DIK_0 : k = Key.D0; break;
			case DxKey.DIK_1 : k = Key.D1; break;
			case DxKey.DIK_2 : k = Key.D2; break;
			case DxKey.DIK_3 : k = Key.D3; break;
			case DxKey.DIK_4 : k = Key.D4; break;
			case DxKey.DIK_5 : k = Key.D5; break;
			case DxKey.DIK_6 : k = Key.D6; break;
			case DxKey.DIK_7 : k = Key.D7; break;
			case DxKey.DIK_8 : k = Key.D8; break;
			case DxKey.DIK_9 : k = Key.D9; break;
			case DxKey.DIK_A : k = Key.A; break;
			case DxKey.DIK_B : k = Key.B; break;
			case DxKey.DIK_C : k = Key.C; break;
			case DxKey.DIK_D : k = Key.D; break;
			case DxKey.DIK_E : k = Key.E; break;
			case DxKey.DIK_F : k = Key.F; break;
			case DxKey.DIK_G : k = Key.G; break;
			case DxKey.DIK_H : k = Key.H; break;
			case DxKey.DIK_I : k = Key.I; break;
			case DxKey.DIK_J : k = Key.J; break;
			case DxKey.DIK_K : k = Key.K; break;
			case DxKey.DIK_L : k = Key.L; break;
			case DxKey.DIK_M : k = Key.M; break;
			case DxKey.DIK_N : k = Key.N; break;
			case DxKey.DIK_O : k = Key.O; break;
			case DxKey.DIK_P : k = Key.P; break;
			case DxKey.DIK_Q : k = Key.Q; break;
			case DxKey.DIK_R : k = Key.R; break;
			case DxKey.DIK_S : k = Key.S; break;
			case DxKey.DIK_T : k = Key.T; break;
			case DxKey.DIK_U : k = Key.U; break;
			case DxKey.DIK_V : k = Key.V; break;
			case DxKey.DIK_W : k = Key.W; break;
			case DxKey.DIK_X : k = Key.X; break;
			case DxKey.DIK_Y : k = Key.Y; break;
			case DxKey.DIK_Z : k = Key.Z; break;

			case DxKey.DIK_ABNT_C1 : k = Key.AbntC1; break;
			case DxKey.DIK_ABNT_C2 : k = Key.AbntC2; break;
			case DxKey.DIK_APOSTROPHE : k = Key.Apostrophe; break;
			case DxKey.DIK_APPS : k = Key.Applications; break;
			case DxKey.DIK_AT : k = Key.AT; break;
			case DxKey.DIK_AX : k = Key.AX; break;
			case DxKey.DIK_BACKSPACE : k = Key.Backspace; break;
			case DxKey.DIK_BACKSLASH : k = Key.Backslash; break;
			case DxKey.DIK_CALCULATOR : k = Key.Calculator; break;
			case DxKey.DIK_CAPSLOCK : k = Key.CapsLock; break;
			case DxKey.DIK_COLON : k = Key.Colon; break;
			case DxKey.DIK_COMMA : k = Key.Comma; break;
			case DxKey.DIK_CONVERT : k = Key.Convert; break;
			case DxKey.DIK_DELETE : k = Key.Delete; break;
			case DxKey.DIK_DOWNARROW : k = Key.DownArrow; break;
			case DxKey.DIK_END : k = Key.End; break;
			case DxKey.DIK_EQUALS : k = Key.Equals; break;
			case DxKey.DIK_ESCAPE : k = Key.Escape; break;

			case DxKey.DIK_F1 : k = Key.F1; break;
			case DxKey.DIK_F2 : k = Key.F2; break;
			case DxKey.DIK_F3 : k = Key.F3; break;
			case DxKey.DIK_F4 : k = Key.F4; break;
			case DxKey.DIK_F5 : k = Key.F5; break;
			case DxKey.DIK_F6 : k = Key.F6; break;
			case DxKey.DIK_F7 : k = Key.F7; break;
			case DxKey.DIK_F8 : k = Key.F8; break;
			case DxKey.DIK_F9 : k = Key.F9; break;
			case DxKey.DIK_F10 : k = Key.F10; break;
			case DxKey.DIK_F11 : k = Key.F11; break;
			case DxKey.DIK_F12 : k = Key.F12; break;
			case DxKey.DIK_F13 : k = Key.F13; break;
			case DxKey.DIK_F14 : k = Key.F14; break;
			case DxKey.DIK_F15 : k = Key.F15; break;

			case DxKey.DIK_GRAVE : k = Key.Grave; break;
			case DxKey.DIK_HOME : k = Key.Home; break;
			case DxKey.DIK_INSERT : k = Key.Insert; break;
			case DxKey.DIK_KANA : k = Key.Kana; break;
			case DxKey.DIK_KANJI : k = Key.Kanji; break;
			case DxKey.DIK_LBRACKET : k = Key.LeftBracket; break;
			case DxKey.DIK_LCONTROL : k = Key.LeftControl; break;
			case DxKey.DIK_LEFTARROW : k = Key.LeftArrow; break;
			case DxKey.DIK_LALT : k = Key.LeftAlt; break;
			case DxKey.DIK_LSHIFT : k = Key.LeftShift; break;
			case DxKey.DIK_LWIN : k = Key.LeftWindowsKey; break;
			case DxKey.DIK_MAIL : k = Key.Mail; break;
			case DxKey.DIK_MEDIASELECT : k = Key.MediaSelect; break;
			case DxKey.DIK_MEDIASTOP : k = Key.MediaStop; break;
			case DxKey.DIK_MINUS : k = Key.Minus; break;
			case DxKey.DIK_MUTE : k = Key.Mute; break;
			case DxKey.DIK_MYCOMPUTER : k = Key.MyComputer; break;
			case DxKey.DIK_NEXTTRACK : k = Key.NextTrack; break;
			case DxKey.DIK_NOCONVERT : k = Key.NoConvert; break;

			case DxKey.DIK_NUMLOCK : k = Key.NumberLock; break;
			case DxKey.DIK_NUMPAD0 : k = Key.NumberPad0; break;
			case DxKey.DIK_NUMPAD1 : k = Key.NumberPad1; break;
			case DxKey.DIK_NUMPAD2 : k = Key.NumberPad2; break;
			case DxKey.DIK_NUMPAD3 : k = Key.NumberPad3; break;
			case DxKey.DIK_NUMPAD4 : k = Key.NumberPad4; break;
			case DxKey.DIK_NUMPAD5 : k = Key.NumberPad5; break;
			case DxKey.DIK_NUMPAD6 : k = Key.NumberPad6; break;
			case DxKey.DIK_NUMPAD7 : k = Key.NumberPad7; break;
			case DxKey.DIK_NUMPAD8 : k = Key.NumberPad8; break;
			case DxKey.DIK_NUMPAD9 : k = Key.NumberPad9; break;
			case DxKey.DIK_NUMPADCOMMA : k = Key.NumberPadComma; break;
			case DxKey.DIK_NUMPADENTER : k = Key.NumberPadEnter; break;
			case DxKey.DIK_NUMPADEQUALS : k = Key.NumberPadEquals; break;
			case DxKey.DIK_NUMPADMINUS : k = Key.NumberPadMinus; break;
			case DxKey.DIK_NUMPADPERIOD : k = Key.NumberPadPeriod; break;
			case DxKey.DIK_NUMPADPLUS : k = Key.NumberPadPlus; break;
			case DxKey.DIK_NUMPADSLASH : k = Key.NumberPadSlash; break;
			case DxKey.DIK_NUMPADSTAR : k = Key.NumberPadStar; break;

			case DxKey.DIK_OEM_102 : k = Key.Oem102; break;
			case DxKey.DIK_PGDN : k = Key.PageDown; break;
			case DxKey.DIK_PGUP : k = Key.PageUp; break;
			case DxKey.DIK_PAUSE : k = Key.Pause; break;
			case DxKey.DIK_PERIOD : k = Key.Period; break;
			case DxKey.DIK_PLAYPAUSE : k = Key.PlayPause; break;
			case DxKey.DIK_POWER : k = Key.Power; break;
			case DxKey.DIK_PREVTRACK : k = Key.PreviousTrack; break;
			case DxKey.DIK_RBRACKET : k = Key.RightBracket; break;
			case DxKey.DIK_RCONTROL : k = Key.RightControl; break;
			case DxKey.DIK_RETURN : k = Key.Return; break;
			case DxKey.DIK_RIGHTARROW : k = Key.RightArrow; break;
			case DxKey.DIK_RALT : k = Key.RightAlt; break;
			case DxKey.DIK_RSHIFT : k = Key.RightShift; break;
			case DxKey.DIK_RWIN : k = Key.RightWindowsKey; break;
			case DxKey.DIK_SCROLL : k = Key.ScrollLock; break;
			case DxKey.DIK_SEMICOLON : k = Key.Semicolon; break;
			case DxKey.DIK_SLASH : k = Key.Slash; break;
			case DxKey.DIK_SLEEP : k = Key.Sleep; break;
			case DxKey.DIK_SPACE : k = Key.Space; break;
			case DxKey.DIK_STOP : k = Key.Stop; break;
/*			case DxKey. : k = Key.PrintScreen; break;	*/
			case DxKey.DIK_TAB : k = Key.Tab; break;
			case DxKey.DIK_UNDERLINE : k = Key.Underline; break;
			case DxKey.DIK_UNLABELED : k = Key.Unlabeled; break;
			case DxKey.DIK_UPARROW : k = Key.UpArrow; break;
			case DxKey.DIK_VOLUMEDOWN : k = Key.VolumeDown; break;
			case DxKey.DIK_VOLUMEUP : k = Key.VolumeUp; break;
			case DxKey.DIK_WAKE : k = Key.Wake; break;
			case DxKey.DIK_WEBBACK : k = Key.WebBack; break;
			case DxKey.DIK_WEBFAVORITES : k = Key.WebFavorites; break;
			case DxKey.DIK_WEBFORWARD : k = Key.WebForward; break;
			case DxKey.DIK_WEBHOME : k = Key.WebHome; break;
			case DxKey.DIK_WEBREFRESH : k = Key.WebRefresh; break;
			case DxKey.DIK_WEBSEARCH : k = Key.WebSearch; break;
			case DxKey.DIK_WEBSTOP : k = Key.WebStop; break;
			case DxKey.DIK_YEN : k = Key.Yen; break;

			default:  k = Key.Unknown; break;
            }

			return k;
		}


		//--------------------------------------------
		//デバイス定数
		//(設定保存ファイル)(C++)DirectInputから(C#)SlimDx定義の定数に変換
		//--------------------------------------------

		//入力デバイス
		public DeviceType CnvDeviceType ( DeviceTypeForGame dvc_type )
		{
			DeviceType ret = DeviceType.Other;

			switch ( dvc_type )
			{
			case DeviceTypeForGame.JOYSTICK: ret = DeviceType.Joystick; break;
			case DeviceTypeForGame.KEYBOARD: ret = DeviceType.Keyboard; break;
			case DeviceTypeForGame.NODEVICE: ret = DeviceType.Other; break;
			}

			return ret;
		}

		//ジョイスティックにおける入力の種類
		public ObjectDeviceType CnvJoyType ( JOY_INPUT_TYPE joy_type )
		{
			ObjectDeviceType ret = ObjectDeviceType.NoData;

			switch ( joy_type )
			{
			case JOY_INPUT_TYPE.AXIS: ret = ObjectDeviceType.Axis; break;
			case JOY_INPUT_TYPE.POINT_OF_VIEW: ret = ObjectDeviceType.PointOfViewController; break;
			case JOY_INPUT_TYPE.BUTTON: ret = ObjectDeviceType.Button; break;
			case JOY_INPUT_TYPE.NO_DATA: ret = ObjectDeviceType.NoData; break;
			}

			return ret;
		}

	}
}
