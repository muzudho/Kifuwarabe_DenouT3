using Grayscale.P003Log.I500Struct;
using System;
using System.IO;
using System.Windows.Forms;

namespace Grayscale.P003Log.L500Struct
{


    /// <summary>
    /// ************************************************************************************************************************
    /// オワタ大臣
    /// ************************************************************************************************************************
    /// 
    /// エラー・ハンドラーを集中管理します。
    /// 
    /// </summary>
    public class Util_OwataMinister // partial /// partial … ロガー定数を拡張できるクラスとして開放。
    {
        /// <summary>
        /// ログを入れておくフォルダーのホームは、固定しておきましょう。
        /// </summary>
        public const string LOGS_FOLDER = "../../Logs/";


        public static readonly KwErrorHandler DEFAULT_BY_PROCESS = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(Util_OwataMinister.LOGS_FOLDER, "_log_default_false_(" + System.Diagnostics.Process.GetCurrentProcess() + ")"), ".txt", false, false));

        public static readonly KwErrorHandler ERROR = new ErrorHandlerImpl( new KwLoggerImpl( Path.Combine(Util_OwataMinister.LOGS_FOLDER, "_log_エラー"), ".txt", true, false));




        #region 汎用ログ
        /// <summary>
        /// 千日手判定用。
        /// </summary>
        public static readonly KwErrorHandler DEFAULT_SENNITITE = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(Util_OwataMinister.LOGS_FOLDER,"_log_Default_千日手判定"), ".txt", true, false));
        #endregion



        #region 擬似将棋サーバーのログ
        public static readonly KwErrorHandler SERVER_DEFAULT = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(Util_OwataMinister.LOGS_FOLDER,"_log_ｻｰﾊﾞｰ_ﾃﾞﾌｫﾙﾄ"), ".txt", true, false));
        //public static readonly KwErrorHandler SERVER_KIFU_YOMITORI = new ErrorHandlerImpl(new KwLoggerImpl("../../Logs/_log_ｻｰﾊﾞｰ_棋譜読取", ".txt", true, false));
        /// <summary>
        /// ログ。送受信内容の記録専用です。
        /// </summary>
        public static readonly KwErrorHandler SERVER_NETWORK_ASYNC = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(Util_OwataMinister.LOGS_FOLDER,"_log_ｻｰﾊﾞｰ_非同期通信"), ".txt", true, true));
        #endregion


        #region C# GUIのログ
        public static readonly KwErrorHandler CsharpGui_DEFAULT = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(Util_OwataMinister.LOGS_FOLDER,"_log_CsharpGUI_ﾃﾞﾌｫﾙﾄ"), ".txt", true, false));
        public static readonly KwErrorHandler CsharpGui_KIFU_YOMITORI = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(Util_OwataMinister.LOGS_FOLDER,"_log_CsharpGUI_棋譜読取"), ".txt", true, false));
        public static readonly KwErrorHandler CsharpGui_NETWORK = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(Util_OwataMinister.LOGS_FOLDER,"_log_CsharpGUI_通信"), ".txt", true, true));
        public static readonly KwErrorHandler CsharpGui_PAINT = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(Util_OwataMinister.LOGS_FOLDER,"_log_CsharpGUI_ﾍﾟｲﾝﾄ"), ".txt", true, false));
        public static readonly KwErrorHandler CsharpGui_SENNITITE = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(Util_OwataMinister.LOGS_FOLDER,"_log_CsharpGui_千日手判定"), ".txt", true, false));
        #endregion

        #region AIMS GUIに対応する用のログ
        public static readonly KwErrorHandler AIMS_DEFAULT = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(Util_OwataMinister.LOGS_FOLDER,"_log_AIMS対応用_ﾃﾞﾌｫﾙﾄ"), ".txt", true, false));
        #endregion


        #region 将棋エンジンのログ
        /// <summary>
        /// ログ。将棋エンジンきふわらべで汎用に使います。
        /// </summary>
        public static readonly KwErrorHandler ENGINE_DEFAULT = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(Util_OwataMinister.LOGS_FOLDER,"_log_ｴﾝｼﾞﾝ_ﾃﾞﾌｫﾙﾄ"), ".txt", true, false));

        /// <summary>
        /// ログ。送受信内容の記録専用です。
        /// </summary>
        public static readonly KwErrorHandler ENGINE_NETWORK = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(Util_OwataMinister.LOGS_FOLDER,"_log_ｴﾝｼﾞﾝ_通信"), ".txt", true, true));
        /// <summary>
        /// ログ。思考ルーチン専用です。
        /// </summary>
        public static readonly KwErrorHandler ENGINE_MOUSOU_RIREKI = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(Util_OwataMinister.LOGS_FOLDER,"_log_ｴﾝｼﾞﾝ_妄想履歴"), ".txt", true, false));
        public static readonly KwErrorHandler ENGINE_SENNITITE = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(Util_OwataMinister.LOGS_FOLDER,"_log_ｴﾝｼﾞﾝ_千日手判定"), ".txt", true, false));
        #endregion


        #region その他のログ

        /// <summary>
        /// 汎用。テスト・プログラム用。
        /// </summary>
        public static readonly KwErrorHandler TEST_PROGRAM = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(Util_OwataMinister.LOGS_FOLDER,"_log_テスト・プログラム用（汎用）"), ".txt", true, false));

        /// <summary>
        /// 棋譜学習ソフト用。
        /// </summary>
        public static readonly KwErrorHandler LEARNER = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(Util_OwataMinister.LOGS_FOLDER,"_log_棋譜学習ソフト用"), ".txt", true, false));

        /// <summary>
        /// スピード計測ソフト用。
        /// </summary>
        public static readonly KwErrorHandler SPEED_KEISOKU = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(Util_OwataMinister.LOGS_FOLDER,"_log_スピード計測ソフト用"), ".txt", true, false));
        #endregion



        /// <summary>
        /// ************************************************************************************************************************
        /// ログファイルを削除します。(連番がなければ)
        /// ************************************************************************************************************************
        /// 
        /// FIXME: アプリ起動後、ログが少し取られ始めたあとに削除が開始されることがあります。
        /// FIXME: 将棋エンジン起動時に、またログが削除されることがあります。
        /// </summary>
        public static void Remove_AllLogFiles()
        {
            try
            {
                //string filepath2 = Path.Combine(Application.StartupPath, this.DefaultFile.FileName);
                //System.IO.File.Delete(filepath2);

                string[] paths = Directory.GetFiles(Path.Combine(Application.StartupPath, Util_OwataMinister.LOGS_FOLDER));
                foreach(string path in paths)
                {
                    string name = Path.GetFileName(path);
                    if (name.StartsWith("_log_"))
                    {
                        string fullpath = Path.Combine(Application.StartupPath, Util_OwataMinister.LOGS_FOLDER, name);
                        //MessageBox.Show("fullpath=[" + fullpath + "]", "ログ・ファイルの削除");
                        System.IO.File.Delete(fullpath);
                    }
                }
            }
            catch (Exception ex) { Util_OwataMinister.ERROR.DonimoNaranAkirameta(ex, "ﾛｸﾞﾌｧｲﾙ削除中☆"); throw ex; }
        }


    }
}
