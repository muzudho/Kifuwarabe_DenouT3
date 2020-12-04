using Grayscale.P274_KifuReaderB.I500Reader;

namespace Grayscale.P274_KifuReaderB.L500Reader
{
    public class KifuReaderB_StateB0 : KifuReaderB_State
    {

        public void Execute(string inputLine, out string nextCommand, out string rest)
        {
            nextCommand = "";
            rest = inputLine;
        }

    }
}
