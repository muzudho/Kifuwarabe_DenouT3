using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P213_Komasyurui_.L250____Word;

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
