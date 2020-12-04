
namespace Grayscale.P274_KifuReaderB.I500Reader
{
    public interface KifuReaderB_State
    {

        void Execute(string inputLine, out string nextCommand, out string rest);

    }
}
