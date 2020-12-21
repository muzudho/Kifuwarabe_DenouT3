namespace Grayscale.Kifuwarakaku.Entities.Features
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
