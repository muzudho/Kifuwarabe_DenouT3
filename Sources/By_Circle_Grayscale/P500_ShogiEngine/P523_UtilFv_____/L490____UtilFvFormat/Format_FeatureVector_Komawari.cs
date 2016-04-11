using Grayscale.P213_Komasyurui_.L250____Word;
using Grayscale.P521_FeatureVect.L___500_Struct;
using System.Text;


namespace Grayscale.P523_UtilFv_____.L490____UtilFvFormat
{
    public abstract class Format_FeatureVector_Komawari
    {

        /// <summary>
        /// テキストを作ります。
        /// 駒割。
        /// </summary>
        /// <param name="fv"></param>
        /// <returns></returns>
        public static string Format_Text(FeatureVector fv)
        {
            StringBuilder sb = new StringBuilder();

            //
            // コメント
            //
            sb.AppendLine("\"#紹介文\",");
            sb.AppendLine("\"#\",");
            sb.AppendLine("\"#きふわらべ評価値　駒割\",");
            sb.AppendLine("\"#\",");
            sb.AppendLine("\"#ここにコメントを書いても、自動的に上書きされてしまうぜ☆？\",");

            //
            // 仕様バージョン
            //
            sb.AppendLine();
            sb.AppendLine("\"Version\",1.0,");

            //
            // 駒割
            //
            sb.AppendLine();
            sb.AppendLine("\"#KomaWari 順番を崩さないように書いてくれ☆\",");//コメント行

            sb.Append("\"Komawari\",\"(0)歩\",");
            sb.Append(fv.Komawari[(int)Komasyurui14.H01_Fu_____]);
            sb.AppendLine(",");

            sb.Append("\"Komawari\",\"(1)香\",");
            sb.Append(fv.Komawari[(int)Komasyurui14.H02_Kyo____]);
            sb.AppendLine(",");

            sb.Append("\"Komawari\",\"(2)桂\",");
            sb.Append(fv.Komawari[(int)Komasyurui14.H03_Kei____]);
            sb.AppendLine(",");

            sb.Append("\"Komawari\",\"(3)銀\",");
            sb.Append(fv.Komawari[(int)Komasyurui14.H04_Gin____]);
            sb.AppendLine(",");

            sb.Append("\"Komawari\",\"(4)金\",");
            sb.Append(fv.Komawari[(int)Komasyurui14.H05_Kin____]);
            sb.AppendLine(",");

            sb.Append("\"Komawari\",\"(5)玉\",");
            sb.Append(fv.Komawari[(int)Komasyurui14.H06_Gyoku__]);
            sb.AppendLine(",");

            sb.Append("\"Komawari\",\"(6)飛\",");
            sb.Append(fv.Komawari[(int)Komasyurui14.H07_Hisya__]);
            sb.AppendLine(",");

            sb.Append("\"Komawari\",\"(7)角\",");
            sb.Append(fv.Komawari[(int)Komasyurui14.H08_Kaku___]);
            sb.AppendLine(",");

            sb.Append("\"Komawari\",\"(8)と金\",");
            sb.Append(fv.Komawari[(int)Komasyurui14.H11_Tokin__]);
            sb.AppendLine(",");

            sb.Append("\"Komawari\",\"(9)成香\",");
            sb.Append(fv.Komawari[(int)Komasyurui14.H12_NariKyo]);
            sb.AppendLine(",");

            sb.Append("\"Komawari\",\"(10)成桂\",");
            sb.Append(fv.Komawari[(int)Komasyurui14.H13_NariKei]);
            sb.AppendLine(",");

            sb.Append("\"Komawari\",\"(11)成銀\",");
            sb.Append(fv.Komawari[(int)Komasyurui14.H14_NariGin]);
            sb.AppendLine(",");

            sb.Append("\"Komawari\",\"(12)竜\",");
            sb.Append(fv.Komawari[(int)Komasyurui14.H09_Ryu____]);
            sb.AppendLine(",");

            sb.Append("\"Komawari\",\"(13)馬\",");
            sb.Append(fv.Komawari[(int)Komasyurui14.H10_Uma____]);
            sb.AppendLine(",");

            return sb.ToString();
        }
    }
}
