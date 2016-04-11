
namespace Grayscale.P274_KifuReaderB.L___500_Reader
{
    public interface KifuReaderB_State
    {

        void Execute(string inputLine, out string nextCommand, out string rest);

    }
}
