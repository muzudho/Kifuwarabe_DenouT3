using Grayscale.P211WordShogi.L500Word;
using Grayscale.P213Komasyurui.L250Word;

namespace Grayscale.P372_KyokuParser.L___500_Parser
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
