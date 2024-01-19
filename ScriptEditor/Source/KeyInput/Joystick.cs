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
	//	ジョイスティックが設定されているが、一つも接続されていないときのエラー処理
	//	途中で接続を外したときのエラー処理


	//---------------------------------------------------------------------
	//ジョイスティックの管理
	//---------------------------------------------------------------------
	public class Joystick
	{

		//ジョイスティックの配列
		private List<Device> joysticks = new List<Device>();

		//ジョイスティック状態の配列
		private List<JoystickState> joyStates = new List<JoystickState> ();

		//一つ前のジョイスティック状態の配列
		private List<JoystickState> preJoyStates = new List<JoystickState> ();


		//ジョイスティックの方向キーをボタンと統一する
#if false
		public enum JOYSTICK_KEY
		{
			NUM_JOYSTICK_BUTTON = 128,					 //ジョイスティックのボタンの数
			JOYSTICK_UP = NUM_JOYSTICK_BUTTON + 1,       //レバー上
			JOYSTICK_DOWN = NUM_JOYSTICK_BUTTON + 2,     //レバー下
			JOYSTICK_LEFT = NUM_JOYSTICK_BUTTON + 3,     //レバー左
			JOYSTICK_RIGHT = NUM_JOYSTICK_BUTTON + 4,    //レバー右
			JOYSTICK_NEUTRAL = NUM_JOYSTICK_BUTTON + 5,    //未入力
		};
#endif

		//ボタンと方向の定数
		//ボタン数と合わせて比較のためにenumではなくconst intにする
		public const int NUM_JOYSTICK_BUTTON = 128;   //ジョイスティックのボタンの数
		public const int JOYSTICK_UP		 = NUM_JOYSTICK_BUTTON + 0;		//レバー上
		public const int JOYSTICK_DOWN		 = NUM_JOYSTICK_BUTTON + 1;		//レバー下
		public const int JOYSTICK_LEFT		 = NUM_JOYSTICK_BUTTON + 2;		//レバー左
		public const int JOYSTICK_RIGHT		 = NUM_JOYSTICK_BUTTON + 3;		//レバー右
		public const int JOYSTICK_NEUTRAL	 = NUM_JOYSTICK_BUTTON + 4;		//未入力

		public const int NO_DEVICE = -1;	//デバイスが一つも存在しないとき

		//コンストラクタ
		public Joystick ()
		{
			//デバイスリスト
			DeviceList devList = Manager.GetDevices ( DeviceClass.GameControl, EnumDevicesFlags.AllDevices );

			//取得したデバイスリストをすべて登録
			foreach ( DeviceInstance devInst in devList )
			{
				Device joystick = new Device ( devInst.InstanceGuid );
				joystick.SetCooperativeLevel ( null,
					  CooperativeLevelFlags.NonExclusive
					| CooperativeLevelFlags.NoWindowsKey
					| CooperativeLevelFlags.Background
				);

				//ジョイスティックタイプのデータフォーマットを設定
				joystick.SetDataFormat ( DeviceDataFormat.Joystick );

				//Axisレンジを設定
				foreach ( DeviceObjectInstance devObInst in joystick.Objects )
				{
					if ( 0 != ( devObInst.ObjectId & ( int ) DeviceObjectTypeFlags.Axis ) )
					{
						joystick.Properties.SetRange (
							ParameterHow.ById,
							devObInst.ObjectId,
							new InputRange ( -1000, 1000 )
						);
					}
				}

				//軸の絶対位置モードを設定
				joystick.Properties.AxisModeAbsolute = true;

				//デバイスの動作
				try { joystick.Acquire (); }
				catch ( Microsoft.DirectX.DirectXException )
				{
				}

				joysticks.Add ( joystick );

				//状態保存
				JoystickState js = new DirectInput.JoystickState ();
				joyStates.Add ( js );
				preJoyStates.Add ( js );
			}

		}


		//毎回の更新
		public void Update ()
		{
			int index = 0;
			foreach ( Device joystick in joysticks )
			{
				preJoyStates[index] = joyStates[index];
				try
				{
					joystick.Poll ();
				}
				catch ( NotAcquiredException e1 )
				{
					Debug.WriteLine ( e1.ToString () );
					joystick.Acquire ();
					joystick.Poll ();
				}
				joyStates[index] = joystick.CurrentJoystickState;
				++index;
			}
		}

		//ボタンが押されているかどうか
		//引数：デバイスID, ボタンID
		public bool IsButton ( int deviceID, int buttonID )
		{
			return joyStates[deviceID].GetButtons ()[buttonID].Equals ( 128 );
		}

		//ボタンが押された瞬間かどうか(前回off 今回on)
		//引数：デバイスID, ボタンID
		public bool PushButton ( int deviceID, int buttonID )
		{
			bool b = joyStates[deviceID].GetButtons ()[buttonID].Equals ( 128 );
			bool pb = preJoyStates[deviceID].GetButtons ()[buttonID].Equals ( 128 );
			return ( b && ! pb );
		}

		//ボタンが押し続けている状態かどうか(前回on 今回on)
		//引数：デバイスID, ボタンID
		public bool KeepButton ( int deviceID, int buttonID )
		{
			bool b = joyStates[deviceID].GetButtons ()[buttonID].Equals ( 128 );
			bool pb = preJoyStates[deviceID].GetButtons ()[buttonID].Equals ( 128 );
			return ( b && pb );
		}

		//ボタンが離された瞬間かどうか(前回on 今回off)
		//引数：デバイスID, ボタンID
		public bool ReleaseButton ( int deviceID, int buttonID )
		{
			bool b = joyStates[deviceID].GetButtons ()[buttonID].Equals ( 128 );
			bool pb = preJoyStates[deviceID].GetButtons ()[buttonID].Equals ( 128 );
			return ( ! b && pb );
		}

		//レバー方向（上下左右）
		public bool IsUp    ( int deviceID ) { return ( joyStates[deviceID].Y < -500 ); }
		public bool IsDown  ( int deviceID ) { return ( joyStates[deviceID].Y >  500 ); }
		public bool IsLeft  ( int deviceID ) { return ( joyStates[deviceID].X < -500 ); }
		public bool IsRight ( int deviceID ) { return ( joyStates[deviceID].X >  500 ); }

		public bool PushUp    ( int deviceID ) { return ( joyStates[deviceID].Y < -500 ) && ! ( preJoyStates[deviceID].Y < -500 ); }
		public bool PushDown  ( int deviceID ) { return ( joyStates[deviceID].Y >  500 ) && ! ( preJoyStates[deviceID].Y >  500 ); }
		public bool PushLeft  ( int deviceID ) { return ( joyStates[deviceID].X < -500 ) && ! ( preJoyStates[deviceID].X < -500 ); }
		public bool PushRight ( int deviceID ) { return ( joyStates[deviceID].X >  500 ) && ! ( preJoyStates[deviceID].X >  500 ); }

		public bool KeepUp    ( int deviceID ) { return ( joyStates[deviceID].Y < -500 ) && ( preJoyStates[deviceID].Y < -500 ); }
		public bool KeepDown  ( int deviceID ) { return ( joyStates[deviceID].Y >  500 ) && ( preJoyStates[deviceID].Y >  500 ); }
		public bool KeepLeft  ( int deviceID ) { return ( joyStates[deviceID].X < -500 ) && ( preJoyStates[deviceID].X < -500 ); }
		public bool KeepRight ( int deviceID ) { return ( joyStates[deviceID].X >  500 ) && ( preJoyStates[deviceID].X >  500 ); }

		public bool ReleaseUp    ( int deviceID ) { return ! ( joyStates[deviceID].Y < -500 ) && ( preJoyStates[deviceID].Y < -500 ); }
		public bool ReleaseDown  ( int deviceID ) { return ! ( joyStates[deviceID].Y >  500 ) && ( preJoyStates[deviceID].Y >  500 ); }
		public bool ReleaseLeft  ( int deviceID ) { return ! ( joyStates[deviceID].X < -500 ) && ( preJoyStates[deviceID].X < -500 ); }
		public bool ReleaseRight ( int deviceID ) { return ! ( joyStates[deviceID].X >  500 ) && ( preJoyStates[deviceID].X >  500 ); }

		//列挙されたjoystickの個数を返す
		public int GetJoystickNum () { return joysticks.Count; }
	}
}
