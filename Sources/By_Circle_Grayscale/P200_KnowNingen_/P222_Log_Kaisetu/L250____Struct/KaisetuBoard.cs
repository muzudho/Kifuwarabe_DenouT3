using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P218_Starlight__.L___500_Struct;
using System.Collections.Generic;

namespace Grayscale.P222_Log_Kaisetu.L250____Struct
{

    /// <summary>
    /// グラフィカル局面ログの、盤１個。
    /// </summary>
    public class KaisetuBoard
    {
        /// <summary>
        /// あれば指し手。なければヌル。
        /// </summary>
        public Starbeamable sasiteOrNull{get;set;}

        /// <summary>
        /// 説明文章（１行）。
        /// </summary>
        public string Caption { get; set; }

        public Gkl_NounaiSeme NounaiSeme { get; set; }

        public List<Gkl_KomaMasu> KomaMasu1 { get; set; }
        public List<Gkl_KomaMasu> KomaMasu2 { get; set; }
        public List<Gkl_KomaMasu> KomaMasu3 { get; set; }
        public List<Gkl_KomaMasu> KomaMasu4 { get; set; }

        /// <summary>
        /// 手目済
        /// </summary>
        public int Temezumi { get; set; }

        /// <summary>
        /// 読み開始手目済み
        /// </summary>
        public int YomikaisiTemezumi { get; set; }

        public Playerside GenTeban { get; set; }
        public float Score { get; set; }//局面評価関数の評価値

        /// <summary>
        /// 色つきマス
        /// </summary>
        public List<int> Masu_theMove { get; set; }//移動可能
        public List<int> Masu_theEffect { get; set; }//利き
        public List<int> Masu_3 { get; set; }
        public List<int> Masu_4 { get; set; }

        /// <summary>
        /// マークがあるマス
        /// </summary>
        public List<int> MarkMasu1 { get; set; }
        public List<int> MarkMasu2 { get; set; }
        public List<int> MarkMasu3 { get; set; }
        public List<int> MarkMasu4 { get; set; }

        /// <summary>
        /// 矢印
        /// </summary>
        public List<Gkl_Arrow> Arrow { get; set; }


        public KaisetuBoard()
        {
            this.Caption = "";

            this.NounaiSeme = Gkl_NounaiSeme.Empty;
            this.KomaMasu1 = new List<Gkl_KomaMasu>();
            this.KomaMasu2 = new List<Gkl_KomaMasu>();
            this.KomaMasu3 = new List<Gkl_KomaMasu>();
            this.KomaMasu4 = new List<Gkl_KomaMasu>();

            this.Temezumi = int.MinValue;
            this.GenTeban = Playerside.Empty;
            this.YomikaisiTemezumi = int.MinValue;
            this.Score = float.MinValue;

            this.Masu_theMove = new List<int>();
            this.Masu_theEffect = new List<int>();
            this.Masu_3 = new List<int>();
            this.Masu_4 = new List<int>();

            this.MarkMasu1 = new List<int>();
            this.MarkMasu2 = new List<int>();
            this.MarkMasu3 = new List<int>();
            this.MarkMasu4 = new List<int>();

            this.Arrow = new List<Gkl_Arrow>();
        }

        public KaisetuBoard(KaisetuBoard src)
        {
            this.Caption = src.Caption;

            this.NounaiSeme = src.NounaiSeme;
            this.KomaMasu1 = src.KomaMasu1;
            this.KomaMasu2 = src.KomaMasu2;
            this.KomaMasu3 = src.KomaMasu3;
            this.KomaMasu4 = src.KomaMasu4;

            this.Temezumi = src.Temezumi;
            this.GenTeban = src.GenTeban;
            this.YomikaisiTemezumi = src.YomikaisiTemezumi;
            this.Score = src.Score;

            this.Masu_theMove = src.Masu_theMove;
            this.Masu_theEffect = src.Masu_theEffect;
            this.Masu_3 = src.Masu_3;
            this.Masu_4 = src.Masu_4;

            this.MarkMasu1 = src.MarkMasu1;
            this.MarkMasu2 = src.MarkMasu2;
            this.MarkMasu3 = src.MarkMasu3;
            this.MarkMasu4 = src.MarkMasu4;

            this.Arrow = src.Arrow;
        }

        public KaisetuBoard Clone()
        {
            KaisetuBoard clone = new KaisetuBoard(this);
            return clone;
        }


    }

    /// <summary>
    /// 脳内攻め（Playersideとはわざと区別してあります）
    /// </summary>
    public enum Gkl_NounaiSeme
    {
        Sente,
        Gote,
        Empty
    }

    /// <summary>
    /// 駒画像とマス
    /// </summary>
    public class Gkl_KomaMasu
    {
        public string KomaImg { get; set; }
        public int Masu { get; set; }

        public Gkl_KomaMasu(string komaImg, int masu)
        {
            this.KomaImg = komaImg;
            this.Masu = masu;
        }
    }

    /// <summary>
    /// 矢印１個
    /// </summary>
    public class Gkl_Arrow
    {
        public int From { get; set; }
        public int To { get; set; }

        public Gkl_Arrow()
        {
            this.From = int.MinValue;
            this.To = int.MinValue;
        }

        public Gkl_Arrow(int from, int to)
        {
            this.From = from;
            this.To = to;
        }
    }

}
