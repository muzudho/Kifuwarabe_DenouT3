using Grayscale.P157_KyokumenPng.L___500_Struct;
using Grayscale.P222_Log_Kaisetu.L250____Struct;
using Grayscale.P323_Sennitite__.L___500_Struct;
using Grayscale.P521_FeatureVect.L___500_Struct;
using Grayscale.P542_Scoreing___.L___240_Shogisasi;

namespace Grayscale.P542_Scoreing___.L___250_Args
{
    public interface EvaluationArgs
    {
        /// <summary>
        /// 千日手になるかどうかの判定だけを行うクラスです。
        /// </summary>
        SennititeConfirmer SennititeConfirmer { get; }

        /// <summary>
        /// フィーチャー・ベクター。
        /// </summary>
        FeatureVector FeatureVector { get; }

        Shogisasi Shogisasi { get; }

        KyokumenPngEnvironment ReportEnvironment { get; }

#if DEBUG
        /// <summary>
        /// デバッグ用。
        /// </summary>
        KaisetuBoards KaisetuBoards_orNull { get; }
#endif
    }
}
