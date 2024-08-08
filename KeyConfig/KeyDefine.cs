namespace ScriptEditor
{
	//ゲームで用いる入力種類
    public enum GameInput
    {
        P1_UP,
		P1_DOWN,
		P1_LEFT,
		P1_RIGHT,
		P1_KEY0,
        P1_KEY1,
        P1_KEY2,
        P1_KEY3,
        P1_KEY4,
        P1_KEY5,
        P1_KEY6,
        P1_KEY7,

        P2_UP,
        P2_DOWN,
        P2_LEFT,
        P2_RIGHT,
        P2_KEY0,
        P2_KEY1,
        P2_KEY2,
        P2_KEY3,
        P2_KEY4,
        P2_KEY5,
        P2_KEY6,
        P2_KEY7,
    };

    //ゲームで利用できるデバイスの種類
    public enum DeviceTypeForGame
    {
        KEYBOARD = 0,
        JOYSTICK = 1,
		MOUSE = 2,
        NODEVICE = 3,
    }

	//ジョイスティック入力種類
	public enum JOY_INPUT_TYPE
	{
		AXIS,
		POINT_OF_VIEW,
		BUTTON,
		NO_DATA,
	}

	//レバー定義
	public enum LEVER
	{
		UP,
		DOWN,
		LEFT,
		RIGHT,
	}

}

#if false
namespace SlimDX.DirectInput
{
	//
	// 概要:
	//     Specifies the main type of a DirectInput device.
	public enum DeviceType
	{
		//
		// 概要:
		//     A device that does not fall into any other category.
		Other = 17,
		//
		// 概要:
		//     A mouse or mouse-like device.
		Mouse = 18,
		//
		// 概要:
		//     A keyboard or keyboard-like device.
		Keyboard = 19,
		//
		// 概要:
		//     A generic joystick.
		Joystick = 20,
		//
		// 概要:
		//     A console game pad.
		Gamepad = 21,
		//
		// 概要:
		//     A device for steering.
		Driving = 22,
		//
		// 概要:
		//     Controller for a flight simulation.
		Flight = 23,
		//
		// 概要:
		//     A first-person action game device.
		FirstPerson = 24,
		//
		// 概要:
		//     Input device used to control another type of device from within the context of
		//     the application.
		ControlDevice = 25,
		//
		// 概要:
		//     A screen pointer device.
		ScreenPointer = 26,
		//
		// 概要:
		//     A remote-control device.
		Remote = 27,
		//
		// 概要:
		//     A specialized device with functionality unsuitable for main control of an application,
		//     such as pedals with a wheel.
		Supplemental = 28
	}
}
#endif

#if false

//
// 概要:
//     Specifies the device type of an object data format.
[Flags]
	public enum ObjectDeviceType
	{
		All = 0,
		//
		// 概要:
		//     The object must be a relative axis.
		RelativeAxis = 1,
		//
		// 概要:
		//     The object must be an absolute axis.
		AbsoluteAxis = 2,
		//
		// 概要:
		//     The object must be an absolute or relative axis.
		Axis = 3,
		//
		// 概要:
		//     The object must be a push button.
		PushButton = 4,
		//
		// 概要:
		//     The object must be a toggle button.
		ToggleButton = 8,
		//
		// 概要:
		//     The object must be a toggle or push button.
		Button = 12,
		//
		// 概要:
		//     The object must be a Point-Of-View controller.
		PointOfViewController = 16,
		Collection = 64,
		NoData = 128,
		NoCollection = 16776960,
		//
		// 概要:
		//     The object must contain a force-feedback actuator.
		ForceFeedbackActuator = 16777216,
		//
		// 概要:
		//     The object must be a valid force-feedback effect trigger.
		ForceFeedbackEffectTrigger = 33554432,
		//
		// 概要:
		//     The object must be a type defined by the manufacturer.
		VendorDefined = 67108864,
		Alias = 134217728,
		Output = 268435456
	}
#endif
