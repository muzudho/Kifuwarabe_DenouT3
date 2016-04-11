using Grayscale.P005_Tushin_____.L500____Util;
using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Grayscale.P027_Settei_____.L510____Xml
{

    /// <summary>
    /// ************************************************************************************************************************
    /// 設定XMLファイル
    /// ************************************************************************************************************************
    /// </summary>
    public class SetteiXmlFile
    {

        #region プロパティ類

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 設定XMLファイル名です。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public string FileName
        {
            get
            {
                return this.fileName;
            }
        }
        private string fileName;

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 設定ファイルのバージョンです。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public string SetteiFileVer
        {
            get
            {
                return this.setteiFileVer;
            }
        }
        private string setteiFileVer;

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 将棋エンジンの名前です。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public string ShogiEngineName
        {
            get
            {
                return this.shogiEngineName;
            }
        }
        private string shogiEngineName;

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 将棋エンジンのファイル・パスです。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public string ShogiEngineFilePath
        {
            get
            {
                return this.shogiEngineFilePath;
            }
        }
        private string shogiEngineFilePath;

        #endregion



        /// <summary>
        /// コンストラクターです。
        /// </summary>
        public SetteiXmlFile(string fileName)
        {
            this.fileName = fileName;
            this.setteiFileVer = "0.00.0";
            this.shogiEngineName = "The将棋エンジン";
            this.shogiEngineFilePath = "shogiEngine.exe";
        }

        public void DebugWrite()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("設定ファイル          : " + this.FileName);
            sb.AppendLine("設定ファイルVer       : " + this.SetteiFileVer);
            sb.AppendLine("将棋エンジン          : " + this.ShogiEngineName);
            sb.AppendLine("将棋エンジン・ファイル: " + this.ShogiEngineFilePath);
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine();

            Util_Message.Whisper(sb.ToString());
        }

        public bool Exists()
        {
            return File.Exists(this.FileName);
        }

        public bool Read()
        {
            bool successfule = true;

            XmlDocument xDoc = new XmlDocument();

            try
            {
                xDoc.Load(this.fileName);

                XmlElement xKifunarabe = xDoc.DocumentElement;
                this.setteiFileVer = xKifunarabe.GetAttribute("setteiFileVer");

                XmlNodeList xShogiEngineNodeList = xKifunarabe.GetElementsByTagName("shogiEngine");
                foreach (XmlNode xShogiEngineNode in xShogiEngineNodeList)
                {
                    XmlElement xShogiEngine = (XmlElement)xShogiEngineNode;

                    this.shogiEngineName = xShogiEngine.GetAttribute("name");
                    this.shogiEngineFilePath = xShogiEngine.GetAttribute("file");
                    break;
                }
            }
            catch(Exception ex)
            {
                // エラー
                successfule = false;
                Util_Message.Whisper(ex.GetType().Name+"　"+ex.Message);
            }

            return successfule;
        }


        public bool Write()
        {
            bool successfule = true;

            XmlDocument xDoc = new XmlDocument();

            // UTF-8 エンコーディングで書くものとします。
            XmlProcessingInstruction xPi = xDoc.CreateProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");
            xDoc.AppendChild(xPi);


            try
            {
                // ルート要素 <kifunarabe> を作成
                XmlElement xKifunarabe = xDoc.CreateElement("kifunarabe");
                xDoc.AppendChild(xKifunarabe);

                // setteiFileVer="1.00.0"
                xKifunarabe.SetAttribute("setteiFileVer", this.SetteiFileVer);

                // コメント
                xKifunarabe.AppendChild(xDoc.CreateComment("v(^-^)vｲｪｰｲ☆ 『将棋ＧＵＩ きふならべ』の設定ファイルなんだぜ☆！ 今は一番上に書いてある ＜shogiEngine＞ を見に行くぜ☆"));

                // <shogiEngine>
                XmlElement xShogiEngine = xDoc.CreateElement("shogiEngine");

                // name="The将棋エンジン"
                xShogiEngine.SetAttribute("file", this.ShogiEngineName);

                // file="shogiEngine.exe"
                xShogiEngine.SetAttribute("file", this.ShogiEngineFilePath);

                xKifunarabe.AppendChild(xShogiEngine);

                // .xmlファイルを保存
                xDoc.Save(this.FileName);
            }
            catch (Exception ex)
            {
                // エラー
                successfule = false;
                Util_Message.Show(ex.GetType().Name + "　" + ex.Message);
            }

            return successfule;
        }




    }
}
