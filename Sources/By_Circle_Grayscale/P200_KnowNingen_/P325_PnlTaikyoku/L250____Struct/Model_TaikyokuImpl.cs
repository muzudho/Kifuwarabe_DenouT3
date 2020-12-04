using Grayscale.P324_KifuTree___.I250Struct;
using Grayscale.P325_PnlTaikyoku.I250Struct;

namespace Grayscale.P325_PnlTaikyoku.L250Struct
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
