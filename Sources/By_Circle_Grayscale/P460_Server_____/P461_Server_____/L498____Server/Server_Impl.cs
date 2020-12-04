using Grayscale.P224Sky.L500Struct;
using Grayscale.P247KyokumenWra.L500Struct;
using Grayscale.P258UtilSky258.L500UtilSky;
using Grayscale.P324KifuTree.L250Struct;
using Grayscale.P325PnlTaikyoku.I250Struct;
using Grayscale.P325PnlTaikyoku.L250Struct;
using Grayscale.P461_Server_____.L___125_Receiver;
using Grayscale.P461_Server_____.L___497_EngineClient;
using Grayscale.P461_Server_____.L___498_Server;
using Grayscale.P461_Server_____.L497____EngineClient;

namespace Grayscale.P461_Server_____.L498____Server
{
    /// <summary>
    /// 擬似将棋サーバー。
    /// </summary>
    public class Server_Impl : Server
    {
        #region プロパティ

        public Model_Taikyoku Model_Taikyoku { get { return this.model_Taikyoku; } }
        private Model_Taikyoku model_Taikyoku;


        /// <summary>
        /// サーバーが持つ、将棋エンジン。
        /// </summary>
        public EngineClient EngineClient { get { return this.engineClient; } }
        protected EngineClient engineClient;

        /// <summary>
        /// 将棋エンジンからの入力文字列（入力欄に入ったもの）を、一旦　蓄えたもの。
        /// </summary>
        public string InputString99 { get { return this.inputString99; } }
        public void AddInputString99(string inputString99)
        {
            this.inputString99 += inputString99;
        }
        public void SetInputString99(string inputString99)
        {
            this.inputString99 = inputString99;
        }
        public void ClearInputString99()
        {
            this.inputString99 = "";
        }
        private string inputString99;

        #endregion


        public Server_Impl(SkyConst src_Sky, int temezumi, Receiver receiver)
        {
            this.engineClient = new EngineClient_Impl(receiver);
            this.engineClient.SetOwner_Server(this);

            //----------
            // モデル
            //----------
            this.model_Taikyoku = new Model_TaikyokuImpl(new KifuTreeImpl(
                    new KifuNodeImpl(
                        Util_Sky258A.ROOT_SASITE,
                        new KyokumenWrapper(SkyConst.NewInstance(
                            src_Sky,//model_Manual.GuiSkyConst,
                            temezumi//model_Manual.GuiTemezumi
                            ))
                    )
            ));
            this.Model_Taikyoku.Kifu.SetProperty(Word_KifuTree.PropName_Startpos, "9/9/9/9/9/9/9/9/9");

            this.inputString99 = "";
        }

    }
}
