using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;

namespace Grayscale.P372KyokuParser.I500Parser
{
    public interface MotiItem
    {
        /// <summary>
        /// 駒の種類。
        /// </summary>
        Komasyurui14 Komasyurui { get; }

        /// <summary>
        /// 持っている枚数。
        /// </summary>
        int Maisu { get; }

        /// <summary>
        /// プレイヤーサイド。
        /// </summary>
        Playerside Playerside { get; }

    }
}
