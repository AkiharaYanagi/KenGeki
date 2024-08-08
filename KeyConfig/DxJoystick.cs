using System;
using System.Collections.Generic;

using SlimDX.DirectInput;


namespace ScriptEditor
{
	//ジョイスティックデータ
	public class JoystickData
	{
		public Joystick joystick;
		public JoystickState jsState = new JoystickState ();
		public JoystickState pre_jsState = new JoystickState ();

		public void State ()
		{
			pre_jsState = jsState;
			jsState = joystick.GetCurrentState(); 
		}
	}

	//ジョイスティックを複数持つ

	public class DxJoystick
	{
		//ジョイスティック
//		private List < Joystick > L_joystick = new List<Joystick> ();

		//状態の保存
		private List < JoystickData > L_jsData = new List<JoystickData> ();

		//コンストラクタ
		public DxJoystick ()
		{
		}
		
		//開始
		public void Load ( DirectInput dInput )
		{
			//接続されたデバイスリスト
			DeviceClass dc = DeviceClass.GameController;
			DeviceEnumerationFlags flg = DeviceEnumerationFlags.AttachedOnly;
			IList < DeviceInstance > L_Dvc = dInput.GetDevices ( dc, flg );

			//DirectInputから認識されたジョイスティックを取得する
			foreach ( DeviceInstance dvcInst in L_Dvc )
			{
				Joystick js = new Joystick ( dInput, dvcInst.InstanceGuid );
				js.Acquire ();
				foreach ( DeviceObjectInstance dvcOb in js.GetObjects () )
				{
					if ( 0 != ( dvcOb.ObjectType & ObjectDeviceType.Axis ) )
					{
						ObjectProperties op = js.GetObjectPropertiesById ( (int)dvcOb.ObjectType );
						op.SetRange ( -1000, 1000 );
					}
				}
				//L_joystick.Add ( js );

				JoystickData jsData = new JoystickData ();
				jsData.joystick = js;
				L_jsData.Add ( jsData );
			}
		}

		//毎回の更新
		public void Update ()
		{
			for ( int i = 0; i < L_jsData.Count; ++ i )
			{
				L_jsData[i].joystick.Poll ();
				L_jsData[i].State ();
			}
		}

		//押されている状態のいずれかを優先順で返す
		public JoystickInput GetInput ()
		{
			int deviceID = 0;
			JoystickInput jsi = new JoystickInput ( deviceID, 0 );

			for ( int i = 0; i < L_jsData.Count; ++ i )
			{
				int buttonID = 0;
				bool[] ary_b = L_jsData[i].jsState.GetButtons ();
				foreach ( bool b in ary_b )
				{
					if ( b )
					{
						jsi.ObDvcType = ObjectDeviceType.Button;
						jsi.DeviceID = deviceID;
						jsi.ButtonID = buttonID;
						return jsi;
					}
					++ buttonID;
				}

				++ deviceID;
			}
			return jsi;
		}

		//押された瞬間の入力を優先順で返す
		public JoystickInput PushInput ()
		{
			int deviceID = 0;
			JoystickInput jsi = new JoystickInput ( deviceID, 0 );

			//すべてのデバイスをチェック
			//１つでも該当すればその時点でreturn
			for ( int i = 0; i < L_jsData.Count; ++ i )
			{
				jsi.DeviceID = deviceID;
				//各デバイスの中の入力種類

				// ◆ ボタン
				int buttonID = 0;
				bool[] ary_b = L_jsData[i].jsState.GetButtons ();
				foreach ( bool b in ary_b )
				{
					if ( b )
					{
						//前回の状態をチェック
						bool[] ary_pre_b = L_jsData[i].pre_jsState.GetButtons ();

						//前回もtrueだった場合、非該当
						if ( ary_pre_b [ buttonID ] ) { continue; }

						jsi.ObDvcType = ObjectDeviceType.Button;
						jsi.ButtonID = buttonID;
						return jsi;
					}
					++ buttonID;
				}

				// ◆ PointOfView (POV)
				//POV	: [0]に角度*100 ( 0, 9000, 18000, 27000 )が入る
				//		: ( UP, RIGHT, DOWN, LEFT )
				//		: 未入力のとき  -1
				int[] ary_pov = L_jsData[i].jsState.GetPointOfViewControllers ();
				int pov = ary_pov [ 0 ];

				//前回の状態をチェック
				int[] ary_pre_pov = L_jsData[i].pre_jsState.GetPointOfViewControllers ();
				int pre_pov = ary_pre_pov [ 0 ];

				//現在に入力がありつつ
				if ( pov != -1 )
				{
					//１つ前の入力は無し
					//前回も同一だった場合、非該当
					if ( pov == 0 && pre_pov == -1 )
					{
						jsi.ObDvcType = ObjectDeviceType.PointOfViewController;
						jsi.lvr = LEVER.UP;
						return jsi;
					}
					if ( pov == 9000 && pre_pov == -1 )
					{
						jsi.ObDvcType = ObjectDeviceType.PointOfViewController;
						jsi.lvr = LEVER.RIGHT;
						return jsi;
					}
					if ( pov == 18000 && pre_pov == -1 )
					{
						jsi.ObDvcType = ObjectDeviceType.PointOfViewController;
						jsi.lvr = LEVER.DOWN;
						return jsi;
					}
					if ( pov == 27000 && pre_pov == -1 )
					{
						jsi.ObDvcType = ObjectDeviceType.PointOfViewController;
						jsi.lvr = LEVER.LEFT;
						return jsi;
					}
				}


				// ◆ XY Axis
				int x = L_jsData[i].jsState.X;
				int y = L_jsData[i].jsState.Y;
				int pre_x = L_jsData[i].pre_jsState.X;
				int pre_y = L_jsData[i].pre_jsState.Y;

				if ( y < -500 && pre_y >= -500)
				{
					jsi.ObDvcType = ObjectDeviceType.Axis;
					jsi.lvr = LEVER.UP;
					return jsi;
				}
				if ( y > 500 && pre_y <= 500)
				{
					jsi.ObDvcType = ObjectDeviceType.Axis;
					jsi.lvr = LEVER.DOWN;
					return jsi;
				}
				if ( x < -500 && pre_x >= -500)
				{
					jsi.ObDvcType = ObjectDeviceType.Axis;
					jsi.lvr = LEVER.LEFT;
					return jsi;
				}
				if ( x > 500 && pre_x <= 500)
				{
					jsi.ObDvcType = ObjectDeviceType.Axis;
					jsi.lvr = LEVER.RIGHT;
					return jsi;
				}


				++ deviceID;
			}
			return jsi;
		}

	}
}
