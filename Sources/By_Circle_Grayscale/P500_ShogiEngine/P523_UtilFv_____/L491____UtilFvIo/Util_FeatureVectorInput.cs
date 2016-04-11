using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P011_Csv________.L500____Util;
using Grayscale.P061_ConstShogi_.L250____Const;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P213_Komasyurui_.L250____Word;
using Grayscale.P214_Masu_______.L500____Util;
using Grayscale.P521_FeatureVect.L___500_Struct;
using Grayscale.P521_FeatureVect.L500____Struct;
using Grayscale.P522_Conv_FvKoumoku.L500____Converter;
using Grayscale.P523_UtilFv_____.L480____UtilFvEdit;
using Grayscale.P523_UtilFv_____.L490____UtilFvFormat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

#if DEBUG
//using Grayscale.P521_Atama_Unou_.L480____UtilFvEdit;
#endif

namespace Grayscale.P523_UtilFv_____.L491____UtilFvIo
{
    public abstract class Util_FeatureVectorInput
    {

        /// <summary>
        /// ファイルから読み込みます。
        /// 駒割。
        /// </summary>
        /// <param name="filepath1"></param>
        public static bool Make_FromFile_Komawari(FeatureVector fv, string filepath1)
        {
            bool successful = false;

            string filepath2 = Path.Combine(Application.StartupPath, filepath1);

            if (!File.Exists(filepath2))
            {
                MessageBox.Show("Make_FromFile_Komawari: ファイルがありません。\n"+
                    "filepath2=[" + filepath2 + "]\n" +
                    "Application.StartupPath=[" + Application.StartupPath+"]"
                    , "情報");
                goto gt_EndMethod;
            }

            List<List<string>> table = Util_Csv.ReadCsv(filepath2);

            int rowVersion = 0;
            int rowKomawari = 0;

            foreach (List<string> row in table)
            {
                if (row.Count < 1)//空行
                {
                    goto gt_Next;
                }

                string col0 = row[0].Trim();

                if (col0.StartsWith("#"))//コメント行
                {
                    goto gt_Next;
                }

                switch (col0)
                {
                    case "Version":
                        rowVersion++;
                        break;
                    case "Komawari":

                        if (row.Count < 2)//エラー
                        {
                            rowKomawari++;
                            goto gt_Next;
                        }
                        else
                        {
                            int col2;
                            int.TryParse(row[2], out col2);

                            switch (rowKomawari)
                            {
                                case 0: fv.Komawari[(int)Komasyurui14.H01_Fu_____] = col2; break;
                                case 1: fv.Komawari[(int)Komasyurui14.H02_Kyo____] = col2; break;
                                case 2: fv.Komawari[(int)Komasyurui14.H03_Kei____] = col2; break;
                                case 3: fv.Komawari[(int)Komasyurui14.H04_Gin____] = col2; break;
                                case 4: fv.Komawari[(int)Komasyurui14.H05_Kin____] = col2; break;
                                case 5: fv.Komawari[(int)Komasyurui14.H06_Gyoku__] = col2; break;
                                case 6: fv.Komawari[(int)Komasyurui14.H07_Hisya__] = col2; break;
                                case 7: fv.Komawari[(int)Komasyurui14.H08_Kaku___] = col2; break;
                                case 8: fv.Komawari[(int)Komasyurui14.H11_Tokin__] = col2; break;
                                case 9: fv.Komawari[(int)Komasyurui14.H12_NariKyo] = col2; break;
                                case 10: fv.Komawari[(int)Komasyurui14.H13_NariKei] = col2; break;
                                case 11: fv.Komawari[(int)Komasyurui14.H14_NariGin] = col2; break;
                                case 12: fv.Komawari[(int)Komasyurui14.H09_Ryu____] = col2; break;
                                case 13: fv.Komawari[(int)Komasyurui14.H10_Uma____] = col2; break;
                                default: break;
                            }
                        }
                        rowKomawari++;
                        break;

                    default:
                        break;
                }


            gt_Next:
                ;
            }

//#if DEBUG
//            MessageBox.Show(
//                "rowVersion=[" + rowVersion + "]\n" +
//                    "rowKomawari=[" + rowKomawari + "]\n"
//            , "デバッグ");
//#endif
            successful = true;
        gt_EndMethod:
            return successful;
        }


        /// <summary>
        /// ファイルから読み込みます。
        /// 評価値の割合を調整するもの。
        /// </summary>
        /// <param name="filepath1"></param>
        public static bool Make_FromFile_Scale(FeatureVector fv, string filepath1)
        {
            bool successful = false;

            string filepath2 = Path.Combine(Application.StartupPath, filepath1);

            if (!File.Exists(filepath2))
            {
                MessageBox.Show("Make_FromFile_Scale: ファイルがありません。\n" +
                    "filepath2=[" + filepath2 + "]\n" +
                    "Application.StartupPath=[" + Application.StartupPath + "]"
                    , "情報");
                goto gt_EndMethod;
            }

            List<List<string>> table = Util_Csv.ReadCsv(filepath2);

            int row_Version = 0;
            int row_NikomaKankeiPp = 0;

            foreach (List<string> row in table)
            {
                if (row.Count < 1)//空行は飛ばす。
                {
                    goto gt_Next;
                }

                string col0 = row[0].Trim();

                if (col0.StartsWith("#"))//コメント行
                {
                    goto gt_Next;
                }

                switch (col0)//1列目
                {
                    case "Version":
                        row_Version++;
                        break;
                    case "NikomaKankeiPp":

                        if (row.Count < 2)//2列無いのはエラー
                        {
                            row_NikomaKankeiPp++;
                            goto gt_Next;
                        }
                        else
                        {
                            switch (row_NikomaKankeiPp)
                            {
                                case 0:
                                    {
                                        // [0]列目は NikomaKankeiPP
                                        // [1]列目はコメント。
                                        // [2]列目が値。
                                        float value_NikomaKankeiPp = 0.4649f;//ダミー
                                        float.TryParse(row[2], out value_NikomaKankeiPp);
                                        fv.SetBairitu_NikomaKankeiPp( value_NikomaKankeiPp);
                                    }
                                    break;
                                default: break;
                            }
                        }
                        row_NikomaKankeiPp++;
                        break;

                    case "NikomaKankeiPp_TyoseiryoSmallest":

                        if (row.Count < 2)//2列無いのはエラー
                        {
                            goto gt_Next;
                        }
                        else
                        {
                            // [0]列目は プロパティー名。
                            // [1]列目はコメント。
                            // [2]列目が値。
                            float value = 0.4649f;//ダミー
                            float.TryParse(row[2], out value);
                            fv.SetTyoseiryoSmallest_NikomaKankeiPp(value);
                        }
                        break;

                    case "NikomaKankeiPp_TyoseiryoLargest":

                        if (row.Count < 2)//2列無いのはエラー
                        {
                            goto gt_Next;
                        }
                        else
                        {
                            // [0]列目は プロパティー名。
                            // [1]列目はコメント。
                            // [2]列目が値。
                            float value = 0.4649f;//ダミー
                            float.TryParse(row[2], out value);
                            fv.SetTyoseiryoLargest_NikomaKankeiPp(value);
                        }
                        break;

                    case "NikomaKankeiPp_TyoseiryoInit":

                        if (row.Count < 2)//2列無いのはエラー
                        {
                            goto gt_Next;
                        }
                        else
                        {
                            // [0]列目は プロパティー名。
                            // [1]列目はコメント。
                            // [2]列目が値。
                            float value = 0.4649f;//ダミー
                            float.TryParse(row[2], out value);
                            fv.SetTyoseiryoInit_NikomaKankeiPp(value);
                        }
                        break;

                    default:
                        break;
                }


            gt_Next:
                ;
            }

            successful = true;
        gt_EndMethod:
            return successful;
        }



        /// <summary>
        /// ファイルから読み込みます。
        /// </summary>
        /// <param name="filepath1"></param>
        public static bool Make_FromFile_KK(FeatureVector fv, string filepath1, KwErrorHandler errH)
        {
            bool successful = false;

            string filepath2 = Path.Combine(Application.StartupPath, filepath1);

#if DEBUG
            errH.Logger.WriteLine_AddMemo("----------------------------------------");
            errH.Logger.WriteLine_AddMemo("FV 総合点（読込前）2");
            errH.Logger.WriteLine_AddMemo("      PP =" + Util_FeatureVectorEdit.GetTotal_PP(fv));
            errH.Logger.WriteLine_AddMemo("----------------------------------------");
#endif

            if (!File.Exists(filepath2))
            {
                MessageBox.Show("Make_FromFile_KK: ファイルがありません。\n"+
                    "filepath2=[" + filepath2 + "]\n" +
                                    "Application.StartupPath=[" + Application.StartupPath + "]"
                , "情報");
                goto gt_EndMethod;
            }

            List<List<string>> table = Util_Csv.ReadCsv(filepath2);

            int rowVersion = 0;
            int rowKk = 0;

            foreach (List<string> row in table)
            {
                if (row.Count < 1)//空行はスキップ。
                {
                    goto gt_Next;
                }

                string col0 = row[0].Trim();

                if (col0.StartsWith("#"))//コメント行はスキップ。
                {
                    goto gt_Next;
                }

                switch (col0)
                {
                    case "Version":
                        rowVersion++;
                        break;

                    default:
                        if (row.Count < ConstShogi.SUJI_SIZE * ConstShogi.SUJI_SIZE)//エラー行は進む。
                        {
                            rowKk++;
                            goto gt_Next;
                        }
                        else
                        {
                            int k1dan = (int)(rowKk / 9) + 1;
                            int k2dan = (int)(rowKk % 9) + 1;

                            for (int col = 0; col < ConstShogi.SUJI_SIZE * ConstShogi.SUJI_SIZE; col++)
                            {
                                int k1suji = 9 - (int)(col / 9);
                                int k2suji = 9 - (int)(col % 9);

                                // 升番号
                                int k1 = Util_Masu10.Handle_OkibaSujiDanToMasu(Okiba.ShogiBan, k1suji, k1dan);
                                int k2 = Util_Masu10.Handle_OkibaSujiDanToMasu(Okiba.ShogiBan, k2suji, k2dan);

                                int p1;
                                Conv_FvKoumoku522.Converter_K1_to_P(Playerside.P1, k1dan, k1suji, out p1);
                                int p2;
                                Conv_FvKoumoku522.Converter_K1_to_P(Playerside.P2, k2dan, k2suji, out p2);


                                if (Const_FeatureVectorFormat.PARAMETER_INDEX_INPUT)
                                {
                                    // これは、評価値の代わりにインデックスを入れています。
                                    fv.NikomaKankeiPp_ForMemory[p1, p2] = k1*10000+k2;
                                }
                                else
                                {
                                    int value;
                                    int.TryParse(row[col], out value);

                                    // スコアに倍率を掛けます。
                                    float scoreF = value * fv.Bairitu_NikomaKankeiPp;
                                    fv.NikomaKankeiPp_ForMemory[p1, p2] = scoreF;
                                }
                            }
                        }
                        rowKk++;
                        break;
                }


            gt_Next:
                ;
            }

#if DEBUG
            errH.Logger.WriteLine_AddMemo("----------------------------------------");
            errH.Logger.WriteLine_AddMemo("FV 総合点（読込後）4");
            errH.Logger.WriteLine_AddMemo("      PP =" + Util_FeatureVectorEdit.GetTotal_PP(fv));
            errH.Logger.WriteLine_AddMemo("----------------------------------------");

            //MessageBox.Show(
            //    "rowVersion=[" + rowVersion + "]\n" +
            //            "rowKk=[" + rowKk + "]\n"
            //, "デバッグ");
#endif
            successful = true;
        gt_EndMethod:
            return successful;
        }


        /// <summary>
        /// ファイルから読み込みます。
        /// </summary>
        /// <param name="filepath1"></param>
        /// <param name="kingPlayer">1 or 2</param>
        /// <returns></returns>
        public static bool Make_FromFile_KP(FeatureVector fv, string filepath1, Playerside k_pside, KwErrorHandler errH)
        {
            bool successful = false;

            string filepath2 = Path.Combine(Application.StartupPath, filepath1);

#if DEBUG
            errH.Logger.WriteLine_AddMemo("----------------------------------------");
            errH.Logger.WriteLine_AddMemo("FV 総合点（読込前）6");
            errH.Logger.WriteLine_AddMemo("    PP =" + Util_FeatureVectorEdit.GetTotal_PP(fv));
            errH.Logger.WriteLine_AddMemo("----------------------------------------");
#endif

            if (!File.Exists(filepath2))
            {
                MessageBox.Show("Make_FromFile_KP: ファイルがありません。\n"+
                    "filepath2=[" + filepath2 + "]\n" +
                                        "Application.StartupPath=[" + Application.StartupPath + "]"
                , "情報");
                goto gt_EndMethod;
            }

            List<List<string>> table = Util_Csv.ReadCsv(filepath2);

            int rowVersion = 0;
            int rowBanjo = 0; //盤上の駒エリア
            int rowMoti = 0; //持ち駒エリア

            foreach (List<string> row in table)
            {
                if (row.Count < 1)//空行はスキップ。
                {
                    goto gt_Next;
                }

                string col0 = row[0].Trim();

                if (col0.StartsWith("#"))//コメント行はスキップ。
                {
                    goto gt_Next;
                }

                switch (col0)
                {
                    case "Version":
                        rowVersion++;
                        break;

                    default:
                        if (
                            row.Count == ConstShogi.SUJI_SIZE * ConstShogi.SUJI_SIZE // 盤状の駒エリア
                            )
                        {
                            int pKoumoku_base;
                            int area = (int)(rowBanjo / 81);
                            switch (area)
                            {
                                case 0: pKoumoku_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____FU_____; break;
                                case 1: pKoumoku_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KYO____; break;
                                case 2: pKoumoku_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KEI____; break;
                                case 3: pKoumoku_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____GIN____; break;
                                case 4: pKoumoku_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KIN____; break;
                                case 5: pKoumoku_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____HISYA__; break;
                                case 6: pKoumoku_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KAKU___; break;
                                case 7: pKoumoku_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____FU_____; break;
                                case 8: pKoumoku_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KYO____; break;
                                case 9: pKoumoku_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KEI____; break;
                                case 10: pKoumoku_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____GIN____; break;
                                case 11: pKoumoku_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KIN____; break;
                                case 12: pKoumoku_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____HISYA__; break;
                                case 13: pKoumoku_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KAKU___; break;
                                default: throw new Exception("範囲外rowBanjo=[" + rowBanjo + "]");
                            }
                            int kDan = (int)(rowBanjo % 81 / 9) + 1;
                            int pDan = (int)(rowBanjo % 9) + 1;

                            for (int col = 0; col < ConstShogi.SUJI_SIZE * ConstShogi.SUJI_SIZE; col++)
                            {
                                int kSuji = 9 - (int)(col / 9);
                                int pSuji = 9 - (int)(col % 9);

                                // 升番号。
                                int k1;
                                Conv_FvKoumoku522.Converter_K1_to_P(k_pside, kDan, kSuji, out k1);
                                int p2 = Util_Masu10.Handle_OkibaSujiDanToMasu(Okiba.ShogiBan, pSuji, pDan);

                                if (Const_FeatureVectorFormat.PARAMETER_INDEX_INPUT)
                                {
                                    // これは、評価値の代わりにインデックスを入れています。
                                    fv.NikomaKankeiPp_ForMemory[k1, pKoumoku_base + p2] = k1 * 10000 + (pKoumoku_base + p2);
                                }
                                else
                                {
                                    int value;
                                    int.TryParse(row[col], out value);

                                    // スコアに倍率を掛けます。
                                    float scoreF = value * fv.Bairitu_NikomaKankeiPp;
                                    fv.NikomaKankeiPp_ForMemory[k1, pKoumoku_base + p2] = scoreF;
                                }

                            }

                            rowBanjo++;
                        }
                        else if (
                            row.Count == ConstShogi.SUJI_SIZE // 持ち駒エリア
                            )
                        {
                            int p2;
                            int area = (int)(rowMoti / 9);
                            if (0 <= area && area <= 18)//1P歩
                            {
                                p2 = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIFU_____ + area;
                            }
                            else if (19 <= area && area <= 23)//1P香
                            {
                                p2 = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKYO____ + (area - 19);
                            }
                            else if (24 <= area && area <= 28)//1P桂
                            {
                                p2 = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKEI____ + (area - 24);
                            }
                            else if (29 <= area && area <= 33)//1P銀
                            {
                                p2 = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIGIN____ + (area - 29);
                            }
                            else if (34 <= area && area <= 38)//1P金
                            {
                                p2 = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKIN____ + (area - 34);
                            }
                            else if (39 <= area && area <= 41)//1P飛
                            {
                                p2 = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIHISYA__ + (area - 39);
                            }
                            else if (42 <= area && area <= 44)//1P角
                            {
                                p2 = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKAKU___ + (area - 42);
                            }
                            else if (45 <= area && area <= 63)//2P歩
                            {
                                p2 = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIFU_____ + (area - 45);
                            }
                            else if (64 <= area && area <= 68)//2P香
                            {
                                p2 = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKYO____ + (area - 64);
                            }
                            else if (69 <= area && area <= 73)//2P桂
                            {
                                p2 = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKEI____ + (area - 69);
                            }
                            else if (74 <= area && area <= 78)//2P銀
                            {
                                p2 = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIGIN____ + (area - 74);
                            }
                            else if (79 <= area && area <= 83)//2P金
                            {
                                p2 = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKIN____ + (area - 79);
                            }
                            else if (84 <= area && area <= 86)//2P飛
                            {
                                p2 = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIHISYA__ + (area - 84);
                            }
                            else if (87 <= area && area <= 89)//2P角
                            {
                                p2 = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKAKU___ + (area - 87);
                            }
                            else
                            {
                                throw new Exception("範囲外rowMoti=[" + rowMoti + "]");
                            }

                            int kDan = (int)(rowMoti % 9) + 1;

                            for (int col = 0; col < ConstShogi.SUJI_SIZE; col++)
                            {
                                int kSuji = 9 - (int)(col % 9);

                                int k1;
                                Conv_FvKoumoku522.Converter_K1_to_P(k_pside, kDan, kSuji, out k1);

                                if (Const_FeatureVectorFormat.PARAMETER_INDEX_INPUT)
                                {
                                    // これは、評価値の代わりにインデックスを入れています。
                                    fv.NikomaKankeiPp_ForMemory[k1, p2] = k1 * 10000 + p2;
                                }
                                else
                                {
                                    int value;
                                    int.TryParse(row[col], out value);

                                    // スコアに倍率を掛けます。
                                    float scoreF = value * fv.Bairitu_NikomaKankeiPp;
                                    fv.NikomaKankeiPp_ForMemory[k1, p2] = scoreF;
                                }

                            }

                            rowMoti++;
                        }
                        else//エラー行はスキップ。
                        {
                            //rowKp++;
                            goto gt_Next;
                        }

                        break;
                }


            gt_Next:
                ;
            }

#if DEBUG
            errH.Logger.WriteLine_AddMemo("----------------------------------------");
            errH.Logger.WriteLine_AddMemo("FV 総合点（読込後）5");
            errH.Logger.WriteLine_AddMemo("      PP =" + Util_FeatureVectorEdit.GetTotal_PP(fv));
            errH.Logger.WriteLine_AddMemo("----------------------------------------");

            //MessageBox.Show(
            //    "rowVersion=[" + rowVersion + "]\n" +
            //            "rowKk=[" + rowBanjo + "]\n"
            //, "デバッグ");
#endif
            successful = true;
        gt_EndMethod:
            return successful;
        }


        /// <summary>
        /// ファイルから読み込みます。
        /// PP 盤上型。
        /// 
        /// 該当するファイルは。
        /// fv_04_PP_1p____Fu__.csv
        /// fv_05_PP_1p____Kyo_.csv
        /// fv_06_pp_1p____Kei_.csv
        /// fv_07_pp_1p____Gin_.csv
        /// fv_08_pp_1p____Kin_.csv
        /// fv_09_pp_1p____Hi__.csv
        /// fv_10_pp_1p____Kaku.csv
        /// fv_18_pp_2p____Fu__.csv
        /// fv_19_pp_2p____Kyo_.csv
        /// fv_20_pp_2p____Kei_.csv
        /// fv_21_pp_2p____Gin_.csv
        /// fv_22_pp_2p____Kin_.csv
        /// fv_23_pp_2p____Hi__.csv
        /// fv_24_pp_2p____Kaku.csv
        /// </summary>
        /// <param name="filepath1"></param>
        /// <returns></returns>
        public static bool Make_FromFile_PP_Banjo(FeatureVector fv, string filepath1, int p1_base, KwErrorHandler errH)
        {
            bool successful = false;

            string filepath2 = Path.Combine(Application.StartupPath, filepath1);

#if DEBUG
            errH.Logger.WriteLine_AddMemo("----------------------------------------");
            errH.Logger.WriteLine_AddMemo("FV 総合点（読込前）3");
            errH.Logger.WriteLine_AddMemo("    PP =" + Util_FeatureVectorEdit.GetTotal_PP(fv));
            errH.Logger.WriteLine_AddMemo("----------------------------------------");
#endif

            if (!File.Exists(filepath2))
            {
                MessageBox.Show("Make_FromFile_PP_Banjo: ファイルがありません。\n"+
                    "filepath2=[" + filepath2 + "]\n" +
                                        "Application.StartupPath=[" + Application.StartupPath + "]"
                , "情報");
                goto gt_EndMethod;
            }

            List<List<string>> table = Util_Csv.ReadCsv(filepath2);

            int rowVersion = 0;
            int rowNestedShogiban = 0; //盤上の駒エリア
            int rowSingleShogiban = 0; //持ち駒エリア

            foreach (List<string> row in table)
            {
                if (row.Count < 1)//空行はスキップ。
                {
                    goto gt_Next;
                }

                string col0 = row[0].Trim();

                if (col0.StartsWith("#"))//コメント行はスキップ。
                {
                    goto gt_Next;
                }

                switch (col0)
                {
                    case "Version":
                        rowVersion++;
                        break;

                    default:
                        if (
                            row.Count == ConstShogi.SUJI_SIZE * ConstShogi.SUJI_SIZE // 盤状の駒エリア
                            )
                        {
                            int p2_base;//大テーブル番号ベースは、P2のベース。
                            int largeTableNo = (int)(rowNestedShogiban / 81);//大テーブル番号 0～
                            switch (largeTableNo)
                            {
                                case 0: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____FU_____; break;//1P歩
                                case 1: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KYO____; break;//1P香
                                case 2: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KEI____; break;//1P桂
                                case 3: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____GIN____; break;//1P銀
                                case 4: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KIN____; break;//1P金
                                case 5: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____HISYA__; break;//1P飛
                                case 6: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KAKU___; break;//1P角
                                case 7: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____FU_____; break;//2P歩
                                case 8: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KYO____; break;//2P香
                                case 9: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KEI____; break;//2P桂
                                case 10: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____GIN____; break;//2P銀
                                case 11: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KIN____; break;//2P金
                                case 12: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____HISYA__; break;//2P飛
                                case 13: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KAKU___; break;//2P角
                                default: throw new Exception("範囲外rowBanjo=[" + rowNestedShogiban + "]");
                            }
                            //int p2_base = largeTableNo * ConstShogi.BAN_SIZE;//小テーブル番号ベースは、P2のベース。

                            int largeDan = (int)(rowNestedShogiban % 81 / 9) + 1;
                            int smallDan = (int)(rowNestedShogiban % 9) + 1;

                            for (int col = 0; col < ConstShogi.SUJI_SIZE * ConstShogi.SUJI_SIZE; col++)
                            {
                                int largeSuji = 9 - (int)(col / 9);
                                int smallSuji = 9 - (int)(col % 9);




                                // 大テーブル升と、小テーブル升。
                                int p1 = p1_base + Util_Masu10.Handle_OkibaSujiDanToMasu(Okiba.ShogiBan, largeSuji, largeDan);
                                int p2 = p2_base + Util_Masu10.Handle_OkibaSujiDanToMasu(Okiba.ShogiBan, smallSuji, smallDan);

                                if (Const_FeatureVectorFormat.PARAMETER_INDEX_INPUT)
                                {
                                    // これは、評価値の代わりにインデックスを入れています。
                                    fv.NikomaKankeiPp_ForMemory[p1, p2] = p1 * 10000 + p2;
                                }
                                else
                                {
                                    int value;
                                    int.TryParse(row[col], out value);

                                    // スコアに倍率を掛けます。
                                    float scoreF = value * fv.Bairitu_NikomaKankeiPp;
                                    fv.NikomaKankeiPp_ForMemory[p1, p2] = scoreF; //  1Pの歩のみに影響??
                                }

                            }

                            rowNestedShogiban++;
                        }
                        else if (
                            row.Count == ConstShogi.SUJI_SIZE // 持ち駒エリア
                            )
                        {
                            int p2;//大テーブル項目。※大テーブルがＰ２、小テーブルがＰ１と、ひっくり返している。
                            int largeTableNo = (int)(rowSingleShogiban / 9);//大テーブル番号
                            if (0 <= largeTableNo && largeTableNo <= 18)//1P歩
                            {
                                p2 = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIFU_____ + largeTableNo;
                            }
                            else if (19 <= largeTableNo && largeTableNo <= 23)//1P香
                            {
                                p2 = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKYO____ + (largeTableNo - 19);
                            }
                            else if (24 <= largeTableNo && largeTableNo <= 28)//1P桂
                            {
                                p2 = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKEI____ + (largeTableNo - 24);
                            }
                            else if (29 <= largeTableNo && largeTableNo <= 33)//1P銀
                            {
                                p2 = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIGIN____ + (largeTableNo - 29);
                            }
                            else if (34 <= largeTableNo && largeTableNo <= 38)//1P金
                            {
                                p2 = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKIN____ + (largeTableNo - 34);
                            }
                            else if (39 <= largeTableNo && largeTableNo <= 41)//1P飛
                            {
                                p2 = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIHISYA__ + (largeTableNo - 39);
                            }
                            else if (42 <= largeTableNo && largeTableNo <= 44)//1P角
                            {
                                p2 = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKAKU___ + (largeTableNo - 42);
                            }
                            else if (45 <= largeTableNo && largeTableNo <= 63)//2P歩
                            {
                                p2 = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIFU_____ + (largeTableNo - 45);
                            }
                            else if (64 <= largeTableNo && largeTableNo <= 68)//2P香
                            {
                                p2 = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKYO____ + (largeTableNo - 64);
                            }
                            else if (69 <= largeTableNo && largeTableNo <= 73)//2P桂
                            {
                                p2 = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKEI____ + (largeTableNo - 69);
                            }
                            else if (74 <= largeTableNo && largeTableNo <= 78)//2P銀
                            {
                                p2 = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIGIN____ + (largeTableNo - 74);
                            }
                            else if (79 <= largeTableNo && largeTableNo <= 83)//2P金
                            {
                                p2 = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKIN____ + (largeTableNo - 79);
                            }
                            else if (84 <= largeTableNo && largeTableNo <= 86)//2P飛
                            {
                                p2 = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIHISYA__ + (largeTableNo - 84);
                            }
                            else if (87 <= largeTableNo && largeTableNo <= 89)//2P角
                            {
                                p2 = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKAKU___ + (largeTableNo - 87);
                            }
                            else
                            {
                                throw new Exception("範囲外rowMoti=[" + rowSingleShogiban + "]");
                            }

                            // 小テーブルは、P1項目。
                            int smallDan = (int)(rowSingleShogiban % 9) + 1;

                            for (int col = 0; col < ConstShogi.SUJI_SIZE; col++)
                            {
                                int smallSuji = 9 - (int)(col % 9);

                                //　小テーブル升は、P1項目。
                                //p1_base + 
                                int p1 = Util_Masu10.Handle_OkibaSujiDanToMasu(Okiba.ShogiBan, smallSuji, smallDan);

                                if (Const_FeatureVectorFormat.PARAMETER_INDEX_INPUT)
                                {
                                    // これは、評価値の代わりにインデックスを入れています。
                                    fv.NikomaKankeiPp_ForMemory[p1, p2] = p1 * 10000 + p2;
                                }
                                else
                                {
                                    int value;
                                    int.TryParse(row[col], out value);

                                    // スコアに倍率を掛けます。
                                    float scoreF = value * fv.Bairitu_NikomaKankeiPp;
                                    fv.NikomaKankeiPp_ForMemory[p1, p2] = scoreF;//盤上の駒 vs 持ち駒
                                }

                            }

                            rowSingleShogiban++;
                        }
                        else//エラー行はスキップ。
                        {
                            //rowKp++;
                            goto gt_Next;
                        }

                        break;
                }


            gt_Next:
                ;
            }

#if DEBUG
            errH.Logger.WriteLine_AddMemo("----------------------------------------");
            errH.Logger.WriteLine_AddMemo("FV 総合点（読込後）1");
            errH.Logger.WriteLine_AddMemo("      PP =" + Util_FeatureVectorEdit.GetTotal_PP(fv));
            errH.Logger.WriteLine_AddMemo("----------------------------------------");

            //MessageBox.Show(
            //    "rowVersion=[" + rowVersion + "]\n" +
            //            "rowKk=[" + rowNestedShogiban + "]\n"
            //, "デバッグ");
#endif
            successful = true;
        gt_EndMethod:
            return successful;
        }


        /// <summary>
        /// ファイルから読み込みます。
        /// PP 持ち駒19枚型。
        /// 
        /// 該当するファイルは。
        /// fv_11_PP_1pMotiFu__.csv
        /// fv_25_pp_2pMotiFu__.csv
        /// </summary>
        /// <param name="fv"></param>
        /// <param name="filepath1"></param>
        /// <returns></returns>
        public static bool Make_FromFile_PP_Moti19Mai(FeatureVector fv, string filepath1, int p1_base, KwErrorHandler errH)
        {
            bool successful = false;
            string filepath2 = Path.Combine(Application.StartupPath, filepath1);

            int p1Koumoku19Length = 19;


#if DEBUG
            errH.Logger.WriteLine_AddMemo("----------------------------------------");
            errH.Logger.WriteLine_AddMemo("FV 総合点（読込前）4");
            errH.Logger.WriteLine_AddMemo("    PP =" + Util_FeatureVectorEdit.GetTotal_PP(fv));
            errH.Logger.WriteLine_AddMemo("----------------------------------------");
#endif

            if (!File.Exists(filepath2))
            {
                MessageBox.Show("Make_FromFile_PP_Moti19Mai: ファイルがありません。\n"+
                    "filepath2=[" + filepath2 + "]\n" +
                                        "Application.StartupPath=[" + Application.StartupPath + "]"
                , "情報");
                goto gt_EndMethod;
            }

            List<List<string>> table = Util_Csv.ReadCsv(filepath2);

            int rowVersion = 0;
            int row_banjoArea = 0; //盤上の駒エリア
            int row_motiArea = 0; //持ち駒エリア

            foreach (List<string> row in table)
            {
                if (row.Count < 1)//空行はスキップ。
                {
                    goto gt_Next;
                }

                string col0 = row[0].Trim();

                if (col0.StartsWith("#"))//コメント行はスキップ。
                {
                    goto gt_Next;
                }

                switch (col0)
                {
                    case "Version":
                        rowVersion++;
                        break;

                    default:
                        if(
                            row.Count == ConstShogi.SUJI_SIZE * 10 // 将棋盤が１０枚並んでいるエリア
                            || row.Count == ConstShogi.SUJI_SIZE * 9 // 将棋盤が９枚並んでいるエリア
                            )
                        {
                            int p2_base;
                            int row_LargeShogiban = (int)(row_banjoArea / ConstShogi.DAN_SIZE);//将棋盤が縦に何個目か。
                            switch (row_LargeShogiban)
                            {
                                case 0:
                                case 1: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____FU_____; break;//1P歩
                                case 2:
                                case 3: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KYO____; break;//1P香
                                case 4:
                                case 5: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KEI____; break;//1P桂
                                case 6:
                                case 7: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____GIN____; break;//1P銀
                                case 8:
                                case 9: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KIN____; break;//1P金
                                case 10:
                                case 11: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____HISYA__; break;//1P飛
                                case 12:
                                case 13: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KAKU___; break;//1P角
                                case 14:
                                case 15: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____FU_____; break;//2P歩
                                case 16:
                                case 17: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KYO____; break;//2P香
                                case 18:
                                case 19: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KEI____; break;//2P桂
                                case 20:
                                case 21: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____GIN____; break;//2P銀
                                case 22:
                                case 23: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KIN____; break;//2P金
                                case 24:
                                case 25: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____HISYA__; break;//2P飛
                                case 26:
                                case 27: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KAKU___; break;//2P角
                                default: throw new Exception("範囲外rowBanjo=[" + row_banjoArea + "]");
                            }
                            int largeDan = row_LargeShogiban % 2 + 1;//1 or 2
                            int smallDan = (int)(row_banjoArea % 9) + 1;//1～9

                            for (int col = 0; col < row.Count; col++)//0～80 or 0～89
                            {
                                int largeCol = (int)(col / ConstShogi.SUJI_SIZE);//0～8 or 0～9
                                int smallSuji = 9 - (int)(col % 9);//9～1

                                // P1 は、将棋盤上の升ではなく、歩の枚数。
                                int largeFuMaisu = (largeDan - 1) * 10 + largeCol;//上段１０列、下段９列。0～18。

                                int p1 = p1_base + largeFuMaisu;
                                int p2 = p2_base + Util_Masu10.Handle_OkibaSujiDanToMasu(Okiba.ShogiBan, smallSuji, smallDan);
                                if (Const_FeatureVectorFormat.PARAMETER_INDEX_INPUT)
                                {
                                    // これは、評価値の代わりにインデックスを入れています。
                                    fv.NikomaKankeiPp_ForMemory[p1, p2] = p1 * 10000 + p2;
                                }
                                else
                                {
                                    int value;
                                    int.TryParse(row[col], out value);

                                    // スコアに倍率を掛けます。
                                    float scoreF = value * fv.Bairitu_NikomaKankeiPp;
                                    fv.NikomaKankeiPp_ForMemory[p1, p2] = scoreF;//持ち歩 vs 盤上の駒
                                }

                            }

                            row_banjoArea++;
                        }
                        else if (
                            row.Count == 19 // 持ち駒エリア
                            || row.Count == 5
                            || row.Count == 3
                            )
                        {
                            int p2_base;
                            int p2MaisuLength;
                            int largeTableNo = (int)(row_motiArea / p1Koumoku19Length);//縦に並んでいる大テーブル番号。0～14。
                            switch (largeTableNo)
                            {
                                case 0://1P歩 0～18枚
                                    p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIFU_____;
                                    p2MaisuLength = 19;
                                    break;
                                case 1://1P香 0～5枚
                                    p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKYO____;
                                    p2MaisuLength = 5;
                                    break;
                                case 2://1P桂 0～5枚
                                    p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKEI____;
                                    p2MaisuLength = 5;
                                    break;
                                case 3://1P銀 0～5枚
                                    p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIGIN____;
                                    p2MaisuLength = 5;
                                    break;
                                case 4://1P金 0～5枚
                                    p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKIN____;
                                    p2MaisuLength = 5;
                                    break;
                                case 5://1P飛 0～3枚
                                    p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIHISYA__;
                                    p2MaisuLength = 3;
                                    break;
                                case 6://1P角 0～3枚
                                    p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKAKU___;
                                    p2MaisuLength = 3;
                                    break;
                                case 7://2P歩 0～18枚
                                    p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIFU_____;
                                    p2MaisuLength = 19;
                                    break;
                                case 8://2P香 0～5枚
                                    p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKYO____;
                                    p2MaisuLength = 5;
                                    break;
                                case 9://2P桂 0～5枚
                                    p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKEI____;
                                    p2MaisuLength = 5;
                                    break;
                                case 10://2P銀 0～5枚
                                    p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIGIN____;
                                    p2MaisuLength = 5;
                                    break;
                                case 11://2P金 0～5枚
                                    p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKIN____;
                                    p2MaisuLength = 5;
                                    break;
                                case 12://2P飛 0～3枚
                                    p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIHISYA__;
                                    p2MaisuLength = 3;
                                    break;
                                case 13://2P角 0～3枚
                                    p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKAKU___;
                                    p2MaisuLength = 3;
                                    break;
                                default: throw new Exception("範囲外rowMoti=[" + row_motiArea + "]");
                            }

                            int smallDan = (int)(row_motiArea % 9) + 1;

                            for (int maisuRow = 0; maisuRow < p1Koumoku19Length; maisuRow++)//行＝大枚数
                            {
                                for (int maisuCol = 0; maisuCol < p2MaisuLength; maisuCol++)//列＝小枚数
                                {
                                    int p1 = p1_base + maisuRow;
                                    int p2 = p2_base + maisuCol;
                                    if (Const_FeatureVectorFormat.PARAMETER_INDEX_INPUT)
                                    {
                                        // これは評価値の代わりに インデックス を入れています。
                                        fv.NikomaKankeiPp_ForMemory[p1, p2] = p1 * 10000 + p2;
                                    }
                                    else
                                    {
                                        int value;
                                        int.TryParse(row[maisuCol], out value);

                                        // スコアに倍率を掛けます。
                                        float scoreF = value * fv.Bairitu_NikomaKankeiPp;
                                        fv.NikomaKankeiPp_ForMemory[p1, p2] = scoreF;
                                    }

                                }
                            }


                            row_motiArea++;
                        }
                        else//エラー行はスキップ。
                        {
                            goto gt_Next;
                        }

                        break;
                }


            gt_Next:
                ;
            }

#if DEBUG
            errH.Logger.WriteLine_AddMemo("----------------------------------------");
            errH.Logger.WriteLine_AddMemo("FV 総合点（読込後）2");
            errH.Logger.WriteLine_AddMemo("      PP =" + Util_FeatureVectorEdit.GetTotal_PP(fv));
            errH.Logger.WriteLine_AddMemo("----------------------------------------");

            //MessageBox.Show(
            //    "rowVersion=[" + rowVersion + "]\n" 
            //, "デバッグ");
#endif
            successful = true;
        gt_EndMethod:
            return successful;
        }


        /// <summary>
        /// ファイルから読み込みます。
        /// PP 持ち駒3（飛など） or 5（香など）枚型。
        /// </summary>
        /// <param name="fv"></param>
        /// <param name="filepath1"></param>
        /// <returns></returns>
        public static bool Make_FromFile_PP_Moti3or5Mai(FeatureVector fv, string filepath1, int p1_base, int p1Koumoku3or5Length, KwErrorHandler errH)
        {

            bool successful = false;

            string filepath2 = Path.Combine(Application.StartupPath, filepath1);

#if DEBUG
            errH.Logger.WriteLine_AddMemo("----------------------------------------");
            errH.Logger.WriteLine_AddMemo("FV 総合点（読込前）5");
            errH.Logger.WriteLine_AddMemo("    PP =" + Util_FeatureVectorEdit.GetTotal_PP(fv));
            errH.Logger.WriteLine_AddMemo("----------------------------------------");
#endif

            if (!File.Exists(filepath2))
            {
                MessageBox.Show("Make_FromFile_PP_Moti3or5Mai: ファイルがありません。\n"+
                    "filepath2=[" + filepath2 + "]\n" +
                                        "Application.StartupPath=[" + Application.StartupPath + "]"
                , "情報");
                goto gt_EndMethod;
            }

            List<List<string>> table = Util_Csv.ReadCsv(filepath2);

            int rowVersion = 0;
            int row_banjoArea = 0; //盤上の駒エリア
            int row_motiArea = 0; //持ち駒エリア

            foreach (List<string> row in table)
            {
                if (row.Count < 1)//空行はスキップ。
                {
                    goto gt_Next;
                }

                string col0 = row[0].Trim();

                if (col0.StartsWith("#"))//コメント行はスキップ。
                {
                    goto gt_Next;
                }

                switch (col0)
                {
                    case "Version":
                        rowVersion++;
                        break;

                    default:
                        if (
                            row.Count == ConstShogi.SUJI_SIZE * p1Koumoku3or5Length // 将棋盤が３or５枚並んでいるエリア
                            )
                        {
                            int p2_base;
                            int row_largeShogiban = (int)(row_banjoArea / ConstShogi.DAN_SIZE);//将棋盤が縦に何個目か。0～13。
                            switch (row_largeShogiban)
                            {
                                case 0: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____FU_____; break;//1P歩
                                case 1: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KYO____; break;//1P香
                                case 2: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KEI____; break;//1P桂
                                case 3: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____GIN____; break;//1P銀
                                case 4: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KIN____; break;//1P金
                                case 5: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____HISYA__; break;//1P飛
                                case 6: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_____KAKU___; break;//1P角
                                case 7: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____FU_____; break;//2P歩
                                case 8: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KYO____; break;//2P香
                                case 9: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KEI____; break;//2P桂
                                case 10: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____GIN____; break;//2P銀
                                case 11: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KIN____; break;//2P金
                                case 12: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____HISYA__; break;//2P飛
                                case 13: p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_____KAKU___; break;//2P角
                                default: throw new Exception("範囲外rowBanjo=[" + row_banjoArea + "]");
                            }
                            //int largeMaisu = (int)(row_banjoArea % p1Koumoku3or5Length);//大グループの枚数。０枚～。
                            //int smallDan = largeMaisu + 1;//小テーブルの段。１スタート。
                            int smallDan = (int)(row_banjoArea % 9) + 1;//1～9

                            for (int col = 0; col < row.Count; col++)
                            {
                                // P1 は、将棋盤上の升ではなく、持駒の枚数。＝大テーブル列番号。
                                int largeCol_motiMaisu = (int)(col / ConstShogi.SUJI_SIZE);//0～4 or 0～2

                                //int largeSuji = 9 - (int)(col / 9);
                                int smallSuji = 9 - (int)(col % 9);

                                int p1 = p1_base + largeCol_motiMaisu;
                                int p2 = p2_base + Util_Masu10.Handle_OkibaSujiDanToMasu(Okiba.ShogiBan, smallSuji, smallDan);
                                if (Const_FeatureVectorFormat.PARAMETER_INDEX_INPUT)
                                {
                                    // これは、評価値の代わりにインデックスを入れています。
                                    fv.NikomaKankeiPp_ForMemory[p1, p2] = p1 * 10000 + p2;
                                }
                                else
                                {
                                    int value;
                                    int.TryParse(row[col], out value);

                                    // スコアに倍率を掛けます。
                                    float scoreF = value * fv.Bairitu_NikomaKankeiPp;
                                    fv.NikomaKankeiPp_ForMemory[p1, p2] = scoreF;
                                }

                            }

                            row_banjoArea++;
                        }
                        else if (
                            row.Count == 19 // 持ち駒エリア
                            || row.Count == 5
                            || row.Count == 3
                            )
                        {
                            int p2_base;
                            int p2MaisuLength;
                            int largeTableNo = (int)(row_motiArea / p1Koumoku3or5Length);//大グループ番号
                            switch (largeTableNo)
                            {
                                case 0://1P歩 0～18枚
                                    p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIFU_____;// + largeTableNo;
                                    p2MaisuLength = 19;
                                    break;
                                case 1://1P香 0～5枚
                                    p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKYO____;// + (largeTableNo - 19);
                                    p2MaisuLength = 5;
                                    break;
                                case 2://1P桂 0～5枚
                                    p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKEI____;// + (largeTableNo - 24);
                                    p2MaisuLength = 5;
                                    break;
                                case 3://1P銀 0～5枚
                                    p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIGIN____;// + (largeTableNo - 29);
                                    p2MaisuLength = 5;
                                    break;
                                case 4://1P金 0～5枚
                                    p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKIN____;// + (largeTableNo - 34);
                                    p2MaisuLength = 5;
                                    break;
                                case 5://1P飛 0～3枚
                                    p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIHISYA__;// + (largeTableNo - 39);
                                    p2MaisuLength = 3;
                                    break;
                                case 6://1P角 0～3枚
                                    p2_base = FeatureVectorImpl.CHOSA_KOMOKU_1P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKAKU___;// + (largeTableNo - 42);
                                    p2MaisuLength = 3;
                                    break;
                                case 7://2P歩 0～18枚
                                    p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIFU_____;// + (largeTableNo - 45);
                                    p2MaisuLength = 19;
                                    break;
                                case 8://2P香 0～5枚
                                    p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKYO____;// + (largeTableNo - 64);
                                    p2MaisuLength = 5;
                                    break;
                                case 9://2P桂 0～5枚
                                    p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKEI____;// + (largeTableNo - 69);
                                    p2MaisuLength = 5;
                                    break;
                                case 10://2P銀 0～5枚
                                    p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIGIN____;// + (largeTableNo - 74);
                                    p2MaisuLength = 5;
                                    break;
                                case 11://2P金 0～5枚
                                    p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKIN____;// + (largeTableNo - 79);
                                    p2MaisuLength = 5;
                                    break;
                                case 12://2P飛 0～3枚
                                    p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIHISYA__;// + (largeTableNo - 84);
                                    p2MaisuLength = 3;
                                    break;
                                case 13://2P角 0～3枚
                                    p2_base = FeatureVectorImpl.CHOSA_KOMOKU_2P + FeatureVectorImpl.CHOSA_KOMOKU_MOTIKAKU___;// + (largeTableNo - 87);
                                    p2MaisuLength = 3;
                                    break;
                                default:
                                    throw new Exception("範囲外rowMoti=[" + row_motiArea + "]");
                                    break;
                            }

                            int smallDan = (int)(row_motiArea % 9) + 1;

                            for (int maisuRow = 0; maisuRow < p1Koumoku3or5Length; maisuRow++)//行＝大枚数
                            {
                                for (int maisuCol = 0; maisuCol < p2MaisuLength; maisuCol++)//列＝小枚数
                                {
                                    int p1 = p1_base + maisuRow;
                                    int p2 = p2_base + maisuCol;
                                    if (Const_FeatureVectorFormat.PARAMETER_INDEX_INPUT)
                                    {
                                        // これは、評価値の代わりにインデックスを入れています。
                                        fv.NikomaKankeiPp_ForMemory[p1, p2] = p1 * 10000 + p2;
                                    }
                                    else
                                    {
                                        int value;
                                        int.TryParse(row[maisuCol], out value);

                                        // スコアに倍率を掛けます。
                                        float scoreF = value * fv.Bairitu_NikomaKankeiPp;
                                        fv.NikomaKankeiPp_ForMemory[p1, p2] = scoreF;
                                    }

                                }
                            }


                            row_motiArea++;
                        }
                        else//エラー行はスキップ。
                        {
                            goto gt_Next;
                        }

                        break;
                }

            gt_Next:
                ;
            }

#if DEBUG
            errH.Logger.WriteLine_AddMemo("----------------------------------------");
            errH.Logger.WriteLine_AddMemo("FV 総合点（読込後）3");
            errH.Logger.WriteLine_AddMemo("      PP =" + Util_FeatureVectorEdit.GetTotal_PP(fv));
            errH.Logger.WriteLine_AddMemo("----------------------------------------");

            //MessageBox.Show(
            //    "rowVersion=[" + rowVersion + "]\n" 
            //, "デバッグ");
#endif
            successful = true;
        gt_EndMethod:
            return successful;
        }


    }
}
