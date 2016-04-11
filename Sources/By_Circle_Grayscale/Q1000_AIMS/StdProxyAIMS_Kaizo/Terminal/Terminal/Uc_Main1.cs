using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace Sample.Terminal
{
    public partial class Uc_Main1 : UserControl
    {
        private System.Timers.Timer Timer { get; set; }

        public Uc_Main1()
        {
            InitializeComponent();
        }

        private void Uc_Main1_Load(object sender, EventArgs e)
        {
            // 精度の高いタイマーの生成
            this.Timer = new System.Timers.Timer();
            this.Timer.Elapsed += new ElapsedEventHandler(OnElapsed_TimersTimer);
            this.Timer.Interval = 20;

            // タイマーを開始
            this.Timer.Start();

            // タイマーを停止
            //timer.Stop();
        }

        static void OnElapsed_TimersTimer(object sender, ElapsedEventArgs e)
        {
            string serverSaid = Console.ReadLine();

            if (null!=serverSaid) // しゃべったあ☆！
            {
                //MessageBox.Show("["+serverSaid+"]", "サーバーがしゃべったあ☆");

                if (serverSaid == "ちゅん☆ちゅん☆ちゅん☆ｗｗ")
                {
                    // 書き出せば、サーバー に届きます。
                    Console.WriteLine("むくり☆ｗｗ");
                }
            }
        }

    }
}
