using Grayscale.P003_Log________.L___500_Struct;
using System.Diagnostics;

namespace Grayscale.P461_Server_____.L___496_EngineWrapper
{
    public delegate void DELEGATE_ShogiServer_ToEngine(string line, KwErrorHandler errH);

    /// <summary>
    /// 至将棋エンジン 通信インターフェース。
    /// </summary>
    public interface EngineProcessWrapper
    {
        DELEGATE_ShogiServer_ToEngine Delegate_ShogiServer_ToEngine { get; }
        void  SetDelegate_ShogiServer_ToEngine( DELEGATE_ShogiServer_ToEngine delegateMethod);

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// これが、将棋エンジン（プロセス）です。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        Process ShogiEngine { get; }
        void SetShogiEngine(Process shogiEngine);


        /// <summary>
        /// 将棋エンジンに、"noop from server"を送信します。
        /// </summary>
        void Send_Noop_from_server( KwErrorHandler errH);

        
        /// <summary>
        /// 将棋エンジンに、"usinewgame"を送信します。
        /// </summary>
        void Send_Usinewgame( KwErrorHandler errH);


        /// <summary>
        /// 将棋エンジンに、"usi"を送信します。
        /// </summary>
        void Send_Usi( KwErrorHandler errH);


                
        /// <summary>
        /// 将棋エンジンに、終了するように促します。
        /// </summary>
        void Send_Shutdown( KwErrorHandler errH);


        
        /// <summary>
        /// 将棋エンジンに、"setoption ～略～"を送信します。
        /// </summary>
        void Send_Setoption(string setoption, KwErrorHandler errH);


                
        /// <summary>
        /// 将棋エンジンに、"quit"を送信します。
        /// </summary>
        void Send_Quit( KwErrorHandler errH);


                
        /// <summary>
        /// 将棋エンジンに、"position ～略～"を送信します。
        /// </summary>
        void Send_Position(string position, KwErrorHandler errH);


        
        /// <summary>
        /// 将棋エンジンに、ログを出すように促します。
        /// </summary>
        void Send_Logdase( KwErrorHandler errH);


                
        /// <summary>
        /// 将棋エンジンに、"isready"を送信します。
        /// </summary>
        void Send_Isready( KwErrorHandler errH);


                
        /// <summary>
        /// 将棋エンジンが起動しているか否かです。
        /// </summary>
        /// <returns></returns>
        bool IsLive_ShogiEngine();



        /// <summary>
        /// 将棋エンジンに、"go"を送信します。
        /// </summary>
        void Send_Go( KwErrorHandler errH);



        /// <summary>
        /// 将棋エンジンに、"gameover lose"を送信します。
        /// </summary>
        void Send_Gameover_lose( KwErrorHandler errH);

    }
}
