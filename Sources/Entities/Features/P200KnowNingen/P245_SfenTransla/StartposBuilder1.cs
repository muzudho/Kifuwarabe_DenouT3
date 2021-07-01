using System;
using System.Collections.Generic;
using System.Text;

namespace Grayscale.Kifuwarakaku.Entities.Features
{
    /// <summary>
    /// 将棋盤上の情報を数えます。未使用？
    /// </summary>
    [Obsolete("使ってない？")]
    public class StartposBuilder1
    {
        /// <summary>
        /// 先後。
        /// </summary>
        private bool psideIsBlack;

        /// <summary>
        /// 盤上の駒。
        /// </summary>
        private Dictionary<int, RO_Star> ban81;

        /// <summary>
        /// 先手の持ち駒の数。
        /// </summary>
        private Dictionary<PieceType, int> motiBlack;

        /// <summary>
        /// 後手の持ち駒の数。
        /// </summary>
        private Dictionary<PieceType, int> motiWhite;

        public StartposBuilder1()
        {
            this.psideIsBlack = true;
            this.ban81 = new Dictionary<int, RO_Star>();
            this.motiBlack = new Dictionary<PieceType, int>();
            this.motiWhite = new Dictionary<PieceType, int>();
        }

        /// <summary>
        /// FIXME:使ってない？
        /// </summary>
        /// <param name="masuNumber"></param>
        /// <param name="koma"></param>
        public void AddKoma(int masuNumber, RO_Star koma)
        {
            if (Conv_MasuHandle.OnShogiban(masuNumber))
            {
                this.ban81.Add(masuNumber, koma);
            }
            else if (Conv_MasuHandle.OnSenteKomadai(masuNumber))
            {
                if (this.motiBlack.ContainsKey(koma.Komasyurui))
                {
                    this.motiBlack[koma.Komasyurui] = this.motiBlack[koma.Komasyurui];
                }
                else
                {
                    this.motiBlack.Add(koma.Komasyurui, 0);
                }
            }
            else if (Conv_MasuHandle.OnGoteKomadai(masuNumber))
            {
                if (this.motiWhite.ContainsKey(koma.Komasyurui))
                {
                    this.motiWhite[koma.Komasyurui] = this.motiWhite[koma.Komasyurui];
                }
                else
                {
                    this.motiWhite.Add(koma.Komasyurui, 0);
                }
            }
        }


        private string CreateDanString(int leftestMasu)
        {
            StringBuilder sb = new StringBuilder();

            List<RO_Star> list = new List<RO_Star>();
            for (int masuNumber = leftestMasu; masuNumber >= 0; masuNumber -= 9)
            {
                if (this.ban81.ContainsKey(masuNumber))
                {
                    list.Add(this.ban81[masuNumber]);
                }
                else
                {
                    list.Add(null);
                }
            }

            foreach (RO_Star koma in list)
            {
            }

            return sb.ToString();
        }

        /// <summary>
        /// TODO:作りかけ
        /// </summary>
        /// <returns></returns>
        public string ToSfenString()
        {
            StringBuilder sb = new StringBuilder();

            // 1段目
            {
                //マス番号は、72,63,54,45,36,27,18,9,0。
                sb.Append(this.CreateDanString(72));
            }
            sb.Append("/");

            // 2段目
            {
                //マス番号は、73,64,55,46,37,28,19,10,1。
                sb.Append(this.CreateDanString(73));
            }
            sb.Append("/");

            // 3段目
            {
                sb.Append(this.CreateDanString(74));
            }
            sb.Append("/");

            // 4段目
            {
                sb.Append(this.CreateDanString(75));
            }
            sb.Append("/");

            // 5段目
            {
                sb.Append(this.CreateDanString(76));
            }
            sb.Append("/");

            // 6段目
            {
                sb.Append(this.CreateDanString(77));
            }
            sb.Append("/");

            // 7段目
            {
                sb.Append(this.CreateDanString(78));
            }
            sb.Append("/");

            // 8段目
            {
                sb.Append(this.CreateDanString(79));
            }
            sb.Append("/");

            // 9段目
            {
                sb.Append(this.CreateDanString(80));
            }

            // 先後
            if (this.psideIsBlack)
            {
                sb.Append(" b");
            }
            else
            {
                sb.Append(" w");
            }

            // 持ち駒
            if (this.motiBlack.Count < 1 && this.motiWhite.Count < 1)
            {
                sb.Append(" -");
            }
            else
            {
                sb.Append(" ");

                // 先手持ち駒
                //王
                if (this.motiBlack.ContainsKey(PieceType.K))
                {
                    sb.Append(this.motiBlack[PieceType.K]);
                    sb.Append("K");
                }
                //飛車
                else if (this.motiBlack.ContainsKey(PieceType.R))
                {
                    sb.Append(this.motiBlack[PieceType.R]);
                    sb.Append("R");
                }
                //角
                else if (this.motiBlack.ContainsKey(PieceType.B))
                {
                    sb.Append(this.motiBlack[PieceType.B]);
                    sb.Append("B");
                }
                //金
                else if (this.motiBlack.ContainsKey(PieceType.G))
                {
                    sb.Append(this.motiBlack[PieceType.G]);
                    sb.Append("G");
                }
                //銀
                else if (this.motiBlack.ContainsKey(PieceType.S))
                {
                    sb.Append(this.motiBlack[PieceType.S]);
                    sb.Append("S");
                }
                //桂馬
                else if (this.motiBlack.ContainsKey(PieceType.N))
                {
                    sb.Append(this.motiBlack[PieceType.N]);
                    sb.Append("N");
                }
                //香車
                else if (this.motiBlack.ContainsKey(PieceType.L))
                {
                    sb.Append(this.motiBlack[PieceType.L]);
                    sb.Append("L");
                }
                //歩
                else if (this.motiBlack.ContainsKey(PieceType.P))
                {
                    sb.Append(this.motiBlack[PieceType.P]);
                    sb.Append("P");
                }

                // 後手持ち駒
                //王
                if (this.motiWhite.ContainsKey(PieceType.K))
                {
                    sb.Append(this.motiBlack[PieceType.K]);
                    sb.Append("k");
                }
                //飛車
                else if (this.motiWhite.ContainsKey(PieceType.R))
                {
                    sb.Append(this.motiBlack[PieceType.R]);
                    sb.Append("r");
                }
                //角
                else if (this.motiWhite.ContainsKey(PieceType.B))
                {
                    sb.Append(this.motiBlack[PieceType.B]);
                    sb.Append("b");
                }
                //金
                else if (this.motiWhite.ContainsKey(PieceType.G))
                {
                    sb.Append(this.motiBlack[PieceType.G]);
                    sb.Append("g");
                }
                //銀
                else if (this.motiWhite.ContainsKey(PieceType.S))
                {
                    sb.Append(this.motiBlack[PieceType.S]);
                    sb.Append("s");
                }
                //桂馬
                else if (this.motiWhite.ContainsKey(PieceType.N))
                {
                    sb.Append(this.motiBlack[PieceType.N]);
                    sb.Append("n");
                }
                //香車
                else if (this.motiWhite.ContainsKey(PieceType.L))
                {
                    sb.Append(this.motiBlack[PieceType.L]);
                    sb.Append("l");
                }
                //歩
                else if (this.motiWhite.ContainsKey(PieceType.P))
                {
                    sb.Append(this.motiBlack[PieceType.P]);
                    sb.Append("p");
                }
            }

            // 1固定
            sb.Append(" 1");


            return sb.ToString();
        }


    }
}
