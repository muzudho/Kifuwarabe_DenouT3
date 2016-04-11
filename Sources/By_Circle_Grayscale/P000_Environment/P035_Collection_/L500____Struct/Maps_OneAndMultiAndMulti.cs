using System.Collections.Generic;

namespace Grayscale.P035_Collection_.L500____Struct
{


    /// <summary>
    /// FIXME: 即席で作ったので大雑把。
    /// 
    /// 駒ハンドルとマス番号をキーに、Masusを結びつけたものです。
    /// 
    /// このコレクションは、「持ち駒の利き」の格納を想定しています。（同じ駒を複数個所に置くのに使う）
    /// </summary>
    //[DebuggerDisplay("v(^▽^)vｲｪｰｲ☆{Words.Length} | {Message}")]
    public class Maps_OneAndMultiAndMulti<T1,T2,T3>//Finger,Masu,Masus
    {
        //#region DebuggerDisplay用

        //private string _message = "こんにちわわ。";
        //public string Message
        //{
        //    get { return ShogibanTermDisplay.Kamd_ToTerm(this); }
        //    set { this._message = value; }
        //}

        //private string[] _words = new string[] { "壱", "弐", "参" };
        //public string[] Words
        //{
        //    get { return this._words; }
        //}

        //#endregion



        #region プロパティー類

        /// <summary>
        /// スプライト別、マス別、マス複数
        /// </summary>
        private Dictionary<T1, Dictionary<T2, T3>> items;

        /// <summary>
        /// 差替え。
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="value"></param>
        /// <param name="addIfNothing">無ければ追加します。</param>
        public void AddReplace(T1 key1, T2 key2, T3 value, bool addIfNothing)
        {
            if (this.items.ContainsKey(key1))
            {
                // 既に登録されている駒なら

                if (this.items[key1].ContainsKey(key2))
                {
                    // 既に登録されているマスなら
                    this.items[key1][key2] = value;//差替えます。
                }
                else
                {
                    // 新規のマスなら、新しく追加します。
                    this.items[key1].Add(key2, value);
                }

            }
            else if (addIfNothing)
            {
                // 新規のスプライトでしたので、新しく追加します。
                Dictionary<T2, T3> values = new Dictionary<T2, T3>();
                values.Add(key2, value);

                this.items.Add(key1, values);
            }
        }

        /// <summary>
        /// 無ければ追加、あれば上書き。
        /// </summary>
        /// <param name="hKoma"></param>
        /// <param name="value"></param>
        public void AddOverwrite(T1 key1, T2 key2, T3 value)
        {
            if (this.items.ContainsKey(key1))
            {
                // 既に登録されている駒なら

                if (this.items[key1].ContainsKey(key2))
                {
                    // 既に登録されているマスなら
                    this.items[key1][key2] = value;//差替えます。
                }
                else
                {
                    // 新規のマスなら、新しく追加します。
                    this.items[key1].Add(key2, value);
                }
            }
            else
            {
                // 新規のスプライトでしたので、新しく追加します。
                Dictionary<T2, T3> values = new Dictionary<T2, T3>();
                values.Add(key2, value);

                this.items.Add(key1, values);
            }
        }

        public T3 ElementAt(T1 key1, T2 key2)
        {
            return this.items[key1][key2];
        }
        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }

        public void SetEntries(Dictionary<T1, Dictionary<T2, T3>> src_items)
        {
            this.items = src_items;
        }

        #endregion


        public Maps_OneAndMultiAndMulti()
        {
            this.items = new Dictionary<T1, Dictionary<T2, T3>>();
        }

        /// <summary>
        /// クローンを作ります。
        /// </summary>
        /// <param name="entries"></param>
        public Maps_OneAndMultiAndMulti(Maps_OneAndMultiAndMulti<T1, T2,T3> src)
        {
            this.items = src.items;
        }


        public delegate void DELEGATE_Foreach_Entry(T1 key1, T2 key2, T3 value, ref bool toBreak);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="delegate_Foreach_Entry"></param>
        public void Foreach_Entry(DELEGATE_Foreach_Entry delegate_Foreach_Entry)
        {
            bool toBreak = false;

            foreach (KeyValuePair<T1, Dictionary<T2, T3>> entry1 in this.items)
            {
                T1 key = entry1.Key;

                foreach (KeyValuePair<T2, T3> entry2 in entry1.Value)
                {
                    delegate_Foreach_Entry(key, entry2.Key, entry2.Value, ref toBreak);

                    if (toBreak)
                    {
                        goto gt_EndMethod;
                    }
                }
            }

        gt_EndMethod:
            ;
        }


        public delegate void DELEGATE_Foreach_Keys(T1 key, ref bool toBreak);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="delegate_Foreach_Entry"></param>
        public void Foreach_Keys(DELEGATE_Foreach_Keys delegate_Foreach_Keys)
        {
            bool toBreak = false;

            foreach (T1 key in this.items.Keys)
            {
                delegate_Foreach_Keys(key, ref toBreak);

                if (toBreak)
                {
                    break;
                }
            }
        }


        //public delegate void DELEGATE_Foreach_Values(SySet<SyElement> values, ref bool toBreak);
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="delegate_Foreach_Entry"></param>
        //public void Foreach_Values(DELEGATE_Foreach_Values delegate_Foreach_Values)
        //{
        //    bool toBreak = false;

        //    foreach (SySet<SyElement> value in this.items.Values)
        //    {
        //        delegate_Foreach_Values(value, ref toBreak);

        //        if (toBreak)
        //        {
        //            break;
        //        }
        //    }
        //}




        public List<T1> ToKeyList()
        {
            List<T1> keyList = new List<T1>();

            this.Foreach_Keys((T1 hKoma3, ref bool toBreak) =>
            {
                keyList.Add(hKoma3);
            });

            keyList.Sort();//一応ソートしとく？

            return keyList;
        }

    }


}
