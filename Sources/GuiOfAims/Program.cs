﻿using System.IO;
using System.Windows.Forms;
using Grayscale.Kifuwarakaku.Engine.Configuration;
using Grayscale.Kifuwarakaku.Entities;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.UseCases.Features;
using Nett;

namespace Grayscale.Kifuwarakaku.CliOfAims
{
    class Program
    {

        static void Main(string[] args)
        {
            MessageBox.Show("AIMSサーバー");

            var engineConf = new EngineConf();
            EntitiesLayer.Implement(engineConf);

            string filepath = engineConf.GetResourceFullPath("AimsDataSetteiXml");
            MessageBox.Show($"設定ファイルパス＝[{filepath}]");

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

                setteiXmlFile.Read();

                // デバッグ
                //setteiXmlFile.DebugWrite();
            }


            MessageBox.Show($"AIMSサーバー\n将棋エンジン・ファイルパス＝[{setteiXmlFile.ShogiEngineFilePath}]");

            SkyConst src_Sky = Util_SkyWriter.New_Hirate(Playerside.P1);

            AimsServerImpl aimsServer = new AimsServerImpl(src_Sky, 0);
            aimsServer.ShogiEngineFilePath = setteiXmlFile.ShogiEngineFilePath;

            aimsServer.AtBegin();
            aimsServer.AtBody();
        }
    }
}
