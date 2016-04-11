using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P003_Log________.L500____Struct;
using Grayscale.P027_Settei_____.L500____Struct;
using Grayscale.P027_Settei_____.L510____Xml;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P258_UtilSky258_.L500____UtilSky;
using Grayscale.P481_AimsServer_.L500____Server;
using System.Windows.Forms;

namespace Grayscale.P489_Form_______
{
    class Program
    {

        static void Main(string[] args)
        {
            KwErrorHandler errH = Util_OwataMinister.AIMS_DEFAULT;
            MessageBox.Show("AIMSサーバー");


            string filepath = Const_Filepath.AIMS_TO_DATA + "data_settei.xml";
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
            aimsServer.AtBody(errH);
            aimsServer.AtEnd();
        }
    }
}
