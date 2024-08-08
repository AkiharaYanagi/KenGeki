//
//	キー設定のメインデータ
//
using System;
using System.Collections.Generic;
using System.IO;

using SlimDX.DirectInput;


namespace ScriptEditor
{
	using DCT_GM_DVC = Dictionary < GameInput, DeviceInput >;

	public class KeySettings
	{
		//保存ファイル名
		private readonly string Filename = "keyConfig.dat";

		//ゲーム入力とデバイス入力
		public DCT_GM_DVC Dct_Gm_Dvc = new DCT_GM_DVC ();

		//コンストラクタ
		public KeySettings ()
		{
			foreach ( GameInput gi in Enum.GetValues ( typeof ( GameInput ) ) )
			{
				Dct_Gm_Dvc.Add ( gi, new DeviceInput () );
			}
			SetDefault ();
		}

		//保存
		public void Save ()
		{
			ConvertSlimToDx cnvSToD = new ConvertSlimToDx ();

			using ( FileStream fs = new FileStream ( Filename, FileMode.Create, FileAccess.Write ) )
			using ( BinaryWriter bw = new BinaryWriter ( fs ) )
			{
				foreach ( GameInput gmIpt in Dct_Gm_Dvc.Keys )
				{
					DeviceInput dvcIpt = Dct_Gm_Dvc [ gmIpt ];

					//デバイスタイプ
					bw.Write ( cnvSToD.CnvDeviceType ( dvcIpt.Type ) );

					//各デバイスの記録
					switch ( dvcIpt.Type )
					{
					case DeviceType.Keyboard:
						int iKey = cnvSToD.Keyboard ( dvcIpt.keyboardInput );
						bw.Write ( iKey );
					break;

					case DeviceType.Joystick:
						bw.Write ( dvcIpt.JoystickInput.DeviceID );
						bw.Write ( cnvSToD.CnvJoyType ( dvcIpt.JoystickInput.ObDvcType ) );
						bw.Write ( dvcIpt.JoystickInput.ButtonID );
						bw.Write ( (int)dvcIpt.JoystickInput.lvr );
					break;
					}

				}

				bw.Flush ();
			}
		}

		//読込
		public void Load ()
		{
			try
			{ 
				using ( FileStream fs = new FileStream ( Filename, FileMode.Open, FileAccess.Read ) )
				using ( BinaryReader br = new BinaryReader ( fs ) )
				{
					_Load ( br );
				}
			}			
			//ファイルが見つからないとき
			catch ( Exception e ) when ( e is FileNotFoundException || e is FileLoadException)
			{
				e.ToString ();
				//デフォルトの値を設定
				SetDefault ();
			}
			catch
			{
				//デフォルトの値を設定
				SetDefault ();
			}
		}

		private void _Load ( BinaryReader br )
		{
			ConvertDxToSlim cnvDToS = new ConvertDxToSlim ();
			foreach ( GameInput gmIpt in Dct_Gm_Dvc.Keys )
			{
				DeviceInput dvcIpt = Dct_Gm_Dvc [ gmIpt ];

				int dvcType = br.ReadInt32 ();
				dvcIpt.Type = cnvDToS.CnvDeviceType ( (DeviceTypeForGame)dvcType );

				//各デバイスの記録
				switch ( dvcIpt.Type )
				{
					case DeviceType.Keyboard:
					int iKey = br.ReadInt32 ();
					dvcIpt.keyboardInput = cnvDToS.Keyboard ( (DxKey)iKey );
					break;

					case DeviceType.Joystick:
					dvcIpt.JoystickInput.DeviceID = br.ReadInt32 ();
					dvcIpt.JoystickInput.ObDvcType = cnvDToS.CnvJoyType ( (JOY_INPUT_TYPE)br.ReadInt32 () );
					dvcIpt.JoystickInput.ButtonID = br.ReadInt32 ();
					dvcIpt.JoystickInput.lvr = (LEVER)br.ReadInt32 ();
					break;
				}
			}
		}

		//データ指定
		public void Set ( GameInput gmIpt, DeviceInput dvcIpt )
		{
			Dct_Gm_Dvc [ gmIpt ] = dvcIpt;
		}

		//デフォルトのデータ
		private void SetDefault ()
		{
			Set ( GameInput.P1_UP		, new DeviceInput ( DeviceType.Keyboard, Key.UpArrow ) );
			Set ( GameInput.P1_DOWN		, new DeviceInput ( DeviceType.Keyboard, Key.DownArrow ) );
			Set ( GameInput.P1_LEFT		, new DeviceInput ( DeviceType.Keyboard, Key.LeftArrow ) );
			Set ( GameInput.P1_RIGHT	, new DeviceInput ( DeviceType.Keyboard, Key.RightArrow ) );
			Set ( GameInput.P1_KEY0		, new DeviceInput ( DeviceType.Keyboard, Key.Z ) );
			Set ( GameInput.P1_KEY1		, new DeviceInput ( DeviceType.Keyboard, Key.X ) );
			Set ( GameInput.P1_KEY2		, new DeviceInput ( DeviceType.Keyboard, Key.C ) );
			Set ( GameInput.P1_KEY3		, new DeviceInput ( DeviceType.Keyboard, Key.V ) );
			Set ( GameInput.P1_KEY4		, new DeviceInput ( DeviceType.Keyboard, Key.Comma ) );
			Set ( GameInput.P1_KEY5		, new DeviceInput ( DeviceType.Keyboard, Key.Period ) );
			Set ( GameInput.P1_KEY6		, new DeviceInput ( DeviceType.Keyboard, Key.Slash ) );
			Set ( GameInput.P1_KEY7		, new DeviceInput ( DeviceType.Keyboard, Key.Backslash ) );

			Set ( GameInput.P2_UP		, new DeviceInput ( DeviceType.Keyboard, Key.Home ) );
			Set ( GameInput.P2_DOWN		, new DeviceInput ( DeviceType.Keyboard, Key.End ) );
			Set ( GameInput.P2_LEFT		, new DeviceInput ( DeviceType.Keyboard, Key.Delete ) );
			Set ( GameInput.P2_RIGHT	, new DeviceInput ( DeviceType.Keyboard, Key.PageDown ) );
			Set ( GameInput.P2_KEY0		, new DeviceInput ( DeviceType.Keyboard, Key.A ) );
			Set ( GameInput.P2_KEY1		, new DeviceInput ( DeviceType.Keyboard, Key.S ) );
			Set ( GameInput.P2_KEY2		, new DeviceInput ( DeviceType.Keyboard, Key.D ) );
			Set ( GameInput.P2_KEY3		, new DeviceInput ( DeviceType.Keyboard, Key.F ) );
			Set ( GameInput.P2_KEY4		, new DeviceInput ( DeviceType.Keyboard, Key.L ) );
			Set ( GameInput.P2_KEY5		, new DeviceInput ( DeviceType.Keyboard, Key.Semicolon ) );
			Set ( GameInput.P2_KEY6		, new DeviceInput ( DeviceType.Keyboard, Key.Colon ) );
			Set ( GameInput.P2_KEY7		, new DeviceInput ( DeviceType.Keyboard, Key.RightBracket ) );
		}		  

	}

}
