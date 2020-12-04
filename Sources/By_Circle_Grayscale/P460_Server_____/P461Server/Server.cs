using Grayscale.P325PnlTaikyoku.I250Struct;
using Grayscale.P461Server.I497EngineClient;

namespace Grayscale.P461Server.L498Server
{
    /// <summary>
    /// 将棋サーバー。
    /// </summary>
    public interface Server
    {

        /// <summary>
        /// 将棋エンジン。
        /// </summary>
        EngineClient EngineClient { get; }

        /// <summary>
        /// 将棋エンジンからの入力文字列（入力欄に入ったもの）を、一旦　蓄えたもの。
        /// </summary>
        string InputString99 { get; }
        void AddInputString99(string inputString99);
        void SetInputString99(string inputString99);
        void ClearInputString99();


        Model_Taikyoku Model_Taikyoku { get; }
    }
}
