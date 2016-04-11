using Grayscale.P003_Log________.L500____Struct;
using Grayscale.P323_Sennitite__.L___500_Struct;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Grayscale.P323_Sennitite__.L500____Struct
{
    public class SennititeCounterImpl : SennititeCounter
    {
        /// <summary>
        /// 次に足したら、４回目以上になる場合、真。
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        public bool IsNextSennitite(ulong hash)
        {
            bool isNextSennitite;
            if (this.douituKyokumenCounterDictionary.ContainsKey(hash) && 2<this.douituKyokumenCounterDictionary[hash])
            {
                isNextSennitite = true;

#if DEBUG
                // ログ出力
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("----------------------------------------");
                    sb.AppendLine("is千日手☆！:" + this.douituKyokumenCounterDictionary[hash].ToString());
                    sb.AppendLine(this.Dump_Format());
                    this.WriteLog(sb.ToString());
                }
#endif
            }
            else
            {
                isNextSennitite = false;
            }
            return isNextSennitite;
        }
        public void CountDown(ulong hash, string hint)
        {
            if (this.douituKyokumenCounterDictionary.ContainsKey(hash))
            {
                // カウントダウン。
                this.douituKyokumenCounterDictionary[hash]--;

#if DEBUG
                // ログ出力
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("----------------------------------------");
                    sb.AppendLine("千日手、カウントダウン☆！：" + hint + ":" + this.douituKyokumenCounterDictionary[hash].ToString());
                    sb.AppendLine(this.Dump_Format());
                    this.WriteLog(sb.ToString());
                }
#endif
            }
            else
            {
                // エラー
                throw new Exception("指定のハッシュは存在せず、カウントダウンできませんでした。hash=[" + hash + "]");
            }
        }
        public void CountUp_New(ulong hash, string hint)
        {
            if (this.douituKyokumenCounterDictionary.ContainsKey(hash))
            {
                // カウントアップ。
                this.douituKyokumenCounterDictionary[hash]++;

#if DEBUG
                // ログ出力
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("----------------------------------------");
                    sb.AppendLine("千日手カウントアップした☆！：" + hint + ":" + this.douituKyokumenCounterDictionary[hash].ToString());
                    sb.AppendLine(this.Dump_Format());
                    this.WriteLog(sb.ToString());
                }
#endif
            }
            else
            {
                // 新規追加。
                this.douituKyokumenCounterDictionary.Add(hash, 1);//1スタート。

#if DEBUG
                // ログ出力
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("----------------------------------------");
                    sb.AppendLine("千日手、新局面追加☆！：" + hint + ":" + this.douituKyokumenCounterDictionary[hash].ToString());
                    sb.AppendLine(this.Dump_Format());
                    this.WriteLog(sb.ToString());
                }
#endif
            }
        }
        public void CountUp_Overwrite(ulong hash_old, ulong hash_new, string hint)
        {
            // カウントダウン。
            this.CountDown(hash_old, hint+"/CountUp_Overwrite");

            // カウントアップ。
            this.CountUp_New(hash_new, hint+"/CountUp_Overwrite");
        }
        /// <summary>
        /// 同一局面カウンター。
        /// key:盤面のゾブリッシュ・ハッシュ値
        /// value:出現回数
        /// </summary>
        private Dictionary<ulong, int> douituKyokumenCounterDictionary;

        public SennititeCounterImpl()
        {
            this.douituKyokumenCounterDictionary = new Dictionary<ulong, int>();
        }

        /// <summary>
        /// 棋譜のクリアーに合わせます。
        /// </summary>
        public void Clear()
        {
            this.douituKyokumenCounterDictionary.Clear();
        }

        /// <summary>
        /// 内部状態を全て出力します。
        /// </summary>
        /// <returns></returns>
        private string Dump_Format()
        {
            StringBuilder sb = new StringBuilder();

            // 現在のプロセス名
            sb.Append("プロセス名:");
            sb.AppendLine(Process.GetCurrentProcess().ProcessName);

            foreach (KeyValuePair<ulong, int> entry in this.douituKyokumenCounterDictionary)
            {
                sb.Append(entry.Key);
                sb.Append(":");
                sb.Append(entry.Value);
                sb.AppendLine("");
            }

            return sb.ToString();
        }

        /// <summary>
        /// プロセス名を見て、ログ・ファイルを切り替えます。
        /// TODO: 名称変更した場合は、その都度　書き替えてください。
        /// </summary>
        /// <param name="text"></param>
        private void WriteLog(string text)
        {
            string processName = Process.GetCurrentProcess().ProcessName;

            if (processName=="Grayscale.P800_ShogiGuiVs.vshost")
            {
                Util_OwataMinister.CsharpGui_SENNITITE.Logger.WriteLine_AddMemo(text);
            }
            else if (processName == "Grayscale.P500_ShogiEngine_KifuWarabe")
            {
                Util_OwataMinister.ENGINE_SENNITITE.Logger.WriteLine_AddMemo(text);
            }
            else
            {
                // 名称変更したことを忘れていた場合は、デフォルトの書き出し先へ退避。
                Util_OwataMinister.DEFAULT_SENNITITE.Logger.WriteLine_AddMemo(text);
            }
        }


        ///// <summary>
        ///// FIXME: 初手から、計算しなおします。
        ///// </summary>
        //public void Recount_All(KifuTree kifuTree)
        //{
        //    //----------------------------------------
        //    // 初手から、全ノードを取得
        //    //----------------------------------------
        //    Node<Starbeamable, KyokumenWrapper> node1 = kifuTree.GetRoot();
        //    List<Node<Starbeamable, KyokumenWrapper>> nodeList = new List<Node<Starbeamable, KyokumenWrapper>>();
        //    this.Recursive(node1, nodeList);

        //    //----------------------------------------
        //    // 同一局面が何回出たかカウント。
        //    //----------------------------------------


        //}

        //private void Recursive(Node<Starbeamable, KyokumenWrapper> node1, List<Node<Starbeamable, KyokumenWrapper>> nodeList)
        //{
        //    node1.Foreach_ChildNodes((string key2, Node<Starbeamable, KyokumenWrapper> node2, ref bool toBreak2) =>
        //    {
        //        nodeList.Add(node2);
        //        this.Recursive(node2, nodeList);
        //    });
        //}
    }
}
