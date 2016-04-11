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
    /// 二駒関係ＰＰで、Ｐ１が盤上の駒のもの。
    /// </summary>
    public abstract class Format_FeatureVector_PP_P1Banjo
    {

        private class PpItem_P2
        {
            public string Title { get; set; }
            public int P2_base { get; set; }
            public PpItem_P2(string title, int p2_base)
            {
                this.Title = title;
                this.P2_base = p2_base;
            }
        }

        /// <summary>
        /// テキストを作ります。
        /// </summary>
        /// <param name="fv"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public static string Format_PP_P1Banjo(FeatureVector fv, string title, int p1_base)
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
            // vs 1P歩
            // vs 1P香
            // vs 1P桂
            // vs 1P銀
            // vs 1P金
            // vs 1P飛
            // vs 1P角
            // vs 2P歩
            // vs 2P香
            // vs 2P桂
            // vs 2P銀
            // vs 2P金
            // vs 2P飛
            // vs 2P角
            //----------------------------------------
            {
                List<PpItem_P2> p2List = new List<PpItem_P2>()
                {
                    new PpItem_P2( "vs 1P歩",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____FU_____),
                    new PpItem_P2( "vs 1P香",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KYO____),
                    new PpItem_P2( "vs 1P桂",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KEI____),
                    new PpItem_P2( "vs 1P銀",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____GIN____),
                    new PpItem_P2( "vs 1P金",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KIN____),
                    new PpItem_P2( "vs 1P飛",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____HISYA__),
                    new PpItem_P2( "vs 1P角",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KAKU___),
                    new PpItem_P2( "vs 2P歩",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____FU_____),
                    new PpItem_P2( "vs 2P香",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KYO____),
                    new PpItem_P2( "vs 2P桂",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KEI____),
                    new PpItem_P2( "vs 2P銀",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____GIN____),
                    new PpItem_P2( "vs 2P金",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KIN____),
                    new PpItem_P2( "vs 2P飛",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____HISYA__),
                    new PpItem_P2( "vs 2P角",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KAKU___),
                };
                foreach (PpItem_P2 p2Item in p2List)
                {
                    sb.Append(Format_FeatureVector_PP_P1Banjo.Format_NestedShogiban_P1Banjo(fv, p1_base, p2Item));
                }
            }

            //----------------------------------------
            // vs 1P持ち歩
            // vs 1P持ち香
            // vs 1P持ち桂
            // vs 1P持ち銀
            // vs 1P持ち金
            // vs 1P持ち飛
            // vs 1P持ち角
            // vs 2P持ち歩
            // vs 2P持ち香
            // vs 2P持ち桂
            // vs 2P持ち銀
            // vs 2P持ち金
            // vs 2P持ち飛
            // vs 2P持ち角
            // 計 45項目
            //----------------------------------------
            {
                List<PpItem_P2> p2List = new List<PpItem_P2>();

                // pieceの 1Pと2P
                for ( int piecePlayer = 1; piecePlayer < 3; piecePlayer++)
                {
                    int koumokuIndex;
                    if (piecePlayer == 1)
                    {
                        koumokuIndex = FeatureVectorImpl.CHOSA_KOMOKU_1P;
                    }
                    else if (piecePlayer == 2)
                    {
                        koumokuIndex = FeatureVectorImpl.CHOSA_KOMOKU_2P;
                    }
                    else
                    {
                        throw new Exception("範囲外");
                    }

                    // vs 持ち駒 1P歩x 0～18
                    for (int maisu = 0; maisu < 19; maisu++)//枚数
                    {
                        p2List.Add(new PpItem_P2(string.Format("vs 持ち駒 {0}P歩x{1,2}", piecePlayer, maisu), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIFU_____ + maisu));
                    }

                    // vs 持ち駒 1P香x 0～4
                    for (int maisu = 0; maisu < 5; maisu++)//枚数
                    {
                        p2List.Add(new PpItem_P2(string.Format("vs 持ち駒 {0}P香x{1,2}", piecePlayer, maisu), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKYO____ + maisu));
                    }

                    // vs 持ち駒 1P桂x 0～4
                    for (int maisu = 0; maisu < 5; maisu++)//枚数
                    {
                        p2List.Add(new PpItem_P2(string.Format("vs 持ち駒 {0}P桂x{1,2}", piecePlayer, maisu), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKEI____ + maisu));
                    }

                    // vs 持ち駒 1P銀x 0～4
                    for (int maisu = 0; maisu < 5; maisu++)//枚数
                    {
                        p2List.Add(new PpItem_P2(string.Format("vs 持ち駒 {0}P銀x{1,2}", piecePlayer, maisu), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIGIN____ + maisu));
                    }

                    // vs 持ち駒 1P金x 0～4
                    for (int maisu = 0; maisu < 5; maisu++)//枚数
                    {
                        p2List.Add(new PpItem_P2(string.Format("vs 持ち駒 {0}P金x{1,2}", piecePlayer, maisu), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKIN____ + maisu));
                    }

                    // vs 持ち駒 1P飛x 0～4
                    for (int maisu = 0; maisu < 3; maisu++)//枚数
                    {
                        p2List.Add(new PpItem_P2(string.Format("vs 持ち駒 {0}P飛x{1,2}", piecePlayer, maisu), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIHISYA__ + maisu));
                    }

                    // vs 持ち駒 1P角x 0～4
                    for (int maisu = 0; maisu < 3; maisu++)//枚数
                    {
                        p2List.Add(new PpItem_P2(string.Format("vs 持ち駒 {0}P角x{1,2}", piecePlayer, maisu), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKAKU___ + maisu));
                    }
                }
                // 計90項目

                for (int moti = 0; moti < p2List.Count; moti++)
                {
                    // サンプルで作るだけ
                    sb.Append(Format_FeatureVector_PP_P1Banjo.Format_SingleShogiban_P1Banjo(fv, p1_base, p2List[moti]));
                }
            }



            return sb.ToString();
        }

        /// <summary>
        /// ２重の入れ子の将棋盤。
        /// </summary>
        /// <returns></returns>
        private static string Format_NestedShogiban_P1Banjo(FeatureVector fv, int p1_base, PpItem_P2 p2Item)
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
            // p1 が 一段～九段
            //----------------------------------------
            for (int p1Dan = 1; p1Dan < 10; p1Dan++)
            {
                // 列見出し行を作ります。
                sb.Append("\"#");//2文字
                string danStr = Conv_Int.ToKanSuji(p1Dan);
                for (int suji = 9; suji > 0; suji--)
                {
                    if (suji != 9)
                    {
                        sb.Append("    ");//4文字（列間）
                    }
                    string sujiStr = Conv_Int.ToArabiaSuji(suji);
                    sb.Append(" ");
                    sb.Append(sujiStr);
                    sb.Append(danStr);
                    sb.Append("          ");//15文字（3列分）
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
                for (int p2Dan = 1; p2Dan < 10; p2Dan++)
                {
                    // 行頭
                    sb.Append("    ");//4文字
                    //----------------------------------------
                    // 任意駒 p1 が ９筋～１筋
                    //----------------------------------------
                    for (int p1Suji = 9; p1Suji > 0; p1Suji--)
                    {
                        if(p1Suji!=9)
                        {
                            // 表の横の隙間
                            sb.Append("    ");
                        }
                        // 大テーブル升は、P1。
                        int p1Masu = Util_Masu10.Handle_OkibaSujiDanToMasu(Okiba.ShogiBan, p1Suji, p1Dan);

                        //----------------------------------------
                        // p2 が ９筋～１筋
                        //----------------------------------------
                        for (int p2Suji = 9; p2Suji > 0; p2Suji--)
                        {
                            // 小テーブル升は、P2。
                            int p2Masu = Util_Masu10.Handle_OkibaSujiDanToMasu(Okiba.ShogiBan, p2Suji, p2Dan);

                            int p1 = p1_base + p1Masu;
                            int p2 = p2Item.P2_base + p2Masu;
                            if (Const_FeatureVectorFormat.PARAMETER_INDEX_OUTPUT)
                            {
                                sb.Append(string.Format("{0,4}_{1,4}", p1,p2));
                            }
                            else
                            {
                                // スコアの倍率を復元します。
                                float scoreF = fv.NikomaKankeiPp_ForMemory[p1, p2] / fv.Bairitu_NikomaKankeiPp;
                                int value = (int)Math.Round(scoreF, 0);//小数点以下を丸めます。
                                sb.Append(string.Format("{0,4}", value));//二重将棋盤
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
        /// １重の入れ子の将棋盤。持ち駒の一覧に利用。
        /// </summary>
        /// <returns></returns>
        private static string Format_SingleShogiban_P1Banjo(FeatureVector fv, int p1_base, PpItem_P2 p2Item)
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
            // 任意駒 p1 が 一段～九段
            //----------------------------------------
            for (int p1Dan = 1; p1Dan < 10; p1Dan++)
            {
                // 行頭
                sb.Append("    ");//4文字
                //----------------------------------------
                // 任意駒 p1 が ９筋～１筋
                //----------------------------------------
                for (int p1Suji = 9; p1Suji > 0; p1Suji--)
                {
                    // 任意駒の升
                    int nMasu = Util_Masu10.Handle_OkibaSujiDanToMasu(Okiba.ShogiBan, p1Suji, p1Dan);

                    int p1 = p1_base + nMasu;
                    int p2 = p2Item.P2_base;
                    if (Const_FeatureVectorFormat.PARAMETER_INDEX_OUTPUT)
                    {
                        sb.Append(string.Format("{0,4}_{1,4}", p1,p2));
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
                // 次の段へ
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
