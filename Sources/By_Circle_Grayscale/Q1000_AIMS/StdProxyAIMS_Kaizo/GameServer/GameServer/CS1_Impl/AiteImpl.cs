using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;

namespace Sample.GameServer
{

    /// <summary>
    /// 通信相手☆ｗｗ クライアントのことだぜ☆ｗｗ
    /// </summary>
    public class AiteImpl
    {

        /// <summary>
        /// 相手☆ｗｗ
        /// </summary>
        public Process Aite { get; set; }

        /// <summary>
        /// まずプロセスを作るんだぜ☆
        /// </summary>
        /// <param name="filePath"></param>
        public void CreateAite(string filePath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();

            startInfo.FileName = filePath; // 実行するファイル名
            //startInfo.CreateNoWindow = true; // コンソール・ウィンドウを開かない
            startInfo.UseShellExecute = false; // シェル機能を使用しない
            startInfo.RedirectStandardInput = true;//標準入力をリダイレクト
            startInfo.RedirectStandardOutput = true; // 標準出力をリダイレクト

            this.Aite = Process.Start(startInfo); // アプリの実行開始
        }

        /// <summary>
        /// プロセスを作ったあと、スタートさせるんだぜ☆ｗｗ
        /// </summary>
        public void Start()
        {

            try
            {
                // 非同期受信スタート☆！
                this.Aite.BeginOutputReadLine();

                this.Aite.StandardInput.WriteLine("ちゅん☆ちゅん☆ちゅん☆ｗｗ");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().Name + "：" + ex.Message);
            }

        }

        /// <summary>
        /// 相手にメッセージを送ります。
        /// </summary>
        public void Send(string message)
        {
            this.Aite.StandardInput.WriteLine(message);
        }

        /// <summary>
        /// 相手が起動しているか否かです。
        /// </summary>
        /// <returns></returns>
        public bool IsLive()
        {
            return null != this.Aite && !this.Aite.HasExited;
        }


    }
}
