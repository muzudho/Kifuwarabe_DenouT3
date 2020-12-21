using Grayscale.Kifuwarakaku.Entities.Features;

namespace Grayscale.Kifuwarakaku.UseCases.Features
{

    /// <summary>
    /// 将棋指し。
    /// </summary>
    public interface Shogisasi
    {
        /// <summary>
        /// 右脳。
        /// </summary>
        FeatureVector FeatureVector { get; set; }

        /// <summary>
        /// 対局開始のとき。
        /// </summary>
        void OnTaikyokuKaisi();


        /// <summary>
        /// 指し手を決めます。
        /// </summary>
        /// <param name="enableLog"></param>
        /// <param name="isHonshogi"></param>
        /// <param name="kifu"></param>
        /// <param name="logTag"></param>
        /// <returns></returns>
        KifuNode WA_Bestmove(
            bool isHonshogi,
            KifuTree kifu
            );

    }

}
