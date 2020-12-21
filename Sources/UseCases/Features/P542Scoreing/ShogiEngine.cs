using System.Collections.Generic;

namespace Grayscale.Kifuwarakaku.UseCases.Features
{
    public interface ShogiEngine
    {
        /// <summary>
        /// 送信
        /// </summary>
        /// <param name="line">メッセージ</param>
        void Send(string line);

        /// <summary>
        /// USI「setoption」コマンドのリストです。
        /// </summary>
        Dictionary<string, string> SetoptionDictionary { get; set; }

        /// <summary>
        /// USI「ponder」の使用の有無です。
        /// </summary>
        bool Option_enable_usiPonder { get; set; }

        /// <summary>
        /// サーバーに「noop」コマンドを投げてもよいかどうかです。
        /// サーバーに「noop」を送ると、「ok」を返してくれるものとします。１分間を空けてください。
        /// </summary>
        bool Option_enable_serverNoopable { get; set; }

    }
}
