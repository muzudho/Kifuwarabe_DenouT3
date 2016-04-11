using Grayscale.P011_Csv________.L500____Util;
using Grayscale.P055_Conv_Sy.L500____Converter;
using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P056_Syugoron___.L250____Struct;
using Grayscale.P211_WordShogi__.L250____Masu;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P214_Masu_______.L500____Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Grayscale.P248_Michi______.L500____Word
{


    /// <summary>
    /// 「1,上一,１九,１八,１七,１六,１五,１四,１三,１二,１一」を、
    /// 「[1]={0,1,2,3,4,5,6,7,8}」に変換して持ちます。
    /// </summary>
    public abstract class Michi187Array
    {

                
        #region 静的プロパティー類

        public static List<SySet<SyElement>> Items
        {
            get
            {
                return Michi187Array.items;
            }
        }
        private static List<SySet<SyElement>> items;

        static Michi187Array()
        {

            //----------
            // 筋１８７
            //----------
            Michi187Array.items = new List<SySet<SyElement>>();


            //----------------------------------------
            // kanjiToEnum
            //----------------------------------------
            // ファイルでの指定→ビットフィールド 変換用
            // 
            // 将棋盤の８１マスの符号だぜ☆　０～８０の、８１個の連番を振っているぜ☆
            // 
            // 再利用に利用。
            Conv_Sy.Put_WordBitfield("１一", Masu_Honshogi.nban11_１一 + 0);//１一
            Conv_Sy.Put_WordBitfield("１二", Masu_Honshogi.nban11_１一 + 1);
            Conv_Sy.Put_WordBitfield("１三", Masu_Honshogi.nban11_１一 + 2);
            Conv_Sy.Put_WordBitfield("１四", Masu_Honshogi.nban11_１一 + 3);
            Conv_Sy.Put_WordBitfield("１五", Masu_Honshogi.nban11_１一 + 4);
            Conv_Sy.Put_WordBitfield("１六", Masu_Honshogi.nban11_１一 + 5);
            Conv_Sy.Put_WordBitfield("１七", Masu_Honshogi.nban11_１一 + 6);
            Conv_Sy.Put_WordBitfield("１八", Masu_Honshogi.nban11_１一 + 7);
            Conv_Sy.Put_WordBitfield("１九", Masu_Honshogi.nban11_１一 + 8);
            Conv_Sy.Put_WordBitfield("２一", Masu_Honshogi.nban11_１一 + 9);
            Conv_Sy.Put_WordBitfield("２二", Masu_Honshogi.nban11_１一 + 10);
            Conv_Sy.Put_WordBitfield("２三", Masu_Honshogi.nban11_１一 + 11);
            Conv_Sy.Put_WordBitfield("２四", Masu_Honshogi.nban11_１一 + 12);
            Conv_Sy.Put_WordBitfield("２五", Masu_Honshogi.nban11_１一 + 13);
            Conv_Sy.Put_WordBitfield("２六", Masu_Honshogi.nban11_１一 + 14);
            Conv_Sy.Put_WordBitfield("２七", Masu_Honshogi.nban11_１一 + 15);
            Conv_Sy.Put_WordBitfield("２八", Masu_Honshogi.nban11_１一 + 16);
            Conv_Sy.Put_WordBitfield("２九", Masu_Honshogi.nban11_１一 + 17);
            Conv_Sy.Put_WordBitfield("３一", Masu_Honshogi.nban11_１一 + 18);
            Conv_Sy.Put_WordBitfield("３二", Masu_Honshogi.nban11_１一 + 19);
            Conv_Sy.Put_WordBitfield("３三", Masu_Honshogi.nban11_１一 + 20);
            Conv_Sy.Put_WordBitfield("３四", Masu_Honshogi.nban11_１一 + 21);
            Conv_Sy.Put_WordBitfield("３五", Masu_Honshogi.nban11_１一 + 22);
            Conv_Sy.Put_WordBitfield("３六", Masu_Honshogi.nban11_１一 + 23);
            Conv_Sy.Put_WordBitfield("３七", Masu_Honshogi.nban11_１一 + 24);
            Conv_Sy.Put_WordBitfield("３八", Masu_Honshogi.nban11_１一 + 25);
            Conv_Sy.Put_WordBitfield("３九", Masu_Honshogi.nban11_１一 + 26);
            Conv_Sy.Put_WordBitfield("４一", Masu_Honshogi.nban11_１一 + 27);
            Conv_Sy.Put_WordBitfield("４二", Masu_Honshogi.nban11_１一 + 28);
            Conv_Sy.Put_WordBitfield("４三", Masu_Honshogi.nban11_１一 + 29);
            Conv_Sy.Put_WordBitfield("４四", Masu_Honshogi.nban11_１一 + 30);
            Conv_Sy.Put_WordBitfield("４五", Masu_Honshogi.nban11_１一 + 31);
            Conv_Sy.Put_WordBitfield("４六", Masu_Honshogi.nban11_１一 + 32);
            Conv_Sy.Put_WordBitfield("４七", Masu_Honshogi.nban11_１一 + 33);
            Conv_Sy.Put_WordBitfield("４八", Masu_Honshogi.nban11_１一 + 34);
            Conv_Sy.Put_WordBitfield("４九", Masu_Honshogi.nban11_１一 + 35);
            Conv_Sy.Put_WordBitfield("５一", Masu_Honshogi.nban11_１一 + 36);
            Conv_Sy.Put_WordBitfield("５二", Masu_Honshogi.nban11_１一 + 37);
            Conv_Sy.Put_WordBitfield("５三", Masu_Honshogi.nban11_１一 + 38);
            Conv_Sy.Put_WordBitfield("５四", Masu_Honshogi.nban11_１一 + 39);
            Conv_Sy.Put_WordBitfield("５五", Masu_Honshogi.nban11_１一 + 40);
            Conv_Sy.Put_WordBitfield("５六", Masu_Honshogi.nban11_１一 + 41);
            Conv_Sy.Put_WordBitfield("５七", Masu_Honshogi.nban11_１一 + 42);
            Conv_Sy.Put_WordBitfield("５八", Masu_Honshogi.nban11_１一 + 43);
            Conv_Sy.Put_WordBitfield("５九", Masu_Honshogi.nban11_１一 + 44);
            Conv_Sy.Put_WordBitfield("６一", Masu_Honshogi.nban11_１一 + 45);
            Conv_Sy.Put_WordBitfield("６二", Masu_Honshogi.nban11_１一 + 46);
            Conv_Sy.Put_WordBitfield("６三", Masu_Honshogi.nban11_１一 + 47);
            Conv_Sy.Put_WordBitfield("６四", Masu_Honshogi.nban11_１一 + 48);
            Conv_Sy.Put_WordBitfield("６五", Masu_Honshogi.nban11_１一 + 49);
            Conv_Sy.Put_WordBitfield("６六", Masu_Honshogi.nban11_１一 + 50);
            Conv_Sy.Put_WordBitfield("６七", Masu_Honshogi.nban11_１一 + 51);
            Conv_Sy.Put_WordBitfield("６八", Masu_Honshogi.nban11_１一 + 52);
            Conv_Sy.Put_WordBitfield("６九", Masu_Honshogi.nban11_１一 + 53);
            Conv_Sy.Put_WordBitfield("７一", Masu_Honshogi.nban11_１一 + 54);
            Conv_Sy.Put_WordBitfield("７二", Masu_Honshogi.nban11_１一 + 55);
            Conv_Sy.Put_WordBitfield("７三", Masu_Honshogi.nban11_１一 + 56);
            Conv_Sy.Put_WordBitfield("７四", Masu_Honshogi.nban11_１一 + 57);
            Conv_Sy.Put_WordBitfield("７五", Masu_Honshogi.nban11_１一 + 58);
            Conv_Sy.Put_WordBitfield("７六", Masu_Honshogi.nban11_１一 + 59);
            Conv_Sy.Put_WordBitfield("７七", Masu_Honshogi.nban11_１一 + 60);
            Conv_Sy.Put_WordBitfield("７八", Masu_Honshogi.nban11_１一 + 61);
            Conv_Sy.Put_WordBitfield("７九", Masu_Honshogi.nban11_１一 + 62);
            Conv_Sy.Put_WordBitfield("８一", Masu_Honshogi.nban11_１一 + 63);
            Conv_Sy.Put_WordBitfield("８二", Masu_Honshogi.nban11_１一 + 64);
            Conv_Sy.Put_WordBitfield("８三", Masu_Honshogi.nban11_１一 + 65);
            Conv_Sy.Put_WordBitfield("８四", Masu_Honshogi.nban11_１一 + 66);
            Conv_Sy.Put_WordBitfield("８五", Masu_Honshogi.nban11_１一 + 67);
            Conv_Sy.Put_WordBitfield("８六", Masu_Honshogi.nban11_１一 + 68);
            Conv_Sy.Put_WordBitfield("８七", Masu_Honshogi.nban11_１一 + 69);
            Conv_Sy.Put_WordBitfield("８八", Masu_Honshogi.nban11_１一 + 70);
            Conv_Sy.Put_WordBitfield("８九", Masu_Honshogi.nban11_１一 + 71);
            Conv_Sy.Put_WordBitfield("９一", Masu_Honshogi.nban11_１一 + 72);
            Conv_Sy.Put_WordBitfield("９二", Masu_Honshogi.nban11_１一 + 73);
            Conv_Sy.Put_WordBitfield("９三", Masu_Honshogi.nban11_１一 + 74);
            Conv_Sy.Put_WordBitfield("９四", Masu_Honshogi.nban11_１一 + 75);
            Conv_Sy.Put_WordBitfield("９五", Masu_Honshogi.nban11_１一 + 76);
            Conv_Sy.Put_WordBitfield("９六", Masu_Honshogi.nban11_１一 + 77);
            Conv_Sy.Put_WordBitfield("９七", Masu_Honshogi.nban11_１一 + 78);
            Conv_Sy.Put_WordBitfield("９八", Masu_Honshogi.nban11_１一 + 79);
            Conv_Sy.Put_WordBitfield("９九", Masu_Honshogi.nban11_１一 + 80);

        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath1"></param>
        /// <returns></returns>
        public static bool Load(string filepath1)
        {
            bool successful = false;

            string filepath2 = Path.Combine(Application.StartupPath, filepath1);


            List<List<string>> rows = null;

            if (!File.Exists(filepath2))
            {
                MessageBox.Show("ファイルがありません。\n"+
                    "filepath2=[" + filepath2 + "]", "情報");
                rows = null;
                goto gt_EndMethod;
            }

            rows = Util_Csv.ReadCsv(filepath2);



            // 最初の１行は削除。
            rows.RemoveRange(0, 1);



            Michi187Array.Items.Clear();

            // 構文解析は大雑把です。
            // （１）空セルは無視します。
            // （２）「@DEFINE」セルが処理開始の合図です。
            // （３）次のセルには集合の名前です。「味方陣」「平野部」「敵陣」のいずれかです。
            // （４）次のセルは「=」です。
            // （５）次のセルは「{」です。
            // （６）次に「}」セルが出てくるまで、符号のセルが連続します。「１九」「１八」など。
            // （７）「}」セルで、@DEFINEの処理は終了です。
            foreach (List<string> row in rows)
            {
                // ２列目は、道名。
                SySet<SyElement> michi187 = new SySet_Ordered<SyElement>(row[1].Trim());
                SySet<SyElement> michiPart = null;

                // 各行の先頭１列目（連番）と２列目（道名）は削除。
                row.RemoveRange(0, 2);

                bool isPart_Define = false;//@DEFINEパート
                bool isPart_Define_Member = false;//符号パート

                foreach (string cell1 in row)
                {
                    string cell = cell1.Trim();

                    if(cell=="")
                    {
                        goto gt_Next1;
                    }

                    if (isPart_Define)
                    {
                        if (cell == "=")
                        {
                            goto gt_Next1;
                        }

                        if (cell == "{")
                        {
                            isPart_Define_Member = true;
                            goto gt_Next1;
                        }

                        if (cell == "}")
                        {
                            isPart_Define_Member = false;
                            isPart_Define = false;
                            goto gt_Next1;
                        }

                        if (isPart_Define_Member)
                        {
                            // 「１一」を「1」に変換します。
                            SyElement masu81 = Masu_Honshogi.Query_Basho( Conv_Sy.Query_Bitfield(cell));
                            michiPart.AddElement(masu81);
                        }
                        else
                        {
                            switch (cell)
                            {
                                case "味方陣": michiPart = new SySet_Ordered<SyElement>("味方陣"); michi187.AddSupersets(michiPart); goto gt_Next1;
                                case "平野部": michiPart = new SySet_Ordered<SyElement>("平野部"); michi187.AddSupersets(michiPart); goto gt_Next1;
                                case "敵陣": michiPart = new SySet_Ordered<SyElement>("敵陣"); michi187.AddSupersets(michiPart); goto gt_Next1;
                                default: throw new Exception("未定義のキーワードです。[" + cell + "]");
                            }
                        }
                    }
                    else
                    {
                        if (cell == "@DEFINE")
                        {
                            isPart_Define = true;
                            goto gt_Next1;
                        }
                    }

                    gt_Next1:
                        ;
                }

                Michi187Array.Items.Add(michi187);
            }

            successful = true;

        gt_EndMethod:
            return successful;
        }

        /// <summary>
        /// ロードした内容を確認するときに使います。
        /// </summary>
        /// <returns></returns>
        public static string LogHtml()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("    <title>道表</title>");
            sb.AppendLine("    <style type=\"text/css\">");
            sb.AppendLine("            /* 将棋盤 */");
            sb.AppendLine("            table{");
            sb.AppendLine("                border-collapse:collapse;");
            sb.AppendLine("                border:2px #2b2b2b solid;");
            sb.AppendLine("            }");
            sb.AppendLine("            td{");
            sb.AppendLine("                border:1px #2b2b2b solid;");
            sb.AppendLine("                background-color:#ffcc55;");
            sb.AppendLine("                width:48px; height:48px;");
            sb.AppendLine("            }");
            sb.AppendLine("    </style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");


            foreach (SySet<SyElement> michi in Michi187Array.Items)
            {
                sb.Append("<h1>「");
                sb.Append(michi.Word);
                sb.AppendLine("」</h1>");

                // そのマスに振る、順番の番号。「M 味方陣」「H 平野部」「T 敵陣」で分けてあります。
                Dictionary<SyElement, int> orderOnBanM = new Dictionary<SyElement, int>();
                Dictionary<SyElement, int> orderOnBanH = new Dictionary<SyElement, int>();
                Dictionary<SyElement, int> orderOnBanT = new Dictionary<SyElement, int>();

                int order = 1;
                foreach (SySet<SyElement>superset in michi.Supersets)
                {
                    switch(superset.Word)
                    {
                        case "味方陣":
                            {
                                foreach (SyElement masu in superset.Elements)
                                {
                                    orderOnBanM.Add(masu, order);
                                    order++;
                                }
                            }
                            break;
                        case "平野部":
                            {
                                foreach (SyElement masu in superset.Elements)
                                {
                                    orderOnBanH.Add(masu, order);
                                    order++;
                                }
                            }
                            break;
                        case "敵陣":
                            {
                                foreach (SyElement masu in superset.Elements)
                                {
                                    orderOnBanT.Add(masu, order);
                                    order++;
                                }
                            }
                            break;
                        default:
                            throw new Exception("未定義の集合名です。["+superset.Word+"]");
                    }
                }


                

                sb.Append("<table>");
                // ９一～１一、９二～１二、…９九～１九の順だぜ☆
                for (int dan = 1; dan <= 9; dan++)
                {
                    sb.AppendLine("<tr>");

                    sb.Append("    ");
                    for (int suji = 9; suji >= 1; suji--)
                    {

                        SyElement masu = Util_Masu10.OkibaSujiDanToMasu(Okiba.ShogiBan, suji, dan);

                        if (orderOnBanM.ContainsKey(masu))
                        {
                            // 順番が記されている味方陣マス
                            sb.Append("<td style=\"text-align:center; background-color:blue;\">");
                            sb.Append(orderOnBanM[masu]);
                            sb.Append("</td>");
                        }
                        else if (orderOnBanH.ContainsKey(masu))
                        {
                            // 順番が記されている平野部マス
                            sb.Append("<td style=\"text-align:center; background-color:green;\">");
                            sb.Append(orderOnBanH[masu]);
                            sb.Append("</td>");
                        }
                        else if (orderOnBanT.ContainsKey(masu))
                        {
                            // 順番が記されている敵陣マス
                            sb.Append("<td style=\"text-align:center; background-color:red;\">");
                            sb.Append(orderOnBanT[masu]);
                            sb.Append("</td>");
                        }
                        else
                        {
                            // 特に指定のないマス。
                            sb.Append("<td></td>");
                        }

                    }
                    sb.AppendLine();
                    sb.AppendLine("</tr>");

                }
                sb.AppendLine("</table>");
            }


            sb.AppendLine("</body>");
            sb.AppendLine("</html>");
            return sb.ToString();
        }

    }
}
