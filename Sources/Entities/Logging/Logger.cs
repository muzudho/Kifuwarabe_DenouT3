namespace Grayscale.Kifuwarakaku.Entities.Logging
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using Grayscale.Kifuwarakaku.Entities.Configuration;
    using Nett;

    /// <summary>
    /// * Panic( ) に相当するものは無いので、throw new Exception("") で代替してください。
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// このクラスを使う前にセットしてください。
        /// </summary>
        public static void Init(IEngineConf engineConf)
        {
            EngineConf = engineConf;

            TraceRecord = LogEntry("Trace", true, true);
            DebugRecord = LogEntry("Debug", true, true);
            InfoRecord = LogEntry("Info", true, true);
            NoticeRecord = LogEntry("Notice", true, true);
            WarnRecord = LogEntry("Warn", true, true);
            ErrorRecord = LogEntry("Error", true, true);
            FatalRecord = LogEntry("Fatal", true, true);

            /*
            AddLog(LogTags.Default, DefaultByProcess);

            AddLog(LogTags.Error, LogEntry( "N01ErrorLog", true, false));

            // 汎用ログ。千日手判定用。
            AddLog(LogTags.DefaultSennitite, LogEntry( "N02DefaultSennititeLog", true, false));

            // 擬似将棋サーバーのログ
            AddLog(LogTags.ServerDefault, LogEntry( "N03ServerDefaultLog", true, false));

            // 擬似将棋サーバーのログ。ログ。送受信内容の記録専用です。
            AddLog(LogTags.ServerNetworkAsync, LogEntry( "N04ServerNetworkAsyncLog", true, true));

            // C# GUIのログ
            AddLog(LogTags.CsharpGuiDefault, LogEntry( "N05CsharpGuiDefaultLog", true, false));

            // C# GUIのログ
            AddLog(LogTags.CsharpGuiKifuYomitori, LogEntry( "N06CsharpGuiKifuYomitoriLog", true, false));

            // C# GUIのログ
            AddLog(LogTags.CsharpGuiNetwork, LogEntry( "N07CsharpGuiNetworkLog", true, true));

            // C# GUIのログ
            AddLog(LogTags.CsharpGuiPaint, LogEntry( "N08CsharpGuiPaintLog", true, false));

            // C# GUIのログ
            AddLog(LogTags.CsharpGuiSennitite, LogEntry( "N09CsharpGuiSennititeLog", true, false));

            // AIMS GUIに対応する用のログ
            AddLog(LogTags.AimsDefault, LogEntry( "N10AimsDefaultLog", true, false));

            // 将棋エンジンのログ。将棋エンジンきふわらべで汎用に使います。
            AddLog(LogTags.EngineDefault, LogEntry( "N11EngineDefaultLog", true, false));

            // 将棋エンジンのログ。送受信内容の記録専用です。
            AddLog(LogTags.EngineNetwork, LogEntry( "N12EngineNetworkLog", true, true));

            // 将棋エンジンのログ。思考ルーチン専用です。
            AddLog(LogTags.EngineMousouRireki, LogEntry( "N13EngineMousouRirekiLog", true, false));

            // 将棋エンジンのログ
            AddLog(LogTags.EngineSennitite, LogEntry( "N14EngineSennititeLog", true, false));

            // その他のログ。汎用。テスト・プログラム用。
            AddLog(LogTags.TestProgram, LogEntry( "N15TestProgramLog", true, false));

            // その他のログ。棋譜学習ソフト用。
            AddLog(LogTags.Learner, LogEntry( "N16LearnerLog", true, false));

            // その他のログ。スピード計測ソフト用。
            AddLog(LogTags.SpeedKeisoku, LogEntry( "N17SpeedKeisokuLog", true, false));
            */
        }

        static ILogRecord LogEntry(string key, bool enabled, bool timeStampPrintable)
        {
            var logFile = ResFile.AsLog(EngineConf.LogDirectory, EngineConf.GetLogBasename(key));
            return new LogRecord(logFile, enabled, timeStampPrintable);
        }

        static IEngineConf EngineConf { get; set; }
        public static ILogRecord TraceRecord { get; private set; }
        public static ILogRecord DebugRecord { get; private set; }
        public static ILogRecord InfoRecord { get; private set; }
        public static ILogRecord NoticeRecord { get; private set; }
        public static ILogRecord WarnRecord { get; private set; }
        public static ILogRecord ErrorRecord { get; private set; }
        public static ILogRecord FatalRecord { get; private set; }

        /// <summary>
        /// テキストをそのまま、ファイルへ出力するためのものです。
        /// </summary>
        /// <param name="path"></param>
        /// <param name="contents"></param>
        public static void WriteFile(IResFile logFile, string contents)
        {
            File.WriteAllText(logFile.Name, contents);
        }

        /// <summary>
        /// トレース・レベル。
        /// </summary>
        /// <param name="line"></param>
        [Conditional("DEBUG")]
        public static void Trace(string line, IResFile targetOrNull = null)
        {
            Logger.XLine(TraceRecord, "Trace", line, targetOrNull);
        }

        /// <summary>
        /// デバッグ・レベル。
        /// </summary>
        /// <param name="line"></param>
        [Conditional("DEBUG")]
        public static void Debug(string line, IResFile targetOrNull = null)
        {
            Logger.XLine(DebugRecord, "Debug", line, targetOrNull);
        }

        /// <summary>
        /// インフォ・レベル。
        /// </summary>
        /// <param name="line"></param>
        public static void Info(string line, IResFile targetOrNull = null)
        {
            Logger.XLine(InfoRecord, "Info", line, targetOrNull);
        }

        /// <summary>
        /// ノティス・レベル。
        /// </summary>
        /// <param name="line"></param>
        public static void Notice(string line, IResFile targetOrNull = null)
        {
            Logger.XLine(NoticeRecord, "Notice", line, targetOrNull);
        }

        /// <summary>
        /// ワーン・レベル。
        /// </summary>
        /// <param name="line"></param>
        public static void Warn(string line, IResFile targetOrNull = null)
        {
            Logger.XLine(WarnRecord, "Warn", line, targetOrNull);
        }

        /// <summary>
        /// エラー・レベル。
        /// </summary>
        /// <param name="line"></param>
        public static void Error(string line, IResFile targetOrNull = null)
        {
            Logger.XLine(ErrorRecord, "Error", line, targetOrNull);
        }

        /// <summary>
        /// ファータル・レベル。
        /// </summary>
        /// <param name="line"></param>
        public static void Fatal(string line, IResFile targetOrNull = null)
        {
            Logger.XLine(FatalRecord, "Fatal", line, targetOrNull);
        }

        /// <summary>
        /// ログ・ファイルに記録します。失敗しても無視します。
        /// </summary>
        /// <param name="line"></param>
        static void XLine(ILogRecord record, string level, string line, IResFile targetOrNull)
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
        /// ログ・ディレクトリー直下の ログファイルを削除します。
        /// 
        /// Example:
        /// [GUID]name.log
        /// name.log.png
        /// ...
        ///
        /// * 将棋エンジン起動後、ログが少し取られ始めたあとに削除を開始するようなところで実行しないでください。
        /// * TODO usinewgame のタイミングでログを削除したい。
        /// </summary>
        public static void RemoveAllLogFiles()
        {
            try
            {
                string[] paths = Directory.GetFiles(EngineConf.LogDirectory);
                foreach (string path in paths)
                {
                    string name = Path.GetFileName(path);
                    if (name.EndsWith(".log") || name.Contains(".log."))
                    {
                        System.IO.File.Delete(path);
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
