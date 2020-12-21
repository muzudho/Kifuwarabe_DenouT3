using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using System.Text;

#if DEBUG

#endif

namespace Grayscale.P370LogGraphiEx.L500Util
{
    public abstract class Conv_KaisetuBoards
    {
        public static string ToJsonStr(KaisetuBoards boards1)
        {
            StringBuilder sb_json_boardsLog = new StringBuilder();

            foreach (KaisetuBoard board1 in boards1.boards)
            {
                // 指し手。分かれば。
                string moveStr = ConvMove.ChangeMoveTo_KsString_ForLog(board1.moveOrNull, board1.GenTeban);

                //string oldCaption = boardLog1.Caption;
                //boardLog1.Caption += "_" + moveStr;
                sb_json_boardsLog.Append(Util_LogWriter_Json.ToJsonStr(board1));
                //boardLog1.Caption = oldCaption;
            }

            return sb_json_boardsLog.ToString();
        }

    }
}
