using Grayscale.P211WordShogi.L500Word;
using Grayscale.P218Starlight.I500Struct;
using Grayscale.P223TedokuHisto.I250Struct;
using Grayscale.P223TedokuHisto.L250Struct;
using Grayscale.P224Sky.I500Struct;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //フィンガー番号

namespace Grayscale.P224Sky.L500Struct
{

    /// <summary>
    /// ************************************************************************************************************************
    /// 天空
    /// ************************************************************************************************************************
    /// 
    /// 局面のことです。
    /// 
    /// </summary>
    public class SkyBuffer : Sky
    {
        #region プロパティー

        public TedokuHistory TedokuHistory { get { return this.tedokuHistory; } }
        private TedokuHistory tedokuHistory;

        /// <summary>
        /// TODO:
        /// </summary>
        public Playerside KaisiPside { get { return this.kaisiPside; } }
        public void SetKaisiPside(Playerside pside)
        {
            this.kaisiPside = pside;
        }
        private Playerside kaisiPside;

        /// <summary>
        /// 何手目済みか。初期局面を 0 とする。
        /// </summary>
        public int Temezumi { get { return this.temezumi; } }
        public void SetTemezumi(int temezumi)
        {
            this.temezumi = temezumi;
        }
        private int temezumi;

        /// <summary>
        /// 「置き場に置けるものの素性」リストです。駒だけとは限りませんので、４０個以上になることもあります。
        /// </summary>
        public List<IMoveHalf> Starlights
        {
            get
            {
                return this.starlights;
            }
        }
        private List<IMoveHalf> starlights;

        #endregion


        public Sky Clone()
        {
            return new SkyBuffer(this);
        }


        public int Count
        {
            get
            {
                return this.Starlights.Count;
            }
        }


        public void Clear()
        {
            this.starlights.Clear();
        }

        /// <summary>
        /// 駒台に戻すとき
        /// </summary>
        /// <param name="kifu"></param>
        /// <param name="finger"></param>
        /// <param name="light"></param>
        public void PutOverwriteOrAdd_Starlight(Finger finger, IMoveHalf light)
        {
            if(this.starlights.Count==(int)finger)
            {
                // オブジェクトを追加します。
                this.Starlights.Add(light);
            }
            else if (this.starlights.Count+1 <= (int)finger)
            {
                // エラー
                throw new Exception($"{this.GetType().Name}#SetStarPos：　リストの要素より2多いインデックスを指定されましたので、追加しません。starIndex=[{finger}] / this.stars.Count=[{this.starlights.Count}]");
            }
            else
            {
                this.Starlights[(int)finger] = light;
            }
        }





        /// <summary>
        /// 棋譜を新規作成するときに使うコンストラクター。
        /// </summary>
        public SkyBuffer(Playerside kaisiPside, int temezumi)
        {
            switch(kaisiPside)
            {
                case Playerside.P1: this.tedokuHistory = TedokuHistoryConst.New_HirateSyokikyokumen_1P(); break;
                case Playerside.P2: this.tedokuHistory = TedokuHistoryConst.New_HirateSyokikyokumen_2P(); break;
                default: break;
            }            

            this.kaisiPside = kaisiPside;
            this.temezumi = temezumi;
            this.starlights = new List<IMoveHalf>();
        }

        /// <summary>
        /// クローンを作ります。
        /// </summary>
        /// <param name="src"></param>
        public SkyBuffer(Sky src)
        {
            // 手得ヒストリーのクローン
            this.tedokuHistory = TedokuHistoryConst.New_Clone(src.TedokuHistory);

            // 手番のクローン
            this.kaisiPside = src.KaisiPside;
            this.temezumi = src.Temezumi;

            // 星々のクローン
            this.starlights = new List<IMoveHalf>();
            src.Foreach_Starlights((Finger finger, IMoveHalf light, ref bool toBreak) =>
            {
                this.starlights.Add(light);
            });
        }

        public IMoveHalf StarlightIndexOf(
            Finger finger,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
        )
        {
            IMoveHalf found;

            if ((int)finger < this.starlights.Count)
            {
                found = this.starlights[(int)finger];
            }
            else
            {
                throw new Exception($@"{this.GetType().Name}#StarIndexOf：　スプライトの数より多いスプライト番号を指定されましたので、取得できません。スプライト番号=[{finger}] / スプライトの数=[{ this.starlights.Count}]
memberName={memberName}
sourceFilePath={sourceFilePath}
sourceLineNumber={sourceLineNumber}");
            }

            return found;
        }


        public delegate void DELEGATE_Sky_Foreach(Finger finger, IMoveHalf light, ref bool toBreak);
        public void Foreach_Starlights(DELEGATE_Sky_Foreach delegate_Sky_Foreach)
        {
            bool toBreak = false;

            Finger finger = 0;
            foreach (IMoveHalf light in this.Starlights)
            {
                delegate_Sky_Foreach(finger, light, ref toBreak);

                finger = (int)finger + 1;
                if (toBreak)
                {
                    break;
                }
            }
        }

        
        /// <summary>
        /// ************************************************************************************************************************
        /// 天上のすべての星の光
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="kifu"></param>
        /// <param name="okiba"></param>
        /// <returns></returns>
        public Fingers Fingers_All()
        {
            Fingers fingers = new Fingers();

            this.Foreach_Starlights((Finger finger, IMoveHalf light, ref bool toBreak) =>
            {
                fingers.Add(finger);
            });

            return fingers;
        }
    }
}
