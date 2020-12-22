using System.Text.RegularExpressions;

namespace Grayscale.Kifuwarakaku.Entities.Features
{
    public abstract class SfenMovesTextConv
    {
        static readonly Regex regexOfMove = new Regex(
            @"^\s*([123456789PLNSGKRB])([abcdefghi\*])([123456789])([abcdefghi])(\+)?",
            RegexOptions.Singleline | RegexOptions.Compiled
        );

        /// <summary>
        /// テキスト形式の符号「7g7f 3c3d 6g6f…」の最初の１要素を、切り取ってトークンに分解します。
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
        /// <returns></returns>
        public static bool ToTokens(
            string inputLine,
            out string moji1,
            out string moji2,
            out string moji3,
            out string moji4,
            out string moji5,
            out string rest
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

            //------------------------------------------------------------
            // リスト作成
            //------------------------------------------------------------
            MatchCollection mc = regexOfMove.Matches(inputLine);
            foreach (Match m in mc)
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

            rest = rest.Trim();


            return successful;
        }
    }
}
