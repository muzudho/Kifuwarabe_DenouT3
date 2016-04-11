using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grayscale.P743_FvLearn____.L___400_54List;

namespace Grayscale.P743_FvLearn____.L400____54List
{
    /// <summary>
    /// 40+14要素のリスト。
    /// ４０枚の駒、または１４種類の持駒。多くても５４要素。
    /// </summary>
    public class N40t14ListImpl : N40t14List
    {

        public int P40Next { get { return this.p40Next; } }
        public void SetP40Next(int value)
        {
            this.p40Next = value;
        }
        private int p40Next;

        /// <summary>
        /// ソートしていなくても構わないものとします。
        /// </summary>
        public int[] P40List_unsorted { get { return this.p40List_unsorted; } }
        public void SetP40List_Unsorted(int[] value)
        {
            //Array.Sort(value);//昇順
            this.p40List_unsorted = value;
        }
        private int[] p40List_unsorted;


        public int P14Next { get { return this.p14Next; } }
        public void SetP14Next(int value)
        {
            this.p14Next = value;
        }
        private int p14Next;

        /// <summary>
        /// ソートしていなくても構わないものとします。
        /// </summary>
        public int[] P14List_unsorted { get { return this.p14List_unsorted; } }
        public void SetP14List_Unsorted(int[] value)
        {
            //Array.Sort(value);//昇順
            this.p14List_unsorted = value;
        }
        private int[] p14List_unsorted;


        public N40t14ListImpl()
        {
            this.SetP40Next( 0);
            this.SetP40List_Unsorted( new int[40]);

            this.SetP14Next( 0);
            this.SetP14List_Unsorted( new int[14]);
        }

        ///// <summary>
        ///// 2つに分かれているのが嫌な場合。
        ///// </summary>
        ///// <param name="out_p54List_asc">昇順のリスト。</param>
        ///// <param name="out_p54Next"></param>
        //public void To54List_Unsorted(out int[] out_p54List_asc, out int out_p54Next)
        //{
        //    out_p54Next = this.P40Next+this.P14Next;
        //    out_p54List_asc = new int[out_p54Next];

        //    Array.Copy(this.P40List_unsorted, out_p54List_asc, this.P40Next);
        //    Array.Copy(this.P14List_unsorted, 0, out_p54List_asc, this.P40Next, this.P14Next);

        //    // 昇順に並べ替えます。
        //    //Array.Sort(out_p54List_asc);
        //}
    }
}
