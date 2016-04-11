using System.Collections.Generic;
using System.Text;

namespace Grayscale.P011_Csv________.L250____Parser
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
            if(source.Length<1)
            {
                goto gt_EndMethod;
            }


            //ystem.C onsole.WriteLine("（１）source[" + source + "]");

            //１セル分の文字列
            StringBuilder cell = new StringBuilder();
            int index = 0;
            while(index < length)
            {
                cell.Length = 0;
                ch = source[index];

                //ystem.C onsole.WriteLine("（２）index[" + index + "] ch[" + ch + "]");

                if(','==ch)
                {
                    // 空を追加して次へ。
                    index++;

                    //ystem.C onsole.WriteLine("（３）index[" + index + "] ");
                }
                else if ('"' == ch)
                {
                    // 1文字目が「"」なら、2文字目へ。
                    index++;

                    //ystem.C onsole.WriteLine("（４）index[" + index + "] ");

                    // エスケープしながら、単独「"」が出てくるまでそのまま出力。
                    while (index < length)
                    {
                        ch = source[index];

                        //ystem.C onsole.WriteLine("（５）index[" + index + "] ");

                        if ('"' == ch)
                        {
                            // 「"」だった。


                            // ここで文字列終わりなのだが、
                            // しかし次の文字が「"」の場合、まだこの「"」で終わってはいけない。
                            // 

                            //ystem.C onsole.WriteLine("（６）index[" + index + "] ");


                            if (index + 1 == length)
                            {
                                // 2文字目が無ければ、
                                //「"」を無視して終了。
                                index++;

                                //ystem.C onsole.WriteLine("（７）index[" + index + "] ");

                                break;
                            }
                            else if ('"' == source[index + 1])
                            {
                                // 2文字目も「"」なら、
                                // 1,2文字目の「""」を「"」に変換して続行。
                                index += 2;
                                cell.Append('"');

                                //ystem.C onsole.WriteLine("（８）index[" + index + "] ");
                            }
                            else
                            {
                                // 2文字目が「"」でなければ、
                                //「"」を無視して終了。
                                index += 2;//【改変/】2012年10月30日変更。旧： index++;

                                //ystem.C onsole.WriteLine("（９）index[" + index + "] 　2文字目が「\"」でなければ、「\"」を無視して終了。");

                                break;
                            }
                        }
                        else
                        {
                            // 通常文字なので続行。
                            cell.Append(ch);
                            index++;

                            //ystem.C onsole.WriteLine("（１１）index[" + index + "] ch[" + ch + "]");
                        }

                        //ystem.C onsole.WriteLine("（１２）index[" + index + "] ");
                    }

                    //ystem.C onsole.WriteLine("（１３）index[" + index + "] ");
                }
                else
                {
                    //ystem.C onsole.WriteLine("（１４a）index[" + index + "] s_Cell[" + s_Cell.ToString() + "] ch[" + ch + "]");

                    cell.Append(ch);
                    index++;

                    //ystem.C onsole.WriteLine("（１４b）index[" + index + "] s_Cell[" + s_Cell.ToString() + "]");

                    // 1文字目が「"」でないなら、「,」が出てくるか、次がなくなるまでそのまま出力。
                    // フォーマットチェックは行わない。
                    while(index < length)
                    {
                        ch = source[index];

                        //ystem.C onsole.WriteLine("（１５）index[" + index + "] ch[" + ch + "]");


                        if (chDelimiter != ch)
                        {
                            // 文字を追加して次へ。
                            cell.Append(ch);
                            index++;

                            //ystem.C onsole.WriteLine("（１６）index[" + index + "] ");

                        }
                        else
                        {
                            // 「,」を見つけたのでこれを無視し、
                            // このセル読取は脱出。
                            index++;

                            //ystem.C onsole.WriteLine("（１７）index[" + index + "] 「,」を見つけたのでこれを無視し、このセル読取は脱出。");

                            break;
                        }

                        //ystem.C onsole.WriteLine("（１８）index[" + index + "] ");

                    }
                    // 次が無くなったか、「,」の次の文字を指している。
                }

                //ystem.C onsole.WriteLine("（２０）index[" + index + "] s_Cell.ToString()[" + s_Cell.ToString() + "]");

                list_Destination.Add(cell.ToString());
            }

            //ystem.C onsole.WriteLine("（２１）index[" + index + "] ");


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

            for (int index = 0; index < length; )
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
