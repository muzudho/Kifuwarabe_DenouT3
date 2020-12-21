
namespace Grayscale.Kifuwarakaku.Entities.Features
{
    public interface KifuReaderB_State
    {

        void Execute(string inputLine, out string nextCommand, out string rest);

    }
}
