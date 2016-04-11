
namespace Grayscale.P323_Sennitite__.L___500_Struct
{
    /// <summary>
    /// TODO: 巻き戻しボタンへの対応がまだ。
    /// </summary>
    public interface SennititeCounter : SennititeConfirmer
    {
        void CountUp_New(ulong hash, string hint);
        void CountDown(ulong hash, string hint);
        void CountUp_Overwrite(ulong hash_old, ulong hash_new, string hint);

        /// <summary>
        /// 棋譜のクリアーに合わせます。
        /// </summary>
        void Clear();

        ///// <summary>
        ///// FIXME: 初手から、計算しなおします。
        ///// </summary>
        //void Recount_All(KifuTree kifuTree);

    }
}
