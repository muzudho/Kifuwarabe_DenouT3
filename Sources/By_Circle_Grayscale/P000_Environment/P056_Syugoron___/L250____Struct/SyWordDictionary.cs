using Grayscale.P056_Syugoron___.L___250_Struct;
using System.Collections.Generic;
using System.Text;


namespace Grayscale.P056_Syugoron___.L250____Struct
{


    /// <summary>
    /// 集合論用の、辞書ツール。
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    public class SyWordDictionary<T1>
        where T1 : SyElement
    {

        private Dictionary<string, SySet<T1>> dictionary;

        public SyWordDictionary()
        {
            this.dictionary = new Dictionary<string, SySet<T1>>();
        }

        public SySet<T1> GetWord(string newWord)
        {
            SySet<T1> result;

            if (this.dictionary.ContainsKey(newWord.Trim()))
            {
                result = this.dictionary[newWord.Trim()];
            }
            else
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// 単語登録
        /// </summary>
        /// <param name="newWord"></param>
        /// <param name="mean"></param>
        public void AddWord(string newWord, SySet<T1> mean)
        {

            if (this.dictionary.ContainsKey(newWord.Trim()))
            {
                // キーが既存なら
                this.dictionary[newWord.Trim()] = mean;
            }
            else
            {
                this.dictionary.Add(newWord.Trim(), mean);
            }

        }

        /// <summary>
        /// 辞書の内容を、JSON形式で出力します。
        /// </summary>
        /// <returns></returns>
        public string ExportJson()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");

            bool first = true;
            foreach(KeyValuePair<string,SySet<T1>> entry in this.dictionary)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    sb.Append(",");
                }

                sb.Append("\"");
                sb.Append(entry.Key);
                sb.Append("\":");
                sb.Append("\"");
                sb.Append("意味略");
                sb.Append("\"");
            }

            sb.Append("}");
            return sb.ToString();
        }
    }
}
