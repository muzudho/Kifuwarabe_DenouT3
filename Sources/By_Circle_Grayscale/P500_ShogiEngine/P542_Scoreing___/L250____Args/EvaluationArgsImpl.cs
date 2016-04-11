using Grayscale.P157_KyokumenPng.L___500_Struct;
using Grayscale.P222_Log_Kaisetu.L250____Struct;
using Grayscale.P323_Sennitite__.L___500_Struct;
using Grayscale.P521_FeatureVect.L___500_Struct;
using Grayscale.P542_Scoreing___.L___240_Shogisasi;
using Grayscale.P542_Scoreing___.L___250_Args;

namespace Grayscale.P542_Scoreing___.L250____Args
{
    public class EvaluationArgsImpl : EvaluationArgs
    {
        /// <summary>
        /// 千日手になるかどうかの判定だけを行うクラスです。
        /// </summary>
        public SennititeConfirmer SennititeConfirmer { get{return this.sennititeConfirmer;} }
        private SennititeConfirmer sennititeConfirmer;

        public FeatureVector FeatureVector { get { return this.featureVector; } }
        private FeatureVector featureVector;

        public Shogisasi Shogisasi { get { return this.shogisasi; } }
        private Shogisasi shogisasi;


        public KyokumenPngEnvironment ReportEnvironment { get { return this.reportEnvironment; } }
        private KyokumenPngEnvironment reportEnvironment;

#if DEBUG
        /// <summary>
        /// デバッグ用。
        /// </summary>
        public KaisetuBoards KaisetuBoards_orNull { get { return this.kaisetuBoards_orNull; } }
        private KaisetuBoards kaisetuBoards_orNull;
#endif

        public EvaluationArgsImpl(
            SennititeConfirmer sennititeConfirmer,
            FeatureVector featureVector,
            Shogisasi shogisasi,
            KyokumenPngEnvironment reportEnvironment
#if DEBUG
            ,
            KaisetuBoards kaisetuBoards_orNull
#endif
            )
        {
            this.sennititeConfirmer = sennititeConfirmer;
            this.featureVector = featureVector;
            this.shogisasi = shogisasi;
            this.reportEnvironment = reportEnvironment;
#if DEBUG
            this.kaisetuBoards_orNull = kaisetuBoards_orNull;
#endif
        }
    }
}
