namespace Grayscale.Kifuwarakaku.Entities.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using Nett;

    public static class Logger
    {
        public static readonly ILogRecord DefaultByProcess = new LogRecord($"./default({System.Diagnostics.Process.GetCurrentProcess()})", false, false);

        static ILogRecord LogEntry(string profilePath, TomlTable toml, string resourceKey, bool enabled, bool timeStampPrintable)
        {
            return new LogRecord(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>(resourceKey)), enabled, timeStampPrintable);
        }

        static Logger()
        {
            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));

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
        }

        /// <summary>
        /// アドレスの登録。ログ・ファイルのリムーブに使用。
        /// </summary>
        public static Dictionary<ILogTag, ILogRecord> LogMap
        {
            get
            {
                if (Logger.logMap == null)
                {
                    Logger.logMap = new Dictionary<ILogTag, ILogRecord>();
                }
                return Logger.logMap;
            }
        }
        private static Dictionary<ILogTag, ILogRecord> logMap;

        public static void AddLog(ILogTag key, ILogRecord value)
        {
            Logger.LogMap.Add(key, value);
        }

        public static ILogRecord GetRecord(ILogTag logTag)
        {
            try
            {
                return LogMap[logTag];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"エラー: GetRecord(). [{logTag.Name}] {ex.Message}");
                throw;
            }
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
                foreach (string path in paths)
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
            catch (Exception ex) { Logger.Panic(LogTags.Error, ex, "ﾛｸﾞﾌｧｲﾙ削除中☆"); throw; }
        }

        /// <summary>
        /// メモを、ログ・ファイルの末尾に追記します。
        /// </summary>
        /// <param name="line"></param>
        public static void WriteLineAddMemo(
            ILogTag logTag,
            string line
            )
        {
            ILogRecord record = GetRecord(logTag);

            bool enable = record.Enabled;
            string filepath2 = Path.Combine(Application.StartupPath, record.FileName);

            if (!enable)
            {
                // ログ出力オフ
                goto gt_EndMethod;
            }

            // ログ追記 TODO:非同期
            try
            {
                StringBuilder sb = new StringBuilder();

                // タイムスタンプ
                if (record.TimeStampPrintable)
                {
                    sb.Append(DateTime.Now.ToString());
                    sb.Append(" : ");
                }
                else
                {
                    sb.Append("Memo:   ");
                }

                sb.Append(line);
                sb.AppendLine();

                System.IO.File.AppendAllText(filepath2, sb.ToString());
            }
            catch (Exception)
            {
                // 循環参照になるので、ログを取れません。
                // メモなので、ログ出力に失敗しても、続行します。
            }

        gt_EndMethod:
            ;
        }

        /// <summary>
        /// エラーを、ログ・ファイルに記録します。
        /// </summary>
        /// <param name="line"></param>
        public static void WriteLineError(
            ILogTag logTag,
            string line
            )
        {
            ILogRecord record = GetRecord(logTag);

            bool enable = record.Enabled;
            bool printTimestamp = record.TimeStampPrintable;
            string filepath2 = Path.Combine(Application.StartupPath, record.FileName);

            if (!enable)
            {
                // ログ出力オフ
                goto gt_EndMethod;
            }

            // ログ追記 TODO:非同期
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Error:   ");

                // タイムスタンプ
                if (printTimestamp)
                {
                    sb.Append(DateTime.Now.ToString());
                    sb.Append(" : ");
                }

                sb.Append(line);
                sb.AppendLine();

                string message = sb.ToString();
                MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);

                System.IO.File.AppendAllText(filepath2, message);
            }
            catch (Exception)
            {
                //>>>>> エラーが起こりました。

                // どうにもできないので無視します。
            }

        gt_EndMethod:
            ;
        }

        /// <summary>
        /// メモを、ログ・ファイルに記録します。
        /// </summary>
        /// <param name="line"></param>
        public static void WriteLineOverMemo(
            ILogTag logTag,
            string line
            )
        {
            ILogRecord record = GetRecord(logTag);

            bool enable = record.Enabled;
            string filepath2 = Path.Combine(Application.StartupPath, record.FileName);

            if (!enable)
            {
                // ログ出力オフ
                goto gt_EndMethod;
            }

            // ログ追記 TODO:非同期
            try
            {
                StringBuilder sb = new StringBuilder();

                // タイムスタンプ
                if (record.TimeStampPrintable)
                {
                    sb.Append(DateTime.Now.ToString());
                    sb.Append(" : ");
                }
                else
                {
                    sb.Append("Memo:   ");
                }

                sb.Append(line);
                sb.AppendLine();

                System.IO.File.WriteAllText(filepath2, sb.ToString());
            }
            catch (Exception)
            {
                // 循環参照になるので、ログを取れません。
                // メモなので、ログ出力に失敗しても、続行します。
            }

        gt_EndMethod:
            ;
        }

        /// <summary>
        /// サーバーへ送ったコマンドを、ログ・ファイルに記録します。
        /// </summary>
        /// <param name="line"></param>
        public static void WriteLineS(
            LogTag logTag,
            string line
            //,
            //[CallerMemberName] string memberName = "",
            //[CallerFilePath] string sourceFilePath = "",
            //[CallerLineNumber] int sourceLineNumber = 0
            )
        {
            ILogRecord record = GetRecord(logTag);

            bool enable = record.Enabled;
            bool print_TimeStamp = record.TimeStampPrintable;
            string filepath2 = Path.Combine(Application.StartupPath, record.FileName);

            if (!enable)
            {
                // ログ出力オフ
                goto gt_EndMethod;
            }

            // ログ追記 TODO:非同期
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(DateTime.Now.ToString());
                sb.Append("<   ");
                sb.Append(line);
                //sb.Append("：");
                //sb.Append(memberName);
                //sb.Append("：");
                //sb.Append(sourceFilePath);
                //sb.Append("：");
                //sb.Append(sourceLineNumber);
                sb.AppendLine();

                System.IO.File.AppendAllText(filepath2, sb.ToString());
            }
            catch (Exception)
            {
                // 循環参照になるので、ログを取れません。
                // Logger.ERROR.Panic(ex, "ログ取り中☆");
            }

        gt_EndMethod:
            ;
        }

        /// <summary>
        /// サーバーから受け取ったコマンドを、ログ・ファイルに記録します。
        /// </summary>
        /// <param name="line"></param>
        public static void WriteLineC(
            ILogTag logTag,
            string line
            //,
            //[CallerMemberName] string memberName = "",
            //[CallerFilePath] string sourceFilePath = "",
            //[CallerLineNumber] int sourceLineNumber = 0
            )
        {
            ILogRecord record = GetRecord(logTag);

            bool enable = record.Enabled;
            bool print_TimeStamp = record.TimeStampPrintable;
            string filepath2 = Path.Combine(Application.StartupPath, record.FileName);

            if (!enable)
            {
                // ログ出力オフ
                goto gt_EndMethod;
            }

            // ログ追記 TODO:非同期
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(DateTime.Now.ToString());
                sb.Append("  > ");
                sb.Append(line);
                //sb.Append("：");
                //sb.Append(memberName);
                //sb.Append("：");
                //sb.Append(sourceFilePath);
                //sb.Append("：");
                //sb.Append(sourceLineNumber);
                sb.AppendLine();

                System.IO.File.AppendAllText(filepath2, sb.ToString());
            }
            catch (Exception)
            {
                // 循環参照になるので、ログを取れません。
                // Logger.ERROR.Panic(ex, "ログ取り中☆");
            }

        gt_EndMethod:
            ;
        }

        /// <summary>
        /// 「どうにもならん、あきらめた」
        /// 
        /// 例外が発生したが、対応できないのでログだけ出します。
        /// デバッグ時は、ダイアログボックスを出します。
        /// </summary>
        /// <param name="okottaBasho"></param>
        public static void Panic(ILogTag logTag, string okottaBasho)
        {
            //>>>>> エラーが起こりました。
            string message = "起こった場所：" + okottaBasho;
            Debug.Fail(message);

            // どうにもできないので  ログだけ取って、上に投げます。
            Logger.WriteLineError(logTag, message);
            // ログ出力に失敗することがありますが、無視します。
        }

        /// <summary>
        /// 「どうにもならん、あきらめた」
        /// 
        /// 例外が発生したが、対応できないのでログだけ出します。
        /// デバッグ時は、ダイアログボックスを出します。
        /// </summary>
        /// <param name="okottaBasho"></param>
        public static void Panic(ILogTag logTag, Exception ex, string okottaBasho)
        {
            //>>>>> エラーが起こりました。
            string message = ex.GetType().Name + " " + ex.Message + "：" + okottaBasho;
            Debug.Fail(message);

            // どうにもできないので  ログだけ取って、上に投げます。
            Logger.WriteLineError(logTag, message);
            // ログ出力に失敗することがありますが、無視します。
        }
    }
}
