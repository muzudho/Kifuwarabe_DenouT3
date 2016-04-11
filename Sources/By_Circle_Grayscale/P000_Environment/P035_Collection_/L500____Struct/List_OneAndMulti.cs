using System.Collections.Generic;

namespace Grayscale.P035_Collection_.L500____Struct
{

    /// <summary>
    /// １つのキーに、複数の要素が対応。
    /// 
    /// 例：どの駒が、どんな手を指せるか。
    /// </summary>
    public class List_OneAndMulti<T1,T2>
    {

        public List<Couple<T1,T2>> Items { get; set; }

        public List_OneAndMulti()
        {
            this.Items = new List<Couple<T1, T2>>();
        }

        /// <summary>
        /// まだ登録されていない駒を追加します。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddNew(T1 key, T2 value)
        {
            this.Items.Add(new Couple<T1, T2>(key, value));
        }

        public void AddRange_New(Maps_OneAndOne<T1, T2> oo)
        {
            oo.Foreach_Entry((T1 key, T2 value, ref bool toBreak) =>
            {
                this.AddNew(key, value);
            });
        }

        public void AddRange_New(List_OneAndMulti<T1, T2> om)
        {
            om.Foreach_Entry((T1 key, T2 value, ref bool toBreak) =>
            {
                this.AddNew(key, value);
            });
        }

        public int Count
        {
            get
            {
                return this.Items.Count;
            }
        }



        public delegate void DELEGATE_Foreach_Entry(T1 a, T2 b, ref bool toBreak);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="delegate_Foreach_Entry"></param>
        public void Foreach_Entry(DELEGATE_Foreach_Entry delegate_Foreach_Entry)
        {
            bool toBreak = false;

            foreach (Couple<T1, T2> entry1 in this.Items)
            {
                delegate_Foreach_Entry(entry1.A, entry1.B, ref toBreak);

                if (toBreak)
                {
                    goto gt_EndMethod;
                }
            }

        gt_EndMethod:
            ;
        }

    }
}
