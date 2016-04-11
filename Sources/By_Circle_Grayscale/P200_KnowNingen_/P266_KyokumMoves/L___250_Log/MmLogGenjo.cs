#if DEBUG
using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P035_Collection_.L500____Struct;
using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P222_Log_Kaisetu.L250____Struct;
using Grayscale.P224_Sky________.L500____Struct;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P266_KyokumMoves.L___250_Log
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

        Starbeamable Sasite { get; }

        KwErrorHandler ErrH { get; }

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
