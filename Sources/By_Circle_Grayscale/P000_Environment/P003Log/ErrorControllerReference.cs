using Grayscale.P003Log.I500Struct;
using System;
using System.IO;
using System.Windows.Forms;
using Grayscale.P003Log.PartOfErrorControllerReference;


namespace Grayscale.P003Log.L500Struct
{


    /// <summary>
    /// オワタ大臣
    /// 
    /// エラー・ハンドラーを集中管理します。
    /// 
    /// </summary>
    public static class ErrorControllerReference // partial /// partial … ロガー定数を拡張できるクラスとして開放。
    {
        /// <summary>
        /// ログを入れておくフォルダーのホームは、固定しておきましょう。
        /// </summary>
        public const string LogsFolder = "../../Logs/";


        public static readonly IErrorController DefaultByProcess = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(ErrorControllerReference.LogsFolder, "_log_default_false_(" + System.Diagnostics.Process.GetCurrentProcess() + ")"), ".txt", false, false));

        public static readonly IErrorController Error = new ErrorHandlerImpl( new KwLoggerImpl( Path.Combine(ErrorControllerReference.LogsFolder, "_log_エラー"), ".txt", true, false));

        /// <summary>
        /// 汎用ログ。
        /// 千日手判定用。
        /// </summary>
        public static readonly IErrorController DefaultSennitite = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(ErrorControllerReference.LogsFolder,"_log_Default_千日手判定"), ".txt", true, false));

        /// <summary>
        /// 擬似将棋サーバーのログ
        /// </summary>
        public static readonly IErrorController ServerDefault = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(ErrorControllerReference.LogsFolder,"_log_ｻｰﾊﾞｰ_ﾃﾞﾌｫﾙﾄ"), ".txt", true, false));
        /// <summary>
        /// 擬似将棋サーバーのログ
        /// ログ。送受信内容の記録専用です。
        /// </summary>
        public static readonly IErrorController ServerNetworkAsync = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(ErrorControllerReference.LogsFolder,"_log_ｻｰﾊﾞｰ_非同期通信"), ".txt", true, true));

        /// <summary>
        /// C# GUIのログ
        /// </summary>
        public static readonly IErrorController CsharpGuiDefault = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(ErrorControllerReference.LogsFolder,"_log_CsharpGUI_ﾃﾞﾌｫﾙﾄ"), ".txt", true, false));
        /// <summary>
        /// C# GUIのログ
        /// </summary>
        public static readonly IErrorController CsharpGuiKifuYomitori = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(ErrorControllerReference.LogsFolder,"_log_CsharpGUI_棋譜読取"), ".txt", true, false));
        /// <summary>
        /// C# GUIのログ
        /// </summary>
        public static readonly IErrorController CsharpGuiNetwork = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(ErrorControllerReference.LogsFolder,"_log_CsharpGUI_通信"), ".txt", true, true));
        /// <summary>
        /// C# GUIのログ
        /// </summary>
        public static readonly IErrorController CsharpGuiPaint = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(ErrorControllerReference.LogsFolder,"_log_CsharpGUI_ﾍﾟｲﾝﾄ"), ".txt", true, false));
        /// <summary>
        /// C# GUIのログ
        /// </summary>
        public static readonly IErrorController CsharpGuiSennitite = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(ErrorControllerReference.LogsFolder,"_log_CsharpGui_千日手判定"), ".txt", true, false));

        #region AIMS GUIに対応する用のログ
        public static readonly IErrorController AimsDefault = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(ErrorControllerReference.LogsFolder,"_log_AIMS対応用_ﾃﾞﾌｫﾙﾄ"), ".txt", true, false));
        #endregion

        /// <summary>
        /// 将棋エンジンのログ。将棋エンジンきふわらべで汎用に使います。
        /// </summary>
        public static readonly IErrorController EngineDefault = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(ErrorControllerReference.LogsFolder,"_log_ｴﾝｼﾞﾝ_ﾃﾞﾌｫﾙﾄ"), ".txt", true, false));

        /// <summary>
        /// 将棋エンジンのログ。送受信内容の記録専用です。
        /// </summary>
        public static readonly IErrorController EngineNetwork = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(ErrorControllerReference.LogsFolder,"_log_ｴﾝｼﾞﾝ_通信"), ".txt", true, true));
        /// <summary>
        /// 将棋エンジンのログ。思考ルーチン専用です。
        /// </summary>
        public static readonly IErrorController EngineMousouRireki = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(ErrorControllerReference.LogsFolder,"_log_ｴﾝｼﾞﾝ_妄想履歴"), ".txt", true, false));
        /// <summary>
        /// 将棋エンジンのログ
        /// </summary>
        public static readonly IErrorController EngineSennitite = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(ErrorControllerReference.LogsFolder,"_log_ｴﾝｼﾞﾝ_千日手判定"), ".txt", true, false));

        /// <summary>
        /// その他のログ。汎用。テスト・プログラム用。
        /// </summary>
        public static readonly IErrorController TestProgram = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(ErrorControllerReference.LogsFolder,"_log_テスト・プログラム用（汎用）"), ".txt", true, false));

        /// <summary>
        /// その他のログ。棋譜学習ソフト用。
        /// </summary>
        public static readonly IErrorController Learner = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(ErrorControllerReference.LogsFolder,"_log_棋譜学習ソフト用"), ".txt", true, false));

        /// <summary>
        /// その他のログ。スピード計測ソフト用。
        /// </summary>
        public static readonly IErrorController SpeedKeisoku = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(ErrorControllerReference.LogsFolder,"_log_スピード計測ソフト用"), ".txt", true, false));

        /// <summary>
        /// ログファイルを削除します。(連番がなければ)
        /// 
        /// FIXME: アプリ起動後、ログが少し取られ始めたあとに削除が開始されることがあります。
        /// FIXME: 将棋エンジン起動時に、またログが削除されることがあります。
        /// </summary>
        public static void RemoveAllLogFiles()
        {
            try
            {
                //string filepath2 = Path.Combine(Application.StartupPath, this.DefaultFile.FileName);
                //System.IO.File.Delete(filepath2);

                string[] paths = Directory.GetFiles(Path.Combine(Application.StartupPath, ErrorControllerReference.LogsFolder));
                foreach(string path in paths)
                {
                    string name = Path.GetFileName(path);
                    if (name.StartsWith("_log_", StringComparison.CurrentCulture))
                    {
                        string fullpath = Path.Combine(Application.StartupPath, ErrorControllerReference.LogsFolder, name);
                        //MessageBox.Show("fullpath=[" + fullpath + "]", "ログ・ファイルの削除");
                        System.IO.File.Delete(fullpath);
                    }
                }
            }
            catch (Exception ex) { ErrorControllerReference.Error.Panic(ex, "ﾛｸﾞﾌｧｲﾙ削除中☆"); throw ex; }
        }
    }
}
