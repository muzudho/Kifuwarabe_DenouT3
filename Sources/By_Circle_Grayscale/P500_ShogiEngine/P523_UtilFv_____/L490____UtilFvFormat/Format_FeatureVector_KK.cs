using Grayscale.P062_ConvText___.L500____Converter;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P214_Masu_______.L500____Util;
using Grayscale.P521_FeatureVect.L___500_Struct;
using Grayscale.P522_Conv_FvKoumoku.L500____Converter;
using System;
using System.Text;

namespace Grayscale.P523_UtilFv_____.L490____UtilFvFormat
{
    /// <summary>
    /// TODO:
    /// </summary>
    public abstract class Format_FeatureVector_KK
    {
        /// <summary>
        /// テキストを作ります。
        /// </summary>
        /// <param name="fv"></param>
        /// <returns></returns>
        public static string Format_KK(FeatureVector fv)
        {
            StringBuilder sb = new StringBuilder();

            //
            // コメント
            //
            sb.AppendLine("\"#紹介文\",");
            sb.AppendLine("\"#\",");
            sb.AppendLine("\"#ボナンザ６．０アレンジ式きふわらべ２駒関係\",");
            sb.AppendLine("\"#\",");
            sb.AppendLine("\"#----------------------------------------\",");
            sb.AppendLine("\"#ＫＫ表☆\",");
            sb.AppendLine("\"#----------------------------------------\",");
            sb.AppendLine("\"#\",");
            sb.AppendLine("\"#常に先手は正の数、後手は負の数の絶対値が大きい方が有利。０は互角。\",");

            //
            // 仕様バージョン
            //
            sb.AppendLine();
            sb.AppendLine("\"Version\",1.0,");
            sb.AppendLine();
            //----------------------------------------
            // プレイヤー1のK が 一段～九段
            //----------------------------------------
            for (int k1dan = 1; k1dan < 10; k1dan++)
            {
                // コメント行を作ります。
                sb.Append("\"#KK");//4文字
                string danStr = Conv_Int.ToKanSuji(k1dan);
                for (int suji = 9; suji > 0; suji--)
                {
                    string sujiStr = Conv_Int.ToArabiaSuji(suji);
                    sb.Append(" ");
                    sb.Append(sujiStr);
                    sb.Append(danStr);
                    sb.Append("1P玉 vs2P玉   ");//15文字
                    for (int col = 0; col < 5; col++)
                    {
                        sb.Append("     ");//5文字
                    }
                    sb.Append("     ");//5文字

                    if (Const_FeatureVectorFormat.PARAMETER_INDEX_OUTPUT)
                    {
                        sb.Append("                                             ");//調整
                    }
                }
                sb.AppendLine("\",");

                //----------------------------------------
                // プレイヤー2のK が 一段～九段
                //----------------------------------------
                for (int k2dan = 1; k2dan < 10; k2dan++)
                {
                    // 行頭
                    sb.Append("    ");//4文字
                    //----------------------------------------
                    // プレイヤー1のK が ９筋～１筋
                    //----------------------------------------
                    for (int k1suji = 9; k1suji > 0; k1suji--)
                    {
                        int p1;
                        int p2;

                        //----------------------------------------
                        // プレイヤー2のK が ９筋～１筋
                        //----------------------------------------
                        for (int k2suji = 9; k2suji > 0; k2suji--)
                        {
                            int k2masu = Util_Masu10.Handle_OkibaSujiDanToMasu(Okiba.ShogiBan, k2suji, k2dan);

                            Conv_FvKoumoku522.Converter_KK_to_PP(k1dan, k2dan, k1suji, k2suji, out p1, out p2);

                            if (Const_FeatureVectorFormat.PARAMETER_INDEX_OUTPUT)
                            {
                                sb.Append(string.Format("{0,4}_{1,4}", p1, p2));
                            }
                            else
                            {
                                // スコアの倍率を復元します。
                                float scoreF = fv.NikomaKankeiPp_ForMemory[p1, p2] / fv.Bairitu_NikomaKankeiPp;
                                int value = (int)Math.Round(scoreF, 0);//小数点以下を丸めます。
                                sb.Append(string.Format("{0,4}", value));
                            }

                            sb.Append(",");
                        }

                        // 表の横の隙間
                        sb.Append("    ");
                    }
                    // 次の段へ
                    sb.AppendLine();

                }
                // 段の隙間
                sb.AppendLine();
            }

            return sb.ToString();
        }

    }
}
