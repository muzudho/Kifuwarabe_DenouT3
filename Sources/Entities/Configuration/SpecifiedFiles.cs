namespace Grayscale.Kifuwarakaku.Entities.Configuration
{
    using System.IO;
    using Grayscale.Kifuwarakaku.Entities.Configuration;
    using Nett;

    public static class SpecifiedFiles
    {
        /// <summary>
        /// このクラスを使う前にセットしてください。
        /// </summary>
        public static void Init(IEngineConf engineConf)
        {
            /*
            AddLog(LogTags.NarabeNetwork, new LogRecord("../../Logs/#将棋GUI_ﾈｯﾄﾜｰｸ", true, false));
            // ログ。将棋エンジンきふわらべで汎用に使います。
            AddLog(LogTags.Engine, new LogRecord("../../Logs/#将棋ｴﾝｼﾞﾝ_汎用", true, false));
            // ログ。送受信内容の記録専用です。
            AddLog(LogTags.Client, new LogRecord("../../Logs/#将棋ｴﾝｼﾞﾝ_ｸﾗｲｱﾝﾄ", true, false));
            // ログ。思考ルーチン専用です。
            AddLog(LogTags.MousouRireki, new LogRecord("../../Logs/#将棋ｴﾝｼﾞﾝ_妄想履歴", true, false));
            */

            /*
            OutputForcePromotion = DataEntry( "OutputForcePromotion");
            OutputPieceTypeToHaiyaku = DataEntry( "OutputPieceTypeToHaiyaku");
            HaichiTenkanHyoOnlyDataLog = DataEntry( "HaichiTenkanHyoOnlyDataLog");
            HaichiTenkanHyoAllLog = DataEntry( "HaichiTenkanHyoAllLog");
            LatestPositionLogPng = LogEntry("LatestPositionLogPng");
            MousouRireki = LogEntry("MousouRireki");
            GuiDefault = LogEntry("GuiRecordLog");
            LinkedList = LogEntry("LinkedListLog");
            GuiPaint = LogEntry("GuiPaint");
            LegalMove = LogEntry("LegalMoveLog");
            LegalMoveEvasion = LogEntry("LegalMoveEvasionLog");
            GenMove = LogEntry("GenMoveLog");
            */
        }

        static IResFile LogEntry(IEngineConf engineConf, string key)
        {
            return ResFile.AsLog(engineConf.LogDirectory, engineConf.GetLogBasename(key));
        }
        static IResFile DataEntry(IEngineConf engineConf, string key)
        {
            return ResFile.AsData(engineConf.GetResourceFullPath(key));
        }

        /*
        public static ILogFile OutputForcePromotion { get; private set; }
        public static ILogFile OutputPieceTypeToHaiyaku { get; private set; }
        public static ILogFile HaichiTenkanHyoOnlyDataLog { get; private set; }
        public static ILogFile HaichiTenkanHyoAllLog { get; private set; }
        public static ILogFile LatestPositionLogPng { get; private set; }
        public static ILogFile MousouRireki { get; private set; }
        public static ILogFile GuiDefault { get; private set; }
        public static ILogFile LinkedList { get; private set; }
        public static ILogFile GuiPaint { get; private set; }
        public static ILogFile LegalMove { get; private set; }
        public static ILogFile LegalMoveEvasion { get; private set; }
        /// <summary>
        /// 指し手生成だけ別ファイルにログを取りたいとき。
        /// </summary>
        public static ILogFile GenMove { get; private set; }
        */
    }
}
