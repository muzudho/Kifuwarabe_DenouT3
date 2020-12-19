using Grayscale.Kifuwarakaku.Entities.Logging;
using Grayscale.P693ShogiGui.I125Scene;

namespace Grayscale.P693ShogiGui.L250Timed
{
    public abstract class Timed_Abstract : Timed
    {
        abstract public void Step(ILogTag logTag);
    }
}
