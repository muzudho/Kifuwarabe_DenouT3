using Grayscale.P144_ConvSujiDan.L500____Converter;
using Grayscale.P145_SfenStruct_.L___250_Struct;
using Grayscale.P145_SfenStruct_.L250____Struct;
using Grayscale.P145_SfenStruct_.L500____Util;
using System.Text.RegularExpressions;

namespace Grayscale.P146_ConvSfen___.L500____Converter
{
    public abstract class Conv_Sfenstring146
    {
        /// <summary>
        /// ************************************************************************************************************************
        /// 「lnsgkgsnl/1r5b1/ppppppppp/9/9/9/PPPPPPPPP/1B5R1/LNSGKGSNL w - 1」といった記述を解析します。
        /// ************************************************************************************************************************
        /// </summary>
        /// <returns></returns>
        public static bool ToKyokumen2(
            string inputLine,
            out string rest,
            out RO_Kyokumen2_ForTokenize result_kyokumen2
            )
        {

            // 頭に 「position」 があれば、外します。
            {
                inputLine = inputLine.TrimStart();
                if (inputLine.StartsWith("position"))
                {
                    inputLine = inputLine.Substring("position".Length);
                }
            }

            // 頭に 「startpos」があれば、置き換えます。
            {
                inputLine = inputLine.TrimStart();
                if (inputLine.StartsWith("startpos"))
                {
                    inputLine = inputLine.Substring("startpos".Length);
                }

                inputLine = "sfen lnsgkgsnl/1r5b1/ppppppppp/9/9/9/PPPPPPPPP/1B5R1/LNSGKGSNL w - 1 " + inputLine;
            }


            bool successful = false;
            result_kyokumen2 = null;

            // 1～27
            string[] stra = new string[28];
            //stra[0] = "";//未使用
            for (int i = 1; i < 28; i++)
            {
                stra[i] = "";
            }

            rest = inputLine;

            bool startposPattern = false;

            //------------------------------------------------------------
            // リスト作成
            //------------------------------------------------------------
            if (!startposPattern)
            {
                Regex regex2 = new Regex(
                    @"^\s*" +
                    @"sfen " +
                    @"((?:[123456789]|\+?[KRBGSNLPkrbgsnlp])+)/" +//1段目
                    @"((?:[123456789]|\+?[KRBGSNLPkrbgsnlp])+)/" +//2段目
                    @"((?:[123456789]|\+?[KRBGSNLPkrbgsnlp])+)/" +//3段目
                    @"((?:[123456789]|\+?[KRBGSNLPkrbgsnlp])+)/" +//4段目
                    @"((?:[123456789]|\+?[KRBGSNLPkrbgsnlp])+)/" +//5段目
                    @"((?:[123456789]|\+?[KRBGSNLPkrbgsnlp])+)/" +//6段目
                    @"((?:[123456789]|\+?[KRBGSNLPkrbgsnlp])+)/" +//7段目
                    @"((?:[123456789]|\+?[KRBGSNLPkrbgsnlp])+)/" +//8段目
                    @"((?:[123456789]|\+?[KRBGSNLPkrbgsnlp])+) " +//9段目
                    @"(b|w) " +//先後
                    @"\-?" +//持駒なし
                    // ↓この書き方だと、順序が決まってしまうが。
                    @"(\d*K)?" +//持駒▲王 ※持ち駒が１個だけの場合は、数字が省略されます。
                    @"(\d*R)?" +//持駒▲飛
                    @"(\d*B)?" +//持駒▲角
                    @"(\d*G)?" +//持駒▲金
                    @"(\d*S)?" +//持駒▲銀
                    @"(\d*N)?" +//持駒▲桂
                    @"(\d*L)?" +//持駒▲香
                    @"(\d*P)?" +//持駒▲歩
                    @"(\d*k)?" +//持駒△王
                    @"(\d*r)?" +//持駒△飛
                    @"(\d*b)?" +//持駒△角
                    @"(\d*g)?" +//持駒△金
                    @"(\d*s)?" +//持駒△銀
                    @"(\d*n)?" +//持駒△桂
                    @"(\d*l)?" +//持駒△香
                    @"(\d*p)?" +//持駒△歩
                    @" (\d+)" +//手目
                    @"",
                    RegexOptions.Singleline
                );
                MatchCollection mc = regex2.Matches(inputLine);
                foreach (Match m in mc)
                {
                    if (0 < m.Groups.Count)
                    {
                        // 残りのテキスト
                        rest = inputLine.Substring(0, m.Index) + inputLine.Substring(m.Index + m.Length, inputLine.Length - (m.Index + m.Length));

                        stra[1] = m.Groups[1].Value;  //1段目
                        stra[2] = m.Groups[2].Value;  //2段目
                        stra[3] = m.Groups[3].Value;  //3段目
                        stra[4] = m.Groups[4].Value;  //4段目
                        stra[5] = m.Groups[5].Value;  //5段目
                        stra[6] = m.Groups[6].Value;  //6段目
                        stra[7] = m.Groups[7].Value;  //7段目
                        stra[8] = m.Groups[8].Value;  //8段目
                        stra[9] = m.Groups[9].Value;  //9段目
                        stra[10] = m.Groups[10].Value;  //先後
                        stra[11] = m.Groups[11].Value;  //持駒▲王
                        stra[12] = m.Groups[12].Value;  //持駒▲飛
                        stra[13] = m.Groups[13].Value;  //持駒▲角
                        stra[14] = m.Groups[14].Value;  //持駒▲金
                        stra[15] = m.Groups[15].Value;  //持駒▲銀
                        stra[16] = m.Groups[16].Value;  //持駒▲桂
                        stra[17] = m.Groups[17].Value;  //持駒▲香
                        stra[18] = m.Groups[18].Value;  //持駒▲歩
                        stra[19] = m.Groups[19].Value;  //持駒△王
                        stra[20] = m.Groups[20].Value;  //持駒△飛
                        stra[21] = m.Groups[21].Value;  //持駒△角
                        stra[22] = m.Groups[22].Value;  //持駒△金
                        stra[23] = m.Groups[23].Value;  //持駒△銀
                        stra[24] = m.Groups[24].Value;  //持駒△桂
                        stra[25] = m.Groups[25].Value;  //持駒△香
                        stra[26] = m.Groups[26].Value;  //持駒△歩
                        stra[27] = m.Groups[27].Value;  //手目

                        if (
                                                !(
                            stra[1] == "" && stra[2] == "" && stra[3] == "" && stra[4] == "" && stra[5] == ""
                            && stra[6] == "" && stra[7] == "" && stra[8] == "" && stra[9] == "" && stra[10] == ""
                            && stra[11] == "" && stra[12] == "" && stra[13] == "" && stra[14] == "" && stra[15] == ""
                            && stra[16] == "" && stra[17] == "" && stra[18] == "" && stra[19] == "" && stra[20] == ""
                            && stra[21] == "" && stra[22] == "" && stra[23] == "" && stra[24] == "" && stra[25] == ""
                            && stra[26] == "" && stra[27] == ""
                            )

                            )
                        {
                            result_kyokumen2 = Conv_Sfenstring146.ReadString2(
                                stra[1],  //1段目
                                stra[2],  //2段目
                                stra[3],  //3段目
                                stra[4],  //4段目
                                stra[5],  //5段目
                                stra[6],  //6段目
                                stra[7],  //7段目
                                stra[8],  //8段目
                                stra[9],  //9段目
                                stra[10],  //先後
                                stra[11],  //持駒▲王
                                stra[12],  //持駒▲飛
                                stra[13],  //持駒▲角
                                stra[14],  //持駒▲金
                                stra[15],  //持駒▲銀
                                stra[16],  //持駒▲桂
                                stra[17],  //持駒▲香
                                stra[18],  //持駒▲歩
                                stra[19],  //持駒△王
                                stra[20],  //持駒△飛
                                stra[21],  //持駒△角
                                stra[22],  //持駒△金
                                stra[23],  //持駒△銀
                                stra[24],  //持駒△桂
                                stra[25],  //持駒△香
                                stra[26],  //持駒△歩
                                stra[27]  //手目
                            );
                            successful = true;
                        }
                    }

                    // 最初の１件だけ処理して終わります。
                    break;
                }
            }


            rest = rest.Trim();

            //// 解析失敗時用
            //{
            //    StringBuilder sb = new StringBuilder();
            //    sb.Append("解析失敗☆ inputLine=[" + inputLine + "]");

            //    for (int i = 1; i < 28; i++)
            //    {
            //        sb.Append("str" + i + "[" + stra[i] + "]");
            //    }

            //    Debug.Assert(result_Sfenstring != null, sb.ToString());

            //    Util_SfenstringReader.Assert_Koma40(result_Sfenstring, "inputLine=[" + inputLine + "]");
            //}

            return successful;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// インスタンスを作成
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="dan1"></param>
        /// <param name="dan2"></param>
        /// <param name="dan3"></param>
        /// <param name="dan4"></param>
        /// <param name="dan5"></param>
        /// <param name="dan6"></param>
        /// <param name="dan7"></param>
        /// <param name="dan8"></param>
        /// <param name="dan9"></param>
        /// <param name="pside_Str"></param>
        /// <param name="mK_Str"></param>
        /// <param name="mR_Str"></param>
        /// <param name="mB_Str"></param>
        /// <param name="mG_Str"></param>
        /// <param name="mS_Str"></param>
        /// <param name="mN_Str"></param>
        /// <param name="mL_Str"></param>
        /// <param name="mP_Str"></param>
        /// <param name="mk_Str"></param>
        /// <param name="mr_Str"></param>
        /// <param name="mb_Str"></param>
        /// <param name="mg_Str"></param>
        /// <param name="ms_Str"></param>
        /// <param name="mn_Str"></param>
        /// <param name="ml_Str"></param>
        /// <param name="mp_Str"></param>
        /// <param name="temezumi_Str"></param>
        private static RO_Kyokumen2_ForTokenize ReadString2(
            string dan1,  //1段目
            string dan2,//2段目
            string dan3,//3段目
            string dan4,//4段目
            string dan5,//5段目
            string dan6,  //6段目
            string dan7,  //7段目
            string dan8,  //8段目
            string dan9,  //9段目
            string pside_Str,  //先後
            string mK_Str,  //持駒▲王
            string mR_Str,  //持駒▲飛
            string mB_Str,  //持駒▲角
            string mG_Str,  //持駒▲金
            string mS_Str,  //持駒▲銀
            string mN_Str,  //持駒▲桂
            string mL_Str,  //持駒▲香
            string mP_Str,  //持駒▲歩
            string mk_Str,  //持駒△王
            string mr_Str,  //持駒△飛
            string mb_Str,  //持駒△角
            string mg_Str,  //持駒△金
            string ms_Str,  //持駒△銀
            string mn_Str,  //持駒△桂
            string ml_Str,  //持駒△香
            string mp_Str,  //持駒△歩
            string temezumi_Str  //手目済
            )
        {
            int bK = 0;  //盤上 王
            int bR = 0;  //盤上 飛
            int bB = 0;  //盤上 角
            int bG = 0;  //盤上 金
            int bS = 0;  //盤上 銀
            int bN = 0;  //盤上 桂
            int bL = 0;  //盤上 香
            int bP = 0;  //盤上 歩
            int mK = Conv_SfenMotigomaToken.CountMaisu(mK_Str);  //持駒▲王
            int mR = Conv_SfenMotigomaToken.CountMaisu(mR_Str);  //持駒▲飛
            int mB = Conv_SfenMotigomaToken.CountMaisu(mB_Str);  //持駒▲角
            int mG = Conv_SfenMotigomaToken.CountMaisu(mG_Str);  //持駒▲金
            int mS = Conv_SfenMotigomaToken.CountMaisu(mS_Str);  //持駒▲銀
            int mN = Conv_SfenMotigomaToken.CountMaisu(mN_Str);  //持駒▲桂
            int mL = Conv_SfenMotigomaToken.CountMaisu(mL_Str);  //持駒▲香
            int mP = Conv_SfenMotigomaToken.CountMaisu(mP_Str);  //持駒▲歩
            int mk = Conv_SfenMotigomaToken.CountMaisu(mk_Str);  //持駒△王
            int mr = Conv_SfenMotigomaToken.CountMaisu(mr_Str);  //持駒△飛
            int mb = Conv_SfenMotigomaToken.CountMaisu(mb_Str);  //持駒△角
            int mg = Conv_SfenMotigomaToken.CountMaisu(mg_Str);  //持駒△金
            int ms = Conv_SfenMotigomaToken.CountMaisu(ms_Str);  //持駒△銀
            int mn = Conv_SfenMotigomaToken.CountMaisu(mn_Str);  //持駒△桂
            int ml = Conv_SfenMotigomaToken.CountMaisu(ml_Str);  //持駒△香
            int mp = Conv_SfenMotigomaToken.CountMaisu(mp_Str);  //持駒△歩


            string[] strDanArr = new string[]{
                "",
                dan1,
                dan2,
                dan3,
                dan4,
                dan5,
                dan6,
                dan7,
                dan8,
                dan9
            };

            // 盤上の８１升
            string[] masu201;
            {
                masu201 = new string[201];
                for (int i = 0; i < 201; i++)
                {
                    masu201[i] = "";
                }

                int spaceCount;
                //------------------------------
                // 1段目～9段目の順に解析。
                //------------------------------
                for (int dan = 1; dan <= 9; dan++)
                {
                    // 筋は 9 ～ 1 の逆順。
                    int suji = 9;

                    while (suji >= 1)
                    {
                        if (strDanArr[dan].Length < 1)
                        {
                            break;
                        }

                        string moji;
                        moji = strDanArr[dan].Substring(0, 1);
                        if (1 <= strDanArr[dan].Length)
                        {
                            strDanArr[dan] = strDanArr[dan].Substring(1, strDanArr[dan].Length - 1);
                        }
                        else
                        {
                            strDanArr[dan] = "";
                        }

                        if ("+" == moji && 1 <= strDanArr[dan].Length)
                        {
                            // もう１文字、切り取ります。
                            moji = moji + strDanArr[dan].Substring(0, 1);
                            strDanArr[dan] = strDanArr[dan].Substring(1, strDanArr[dan].Length - 1);
                        }


                        if (int.TryParse(moji, out spaceCount))
                        {
                            // スペースの個数です。
                            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                            suji -= spaceCount;
                        }
                        else
                        {
                            // 駒でした。
                            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                            switch (moji)
                            {
                                case "K": bK++; break;
                                case "R": bR++; break;
                                case "B": bB++; break;
                                case "G": bG++; break;
                                case "S": bS++; break;
                                case "N": bN++; break;
                                case "L": bL++; break;
                                case "P": bP++; break;

                                case "+R": bR++; break;
                                case "+B": bB++; break;
                                case "+S": bS++; break;
                                case "+N": bN++; break;
                                case "+L": bL++; break;
                                case "+P": bP++; break;

                                case "k": bK++; break;
                                case "r": bR++; break;
                                case "b": bB++; break;
                                case "g": bG++; break;
                                case "s": bS++; break;
                                case "n": bN++; break;
                                case "l": bL++; break;
                                case "p": bP++; break;

                                case "+r": bR++; break;
                                case "+b": bB++; break;
                                case "+s": bS++; break;
                                case "+n": bN++; break;
                                case "+l": bL++; break;
                                case "+p": bP++; break;
                            }

                            masu201[Conv_SujiDan.ToMasu(suji, dan)] = moji;

                            suji--;
                        }
                    }
                }
            }

            // 駒袋の中に残っている駒の数を数えます。
            int fK = 2 - bK - mK - mk;
            int fR = 2 - bR - mR - mr; // 将棋盤上の駒の数も数えないと☆
            int fB = 2 - bB - mB - mb;
            int fG = 4 - bG - mG - mg;
            int fS = 4 - bS - mS - ms;
            int fN = 4 - bN - mN - mn;
            int fL = 4 - bL - mL - ml;
            int fP = 18 - bP - mP - mp;


            // 盤外
            {
                int iMasu;

                // 持ち駒

                iMasu = 81;// (int)Masu_Honshogi.nsen01;
                for (int i = 0; i < mK; i++) { masu201[(int)iMasu] = "K"; iMasu++; }//▲王
                for (int i = 0; i < mR; i++) { masu201[(int)iMasu] = "R"; iMasu++; }//▲飛
                for (int i = 0; i < mB; i++) { masu201[(int)iMasu] = "B"; iMasu++; }//▲角
                for (int i = 0; i < mG; i++) { masu201[(int)iMasu] = "G"; iMasu++; }//▲金
                for (int i = 0; i < mS; i++) { masu201[(int)iMasu] = "S"; iMasu++; }//▲銀
                for (int i = 0; i < mN; i++) { masu201[(int)iMasu] = "N"; iMasu++; }//▲桂
                for (int i = 0; i < mL; i++) { masu201[(int)iMasu] = "L"; iMasu++; }//▲香
                for (int i = 0; i < mP; i++) { masu201[(int)iMasu] = "P"; iMasu++; }//▲歩

                iMasu = 121;// (int)Masu_Honshogi.ngo01;
                for (int i = 0; i < mk; i++) { masu201[(int)iMasu] = "k"; iMasu++; }//△王
                for (int i = 0; i < mr; i++) { masu201[(int)iMasu] = "r"; iMasu++; }//△飛
                for (int i = 0; i < mb; i++) { masu201[(int)iMasu] = "b"; iMasu++; }//△角
                for (int i = 0; i < mg; i++) { masu201[(int)iMasu] = "g"; iMasu++; }//△金
                for (int i = 0; i < ms; i++) { masu201[(int)iMasu] = "s"; iMasu++; }//△銀
                for (int i = 0; i < mn; i++) { masu201[(int)iMasu] = "n"; iMasu++; }//△桂
                for (int i = 0; i < ml; i++) { masu201[(int)iMasu] = "l"; iMasu++; }//△香
                for (int i = 0; i < mp; i++) { masu201[(int)iMasu] = "p"; iMasu++; }//△歩

                iMasu = 161;// (int)Masu_Honshogi.nfukuro01;
                for (int i = 0; i < fP; i++) { masu201[(int)iMasu] = "P"; iMasu++; }//駒袋 歩
                for (int i = 0; i < fL; i++) { masu201[(int)iMasu] = "L"; iMasu++; }//駒袋 香
                for (int i = 0; i < fN; i++) { masu201[(int)iMasu] = "N"; iMasu++; }//駒袋 桂
                for (int i = 0; i < fS; i++) { masu201[(int)iMasu] = "S"; iMasu++; }//駒袋 銀
                for (int i = 0; i < fG; i++) { masu201[(int)iMasu] = "G"; iMasu++; }//駒袋 金
                for (int i = 0; i < fK; i++) { masu201[(int)iMasu] = "K"; iMasu++; }//駒袋 王
                for (int i = 0; i < fR; i++) { masu201[(int)iMasu] = "R"; iMasu++; }//駒袋 飛
                for (int i = 0; i < fB; i++) { masu201[(int)iMasu] = "B"; iMasu++; }//駒袋 角
            }


            RO_Kyokumen2_ForTokenize result = new RO_Kyokumen2_ForTokenizeImpl(
                masu201,//全升

                mK,//持駒▲王
                mR,//持駒▲飛
                mB,//持駒▲角
                mG,//持駒▲金
                mS,//持駒▲銀
                mN,//持駒▲桂
                mL,//持駒▲香
                mP,//持駒▲歩
                mk,//持駒△王
                mr,//持駒△飛
                mb,//持駒△角
                mg,//持駒△金
                ms,//持駒△銀
                mn,//持駒△桂
                ml,//持駒△香
                mp,//持駒△歩

                fK,//駒袋 王
                fR,//駒袋 飛
                fB,//駒袋 角
                fG,//駒袋 金
                fS,//駒袋 銀
                fN,//駒袋 桂
                fL,//駒袋 香
                fP,//駒袋 歩

                pside_Str,
                temezumi_Str  //手目
            );

            Util_RO_Kyokumen2.Assert_Koma40(result,
                " dan1=[" + dan1 + "]\n"
                + " dan2=[" + dan2 + "]\n"
                + " dan3=[" + dan3 + "]\n"
                + " dan4=[" + dan4 + "]\n"
                + " dan5=[" + dan5 + "]\n"
                + " dan6=[" + dan6 + "]\n"
                + " dan7=[" + dan7 + "]\n"
                + " dan8=[" + dan8 + "]\n"
                + " dan9=[" + dan9 + "]\n"

                + " strPside=[" + pside_Str + "]\n"

                + " K=[" + mK_Str + "]"
                + " R=[" + mR_Str + "]"
                + " B=[" + mB_Str + "]"
                + " G=[" + mG_Str + "]"
                + " S=[" + mS_Str + "]"
                + " N=[" + mN_Str + "]"
                + " L=[" + mL_Str + "]"
                + " P=[" + mP_Str + "]\n"

                + " k=[" + mk_Str + "]"
                + " r=[" + mr_Str + "]"
                + " b=[" + mb_Str + "]"
                + " g=[" + mg_Str + "]"
                + " s=[" + ms_Str + "]"
                + " n=[" + mn_Str + "]"
                + " l=[" + ml_Str + "]"
                + " p=[" + mp_Str + "]\n"

                + " temezumi_Str=[" + temezumi_Str + "]\n"
                );

            return result;
        }

    }
}
