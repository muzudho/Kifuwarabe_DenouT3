using Grayscale.P324KifuTree.I250Struct;
using Grayscale.P325PnlTaikyoku.I250Struct;

namespace Grayscale.P325PnlTaikyoku.L250Struct
{
    public class Model_TaikyokuImpl : Model_Taikyoku
    {
        public KifuTree Kifu
        {
            get
            {
                return this.kifu;
            }
        }
        public void SetKifu(KifuTree kifu)
        {
            this.kifu = kifu;
        }
        private KifuTree kifu;

        public Model_TaikyokuImpl(KifuTree kifu)
        {
            this.kifu = kifu;
        }
    }
}
