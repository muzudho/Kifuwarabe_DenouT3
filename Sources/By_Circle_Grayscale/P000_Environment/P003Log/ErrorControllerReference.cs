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
        public static readonly IErrorController DefaultByProcess = new ErrorHandlerImpl(new KwLoggerImpl($"./default({System.Diagnostics.Process.GetCurrentProcess()})", ".log", false, false));
        public static readonly IErrorController Error;

        /// <summary>
        /// 汎用ログ。
        /// 千日手判定用。
        /// </summary>
        public static readonly IErrorController DefaultSennitite;

        /// <summary>
        /// 擬似将棋サーバーのログ
        /// </summary>
        public static readonly IErrorController ServerDefault;

        /// <summary>
        /// 擬似将棋サーバーのログ
        /// ログ。送受信内容の記録専用です。
        /// </summary>
        public static readonly IErrorController ServerNetworkAsync;

        /// <summary>
        /// C# GUIのログ
        /// </summary>
        public static readonly IErrorController CsharpGuiDefault;

        /// <summary>
        /// C# GUIのログ
        /// </summary>
        public static readonly IErrorController CsharpGuiKifuYomitori;

        /// <summary>
        /// C# GUIのログ
        /// </summary>
        public static readonly IErrorController CsharpGuiNetwork;

        /// <summary>
        /// C# GUIのログ
        /// </summary>
        public static readonly IErrorController CsharpGuiPaint;

        /// <summary>
        /// C# GUIのログ
        /// </summary>
        public static readonly IErrorController CsharpGuiSennitite;

        /// <summary>
        /// AIMS GUIに対応する用のログ
        /// </summary>
        public static readonly IErrorController AimsDefault;

        /// <summary>
        /// 将棋エンジンのログ。将棋エンジンきふわらべで汎用に使います。
        /// </summary>
        public static readonly IErrorController EngineDefault;

        /// <summary>
        /// 将棋エンジンのログ。送受信内容の記録専用です。
        /// </summary>
        public static readonly IErrorController EngineNetwork;

        /// <summary>
        /// 将棋エンジンのログ。思考ルーチン専用です。
        /// </summary>
        public static readonly IErrorController EngineMousouRireki;

        /// <summary>
        /// 将棋エンジンのログ
        /// </summary>
        public static readonly IErrorController EngineSennitite;

        /// <summary>
        /// その他のログ。汎用。テスト・プログラム用。
        /// </summary>
        public static readonly IErrorController TestProgram;

        /// <summary>
        /// その他のログ。棋譜学習ソフト用。
        /// </summary>
        public static readonly IErrorController Learner;

        /// <summary>
        /// その他のログ。スピード計測ソフト用。
        /// </summary>
        public static readonly IErrorController SpeedKeisoku;

        public static ErrorControllerReference()
        {
            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));

            ErrorControllerReference.Error = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("N01ErrorLog")), ".txt", true, false));
            ErrorControllerReference.DefaultSennitite = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("N02DefaultSennititeLog")), ".txt", true, false));
            ErrorControllerReference.ServerDefault = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("N03ServerDefaultLog")), ".txt", true, false));
            ErrorControllerReference.ServerNetworkAsync = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("N04ServerNetworkAsyncLog")), ".txt", true, true));
            ErrorControllerReference.CsharpGuiDefault = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("N05CsharpGuiDefaultLog")), ".txt", true, false));
            ErrorControllerReference.CsharpGuiKifuYomitori = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("N06CsharpGuiKifuYomitoriLog")), ".txt", true, false));
            ErrorControllerReference.CsharpGuiNetwork = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("N07CsharpGuiNetworkLog")), ".txt", true, true));
            ErrorControllerReference.CsharpGuiPaint = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("N08CsharpGuiPaintLog")), ".txt", true, false));
            ErrorControllerReference.CsharpGuiSennitite = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("N09CsharpGuiSennititeLog")), ".txt", true, false));
            ErrorControllerReference.AimsDefault = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("N10AimsDefaultLog")), ".txt", true, false));
            ErrorControllerReference.EngineDefault = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("N11EngineDefaultLog")), ".txt", true, false));
            ErrorControllerReference.EngineNetwork = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("N12EngineNetworkLog")), ".txt", true, true));
            ErrorControllerReference.EngineMousouRireki = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("N13EngineMousouRirekiLog")), ".txt", true, false));
            ErrorControllerReference.EngineSennitite = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("N14EngineSennititeLog")), ".txt", true, false));
            ErrorControllerReference.TestProgram = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("N15TestProgramLog")), ".txt", true, false));
            ErrorControllerReference.Learner = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("N16LearnerLog")), ".txt", true, false));
            ErrorControllerReference.SpeedKeisoku = new ErrorHandlerImpl(new KwLoggerImpl(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("N17SpeedKeisokuLog")), ".txt", true, false));
        }

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

                var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
                var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));
                string logsDirectory = Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("LogsDirectory"));

                string[] paths = Directory.GetFiles(logsDirectory);
                foreach(string path in paths)
                {
                    string name = Path.GetFileName(path);
                    if (name.StartsWith("_log_", StringComparison.CurrentCulture))
                    {
                        string fullpath = Path.Combine(logsDirectory, name);
                        //MessageBox.Show("fullpath=[" + fullpath + "]", "ログ・ファイルの削除");
                        System.IO.File.Delete(fullpath);
                    }
                }
            }
            catch (Exception ex) { ErrorControllerReference.Error.Panic(ex, "ﾛｸﾞﾌｧｲﾙ削除中☆"); throw; }
        }
    }
}
