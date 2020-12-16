// noop 可
//#define NOOPABLE
namespace Grayscale.P571KifuWarabe.L500KifuWarabe
{
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
    using Nett;

    public class ProgramSupport : ShogiEngine
    {
        /// <summary>
        /// USI「setoption」コマンドのリストです。
        /// </summary>
        public Dictionary<string, string> SetoptionDictionary { get; set; }


        /// <summary>
        /// 将棋エンジンの中の一大要素「思考エンジン」です。
        /// 指す１手の答えを出すのが仕事です。
        /// </summary>
        public Shogisasi shogisasi;

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
            ErrorControllerReference.EngineNetwork.Logger.WriteLineS(line);
#endif
        }

        /// <summary>
        /// コンストラクター
        /// </summary>
        public ProgramSupport()
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
                var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
                var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));

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
                    Util_FvLoad.OpenFv(this.shogisasi.FeatureVector, Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("Fv00Komawari")), errH);
                }


                exception_area = 2000;
                //------------------------------------------------------------------------------------------------------------------------
                // ファイル読込み
                //------------------------------------------------------------------------------------------------------------------------
                {
                    // データの読取「道」
                    if (Michi187Array.Load(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("Michi187"))))
                    {
                    }

                    // データの読取「配役」
                    Util_Array_KomahaiyakuEx184.Load(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("Haiyaku185")), Encoding.UTF8);

                    // データの読取「強制転成表」　※駒配役を生成した後で。
                    Array_ForcePromotion.Load(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("InputForcePromotion")), Encoding.UTF8);

#if DEBUG
                    {
                        File.WriteAllText(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("OutputForcePromotion")), Array_ForcePromotion.LogHtml());
                    }
#endif

                    // データの読取「配役転換表」
                    Data_KomahaiyakuTransition.Load(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("InputSyuruiToHaiyaku")), Encoding.UTF8);

#if DEBUG
                    {
                        File.WriteAllText(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("OutputSyuruiToHaiyaku")), Data_KomahaiyakuTransition.Format_LogHtml());
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
                    var engineName = toml.Get<TomlTable>("Engine").Get<string>("Name");
                    ErrorControllerReference.EngineDefault.Logger.WriteLineAddMemo($"v(^▽^)v ｲｪｰｲ☆ ... {engineName} {versionStr}");
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

        public void AtEnd()
        {
        }

    }
}
