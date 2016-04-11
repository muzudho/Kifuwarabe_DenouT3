using Grayscale.P743_FvLearn____.L___490_StopLearning;
using System.IO;
using System.Windows.Forms;

namespace Grayscale.P743_FvLearn____.L490____StopLearning
{
    public class StopLearningImpl : StopLearning
    {

        /// <summary>
        /// Stop_learning.txt ファイルへのパス。
        /// </summary>
        public string StopLearningFilePath { get { return this.stopLearningFilePath; } }
        public void SetStopLearningFilePath(string value)
        {
            this.stopLearningFilePath = value;
        }
        private string stopLearningFilePath;

        public StopLearningImpl(string stopLearningFilePath)
        {
            this.stopLearningFilePath = stopLearningFilePath;
        }

        /// <summary>
        /// 停止させるなら真。
        /// </summary>
        /// <returns></returns>
        public bool IsStop()
        {
            bool isStop = false;

            if (File.Exists(this.StopLearningFilePath))
            {
                DialogResult result = MessageBox.Show("[" + this.StopLearningFilePath + "]ファイルを検知しました。\n" +
                    "自動学習を停止しますか？", "info", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                switch (result)
                {
                    case DialogResult.OK: isStop = true; break;
                    default: break;//続行
                }
            }

            return isStop;
        }

    }
}
