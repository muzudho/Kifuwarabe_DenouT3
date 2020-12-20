using Grayscale.Kifuwarakaku.Entities.Logging;

namespace Grayscale.P693ShogiGui.L125Scene
{
    /// <summary>
    /// [再生]イベントの状態です。
    /// </summary>
    public class SaiseiEventState
    {

        public SaiseiEventStateName Name2 { get { return this.name2; } }
        private SaiseiEventStateName name2;

        public SaiseiEventState()
        {
            this.name2 = SaiseiEventStateName.Ignore;
        }

        public SaiseiEventState(SaiseiEventStateName name2)
        {
            this.name2 = name2;
        }

    }
}
