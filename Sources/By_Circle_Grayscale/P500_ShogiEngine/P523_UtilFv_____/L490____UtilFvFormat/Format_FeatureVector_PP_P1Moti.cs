using Grayscale.P062_ConvText___.L500____Converter;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P214_Masu_______.L500____Util;
using Grayscale.P521_FeatureVect.L___500_Struct;
using Grayscale.P521_FeatureVect.L500____Struct;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grayscale.P523_UtilFv_____.L490____UtilFvFormat
{
    /// <summary>
    /// 二駒関係ＰＰで、Ｐ１が持ち駒のもの。
    /// </summary>
    public abstract class Format_FeatureVector_PP_P1Moti
    {

        private class PpItem_P2Banjo
        {
            public string Title { get; set; }
            public int P2_base { get; set; }
            public PpItem_P2Banjo(string title, int p2_base)
            {
                this.Title = title;
                this.P2_base = p2_base;
            }
        }

        private class PpItem_P2Moti : PpItem_P2Banjo
        {
            public int P2MaisuLength { get; set; }
            public PpItem_P2Moti(string title, int p2_base, int p2MaisuLength)
                : base(title, p2_base)
            {
                this.P2MaisuLength = p2MaisuLength;
            }
        }

        /// <summary>
        /// テキストを作ります。
        /// PP の P1。１９枚の持駒。
        /// </summary>
        /// <param name="fv"></param>
        /// <param name="title"></param>
        /// <param name="p1_base_motiFu">持ち駒の位置から。</param>
        /// <returns></returns>
        public static string Format_PP_P1_Moti19Mai(FeatureVector fv, string title, int p1_base_motiFu)
        {
            StringBuilder sb = new StringBuilder();

            //
            // コメント
            //
            sb.AppendLine("\"#紹介文\",");
            sb.AppendLine("\"#\",");
            sb.AppendLine("\"#ボナンザ６．０アレンジ式きふわらべ２駒関係\",");
            sb.AppendLine("\"#\",");
            sb.AppendLine("\"#----------------------------------------\",");
            sb.AppendLine("\"# ");
            sb.AppendLine(title);
            sb.AppendLine("\",");
            sb.AppendLine("\"#----------------------------------------\",");
            sb.AppendLine("\"#\",");
            sb.AppendLine("\"#常に先手は正の数、後手は負の数の絶対値が大きい方が有利。０は互角。\",");

            //
            // 仕様バージョン
            //
            sb.AppendLine();
            sb.AppendLine("\"Version\",1.0,");
            sb.AppendLine();

            //----------------------------------------
            // 19将棋盤リスト
            //----------------------------------------
            // vs 1P歩 81升
            // vs 1P香 81升
            // vs 1P桂 81升
            // vs 1P銀 81升
            // vs 1P金 81升
            // vs 1P飛 81升
            // vs 1P角 81升
            // vs 2P歩 81升
            // vs 2P香 81升
            // vs 2P桂 81升
            // vs 2P銀 81升
            // vs 2P金 81升
            // vs 2P飛 81升
            // vs 2P角 81升
            //----------------------------------------
            {
                List<PpItem_P2Banjo> p2List = new List<PpItem_P2Banjo>();
                //if (true)
                //{
                    p2List.Add(new PpItem_P2Banjo("vs 1P歩", FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____FU_____));//盤上の駒の項目位置
                    p2List.Add(new PpItem_P2Banjo("vs 1P香", FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KYO____));
                    p2List.Add(new PpItem_P2Banjo("vs 1P桂", FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KEI____));
                    p2List.Add(new PpItem_P2Banjo("vs 1P銀", FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____GIN____));
                    p2List.Add(new PpItem_P2Banjo("vs 1P金", FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KIN____));
                    p2List.Add(new PpItem_P2Banjo("vs 1P飛", FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____HISYA__));
                //}
                p2List.Add(new PpItem_P2Banjo("vs 1P角", FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KAKU___));
                //if (true)
                //{
                    p2List.Add(new PpItem_P2Banjo("vs 2P歩", FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____FU_____));
                    p2List.Add(new PpItem_P2Banjo("vs 2P香", FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KYO____));
                    p2List.Add(new PpItem_P2Banjo("vs 2P桂", FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KEI____));
                    p2List.Add(new PpItem_P2Banjo("vs 2P銀", FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____GIN____));
                    p2List.Add(new PpItem_P2Banjo("vs 2P金", FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KIN____));
                    p2List.Add(new PpItem_P2Banjo("vs 2P飛", FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____HISYA__));
                //}
                p2List.Add(new PpItem_P2Banjo("vs 2P角", FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KAKU___));
                foreach (PpItem_P2Banjo p2Item in p2List)
                {
                    sb.Append(Format_FeatureVector_PP_P1Moti.Format_19Shogibans(fv, p1_base_motiFu, p2Item));
                }
            }

            //----------------------------------------
            // vs 1P持ち歩 0～18枚
            // vs 1P持ち香 0～4枚
            // vs 1P持ち桂 0～4枚
            // vs 1P持ち銀 0～4枚
            // vs 1P持ち金 0～4枚
            // vs 1P持ち飛 0～2枚
            // vs 1P持ち角 0～2枚
            // vs 2P持ち歩 0～18枚
            // vs 2P持ち香 0～4枚
            // vs 2P持ち桂 0～4枚
            // vs 2P持ち銀 0～4枚
            // vs 2P持ち金 0～4枚
            // vs 2P持ち飛 0～2枚
            // vs 2P持ち角 0～2枚
            // 計 45項目
            //----------------------------------------
            int p1MaisuLength = 19;
            {
                List<PpItem_P2Moti> p2List = new List<PpItem_P2Moti>();


                // p2の 1Pと2P
                for (int p2Player = 1; p2Player < 3; p2Player++)
                {
                    int koumokuIndex;
                    if (p2Player == 1)
                    {
                        koumokuIndex = FeatureVectorImpl.CHOSA_KOMOKU_1P;
                    }
                    else if (p2Player == 2)
                    {
                        koumokuIndex = FeatureVectorImpl.CHOSA_KOMOKU_2P;
                    }
                    else
                    {
                        throw new Exception("範囲外");
                    }

                    // vs 持ち駒 1P歩x 0～18
                    p2List.Add(new PpItem_P2Moti(string.Format("vs 持ち駒 {0}P歩x0～18", p2Player), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIFU_____, 19));

                    // vs 持ち駒 1P香x 0～4
                    p2List.Add(new PpItem_P2Moti(string.Format("vs 持ち駒 {0}P香x0～5", p2Player), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKYO____, 5));

                    // vs 持ち駒 1P桂x 0～4
                    p2List.Add(new PpItem_P2Moti(string.Format("vs 持ち駒 {0}P桂x0～5", p2Player), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKEI____, 5));

                    // vs 持ち駒 1P銀x 0～4
                    p2List.Add(new PpItem_P2Moti(string.Format("vs 持ち駒 {0}P銀x0～5", p2Player), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIGIN____, 5));

                    // vs 持ち駒 1P金x 0～4
                    p2List.Add(new PpItem_P2Moti(string.Format("vs 持ち駒 {0}P金x0～5", p2Player), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKIN____, 5));

                    // vs 持ち駒 1P飛x 0～4
                    p2List.Add(new PpItem_P2Moti(string.Format("vs 持ち駒 {0}P飛x0～2", p2Player), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIHISYA__, 3));

                    // vs 持ち駒 1P角x 0～4
                    p2List.Add(new PpItem_P2Moti(string.Format("vs 持ち駒 {0}P角x0～2", p2Player), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKAKU___, 3));
                }
                // 計90項目

                for (int moti = 0; moti < p2List.Count; moti++)
                {
                    // サンプルで作るだけ
                    sb.Append(Format_FeatureVector_PP_P1Moti.Format_MaisuList(fv, p1_base_motiFu, p1MaisuLength, p2List[moti]));
                }
            }


            return sb.ToString();
        }

        /// <summary>
        /// テキストを作ります。
        /// </summary>
        /// <param name="fv"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public static string Format_PP_P1Moti_5Mai(FeatureVector fv, string title, int p1Koumoku)
        {
            int p1MaisuLength = 5;

            StringBuilder sb = new StringBuilder();

            //
            // コメント
            //
            sb.AppendLine("\"#紹介文\",");
            sb.AppendLine("\"#\",");
            sb.AppendLine("\"#ボナンザ６．０アレンジ式きふわらべ２駒関係\",");
            sb.AppendLine("\"#\",");
            sb.AppendLine("\"#----------------------------------------\",");
            sb.AppendLine("\"# ");
            sb.AppendLine(title);
            sb.AppendLine("\",");
            sb.AppendLine("\"#----------------------------------------\",");
            sb.AppendLine("\"#\",");
            sb.AppendLine("\"#常に先手は正の数、後手は負の数の絶対値が大きい方が有利。０は互角。\",");

            //
            // 仕様バージョン
            //
            sb.AppendLine();
            sb.AppendLine("\"Version\",1.0,");
            sb.AppendLine();

            //----------------------------------------
            // 5将棋盤リスト
            //----------------------------------------
            // vs 1P歩 81升
            // vs 1P香 81升
            // vs 1P桂 81升
            // vs 1P銀 81升
            // vs 1P金 81升
            // vs 1P飛 81升
            // vs 1P角 81升
            // vs 2P歩 81升
            // vs 2P香 81升
            // vs 2P桂 81升
            // vs 2P銀 81升
            // vs 2P金 81升
            // vs 2P飛 81升
            // vs 2P角 81升
            //----------------------------------------
            {
                List<PpItem_P2Banjo> p2List = new List<PpItem_P2Banjo>();
                p2List.Add(new PpItem_P2Banjo("vs 1P歩", FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____FU_____));
                p2List.Add(new PpItem_P2Banjo("vs 1P香", FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KYO____));
                p2List.Add(new PpItem_P2Banjo("vs 1P桂", FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KEI____));
                p2List.Add(new PpItem_P2Banjo("vs 1P銀", FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____GIN____));
                p2List.Add(new PpItem_P2Banjo("vs 1P金", FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KIN____));
                p2List.Add(new PpItem_P2Banjo("vs 1P飛", FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____HISYA__));
                p2List.Add(new PpItem_P2Banjo("vs 1P角", FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KAKU___));
                p2List.Add(new PpItem_P2Banjo("vs 2P歩", FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____FU_____));
                p2List.Add(new PpItem_P2Banjo("vs 2P香", FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KYO____));
                p2List.Add(new PpItem_P2Banjo("vs 2P桂", FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KEI____));
                p2List.Add(new PpItem_P2Banjo("vs 2P銀", FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____GIN____));
                p2List.Add(new PpItem_P2Banjo("vs 2P金", FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KIN____));
                p2List.Add(new PpItem_P2Banjo("vs 2P飛", FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____HISYA__));
                p2List.Add(new PpItem_P2Banjo("vs 2P角", FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KAKU___));
                foreach (PpItem_P2Banjo p2Item in p2List)
                {
                    sb.Append(Format_FeatureVector_PP_P1Moti.Format_3or5Shogibans(fv, p1Koumoku, p2Item, 5));
                }
            }
            //----------------------------------------
            // vs 1P持ち歩 0～18枚
            // vs 1P持ち香 0～4枚
            // vs 1P持ち桂 0～4枚
            // vs 1P持ち銀 0～4枚
            // vs 1P持ち金 0～4枚
            // vs 1P持ち飛 0～2枚
            // vs 1P持ち角 0～2枚
            // vs 2P持ち歩 0～18枚
            // vs 2P持ち香 0～4枚
            // vs 2P持ち桂 0～4枚
            // vs 2P持ち銀 0～4枚
            // vs 2P持ち金 0～4枚
            // vs 2P持ち飛 0～2枚
            // vs 2P持ち角 0～2枚
            // 計 45項目
            //----------------------------------------
            {
                List<PpItem_P2Moti> p2List = new List<PpItem_P2Moti>();


                // p2の 1Pと2P
                for (int p2Player = 1; p2Player < 3; p2Player++)
                {
                    int koumokuIndex;
                    if (p2Player == 1)
                    {
                        koumokuIndex = FeatureVectorImpl.CHOSA_KOMOKU_1P;
                    }
                    else if (p2Player == 2)
                    {
                        koumokuIndex = FeatureVectorImpl.CHOSA_KOMOKU_2P;
                    }
                    else
                    {
                        throw new Exception("範囲外");
                    }

                    // vs 持ち駒 1P歩x 0～18
                    p2List.Add(new PpItem_P2Moti(string.Format("vs 持ち駒 {0}P歩x0～18", p2Player), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIFU_____, 19));

                    // vs 持ち駒 1P香x 0～4
                    p2List.Add(new PpItem_P2Moti(string.Format("vs 持ち駒 {0}P香x0～5", p2Player), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKYO____, 5));

                    // vs 持ち駒 1P桂x 0～4
                    p2List.Add(new PpItem_P2Moti(string.Format("vs 持ち駒 {0}P桂x0～5", p2Player), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKEI____, 5));

                    // vs 持ち駒 1P銀x 0～4
                    p2List.Add(new PpItem_P2Moti(string.Format("vs 持ち駒 {0}P銀x0～5", p2Player), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIGIN____, 5));

                    // vs 持ち駒 1P金x 0～4
                    p2List.Add(new PpItem_P2Moti(string.Format("vs 持ち駒 {0}P金x0～5", p2Player), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKIN____, 5));

                    // vs 持ち駒 1P飛x 0～4
                    p2List.Add(new PpItem_P2Moti(string.Format("vs 持ち駒 {0}P飛x0～2", p2Player), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIHISYA__, 3));

                    // vs 持ち駒 1P角x 0～4
                    p2List.Add(new PpItem_P2Moti(string.Format("vs 持ち駒 {0}P角x0～2", p2Player), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKAKU___, 3));
                }
                // 計90項目

                for (int moti = 0; moti < p2List.Count; moti++)
                {
                    // サンプルで作るだけ
                    sb.Append(Format_FeatureVector_PP_P1Moti.Format_MaisuList(fv, p1Koumoku, p1MaisuLength, p2List[moti]));
                }
            }



            return sb.ToString();
        }
        /// <summary>
        /// テキストを作ります。
        /// </summary>
        /// <param name="fv"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public static string Format_PP_P1Moti_3Mai(FeatureVector fv, string title, int p1Koumoku)
        {
            int p1MaisuLength = 3;

            //float[,] nikomaKankei_PP = fv.NikomaKankei_PP;
            StringBuilder sb = new StringBuilder();

            //
            // コメント
            //
            sb.AppendLine("\"#紹介文\",");
            sb.AppendLine("\"#\",");
            sb.AppendLine("\"#ボナンザ６．０アレンジ式きふわらべ２駒関係\",");
            sb.AppendLine("\"#\",");
            sb.AppendLine("\"#----------------------------------------\",");
            sb.AppendLine("\"# ");
            sb.AppendLine(title);
            sb.AppendLine("\",");
            sb.AppendLine("\"#----------------------------------------\",");
            sb.AppendLine("\"#\",");
            sb.AppendLine("\"#常に先手は正の数、後手は負の数の絶対値が大きい方が有利。０は互角。\",");

            //
            // 仕様バージョン
            //
            sb.AppendLine();
            sb.AppendLine("\"Version\",1.0,");
            sb.AppendLine();

            //----------------------------------------
            // vs 1P歩 81升
            // vs 1P香 81升
            // vs 1P桂 81升
            // vs 1P銀 81升
            // vs 1P金 81升
            // vs 1P飛 81升
            // vs 1P角 81升
            // vs 2P歩 81升
            // vs 2P香 81升
            // vs 2P桂 81升
            // vs 2P銀 81升
            // vs 2P金 81升
            // vs 2P飛 81升
            // vs 2P角 81升
            //----------------------------------------
            {
                List<PpItem_P2Banjo> p2List = new List<PpItem_P2Banjo>();
                p2List.Add(new PpItem_P2Banjo("vs 1P歩", FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____FU_____));
                p2List.Add(new PpItem_P2Banjo("vs 1P香", FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KYO____));
                p2List.Add(new PpItem_P2Banjo("vs 1P桂", FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KEI____));
                p2List.Add(new PpItem_P2Banjo("vs 1P銀", FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____GIN____));
                p2List.Add(new PpItem_P2Banjo("vs 1P金", FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KIN____));
                p2List.Add(new PpItem_P2Banjo("vs 1P飛", FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____HISYA__));
                p2List.Add(new PpItem_P2Banjo("vs 1P角", FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KAKU___));
                p2List.Add(new PpItem_P2Banjo("vs 2P歩", FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____FU_____));
                p2List.Add(new PpItem_P2Banjo("vs 2P香", FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KYO____));
                p2List.Add(new PpItem_P2Banjo("vs 2P桂", FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KEI____));
                p2List.Add(new PpItem_P2Banjo("vs 2P銀", FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____GIN____));
                p2List.Add(new PpItem_P2Banjo("vs 2P金", FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KIN____));
                p2List.Add(new PpItem_P2Banjo("vs 2P飛", FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____HISYA__));
                p2List.Add(new PpItem_P2Banjo("vs 2P角", FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KAKU___));
                foreach (PpItem_P2Banjo p2Item in p2List)
                {
                    sb.Append(Format_FeatureVector_PP_P1Moti.Format_3or5Shogibans(fv, p1Koumoku, p2Item, 3));
                }
            }
            //----------------------------------------
            // vs 1P持ち歩 0～18枚
            // vs 1P持ち香 0～4枚
            // vs 1P持ち桂 0～4枚
            // vs 1P持ち銀 0～4枚
            // vs 1P持ち金 0～4枚
            // vs 1P持ち飛 0～2枚
            // vs 1P持ち角 0～2枚
            // vs 2P持ち歩 0～18枚
            // vs 2P持ち香 0～4枚
            // vs 2P持ち桂 0～4枚
            // vs 2P持ち銀 0～4枚
            // vs 2P持ち金 0～4枚
            // vs 2P持ち飛 0～2枚
            // vs 2P持ち角 0～2枚
            // 計 45項目
            //----------------------------------------
            {
                List<PpItem_P2Moti> p2List = new List<PpItem_P2Moti>();


                // p2の 1Pと2P
                for (int p2Player = 1; p2Player < 3; p2Player++)
                {
                    int koumokuIndex;
                    if (p2Player == 1)
                    {
                        koumokuIndex = FeatureVectorImpl.CHOSA_KOMOKU_1P;
                    }
                    else if (p2Player == 2)
                    {
                        koumokuIndex = FeatureVectorImpl.CHOSA_KOMOKU_2P;
                    }
                    else
                    {
                        throw new Exception("範囲外");
                    }

                    // vs 持ち駒 1P歩x 0～18
                    p2List.Add(new PpItem_P2Moti(string.Format("vs 持ち駒 {0}P歩x0～18", p2Player), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIFU_____, 19));

                    // vs 持ち駒 1P香x 0～4
                    p2List.Add(new PpItem_P2Moti(string.Format("vs 持ち駒 {0}P香x0～5", p2Player), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKYO____, 5));

                    // vs 持ち駒 1P桂x 0～4
                    p2List.Add(new PpItem_P2Moti(string.Format("vs 持ち駒 {0}P桂x0～5", p2Player), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKEI____, 5));

                    // vs 持ち駒 1P銀x 0～4
                    p2List.Add(new PpItem_P2Moti(string.Format("vs 持ち駒 {0}P銀x0～5", p2Player), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIGIN____, 5));

                    // vs 持ち駒 1P金x 0～4
                    p2List.Add(new PpItem_P2Moti(string.Format("vs 持ち駒 {0}P金x0～5", p2Player), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKIN____, 5));

                    // vs 持ち駒 1P飛x 0～4
                    p2List.Add(new PpItem_P2Moti(string.Format("vs 持ち駒 {0}P飛x0～2", p2Player), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIHISYA__, 3));

                    // vs 持ち駒 1P角x 0～4
                    p2List.Add(new PpItem_P2Moti(string.Format("vs 持ち駒 {0}P角x0～2", p2Player), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKAKU___, 3));
                }
                // 計90項目

                for (int moti = 0; moti < p2List.Count; moti++)
                {
                    // サンプルで作るだけ
                    sb.Append(Format_FeatureVector_PP_P1Moti.Format_MaisuList(fv, p1Koumoku, p1MaisuLength, p2List[moti]));
                }
            }



            return sb.ToString();
        }










        /// <summary>
        /// 19枚の将棋盤。P1が「持ち歩」のときに使用。
        /// </summary>
        /// <param name="fv"></param>
        /// <param name="p1_base_motiFu">持ち駒の位置から。</param>
        /// <param name="p2Item"></param>
        /// <returns></returns>
        private static string Format_19Shogibans(FeatureVector fv, int p1_base_motiFu, PpItem_P2Banjo p2Item)
        {
            StringBuilder sb = new StringBuilder();

            //----------------------------------------
            // タイトル行
            //----------------------------------------
            sb.AppendLine("\"#----------------------------------------\",");
            sb.Append("\"#");
            sb.Append(p2Item.Title);
            sb.AppendLine("\",");
            sb.AppendLine("\"#----------------------------------------\",");

            //----------------------------------------
            // 2段を用意します。（大グループ）
            // 上段が 0～9枚のとき。（10枚の将棋盤）
            // 下段が 10～18枚のとき。（9枚の将棋盤）
            //----------------------------------------
            for (int largeRow = 0; largeRow < 2; largeRow++)
            {
                // 列見出し行を作ります。
                sb.Append("\"#");//2文字
                for (int largeColumn = 0; largeColumn < 10; largeColumn++)
                {
                    if (largeRow == 1 && largeColumn == 9)
                    {
                        // 下段には 大10列目はありません。
                        break;
                    }

                    if (largeColumn != 0)
                    {
                        sb.Append("    ");//4文字（列間）
                    }

                    // 大テーブルの枚数
                    int maisu = largeRow * 10 + largeColumn;
                    string maisuStr10;
                    if (maisu < 10)
                    {
                        maisuStr10 = "　";
                    }
                    else
                    {
                        maisuStr10 = "１";
                    }
                    string maisuStr01 = Conv_Int.ToArabiaSuji(maisu%10);
                    sb.Append(" ");
                    sb.Append(maisuStr10);
                    sb.Append(maisuStr01);
                    sb.Append("枚        ");//15文字（3列分）
                    for (int col = 0; col < 6; col++)
                    {
                        sb.Append("     ");//5文字
                    }

                    if (Const_FeatureVectorFormat.PARAMETER_INDEX_OUTPUT)
                    {
                        sb.Append("                                             ");//調整
                    }
                }
                sb.AppendLine("\",");

                //----------------------------------------
                // p2 が 一段～九段
                //----------------------------------------
                for (int smallDan = 1; smallDan < 10; smallDan++)
                {
                    // 行頭
                    sb.Append("    ");//4文字
                    //----------------------------------------
                    // 大グループが 0列～9列。
                    //----------------------------------------
                    for (int largeColumn = 0; largeColumn < 10; largeColumn++)
                    {
                        if (largeRow == 1 && largeColumn == 9)
                        {
                            // 下段には 大10列目はありません。
                            break;
                        }

                        if (largeColumn != 0)
                        {
                            // 表の横の隙間
                            sb.Append("    ");
                        }

                        // 大グループの枚数
                        int maisu = largeRow * 10 + largeColumn;//0～18

                        //----------------------------------------
                        // p2 が ９筋～１筋
                        //----------------------------------------
                        for (int smallSuji = 9; smallSuji > 0; smallSuji--)
                        {
                            // 0～80
                            int smallMasu = Util_Masu10.Handle_OkibaSujiDanToMasu(Okiba.ShogiBan, smallSuji, smallDan);

                            int p1 = p1_base_motiFu + maisu;
                            int p2 = p2Item.P2_base + smallMasu;
                            if (Const_FeatureVectorFormat.PARAMETER_INDEX_OUTPUT)
                            {
                                sb.Append(string.Format("{0,4}_{1,4}", p1, p2));
                            }
                            else
                            {
                                // スコアの倍率を復元します。
                                float scoreF = fv.NikomaKankeiPp_ForMemory[p1, p2] / fv.Bairitu_NikomaKankeiPp;
                                int value = (int)Math.Round(scoreF, 0);//小数点以下を丸めます。
                                sb.Append(string.Format("{0,4}", value));//アウトプットはok:持ち歩 vs 盤上の駒
                            }

                            sb.Append(",");
                        }
                    }
                    // 次の段へ
                    sb.AppendLine();

                }
                // 段の隙間
                sb.AppendLine();
            }

            return sb.ToString();
        }

        /// <summary>
        /// 5枚の将棋盤。P1が「持ち香」のときなどに使用。
        /// largeColumnMaxを指定して、3枚 or 5枚の将棋盤にすることも可能。
        /// </summary>
        /// <returns></returns>
        private static string Format_3or5Shogibans(FeatureVector fv, int p1_base, PpItem_P2Banjo p2Item, int largeColumnMax)
        {
            StringBuilder sb = new StringBuilder();

            //----------------------------------------
            // タイトル行
            //----------------------------------------
            sb.AppendLine("\"#----------------------------------------\",");
            sb.Append("\"#");
            sb.Append(p2Item.Title);
            sb.AppendLine("\",");
            sb.AppendLine("\"#----------------------------------------\",");

            //----------------------------------------
            // 1段で、横に5枚の将棋盤を並べます。
            //----------------------------------------
            {
                // 列見出し行を作ります。
                sb.Append("\"#");//2文字
                for (int largeColumn = 0; largeColumn < largeColumnMax; largeColumn++)
                {
                    if (largeColumn != 0)
                    {
                        sb.Append("    ");//4文字（列間）
                    }

                    // 大グループの枚数
                    int maisu = largeColumn;
                    sb.Append(" ");
                    sb.Append("　");//十の位は空っぽ。
                    sb.Append(Conv_Int.ToArabiaSuji(maisu % 10));//一の位。
                    sb.Append("枚        ");//15文字（3列分）
                    for (int col = 0; col < 6; col++)
                    {
                        sb.Append("     ");//5文字
                    }
                }
                sb.AppendLine("\",");

                //----------------------------------------
                // p2 が 一段～九段
                //----------------------------------------
                for (int p2Dan = 1; p2Dan < 10; p2Dan++)
                {
                    // 行頭
                    sb.Append("    ");//4文字
                    //----------------------------------------
                    // 大グループが 0列～9列。
                    //----------------------------------------
                    for (int largeColumn = 0; largeColumn < largeColumnMax; largeColumn++)
                    {
                        if (largeColumn != 0)
                        {
                            // 表の横の隙間
                            sb.Append("    ");
                        }

                        // 大グループの枚数
                        int maisu = largeColumn;

                        //----------------------------------------
                        // p2 が ９筋～１筋
                        //----------------------------------------
                        for (int p2Suji = 9; p2Suji > 0; p2Suji--)
                        {
                            int pMasu = Util_Masu10.Handle_OkibaSujiDanToMasu(Okiba.ShogiBan, p2Suji, p2Dan);

                            int p1 = p1_base + maisu;
                            int p2 = p2Item.P2_base + pMasu;
                            if (Const_FeatureVectorFormat.PARAMETER_INDEX_OUTPUT)
                            {
                                sb.Append(string.Format("{0,4}_{1,4}", p1, p2));
                            }
                            else
                            {
                                // スコアの倍率を復元します。
                                float scoreF = fv.NikomaKankeiPp_ForMemory[p1, p2] / fv.Bairitu_NikomaKankeiPp;
                                int value = (int)Math.Round(scoreF, 0);//小数点以下を丸めます。
                                sb.Append(string.Format("{0,4}", value));
                            }
                            sb.Append(",");
                        }
                    }
                    // 次の段へ
                    sb.AppendLine();

                }
                // 段の隙間
                sb.AppendLine();
            }

            return sb.ToString();
        }

        /// <summary>
        /// PP。ただの枚数分のリスト。持ち駒の一覧に利用。
        /// </summary>
        /// <returns></returns>
        private static string Format_MaisuList(FeatureVector fv, int p1_base, int p1MaisuLength, PpItem_P2Moti p2Item)
        {
            StringBuilder sb = new StringBuilder();

            //----------------------------------------
            // タイトル行
            //----------------------------------------
            sb.AppendLine("\"#----------------------------------------\",");
            sb.Append("\"#");
            sb.Append(p2Item.Title);
            sb.AppendLine("\",");
            sb.AppendLine("\"#----------------------------------------\",");

            // 歩なら19行、香なら5行になるはずです。
            for (int p1MaisuIndex = 0; p1MaisuIndex < p1MaisuLength;p1MaisuIndex++ )
            {
                // 行頭
                sb.Append("    ");//4文字
                //----------------------------------------
                // 持ち駒の枚数 p2 が 0～N枚
                //----------------------------------------
                for (int p2Maisu = 0; p2Maisu < p2Item.P2MaisuLength; p2Maisu++)
                {
                    int p1 = p1_base + p1MaisuIndex;
                    int p2 = p2Item.P2_base + p2Maisu;
                    if (Const_FeatureVectorFormat.PARAMETER_INDEX_OUTPUT)
                    {
                        sb.Append(string.Format("{0,4}_{1,4}", p1, p2));
                    }
                    else
                    {
                        // スコアの倍率を復元します。
                        float scoreF = fv.NikomaKankeiPp_ForMemory[p1, p2] / fv.Bairitu_NikomaKankeiPp;
                        int value = (int)Math.Round(scoreF, 0);//小数点以下を丸めます。
                        sb.Append(string.Format("{0,4}", value));
                    }

                    sb.Append(",");
                }
                // 次の行へ
                sb.AppendLine();
            }

            return sb.ToString();
        }

    }
}
