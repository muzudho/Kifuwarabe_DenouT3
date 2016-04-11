using Grayscale.P145_SfenStruct_.L___250_Struct;

namespace Grayscale.P145_SfenStruct_.L250____Struct
{

    /// <summary>
    /// 盤上の駒を２次元配列で。
    /// 持ち駒を種類別の配列で。
    /// </summary>
    public class RO_Kyokumen1_ForFormatImpl : RO_Kyokumen1_ForFormat
    {

        public string[,] Ban { get; set; }
        public int[,] Moti { get; set; }
        /// <summary>
        /// 手目済み。初期局面を 0手目済み、初手を指した後の局面を 1手目済みとカウントします。
        /// </summary>
        public int Temezumi { get; set; }

        public RO_Kyokumen1_ForFormatImpl()
        {
            this.Ban = new string[10,10];// 将棋盤。10×10。0は使わない。
            {// 全クリアー
                // 将棋盤
                for (int suji = 0; suji < 10; suji++)
                {
                    for (int dan = 0; dan < 10; dan++)
                    {
                        this.Ban[suji, dan] = "";
                    }
                }
            }

            // 先手の持ち駒の数。
            // [player,komasyurui]
            // playerは 1,2。0は使わない。
            // komasyuruiは、[0]から、飛,角,金,銀,桂,香,歩 の順。
            this.Moti = new int[3, 7];

            this.Temezumi = 1;//将棋所に合わせて、 1固定 をデフォルトとする。
        }



        public  void GetMoti(
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
        )
        {
            int player;
            player = 1;
            mK = 0;
            mR = this.Moti[player, 0];
            mB = this.Moti[player, 1];
            mG = this.Moti[player, 2];
            mS = this.Moti[player, 3];
            mN = this.Moti[player, 4];
            mL = this.Moti[player, 5];
            mP = this.Moti[player, 6];

            mk = 0;
            player = 2;
            mr = this.Moti[player, 0];
            mb = this.Moti[player, 1];
            mg = this.Moti[player, 2];
            ms = this.Moti[player, 3];
            mn = this.Moti[player, 4];
            ml = this.Moti[player, 5];
            mp = this.Moti[player, 6];
        }

    }
}
