using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;


using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;
using DirectInput=Microsoft.DirectX.DirectInput;


namespace ScriptEditor
{
	//--------------------------------------------------------------------
	// GameMainから呼び出し、KeySettingで設定されたキーが押されたか判定する
	//--------------------------------------------------------------------

	// GameMain
	//	└KeyInput
	//		├KeyConfig	設定	<-KeySetting 設定を外部ファイルに保存・読込
	//		└DxInput
	//			├Keyboard
	//			└Joystick

	//sealed : 継承不可
	public sealed class KeyInput
	{

		//--------------------------------------------------------------------
		//シングルトンパターン
		private static readonly KeyInput _instance = new KeyInput ();
		public static KeyInput Instance { get { return _instance; } }


		//プライベート コンストラクタ
		private KeyInput ()
		{
		}

		//--------------------------------------------------------------------

		//DirectInputオブジェクト
		private DxInput dxInput = new DxInput();

		//キー設定
		private KeyConfig keyConfig = new KeyConfig ();
		public KeyConfig RefKeyConfig { get { return keyConfig; }  }

		//デバイスによる入力をゲームの入力の分だけ保持
		private DeviceInput[] devInput = new DeviceInput[( int ) GAME_INPUT.KEY_NAME_NUM];


		//毎フレームにおける入力の更新
		public void Update ()
		{
			dxInput.Update ();
		}

		//押された状態のものの中で先頭を返す
		public DeviceInput GetInput ()
		{
			DeviceInput di = new DeviceInput ();

			//ジョイスティック
			di.deviceID = dxInput.GetDeviceId ();

			//押された状態のものがあった場合
			if ( Joystick.NO_DEVICE != di.deviceID )
			{
				di.joystickKey = dxInput.GetJoyKey ( di.deviceID );
				if ( Joystick.JOYSTICK_NEUTRAL != di.joystickKey )
				{
					di.type = GAME_DEVICE_TYPE.JOYSTICK;
					return di;
				}
			}

			//キーボード
			di.keyboard = dxInput.GetKey ();
			if ( Key.Escape != di.keyboard )
			{
				di.type = GAME_DEVICE_TYPE.KEYBOARD;
				return di;
			}

			di.type = GAME_DEVICE_TYPE.NODEVICE;
			return di;
		}

		//押された瞬間ものの中で先頭を返す
		public DeviceInput PushInput ()
		{
			DeviceInput di = new DeviceInput ();

			//ジョイスティック
			di.deviceID = dxInput.PushDeviceId ();

			//押された状態のものがあった場合
			if ( Joystick.NO_DEVICE != di.deviceID )
			{
				di.joystickKey = dxInput.PushJoyKey ( di.deviceID );
				if ( Joystick.JOYSTICK_NEUTRAL != di.joystickKey )
				{
					di.type = GAME_DEVICE_TYPE.JOYSTICK;
					return di;
				}
			}

			//キーボード
			di.keyboard = dxInput.PushKey ();
			if ( Key.Escape != di.keyboard )
			{
				di.type = GAME_DEVICE_TYPE.KEYBOARD;
				return di;
			}

			di.type = GAME_DEVICE_TYPE.NODEVICE;
			return di;
		}


		//押した状態かどうか(今回のUpdateのみの結果)
		public bool IsKey ( GAME_INPUT gameInput )
		{
			//対応するデバイス入力の取得
			DeviceInput di =  keyConfig.Dic[gameInput];

			//ジョイスティックの場合
			if ( di.type == GAME_DEVICE_TYPE.JOYSTICK )
			{
				//指定されたデバイスが存在しないとき何もしない(deviceIDは０スタート)
				if ( dxInput.GetJoystickNum() <= di.deviceID ) { return false; }
 
				//ジョイスティックボタンとXY軸で分岐する
				switch ( di.joystickKey )
				{
				case Joystick.JOYSTICK_UP:       //レバー上
					return dxInput.IsJoyUp ( di.deviceID );
				case Joystick.JOYSTICK_DOWN:     //レバー下
					return dxInput.IsJoyDown ( di.deviceID );
				case Joystick.JOYSTICK_LEFT:     //レバー左
					return dxInput.IsJoyLeft ( di.deviceID );
				case Joystick.JOYSTICK_RIGHT:    //レバー右
					return dxInput.IsJoyRight ( di.deviceID );
				default:				//その他ボタン
					return dxInput.IsJoyButton ( di.deviceID, (int)di.joystickKey );
				}
			}
			//キーボードの場合
			else if ( di.type == GAME_DEVICE_TYPE.KEYBOARD )
			{
				return dxInput.IsKeyboard ( di.keyboard );
			}
			return false;
		}

		//押した瞬間かどうか(前回off 今回on)
		public bool PushKey ( GAME_INPUT gameInput )
		{
			//対応するデバイス入力の取得
			DeviceInput di =  keyConfig.Dic[gameInput];

			//ジョイスティックの場合
			if ( di.type == GAME_DEVICE_TYPE.JOYSTICK )
			{
				//指定されたデバイスが存在しないとき何もしない(deviceIDは０スタート)
				if ( dxInput.GetJoystickNum () <= di.deviceID ) { return false; }

				//ジョイスティックボタンとXY軸で分岐する
				switch ( di.joystickKey )
				{
				case Joystick.JOYSTICK_UP:       //レバー上
					return dxInput.PushJoyUp ( di.deviceID );
				case Joystick.JOYSTICK_DOWN:     //レバー下
					return dxInput.PushJoyDown ( di.deviceID );
				case Joystick.JOYSTICK_LEFT:     //レバー左
					return dxInput.PushJoyLeft ( di.deviceID );
				case Joystick.JOYSTICK_RIGHT:    //レバー右
					return dxInput.PushJoyRight ( di.deviceID );
				default:				//その他ボタン
					return dxInput.PushJoyButton ( di.deviceID, di.joystickKey );
				}
			}
			//キーボードの場合
			else if ( di.type == GAME_DEVICE_TYPE.KEYBOARD )
			{
				return dxInput.PushKeyboard ( di.keyboard );
			}
			return false;
		}

		//押し続けている状態かどうか(前回on 今回on)
		public bool KeepKey ( GAME_INPUT gameInput )
		{
			//対応するデバイス入力の取得
			DeviceInput di =  keyConfig.Dic[gameInput];

			//ジョイスティックの場合
			if ( di.type == GAME_DEVICE_TYPE.JOYSTICK )
			{
				//指定されたデバイスが存在しないとき何もしない(deviceIDは０スタート)
				if ( dxInput.GetJoystickNum () <= di.deviceID ) { return false; }

				//ジョイスティックボタンとXY軸で分岐する
				switch ( di.joystickKey )
				{
				case Joystick.JOYSTICK_UP:       //レバー上
					return dxInput.KeepJoyUp ( di.deviceID );
				case Joystick.JOYSTICK_DOWN:     //レバー下
					return dxInput.KeepJoyDown ( di.deviceID );
				case Joystick.JOYSTICK_LEFT:     //レバー左
					return dxInput.KeepJoyLeft ( di.deviceID );
				case Joystick.JOYSTICK_RIGHT:    //レバー右
					return dxInput.KeepJoyRight ( di.deviceID );
				default:				//その他ボタン
					return dxInput.KeepJoyButton ( di.deviceID, ( int ) di.joystickKey );
				}
			}
			//キーボードの場合
			else if ( di.type == GAME_DEVICE_TYPE.KEYBOARD )
			{
				return dxInput.KeepKeyboard ( di.keyboard );
			}
			return false;
		}

		//離した瞬間かどうか(前回on 今回off)
		public bool ReleaseKey ( GAME_INPUT gameInput )
		{
			//対応するデバイス入力の取得
			DeviceInput di =  keyConfig.Dic[gameInput];

			//ジョイスティックの場合
			if ( di.type == GAME_DEVICE_TYPE.JOYSTICK )
			{
				//指定されたデバイスが存在しないとき何もしない(deviceIDは０スタート)
				if ( dxInput.GetJoystickNum () <= di.deviceID ) { return false; }

				//ジョイスティックボタンとXY軸で分岐する
				switch ( di.joystickKey )
				{
				case Joystick.JOYSTICK_UP:       //レバー上
					return dxInput.ReleaseJoyUp ( di.deviceID );
				case Joystick.JOYSTICK_DOWN:     //レバー下
					return dxInput.ReleaseJoyDown ( di.deviceID );
				case Joystick.JOYSTICK_LEFT:     //レバー左
					return dxInput.ReleaseJoyLeft ( di.deviceID );
				case Joystick.JOYSTICK_RIGHT:    //レバー右
					return dxInput.ReleaseJoyRight ( di.deviceID );
				default:				//その他ボタン
					return dxInput.ReleaseJoyButton ( di.deviceID, ( int ) di.joystickKey );
				}
			}
			//キーボードの場合
			else if ( di.type == GAME_DEVICE_TYPE.KEYBOARD )
			{
				return dxInput.ReleaseKeyboard ( di.keyboard );
			}
			return false;
		}

	}
}
