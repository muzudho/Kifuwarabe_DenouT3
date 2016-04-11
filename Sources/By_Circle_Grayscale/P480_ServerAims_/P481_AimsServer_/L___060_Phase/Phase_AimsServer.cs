
namespace Grayscale.P481_AimsServer_.L___060_Phase
{
    /// <summary>
    /// 内部状態。
    /// </summary>
    public enum Phase_AimsServer
    {

        /// <summary>
        /// 最初の状態はこれ。
        /// </summary>
        _01_Server_Booted,

        /// <summary>
        /// 
        /// </summary>
        _02_WaitAimsUsiok,

        /// <summary>
        /// 将棋エンジンを起動してから、起動を確認できるまでの間のフェーズだぜ☆
        /// </summary>
        _03_WaitEngineLive,

        /// <summary>
        /// AIMSから「usi」メッセージを受け取ったあと。
        /// 将棋エンジンからの usiok を待つ「waitEngineUsiok」フェーズに入ります。
        /// </summary>
        _04_WaitEngineUsiok,

        /// <summary>
        /// 将棋エンジンから「usiok」を受け取ったので、
        /// AIMS GUIに「usiok」を送ったところです。
        /// AIMS GUIからの「isready」の返信を待つフェーズです。
        /// </summary>
        _05_WaitAimsReadyok,

        /// <summary>
        /// AIMSから「usiok」メッセージを受け取ったあと。
        /// 将棋エンジンからの「readyok」を待つ「WaitEngineReadyok」フェーズに入ります。
        /// </summary>
        _05_WaitEngineReadyok,

        /// <summary>
        /// 将棋エンジンから「readyok」を受け取ったので、
        /// AIMS GUIに「isready」を送ったところです。
        /// AIMS GUIからの「readyok」の返信を待つフェーズです。
        /// </summary>
        _06_WaitAimsIsready,

        /// <summary>
        /// AIMS GUIからの「bestmove」の返信を待つフェーズです。
        /// </summary>
        _101_WaitAimsBestmove,

    }
}
