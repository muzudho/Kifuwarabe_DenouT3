using Grayscale.P003_Log________.L500____Struct;
using Grayscale.P005_Tushin_____.L500____Util;
using Grayscale.P542_Scoreing___.L___005_UsiLoop;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Grayscale.P571_KifuWarabe_.L250____UsiLoop
{

    /// <summary>
    /// USIの１番目のループです。
    /// </summary>
    public class UsiLoop1
    {
        private ShogiEngine Owner { get { return this.owner; } }
        private ShogiEngine owner;




        public UsiLoop1(ShogiEngine owner)
        {
            this.owner = owner;
        }

        public void AtStart()
        {
        }

        public PhaseResult_UsiLoop1 AtBody(out bool out_isTimeoutShutdown)
        {
            out_isTimeoutShutdown = false;
            PhaseResult_UsiLoop1 result_UsiLoop1;

            //
            // サーバーに noop を送ってもよいかどうかは setoption コマンドがくるまで分からないので、
            // 作ってしまっておきます。
            // 1回も役に立たずに Loop2 に行くようなら、正常です。
#if NOOPABLE
            NoopTimerImpl noopTimer = new NoopTimerImpl();
            noopTimer._01_BeforeLoop();
#endif
            
            while (true)
            {
                result_UsiLoop1 = PhaseResult_UsiLoop1.None;

                // 将棋サーバーから何かメッセージが届いていないか、見てみます。
                string line = Util_Message.Download_Nonstop();

                if (null == line)//次の行が無ければヌル。
                {
                    // メッセージは届いていませんでした。
                    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
#if NOOPABLE
                    bool isTimeoutShutdown_temp;
                    noopTimer._03_AtEmptyMessage(this.Owner, out isTimeoutShutdown_temp);
                    if (isTimeoutShutdown_temp)
                    {
                        //MessageBox.Show("ループ１でタイムアウトだぜ☆！");
                        out_isTimeoutShutdown = isTimeoutShutdown_temp;
                        result_UsiLoop1 = PhaseResult_UsiLoop1.TimeoutShutdown;
                        goto end_loop1;
                    }
#endif

                    goto gt_NextTime1;
                }

                // 通信ログは必ず取ります。
                Util_OwataMinister.ENGINE_NETWORK.Logger.WriteLine_C(line);

#if NOOPABLE
                noopTimer._04_AtResponsed(this.Owner, line);
#endif




                if ("usi" == line) { this.AtLoop_OnUsi(line, ref result_UsiLoop1); }
                else if (line.StartsWith("setoption")) { this.AtLoop_OnSetoption(line, ref result_UsiLoop1); }
                else if ("isready" == line) { this.AtLoop_OnIsready(line, ref result_UsiLoop1); }
                else if ("usinewgame" == line) { this.AtLoop_OnUsinewgame(line, ref result_UsiLoop1); }
                else if ("quit" == line) { this.AtLoop_OnQuit(line, ref result_UsiLoop1); }
                else
                {
                    //------------------------------------------------------------
                    // ○△□×！？
                    //------------------------------------------------------------
                    #region ↓詳説
                    //
                    // ／(＾×＾)＼
                    //

                    // 通信が届いていますが、このプログラムでは  聞かなかったことにします。
                    // USIプロトコルの独習を進め、対応／未対応を選んでください。
                    //
                    // ログだけ取って、スルーします。
                    #endregion
                }

                switch (result_UsiLoop1)
                {
                    case PhaseResult_UsiLoop1.Break:
                        goto end_loop1;

                    case PhaseResult_UsiLoop1.Quit:
                        goto end_loop1;

                    default:
                        break;
                }

            gt_NextTime1:
                ;
            }
        end_loop1:
            return result_UsiLoop1;
        }

        public void AtEnd()
        {
        }

        public void AtLoop_OnUsi(string line, ref PhaseResult_UsiLoop1 result_Usi)
        {
            //------------------------------------------------------------
            // あなたは USI ですか？
            //------------------------------------------------------------
            #region ↓詳説
            //
            // 図.
            //
            //      log.txt
            //      ┌────────────────────────────────────────
            //      ～
            //      │2014/08/02 1:31:35> usi
            //      │
            //
            //
            // 将棋所で [対局(G)]-[エンジン管理...]-[追加...] でファイルを選んだときに、
            // 送られてくる文字が usi です。
            #endregion


            //------------------------------------------------------------
            // エンジン設定ダイアログボックスを作ります
            //------------------------------------------------------------
            #region ↓詳説
            //
            // 図.
            //
            //      log.txt
            //      ┌────────────────────────────────────────
            //      ～
            //      │2014/08/02 23:40:15< option name 子 type check default true
            //      │2014/08/02 23:40:15< option name USI type spin default 2 min 1 max 13
            //      │2014/08/02 23:40:15< option name 寅 type combo default tiger var マウス var うし var tiger var ウー var 龍 var へび var 馬 var ひつじ var モンキー var バード var ドッグ var うりぼー
            //      │2014/08/02 23:40:15< option name 卯 type button default うさぎ
            //      │2014/08/02 23:40:15< option name 辰 type string default DRAGON
            //      │2014/08/02 23:40:15< option name 巳 type filename default スネーク.html
            //      │
            //
            //
            // 将棋所で [エンジン設定] ボタンを押したときに出てくるダイアログボックスに、
            //      ・チェックボックス
            //      ・スピン
            //      ・コンボボックス
            //      ・ボタン
            //      ・テキストボックス
            //      ・ファイル選択テキストボックス
            // を置くことができます。
            //
            #endregion
            this.Owner.Send("option name 子 type check default true");
            this.Owner.Send("option name USI type spin default 2 min 1 max 13");
            this.Owner.Send("option name 寅 type combo default tiger var マウス var うし var tiger var ウー var 龍 var へび var 馬 var ひつじ var モンキー var バード var ドッグ var うりぼー");
            this.Owner.Send("option name 卯 type button default うさぎ");
            this.Owner.Send("option name 辰 type string default DRAGON");
            this.Owner.Send("option name 巳 type filename default スネーク.html");


            //------------------------------------------------------------
            // USI です！！
            //------------------------------------------------------------
            #region ↓詳説
            //
            // 図.
            //
            //      log.txt
            //      ┌────────────────────────────────────────
            //      ～
            //      │2014/08/02 2:03:33< id name fugafuga 1.00.0
            //      │2014/08/02 2:03:33< id author hogehoge
            //      │2014/08/02 2:03:33< usiok
            //      │
            //
            // プログラム名と、作者名を送り返す必要があります。
            // オプションも送り返せば、受け取ってくれます。
            // usi を受け取ってから、5秒以内に usiok を送り返して完了です。
            #endregion
            this.Owner.Send("id name " + this.Owner.SeihinName);
            this.Owner.Send("id author " + this.Owner.AuthorName);
            this.Owner.Send("usiok");
        }


        public void AtLoop_OnSetoption(string line, ref PhaseResult_UsiLoop1 result_Usi)
        {
            //------------------------------------------------------------
            // 設定してください
            //------------------------------------------------------------
            #region ↓詳説
            //
            // 図.
            //
            //      log.txt
            //      ┌────────────────────────────────────────
            //      ～
            //      │2014/08/02 8:19:36> setoption name USI_Ponder value true
            //      │2014/08/02 8:19:36> setoption name USI_Hash value 256
            //      │
            //
            // ↑ゲーム開始時には、[対局]ダイアログボックスの[エンジン共通設定]の２つの内容が送られてきます。
            //      ・[相手の手番中に先読み] チェックボックス
            //      ・[ハッシュメモリ  ★　MB] スピン
            //
            // または
            //
            //      log.txt
            //      ┌────────────────────────────────────────
            //      ～
            //      │2014/08/02 23:47:35> setoption name 卯
            //      │2014/08/02 23:47:35> setoption name 卯
            //      │2014/08/02 23:48:29> setoption name 子 value true
            //      │2014/08/02 23:48:29> setoption name USI value 6
            //      │2014/08/02 23:48:29> setoption name 寅 value 馬
            //      │2014/08/02 23:48:29> setoption name 辰 value DRAGONabcde
            //      │2014/08/02 23:48:29> setoption name 巳 value C:\Users\Takahashi\Documents\新しいビットマップ イメージ.bmp
            //      │
            //
            //
            // 将棋所から、[エンジン設定] ダイアログボックスの内容が送られてきます。
            // このダイアログボックスは、将棋エンジンから将棋所に  ダイアログボックスを作るようにメッセージを送って作ったものです。
            //
            #endregion

            //------------------------------------------------------------
            // 設定を一覧表に変えます
            //------------------------------------------------------------
            #region ↓詳説
            //
            // 上図のメッセージのままだと使いにくいので、
            // あとで使いやすいように Key と Value の表に分けて持ち直します。
            //
            // 図.
            //
            //      setoptionDictionary
            //      ┌──────┬──────┐
            //      │Key         │Value       │
            //      ┝━━━━━━┿━━━━━━┥
            //      │USI_Ponder  │true        │
            //      ├──────┼──────┤
            //      │USI_Hash    │256         │
            //      └──────┴──────┘
            //
            #endregion
            Regex regex = new Regex(@"setoption name ([^ ]+)(?: value (.*))?", RegexOptions.Singleline);
            Match m = regex.Match(line);

            if (m.Success)
            {
                string name = (string)m.Groups[1].Value;
                string value = "";

                if (3 <= m.Groups.Count)
                {
                    // 「value ★」も省略されずにありました。
                    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                    value = (string)m.Groups[2].Value;
                }

                if (this.Owner.SetoptionDictionary.ContainsKey(name))
                {
                    // 設定を上書きします。
                    this.Owner.SetoptionDictionary[name] = value;
                }
                else
                {
                    // 設定を追加します。
                    this.Owner.SetoptionDictionary.Add(name, value);
                }
            }

            if (this.Owner.SetoptionDictionary.ContainsKey("USI_ponder"))
            {
                string value = this.Owner.SetoptionDictionary["USI_ponder"];

                bool result;
                if (Boolean.TryParse(value, out result))
                {
                    this.Owner.Option_enable_usiPonder = result;
                }
            }
            else if (this.Owner.SetoptionDictionary.ContainsKey("noopable"))
            {
                //
                // 独自実装。
                //
                string value = this.Owner.SetoptionDictionary["noopable"];

                bool result;
                if (Boolean.TryParse(value, out result))
                {
                    this.Owner.Option_enable_serverNoopable = result;
                }
            }
        }


        public void AtLoop_OnIsready(string line, ref PhaseResult_UsiLoop1 result_Usi)
        {
            //------------------------------------------------------------
            // それでは定刻になりましたので……
            //------------------------------------------------------------
            #region ↓詳説
            //
            // 図.
            //
            //      log.txt
            //      ┌────────────────────────────────────────
            //      ～
            //      │2014/08/02 1:31:35> isready
            //      │
            //
            //
            // 対局開始前に、将棋所から送られてくる文字が isready です。
            #endregion


            //------------------------------------------------------------
            // 将棋エンジン「おっおっ、設定を終わらせておかなければ（汗、汗…）」
            //------------------------------------------------------------
#if DEBUG
            Util_OwataMinister.ENGINE_DEFAULT.Logger.WriteLine_AddMemo("┏━━━━━設定━━━━━┓");
            foreach (KeyValuePair<string, string> pair in this.Owner.SetoptionDictionary)
            {
                // ここで将棋エンジンの設定を済ませておいてください。
                Util_OwataMinister.ENGINE_DEFAULT.Logger.WriteLine_AddMemo(pair.Key + "=" + pair.Value);
            }
            Util_OwataMinister.ENGINE_DEFAULT.Logger.WriteLine_AddMemo("┗━━━━━━━━━━━━┛");
#endif

            //------------------------------------------------------------
            // よろしくお願いします(^▽^)！
            //------------------------------------------------------------
            #region ↓詳説
            //
            // 図.
            //
            //      log.txt
            //      ┌────────────────────────────────────────
            //      ～
            //      │2014/08/02 2:03:33< readyok
            //      │
            //
            //
            // いつでも対局する準備が整っていましたら、 readyok を送り返します。
            #endregion
            this.Owner.Send("readyok");
        }


        public void AtLoop_OnUsinewgame(string line, ref PhaseResult_UsiLoop1 result_Usi)
        {
            //------------------------------------------------------------
            // 対局時計が ポチッ とされました
            //------------------------------------------------------------
            #region ↓詳説
            //
            // 図.
            //
            //      log.txt
            //      ┌────────────────────────────────────────
            //      ～
            //      │2014/08/02 2:03:33> usinewgame
            //      │
            //
            //
            // 対局が始まったときに送られてくる文字が usinewgame です。
            #endregion


            // 無限ループ（１つ目）を抜けます。無限ループ（２つ目）に進みます。
            result_Usi = PhaseResult_UsiLoop1.Break;
            return;
        }


        public void AtLoop_OnQuit(string line, ref PhaseResult_UsiLoop1 result_Usi)
        {
            //------------------------------------------------------------
            // おつかれさまでした
            //------------------------------------------------------------
            #region ↓詳説
            //
            // 図.
            //
            //      log.txt
            //      ┌────────────────────────────────────────
            //      ～
            //      │2014/08/02 1:31:38> quit
            //      │
            //
            //
            // 将棋エンジンを止めるときに送られてくる文字が quit です。
            #endregion


            //------------------------------------------------------------
            // ﾉｼ
            //------------------------------------------------------------
            #region ↓詳説
            //
            // 図.
            //
            //      log.txt
            //      ┌────────────────────────────────────────
            //      ～
            //      │2014/08/02 3:08:34> (^-^)ﾉｼ
            //      │
            //
            //
            #endregion
#if DEBUG
            Util_OwataMinister.ENGINE_DEFAULT.Logger.WriteLine_AddMemo("(^-^)ﾉｼ");
#endif


            // このプログラムを終了します。
            result_Usi = PhaseResult_UsiLoop1.Quit;
        }


    }
}
