using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grayscale.P743_FvLearn____.L___490_StopLearning
{
    public interface StopLearning
    {

        /// <summary>
        /// Stop_learning.txt ファイルへのパス。
        /// </summary>
        string StopLearningFilePath { get; }
        void SetStopLearningFilePath(string value);

        /// <summary>
        /// 停止させるなら真。
        /// </summary>
        /// <returns></returns>
        bool IsStop();
    }
}
