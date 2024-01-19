using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.IO;
using System.Diagnostics;


using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;
using DirectInput=Microsoft.DirectX.DirectInput;


namespace ScriptEditor
{
	//=============================================================================
	//	共通定義
	//=============================================================================
	
	//ゲームで用いる入力の種類
	public enum GAME_INPUT
	{
		P1_UP,
		P1_DOWN,
		P1_LEFT,
		P1_RIGHT,
		P1_BUTTON1,
		P1_BUTTON2,
		P1_BUTTON3,
		P1_BUTTON4,

		P2_UP,
		P2_DOWN,
		P2_LEFT,
		P2_RIGHT,
		P2_BUTTON1,
		P2_BUTTON2,
		P2_BUTTON3,
		P2_BUTTON4,

		KEY_NAME_NUM,	//ゲームで用いる入力の数
	};

	//デバイスタイプ
	public enum GAME_DEVICE_TYPE
	{
		KEYBOARD,
		JOYSTICK,
		NODEVICE,
	};

#if false
	//ジョイスティックのデバイスIDとキー
	//方向キーとボタンをJoystick.JOYSTICK_KEYにて統一
	public struct JoystickKey
	{
		public int deviceID { get; set; }
		public Joystick.JOYSTICK_KEY joystickKey { get; set; }
	}

	//キー設定
	public struct KEY_CONFIG
	{
		public GAME_DEVICE_TYPE type { get; set; }	//デバイスの種類
		public int device { get; set; }				//デバイス番号
		public int key { get; set; }				//キー番号
	};
#endif

	//デバイス入力  ジョイスティック、キーボードの値を一つか、または未入力状態を持つ。
	public struct DeviceInput
	{
		//デバイスタイプ ( KEYBOARD, JOYSTICK, NODEVICE )
		public GAME_DEVICE_TYPE type { get; set; }

		//ジョイスティック
		public int deviceID { get; set; }
		public int joystickKey { get; set; }        //ジョイスティック入力

		//キーボード
		public Key keyboard { get; set; }		  //キーボード入力

#if false
		//コンストラクタ
		public DeviceInput ( GAME_DEVICE_TYPE deviceType, int device, Joystick.JOYSTICK_KEY joyKey, Key keybord )
		{
			type = deviceType;
			joystickKey = joyKey;
			key = keyboard;
		}

		//文字列変換オーバーライド
		public override string ToString ()
		{
			StrGameInput str = new StrGameInput ();
			return str.GetName ( this );
		}
#endif
	}

	//=============================================================================

	
	//ゲームで用いるキーにキーボードとジョイスティックに割り振った設定
	//バイナリファイルI/O
	public class KeyConfig
	{
		//デバイス入力とゲーム入力の組み合わせで保持する
		private Dictionary<GAME_INPUT, DeviceInput> dic = new Dictionary<GAME_INPUT, DeviceInput> ();
		public Dictionary<GAME_INPUT, DeviceInput> Dic { get { return dic; } }


		//コンストラクタ
		public KeyConfig ()
		{
			//デバイス入力をゲーム入力の数だけ保持する
			foreach ( GAME_INPUT gameInput in Enum.GetValues ( typeof ( GAME_INPUT ) ) )
			{
				if ( GAME_INPUT.KEY_NAME_NUM == gameInput ) { continue; } 
				dic.Add ( gameInput, new DeviceInput () );
			}

			Read ();
		}

		private const string fileName = "keyconfig.dat";

		//ファイル読込
		public void Read ()
		{
			if ( ! File.Exists ( fileName ) ) { return; }

			FileStream fstrm = new FileStream ( fileName, FileMode.Open, FileAccess.Read );
			BinaryReader biReader = new BinaryReader ( fstrm );

			dic.Clear ();

			foreach ( GAME_INPUT gi in Enum.GetValues ( typeof ( GAME_INPUT ) ) )
			{
				if ( GAME_INPUT.KEY_NAME_NUM == gi ) { continue; }
				
				DeviceInput di = new DeviceInput ();

				di.type = ( GAME_DEVICE_TYPE ) biReader.ReadByte ();

				if ( GAME_DEVICE_TYPE.JOYSTICK == di.type )
				{
					di.deviceID = ( int ) biReader.ReadByte ();
					di.joystickKey = ( int ) biReader.ReadByte ();
				}
				else
				{
					di.keyboard = ( Key ) biReader.ReadByte ();
				}

				dic.Add ( gi, di );
			}

			biReader.Close ();
			fstrm.Close ();
		}

		//ファイル書出
		public void Write ()
		{
#if false
			//テキスト書出
			StreamWriter strmWriter = new StreamWriter ( fileName, false, Encoding.GetEncoding ( "Shift_JIS" ) );

			foreach ( DeviceInput devInput in dic.Values )
			{
				strmWriter.Write ( devInput.type.ToString() + ", " );

				if ( GAME_DEVICE_TYPE.JOYSTICK == devInput.type )
				{
					strmWriter.Write ( devInput.deviceID.ToString () + ", " );
					strmWriter.WriteLine ( devInput.joystickKey.ToString () );
				}
				else
				{
					strmWriter.WriteLine ( devInput.keyboard.ToString () );
				}
			}

			strmWriter.Close ();
#endif			
			
			//ファイルストリーム
			FileStream fstrm = new FileStream ( fileName, FileMode.Create, FileAccess.Write );

			//バイナリライタ
			BinaryWriter biWriter = new BinaryWriter ( fstrm );


			//バイナリ書出
			foreach ( DeviceInput devInput in dic.Values )
			{
				biWriter.Write ( ( byte ) devInput.type );

				if ( GAME_DEVICE_TYPE.JOYSTICK == devInput.type )
				{
					biWriter.Write ( ( byte ) devInput.deviceID );
					//JoystickKey最大値 127 + 5
					biWriter.Write ( ( byte ) devInput.joystickKey );
				}
				else
				{
					//Key最大値 MediaSelect(==237)
					biWriter.Write ( ( byte ) devInput.keyboard );
				}
			}

			biWriter.Close ();
			fstrm.Close ();
			
			
		}
	}
}
