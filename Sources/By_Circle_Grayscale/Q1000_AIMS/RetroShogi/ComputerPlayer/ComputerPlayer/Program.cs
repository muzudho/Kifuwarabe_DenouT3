using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample.ComputerPlayer
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {

            string serverSaid; // ゲームサーバー からのメッセージ。
            while (true)
            {
                serverSaid = Console.ReadLine();

                if (null != serverSaid) // しゃべったあ☆！
                {
                    if (serverSaid == "ちゅん☆ちゅん☆ちゅん☆ｗｗ")
                    {
                        // 書き出せば、サーバー に届きます。
                        Console.WriteLine("むくり☆ｗｗ");
                        System.Threading.Thread.Sleep(1000);

                        break; // でもすぐに寝る
                    }
                }
            }

            Console.WriteLine("寝るぜ☆ｗｗ");
            System.Threading.Thread.Sleep(1000);
        }
    }
}
