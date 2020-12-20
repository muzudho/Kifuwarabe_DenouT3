namespace Grayscale.Kifuwarakaku.Entities.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using Nett;

    /// <summary>
    /// * Panic( ) に相当するものは無いので、throw new Exception("") で代替してください。
    /// </summary>
    public static class Logger
    {
        private static readonly Guid unique = Guid.NewGuid();
        public static Guid Unique { get { return unique; } }

        static Logger()
        {
            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));
            var logDirectory = Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("LogDirectory"));

            TraceRecord = LogEntry(logDirectory, toml, "Trace", true, true);
            DebugRecord = LogEntry(logDirectory, toml, "Debug", true, true);
            InfoRecord = LogEntry(logDirectory, toml, "Info", true, true);
            NoticeRecord = LogEntry(logDirectory, toml, "Notice", true, true);
            WarnRecord = LogEntry(logDirectory, toml, "Warn", true, true);
            ErrorRecord = LogEntry(logDirectory, toml, "Error", true, true);
            FatalRecord = LogEntry(logDirectory, toml, "Fatal", true, true);

            /*
            AddLog(LogTags.Default, DefaultByProcess);

            AddLog(LogTags.Error, LogEntry(profilePath, toml, "N01ErrorLog", true, false));

            // 汎用ログ。千日手判定用。
            AddLog(LogTags.DefaultSennitite, LogEntry(profilePath, toml, "N02DefaultSennititeLog", true, false));

            // 擬似将棋サーバーのログ
            AddLog(LogTags.ServerDefault, LogEntry(profilePath, toml, "N03ServerDefaultLog", true, false));

            // 擬似将棋サーバーのログ。ログ。送受信内容の記録専用です。
            AddLog(LogTags.ServerNetworkAsync, LogEntry(profilePath, toml, "N04ServerNetworkAsyncLog", true, true));

            // C# GUIのログ
            AddLog(LogTags.CsharpGuiDefault, LogEntry(profilePath, toml, "N05CsharpGuiDefaultLog", true, false));

            // C# GUIのログ
            AddLog(LogTags.CsharpGuiKifuYomitori, LogEntry(profilePath, toml, "N06CsharpGuiKifuYomitoriLog", true, false));

            // C# GUIのログ
            AddLog(LogTags.CsharpGuiNetwork, LogEntry(profilePath, toml, "N07CsharpGuiNetworkLog", true, true));

            // C# GUIのログ
            AddLog(LogTags.CsharpGuiPaint, LogEntry(profilePath, toml, "N08CsharpGuiPaintLog", true, false));

            // C# GUIのログ
            AddLog(LogTags.CsharpGuiSennitite, LogEntry(profilePath, toml, "N09CsharpGuiSennititeLog", true, false));

            // AIMS GUIに対応する用のログ
            AddLog(LogTags.AimsDefault, LogEntry(profilePath, toml, "N10AimsDefaultLog", true, false));

            // 将棋エンジンのログ。将棋エンジンきふわらべで汎用に使います。
            AddLog(LogTags.EngineDefault, LogEntry(profilePath, toml, "N11EngineDefaultLog", true, false));

            // 将棋エンジンのログ。送受信内容の記録専用です。
            AddLog(LogTags.EngineNetwork, LogEntry(profilePath, toml, "N12EngineNetworkLog", true, true));

            // 将棋エンジンのログ。思考ルーチン専用です。
            AddLog(LogTags.EngineMousouRireki, LogEntry(profilePath, toml, "N13EngineMousouRirekiLog", true, false));

            // 将棋エンジンのログ
            AddLog(LogTags.EngineSennitite, LogEntry(profilePath, toml, "N14EngineSennititeLog", true, false));

            // その他のログ。汎用。テスト・プログラム用。
            AddLog(LogTags.TestProgram, LogEntry(profilePath, toml, "N15TestProgramLog", true, false));

            // その他のログ。棋譜学習ソフト用。
            AddLog(LogTags.Learner, LogEntry(profilePath, toml, "N16LearnerLog", true, false));

            // その他のログ。スピード計測ソフト用。
            AddLog(LogTags.SpeedKeisoku, LogEntry(profilePath, toml, "N17SpeedKeisokuLog", true, false));
            */
        }

        static ILogRecord LogEntry(string logDirectory, TomlTable toml, string resourceKey, bool enabled, bool timeStampPrintable)
        {
            var logFile = LogFile.AsLog(logDirectory, toml.Get<TomlTable>("Logs").Get<string>(resourceKey));
            return new LogRecord(logFile, enabled, timeStampPrintable);
        }

        static readonly ILogRecord TraceRecord;
        static readonly ILogRecord DebugRecord;
        static readonly ILogRecord InfoRecord;
        static readonly ILogRecord NoticeRecord;
        static readonly ILogRecord WarnRecord;
        static readonly ILogRecord ErrorRecord;
        static readonly ILogRecord FatalRecord;

        /// <summary>
        /// テキストをそのまま、ファイルへ出力するためのものです。
        /// </summary>
        /// <param name="path"></param>
        /// <param name="contents"></param>
        public static void WriteFile(ILogFile logFile, string contents)
        {
            File.WriteAllText(logFile.Name, contents);
            // MessageBox.Show("ファイルを出力しました。\n[" + path + "]");
        }

        /// <summary>
        /// トレース・レベル。
        /// </summary>
        /// <param name="line"></param>
        [Conditional("DEBUG")]
        public static void Trace(string line, ILogFile targetOrNull = null)
        {
            Logger.XLine(TraceRecord, "Trace", line, targetOrNull);
        }

        /// <summary>
        /// デバッグ・レベル。
        /// </summary>
        /// <param name="line"></param>
        [Conditional("DEBUG")]
        public static void Debug(string line, ILogFile targetOrNull = null)
        {
            Logger.XLine(DebugRecord, "Debug", line, targetOrNull);
        }

        /// <summary>
        /// インフォ・レベル。
        /// </summary>
        /// <param name="line"></param>
        public static void Info(string line, ILogFile targetOrNull = null)
        {
            Logger.XLine(InfoRecord, "Info", line, targetOrNull);
        }

        /// <summary>
        /// ノティス・レベル。
        /// </summary>
        /// <param name="line"></param>
        public static void Notice(string line, ILogFile targetOrNull = null)
        {
            Logger.XLine(NoticeRecord, "Notice", line, targetOrNull);
        }

        /// <summary>
        /// ワーン・レベル。
        /// </summary>
        /// <param name="line"></param>
        public static void Warn(string line, ILogFile targetOrNull = null)
        {
            Logger.XLine(WarnRecord, "Warn", line, targetOrNull);
        }

        /// <summary>
        /// エラー・レベル。
        /// </summary>
        /// <param name="line"></param>
        public static void Error(string line, ILogFile targetOrNull = null)
        {
            Logger.XLine(ErrorRecord, "Error", line, targetOrNull);
        }

        /// <summary>
        /// ファータル・レベル。
        /// </summary>
        /// <param name="line"></param>
        public static void Fatal(string line, ILogFile targetOrNull = null)
        {
            Logger.XLine(FatalRecord, "Fatal", line, targetOrNull);
        }

        /// <summary>
        /// ログ・ファイルに記録します。失敗しても無視します。
        /// </summary>
        /// <param name="line"></param>
        static void XLine(ILogRecord record, string level, string line, ILogFile targetOrNull)
        {
            // ログ出力オフ
            if (!record.Enabled)
            {
                return;
            }

            // ログ追記
            try
            {
                StringBuilder sb = new StringBuilder();

                // タイムスタンプ
                if (record.TimeStampPrintable)
                {
                    sb.Append($"[{DateTime.Now.ToString()}] ");
                }

                sb.Append($"{level} {line}");
                sb.AppendLine();

                string message = sb.ToString();

                if (targetOrNull != null)
                {
                    System.IO.File.AppendAllText(targetOrNull.Name, message);
                }
                else
                {
                    System.IO.File.AppendAllText(record.LogFile.Name, message);
                }
            }
            catch (Exception)
            {
                //>>>>> エラーが起こりました。

                // どうにもできないので 無視します。
            }
        }

        /// <summary>
        /// サーバーから受け取ったコマンドを、ログ・ファイルに記録します。
        /// Client の C☆（＾～＾）
        /// </summary>
        /// <param name="line"></param>
        public static void WriteLineC(
            string line
            /*
            ,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            */
            )
        {
            // ログ追記
            // ：{memberName}：{sourceFilePath}：{sourceLineNumber}
            File.AppendAllText(NoticeRecord.LogFile.Name, $@"{DateTime.Now.ToString()}  > {line}
");
        }

        /// <summary>
        /// サーバーへ送ったコマンドを、ログ・ファイルに記録します。
        /// </summary>
        /// <param name="line"></param>
        public static void WriteLineS(
            string line
            /*
            ,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            */
            )
        {
            // ログ追記
            // ：{memberName}：{sourceFilePath}：{sourceLineNumber}
            File.AppendAllText(NoticeRecord.LogFile.Name, $@"{DateTime.Now.ToString()}<   {line}
");
        }

        /// <summary>
        /// ログファイルを削除します。(連番がなければ)
        /// 
        /// * 将棋エンジン起動後、ログが少し取られ始めたあとに削除を開始するようなところで実行しないでください。
        /// * TODO usinewgame のタイミングでログを削除したい。
        /// </summary>
        public static void RemoveAllLogFiles()
        {
            try
            {
                //string filepath2 = Path.Combine(Application.StartupPath, this.DefaultFile.FileName);
                //System.IO.File.Delete(filepath2);

                var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
                var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));
                string logsDirectory = Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("LogDirectory"));

                string[] paths = Directory.GetFiles(logsDirectory);
                foreach (string path in paths)
                {
                    string name = Path.GetFileName(path);
                    // if (name.StartsWith("_log_", StringComparison.CurrentCulture))
                    if (name.EndsWith(".log") || name.Contains(".log."))
                    {
                        string fullpath = Path.Combine(logsDirectory, name);
                        //MessageBox.Show("fullpath=[" + fullpath + "]", "ログ・ファイルの削除");
                        System.IO.File.Delete(fullpath);
                    }
                }
            }
            catch (Exception)
            {
                // ログ・ファイルの削除に失敗しても無視します。
            }
        }
    }
}
