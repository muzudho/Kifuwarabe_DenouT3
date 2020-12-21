using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P353ConvSasuEx.L500Converter
{
    public class SasuEntry
    {
        public IMove NewMove { get; set; }

        //public string MoveStr { get; set; }

        public Finger Finger { get; set; }

        public SyElement Masu { get; set; }

        /// <summary>
        /// 成るなら真。
        /// </summary>
        public bool Naru { get; set; }

        public SasuEntry(
            IMove newMove,
            //string moveStr,
            Finger finger,
            SyElement masu,
            bool naru
            )
        {
            this.NewMove = newMove;
            //this.MoveStr = moveStr;
            this.Finger = finger;
            this.Masu = masu;
            this.Naru = naru;
        }
    }
}
