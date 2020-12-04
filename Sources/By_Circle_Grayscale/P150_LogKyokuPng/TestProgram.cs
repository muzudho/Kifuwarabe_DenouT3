using Grayscale.P157KyokumenPng.I500Struct;
using Grayscale.P157KyokumenPng.L500Struct;
using Grayscale.P158LogKyokuPng.L500UtilWriter;
using System;
using System.Collections.Generic;

namespace Grayscale.P159_Form_______
{
    static class TestProgram
    {

        static void AppendCommandline(Dictionary<string, string> dic)
        {
            string[] args = Environment.GetCommandLineArgs();

            foreach(string arg in args)
            {
                string name;
                string value;

                string rest = arg;
                if (!rest.StartsWith("--"))
                {
                    goto gt_Next1;
                }

                rest = rest.Substring(2);

                int eq = rest.IndexOf('=');
                if (-1 == eq)
                {
                    goto gt_Next1;
                }

                name = rest.Substring(0, eq).Trim();
                value = rest.Substring(eq + 1).Trim();

                if (value.StartsWith("\"") && value.EndsWith("\""))
                {
                    value = value.Substring(1, value.Length - 2);
                }

                if (dic.ContainsKey(name))
                {
                    dic[name] = value;
                }
                else
                {
                    dic.Add(name, value);
                }

            gt_Next1:
                ;
            }
        }


        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void TestMain()
        {
            //
            // コマンドライン引数の例
            //
            // --position="position sfen 1nsgkgsnl/9/p2pppppp/9/9/9/P2PPPPPP/1B5R1/1NSGKGSNL w L2Pl2p 1 moves 5a6b 7g7f 3a3b" \
            // --outFolder="../../Logs/"
            // --outFile="_log_局面1.png"
            // --imgFolder="../../Data/img/gkLog/" \
            // --kmFile="koma1.png" \
            // --sjFile="suji1.png" \
            // --kmW=20 \
            // --kmH=20 \
            // --sjW=8 \
            // --sjH=12 \
            // --end
            //

            // ヌル防止のための初期値
            Dictionary<string, string> argsDic = new Dictionary<string, string>();
            argsDic.Add("position", "position startpos moves");
            argsDic.Add("outFolder", "./");//出力フォルダー "../../Logs/"
            argsDic.Add("outFile", "1.png");//出力ファイル
            argsDic.Add("imgFolder", ".");//画像フォルダーへのパス image path
            argsDic.Add("kmFile", "2.png");//駒画像へのパス。
            argsDic.Add("kmW", "1");//駒の横幅。koma width
            argsDic.Add("kmH", "1");
            argsDic.Add("sjFile", "3.png");//数字・小
            argsDic.Add("sjW", "1");//数字の横幅。suji width
            argsDic.Add("sjH", "1");
            TestProgram.AppendCommandline(argsDic);

            //foreach (KeyValuePair<string, string> entry in argsDic)
            //{
            //    MessageBox.Show("["+entry.Key + "]=[" + entry.Value+"]", "デバッグ");
            //}
            //MessageBox.Show("出力先=[" + Path.Combine(Application.StartupPath, argsDic["outPath"]) + "]", "デバッグ");



            //
            // SFEN
            //
            string sfen;
            {
                // SFEN を分解したい。
                //string sfen = "lnsgkgsn1/1r5b1/ppppppppp/9/9/9/PPPPPPPPP/1B5R1/LNSGKGSNL w - 1";
                //string sfen = "position sfen lnsgkgsnl/9/ppppppppp/9/9/9/PPPPPPPPP/1B5R1/LNSGKGSNL w - 1 moves 5a6b 7g7f 3a3b";
                //string sfen = "position sfen lnsgkgsnl/9/p1ppppppp/9/9/9/P1PPPPPPP/1B5R1/LNSGKGSNL w Pp 1 moves 5a6b 7g7f 3a3b";
                //string sfen = "position sfen 1nsgkgsnl/9/p2pppppp/9/9/9/P2PPPPPP/1B5R1/1NSGKGSNL w L2Pl2p 1 moves 5a6b 7g7f 3a3b";
                sfen = argsDic["position"];
            }

            KyokumenPngEnvironment reportEnvironment = new KyokumenPngEnvironmentImpl(
                    argsDic["outFolder"],
                    argsDic["imgFolder"],
                    argsDic["kmFile"],
                    argsDic["sjFile"],
                    argsDic["kmW"],
                    argsDic["kmH"],
                    argsDic["sjW"],
                    argsDic["sjH"]
                );
            // テスト・プログラム
            Util_KyokumenPng_Writer.Write_ForTest(
                sfen,
                "",
                argsDic["outFile"],
                reportEnvironment
                );

        }
    }
}
