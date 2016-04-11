using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P145_SfenStruct_.L___250_Struct;
using Grayscale.P145_SfenStruct_.L250____Struct;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P213_Komasyurui_.L500____Util;
using Grayscale.P214_Masu_______.L500____Util;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P234_Komahaiyaku.L500____Util;
using Grayscale.P238_Seiza______.L250____Struct;
using Grayscale.P238_Seiza______.L500____Util;
using Grayscale.P258_UtilSky258_.L500____UtilSky;
using Grayscale.P324_KifuTree___.L___250_Struct;
using System.Text;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P339_ConvKyokume.L500____Converter
{
    public abstract class Conv_KifuNode
    {
        /// <summary>
        /// 表形式の局面データを出力します。SFENとの親和性高め。
        /// </summary>
        /// <param name="errH"></param>
        /// <returns></returns>
        public static RO_Kyokumen1_ForFormat ToRO_Kyokumen1(KifuNode kifuNode, KwErrorHandler errH)
        {
            RO_Kyokumen1_ForFormat ro_Kyokumen1 = new RO_Kyokumen1_ForFormatImpl();

            SkyConst src_Sky = kifuNode.Value.KyokumenConst;

            // 将棋盤
            for (int suji = 1; suji < 10; suji++)
            {
                for (int dan = 1; dan < 10; dan++)
                {
                    Finger koma0 = Util_Sky_FingersQuery.InMasuNow(
                        src_Sky, Util_Masu10.OkibaSujiDanToMasu(Okiba.ShogiBan, suji, dan)
                        ).ToFirst();

                    if (Fingers.Error_1 != koma0)
                    {
                        RO_Star koma1 = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(koma0).Now);

                        ro_Kyokumen1.Ban[suji,dan] = Util_Komasyurui14.SfenText(
                            Util_Komahaiyaku184.Syurui(koma1.Haiyaku),
                            koma1.Pside
                            );
                    }
                }
            }

            // 持ち駒
            int mK = 0;
            int mR = 0;
            int mB = 0;
            int mG = 0;
            int mS = 0;
            int mN = 0;
            int mL = 0;
            int mP = 0;

            int mk = 0;
            int mr = 0;
            int mb = 0;
            int mg = 0;
            int ms = 0;
            int mn = 0;
            int ml = 0;
            int mp = 0;
            Util_Sky_CountQuery.CountMoti(
                src_Sky,
                out mK,
                out mR,
                out mB,
                out mG,
                out mS,
                out mN,
                out mL,
                out mP,

                out mk,
                out mr,
                out mb,
                out mg,
                out ms,
                out mn,
                out ml,
                out mp,
                errH
                );

            int player;
            player = 1;
            ro_Kyokumen1.Moti[player, 0] = mR;
            ro_Kyokumen1.Moti[player, 1] = mB;
            ro_Kyokumen1.Moti[player, 2] = mG;
            ro_Kyokumen1.Moti[player, 3] = mS;
            ro_Kyokumen1.Moti[player, 4] = mN;
            ro_Kyokumen1.Moti[player, 5] = mL;
            ro_Kyokumen1.Moti[player, 6] = mP;

            player = 2;
            ro_Kyokumen1.Moti[player, 0] = mr;
            ro_Kyokumen1.Moti[player, 1] = mb;
            ro_Kyokumen1.Moti[player, 2] = mg;
            ro_Kyokumen1.Moti[player, 3] = ms;
            ro_Kyokumen1.Moti[player, 4] = mn;
            ro_Kyokumen1.Moti[player, 5] = ml;
            ro_Kyokumen1.Moti[player, 6] = mp;

            // 手目済み
            ro_Kyokumen1.Temezumi = src_Sky.Temezumi;

            return ro_Kyokumen1;
        }


        /// <summary>
        /// 局面データから、SFEN文字列を作ります。
        /// </summary>
        /// <param name="pside"></param>
        /// <param name="errH"></param>
        /// <returns></returns>
        public static string ToSfenstring(KifuNode kifuNode, Playerside pside, KwErrorHandler errH)
        {
            SkyConst src_Sky = kifuNode.Value.KyokumenConst;

            StringBuilder sb = new StringBuilder();
            sb.Append("sfen ");

            for (int dan = 1; dan <= 9; dan++)
            {
                int spaceCount = 0;

                for (int suji = 9; suji >= 1; suji--)
                {
                    // 将棋盤上のどこかにある駒？
                    Finger koma0 = Util_Sky_FingersQuery.InMasuNow(
                        src_Sky, Util_Masu10.OkibaSujiDanToMasu(Okiba.ShogiBan, suji, dan)
                        ).ToFirst();

                    if (Fingers.Error_1 != koma0)
                    {
                        if (0 < spaceCount)
                        {
                            sb.Append(spaceCount);
                            spaceCount = 0;
                        }


                        RO_Star koma1 = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(koma0).Now);



                        sb.Append(Util_Komasyurui14.SfenText(
                            Util_Komahaiyaku184.Syurui(koma1.Haiyaku),
                            koma1.Pside
                            ));
                    }
                    else
                    {
                        spaceCount++;
                    }

                }

                if (0 < spaceCount)
                {
                    sb.Append(spaceCount);
                    spaceCount = 0;
                }

                if (dan != 9)
                {
                    sb.Append("/");
                }
            }

            sb.Append(" ");

            //------------------------------------------------------------
            // 先後
            //------------------------------------------------------------
            switch (pside)
            {
                case Playerside.P2:
                    sb.Append("w");
                    break;
                default:
                    sb.Append("b");
                    break;
            }

            sb.Append(" ");

            //------------------------------------------------------------
            // 持ち駒
            //------------------------------------------------------------
            {
                int mK = 0;
                int mR = 0;
                int mB = 0;
                int mG = 0;
                int mS = 0;
                int mN = 0;
                int mL = 0;
                int mP = 0;

                int mk = 0;
                int mr = 0;
                int mb = 0;
                int mg = 0;
                int ms = 0;
                int mn = 0;
                int ml = 0;
                int mp = 0;
                Util_Sky_CountQuery.CountMoti(
                    src_Sky,
                    out mK,
                    out mR,
                    out mB,
                    out mG,
                    out mS,
                    out mN,
                    out mL,
                    out mP,

                    out mk,
                    out mr,
                    out mb,
                    out mg,
                    out ms,
                    out mn,
                    out ml,
                    out mp,
                    errH
                    );



                if (0 == mK + mR + mB + mG + mS + mN + mL + mP + mk + mr + mb + mg + ms + mn + ml + mp)
                {
                    sb.Append("-");
                }
                else
                {
                    if (0 < mK)
                    {
                        if (1 < mK)
                        {
                            sb.Append(mK);
                        }
                        sb.Append("K");
                    }

                    if (0 < mR)
                    {
                        if (1 < mR)
                        {
                            sb.Append(mR);
                        }
                        sb.Append("R");
                    }

                    if (0 < mB)
                    {
                        if (1 < mB)
                        {
                            sb.Append(mB);
                        }
                        sb.Append("B");
                    }

                    if (0 < mG)
                    {
                        if (1 < mG)
                        {
                            sb.Append(mG);
                        }
                        sb.Append("G");
                    }

                    if (0 < mS)
                    {
                        if (1 < mS)
                        {
                            sb.Append(mS);
                        }
                        sb.Append("S");
                    }

                    if (0 < mN)
                    {
                        if (1 < mN)
                        {
                            sb.Append(mN);
                        }
                        sb.Append("N");
                    }

                    if (0 < mL)
                    {
                        if (1 < mL)
                        {
                            sb.Append(mL);
                        }
                        sb.Append("L");
                    }

                    if (0 < mP)
                    {
                        if (1 < mP)
                        {
                            sb.Append(mP);
                        }
                        sb.Append("P");
                    }

                    if (0 < mk)
                    {
                        if (1 < mk)
                        {
                            sb.Append(mk);
                        }
                        sb.Append("k");
                    }

                    if (0 < mr)
                    {
                        if (1 < mr)
                        {
                            sb.Append(mr);
                        }
                        sb.Append("r");
                    }

                    if (0 < mb)
                    {
                        if (1 < mb)
                        {
                            sb.Append(mb);
                        }
                        sb.Append("b");
                    }

                    if (0 < mg)
                    {
                        if (1 < mg)
                        {
                            sb.Append(mg);
                        }
                        sb.Append("g");
                    }

                    if (0 < ms)
                    {
                        if (1 < ms)
                        {
                            sb.Append(ms);
                        }
                        sb.Append("s");
                    }

                    if (0 < mn)
                    {
                        if (1 < mn)
                        {
                            sb.Append(mn);
                        }
                        sb.Append("n");
                    }

                    if (0 < ml)
                    {
                        if (1 < ml)
                        {
                            sb.Append(ml);
                        }
                        sb.Append("l");
                    }

                    if (0 < mp)
                    {
                        if (1 < mp)
                        {
                            sb.Append(mp);
                        }
                        sb.Append("p");
                    }
                }

            }

            // 手目
            sb.Append(" 1");

            return sb.ToString();
        }
    }
}
