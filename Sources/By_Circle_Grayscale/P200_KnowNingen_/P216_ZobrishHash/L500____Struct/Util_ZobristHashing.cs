using Grayscale.P031_Random_____.L500____Struct;
using Grayscale.P061_ConstShogi_.L250____Const;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P213_Komasyurui_.L250____Word;

namespace Grayscale.P216_ZobrishHash.L500____Struct
{
    /// <summary>
    /// 本将棋の千日手検出用のゾブリスト・ハッシュ・テーブルを作成します。
    /// </summary>
    public abstract class Util_ZobristHashing
    {

        /// <summary>
        /// 升の数　×　プレイヤー２人分の駒種類。
        /// </summary>
        private static ulong[,] randamValueTable = null;

        private static void Init()
        {
            Util_ZobristHashing.randamValueTable = new ulong[ConstShogi.BAN_SIZE, 2 * Array_Komasyurui.Items_AllElements.Length];
            for (int masu2 = 0; masu2 < ConstShogi.BAN_SIZE; masu2++)
            {
                
                //Util_ZobristHashing.randamValueTable[masu2] = new int[
                //    Array_Komasyurui.Items_All.Length * 2// プレイヤー２人分
                //    ];

                foreach (Komasyurui14 komasyurui2 in Array_Komasyurui.Items_AllElements)
                {
                    // プレイヤー２人分
                    Util_ZobristHashing.randamValueTable[masu2,(int)komasyurui2] = (ulong)(KwRandom.Random.NextDouble()*ulong.MaxValue);
                    Util_ZobristHashing.randamValueTable[masu2,(int)komasyurui2 + Array_Komasyurui.Items_AllElements.Length] = (ulong)(KwRandom.Random.NextDouble()*ulong.MaxValue);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="masu">升0～80。</param>
        /// <param name="playerNumber">1,2</param>
        /// <param name="komaSyurui">駒種類</param>
        /// <returns></returns>
        public static ulong GetValue(int masu1, Playerside playerNumber1, Komasyurui14 komaSyurui1)
        {
            ulong result;

            if (null == Util_ZobristHashing.randamValueTable)
            {
                Util_ZobristHashing.Init();
            }

            if(masu1<0 || ConstShogi.BAN_SIZE<= masu1)
            {
                result = 0;
                goto gt_EndMethod;
            }

            int b = ((int)playerNumber1-1) * Array_Komasyurui.Items_AllElements.Length + (int)komaSyurui1;

            result = Util_ZobristHashing.randamValueTable[masu1,b];

        gt_EndMethod:
            return result;
        }


    }
}
