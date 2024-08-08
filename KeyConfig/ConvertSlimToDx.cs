using SlimDX.DirectInput;

namespace ScriptEditor
{
    public class ConvertSlimToDx
    {
		//--------------------------------------------
		//キーボード定数
		//(C#)SlimDxから(設定保存ファイル)(C++)DirectInput定義の定数に変換
		//--------------------------------------------
        public int Keyboard ( Key key )
        {
			int ret = 0;

            switch ( key )
            {
			case Key.D0 : ret = (int)DxKey.DIK_0;	break;
			case Key.D1 : ret = (int)DxKey.DIK_1;	break;
			case Key.D2 : ret = (int)DxKey.DIK_2;	break;
			case Key.D3 : ret = (int)DxKey.DIK_3;	break;
			case Key.D4 : ret = (int)DxKey.DIK_4;	break;
			case Key.D5 : ret = (int)DxKey.DIK_5;	break;
			case Key.D6 : ret = (int)DxKey.DIK_6;	break;
			case Key.D7 : ret = (int)DxKey.DIK_7;	break;
			case Key.D8 : ret = (int)DxKey.DIK_8;	break;
			case Key.D9 : ret = (int)DxKey.DIK_9;	break;
			case Key.A : ret = (int)DxKey.DIK_A;  break;
			case Key.B : ret = (int)DxKey.DIK_B;  break;
			case Key.C : ret = (int)DxKey.DIK_C;  break;
			case Key.D : ret = (int)DxKey.DIK_D;  break;
			case Key.E : ret = (int)DxKey.DIK_E;  break;
			case Key.F : ret = (int)DxKey.DIK_F;  break;
			case Key.G : ret = (int)DxKey.DIK_G;  break;
			case Key.H : ret = (int)DxKey.DIK_H;  break;
			case Key.I : ret = (int)DxKey.DIK_I;  break;
			case Key.J : ret = (int)DxKey.DIK_J;  break;
			case Key.K : ret = (int)DxKey.DIK_K;  break;
			case Key.L : ret = (int)DxKey.DIK_L;  break;
			case Key.M : ret = (int)DxKey.DIK_M;  break;
			case Key.N : ret = (int)DxKey.DIK_N;  break;
			case Key.O : ret = (int)DxKey.DIK_O;  break;
			case Key.P : ret = (int)DxKey.DIK_P;  break;
			case Key.Q : ret = (int)DxKey.DIK_Q;  break;
			case Key.R : ret = (int)DxKey.DIK_R;  break;
			case Key.S : ret = (int)DxKey.DIK_S;  break;
			case Key.T : ret = (int)DxKey.DIK_T;  break;
			case Key.U : ret = (int)DxKey.DIK_U;  break;
			case Key.V : ret = (int)DxKey.DIK_V;  break;
			case Key.W : ret = (int)DxKey.DIK_W;  break;
			case Key.X : ret = (int)DxKey.DIK_X;  break;
			case Key.Y : ret = (int)DxKey.DIK_Y;  break;
			case Key.Z : ret = (int)DxKey.DIK_Z;  break;

			case Key.AbntC1 : ret = (int)DxKey.DIK_ABNT_C1; break;
			case Key.AbntC2 : ret = (int)DxKey.DIK_ABNT_C2; break;
			case Key.Apostrophe : ret = (int)DxKey.DIK_APOSTROPHE;  break;
			case Key.Applications : ret = (int)DxKey.DIK_APPS;  break;
			case Key.AT : ret = (int)DxKey.DIK_AT;  break;
			case Key.AX : ret = (int)DxKey.DIK_AX;  break;
			case Key.Backspace : ret = (int)DxKey.DIK_BACKSPACE;  break;
			case Key.Backslash : ret = (int)DxKey.DIK_BACKSLASH;  break;
			case Key.Calculator : ret = (int)DxKey.DIK_CALCULATOR;  break;
			case Key.CapsLock : ret = (int)DxKey.DIK_CAPSLOCK;  break;
			case Key.Colon : ret = (int)DxKey.DIK_COLON;  break;
			case Key.Comma : ret = (int)DxKey.DIK_COMMA;  break;
			case Key.Convert : ret = (int)DxKey.DIK_CONVERT;  break;
			case Key.Delete : ret = (int)DxKey.DIK_DELETE;  break;
			case Key.DownArrow : ret = (int)DxKey.DIK_DOWNARROW;  break;
			case Key.End : ret = (int)DxKey.DIK_END;  break;
			case Key.Equals : ret = (int)DxKey.DIK_EQUALS;  break;
			case Key.Escape : ret = (int)DxKey.DIK_ESCAPE;  break;

			case Key.F1 : ret = (int)DxKey.DIK_F1;  break;
			case Key.F2 : ret = (int)DxKey.DIK_F2;  break;
			case Key.F3 : ret = (int)DxKey.DIK_F3;  break;
			case Key.F4 : ret = (int)DxKey.DIK_F4;  break;
			case Key.F5 : ret = (int)DxKey.DIK_F5;  break;
			case Key.F6 : ret = (int)DxKey.DIK_F6;  break;
			case Key.F7 : ret = (int)DxKey.DIK_F7;  break;
			case Key.F8 : ret = (int)DxKey.DIK_F8;  break;
			case Key.F9 : ret = (int)DxKey.DIK_F9;  break;
			case Key.F10 : ret = (int)DxKey.DIK_F10;  break;
			case Key.F11 : ret = (int)DxKey.DIK_F11;  break;
			case Key.F12 : ret = (int)DxKey.DIK_F12;  break;
			case Key.F13 : ret = (int)DxKey.DIK_F13;  break;
			case Key.F14 : ret = (int)DxKey.DIK_F14;  break;
			case Key.F15 : ret = (int)DxKey.DIK_F15;  break;

			case Key.Grave : ret = (int)DxKey.DIK_GRAVE;  break;
			case Key.Home : ret = (int)DxKey.DIK_HOME;  break;
			case Key.Insert : ret = (int)DxKey.DIK_INSERT;  break;
			case Key.Kana : ret = (int)DxKey.DIK_KANA;  break;
			case Key.Kanji : ret = (int)DxKey.DIK_KANJI;  break;
			case Key.LeftBracket : ret = (int)DxKey.DIK_LBRACKET;  break;
			case Key.LeftControl : ret = (int)DxKey.DIK_LCONTROL;  break;
			case Key.LeftArrow : ret = (int)DxKey.DIK_LEFTARROW;  break;
			case Key.LeftAlt : ret = (int)DxKey.DIK_LALT;  break;
			case Key.LeftShift : ret = (int)DxKey.DIK_LSHIFT;  break;
			case Key.LeftWindowsKey : ret = (int)DxKey.DIK_LWIN;  break;
			case Key.Mail : ret = (int)DxKey.DIK_MAIL;  break;
			case Key.MediaSelect : ret = (int)DxKey.DIK_MEDIASELECT;  break;
			case Key.MediaStop : ret = (int)DxKey.DIK_MEDIASTOP;  break;
			case Key.Minus : ret = (int)DxKey.DIK_MINUS;  break;
			case Key.Mute : ret = (int)DxKey.DIK_MUTE;  break;
			case Key.MyComputer : ret = (int)DxKey.DIK_MYCOMPUTER;  break;
			case Key.NextTrack : ret = (int)DxKey.DIK_NEXTTRACK;  break;
			case Key.NoConvert : ret = (int)DxKey.DIK_NOCONVERT;  break;

			case Key.NumberLock : ret = (int)DxKey.DIK_NUMLOCK;  break;
			case Key.NumberPad0 : ret = (int)DxKey.DIK_NUMPAD0;  break;
			case Key.NumberPad1 : ret = (int)DxKey.DIK_NUMPAD1;  break;
			case Key.NumberPad2 : ret = (int)DxKey.DIK_NUMPAD2;  break;
			case Key.NumberPad3 : ret = (int)DxKey.DIK_NUMPAD3;  break;
			case Key.NumberPad4 : ret = (int)DxKey.DIK_NUMPAD4;  break;
			case Key.NumberPad5 : ret = (int)DxKey.DIK_NUMPAD5;  break;
			case Key.NumberPad6 : ret = (int)DxKey.DIK_NUMPAD6;  break;
			case Key.NumberPad7 : ret = (int)DxKey.DIK_NUMPAD7;  break;
			case Key.NumberPad8 : ret = (int)DxKey.DIK_NUMPAD8;  break;
			case Key.NumberPad9 : ret = (int)DxKey.DIK_NUMPAD9;  break;
			case Key.NumberPadComma : ret = (int)DxKey.DIK_NUMPADCOMMA;  break;
			case Key.NumberPadEnter : ret = (int)DxKey.DIK_NUMPADENTER;  break;
			case Key.NumberPadEquals : ret = (int)DxKey.DIK_NUMPADEQUALS;  break;
			case Key.NumberPadMinus : ret = (int)DxKey.DIK_NUMPADMINUS;  break;
			case Key.NumberPadPeriod : ret = (int)DxKey.DIK_NUMPADPERIOD;  break;
			case Key.NumberPadPlus : ret = (int)DxKey.DIK_NUMPADPLUS;  break;
			case Key.NumberPadSlash : ret = (int)DxKey.DIK_NUMPADSLASH;  break;
			case Key.NumberPadStar : ret = (int)DxKey.DIK_NUMPADSTAR;  break;

			case Key.Oem102 : ret = (int)DxKey.DIK_OEM_102;  break;
			case Key.PageDown : ret = (int)DxKey.DIK_PGDN;  break;
			case Key.PageUp : ret = (int)DxKey.DIK_PGUP;  break;
			case Key.Pause : ret = (int)DxKey.DIK_PAUSE;  break;
			case Key.Period : ret = (int)DxKey.DIK_PERIOD;  break;
			case Key.PlayPause : ret = (int)DxKey.DIK_PLAYPAUSE;  break;
			case Key.Power : ret = (int)DxKey.DIK_POWER;  break;
			case Key.PreviousTrack : ret = (int)DxKey.DIK_PREVTRACK;  break;
			case Key.RightBracket : ret = (int)DxKey.DIK_RBRACKET;  break;
			case Key.RightControl : ret = (int)DxKey.DIK_RCONTROL;  break;
			case Key.Return : ret = (int)DxKey.DIK_RETURN;  break;
			case Key.RightArrow : ret = (int)DxKey.DIK_RIGHTARROW;  break;
			case Key.RightAlt : ret = (int)DxKey.DIK_RALT;  break;
			case Key.RightShift : ret = (int)DxKey.DIK_RSHIFT;  break;
			case Key.RightWindowsKey : ret = (int)DxKey.DIK_RWIN;  break;
			case Key.ScrollLock : ret = (int)DxKey.DIK_SCROLL;  break;
			case Key.Semicolon : ret = (int)DxKey.DIK_SEMICOLON;  break;
			case Key.Slash : ret = (int)DxKey.DIK_SLASH;  break;
			case Key.Sleep : ret = (int)DxKey.DIK_SLEEP;  break;
			case Key.Space : ret = (int)DxKey.DIK_SPACE;  break;
			case Key.Stop : ret = (int)DxKey.DIK_STOP;  break;
/*			case Key.PrintScreen : ret = (int)DxKey.;  break;	*/
			case Key.Tab : ret = (int)DxKey.DIK_TAB;  break;
			case Key.Underline : ret = (int)DxKey.DIK_UNDERLINE;  break;
			case Key.Unlabeled : ret = (int)DxKey.DIK_UNLABELED;  break;
			case Key.UpArrow : ret = (int)DxKey.DIK_UPARROW;  break;
			case Key.VolumeDown : ret = (int)DxKey.DIK_VOLUMEDOWN;  break;
			case Key.VolumeUp : ret = (int)DxKey.DIK_VOLUMEUP;  break;
			case Key.Wake : ret = (int)DxKey.DIK_WAKE;  break;
			case Key.WebBack : ret = (int)DxKey.DIK_WEBBACK;  break;
			case Key.WebFavorites : ret = (int)DxKey.DIK_WEBFAVORITES;  break;
			case Key.WebForward : ret = (int)DxKey.DIK_WEBFORWARD;  break;
			case Key.WebHome : ret = (int)DxKey.DIK_WEBHOME;  break;
			case Key.WebRefresh : ret = (int)DxKey.DIK_WEBREFRESH;  break;
			case Key.WebSearch : ret = (int)DxKey.DIK_WEBSEARCH;  break;
			case Key.WebStop : ret = (int)DxKey.DIK_WEBSTOP;  break;
			case Key.Yen : ret = (int)DxKey.DIK_YEN;  break;
			case Key.Unknown : ret = (int)Key.Unknown;  break;
			default:  ret = (int)Key.Unknown; break;
            }

            return ret;
        }


		//--------------------------------------------
		//デバイス定数
		//(C#)SlimDxから(設定保存ファイル)(C++)DirectInput定義の定数に変換
		//--------------------------------------------

		//入力デバイス
		public int CnvDeviceType ( DeviceType dvc_type )
		{
			int ret = (int)DeviceTypeForGame.NODEVICE;

			switch ( dvc_type )
			{
			case DeviceType.Joystick: ret = (int)DeviceTypeForGame.JOYSTICK; break;
			case DeviceType.Keyboard: ret = (int)DeviceTypeForGame.KEYBOARD; break;
			case DeviceType.Other: ret = (int)DeviceTypeForGame.NODEVICE; break;
			}

			return ret;
		}

		//ジョイスティックにおける入力の種類
		public int CnvJoyType ( ObjectDeviceType joy_type )
		{
			int ret = (int)JOY_INPUT_TYPE.NO_DATA;

			switch ( joy_type )
			{
			case ObjectDeviceType.Axis: ret = (int)JOY_INPUT_TYPE.AXIS; break;
			case ObjectDeviceType.PointOfViewController: ret = (int)JOY_INPUT_TYPE.POINT_OF_VIEW; break;
			case ObjectDeviceType.Button: ret = (int)JOY_INPUT_TYPE.BUTTON; break;
			}

			return ret;
		}

    }

}
