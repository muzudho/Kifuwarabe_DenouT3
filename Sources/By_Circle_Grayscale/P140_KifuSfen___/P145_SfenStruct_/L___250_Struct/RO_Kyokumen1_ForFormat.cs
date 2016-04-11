
namespace Grayscale.P145_SfenStruct_.L___250_Struct
{

    /// <summary>
    /// テーブル形式の局面データ。
    /// </summary>
    public interface RO_Kyokumen1_ForFormat
    {

        /// <summary>
        /// 将棋盤上の駒。[suji][dan]。
        /// sujiは1～9。danは1～9。0は空欄。つまり 100要素ある。
        /// 「K」「+p」といった形式で書く。(SFEN形式)
        /// </summary>
        string[,] Ban { get; set; }

        /// <summary>
        /// 持ち駒の数。[player,komasyurui]
        /// ここで、player は 1,2。0は使わない。
        /// 駒の種類は [0]から、飛,角,金,銀,桂,香,歩 の順。
        /// </summary>
        int[,] Moti { get; set; }

        /// <summary>
        /// 手目済み。初期局面を 0手目済み、初手を指した後の局面を 1手目済みとカウントします。
        /// </summary>
        int Temezumi { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="white">「w」なら真、「b」なら偽。</param>
        ///// <returns></returns>
        //string ToSfenstring(bool white);

        void GetMoti(
            out int mK,
            out int mR,
            out int mB,
            out int mG,
            out int mS,
            out int mN,
            out int mL,
            out int mP,

            out int mk,
            out int mr,
            out int mb,
            out int mg,
            out int ms,
            out int mn,
            out int ml,
            out int mp
        );

    }
}
