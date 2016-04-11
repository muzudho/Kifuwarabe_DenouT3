using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P218_Starlight__.L___500_Struct;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P353_Conv_SasuEx.L500____Converter
{
    public class SasuEntry
    {
        public Starbeamable NewSasite { get; set; }

        //public string SasiteStr { get; set; }

        public Finger Finger { get; set; }

        public SyElement Masu { get; set; }

        /// <summary>
        /// 成るなら真。
        /// </summary>
        public bool Naru { get; set; }

        public SasuEntry(
            Starbeamable newSasite,
            //string sasiteStr,
            Finger finger,
            SyElement masu,
            bool naru
            )
        {
            this.NewSasite = newSasite;
            //this.SasiteStr = sasiteStr;
            this.Finger = finger;
            this.Masu = masu;
            this.Naru = naru;
        }
    }
}
