using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P324_KifuTree___.L___250_Struct;
using System;
using System.Text.RegularExpressions;

namespace Grayscale.P341_Ittesasu___.L500____UtilA
{
    public abstract class Conv_JsaFugoText
    {
        /// <summary>
        /// ************************************************************************************************************************
        /// テキスト形式の符号「▲７六歩△３四歩▲２六歩…」の最初の要素を、切り取ってプロセスに変換します。
        /// ************************************************************************************************************************
        /// 
        /// [再生]、[コマ送り]で利用。
        /// </summary>
        /// <returns></returns>
        public static bool ToTokens(
            string inputLine,
            out string str1,
            out string str2,
            out string str3,
            out string str4,
            out string str5,
            out string str6,
            out string str7,
            out string str8,
            out string str9,
            out string rest,
            KifuTree kifu,
            KwErrorHandler errH
            )
        {
            //nextTe = null;
            bool successful = false;
            rest = inputLine;

            str1 = "";
            str2 = "";
            str3 = "";
            str4 = "";
            str5 = "";
            str6 = "";
            str7 = "";
            str8 = "";
            str9 = "";

            try
            {
                //------------------------------------------------------------
                // リスト作成
                //------------------------------------------------------------
                Regex regex = new Regex(
                    @"^\s*([▲△]?)(?:([123456789１２３４５６７８９])([123456789１２３４５６７８９一二三四五六七八九]))?(同)?[\s　]*(歩|香|桂|銀|金|飛|角|王|玉|と|成香|成桂|成銀|竜|龍|馬)(右|左|直)?(寄|引|上)?(成|不成)?(打?)",
                    RegexOptions.Singleline
                );

                MatchCollection mc = regex.Matches(inputLine);
                foreach (Match m in mc)
                {
                    if (0 < m.Groups.Count)
                    {
                        successful = true;

                        // 残りのテキスト
                        rest = inputLine.Substring(0, m.Index) + inputLine.Substring(m.Index + m.Length, inputLine.Length - (m.Index + m.Length));

                        str1 = m.Groups[1].Value;
                        str2 = m.Groups[2].Value;
                        str3 = m.Groups[3].Value;
                        str4 = m.Groups[4].Value;
                        str5 = m.Groups[5].Value;
                        str6 = m.Groups[6].Value;
                        str7 = m.Groups[7].Value;
                        str8 = m.Groups[8].Value;
                        str9 = m.Groups[9].Value;
                    }

                    // 最初の１件だけ処理して終わります。
                    break;
                }

                rest = rest.Trim();
            }
            catch (Exception ex)
            {
                // エラーが起こりました。
                //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                // どうにもできないので  ログだけ取って無視します。
                errH.DonimoNaranAkirameta("TuginoItte_JapanFugo.GetData_FromText（A）：" + ex.GetType().Name + "：" + ex.Message + "：text=「" + inputLine + "」");
            }


            return successful;
        }
    }
}
