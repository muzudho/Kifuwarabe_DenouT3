using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grayscale.P743_FvLearn____.L___450_Tyoseiryo
{

    /// <summary>
    /// 調整量の設定
    /// </summary>
    public interface TyoseiryoSettings
    {
        /// <summary>
        /// 調整量のもっとも細かな値。0より大きな正の数です。
        /// </summary>
        float Smallest { get; }
        void SetSmallest(float value);

        /// <summary>
        /// 調整量のもっとも荒い値。
        /// </summary>
        float Largest { get; }
        void SetLargest(float value);

        /// <summary>
        /// 調整量を上げているときの、連続回数別の調整量表。
        /// </summary>
        Dictionary<int, float> BairituUpDic_AtStep { get; }

        /// <summary>
        /// 調整量を一気に下げるときの、連続回数別の調整量表。
        /// </summary>
        Dictionary<int, float> BairituCooldownDic_AtStep { get; }

    }
}
