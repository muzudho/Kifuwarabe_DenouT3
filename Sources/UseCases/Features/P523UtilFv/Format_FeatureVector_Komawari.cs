using System.Text;
using Grayscale.Kifuwarakaku.Entities.Features;


namespace Grayscale.Kifuwarakaku.UseCases.Features
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
            sb.Append(fv.Komawari[(int)PieceType.P]);
            sb.AppendLine(",");

            sb.Append("\"Komawari\",\"(1)香\",");
            sb.Append(fv.Komawari[(int)PieceType.L]);
            sb.AppendLine(",");

            sb.Append("\"Komawari\",\"(2)桂\",");
            sb.Append(fv.Komawari[(int)PieceType.N]);
            sb.AppendLine(",");

            sb.Append("\"Komawari\",\"(3)銀\",");
            sb.Append(fv.Komawari[(int)PieceType.S]);
            sb.AppendLine(",");

            sb.Append("\"Komawari\",\"(4)金\",");
            sb.Append(fv.Komawari[(int)PieceType.G]);
            sb.AppendLine(",");

            sb.Append("\"Komawari\",\"(5)玉\",");
            sb.Append(fv.Komawari[(int)PieceType.K]);
            sb.AppendLine(",");

            sb.Append("\"Komawari\",\"(6)飛\",");
            sb.Append(fv.Komawari[(int)PieceType.R]);
            sb.AppendLine(",");

            sb.Append("\"Komawari\",\"(7)角\",");
            sb.Append(fv.Komawari[(int)PieceType.B]);
            sb.AppendLine(",");

            sb.Append("\"Komawari\",\"(8)と金\",");
            sb.Append(fv.Komawari[(int)PieceType.PP]);
            sb.AppendLine(",");

            sb.Append("\"Komawari\",\"(9)成香\",");
            sb.Append(fv.Komawari[(int)PieceType.PL]);
            sb.AppendLine(",");

            sb.Append("\"Komawari\",\"(10)成桂\",");
            sb.Append(fv.Komawari[(int)PieceType.PN]);
            sb.AppendLine(",");

            sb.Append("\"Komawari\",\"(11)成銀\",");
            sb.Append(fv.Komawari[(int)PieceType.PS]);
            sb.AppendLine(",");

            sb.Append("\"Komawari\",\"(12)竜\",");
            sb.Append(fv.Komawari[(int)PieceType.PR]);
            sb.AppendLine(",");

            sb.Append("\"Komawari\",\"(13)馬\",");
            sb.Append(fv.Komawari[(int)PieceType.PB]);
            sb.AppendLine(",");

            return sb.ToString();
        }
    }
}
