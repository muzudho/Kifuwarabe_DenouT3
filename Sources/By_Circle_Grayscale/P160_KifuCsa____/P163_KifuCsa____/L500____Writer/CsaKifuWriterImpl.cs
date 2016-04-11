using Grayscale.P163_KifuCsa____.L___250_Struct;
using System.IO;
using System.Text;

namespace Grayscale.P163_KifuCsa____.L500____Writer
{
    public class CsaKifuWriterImpl
    {

        /// <summary>
        /// デバッグ用に、中身を確認できるよう、データの内容をテキスト形式で出力します。
        /// </summary>
        /// <param name="filepath"></param>
        public static void WriteForDebug(string filepath, CsaKifu data)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("棋譜のバージョン：");
            sb.AppendLine(data.Version);

            sb.Append("プレイヤー１：");
            sb.AppendLine(data.Player1Name);

            sb.Append("プレイヤー２：");
            sb.AppendLine(data.Player2Name);

            sb.AppendLine("┏━━━━━━━━━━┓将棋盤");
            for (int dan = 1; dan <= 9; dan++)
            {
                for (int suji = 9; suji >= 1; suji--)
                {
                    sb.Append(data.Shogiban[suji, dan]);
                }
                sb.AppendLine();
            }
            sb.AppendLine("┗━━━━━━━━━━┛");

            sb.Append("初手のプレイヤー：");
            sb.AppendLine(data.FirstSengo);

            sb.AppendLine("┏━━━━━━━━━━┓指し手のリスト");
            foreach (CsaKifuSasite sasite in data.SasiteList)
            {
                sb.AppendLine(sasite.ToStringForDebug());
            }
            sb.AppendLine("┗━━━━━━━━━━┛");

            sb.Append("対局終了の仕方の分類：");
            sb.AppendLine(data.FinishedStatus);

            File.WriteAllText(filepath, sb.ToString());
        }

    }
}
