using Grayscale.P003Log.I500Struct;

namespace Grayscale.P693ShogiGui.L125Scene
{
    /// <summary>
    /// [再生]イベントの状態です。
    /// </summary>
    public class SaiseiEventState
    {

        public SaiseiEventStateName Name2 { get { return this.name2; } }
        private SaiseiEventStateName name2;

        public IKwErrorHandler Flg_logTag { get { return this.flg_logTag; } }
        private IKwErrorHandler flg_logTag;


        public SaiseiEventState()
        {
            this.name2 = SaiseiEventStateName.Ignore;
        }

        public SaiseiEventState(SaiseiEventStateName name2, IKwErrorHandler flg_logTag)
        {
            this.name2 = name2;
            this.flg_logTag = flg_logTag;
        }

    }
}
