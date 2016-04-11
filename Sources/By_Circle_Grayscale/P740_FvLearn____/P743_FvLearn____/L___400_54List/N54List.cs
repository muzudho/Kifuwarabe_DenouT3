using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grayscale.P743_FvLearn____.L___400_54List
{
    public interface N54List
    {

        int P54Next { get; }
        void SetP54Next(int value);

        /// <summary>
        /// ソートしていなくても構わない使い方をしてください。
        /// </summary>
        int[] P54List_unsorted { get; }
        void SetP54List_Unsorted(int[] value);


    }
}
