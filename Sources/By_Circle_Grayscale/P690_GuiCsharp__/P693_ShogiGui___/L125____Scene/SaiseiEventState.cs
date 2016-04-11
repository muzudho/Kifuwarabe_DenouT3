using Grayscale.P003_Log________.L___500_Struct;

namespace Grayscale.P693_ShogiGui___.L125____Scene
{
    /// <summary>
    /// [再生]イベントの状態です。
    /// </summary>
    public class SaiseiEventState
    {

        public SaiseiEventStateName Name2 { get { return this.name2; } }
        private SaiseiEventStateName name2;

        public KwErrorHandler Flg_logTag { get { return this.flg_logTag; } }
        private KwErrorHandler flg_logTag;


        public SaiseiEventState()
        {
            this.name2 = SaiseiEventStateName.Ignore;
        }

        public SaiseiEventState(SaiseiEventStateName name2, KwErrorHandler flg_logTag)
        {
            this.name2 = name2;
            this.flg_logTag = flg_logTag;
        }

    }
}
