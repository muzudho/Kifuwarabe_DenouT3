using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P211_WordShogi__.L250____Masu;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P212_ConvPside__.L500____Converter;
using Grayscale.P213_Komasyurui_.L250____Word;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P223_TedokuHisto.L___250_Struct;
using Grayscale.P223_TedokuHisto.L250____Struct;
using Grayscale.P224_Sky________.L___500_Struct;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //フィンガー番号

namespace Grayscale.P224_Sky________.L500____Struct
{

    /// <summary>
    /// ************************************************************************************************************************
    /// 天空
    /// ************************************************************************************************************************
    /// 
    /// 読取専用の局面データです。
    /// 
    /// 使い方。
    /// 
    /// SkyConst(Sky src) を使って生成してください。
    /// Skyは他に、SkyBuffer があります。
    /// 
    /// </summary>
    public class SkyConst : Sky
    {
        #region プロパティー

        public TedokuHistory TedokuHistory { get { return this.tedokuHistory; } }
        private TedokuHistory tedokuHistory;

        /// <summary>
        /// TODO:
        /// </summary>
        public Playerside KaisiPside { get { return this.kaisiPside; } }
        private Playerside kaisiPside;

        /// <summary>
        /// 何手目済みか。初期局面を 0 とする。
        /// </summary>
        public int Temezumi { get { return this.temezumi; } }
        private int temezumi;

        /// <summary>
        /// 「置き場に置けるものの素性」リストです。駒だけとは限りませんので、４０個以上になることもあります。
        /// </summary>
        private List<Starlight> starlights;

        #endregion


        public Sky Clone()
        {
            return SkyConst.NewInstance(this,
                -1//クローンなので、そのまま。
                );
        }


        public int Count
        {
            get
            {
                return this.starlights.Count;
            }
        }


        ///// <summary>
        ///// 棋譜を新規作成するときに使うコンストラクター。
        ///// </summary>
        //public SkyConst()
        //{
        //    this.PsideIsBlack = true;
        //    this.starlights = new List<Starlight>();
        //}

        public static SkyConst NewInstance(Sky src, int temezumi_orMinus1)
        {
            SkyConst result = new SkyConst(src, false, temezumi_orMinus1, new Finger[] { Fingers.Error_1 }, new Starlight[] { null },
                // 手得計算
                Komasyurui14.H00_Null___, 0, Masu_Honshogi.Query_Basho(Masu_Honshogi.nError)
                );
            return result;
        }

        public static SkyConst NewInstance_ReversePside(Sky src, int temezumi_orMinus1)
        {
            SkyConst result = new SkyConst(src, true, temezumi_orMinus1, new Finger[]{Fingers.Error_1},new Starlight[]{null},
                // 手得計算
                Komasyurui14.H00_Null___, 0, Masu_Honshogi.Query_Basho(Masu_Honshogi.nError)
                );
            return result;
        }

        /// <summary>
        /// 駒を１個　更新します。
        /// 
        /// （１）指したとき。戻したとき。
        /// （２）駒の向き変更にも使われる。
        /// </summary>
        /// <param name="src"></param>
        /// <param name="finger1"></param>
        /// <param name="light1"></param>
        /// <returns></returns>
        public static SkyConst NewInstance_OverwriteOrAdd_Light(Sky src, int temezumi_orMinus1, Finger finger1, Starlight light1)
        {
            SkyConst result = new SkyConst(src, false, temezumi_orMinus1, new Finger[]{finger1},new Starlight[]{light1},
                // 手得計算
                Komasyurui14.H00_Null___, 0, Masu_Honshogi.Query_Basho(Masu_Honshogi.nError)
                );
            return result;
        }

        /// <summary>
        /// 駒を１個　更新します。
        /// 
        /// （１）指したとき。戻したとき。
        /// （２）駒の向き変更にも使われる。
        /// </summary>
        /// <param name="src"></param>
        /// <param name="finger1"></param>
        /// <param name="light1"></param>
        /// <returns></returns>
        public static SkyConst NewInstance_OverwriteOrAdd_Light(Sky src, int temezumi_orMinus1, Finger finger1, Starlight light1,
            // 手得計算
            Komasyurui14 tedokuKeisan_komasyurui, int tedokukeisan_index, SyElement tedokukeisan_sasitamasu
            )
        {
            SkyConst result = new SkyConst(src, false, temezumi_orMinus1, new Finger[] { finger1 }, new Starlight[] { light1 },
                // 手得計算
                tedokuKeisan_komasyurui, tedokukeisan_index, tedokukeisan_sasitamasu
                );
            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="finger1">指した駒、指されていた駒</param>
        /// <param name="light1"></param>
        /// <param name="finger2">取った駒、取っていた駒</param>
        /// <param name="light2"></param>
        /// <returns></returns>
        public static SkyConst NewInstance_OverwriteOrAdd_Light(Sky src, int temezumi_orMinus1, Finger finger1, Starlight light1, Finger finger2, Starlight light2)
        {
            SkyConst result = new SkyConst(src, false, temezumi_orMinus1, new Finger[] { finger1, finger2 }, new Starlight[] { light1, light2 },
                // 手得計算
                Komasyurui14.H00_Null___, 0, Masu_Honshogi.Query_Basho(Masu_Honshogi.nError)
                );
            return result;
        }

        /// <summary>
        /// クローンを作ります。
        /// </summary>
        /// <param name="src"></param>
        private SkyConst(Sky src, bool toReversePlayerside, int update_temezumi_orMinus1, Finger[] finger1, Starlight[] light1,
            //
            // 手得計算
            //
            Komasyurui14 tedokuKeisan_komasyurui, int tedokukeisan_index, SyElement tedokukeisan_sasitamasu)
        {
            Debug.Assert(src.Count == 40, "本将棋とみなしてテスト中。sky.Starlights.Count=[" + src.Count + "]");//将棋の駒の数

            if (tedokuKeisan_komasyurui == Komasyurui14.H00_Null___)
            {
                //----------------------------------------
                // 手得計算のヒストリーを作らない場合
                //----------------------------------------

                // 手得ヒストリーのクローン
                this.tedokuHistory = TedokuHistoryConst.New_Clone(src.TedokuHistory);
            }
            else
            {
                //----------------------------------------
                // 手得計算のヒストリーを作る場合
                //----------------------------------------

                // 手駒ヒストリーへの追加
                this.tedokuHistory = TedokuHistoryConst.NewInstance_AppendSasitamasu(
                    src.TedokuHistory,
                    tedokuKeisan_komasyurui,
                    tedokukeisan_index,
                    tedokukeisan_sasitamasu
                    );
            }


            // 手番のクローン
            if (toReversePlayerside)
            {
                this.kaisiPside = Conv_Playerside.Reverse(src.KaisiPside);
            }
            else
            {
                this.kaisiPside = src.KaisiPside;
            }

            // 手目済み
            if (-1 == update_temezumi_orMinus1)
            {
                // そのまま
                this.temezumi = src.Temezumi;
            }
            else
            {
                // 上書き更新
                this.temezumi = update_temezumi_orMinus1;
            }

            // 星々のクローン
            this.starlights = new List<Starlight>();
            src.Foreach_Starlights((Finger finger2, Starlight light2, ref bool toBreak2) =>
            {
                this.starlights.Add(light2);
            });

            //
            // 追加分があれば。
            //
            for (int i = 0; i < finger1.Length; i++)
            {
                if (finger1[i] != Fingers.Error_1)
                {
                    if (this.starlights.Count == (int)finger1[i])
                    {
                        // オブジェクトを追加します。
                        this.starlights.Add(light1[i]);
                    }
                    else if (this.starlights.Count + 1 <= (int)finger1[i])
                    {
                        // エラー
                        Debug.Assert((int)finger1[i] < this.starlights.Count, "要素の個数より2大きいインデックスを指定しました。 インデックス[" + (int)finger1[i] + "]　要素の個数[" + this.starlights.Count + "]");

                        string message = this.GetType().Name + "#SetStarPos：　リストの要素より2多いインデックスを指定されましたので、追加しません。starIndex=[" + finger1[i] + "] / this.stars.Count=[" + this.starlights.Count + "]";
                        //LarabeLogger.GetInstance().WriteLineError(LarabeLoggerList.ERROR, message);
                        throw new Exception(message);
                    }
                    else
                    {
                        this.starlights[(int)finger1[i]] = light1[i];
                    }
                }
            }
        }

        public Starlight StarlightIndexOf(
            Finger finger,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
        )
        {
            Starlight found;

            if (0<=(int)finger && (int)finger < this.starlights.Count)
            {
                found = this.starlights[(int)finger];
            }
            else
            {
                string message = this.GetType().Name + "#StarIndexOf：　スプライト配列の範囲を外れた添え字を指定されましたので、取得できません。スプライト番号=[" + finger + "] / スプライトの数=[" + this.starlights.Count + "]\n memberName=" + memberName + "\n sourceFilePath=" + sourceFilePath + "\n sourceLineNumber=" + sourceLineNumber;
                Debug.Fail(message);
                throw new Exception(message);
            }

            return found;
        }


        public void Foreach_Starlights(SkyBuffer.DELEGATE_Sky_Foreach delegate_Sky_Foreach)
        {
            bool toBreak = false;

            Finger finger = 0;
            foreach (Starlight light in this.starlights)
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

            this.Foreach_Starlights((Finger finger, Starlight light, ref bool toBreak) =>
            {
                fingers.Add(finger);
            });

            return fingers;
        }


    }
}
