using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P324_KifuTree___.L___250_Struct;
using Grayscale.P461_Server_____.L___125_Receiver;
using Grayscale.P461_Server_____.L___496_EngineWrapper;

namespace Grayscale.P461_Server_____.L___497_EngineClient
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
        /// <param name="errH"></param>
        void OnChangedTurn(KifuTree kifu, KwErrorHandler errH);

        /// <summary>
        /// 将棋エンジンに、終了するように促します。
        /// </summary>
        void Send_Shutdown(KwErrorHandler errH);

        /// <summary>
        /// 将棋エンジンに、ログを出すように促します。
        /// </summary>
        void Send_Logdase(KwErrorHandler errH);

        ///// <summary>
        ///// 将棋エンジンを先手にするために、go を出します。
        ///// </summary>
        //void Send_Go(KwErrorHandler errH);

    }
}
