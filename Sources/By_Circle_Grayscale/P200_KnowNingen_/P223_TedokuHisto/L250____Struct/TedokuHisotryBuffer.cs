using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P223_TedokuHisto.L___250_Struct;
using System.Collections.Generic;

namespace Grayscale.P223_TedokuHisto.L250____Struct
{
    /// <summary>
    /// 手得ヒストリー
    /// </summary>
    public class TedokuHistoryBuffer : TedokuHistory
    {
        #region プロパティー

        public List<SyElement>[] Fu___ { get { return this.fu; } }
        private List<SyElement>[] fu;

        public List<SyElement>[] Kyo__ { get { return this.kyo; } }
        private List<SyElement>[] kyo;

        public List<SyElement>[] Kei__ { get { return this.kei; } }
        private List<SyElement>[] kei;

        public List<SyElement>[] Gin__ { get { return this.gin; } }
        private List<SyElement>[] gin;

        public List<SyElement>[] Kin__ { get { return this.kin; } }
        private List<SyElement>[] kin;

        public List<SyElement> Gyoku { get { return this.gyoku; } }
        private List<SyElement> gyoku;

        public List<SyElement>[] Hisya { get { return this.hisya; } }
        private List<SyElement>[] hisya;

        public List<SyElement>[] Kaku_ { get { return this.kaku; } }
        private List<SyElement>[] kaku;

        #endregion

        public TedokuHistoryBuffer(
            TedokuHistory src
            )
        {
            this.fu = new List<SyElement>[]{
                src.Fu___[0],
                src.Fu___[1],
                src.Fu___[2],
                src.Fu___[3],
                src.Fu___[4],
                src.Fu___[5],
                src.Fu___[6],
                src.Fu___[7],
                src.Fu___[8],
            };
            this.kyo = new List<SyElement>[]{
                src.Kyo__[0],
                src.Kyo__[1],
                src.Kyo__[2],
                src.Kyo__[3],
            };
            this.kei = new List<SyElement>[]{
                src.Kei__[0],
                src.Kei__[1],
                src.Kei__[2],
                src.Kei__[3],
            };
            this.gin = new List<SyElement>[]{
                src.Gin__[0],
                src.Gin__[1],
                src.Gin__[2],
                src.Gin__[3],
            };
            this.kin = new List<SyElement>[]{
                src.Kin__[0],
                src.Kin__[1],
                src.Kin__[2],
                src.Kin__[3],
            };
            this.gyoku = src.Gyoku;
            this.hisya = new List<SyElement>[]{
                src.Hisya[0],
                src.Hisya[1],
                src.Hisya[2],
                src.Hisya[3],
            };
            this.kaku = new List<SyElement>[]{
                src.Kaku_[0],
                src.Kaku_[1],
                src.Kaku_[2],
                src.Kaku_[3],
            };
        }
    }
}
