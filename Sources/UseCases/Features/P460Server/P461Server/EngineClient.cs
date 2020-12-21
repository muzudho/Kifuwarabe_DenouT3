using Grayscale.Kifuwarakaku.Entities.Features;

namespace Grayscale.Kifuwarakaku.UseCases.Features
{

    /// <summary>
    /// 将棋エンジン クライアント。
    /// TODO: MainGui に統合したい。
    /// </summary>
    public interface EngineClient
    {
        object Owner_Server { get; }//Server型
        void SetOwner_Server(object owner_Server);//Server型

        /// <summary>
        /// レシーバー
        /// </summary>
        Receiver Receiver { get; }

        void Start(string shogiEngineFilePath);

        /// <summary>
        /// 将棋エンジンのプロセスです。
        /// </summary>
        EngineProcessWrapper ShogiEngineProcessWrapper { get; set; }

        /// <summary>
        /// 手番が変わったときに、実行する処理をここに書いてください。
        /// </summary>
        /// <param name="kifu"></param>
        /// <param name="logTag"></param>
        void OnChangedTurn(KifuTree kifu);

        /// <summary>
        /// 将棋エンジンに、終了するように促します。
        /// </summary>
        void Send_Shutdown();

        /// <summary>
        /// 将棋エンジンに、ログを出すように促します。
        /// </summary>
        void Send_Logdase();

        ///// <summary>
        ///// 将棋エンジンを先手にするために、go を出します。
        ///// </summary>
        //void Send_Go(ILogTag logTag);

    }
}
