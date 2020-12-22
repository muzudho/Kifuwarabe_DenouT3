using Grayscale.Kifuwarakaku.Entities.Features;

namespace Grayscale.Kifuwarakaku.UseCases.Features
{

    /// <summary>
    /// 将棋指し。
    /// </summary>
    public interface Shogisasi
    {
        /// <summary>
        /// 指し手を決めます。
        /// </summary>
        /// <param name="enableLog"></param>
        /// <param name="isHonshogi"></param>
        /// <param name="kifu"></param>
        /// <param name="logTag"></param>
        /// <returns></returns>
        KifuNode WA_Bestmove(
            Playing playing,
            bool isHonshogi,
            KifuTree kifu
            );

    }

}
