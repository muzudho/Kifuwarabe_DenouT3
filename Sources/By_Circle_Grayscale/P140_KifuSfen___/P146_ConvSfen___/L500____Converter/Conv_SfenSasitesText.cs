using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P003_Log________.L500____Struct;
using System;
using System.Text.RegularExpressions;


namespace Grayscale.P146_ConvSfen___.L500____Converter
{
    public abstract class Conv_SfenSasitesText
    {
        /// <summary>
        /// ************************************************************************************************************************
        /// テキスト形式の符号「7g7f 3c3d 6g6f…」の最初の１要素を、切り取ってトークンに分解します。
        /// ************************************************************************************************************************
        /// 
        /// [再生]、[コマ送り]で利用。
        /// </summary>
        /// <param name="inputLine"></param>
        /// <param name="moji1"></param>
        /// <param name="moji2"></param>
        /// <param name="moji3"></param>
        /// <param name="moji4"></param>
        /// <param name="moji5"></param>
        /// <param name="rest">残りの文字。</param>
        /// <param name="errH"></param>
        /// <returns></returns>
        public static bool ToTokens(
            string inputLine,
            out string moji1,
            out string moji2,
            out string moji3,
            out string moji4,
            out string moji5,
            out string rest,
            KwErrorHandler errH
            )
        {
            bool successful = false;
            //nextTe = null;
            rest = inputLine;
            moji1 = "";
            moji2 = "";
            moji3 = "";
            moji4 = "";
            moji5 = "";

            //System.C onsole.WriteLine("TuginoItte_Sfen.GetData_FromText:text=[" + text + "]");

            try
            {



                //------------------------------------------------------------
                // リスト作成
                //------------------------------------------------------------
                Regex regex = new Regex(
                    @"^\s*([123456789PLNSGKRB])([abcdefghi\*])([123456789])([abcdefghi])(\+)?",
                    RegexOptions.Singleline
                );

                MatchCollection mc = regex.Matches(inputLine);
                foreach (Match m in mc)
                {

                    try
                    {

                        if (0 < m.Groups.Count)
                        {
                            successful = true;

                            // 残りのテキスト
                            rest = inputLine.Substring(0, m.Index) + inputLine.Substring(m.Index + m.Length, inputLine.Length - (m.Index + m.Length));

                            moji1 = m.Groups[1].Value;
                            moji2 = m.Groups[2].Value;
                            moji3 = m.Groups[3].Value;
                            moji4 = m.Groups[4].Value;
                            moji5 = m.Groups[5].Value;
                        }

                        // 最初の１件だけ処理して終わります。
                        break;
                    }
                    catch (Exception ex) { Util_OwataMinister.ERROR.DonimoNaranAkirameta(ex, "moves解析中☆"); throw ex; }
                }

                rest = rest.Trim();

            }
            catch (Exception ex) { Util_OwataMinister.ERROR.DonimoNaranAkirameta(ex, "moves解析中☆"); throw ex; }

            return successful;
        }
    }
}
