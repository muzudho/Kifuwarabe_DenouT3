using Grayscale.Kifuwarakaku.Entities.Logger;
using Grayscale.P324KifuTree.I250Struct;
using Grayscale.P521FeatureVect.I500Struct;

namespace Grayscale.P542Scoreing.L240Shogisasi
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
