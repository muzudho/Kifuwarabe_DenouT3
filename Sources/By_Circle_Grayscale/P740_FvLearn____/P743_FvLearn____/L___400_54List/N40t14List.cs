using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grayscale.P743_FvLearn____.L___400_54List
{
    /// <summary>
    /// 40+14要素のリスト。
    /// ４０枚の駒、または１４種類の持駒。多くても５４要素。
    /// </summary>
    public interface N40t14List
    {

        int P40Next { get; }
        void SetP40Next(int value);

        /// <summary>
        /// ソートしていなくても構わない使い方をしてください。
        /// </summary>
        int[] P40List_unsorted { get; }
        void SetP40List_Unsorted(int[] value);


        int P14Next { get; }
        void SetP14Next(int value);

        /// <summary>
        /// ソートしていなくても構わない使い方をしてください。
        /// </summary>
        int[] P14List_unsorted { get; }
        void SetP14List_Unsorted(int[] value);


        ///// <summary>
        ///// 2つに分かれているのが嫌な場合。
        ///// </summary>
        //void To54List_Unsorted(out int[] out_p54List, out int out_p54Next);

    }
}
