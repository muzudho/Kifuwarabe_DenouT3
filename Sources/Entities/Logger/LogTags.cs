namespace Grayscale.Kifuwarakaku.Entities.Logger
{
    /// <summary>
    /// ログのタグ全集。
    /// </summary>
    public class LogTags
    {
        public static readonly ILogTag Default = new LogTag("Default");
        public static readonly ILogTag Error = new LogTag("Error");
        public static readonly ILogTag DefaultSennitite = new LogTag("DefaultSennitite");
        public static readonly ILogTag ServerDefault = new LogTag("ServerDefault");
        public static readonly ILogTag ServerNetworkAsync = new LogTag("ServerNetworkAsync");
        public static readonly ILogTag CsharpGuiDefault = new LogTag("CsharpGuiDefault");
        public static readonly ILogTag CsharpGuiKifuYomitori = new LogTag("CsharpGuiKifuYomitori");
        public static readonly ILogTag CsharpGuiNetwork = new LogTag("CsharpGuiNetwork");
        public static readonly ILogTag CsharpGuiPaint = new LogTag("CsharpGuiPaint");
        public static readonly ILogTag CsharpGuiSennitite = new LogTag("CsharpGuiSennitite");
        public static readonly ILogTag AimsDefault = new LogTag("AimsDefault");
        public static readonly ILogTag EngineDefault = new LogTag("EngineDefault");
        public static readonly ILogTag EngineNetwork = new LogTag("EngineNetwork");
        public static readonly ILogTag EngineMousouRireki = new LogTag("EngineMousouRireki");
        public static readonly ILogTag EngineSennitite = new LogTag("EngineSennitite");
        public static readonly ILogTag TestProgram = new LogTag("TestProgram");
        public static readonly ILogTag Learner = new LogTag("Learner");
        public static readonly ILogTag SpeedKeisoku = new LogTag("SpeedKeisoku");
    }
}
