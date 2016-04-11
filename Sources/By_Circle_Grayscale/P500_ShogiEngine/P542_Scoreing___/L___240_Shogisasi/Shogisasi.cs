using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P324_KifuTree___.L___250_Struct;
using Grayscale.P521_FeatureVect.L___500_Struct;

namespace Grayscale.P542_Scoreing___.L___240_Shogisasi
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
        /// <param name="errH"></param>
        /// <returns></returns>
        KifuNode WA_Bestmove(
            bool isHonshogi,
            KifuTree kifu,
            KwErrorHandler errH
            );

    }

}
