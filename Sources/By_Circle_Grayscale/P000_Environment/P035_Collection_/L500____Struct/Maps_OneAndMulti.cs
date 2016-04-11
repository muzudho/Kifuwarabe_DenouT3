using System;
using System.Collections.Generic;
using System.Text;

namespace Grayscale.P035_Collection_.L500____Struct
{

    /// <summary>
    /// １つのキーに、複数の要素が対応。Valueはリスト型に入る。
    /// 
    /// 例：どの駒が、どんな手を指せるか。
    /// </summary>
    public class Maps_OneAndMulti<T1,T2>
    {

        public Dictionary<T1, List<T2>> Items { get; set; }

        public Maps_OneAndMulti()
        {
            this.Items = new Dictionary<T1, List<T2>>();
        }

        public bool ContainsKey(T1 key)
        {
            return this.Items.ContainsKey(key);
        }

        /// <summary>
        /// 駒を追加します。
        /// まだ登録されていなければ新規追加、既に登録されていれば上書きします。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Put_NewOrOverwrite(T1 key, T2 value)
        {
            if (this.ContainsKey(key))
            {
                // すでに登録されている駒
                this.AddExists(key, value);
            }
            else
            {
                // まだ登録されていない駒
                this.AddNew(key, value);
            }
        }

        /// <summary>
        /// 駒を追加します。
        /// まだ登録されていなければ新規追加、既に登録されていれば無視します。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddNotOverwrite(T1 key, T2 value)
        {
            if (this.ContainsKey(key))
            {
                // すでに登録されている駒なら、無視します。
            }
            else
            {
                // まだ登録されていない駒
                this.AddNew(key, value);
            }
        }

        /// <summary>
        /// すでに登録されている駒を更新します。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddExists(T1 key, T2 value)
        {
            this.Items[key].Add(value);
        }

        /// <summary>
        /// まだ登録されていない駒を追加します。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddNew(T1 key, T2 value)
        {
            List<T2> values = new List<T2>();
            values.Add(value);
            this.Items.Add(key, values);
        }




        public string Dump()
        {
            // まず、内容確認☆
            StringBuilder sb = new StringBuilder();
            {
                foreach (KeyValuePair<T1, List<T2>> entry1 in this.Items)
                {
                    foreach (T2 value in entry1.Value)
                    {
                        sb.AppendLine("key=[" + entry1.Key.ToString() + "] value=[" + value.ToString() + "]");
                    }
                }
            }

            return sb.ToString();
        }





        /// <summary>
        /// 王手がかかった局面は取り除きます。
        /// </summary>
        [Obsolete("古い。")]
        public Maps_OneAndMulti<T1, T2> ToClone()
        {

            Maps_OneAndMulti<T1, T2> map2 = new Maps_OneAndMulti<T1, T2>();

            foreach (KeyValuePair<T1, List<T2>> entry1 in this.Items)
            {
                T1 key1 = entry1.Key;
                List<T2> values1 = entry1.Value;

                foreach (T2 value1 in values1)
                {
                    if (map2.ContainsKey(key1))
                    {
                        map2.AddExists( key1, value1);
                    }
                    else
                    {
                        map2.AddNew(key1, value1);
                    }

                }

            }

            return map2;
        }

    }
}
