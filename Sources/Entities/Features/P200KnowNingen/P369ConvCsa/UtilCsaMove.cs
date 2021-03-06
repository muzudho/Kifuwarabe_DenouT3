﻿using System.Diagnostics;
using System.Text;

namespace Grayscale.Kifuwarakaku.Entities.Features
{
    public abstract class UtilCsaMove
    {

        /// <summary>
        /// CSA符号→元位置
        /// </summary>
        /// <param name="csa"></param>
        /// <returns></returns>
        public static SyElement ToSrcMasu(CsaKifuMove csa)
        {
            int suji;
            int dan;
            int.TryParse(csa.SourceMasu[0].ToString(), out suji);
            int.TryParse(csa.SourceMasu[1].ToString(), out dan);

            return Util_Masu10.OkibaSujiDanToMasu(Okiba.ShogiBan, suji, dan);
        }

        /// <summary>
        /// CSA符号→先位置
        /// </summary>
        /// <param name="csa"></param>
        /// <returns></returns>
        public static SyElement ToDstMasu(CsaKifuMove csa)
        {
            int suji;
            int dan;
            int.TryParse(csa.DestinationMasu[0].ToString(), out suji);
            int.TryParse(csa.DestinationMasu[1].ToString(), out dan);

            return Util_Masu10.OkibaSujiDanToMasu(Okiba.ShogiBan, suji, dan);
        }


        /// <summary>
        /// CSA符号→先位置での駒種類
        /// </summary>
        /// <param name="csa"></param>
        /// <returns></returns>
        public static PieceType ToKomasyurui(CsaKifuMove csa)
        {
            PieceType result_kifuwarabe;

            switch (csa.Syurui)
            {
                case Word_Csa.FU_FU_____: result_kifuwarabe = PieceType.P; break;
                case Word_Csa.KY_KYO____: result_kifuwarabe = PieceType.L; break;
                case Word_Csa.KE_KEI____: result_kifuwarabe = PieceType.N; break;
                case Word_Csa.GI_GIN____: result_kifuwarabe = PieceType.S; break;
                case Word_Csa.KI_KIN____: result_kifuwarabe = PieceType.G; break;
                case Word_Csa.KA_KAKU___: result_kifuwarabe = PieceType.B; break;
                case Word_Csa.HI_HISYA__: result_kifuwarabe = PieceType.R; break;
                case Word_Csa.OU_OU_____: result_kifuwarabe = PieceType.K; break;
                case Word_Csa.TO_TOKIN__: result_kifuwarabe = PieceType.PP; break;
                case Word_Csa.NY_NARIKYO: result_kifuwarabe = PieceType.PL; break;
                case Word_Csa.NK_NARIKEI: result_kifuwarabe = PieceType.PN; break;
                case Word_Csa.NG_NARIGIN: result_kifuwarabe = PieceType.PS; break;
                case Word_Csa.UM_UMA____: result_kifuwarabe = PieceType.PB; break;
                case Word_Csa.RY_RYU____: result_kifuwarabe = PieceType.PR; break;
                default: result_kifuwarabe = PieceType.None; break;
            }

            return result_kifuwarabe;
        }

        /// <summary>
        /// CSA符号→プレイヤーサイド
        /// </summary>
        /// <param name="csa"></param>
        /// <returns></returns>
        public static Playerside ToPside(CsaKifuMove csa)
        {
            Playerside result;

            switch (csa.Sengo)
            {
                case "+": result = Playerside.P1; break;
                case "-": result = Playerside.P2; break;
                default: result = Playerside.Empty; break;
            }

            return result;
        }

        /// <summary>
        /// CSAの指し手を、SFENの指し手に変換します。
        /// </summary>
        /// <param name="csa"></param>
        /// <param name="ittemae_Sky">1手前の局面。ルート局面などの理由で１手前の局面がない場合はヌル。</param>
        /// <returns></returns>
        public static string ToSfen(CsaKifuMove csa, SkyConst ittemae_Sky_orNull)
        {
            StringBuilder sb = new StringBuilder();

            int dstSuji;
            int.TryParse(csa.DestinationMasu[0].ToString(), out dstSuji);

            string dstDan = Conv_Suji.ToAlphabet(csa.DestinationMasu[1]);

            // 元位置の筋と段は、あとで必ず使う。（成りの判定）
            int srcSuji;
            int.TryParse(csa.SourceMasu[0].ToString(), out srcSuji);
            int srcDan;
            int.TryParse(csa.SourceMasu[1].ToString(), out srcDan);

            if ("00" == csa.SourceMasu)
            {
                // 打

                string syurui;
                switch (csa.Syurui)
                {
                    case Word_Csa.FU_FU_____: syurui = SfenWord.P_PAWN__; break;
                    case Word_Csa.KY_KYO____: syurui = SfenWord.L_LANCE_; break;
                    case Word_Csa.KE_KEI____: syurui = SfenWord.N_KNIGHT; break;
                    case Word_Csa.GI_GIN____: syurui = SfenWord.S_SILVER; break;
                    case Word_Csa.KI_KIN____: syurui = SfenWord.G_GOLD__; break;
                    case Word_Csa.KA_KAKU___: syurui = SfenWord.B_BISHOP; break;
                    case Word_Csa.HI_HISYA__: syurui = SfenWord.R_ROOK__; break;
                    case Word_Csa.OU_OU_____: syurui = SfenWord.K_KING__; break;//おまけ
                    default: syurui = SfenWord.ERROR___; break;//エラー
                }

                sb.Append(syurui);
                sb.Append("*");
            }
            else
            {
                string srcDan_alphabet = Conv_Suji.ToAlphabet(csa.SourceMasu[1]);
                sb.Append(srcSuji);
                sb.Append(srcDan_alphabet);
            }




            sb.Append(dstSuji);
            sb.Append(dstDan);

            bool nari = false;
            switch (csa.Syurui)
            {
                case Word_Csa.TO_TOKIN__: nari = true; break;
                case Word_Csa.NY_NARIKYO: nari = true; break;
                case Word_Csa.NK_NARIKEI: nari = true; break;
                case Word_Csa.NG_NARIGIN: nari = true; break;
                case Word_Csa.UM_UMA____: nari = true; break;
                case Word_Csa.RY_RYU____: nari = true; break;
            }

            //
            // 「成り」をしたのかどうかを、調べます。
            //
            {
                if (null != ittemae_Sky_orNull && "00" != csa.SourceMasu)
                {
                    // ルート局面ではなく、かつ、打ではないとき。

                    //ittemae_Sky_orNull.Foreach_Starlights((Finger finger, Starlight light, ref bool toBreak) =>
                    //{
                    //    RO_Star_Koma koma = Util_Starlightable.AsKoma(light.Now);
                    //    Logger.Trace($"[{finger}] {koma.Masu.Word}"　"{koma.Pside}"　"{KomaSyurui14Array.Ichimoji[(int)koma.Syurui]}");
                    //});

                    SyElement srcMasu = Util_Masu10.OkibaSujiDanToMasu(Okiba.ShogiBan, srcSuji, srcDan);
                    RO_Star srcKoma = Util_Sky_KomaQuery.InMasuNow(ittemae_Sky_orNull, srcMasu);
                    Debug.Assert(null != srcKoma, "元位置の駒を取得できなかった。1");

                    if (!Util_Komasyurui14.IsNari(srcKoma.Komasyurui) && nari)//移動元で「成り」でなかった駒が、移動後に「成駒」になっていた場合。
                    {
                        sb.Append("+");
                    }
                }
            }

            return sb.ToString();
        }

    }
}
