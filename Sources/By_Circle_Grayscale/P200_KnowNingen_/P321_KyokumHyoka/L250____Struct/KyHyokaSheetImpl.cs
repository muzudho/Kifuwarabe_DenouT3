using Grayscale.P321_KyokumHyoka.L___250_Struct;
using System.Collections.Generic;

namespace Grayscale.P321_KyokumHyoka.L250____Struct
{
    /// <summary>
    /// 局面を切り取ったときの、スコアの明細
    /// </summary>
    public class KyHyokaSheetImpl : KyHyokaSheet
    {

        /// <summary>
        /// 全項目。
        /// </summary>
        public Dictionary<string, KyHyokaMeisai_Koumoku> Items { get { return this.items; } }
        private Dictionary<string, KyHyokaMeisai_Koumoku> items;

        public KyHyokaSheetImpl()
        {
            this.items = new Dictionary<string, KyHyokaMeisai_Koumoku>();
        }

        /// <summary>
        /// 枝専用。
        /// </summary>
        /// <param name="kyHyokaItem"></param>
        public KyHyokaSheetImpl(float branchScore)//
        {
            this.items = new Dictionary<string, KyHyokaMeisai_Koumoku>();
            this.items.Add("枝", new KyHyokaMeisai_KoumokuImpl("枝", branchScore));
        }

        public void Clear()
        {
            this.items.Clear();
        }

        /// <summary>
        /// 評価明細項目の追加。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        public void Add(string name, KyHyokaMeisai_Koumoku item)
        {
            if (this.items.ContainsKey(name))
            {
                this.items[name] = item;
            }
            else
            {
                this.items.Add(name, item);
            }
        }

        public KyHyokaMeisai_Koumoku Get(string name)
        {
            KyHyokaMeisai_Koumoku meisai;

            if (this.items.ContainsKey(name))
            {
                meisai = this.items[name];
            }
            else
            {
                float score = 0.0f;
                meisai = new KyHyokaMeisai_KoumokuImpl("ヌル", score);
            }

            return meisai;
        }
    }
}
