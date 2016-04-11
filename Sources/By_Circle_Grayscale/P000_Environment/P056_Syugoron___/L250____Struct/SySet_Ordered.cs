using Grayscale.P056_Syugoron___.L___250_Struct;
using System.Collections.Generic;

namespace Grayscale.P056_Syugoron___.L250____Struct
{

    /// <summary>
    /// 順序を保つ集合です。
    /// 
    /// 「ａ→ｂ→ｃ　の順番で要素を持つ集合　Ａ」といった構造です。
    /// 
    /// </summary>
    public class SySet_Ordered<T1> : SySet<T1>
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


                    foreach (T1 element1 in this.elements_)
                    {
                        elements2.Add(element1);
                    }
                }

                // 自分が示している集合が示している要素
                foreach (SySet<T1> superset in this.supersets_)
                {
                    foreach (T1 element2 in superset.Elements)
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
        private List<T1> elements_;

        public List<T1> Elements_ { get { return this.elements_; } }

        public IEnumerable<SySet<T1>> Supersets
        {
            get
            {
                // 順序を保たなくても構わない、全要素
                

                // 全要素
                HashSet<SySet<T1>> supersets2 = new HashSet<SySet<T1>>();

                //// 自分が直接示している要素をコピー
                //SySet<T1> myElements = new SySet_Ordered<T1>();
                //{
                //    foreach (T1 masuHandle1 in this.elements_)
                //    {
                //        myElements.AddElement(masuHandle1);
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


        public SySet_Ordered(string word)
        {
            this.word = word;
            this.elements_ = new List<T1>();
            this.supersets_ = new List<SySet<T1>>();
        }

        /// <summary>
        /// クローンを作成します。
        /// </summary>
        /// <returns></returns>
        public SySet<T1> Clone()
        {
            SySet<T1> clone = new SySet_Ordered<T1>(this.word);

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
            if (!this.elements_.Contains(element))//マス番号の重複を除外
            {
                this.elements_.Add(element);
            }
        }

        /// <summary>
        /// 親集合の仲間を加えます。
        /// </summary>
        /// <param name="superset"></param>
        public void AddSupersets(SySet<T1> superset)
        {
            this.supersets_.Add(superset);
        }

        /// <summary>
        /// this - b = this
        /// 
        /// 要素を仲間から外します。
        /// </summary>
        /// <param name="b"></param>
        public void Minus_Closed(T1 b, DLGT_SyElement_BynaryOperate dlgt_equals)
        {
            // 削除する要素を検索します。
            int index = this.elements_.IndexOf(b);
            if(-1==index)
            {
                goto gt_Supersets;
            }

            // 削除したい要素を含む、その後ろごと丸ごと削除
            this.elements_.RemoveRange( index, this.elements_.Count-index);

        gt_Supersets:

            // 親集合から削除
            foreach (SySet<T1> thisSuperset in this.supersets_)
            {
                thisSuperset.Minus_Closed(b, dlgt_equals);
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
            // 削除する要素を検索します。
            int index = this.elements_.IndexOf(b);
            if (-1 == index)
            {
                goto gt_Supersets;
            }

            if(this.elements_.Count<=index+1)
            {
                goto gt_Supersets;
            }

            // 削除したい要素を含めず、それより後ろを丸ごと削除
            this.elements_.RemoveRange(index+1, this.elements_.Count - (index+1));

        gt_Supersets:

            // 親集合から削除
            foreach (SySet<T1> thisSuperset in this.supersets_)
            {
                thisSuperset.Minus_Closed(b,dlgt_equals);
            }
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
            // クローンを作成します。
            SySet<T1> c = this.Clone();

            // 要素の削除
            foreach (T1 bElement in b.Elements)
            {
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

            foreach (T1 bElement in b.Elements)
            {
                // bを含まない、それより後ろの要素を丸ごと削除します。
                this.Minus_Opened(bElement, dlgt_equals);
            }
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



        //public bool BynaryOperate(SyElement operand1, SyElement operand2, DLGT_SyElement_BynaryOperate dlgt_SyElement_Operate)
        //{
        //    return dlgt_SyElement_Operate(operand1, operand2);
        //}

    }
}
