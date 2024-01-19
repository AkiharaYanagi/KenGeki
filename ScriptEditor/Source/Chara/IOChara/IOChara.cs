namespace ScriptEditor
{
	//==================================================================================
	//
	//	書出読込共通の要素を定義する
	//
	//==================================================================================
	
	public static class CONST
	{
		public const uint VER = 110;
	}

	//配列添字取得用INDEX
	public static class IOChara 
	{
		public static int Parse ( Element e, int enumName ) 
		{ 
			return int.Parse ( e.Attributes[ enumName ].Value );
		}
	}


	public enum ELEMENT_CHARA
	{
		VER,
		MAIN_IMAGE_LIST,
		EF_IMAGE_LIST,
		ACTION_LIST,
		EF_LIST,
		COMMAND_LIST
	}

	public enum ATTR_ACTION
	{
		ELAC_NAME,
		ELAC_NEXT,
		ELAC_CATEGORY,
		ELAC_POSTURE,
		ELAC_BALANCE,
	}

	public enum ATTRIBUTE_SCRIPT
	{
		GROUP,
//		IMG,
		IMG_NAME,
		X, Y, 
		VX, VY,
		AX, AY,
		CLC_ST, 
		POWER
	}

	public enum ELEMENT_SCRIPT
	{
		ELSC_BRANCH,
		ELSC_EFGNRT,
		ELSC_CRECT,
		ELSC_ARECT,
		ELSC_HRECT,
		ELSC_ORECT,
	}

	public enum ATTRIBUTE_COMMAND
	{
		NAME,
		LIMIT_TIME,
		ID_LVR,
	}

	public enum ELEMENT_BRANCH
	{
		ELBR_COMMAND_INDEX,
		ELBR_COMMAND_NAME,
		ELBR_ACTION_INDEX,
		ELBR_ACTION_NAME,
		ELBR_FRAME,
	}

	public enum ELEMENT_EFGNRT
	{
		ELEG_NAME,
		ELEG_EFID,
		ELEG_PT_X,
		ELEG_PT_Y,
		ELEG_PT_Z,
		ELEG_GNRT,
		ELEG_LOOP,
		ELEG_SYNC,
	}

}
