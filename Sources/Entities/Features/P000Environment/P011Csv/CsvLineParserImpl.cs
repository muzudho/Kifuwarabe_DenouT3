using System.Collections.Generic;
using System.Text;

namespace Grayscale.Kifuwarakaku.Entities.Features
{
    /// <summary>
    /// CSVの１行分のパーサー。
    /// </summary>
    public abstract class CsvLineParserImpl
    {



        #region アクション
        //────────────────────────────────────────

        public static List<string> UnescapeLineToFieldList(string source, char chDelimiter)
        {
            int length = source.Length;
            List<string> list_Destination = new List<string>();
            char ch;

            // 空か。
            if (source.Length < 1)
            {
                goto gt_EndMethod;
            }


            //Logger.Trace($"（１）source[{source}]");

            //１セル分の文字列
            StringBuilder cell = new StringBuilder();
            int index = 0;
            while (index < length)
            {
                cell.Length = 0;
                ch = source[index];

                if (',' == ch)
                {
                    // 空を追加して次へ。
                    index++;
                }
                else if ('"' == ch)
                {
                    // 1文字目が「"」なら、2文字目へ。
                    index++;

                    // エスケープしながら、単独「"」が出てくるまでそのまま出力。
                    while (index < length)
                    {
                        ch = source[index];

                        if ('"' == ch)
                        {
                            // 「"」だった。
                            // ここで文字列終わりなのだが、
                            // しかし次の文字が「"」の場合、まだこの「"」で終わってはいけない。

                            if (index + 1 == length)
                            {
                                // 2文字目が無ければ、
                                //「"」を無視して終了。
                                index++;
                                break;
                            }
                            else if ('"' == source[index + 1])
                            {
                                // 2文字目も「"」なら、
                                // 1,2文字目の「""」を「"」に変換して続行。
                                index += 2;
                                cell.Append('"');
                            }
                            else
                            {
                                // 2文字目が「"」でなければ、
                                //「"」を無視して終了。
                                index += 2;//【改変/】2012年10月30日変更。旧： index++;
                                break;
                            }
                        }
                        else
                        {
                            // 通常文字なので続行。
                            cell.Append(ch);
                            index++;
                        }
                    }
                }
                else
                {
                    cell.Append(ch);
                    index++;

                    // 1文字目が「"」でないなら、「,」が出てくるか、次がなくなるまでそのまま出力。
                    // フォーマットチェックは行わない。
                    while (index < length)
                    {
                        ch = source[index];

                        if (chDelimiter != ch)
                        {
                            // 文字を追加して次へ。
                            cell.Append(ch);
                            index++;
                        }
                        else
                        {
                            // 「,」を見つけたのでこれを無視し、
                            // このセル読取は脱出。
                            index++;
                            break;
                        }
                    }
                    // 次が無くなったか、「,」の次の文字を指している。
                }

                list_Destination.Add(cell.ToString());
            }

        gt_EndMethod:
            return list_Destination;
        }

        //────────────────────────────────────────

        /// <summary>
        /// （１）「,」または「"」が含まれていれば、両端に「"」を付加します。
        /// （２）含まれている「"」は、「""」に変換します。
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string EscapeCell(string source)
        {
            int length = source.Length;

            // エスケープが必要なら真。
            bool isEscape = false;
            char ch;

            StringBuilder s = new StringBuilder();

            for (int index = 0; index < length;)
            {
                ch = source[index];
                if (',' == ch)
                {
                    // エスケープが必要
                    isEscape = true;
                    s.Append(ch);
                    index++;
                }
                else if ('"' == ch)
                {
                    // エスケープが必要
                    isEscape = true;
                    s.Append("\"\"");
                    index++;
                }
                else
                {
                    s.Append(ch);
                    index++;
                }
            }

            if (isEscape)
            {
                s.Insert(0, '"');
                s.Append('"');
            }

            return s.ToString();
        }

        //────────────────────────────────────────
        #endregion



    }
}
