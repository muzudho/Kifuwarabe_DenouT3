// noop 可
//#define NOOPABLE

using Grayscale.P003Log.I500Struct;
using Grayscale.P003Log.L500Struct;
using Grayscale.P005Tushin.L500Util;
using Grayscale.P027Settei.L500Struct;
using Grayscale.P236KomahaiyaTr.L500Table;
using Grayscale.P248Michi.L500Word;
using Grayscale.P250KomahaiyaEx.L500Util;
using Grayscale.P270ForcePromot.L250Struct;
using Grayscale.P523UtilFv.L510UtilFvLoad;
using Grayscale.P542Scoreing.I005UsiLoop;
using Grayscale.P542Scoreing.L240Shogisasi;
using Grayscale.P571KifuWarabe.L100Shogisasi;
using Grayscale.P571KifuWarabe.L250UsiLoop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace Grayscale.P571KifuWarabe.L500KifuWarabe
{

    public class KifuWarabeImpl : ShogiEngine
    {
        /// <summary>
        /// USI「setoption」コマンドのリストです。
        /// </summary>
        public Dictionary<string, string> SetoptionDictionary { get; set; }


        /// <summary>
        /// 将棋エンジンの中の一大要素「思考エンジン」です。
        /// 指す１手の答えを出すのが仕事です。
        /// </summary>
        private Shogisasi shogisasi;

        /// <summary>
        /// USI「ponder」の使用の有無です。
        /// </summary>
        public bool Option_enable_usiPonder { get; set; }

        /// <summary>
        /// サーバーに「noop」コマンドを投げてもよいかどうかです。
        /// サーバーに「noop」を送ると、「ok」を返してくれるものとします。１分間を空けてください。
        /// </summary>
        public bool Option_enable_serverNoopable { get; set; }

        /// <summary>
        /// 送信
        /// </summary>
        /// <param name="line">メッセージ</param>
        public void Send(string line)
        {
            // 将棋サーバーに向かってメッセージを送り出します。
            Util_Message.Upload(line);

#if DEBUG
            // 送信記録をつけます。
            ErrorControllerReference.ENGINE_NETWORK.Logger.WriteLine_S(line);
#endif
        }

        /// <summary>
        /// コンストラクター
        /// </summary>
        public KifuWarabeImpl()
        {
            //-------------+----------------------------------------------------------------------------------------------------------
            // データ設計  |
            //-------------+----------------------------------------------------------------------------------------------------------
            // 将棋所から送られてくるデータを、一覧表に変えたものです。
            this.SetoptionDictionary = new Dictionary<string, string>(); // 不定形

            this.Option_enable_usiPonder = false; // ポンダーに対応している将棋サーバーなら真です。
            this.Option_enable_serverNoopable = false; // 独自実装のコマンドなので、ＯＦＦにしておきます。
        }

        public void AtBegin(IErrorController errH)
        {
            int exception_area = 0;
            try
            {
                exception_area = 500;
                //-------------------+----------------------------------------------------------------------------------------------------
                // ログファイル削除  |
                //-------------------+----------------------------------------------------------------------------------------------------
                {
                    //
                    // 図.
                    //
                    //      フォルダー
                    //          ├─ Engine.KifuWarabe.exe
                    //          └─ log.txt               ←これを削除
                    //
                    ErrorControllerReference.RemoveAllLogFiles();
                }


                exception_area = 1000;
                //------------------------------------------------------------------------------------------------------------------------
                // 思考エンジンの、記憶を読み取ります。
                //------------------------------------------------------------------------------------------------------------------------
                {
                    this.shogisasi = new ShogisasiImpl(this);
                    Util_FvLoad.OpenFv(this.shogisasi.FeatureVector, Const_Filepath.ENGINE_TO_DATA + "fv/fv_00_Komawari.csv", errH);
                }


                exception_area = 2000;
                //------------------------------------------------------------------------------------------------------------------------
                // ファイル読込み
                //------------------------------------------------------------------------------------------------------------------------
                {
                    string dataFolder = Path.Combine(Application.StartupPath, Const_Filepath.ENGINE_TO_DATA);
                    string logsFolder = Path.Combine(Application.StartupPath, Const_Filepath.ENGINE_TO_LOGS);

                    // データの読取「道」
                    string filepath_Michi = Path.Combine(dataFolder, "data_michi187.csv");
                    if (Michi187Array.Load(filepath_Michi))
                    {
                    }

                    // データの読取「配役」
                    string filepath_Haiyaku = Path.Combine(dataFolder, "data_haiyaku185_UTF-8.csv");
                    Util_Array_KomahaiyakuEx184.Load(filepath_Haiyaku, Encoding.UTF8);

                    // データの読取「強制転成表」　※駒配役を生成した後で。
                    string filepath_ForcePromotion = Path.Combine(dataFolder, "data_forcePromotion_UTF-8.csv");
                    Array_ForcePromotion.Load(filepath_ForcePromotion, Encoding.UTF8);

#if DEBUG
                    {
                        string filepath_LogKyosei = Path.Combine(logsFolder, "_log_強制転成表.html");
                        File.WriteAllText(filepath_LogKyosei, Array_ForcePromotion.LogHtml());
                    }
#endif

                    // データの読取「配役転換表」
                    string filepath_HaiyakuTenkan = Path.Combine(dataFolder, "data_syuruiToHaiyaku.csv");
                    Data_KomahaiyakuTransition.Load(filepath_HaiyakuTenkan, Encoding.UTF8);

#if DEBUG
                    {
                        string filepath_LogHaiyakuTenkan = Path.Combine(logsFolder, "_log_配役転換表.html");
                        File.WriteAllText(filepath_LogHaiyakuTenkan, Data_KomahaiyakuTransition.Format_LogHtml());
                    }
#endif
                }

                exception_area = 4000;
                //-------------+----------------------------------------------------------------------------------------------------------
                // ログ書込み  |  ＜この将棋エンジン＞  製品名、バージョン番号
                //-------------+----------------------------------------------------------------------------------------------------------
                //
                // 図.
                //
                //      log.txt
                //      ┌────────────────────────────────────────
                //      │2014/08/02 1:04:59> v(^▽^)v ｲｪｰｲ☆ ... fugafuga 1.00.0
                //      │
                //      │
                //
                //
                // 製品名とバージョン番号は、次のファイルに書かれているものを使っています。
                // 場所：  [ソリューション エクスプローラー]-[ソリューション名]-[プロジェクト名]-[Properties]-[AssemblyInfo.cs] の中の、[AssemblyProduct]と[AssemblyVersion] を参照。
                //
                // バージョン番号を「1.00.0」形式（メジャー番号.マイナー番号.ビルド番号)で書くのは作者の趣味です。
                //
                {
                    string versionStr;

                    // バージョン番号
                    Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                    versionStr = String.Format("{0}.{1}.{2}", version.Major, version.Minor.ToString("00"), version.Build);

                    //seihinName += " " + versionStr;
#if DEBUG
                    ErrorControllerReference.ENGINE_DEFAULT.Logger.WriteLine_AddMemo("v(^▽^)v ｲｪｰｲ☆ ... " + this.SeihinName + " " + versionStr);
#endif
                }

            }
            catch (Exception ex)
            {
                switch (exception_area)
                {
                    case 1000:
                        ErrorControllerReference.EngineDefault.Panic("フィーチャーベクターCSVを読み込んでいるとき。" + ex.GetType().Name + "：" + ex.Message);
                        break;
                }
                throw;
            }
        }

        public void AtBody(out bool out_isTimeoutShutdown, IErrorController errH)
        {
            out_isTimeoutShutdown = false;

            try
            {

                // 
                // 図.
                // 
                //     プログラムの開始：  ここの先頭行から始まります。
                //     プログラムの実行：  この中で、ずっと無限ループし続けています。
                //     プログラムの終了：  この中の最終行を終えたとき、
                //                         または途中で Environment.Exit(0); が呼ばれたときに終わります。
                //                         また、コンソールウィンドウの[×]ボタンを押して強制終了されたときも  ぶつ切り  で突然終わります。


                //************************************************************************************************************************
                // ループ（全体）
                //************************************************************************************************************************
                //
                // 図.
                //
                //      無限ループ（全体）
                //          │
                //          ├─無限ループ（１）
                //          │                      将棋エンジンであることが認知されるまで、目で訴え続けます(^▽^)
                //          │                      認知されると、無限ループ（２）に進みます。
                //          │
                //          └─無限ループ（２）
                //                                  対局中、ずっとです。
                //                                  対局が終わると、無限ループ（１）に戻ります。
                //
                // 無限ループの中に、２つの無限ループが入っています。
                //

                while (true)
                {
#if DEBUG_STOPPABLE
            MessageBox.Show("きふわらべのMainの無限ループでブレイク☆！", "デバッグ");
            System.Diagnostics.Debugger.Break();
#endif
                    //
                    // USIループ（１つ目）
                    //
                    UsiLoop1 usiLoop1 = new UsiLoop1(this);
                    usiLoop1.AtStart();
                    bool isTimeoutShutdown_temp;
                    PhaseResult_UsiLoop1 result_UsiLoop1 = usiLoop1.AtBody(out isTimeoutShutdown_temp);
                    usiLoop1.AtEnd();
                    if (isTimeoutShutdown_temp)
                    {
                        //MessageBox.Show("ループ１で矯正終了するんだぜ☆！");
                        out_isTimeoutShutdown = isTimeoutShutdown_temp;
                        goto gt_EndMethod;
                    }
                    else if (result_UsiLoop1 == PhaseResult_UsiLoop1.Quit)
                    {
                        goto gt_EndMethod;//全体ループを抜けます。
                    }

                    //
                    // USIループ（２つ目）
                    //
                    UsiLoop2 usiLoop2 = new UsiLoop2(this.shogisasi, this);
                    usiLoop2.AtBegin();
                    usiLoop2.AtBody(out isTimeoutShutdown_temp, errH);
                    usiLoop2.AtEnd();
                    if (isTimeoutShutdown_temp)
                    {
                        //MessageBox.Show("ループ２で矯正終了するんだぜ☆！");
                        out_isTimeoutShutdown = isTimeoutShutdown_temp;
                        goto gt_EndMethod;//全体ループを抜けます。
                    }
                }

            }
            catch (Exception ex)
            {
                // エラーが起こりました。
                //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                // どうにもできないので  ログだけ取って無視します。
                ErrorControllerReference.EngineDefault.Panic("Program「大外枠でキャッチ」：" + ex.GetType().Name + " " + ex.Message);
            }

        gt_EndMethod:
            ;
        }

        public void AtEnd()
        {
        }

    }
}
