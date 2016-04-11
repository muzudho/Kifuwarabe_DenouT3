
namespace Grayscale.P323_Sennitite__.L___500_Struct
{

    /// <summary>
    /// 千日手になるかどうかを判定するだけのクラスです。
    /// </summary>
    public interface SennititeConfirmer
    {
        /// <summary>
        /// 次に足したら、４回目以上になる場合、真。
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        bool IsNextSennitite(ulong hash);
    }
}
