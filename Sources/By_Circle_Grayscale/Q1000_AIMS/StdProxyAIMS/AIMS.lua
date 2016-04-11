--
-- AIMS アプリケーションコンフィグスクリプト
-- ※実際に制作者が弄る部分はこのスクリプトの最後にまとめてあります!
--

--
-- DirectInputのキーコード定数 BIND_KEYBOARD～～に与える値
--

DIK_ESCAPE		= 1;
DIK_1			= 2;
DIK_2			= 3;
DIK_3			= 4;
DIK_4			= 5;
DIK_5			= 6;
DIK_6			= 7;
DIK_7			= 8;
DIK_8			= 9;
DIK_9			= 10;
DIK_0			= 11;
DIK_MINUS		= 12;
DIK_EQUALS		= 13;
DIK_BACK		= 14;
DIK_TAB			= 15;
DIK_Q			= 16;
DIK_W			= 17;
DIK_E			= 18;
DIK_R			= 19;
DIK_T			= 20;
DIK_Y			= 21;
DIK_U			= 22;
DIK_I			= 23;
DIK_O			= 24;
DIK_P			= 25;
DIK_LBRACKET	= 26;
DIK_RBRACKET	= 27;
DIK_RETURN		= 28;
DIK_LCONTROL	= 29;
DIK_A			= 30;
DIK_S			= 31;
DIK_D			= 32;
DIK_F			= 33;
DIK_G			= 34;
DIK_H			= 35;
DIK_J			= 36;
DIK_K			= 37;
DIK_L			= 38;
DIK_SEMICOLON	= 39;
DIK_APOSTROPHE	= 40;
DIK_GRAVE		= 41;
DIK_LSHIFT		= 42;
DIK_BACKSLASH	= 43;
DIK_Z			= 44;
DIK_X			= 45;
DIK_C			= 46;
DIK_V			= 47;
DIK_B			= 48;
DIK_N			= 49;
DIK_M			= 50;
DIK_COMMA		= 51;
DIK_PERIOD		= 52;
DIK_SLASH		= 53;
DIK_RSHIFT		= 54;
DIK_MULTIPLY	= 55;
DIK_LMENU		= 56;
DIK_SPACE		= 57;
DIK_CAPITAL		= 58;
DIK_F1			= 59;
DIK_F2			= 60;
DIK_F3			= 61;
DIK_F4			= 62;
DIK_F5			= 63;
DIK_F6			= 64;
DIK_F7			= 65;
DIK_F8			= 66;
DIK_F9			= 67;
DIK_F10			= 68;
DIK_NUMLOCK		= 69;
DIK_SCROLL		= 70;
DIK_NUMPAD7		= 71;
DIK_NUMPAD8		= 72;
DIK_NUMPAD9		= 73;
DIK_SUBTRACT	= 74;
DIK_NUMPAD4		= 75;
DIK_NUMPAD5		= 76;
DIK_NUMPAD6		= 77;
DIK_ADD			= 78;
DIK_NUMPAD1		= 79;
DIK_NUMPAD2		= 80;
DIK_NUMPAD3		= 81;
DIK_NUMPAD0		= 82;
DIK_DECIMAL		= 83;
DIK_OEM_102		= 86;
DIK_F11			= 87;
DIK_F12			= 88;
DIK_F13			= 100;
DIK_F14			= 101;
DIK_F15			= 102;
DIK_KANA		= 112;
DIK_ABNT_C1		= 115;
DIK_CONVERT		= 121;
DIK_NOCONVERT	= 123;
DIK_YEN			= 125;
DIK_ABNT_C2		= 126;
DIK_NUMPADEQUALS = 141;
DIK_PREVTRACK	= 144;
DIK_AT			= 145;
DIK_COLON		= 146;
DIK_UNDERLINE	= 147;
DIK_KANJI		= 148;
DIK_STOP		= 149;
DIK_AX			= 150;
DIK_UNLABELED	= 151;
DIK_NEXTTRACK	= 153;
DIK_NUMPADENTER	= 156;
DIK_RCONTROL	= 157;
DIK_MUTE		= 160;
DIK_CALCULATOR	= 161;
DIK_PLAYPAUSE	= 162;
DIK_MEDIASTOP	= 164;
DIK_VOLUMEDOWN	= 174;
DIK_VOLUMEUP	= 176;
DIK_WEBHOME		= 178;
DIK_NUMPADCOMMA	= 179;
DIK_DIVIDE		= 181;
DIK_SYSRQ		= 183;
DIK_RMENU		= 184;
DIK_PAUSE		= 197;
DIK_HOME		= 199;
DIK_UP			= 200;
DIK_PRIOR		= 201;
DIK_LEFT		= 203;
DIK_RIGHT		= 205;
DIK_END			= 207;
DIK_DOWN		= 208;
DIK_NEXT		= 209;
DIK_INSERT		= 210;
DIK_DELETE		= 211;
DIK_LWIN		= 219;
DIK_RWIN		= 220;
DIK_APPS		= 221;
DIK_POWER		= 222;
DIK_SLEEP		= 223;
DIK_WAKE		= 227;
DIK_WEBSEARCH	= 229;
DIK_WEBFAVORITES = 230;
DIK_WEBREFRESH	= 231;
DIK_WEBSTOP		= 232;
DIK_WEBFORWARD	= 233;
DIK_WEBBACK		= 234;
DIK_MYCOMPUTER	= 235;
DIK_MAIL		= 236;
DIK_MEDIASELECT	= 237;

--
-- 別名定義。
--

DIK_BACKSPACE	= DIK_BACK;
DIK_NUMPADSTAR	= DIK_MULTIPLY;
DIK_LALT		= DIK_LMENU;
DIK_CAPSLOCK	= DIK_CAPITAL;
DIK_NUMPADMINUS	= DIK_SUBTRACT;
DIK_NUMPADPLUS	= DIK_ADD;
DIK_NUMPADPERIOD = DIK_DECIMAL;
DIK_NUMPADSLASH	= DIK_DIVIDE;
DIK_RALT		= DIK_RMENU;
DIK_UPARROW		= DIK_UP;
DIK_PGUP		= DIK_PRIOR;
DIK_LEFTARROW	= DIK_LEFT;
DIK_RIGHTARROW	= DIK_RIGHT;
DIK_DOWNARROW	= DIK_DOWN;
DIK_PGDN		= DIK_NEXT;

--
-- アプリケーション定義ここから
--

-- アプリケーション名 タイトルバーにはこれが出る
APPLICATION_NAME = "AIMS Application";

-- AIMS2007/7月版までの挙動にできるだけ合わせるオプション
-- 普通は意識しなくて良いです
COMPATIBILITY_MODE = 0;

-- ESCキーをアプリケーションで使うか
-- 0だとESCキーはAIMSが終了キーに使います。1だとAIMSは何もしません。
USE_ESC_IN_APPLICATION = 0;

-- F1キーの有効・無効
-- createRenderTargetを使ってるアプリだと、
-- 実行中のウィンドウモード切替はトラブルの元になります。
-- 省略するとF1は有効になります。
USE_F1 = 1;

-- セーブファイルのパス
-- パス区切りは円記号２個で指定してください。環境変数が使えます。
-- 空文字列or省略でexeファイルと同じところになります。
-- 指定する場合は、必ず最後に円記号２個をつけること。
-- 例：SAVE_PATH = "%APPDATA%\\D.N.A. Softwares\\AIMS\\";
SAVE_PATH = "";

-- 物理解像度の設定
RESOLUTION_X = 640;
RESOLUTION_Y = 480;

-- 標準の論理解像度の設定 普通は物理解像度と同じでいい

VRESOLUTION_X = 640;
VRESOLUTION_Y = 480;

-- 標準のキーバインド ジョイスティックはゲーム中に動的再定義が可能です。
-- ボタン番号は0からはじまります
-- 使わないトリガ、プレイヤーについては設定せず放置しておいても結構です。

-- マウスポインタをAIMSに占有させる
CLIP_MOUSE_POINTER = 0;

-- プレイヤー人数　有効デバイス数に直結します。
MAXIMUM_PLAYERS = 2;
MAXIMUM_TRIGGERS = 2;
USE_MOUSE = 0;

-- 1p ゲームパッド
BIND_PAD_1P_TRIG1 = 0;
BIND_PAD_1P_TRIG2 = 1;

-- 1p キーボード
BIND_KEY_1P_DIR_U = DIK_R;
BIND_KEY_1P_DIR_D = DIK_F;
BIND_KEY_1P_DIR_L = DIK_D;
BIND_KEY_1P_DIR_R = DIK_G;
BIND_KEY_1P_TRIG1 = DIK_A;
BIND_KEY_1P_TRIG2 = DIK_S;

-- 2p ゲームパッド
BIND_PAD_2P_TRIG1 = 0;
BIND_PAD_2P_TRIG2 = 1;

-- 2p キーボード
BIND_KEY_2P_DIR_U = DIK_UP;
BIND_KEY_2P_DIR_D = DIK_DOWN;
BIND_KEY_2P_DIR_L = DIK_LEFT;
BIND_KEY_2P_DIR_R = DIK_RIGHT;
BIND_KEY_2P_TRIG1 = DIK_NUMPAD0;
BIND_KEY_2P_TRIG2 = DIK_NUMPADPERIOD;
