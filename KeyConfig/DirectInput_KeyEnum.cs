//------------------------------------------------------------------------------
//	ゲームメイン(C++ : Direct Input)で用いるキーボード入力定数
//		ConvertSlimToDxにおいてスクリプトエディタ、またはゲーム設定(C# : SlimDx)での値を変換する
//------------------------------------------------------------------------------


namespace ScriptEditor
{
    public enum DxKey
    {
/**/DIK_ESCAPE           = 0x01, 
/**/DIK_1                = 0x02, 
/**/DIK_2                = 0x03, 
/**/DIK_3                = 0x04, 
/**/DIK_4                = 0x05, 
/**/DIK_5                = 0x06, 
/**/DIK_6                = 0x07, 
/**/DIK_7                = 0x08, 
/**/DIK_8                = 0x09, 
/**/DIK_9                = 0x0A, 
/**/DIK_0                = 0x0B, 
/**/DIK_MINUS            = 0x0C    /* - on main keyboard */, 
/**/DIK_EQUALS           = 0x0D, 
/**/DIK_BACK             = 0x0E    /* backspace */, 
/**/DIK_TAB              = 0x0F, 
/**/DIK_Q                = 0x10, 
/**/DIK_W                = 0x11, 
/**/DIK_E                = 0x12, 
/**/DIK_R                = 0x13, 
/**/DIK_T                = 0x14, 
/**/DIK_Y                = 0x15, 
/**/DIK_U                = 0x16, 
/**/DIK_I                = 0x17, 
/**/DIK_O                = 0x18, 
/**/DIK_P                = 0x19, 
/**/DIK_LBRACKET         = 0x1A, 
/**/DIK_RBRACKET         = 0x1B, 
/**/DIK_RETURN           = 0x1C    /* Enter on main keyboard */, 
/**/DIK_LCONTROL         = 0x1D, 
/**/DIK_A                = 0x1E, 
/**/DIK_S                = 0x1F, 
/**/DIK_D                = 0x20, 
/**/DIK_F                = 0x21, 
/**/DIK_G                = 0x22, 
/**/DIK_H                = 0x23, 
/**/DIK_J                = 0x24, 
/**/DIK_K                = 0x25, 
/**/DIK_L                = 0x26, 
/**/DIK_SEMICOLON        = 0x27, 
/**/DIK_APOSTROPHE       = 0x28, 
/**/DIK_GRAVE            = 0x29    /* accent grave */, 
/**/DIK_LSHIFT           = 0x2A, 
/**/DIK_BACKSLASH        = 0x2B, 
/**/DIK_Z                = 0x2C, 
/**/DIK_X                = 0x2D, 
/**/DIK_C                = 0x2E, 
/**/DIK_V                = 0x2F, 
/**/DIK_B                = 0x30, 
/**/DIK_N                = 0x31, 
/**/DIK_M                = 0x32, 
/**/DIK_COMMA            = 0x33, 
/**/DIK_PERIOD           = 0x34    /* . on main keyboard */, 
/**/DIK_SLASH            = 0x35    /* / on main keyboard */, 
/**/DIK_RSHIFT           = 0x36, 
/**/DIK_MULTIPLY         = 0x37    /* * on numeric keypad */, 
/**/DIK_LMENU            = 0x38    /* left Alt */, 
/**/DIK_SPACE            = 0x39, 
/**/DIK_CAPITAL          = 0x3A, 

/**/DIK_F1               = 0x3B, 
/**/DIK_F2               = 0x3C, 
/**/DIK_F3               = 0x3D, 
/**/DIK_F4               = 0x3E, 
/**/DIK_F5               = 0x3F, 
/**/DIK_F6               = 0x40, 
/**/DIK_F7               = 0x41, 
/**/DIK_F8               = 0x42, 
/**/DIK_F9               = 0x43, 
/**/DIK_F10              = 0x44, 

/**/DIK_NUMLOCK          = 0x45, 
/**/DIK_SCROLL           = 0x46    /* Scroll Lock */, 
/**/DIK_NUMPAD7          = 0x47, 
/**/DIK_NUMPAD8          = 0x48, 
/**/DIK_NUMPAD9          = 0x49, 
/**/DIK_SUBTRACT         = 0x4A    /* - on numeric keypad */, 
/**/DIK_NUMPAD4          = 0x4B, 
/**/DIK_NUMPAD5          = 0x4C, 
/**/DIK_NUMPAD6          = 0x4D, 
/**/DIK_ADD              = 0x4E    /* + on numeric keypad */, 
/**/DIK_NUMPAD1          = 0x4F, 
/**/DIK_NUMPAD2          = 0x50, 
/**/DIK_NUMPAD3          = 0x51, 
/**/DIK_NUMPAD0          = 0x52, 
/**/DIK_DECIMAL          = 0x53    /* . on numeric keypad */, 
/**/DIK_OEM_102          = 0x56    /* <> or \| on RT 102-key keyboard (Non-U.S.) */, 

/**/DIK_F11              = 0x57, 
/**/DIK_F12              = 0x58, 
/**/DIK_F13              = 0x64    /*                     (NEC PC98) */, 
/**/DIK_F14              = 0x65    /*                     (NEC PC98) */, 
/**/DIK_F15              = 0x66    /*                     (NEC PC98) */, 

/**/DIK_KANA             = 0x70    /* (Japanese keyboard)            */, 
/**/DIK_ABNT_C1          = 0x73    /* /? on Brazilian keyboard */, 
/**/DIK_CONVERT          = 0x79    /* (Japanese keyboard)            */, 
/**/DIK_NOCONVERT        = 0x7B    /* (Japanese keyboard)            */, 
/**/DIK_YEN              = 0x7D    /* (Japanese keyboard)            */, 
/**/DIK_ABNT_C2          = 0x7E    /* Numpad . on Brazilian keyboard */, 
/**/DIK_NUMPADEQUALS     = 0x8D    /* = on numeric keypad (NEC PC98) */, 
/**/DIK_PREVTRACK        = 0x90    /* Previous Track (DIK_CIRCUMFLEX on Japanese keyboard) */, 
/**/DIK_AT               = 0x91    /*                     (NEC PC98) */, 
/**/DIK_COLON            = 0x92    /*                     (NEC PC98) */, 
/**/DIK_UNDERLINE        = 0x93    /*                     (NEC PC98) */, 
/**/DIK_KANJI            = 0x94    /* (Japanese keyboard)            */, 
/**/DIK_STOP             = 0x95    /*                     (NEC PC98) */, 
/**/DIK_AX               = 0x96    /*                     (Japan AX) */, 
/**/DIK_UNLABELED        = 0x97    /*                        (J3100) */, 
/**/DIK_NEXTTRACK        = 0x99    /* Next Track */, 
/**/DIK_NUMPADENTER      = 0x9C    /* Enter on numeric keypad */, 
/**/DIK_RCONTROL         = 0x9D, 
/**/DIK_MUTE             = 0xA0    /* Mute */, 
/**/DIK_CALCULATOR       = 0xA1    /* Calculator */, 
/**/DIK_PLAYPAUSE        = 0xA2    /* Play / Pause */, 
/**/DIK_MEDIASTOP        = 0xA4    /* Media Stop */, 
/**/DIK_VOLUMEDOWN       = 0xAE    /* Volume - */, 
/**/DIK_VOLUMEUP         = 0xB0    /* Volume + */, 
/**/DIK_WEBHOME          = 0xB2    /* Web home */, 
/**/DIK_NUMPADCOMMA      = 0xB3    /* , on numeric keypad (NEC PC98) */, 
/**/DIK_DIVIDE           = 0xB5    /* / on numeric keypad */, 
DIK_SYSRQ            = 0xB7, 
/**/DIK_RMENU            = 0xB8    /* right Alt */, 

/**/DIK_PAUSE            = 0xC5    /* Pause */, 
/**/DIK_HOME             = 0xC7    /* Home on arrow keypad */, 
/**/DIK_UP               = 0xC8    /* UpArrow on arrow keypad */, 
/**/DIK_PRIOR            = 0xC9    /* PgUp on arrow keypad */, 
/**/DIK_LEFT             = 0xCB    /* LeftArrow on arrow keypad */, 
/**/DIK_RIGHT            = 0xCD    /* RightArrow on arrow keypad */, 
/**/DIK_END              = 0xCF    /* End on arrow keypad */, 
/**/DIK_DOWN             = 0xD0    /* DownArrow on arrow keypad */, 
/**/DIK_NEXT             = 0xD1    /* PgDn on arrow keypad */, 
/**/DIK_INSERT           = 0xD2    /* Insert on arrow keypad */, 
/**/DIK_DELETE           = 0xD3    /* Delete on arrow keypad */, 

/**/DIK_LWIN             = 0xDB    /* Left Windows key */, 
/**/DIK_RWIN             = 0xDC    /* Right Windows key */, 
/**/DIK_APPS             = 0xDD    /* AppMenu key */, 
/**/DIK_POWER            = 0xDE    /* System Power */, 
/**/DIK_SLEEP            = 0xDF    /* System Sleep */, 
/**/DIK_WAKE             = 0xE3    /* System Wake */, 
/**/DIK_WEBSEARCH        = 0xE5    /* Web Search */, 
/**/DIK_WEBFAVORITES     = 0xE6    /* Web Favorites */, 
/**/DIK_WEBREFRESH       = 0xE7    /* Web Refresh */, 
/**/DIK_WEBSTOP          = 0xE8    /* Web Stop */, 
/**/DIK_WEBFORWARD       = 0xE9    /* Web Forward */, 
/**/DIK_WEBBACK          = 0xEA    /* Web Back */, 
/**/DIK_MYCOMPUTER       = 0xEB    /* My Computer */, 
/**/DIK_MAIL             = 0xEC    /* Mail */, 
/**/DIK_MEDIASELECT      = 0xED    /* Media Select */, 
    /*
     *  Alternate names for keys, to facilitate transition from DOS.
     */
/**/DIK_BACKSPACE      = DIK_BACK        ,    /* backspace */
/**/DIK_NUMPADSTAR     = DIK_MULTIPLY    ,    /* * on numeric keypad */
/**/DIK_LALT           = DIK_LMENU       ,    /* left Alt */
/**/DIK_CAPSLOCK       = DIK_CAPITAL     ,    /* CapsLock */
/**/DIK_NUMPADMINUS    = DIK_SUBTRACT    ,    /* - on numeric keypad */
/**/DIK_NUMPADPLUS     = DIK_ADD         ,    /* + on numeric keypad */
/**/DIK_NUMPADPERIOD   = DIK_DECIMAL     ,    /* . on numeric keypad */
/**/DIK_NUMPADSLASH    = DIK_DIVIDE      ,    /* / on numeric keypad */
/**/DIK_RALT           = DIK_RMENU       ,    /* right Alt */
/**/DIK_UPARROW        = DIK_UP          ,    /* UpArrow on arrow keypad */
/**/DIK_PGUP           = DIK_PRIOR       ,    /* PgUp on arrow keypad */
/**/DIK_LEFTARROW      = DIK_LEFT        ,    /* LeftArrow on arrow keypad */
/**/DIK_RIGHTARROW     = DIK_RIGHT       ,    /* RightArrow on arrow keypad */
/**/DIK_DOWNARROW      = DIK_DOWN        ,    /* DownArrow on arrow keypad */
/**/DIK_PGDN           = DIK_NEXT        ,    /* PgDn on arrow keypad */

    /*
     *  Alternate names for keys originally not used on US keyboards.
     */
DIK_CIRCUMFLEX      = DIK_PREVTRACK ,       /* Japanese keyboard */
	}
}


//----------------------------------------------------------------------------
// 参考：SlimDxでのキーボード定数定義
//----------------------------------------------------------------------------
#if false
    public enum Key
    {
        D0 = 0,
        D1 = 1,
        D2 = 2,
        D3 = 3,
        D4 = 4,
        D5 = 5,
        D6 = 6,
        D7 = 7,
        D8 = 8,
        D9 = 9,
        A = 10,
        B = 11,
        C = 12,
        D = 13,
        E = 14,
        F = 15,
        G = 16,
        H = 17,
        I = 18,
        J = 19,
        K = 20,
        L = 21,
        M = 22,
        N = 23,
        O = 24,
        P = 25,
        Q = 26,
        R = 27,
        S = 28,
        T = 29,
        U = 30,
        V = 31,
        W = 32,
        X = 33,
        Y = 34,
        Z = 35,
        AbntC1 = 36,
        AbntC2 = 37,
        Apostrophe = 38,
        Applications = 39,
        AT = 40,
        AX = 41,
        Backspace = 42,
        Backslash = 43,
        Calculator = 44,
        CapsLock = 45,
        Colon = 46,
        Comma = 47,
        Convert = 48,
        Delete = 49,
        DownArrow = 50,
        End = 51,
        Equals = 52,
        Escape = 53,
        F1 = 54,
        F2 = 55,
        F3 = 56,
        F4 = 57,
        F5 = 58,
        F6 = 59,
        F7 = 60,
        F8 = 61,
        F9 = 62,
        F10 = 63,
        F11 = 64,
        F12 = 65,
        F13 = 66,
        F14 = 67,
        F15 = 68,
        Grave = 69,
        Home = 70,
        Insert = 71,
        Kana = 72,
        Kanji = 73,
        LeftBracket = 74,
        LeftControl = 75,
        LeftArrow = 76,
        LeftAlt = 77,
        LeftShift = 78,
        LeftWindowsKey = 79,
        Mail = 80,
        MediaSelect = 81,
        MediaStop = 82,
        Minus = 83,
        Mute = 84,
        MyComputer = 85,
        NextTrack = 86,
        NoConvert = 87,
        NumberLock = 88,
        NumberPad0 = 89,
        NumberPad1 = 90,
        NumberPad2 = 91,
        NumberPad3 = 92,
        NumberPad4 = 93,
        NumberPad5 = 94,
        NumberPad6 = 95,
        NumberPad7 = 96,
        NumberPad8 = 97,
        NumberPad9 = 98,
        NumberPadComma = 99,
        NumberPadEnter = 100,
        NumberPadEquals = 101,
        NumberPadMinus = 102,
        NumberPadPeriod = 103,
        NumberPadPlus = 104,
        NumberPadSlash = 105,
        NumberPadStar = 106,
        Oem102 = 107,
        PageDown = 108,
        PageUp = 109,
        Pause = 110,
        Period = 111,
        PlayPause = 112,
        Power = 113,
        PreviousTrack = 114,
        RightBracket = 115,
        RightControl = 116,
        Return = 117,
        RightArrow = 118,
        RightAlt = 119,
        RightShift = 120,
        RightWindowsKey = 121,
        ScrollLock = 122,
        Semicolon = 123,
        Slash = 124,
        Sleep = 125,
        Space = 126,
        Stop = 127,
        PrintScreen = 128,
        Tab = 129,
        Underline = 130,
        Unlabeled = 131,
        UpArrow = 132,
        VolumeDown = 133,
        VolumeUp = 134,
        Wake = 135,
        WebBack = 136,
        WebFavorites = 137,
        WebForward = 138,
        WebHome = 139,
        WebRefresh = 140,
        WebSearch = 141,
        WebStop = 142,
        Yen = 143,
        Unknown = 144
    }
#endif


//----------------------------------------------------------------------------
// 参考：DirectInputでのキーボード定数定義
//----------------------------------------------------------------------------
// 1行コメント扱いを避けるため#を＃に置換している
//----------------------------------------------------------------------------
#if false
/****************************************************************************
*
*      DirectInput keyboard scan codes
*
****************************************************************************/
＃define DIK_ESCAPE          0x01
＃define DIK_1               0x02
＃define DIK_2               0x03
＃define DIK_3               0x04
＃define DIK_4               0x05
＃define DIK_5               0x06
＃define DIK_6               0x07
＃define DIK_7               0x08
＃define DIK_8               0x09
＃define DIK_9               0x0A
＃define DIK_0               0x0B
＃define DIK_MINUS           0x0C    /* - on main keyboard */
＃define DIK_EQUALS          0x0D
＃define DIK_BACK            0x0E    /* backspace */
＃define DIK_TAB             0x0F
＃define DIK_Q               0x10
＃define DIK_W               0x11
＃define DIK_E               0x12
＃define DIK_R               0x13
＃define DIK_T               0x14
＃define DIK_Y               0x15
＃define DIK_U               0x16
＃define DIK_I               0x17
＃define DIK_O               0x18
＃define DIK_P               0x19
＃define DIK_LBRACKET        0x1A
＃define DIK_RBRACKET        0x1B
＃define DIK_RETURN          0x1C    /* Enter on main keyboard */
＃define DIK_LCONTROL        0x1D
＃define DIK_A               0x1E
＃define DIK_S               0x1F
＃define DIK_D               0x20
＃define DIK_F               0x21
＃define DIK_G               0x22
＃define DIK_H               0x23
＃define DIK_J               0x24
＃define DIK_K               0x25
＃define DIK_L               0x26
＃define DIK_SEMICOLON       0x27
＃define DIK_APOSTROPHE      0x28
＃define DIK_GRAVE           0x29    /* accent grave */
＃define DIK_LSHIFT          0x2A
＃define DIK_BACKSLASH       0x2B
＃define DIK_Z               0x2C
＃define DIK_X               0x2D
＃define DIK_C               0x2E
＃define DIK_V               0x2F
＃define DIK_B               0x30
＃define DIK_N               0x31
＃define DIK_M               0x32
＃define DIK_COMMA           0x33
＃define DIK_PERIOD          0x34    /* . on main keyboard */
＃define DIK_SLASH           0x35    /* / on main keyboard */
＃define DIK_RSHIFT          0x36
＃define DIK_MULTIPLY        0x37    /* * on numeric keypad */
＃define DIK_LMENU           0x38    /* left Alt */
＃define DIK_SPACE           0x39
＃define DIK_CAPITAL         0x3A
＃define DIK_F1              0x3B
＃define DIK_F2              0x3C
＃define DIK_F3              0x3D
＃define DIK_F4              0x3E
＃define DIK_F5              0x3F
＃define DIK_F6              0x40
＃define DIK_F7              0x41
＃define DIK_F8              0x42
＃define DIK_F9              0x43
＃define DIK_F10             0x44
＃define DIK_NUMLOCK         0x45
＃define DIK_SCROLL          0x46    /* Scroll Lock */
＃define DIK_NUMPAD7         0x47
＃define DIK_NUMPAD8         0x48
＃define DIK_NUMPAD9         0x49
＃define DIK_SUBTRACT        0x4A    /* - on numeric keypad */
＃define DIK_NUMPAD4         0x4B
＃define DIK_NUMPAD5         0x4C
＃define DIK_NUMPAD6         0x4D
＃define DIK_ADD             0x4E    /* + on numeric keypad */
＃define DIK_NUMPAD1         0x4F
＃define DIK_NUMPAD2         0x50
＃define DIK_NUMPAD3         0x51
＃define DIK_NUMPAD0         0x52
＃define DIK_DECIMAL         0x53    /* . on numeric keypad */
＃define DIK_OEM_102         0x56    /* <> or \| on RT 102-key keyboard (Non-U.S.) */
＃define DIK_F11             0x57
＃define DIK_F12             0x58
＃define DIK_F13             0x64    /*                     (NEC PC98) */
＃define DIK_F14             0x65    /*                     (NEC PC98) */
＃define DIK_F15             0x66    /*                     (NEC PC98) */
＃define DIK_KANA            0x70    /* (Japanese keyboard)            */
＃define DIK_ABNT_C1         0x73    /* /? on Brazilian keyboard */
＃define DIK_CONVERT         0x79    /* (Japanese keyboard)            */
＃define DIK_NOCONVERT       0x7B    /* (Japanese keyboard)            */
＃define DIK_YEN             0x7D    /* (Japanese keyboard)            */
＃define DIK_ABNT_C2         0x7E    /* Numpad . on Brazilian keyboard */
＃define DIK_NUMPADEQUALS    0x8D    /* = on numeric keypad (NEC PC98) */
＃define DIK_PREVTRACK       0x90    /* Previous Track (DIK_CIRCUMFLEX on Japanese keyboard) */
＃define DIK_AT              0x91    /*                     (NEC PC98) */
＃define DIK_COLON           0x92    /*                     (NEC PC98) */
＃define DIK_UNDERLINE       0x93    /*                     (NEC PC98) */
＃define DIK_KANJI           0x94    /* (Japanese keyboard)            */
＃define DIK_STOP            0x95    /*                     (NEC PC98) */
＃define DIK_AX              0x96    /*                     (Japan AX) */
＃define DIK_UNLABELED       0x97    /*                        (J3100) */
＃define DIK_NEXTTRACK       0x99    /* Next Track */
＃define DIK_NUMPADENTER     0x9C    /* Enter on numeric keypad */
＃define DIK_RCONTROL        0x9D
＃define DIK_MUTE            0xA0    /* Mute */
＃define DIK_CALCULATOR      0xA1    /* Calculator */
＃define DIK_PLAYPAUSE       0xA2    /* Play / Pause */
＃define DIK_MEDIASTOP       0xA4    /* Media Stop */
＃define DIK_VOLUMEDOWN      0xAE    /* Volume - */
＃define DIK_VOLUMEUP        0xB0    /* Volume + */
＃define DIK_WEBHOME         0xB2    /* Web home */
＃define DIK_NUMPADCOMMA     0xB3    /* , on numeric keypad (NEC PC98) */
＃define DIK_DIVIDE          0xB5    /* / on numeric keypad */
＃define DIK_SYSRQ           0xB7
＃define DIK_RMENU           0xB8    /* right Alt */
＃define DIK_PAUSE           0xC5    /* Pause */
＃define DIK_HOME            0xC7    /* Home on arrow keypad */
＃define DIK_UP              0xC8    /* UpArrow on arrow keypad */
＃define DIK_PRIOR           0xC9    /* PgUp on arrow keypad */
＃define DIK_LEFT            0xCB    /* LeftArrow on arrow keypad */
＃define DIK_RIGHT           0xCD    /* RightArrow on arrow keypad */
＃define DIK_END             0xCF    /* End on arrow keypad */
＃define DIK_DOWN            0xD0    /* DownArrow on arrow keypad */
＃define DIK_NEXT            0xD1    /* PgDn on arrow keypad */
＃define DIK_INSERT          0xD2    /* Insert on arrow keypad */
＃define DIK_DELETE          0xD3    /* Delete on arrow keypad */
＃define DIK_LWIN            0xDB    /* Left Windows key */
＃define DIK_RWIN            0xDC    /* Right Windows key */
＃define DIK_APPS            0xDD    /* AppMenu key */
＃define DIK_POWER           0xDE    /* System Power */
＃define DIK_SLEEP           0xDF    /* System Sleep */
＃define DIK_WAKE            0xE3    /* System Wake */
＃define DIK_WEBSEARCH       0xE5    /* Web Search */
＃define DIK_WEBFAVORITES    0xE6    /* Web Favorites */
＃define DIK_WEBREFRESH      0xE7    /* Web Refresh */
＃define DIK_WEBSTOP         0xE8    /* Web Stop */
＃define DIK_WEBFORWARD      0xE9    /* Web Forward */
＃define DIK_WEBBACK         0xEA    /* Web Back */
＃define DIK_MYCOMPUTER      0xEB    /* My Computer */
＃define DIK_MAIL            0xEC    /* Mail */
＃define DIK_MEDIASELECT     0xED    /* Media Select */

    /*
     *  Alternate names for keys, to facilitate transition from DOS.
     */
＃define DIK_BACKSPACE       DIK_BACK            /* backspace */
＃define DIK_NUMPADSTAR      DIK_MULTIPLY        /* * on numeric keypad */
＃define DIK_LALT            DIK_LMENU           /* left Alt */
＃define DIK_CAPSLOCK        DIK_CAPITAL         /* CapsLock */
＃define DIK_NUMPADMINUS     DIK_SUBTRACT        /* - on numeric keypad */
＃define DIK_NUMPADPLUS      DIK_ADD             /* + on numeric keypad */
＃define DIK_NUMPADPERIOD    DIK_DECIMAL         /* . on numeric keypad */
＃define DIK_NUMPADSLASH     DIK_DIVIDE          /* / on numeric keypad */
＃define DIK_RALT            DIK_RMENU           /* right Alt */
＃define DIK_UPARROW         DIK_UP              /* UpArrow on arrow keypad */
＃define DIK_PGUP            DIK_PRIOR           /* PgUp on arrow keypad */
＃define DIK_LEFTARROW       DIK_LEFT            /* LeftArrow on arrow keypad */
＃define DIK_RIGHTARROW      DIK_RIGHT           /* RightArrow on arrow keypad */
＃define DIK_DOWNARROW       DIK_DOWN            /* DownArrow on arrow keypad */
＃define DIK_PGDN            DIK_NEXT            /* PgDn on arrow keypad */

    /*
     *  Alternate names for keys originally not used on US keyboards.
     */
＃define DIK_CIRCUMFLEX      DIK_PREVTRACK       /* Japanese keyboard */


#endif


