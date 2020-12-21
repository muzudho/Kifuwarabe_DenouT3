using System.IO;
using System.Windows.Forms;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.P481AimsServer.L500Server;
using Nett;

namespace Grayscale.Kifuwarakaku.CliOfAims
{
    class Program
    {

        static void Main(string[] args)
        {
            MessageBox.Show("AIMSサーバー");

            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));

            string filepath = Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("AimsDataSetteiXml"));
            MessageBox.Show("設定ファイルパス＝["+filepath+"]");

            //
            // 設定XMLファイル
            //
            SetteiXmlFile setteiXmlFile;
            {
                setteiXmlFile = new SetteiXmlFile(filepath);
                //if (!setteiXmlFile.Exists())
                //{
                //    // ファイルが存在しませんでした。

                //    // 作ります。
                //    setteiXmlFile.Write();
                //}

                if (!setteiXmlFile.Read())
                {
                    // 読取に失敗しました。
                }

                // デバッグ
                //setteiXmlFile.DebugWrite();
            }


            MessageBox.Show("AIMSサーバー\n将棋エンジン・ファイルパス＝[" + setteiXmlFile.ShogiEngineFilePath + "]");

            SkyConst src_Sky = Util_SkyWriter.New_Hirate( Playerside.P1 );

            AimsServerImpl aimsServer = new AimsServerImpl(src_Sky, 0);
            aimsServer.ShogiEngineFilePath = setteiXmlFile.ShogiEngineFilePath;

            aimsServer.AtBegin();
            aimsServer.AtBody();
            aimsServer.AtEnd();
        }
    }
}
