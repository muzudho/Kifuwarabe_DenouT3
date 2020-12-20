using Grayscale.P324KifuTree.I250Struct;

namespace Grayscale.P325PnlTaikyoku.I250Struct
{
    public interface Model_Taikyoku
    {

        KifuTree Kifu { get; }
        void SetKifu(KifuTree kifu);
    }
}
