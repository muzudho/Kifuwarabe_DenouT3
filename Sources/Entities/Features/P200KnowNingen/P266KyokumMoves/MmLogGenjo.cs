#if DEBUG
    using Grayscale.Kifuwarakaku.Entities.Logger;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P266KyokumMoves.L250Log
{
    /// <summary>
    /// ログを取るためのもの。
    /// </summary>
    public interface MmLogGenjo
    {
        KaisetuBoard BrdMove { get; set; }

        /// <summary>
        /// 読み開始手目済み
        /// </summary>
        int YomikaisiTemezumi { get; }

        int Temezumi_yomiCur { get; }

        IMove Move { get; }

        IErrorController ErrH { get; }

        void Log1(Playerside pside_genTeban3);

        void Log2(
            Playerside tebanSeme,//手番（利きを調べる側）
            Playerside tebanKurau//手番（喰らう側）
        );

        void Log3(
            SkyConst src_Sky,
            Playerside tebanKurau,//手番（喰らう側）
            Playerside tebanSeme,//手番（利きを調べる側）
            Fingers fingers_kurau_IKUSA,//戦駒（喰らう側）
            Fingers fingers_kurau_MOTI,// 持駒（喰らう側）
            Fingers fingers_seme_IKUSA,//戦駒（利きを調べる側）
            Fingers fingers_seme_MOTI// 持駒（利きを調べる側）
        );

        void Log4(
            SkyConst src_Sky,
            Playerside tebanSeme,//手番（利きを調べる側）
            Maps_OneAndOne<Finger, SySet<SyElement>> kmMove_seme_IKUSA
        );

    }
}
#endif
