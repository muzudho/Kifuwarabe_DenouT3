using Grayscale.Kifuwarakaku.Entities.Features;

namespace Grayscale.Kifuwarakaku.Entities
{
    public interface IGame
    {
        Playerside FirstPside { get; }

        /// <summary>
        /// 棋譜です。
        /// </summary>
        KifuTree Kifu { get; }

        int GoBTime { get; }
        int GoWTime { get; }
        int GoByoYomi { get; }
        int GoBInc { get; }
        int GoWInc { get; }

        /// <summary>
        /// 「go ponder」の属性です。
        /// </summary>
        bool GoPonderNow { get; set; }
    }
}
