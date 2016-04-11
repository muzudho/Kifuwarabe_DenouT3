using Grayscale.P056_Syugoron___.L___250_Struct;
using System.Collections.Generic;

namespace Grayscale.P056_Syugoron___.L250____Struct
{


    /// <summary>
    /// 飛車の移動を表すために作った、インチキな集合です。
    /// 
    /// 「先手から見て、上に向かってずっと」といった構造を表しています。
    /// 
    /// ほんとは　グラフ理論がやって欲しい☆
    /// 
    /// </summary>
    public class SySet_DirectedSegment<T1> : SySet<T1>
        where T1 : SyElement
    {


        #region プロパティ

        public string Word { get { return this.word; } }        private string word;

        /// <summary>
        /// 枡。
        /// </summary>
        public IEnumerable<T1> Elements
        {
            get
            {
                return this.orderedItems;
            }
        }

        private List<T1> orderedItems;


        /// <summary>
        /// 親集合はありません。
        /// </summary>
        public IEnumerable<SySet<T1>> Supersets
        {
            get
            {
                List<SySet<T1>> supersets = new List<SySet<T1>>();

                // 親集合はありません。

                return supersets;
            }
        }

        /// <summary>
        /// 先後。
        /// </summary>
        private object playerside;
        public object Playerside { get { return this.playerside; } }

        /// <summary>
        /// 向き。
        /// </summary>
        private object hogaku;
        public object Hogaku { get { return this.hogaku; } }

        #endregion




        /// <summary>
        /// クローン可能
        /// </summary>
        /// <param name="startPoint">原点</param>
        /// <param name="pside"></param>
        /// <param name="muki"></param>
        public SySet_DirectedSegment(string word, object playerside, object hogaku, List<T1> orderedItems)
        {
            this.word = word;
            this.playerside = playerside;
            this.hogaku = hogaku;

            this.orderedItems = new List<T1>();
            foreach (T1 item in orderedItems)
            {
                this.orderedItems.Add(item);
            }
        }

        public int Nagasa()
        {
            return this.orderedItems.Count;
        }

        public SySet<T1> Clone()
        {
            // クローンを作成します。
            SySet<T1> clone = new SySet_DirectedSegment<T1>(
                this.word,
                this.playerside,
                this.hogaku,
                this.orderedItems
            );


            return clone;
        }



        public int Count
        {
            get
            {
                return this.orderedItems.Count;
            }
        }

        /// <summary>
        /// this - b = this
        /// 
        /// 要素が含まれていれば、切ります。
        ///
        /// 例えば、「10→40→30→20→50」という順番で数字を持っているとき、
        /// 「30」がリムーブされれば、
        /// 「10→40」が残ります。
        /// </summary>
        /// <param name="b"></param>
        public void Minus_Closed(T1 b, DLGT_SyElement_BynaryOperate dlgt_equals)
        {

            int i=0;
            foreach (T1 thisElement in this.orderedItems)
            {
                if (dlgt_equals(thisElement,b))
                {
                    // b 以降を切り捨てます。
                    this.orderedItems.RemoveRange(i, this.orderedItems.Count - i);
                    break;
                }

                i++;
            }
        }

        /// <summary>
        /// もし順序があるならば、「ａ　＝　１→２→３→４」のときに
        /// 「ａ　ＲｅｍｏｖｅＥｌｅｍｅｎｔ＿ＯｖｅｒＴｈｅｒｅ（　２　）」とすれば、
        /// 答えは「３→４」
        /// となる操作がこれです。
        /// 
        /// ｂを含めず、それより後ろを切る、という操作です。
        /// 順序がなければ、ＲｅｍｏｖｅＥｌｅｍｅｎｔと同等です。
        /// </summary>
        /// <param name="masuHandle"></param>
        public void Minus_Opened(T1 b, DLGT_SyElement_BynaryOperate dlgt_equals)
        {
            int i = 0;
            foreach (T1 thisElement in this.orderedItems)
            {
                if (dlgt_equals( thisElement,b))
                {
                    // この次で切ります。

                    if(this.orderedItems.Count<=i+1)
                    {
                        break;
                    }
                    this.orderedItems.RemoveRange(i+1, this.orderedItems.Count - (i+1));

                    break;
                }

                i++;
            }
        }


        #region 一致判定系

        /// <summary>
        /// ************************************************************************************************************************
        /// この図形に、指定のドットが含まれているか。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public bool Contains(T1 element)
        {
            bool matched = true;

            matched = this.orderedItems.Contains(element);

            return matched;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// この図形の指定の場所に、指定のドットが全て打たれているか。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public bool ContainsAll(SySet<T1> superset2)
        {
            bool matched = true;

            foreach (T1 element in superset2.Elements)
            {
                if (!this.orderedItems.Contains(element))
                {
                    matched = false;
                    break;
                }
            }

            return matched;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 空集合なら真です。
        /// ************************************************************************************************************************
        /// </summary>
        /// <returns></returns>
        public bool IsEmptySet()
        {
            bool empty = true;

            // 直接の要素
            if (0 < this.orderedItems.Count)
            {
                empty = false;
                goto gt_EndMethod;
            }

            // 親集合はありませんが、一応コードを書いておきます。
            foreach (SySet<T1> superset in this.Supersets)
            {
                if (!superset.IsEmptySet())
                {
                    empty = false;
                    goto gt_EndMethod;
                }
            }

        gt_EndMethod:
            return empty;
        }

        #endregion

        #region 追加系

        public void AddElement(T1 element)
        {
            this.orderedItems.Add(element);//マス番号の重複あり
        }

        public void Add(T1 element)
        {
            this.orderedItems.Add(element);//マス番号の重複あり
        }


        public void AddSupersets(SySet<T1> superset)
        {
            foreach (T1 element in superset.Elements)
            {
                this.AddElement(element);
            }
        }

        /// <summary>
        /// this - b = c
        /// 
        /// この集合のメンバーから、指定の集合のメンバーを削除します。
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public SySet<T1> Minus_Closed(SySet<T1> b, DLGT_SyElement_BynaryOperate dlgt_equals)
        {
            // クローンを作成します。
            SySet<T1> c = this.Clone();

            // 指定の要素が含まれているかどうか１つ１つ調べます。
            foreach (T1 bElement in b.Elements)
            {
                // 要素を１つ１つ削除していきます。
                c.Minus_Closed(bElement,dlgt_equals);
            }

            return c;
        }

        /// <summary>
        /// this -= b
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public void MinusMe_Opened(SySet<T1> b, DLGT_SyElement_BynaryOperate dlgt_equals)
        {
            // このセットの中にある、スーパーセット１つ１つにも、Minus_OverThere をします。
            foreach (SySet<T1> superset2 in this.Supersets)
            {
                superset2.MinusMe_Opened(b, dlgt_equals);
            }

            // 指定の要素が含まれているかどうか１つ１つ調べます。
            foreach (T1 bElement in b.Elements)
            {
                // bを含まない、それより後ろの要素を　丸ごと削除します。
                this.Minus_Opened(bElement, dlgt_equals);
            }

        }

        #endregion




        //public bool BynaryOperate(SyElement operand1, SyElement operand2, DLGT_SyElement_BynaryOperate dlgt_SyElement_Operate)
        //{
        //    return dlgt_SyElement_Operate(operand1, operand2);
        //}

    }


}
