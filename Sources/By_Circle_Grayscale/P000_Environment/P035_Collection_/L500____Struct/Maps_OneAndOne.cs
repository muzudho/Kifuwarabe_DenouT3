using System.Collections.Generic;
using System.Text;

namespace Grayscale.P035_Collection_.L500____Struct
{


    /// <summary>
    /// 駒ハンドル（重複不可）をキーに、Masusを結びつけたものです。
    /// 
    /// このコレクションでは、「持ち駒の利き」を格納できません。（同じ駒を複数個所に置けるので）
    /// 
    /// 
    /// デバッグ出力を作りたいので用意したオブジェクトです。
    /// </summary>
    //[DebuggerDisplay("v(^▽^)vｲｪｰｲ☆{Words.Length} | {Message}")]
    public class Maps_OneAndOne<T1, T2>//Finger,Masus
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
        /// スプライト番号と、マス複数
        /// </summary>
        private Dictionary<T1, T2> items;

        public Dictionary<T1, T2> Items { get{return this.items;} }

        /// <summary>
        /// 差替え。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="addIfNothing">無ければ追加します。</param>
        public void AddReplace(T1 key, T2 value, bool addIfNothing)
        {
            if (this.items.ContainsKey(key))
            {
                // 既に登録されている駒なら
                this.items[key] = value;//差替えます。
            }
            else if (addIfNothing)
            {
                // 無かったので、新しく追加します。
                this.items.Add(key, value);
            }
        }

        public T2 ElementAt(T1 key)
        {
            return this.items[key];
        }
        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }

        public void SetEntries(Dictionary<T1, T2> src_items)
        {
            this.items = src_items;
        }

        #endregion


        public Maps_OneAndOne()
        {
            this.items = new Dictionary<T1, T2>();
        }

        /// <summary>
        /// クローンを作ります。
        /// </summary>
        /// <param name="entries"></param>
        public Maps_OneAndOne(Maps_OneAndOne<T1, T2> src)
        {
            this.items = src.items;
        }


        public delegate void DELEGATE_Foreach_Entry(T1 key, T2 value, ref bool toBreak);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="delegate_Foreach_Entry"></param>
        public void Foreach_Entry(DELEGATE_Foreach_Entry delegate_Foreach_Entry)
        {
            bool toBreak = false;

            foreach (KeyValuePair<T1, T2> entry in this.items)
            {
                delegate_Foreach_Entry(entry.Key, entry.Value, ref toBreak);

                if (toBreak)
                {
                    break;
                }
            }
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


        public delegate void DELEGATE_Foreach_Values(T2 values, ref bool toBreak);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="delegate_Foreach_Entry"></param>
        public void Foreach_Values(DELEGATE_Foreach_Values delegate_Foreach_Values)
        {
            bool toBreak = false;

            foreach (T2 value in this.items.Values)
            {
                delegate_Foreach_Values(value, ref toBreak);

                if (toBreak)
                {
                    break;
                }
            }
        }



        public List<T1> ToKeyList()
        {
            List<T1> keyList = new List<T1>();

            this.Foreach_Keys((T1 key, ref bool toBreak) =>
            {
                keyList.Add(key);
            });

            keyList.Sort();//一応ソートしとく？

            return keyList;
        }


        public string Dump()
        {
            // まず、内容確認☆
            StringBuilder sb = new StringBuilder();

            this.Foreach_Entry((T1 key, T2 value, ref bool toBreak) =>
            {
                sb.AppendLine("key=[" + key.ToString() + "] value=[" + value.ToString() + "]");
            });

            return sb.ToString();
        }

    }


}
