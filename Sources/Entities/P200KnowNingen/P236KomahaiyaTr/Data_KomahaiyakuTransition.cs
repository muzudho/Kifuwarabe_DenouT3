#if DEBUG
using Grayscale.P003Log.L500Struct;
using Grayscale.P011Csv.L250Parser;
using Grayscale.P056Syugoron.I250Struct;
using Grayscale.P211WordShogi.L500Word;
using Grayscale.P212ConvPside.L500Converter;
using Grayscale.P213Komasyurui.L250Word;
using Grayscale.P214Masu.L500Util;
using Grayscale.P234Komahaiyaku.L250Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Nett;
#else
using Grayscale.Kifuwarakaku.Entities.Logger;
using Grayscale.P011Csv.L250Parser;
using Grayscale.P056Syugoron.I250Struct;
using Grayscale.P211WordShogi.L500Word;
using Grayscale.P212ConvPside.L500Converter;
using Grayscale.P213Komasyurui.L250Word;
using Grayscale.P214Masu.L500Util;
using Grayscale.P234Komahaiyaku.L250Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
#endif

namespace Grayscale.P236KomahaiyaTr.L500Table
{

    /// <summary>
    /// 配役転換表。
    /// </summary>
    public class Data_KomahaiyakuTransition
    {


        #region 静的プロパティー類

        /// <summary>
        /// 種類ハンドル→升ハンドル→次配役ハンドルの連鎖なんだぜ☆
        /// </summary>
        public static Dictionary<Komasyurui14, Komahaiyaku185[]> Map
        {
            get
            {
                return Data_KomahaiyakuTransition.map;
            }
        }
        private static Dictionary<Komasyurui14, Komahaiyaku185[]> map;

        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="syurui"></param>
        /// <param name="masu_shogiban">0～80</param>
        /// <returns></returns>
        public static Komahaiyaku185 ToHaiyaku(Komasyurui14 syurui, SyElement masu_shogiban, Playerside pside)
        {
            Komahaiyaku185 result;

            int masuHandle = Conv_SyElement.ToMasuNumber(Util_Masu10.BothSenteView(masu_shogiban, pside));

            if (Conv_MasuHandle.OnShogiban(masuHandle))
            {

                result = Data_KomahaiyakuTransition.Map[syurui][(int)masuHandle];
            }
            else if (Conv_MasuHandle.OnKomadai(masuHandle))
            {
                switch(syurui)
                {
                    case Komasyurui14.H01_Fu_____: result = Komahaiyaku185.n164_歩打; break;
                    case Komasyurui14.H02_Kyo____: result = Komahaiyaku185.n165_香打; break;
                    case Komasyurui14.H03_Kei____: result = Komahaiyaku185.n166_桂打; break;
                    case Komasyurui14.H04_Gin____: result = Komahaiyaku185.n167_銀打; break;
                    case Komasyurui14.H05_Kin____: result = Komahaiyaku185.n168_金打; break;
                    case Komasyurui14.H06_Gyoku__: result = Komahaiyaku185.n169_王打; break;
                    case Komasyurui14.H07_Hisya__: result = Komahaiyaku185.n170_飛打; break;
                    case Komasyurui14.H08_Kaku___: result = Komahaiyaku185.n171_角打; break;
                    case Komasyurui14.H09_Ryu____: result = Komahaiyaku185.n170_飛打; break;
                    case Komasyurui14.H10_Uma____: result = Komahaiyaku185.n171_角打; break;
                    case Komasyurui14.H11_Tokin__: result = Komahaiyaku185.n164_歩打; break;
                    case Komasyurui14.H12_NariKyo: result = Komahaiyaku185.n165_香打; break;
                    case Komasyurui14.H13_NariKei: result = Komahaiyaku185.n166_桂打; break;
                    case Komasyurui14.H14_NariGin: result = Komahaiyaku185.n167_銀打; break;
                    default: result = Komahaiyaku185.n000_未設定; break;
                }
            }
            else if (Conv_MasuHandle.OnKomabukuro(masuHandle))
            {
                switch (syurui)
                {
                    case Komasyurui14.H01_Fu_____: result = Komahaiyaku185.n172_駒袋歩; break;
                    case Komasyurui14.H02_Kyo____: result = Komahaiyaku185.n173_駒袋香; break;
                    case Komasyurui14.H03_Kei____: result = Komahaiyaku185.n174_駒袋桂; break;
                    case Komasyurui14.H04_Gin____: result = Komahaiyaku185.n175_駒袋銀; break;
                    case Komasyurui14.H05_Kin____: result = Komahaiyaku185.n176_駒袋金; break;
                    case Komasyurui14.H06_Gyoku__: result = Komahaiyaku185.n177_駒袋王; break;
                    case Komasyurui14.H07_Hisya__: result = Komahaiyaku185.n178_駒袋飛; break;
                    case Komasyurui14.H08_Kaku___: result = Komahaiyaku185.n179_駒袋角; break;
                    case Komasyurui14.H09_Ryu____: result = Komahaiyaku185.n180_駒袋竜; break;
                    case Komasyurui14.H10_Uma____: result = Komahaiyaku185.n181_駒袋馬; break;
                    case Komasyurui14.H11_Tokin__: result = Komahaiyaku185.n182_駒袋と金; break;
                    case Komasyurui14.H12_NariKyo: result = Komahaiyaku185.n183_駒袋杏; break;
                    case Komasyurui14.H13_NariKei: result = Komahaiyaku185.n184_駒袋圭; break;
                    case Komasyurui14.H14_NariGin: result = Komahaiyaku185.n185_駒袋全; break;
                    default: result = Komahaiyaku185.n000_未設定; break;
                }
            }
            else
            {
                result = Komahaiyaku185.n000_未設定;
            }

            return result;
        }


        public static List<List<string>> Load(string path, Encoding encoding)
        {
            StringBuilder sbLog = new StringBuilder();

            List<List<string>> rows = new List<List<string>>();

            foreach (string line in File.ReadAllLines(path, encoding))
            {
                rows.Add(CsvLineParserImpl.UnescapeLineToFieldList(line, ','));
            }


            // 最初の2行は削除。
            rows.RemoveRange(0, 2);

            // 各行の先頭3列は削除。
            foreach (List<string> row in rows)
            {
                row.RemoveRange(0, 3);
            }


            //------------------------------
            // データ部だけが残っています。
            //------------------------------


            // コメント行、データ行が交互に出てきます。
            // コメント行を削除します。
            List<List<string>> rows2;
            {
                rows2 = new List<List<string>>();

                int rowCount1 = 0;
                foreach (List<string> row in rows)
                {
                    // 奇数行がデータです。
                    if (rowCount1 % 2 == 1)
                    {
                        rows2.Add(row);
                    }

                    rowCount1++;
                }
            }

#if DEBUG
            // デバッグ出力
            {
                var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
                var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));

                StringBuilder sb = new StringBuilder();

                foreach (List<string> row2 in rows2)
                {

                    foreach (string field in row2)
                    {
                        sb.Append(field);
                        sb.Append(",");

                    }
                    sb.AppendLine();
                }

                string filepath_HaiyakuLoad1 = Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("OutputSyuruiToHaiyaku01DataRowOnly"));
                File.WriteAllText(filepath_HaiyakuLoad1, sb.ToString());
            }
#endif



            Data_KomahaiyakuTransition.map = new Dictionary<Komasyurui14, Komahaiyaku185[]>();


            int rowCount2 = 0;
            Komahaiyaku185[] table81 = null;
            foreach (List<string> row2 in rows2)
            {
                if (rowCount2 % 9 == 0)
                {
                    table81 = new Komahaiyaku185[81];

                    int syuruiNumber = rowCount2 / 9 + 1;
                    if (15 <= syuruiNumber)
                    {
                        goto gt_EndMethod;
                    }
                    Data_KomahaiyakuTransition.map.Add(Array_Komasyurui.Items_AllElements[syuruiNumber], table81);
                }


                //----------
                // テーブル作り
                //----------

                int columnCount = 0;
                foreach (string column in row2)
                {
                    // 空っぽの列は無視します。
                    if ("" == column)
                    {
                        goto gt_NextColumn;
                    }

                    // 空っぽでない列の値を覚えます。

                    // 数値型のはずです。
                    int cellValue;
                    if (!int.TryParse(column, out cellValue))
                    {
                        throw new Exception($@"エラー。
path=[{path}]
「配役転換表」に、int型数値でないものが指定されていました。
rowCount=[{rowCount2}]
columnCount=[{columnCount}");
                    }

                    int masuHandle = (8 - columnCount) * 9 + (rowCount2 % 9);//0～80

                    sbLog.AppendLine("(" + rowCount2 + "," + columnCount + ")[" + masuHandle + "]" + cellValue);

                    table81[masuHandle] = Array_Komahaiyaku185.Items[cellValue];

                gt_NextColumn:
                    columnCount++;
                }

                rowCount2++;
            }

        gt_EndMethod:

#if DEBUG
            {
                var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
                var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));

                string filepath_HaiyakuLoad2 = Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("OutputSyuruiToHaiyaku02Load"));
                File.WriteAllText(filepath_HaiyakuLoad2, sbLog.ToString());
            }
#endif

            return rows;
        }


        /// <summary>
        /// ロードした内容を確認するときに使います。
        /// </summary>
        /// <returns></returns>
        public static string Format_LogHtml()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("    <title>配役転換表</title>");
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

            foreach (KeyValuePair<Komasyurui14, Komahaiyaku185[]> entry1 in Data_KomahaiyakuTransition.Map)
            {
                sb.Append("<h1>");
                sb.Append(entry1.Key);
                sb.AppendLine("</h1>");


                sb.Append("<table>");
                // ９一～１一、９二～１二、…９九～１九の順だぜ☆
                for (int dan = 1; dan <= 9; dan++)
                {
                    sb.AppendLine("<tr>");

                    sb.Append("    ");
                    for (int suji = 9; suji >= 1; suji--)
                    {

                        SyElement masu = Util_Masu10.OkibaSujiDanToMasu(Okiba.ShogiBan, suji, dan);

                        sb.Append("<td>");


                        Komahaiyaku185 kh184 = entry1.Value[Conv_SyElement.ToMasuNumber(masu)];
                        int haiyakuHandle = (int)kh184;


                        sb.Append("<img src=\"../../Data/img/train");


                        if (haiyakuHandle < 10)
                        {
                            sb.Append("00");
                        }
                        else if (haiyakuHandle < 100)
                        {
                            sb.Append("0");
                        }
                        sb.Append(haiyakuHandle);
                        sb.Append(".png\" />");



                        sb.Append("</td>");
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
