using Grayscale.P324_KifuTree___.I250Struct;

namespace Grayscale.P325_PnlTaikyoku.I250Struct
{
    public interface Model_Taikyoku
    {

        KifuTree Kifu { get; }
        void SetKifu(KifuTree kifu);
    }
}
