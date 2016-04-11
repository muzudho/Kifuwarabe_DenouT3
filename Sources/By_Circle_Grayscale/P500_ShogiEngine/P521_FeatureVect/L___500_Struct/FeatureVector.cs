
namespace Grayscale.P521_FeatureVect.L___500_Struct
{
    public interface FeatureVector
    {
        /// <summary>
        /// 駒割の価値。
        /// </summary>
        float[] Komawari { get; set; }

        /// <summary>
        /// 二駒関係ＰＰの評価値に掛ける倍率。
        /// fv_～.csv ファイルを読み書きする際に使用。
        /// </summary>
        float Bairitu_NikomaKankeiPp { get; }
        void SetBairitu_NikomaKankeiPp(float bairitu);

        /// <summary>
        /// 二駒関係ＰＰの評価値調整量の最小値。
        /// </summary>
        float TyoseiryoSmallest_NikomaKankeiPp { get; }
        void SetTyoseiryoSmallest_NikomaKankeiPp(float value);

        /// <summary>
        /// 二駒関係ＰＰの評価値調整量の最大値。
        /// </summary>
        float TyoseiryoLargest_NikomaKankeiPp { get; }
        void SetTyoseiryoLargest_NikomaKankeiPp(float value);

        /// <summary>
        /// 二駒関係ＰＰの評価値調整量の初期値。
        /// </summary>
        float TyoseiryoInit_NikomaKankeiPp { get; }
        void SetTyoseiryoInit_NikomaKankeiPp(float value);

        /// <summary>
        /// 二駒関係ＰＰ☆ ゲームで実際に使う値。
        /// 
        /// １３８６種類の調査項目　×　１３８６種類の調査項目？
        /// </summary>
        float[,] NikomaKankeiPp_ForMemory { get; set; }

        ///// <summary>
        ///// 二駒関係ＰＰ☆ ファイルに保存する用。
        ///// </summary>
        //int Get_NikomaKankeiPp_ForFile(int p1, int p2);

    }
}
