using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grayscale.P743_FvLearn____.L___450_Tyoseiryo;

namespace Grayscale.P743_FvLearn____.L450____Tyoseiryo
{

    /// <summary>
    /// 調整量の設定
    /// </summary>
    public class TyoseiryoSettingsImpl : TyoseiryoSettings
    {

        /// <summary>
        /// 調整量のもっとも細かな値。0より大きな正の数です。
        /// </summary>
        public float Smallest { get { return this.smallest; } }
        public void SetSmallest(float value)
        {
            this.smallest = value;
        }
        private float smallest;

        /// <summary>
        /// 調整量のもっとも荒い値。
        /// </summary>
        public float Largest { get { return this.largest; } }
        public void SetLargest(float value)
        {
            this.largest = value;
        }
        private float largest;

        /// <summary>
        /// 調整量を上げているときの、連続回数別の調整量表。
        /// </summary>
        public Dictionary<int, float> BairituUpDic_AtStep { get { return this.bairituUpDic_AtStep; } }
        private Dictionary<int, float> bairituUpDic_AtStep;

        /// <summary>
        /// 調整量を一気に下げるときの、連続回数別の調整量表。
        /// </summary>
        public Dictionary<int, float> BairituCooldownDic_AtStep { get { return this.bairituCooldownDic_AtStep; } }
        private Dictionary<int, float> bairituCooldownDic_AtStep;


        public TyoseiryoSettingsImpl()
        {
            this.smallest = 1.0f;//0.001f は小さすぎる。//1.0fは大きい？
            this.largest = 1000.0f;
            this.bairituUpDic_AtStep = new Dictionary<int, float>();
            this.bairituCooldownDic_AtStep = new Dictionary<int, float>();
        }
    }
}
