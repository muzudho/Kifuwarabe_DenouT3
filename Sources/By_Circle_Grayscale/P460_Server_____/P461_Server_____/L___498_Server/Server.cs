using Grayscale.P325_PnlTaikyoku.L___250_Struct;
using Grayscale.P461_Server_____.L___497_EngineClient;

namespace Grayscale.P461_Server_____.L___498_Server
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
