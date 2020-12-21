
#if DEBUG
using System.IO;
using System.Text;
using Nett;
#endif


namespace Grayscale.Kifuwarakaku.Entities.Features
{
    public abstract class Util_GraphicalLog
    {

        /// <summary>
        /// ログファイル通し番号。
        /// </summary>
        private static int LogFileCounter{get;set;}




        /// <summary>
        /// Masus版。
        /// </summary>
        /// <param name="masus"></param>
        /// <param name="fileNameMemo"></param>
        /// <param name="comment"></param>
        public static void WriteHtml5(bool enableLog, string fileNameMemo, string json)
        {
#if DEBUG
            if (!enableLog)
            {
                goto gt_EndMethod;
            }

            StringBuilder sb = new StringBuilder();


            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang=\"ja\">");
            sb.AppendLine("<head>");
            sb.AppendLine("    <meta charset=\"UTF-8\">");
            sb.AppendLine("    <title>グラフィカル局面ログ</title>");
            sb.AppendLine("    ");
            sb.AppendLine("    <script type=\"text/javascript\" src=\"../Data/graphicalKyokumenLog.js\">");
            sb.AppendLine("    </script>");
            sb.AppendLine("");
            sb.AppendLine("    <script type=\"text/javascript\">");
            sb.AppendLine("        //");
            sb.AppendLine("        // boardsData は、局面を複数件　持つ配列です。");
            sb.AppendLine("        //");
            sb.AppendLine("        var boardsData =");


            sb.Append(json);
            sb.AppendLine(";");


            sb.AppendLine("");
            sb.AppendLine("    </script>");
            sb.AppendLine("");
            sb.AppendLine("</head>");
            sb.AppendLine("<body onLoad=\"drawBoards(boardsData);\">");
            sb.AppendLine("");
            sb.AppendLine("    ｖ（＾▽＾）ｖ　将棋盤のログを　視覚化したいんだぜ☆<br/>");
            sb.AppendLine("");

            sb.AppendLine("    <div id=\"announce1\" style=\"");
            sb.AppendLine("        visibility:hidden;");
            sb.AppendLine("        color:white;");
            sb.AppendLine("        background-color:gray;");
            sb.AppendLine("        border:solid 2px black;");
            sb.AppendLine("        padding:5px;");
            sb.AppendLine("        margin:5px;");
            sb.AppendLine("        \"");
            sb.AppendLine("    >アナウンス：　別のブラウザに変えて見てくれ☆　このブラウザでは使えない機能があって、もっと色が付くかも☆ｗｗ</div>");

            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));
            string logsDirectory = Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("LogDirectory"));

            File.WriteAllText(Path.Combine(logsDirectory, $"_log{Util_GraphicalLog.LogFileCounter}_{fileNameMemo}.html"), sb.ToString());
            Util_GraphicalLog.LogFileCounter++;

        gt_EndMethod:
            ;
#endif
        }

    }
}
