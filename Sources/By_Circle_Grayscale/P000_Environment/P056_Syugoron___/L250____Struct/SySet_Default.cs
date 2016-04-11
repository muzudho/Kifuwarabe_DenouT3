using Grayscale.P056_Syugoron___.L___250_Struct;
using System.Collections.Generic;
using System.Text;

namespace Grayscale.P056_Syugoron___.L250____Struct
{

    /// <summary>
    /// 順序を持たない集合です。
    /// 
    /// 「要素　ａ、ｂ、ｃ　を持つ集合　Ａ」といった構造です。
    /// 
    /// </summary>
    public class SySet_Default<T1> : SySet<T1>
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
                // 順序を保たなくても構わない、全要素


                // 全要素
                HashSet<T1> elements2 = new HashSet<T1>();

                // 自分が直接示している要素をコピー
                {


                    foreach (T1 element in this.elements_)
                    {
                        elements2.Add(element);
                    }
                }

                // 自分が示している集合が示している要素
                foreach (SySet<T1> spuerset in this.supersets_)
                {
                    foreach (T1 element2 in spuerset.Elements)
                    {
                        if (!elements2.Contains(element2))//マス番号の重複は除外
                        {
                            elements2.Add(element2);
                        }
                    }
                }

                return elements2;
            }
        }
        private HashSet<T1> elements_;

        public HashSet<T1> Elements_ { get { return this.elements_; } }

        public IEnumerable<SySet<T1>> Supersets
        {
            get
            {
                // 順序を保たなくても構わない、全要素
                

                // 全要素
                HashSet<SySet<T1>> supersets2 = new HashSet<SySet<T1>>();

                //// 自分が直接示している要素をコピー
                //SySet<T1> myElements = new SySet_Default<T1>();
                //{
                //    foreach (T1 element in this.elements_)
                //    {
                //        myElements.AddElement(element);
                //    }
                //}

                // 自分が示している集合が示している要素
                foreach (SySet<T1> superset in this.supersets_)
                {
                    if (!supersets2.Contains(superset))//マス番号の重複は除外
                    {
                        supersets2.Add(superset);
                    }
                }

                return supersets2;
            }
        }

        private List<SySet<T1>> supersets_;

        #endregion


        public SySet_Default(string word)
        {
            this.word = word;
            this.elements_ = new HashSet<T1>();
            this.supersets_ = new List<SySet<T1>>();
        }


        public SySet<T1> Clone()
        {
            SySet<T1> clone = new SySet_Default<T1>(this.Word);

            // 要素をコピーします。
            foreach (T1 element in this.elements_)
            {
                clone.AddElement(element);
            }

            // 親集合のクローンをコピーします。
            foreach (SySet<T1> superset in this.supersets_)
            {
                clone.AddSupersets(superset.Clone());
            }

            return clone;
        }


        public int Count
        {
            get
            {
                int count = 0;

                count += this.elements_.Count;

                foreach (SySet<T1> superset in this.supersets_)
                {
                    count += superset.Count;
                }

                return count;
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

            matched = this.elements_.Contains(element);

            return matched;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// この集合は、指定した要素を全てもっているか。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public bool ContainsAll(SySet<T1> superset2)
        {
            bool matched = true;

            foreach (T1 element in superset2.Elements)
            {
                if (!this.elements_.Contains(element))
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
            if (0 < this.elements_.Count)
            {
                empty = false;
                goto gt_EndMethod;
            }

            // 親集合
            foreach (SySet<T1> superset in this.supersets_)
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



        #region 追加・削除系

        /// <summary>
        /// 要素の仲間を加えます。
        /// </summary>
        /// <param name="element"></param>
        public void AddElement(T1 element)
        {
            //if (Okiba.ShogiBan == Converter04.Masu_ToOkiba(masu))
            //{
                if (!this.elements_.Contains(element))//マス番号の重複を除外
                {
                    this.elements_.Add(element);
                }
            //}
        }

        /// <summary>
        /// 親集合の仲間を加えます。
        /// </summary>
        /// <param name="src_superset"></param>
        public void AddSupersets(SySet<T1> src_superset)
        {
            this.supersets_.Add(src_superset);
        }

        /// <summary>
        /// this - b = this
        /// 
        /// 要素を仲間から外します。
        /// </summary>
        /// <param name="bElement"></param>
        public void Minus_Closed(T1 bElement, DLGT_SyElement_BynaryOperate dlgt_equals)
        {
            // 要素から削除
            this.elements_.Remove(bElement);

            // 親集合から削除
            foreach (SySet<T1> thisSuperset in this.supersets_)
            {
                thisSuperset.Minus_Closed(bElement, dlgt_equals);
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
        public void Minus_Opened(T1 bElement, DLGT_SyElement_BynaryOperate dlgt_equals)
        {
            this.Minus_Closed(bElement, dlgt_equals);
        }

        /// <summary>
        /// this - b = c
        /// 
        /// この集合から、指定の要素を全て削除した結果を返します。
        /// 
        /// この集合自身は変更しません。
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public SySet<T1> Minus_Closed(SySet<T1> b, DLGT_SyElement_BynaryOperate dlgt_equals)
        {
            // クローンを作ります。
            SySet<T1> c = this.Clone();

            // 要素の削除
            {
                foreach (T1 element in b.Elements)
                {
                    c.Minus_Closed(element,dlgt_equals);
                }
            }

            return c;
        }

        /// <summary>
        /// this -=overThere b
        /// 
        /// この集合から、指定の要素を全て削除した結果を返します。
        /// 
        /// この集合自身は変更しません。
        /// 
        /// 順序はないが、Minus無印と同じではない☆
        /// 最初の１個（原点）は削除してはいけない☆
        /// </summary>
        /// <param name="targetMasus"></param>
        /// <returns></returns>
        public void MinusMe_Opened(SySet<T1> b, DLGT_SyElement_BynaryOperate dlgt_equals)
        {

            // このセットの中にある、スーパーセット１つ１つにも、Minus_OverThere をします。
            foreach (SySet<T1> superset2 in this.Supersets)
            {
                superset2.MinusMe_Opened(b, dlgt_equals);
            }

            if (b is SySet_Ordered<T1>)
            {
                // 方向をもつ集合で　引こうとした場合だけ有効です。

                // 要素の削除
                {
                    int index = 0;
                    foreach (T1 element in b.Elements)
                    {
                        if (index == 0)
                        {
                            // 最初の要素は、削除しません。
                            goto gt_Next;
                        }

                        this.Minus_Closed(element,dlgt_equals);

                    gt_Next:
                        index++;
                    }
                }
            }
            // 方向を持っていない集合では、Minus_OverThere は無効とします。
        }

        #endregion











        public List<T1> ToList()
        {
            List<T1> anotherCollection = new List<T1>();

            foreach (T1 element in this.Elements)
            {
                anotherCollection.Add(element);
            }

            return anotherCollection;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach(T1 element in this.Elements)
            {
                sb.Append(element.ToString());
                sb.Append(" ");
            }

            return sb.ToString();
        }



        //public bool BynaryOperate(SyElement operand1, SyElement operand2, DLGT_SyElement_BynaryOperate dlgt_SyElement_Operate)
        //{
        //    return dlgt_SyElement_Operate(operand1, operand2);
        //}

    }
}
