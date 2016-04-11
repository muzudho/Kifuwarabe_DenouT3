using Grayscale.P062_ConvText___.L500____Converter;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P214_Masu_______.L500____Util;
using Grayscale.P521_FeatureVect.L___500_Struct;
using Grayscale.P521_FeatureVect.L500____Struct;
using Grayscale.P522_Conv_FvKoumoku.L500____Converter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grayscale.P523_UtilFv_____.L490____UtilFvFormat
{
    public abstract class Format_FeatureVector_KP
    {

        private class Kp_P2Item
        {
            public string Title { get; set; }
            public int P2 { get; set; }
            public Kp_P2Item(string title, int p2)
            {
                this.Title = title;
                this.P2 = p2;
            }
        }

        /// <summary>
        /// テキストを作ります。
        /// </summary>
        /// <param name="fv"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public static string Format_KP(FeatureVector fv, Playerside k_pside)
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
            sb.AppendLine(string.Format("{0}P玉 ＫＰ表☆", (int)k_pside));
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
                Kp_P2Item[] kp_p2items = new Kp_P2Item[]{
                    new Kp_P2Item( "vs 1P歩",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____FU_____),
                    new Kp_P2Item( "vs 1P香",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KYO____),
                    new Kp_P2Item( "vs 1P桂",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KEI____),
                    new Kp_P2Item( "vs 1P銀",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____GIN____),
                    new Kp_P2Item( "vs 1P金",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KIN____),
                    new Kp_P2Item( "vs 1P飛",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____HISYA__),
                    new Kp_P2Item( "vs 1P角",FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KAKU___),
                    new Kp_P2Item( "vs 2P歩",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____FU_____),
                    new Kp_P2Item( "vs 2P香",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KYO____),
                    new Kp_P2Item( "vs 2P桂",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KEI____),
                    new Kp_P2Item( "vs 2P銀",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____GIN____),
                    new Kp_P2Item( "vs 2P金",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KIN____),
                    new Kp_P2Item( "vs 2P飛",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____HISYA__),
                    new Kp_P2Item( "vs 2P角",FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KAKU___),
                };
                for (int banjo = 0; banjo < kp_p2items.Length; banjo++)
                {
                    sb.Append(Format_FeatureVector_KP.Format_NestedShogiban(fv, k_pside, kp_p2items[banjo]));
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
                List<Kp_P2Item> kpList = new List<Kp_P2Item>();

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
                        kpList.Add(new Kp_P2Item(string.Format("vs 持ち駒 {0}P歩x{1,2}", piecePlayer, maisu), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIFU_____ + maisu));
                    }

                    // vs 持ち駒 1P香x 0～4
                    for (int maisu = 0; maisu < 5; maisu++)//枚数
                    {
                        kpList.Add(new Kp_P2Item(string.Format("vs 持ち駒 {0}P香x{1,2}", piecePlayer, maisu), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKYO____ + maisu));
                    }

                    // vs 持ち駒 1P桂x 0～4
                    for (int maisu = 0; maisu < 5; maisu++)//枚数
                    {
                        kpList.Add(new Kp_P2Item(string.Format("vs 持ち駒 {0}P桂x{1,2}", piecePlayer, maisu), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKEI____ + maisu));
                    }

                    // vs 持ち駒 1P銀x 0～4
                    for (int maisu = 0; maisu < 5; maisu++)//枚数
                    {
                        kpList.Add(new Kp_P2Item(string.Format("vs 持ち駒 {0}P銀x{1,2}", piecePlayer, maisu), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIGIN____ + maisu));
                    }

                    // vs 持ち駒 1P金x 0～4
                    for (int maisu = 0; maisu < 5; maisu++)//枚数
                    {
                        kpList.Add(new Kp_P2Item(string.Format("vs 持ち駒 {0}P金x{1,2}", piecePlayer, maisu), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKIN____ + maisu));
                    }

                    // vs 持ち駒 1P飛x 0～4
                    for (int maisu = 0; maisu < 3; maisu++)//枚数
                    {
                        kpList.Add(new Kp_P2Item(string.Format("vs 持ち駒 {0}P飛x{1,2}", piecePlayer, maisu), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIHISYA__ + maisu));
                    }

                    // vs 持ち駒 1P角x 0～4
                    for (int maisu = 0; maisu < 3; maisu++)//枚数
                    {
                        kpList.Add(new Kp_P2Item(string.Format("vs 持ち駒 {0}P角x{1,2}", piecePlayer, maisu), koumokuIndex + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKAKU___ + maisu));
                    }
                }
                // 計90項目

                for (int moti = 0; moti < kpList.Count; moti++)
                {
                    // サンプルで作るだけ
                    sb.Append(Format_FeatureVector_KP.Format_SingleShogiban(fv, k_pside, kpList[moti]));
                }
            }



            return sb.ToString();
        }

        /// <summary>
        /// ２重の入れ子の将棋盤。
        /// </summary>
        /// <returns></returns>
        private static string Format_NestedShogiban(FeatureVector fv, Playerside pside, Kp_P2Item kp_p2item)
        {
            StringBuilder sb = new StringBuilder();

            //----------------------------------------
            // タイトル行
            //----------------------------------------
            sb.AppendLine("\"#----------------------------------------\",");
            sb.Append("\"#");
            sb.Append(kp_p2item.Title);
            sb.AppendLine("\",");
            sb.AppendLine("\"#----------------------------------------\",");

            //----------------------------------------
            // K が 一段～九段
            //----------------------------------------
            for (int kDan = 1; kDan < 10; kDan++)
            {
                // 列見出し行を作ります。
                sb.Append("\"#");//2文字
                string danStr = Conv_Int.ToKanSuji(kDan);
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
                    sb.Append("玉        ");//15文字（3列分）
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
                // P が 一段～九段
                //----------------------------------------
                for (int pDan = 1; pDan < 10; pDan++)
                {
                    // 行頭
                    sb.Append("    ");//4文字
                    //----------------------------------------
                    // K が ９筋～１筋
                    //----------------------------------------
                    for (int kSuji = 9; kSuji > 0; kSuji--)
                    {
                        if(kSuji!=9)
                        {
                            // 表の横の隙間
                            sb.Append("    ");
                        }
                        int p1;
                        Conv_FvKoumoku522.Converter_K1_to_P(pside, kDan, kSuji, out p1);
                        //int kMasu = Util_Masu10.Handle_OkibaSujiDanToMasu(Okiba.ShogiBan, kSuji, kDan);

                        //----------------------------------------
                        // P が ９筋～１筋
                        //----------------------------------------
                        for (int pSuji = 9; pSuji > 0; pSuji--)
                        {
                            int pMasu = Util_Masu10.Handle_OkibaSujiDanToMasu(Okiba.ShogiBan, pSuji, pDan);

                            if (Const_FeatureVectorFormat.PARAMETER_INDEX_OUTPUT)
                            {
                                int p2 = kp_p2item.P2 + pMasu;
                                sb.Append(string.Format("{0,4}_{1,4}", p1, p2));
                            }
                            else
                            {
                                // スコアの倍率を復元します。
                                float scoreF = fv.NikomaKankeiPp_ForMemory[p1, kp_p2item.P2 + pMasu] / fv.Bairitu_NikomaKankeiPp;
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
        /// １重の入れ子の将棋盤。持ち駒の一覧に利用。
        /// </summary>
        /// <returns></returns>
        private static string Format_SingleShogiban(FeatureVector fv, Playerside pside, Kp_P2Item kp_p2Item)
        {
            StringBuilder sb = new StringBuilder();

            //----------------------------------------
            // タイトル行
            //----------------------------------------
            sb.AppendLine("\"#----------------------------------------\",");
            sb.Append("\"#");
            sb.Append(kp_p2Item.Title);
            sb.AppendLine("\",");
            sb.AppendLine("\"#----------------------------------------\",");

            //----------------------------------------
            // K が 一段～九段
            //----------------------------------------
            for (int kDan = 1; kDan < 10; kDan++)
            {
                // 行頭
                sb.Append("    ");//4文字
                //----------------------------------------
                // K が ９筋～１筋
                //----------------------------------------
                for (int kSuji = 9; kSuji > 0; kSuji--)
                {
                    //int kMasu = Util_Masu10.Handle_OkibaSujiDanToMasu(Okiba.ShogiBan, kSuji, kDan);
                    int k1;
                    Conv_FvKoumoku522.Converter_K1_to_P(pside, kDan, kSuji, out k1);

                    int p2 = kp_p2Item.P2;
                    if (Const_FeatureVectorFormat.PARAMETER_INDEX_OUTPUT)
                    {
                        sb.Append(string.Format("{0,4}_{1,4}", k1, p2));
                    }
                    else
                    {
                        // スコアの倍率を復元します。
                        float scoreF = fv.NikomaKankeiPp_ForMemory[k1, p2] / fv.Bairitu_NikomaKankeiPp;
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
