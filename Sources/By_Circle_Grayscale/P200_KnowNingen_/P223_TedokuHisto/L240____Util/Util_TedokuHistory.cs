using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P223_TedokuHisto.L___250_Struct;
using System.Collections.Generic;
using System.Text;
using Grayscale.P055_Conv_Sy.L500____Converter;

#if DEBUG
using System.IO;
#endif

namespace Grayscale.P223_TedokuHisto.L240____Util
{

    public abstract class Util_TedokuHistory
    {
        /// <summary>
        /// 作りかけ。
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="tedokuHistory"></param>
        public static void WriteLog(string filepath, TedokuHistory tedokuHistory)
        {
            StringBuilder sb = new StringBuilder();
            int i;

            //
            // 歩
            //
            i = 0;
            foreach(List<SyElement> list in tedokuHistory.Fu___)
            {
                sb.Append("Fu___[");
                sb.Append(string.Format("{0,2:0}", i));
                sb.Append("] ");
                foreach (SyElement masu in list)
                {
                    sb.Append(Conv_Sy.Query_Word( masu.Bitfield));
                    sb.Append(",");
                }
                sb.AppendLine();
                i++;
            }

            //
            // 香
            //
            i = 0;
            foreach (List<SyElement> list in tedokuHistory.Kyo__)
            {
                sb.Append("Kyo__[");
                sb.Append(string.Format("{0,2:0}", i));
                sb.Append("] ");
                foreach (SyElement masu in list)
                {
                    sb.Append(Conv_Sy.Query_Word( masu.Bitfield));
                    sb.Append(",");
                }
                sb.AppendLine();
                i++;
            }

            //
            // 桂
            //
            i = 0;
            foreach (List<SyElement> list in tedokuHistory.Kei__)
            {
                sb.Append("Kei__[");
                sb.Append(string.Format("{0,2:0}", i));
                sb.Append("] ");
                foreach (SyElement masu in list)
                {
                    sb.Append(Conv_Sy.Query_Word( masu.Bitfield));
                    sb.Append(",");
                }
                sb.AppendLine();
                i++;
            }


            //
            // 銀
            //
            i = 0;
            foreach (List<SyElement> list in tedokuHistory.Gin__)
            {
                sb.Append("Gin__[");
                sb.Append(string.Format("{0,2:0}", i));
                sb.Append("] ");
                foreach (SyElement masu in list)
                {
                    sb.Append(Conv_Sy.Query_Word( masu.Bitfield));
                    sb.Append(",");
                }
                sb.AppendLine();
                i++;
            }

            //
            // 金
            //
            i = 0;
            foreach (List<SyElement> list in tedokuHistory.Kin__)
            {
                sb.Append("Kin__[");
                sb.Append(string.Format("{0,2:0}", i));
                sb.Append("] ");
                foreach (SyElement masu in list)
                {
                    sb.Append(Conv_Sy.Query_Word( masu.Bitfield));
                    sb.Append(",");
                }
                sb.AppendLine();
                i++;
            }

            //
            // 玉
            //
            {
                sb.Append("Gyoku[--] ");
                foreach (SyElement masu in tedokuHistory.Gyoku)
                {
                    sb.Append(Conv_Sy.Query_Word( masu.Bitfield));
                    sb.Append(",");
                }
                sb.AppendLine();
            }

            //
            // 飛
            //
            i = 0;
            foreach (List<SyElement> list in tedokuHistory.Hisya)
            {
                sb.Append("Hisya[");
                sb.Append(string.Format("{0,2:0}", i));
                sb.Append("] ");
                foreach (SyElement masu in list)
                {
                    sb.Append(Conv_Sy.Query_Word( masu.Bitfield));
                    sb.Append(",");
                }
                sb.AppendLine();
                i++;
            }

            //
            // 角
            //
            i = 0;
            foreach (List<SyElement> list in tedokuHistory.Kaku_)
            {
                sb.Append("Kaku_[");
                sb.Append(string.Format("{0,2:0}", i));
                sb.Append("] ");
                foreach (SyElement masu in list)
                {
                    sb.Append(Conv_Sy.Query_Word( masu.Bitfield));
                    sb.Append(",");
                }
                sb.AppendLine();
                i++;
            }

#if DEBUG
            File.WriteAllText(filepath, sb.ToString());
#endif
        }
    }
}
