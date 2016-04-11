using Grayscale.P145_SfenStruct_.L___250_Struct;
using Grayscale.P146_ConvSfen___.L500____Converter;
using Grayscale.P211_WordShogi__.L250____Masu;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P213_Komasyurui_.L250____Word;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P238_Seiza______.L250____Struct;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //フィンガー番号

namespace Grayscale.P276_SeizaStartp.L500____Struct
{
    public class StartposImporter
    {

        private string InputLine;

        /// <summary>
        /// 盤上の駒。
        /// key:升番号
        /// </summary>
        private Dictionary<int, RO_Star> masubetuKoma_banjo;

        public RO_Kyokumen2_ForTokenize RO_SfenStartpos { get; set; }


        public static bool TryParse(
            string inputLine,
            out StartposImporter instance,
            out string rest
            )
        {
            bool successful = true;

            RO_Kyokumen2_ForTokenize ro_SfenStartpos;
            if (!Conv_Sfenstring146.ToKyokumen2(inputLine, out rest, out ro_SfenStartpos))
            {
                successful = false;
                instance = null;
                goto gt_EndMethod;
            }

            instance = new StartposImporter(inputLine, ro_SfenStartpos);

        gt_EndMethod:
            return successful;
        }

        private StartposImporter(
            string inputLine,
            RO_Kyokumen2_ForTokenize ro_SfenStartpos
            )
        {
            this.InputLine = inputLine;

            this.RO_SfenStartpos = ro_SfenStartpos;

            this.StringToObject();
        }

        private void StringToObject()
        {
            this.masubetuKoma_banjo = new Dictionary<int, RO_Star>();

            // 将棋の駒４０個の場所を確認します。

            this.RO_SfenStartpos.Foreach_Masu201((int masuHandle, string masuString, ref bool toBreak) =>
            {
                switch (masuString)
                {
                    case "P": this.masubetuKoma_banjo.Add(masuHandle, new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(masuHandle), Komasyurui14.H01_Fu_____)); break;
                    case "L": this.masubetuKoma_banjo.Add(masuHandle, new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(masuHandle), Komasyurui14.H02_Kyo____)); break;
                    case "N": this.masubetuKoma_banjo.Add(masuHandle, new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(masuHandle), Komasyurui14.H03_Kei____)); break;
                    case "S": this.masubetuKoma_banjo.Add(masuHandle, new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(masuHandle), Komasyurui14.H04_Gin____)); break;
                    case "G": this.masubetuKoma_banjo.Add(masuHandle, new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(masuHandle), Komasyurui14.H05_Kin____)); break;
                    case "K": this.masubetuKoma_banjo.Add(masuHandle, new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(masuHandle), Komasyurui14.H06_Gyoku__)); break;
                    case "R": this.masubetuKoma_banjo.Add(masuHandle, new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(masuHandle), Komasyurui14.H07_Hisya__)); break;
                    case "B": this.masubetuKoma_banjo.Add(masuHandle, new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(masuHandle), Komasyurui14.H08_Kaku___)); break;
                    case "+P": this.masubetuKoma_banjo.Add(masuHandle, new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(masuHandle), Komasyurui14.H11_Tokin__)); break;
                    case "+L": this.masubetuKoma_banjo.Add(masuHandle, new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(masuHandle), Komasyurui14.H12_NariKyo)); break;
                    case "+N": this.masubetuKoma_banjo.Add(masuHandle, new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(masuHandle), Komasyurui14.H13_NariKei)); break;
                    case "+S": this.masubetuKoma_banjo.Add(masuHandle, new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(masuHandle), Komasyurui14.H14_NariGin)); break;
                    case "+R": this.masubetuKoma_banjo.Add(masuHandle, new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(masuHandle), Komasyurui14.H09_Ryu____)); break;
                    case "+B": this.masubetuKoma_banjo.Add(masuHandle, new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(masuHandle), Komasyurui14.H10_Uma____)); break;

                    case "p": this.masubetuKoma_banjo.Add(masuHandle, new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(masuHandle), Komasyurui14.H01_Fu_____)); break;
                    case "l": this.masubetuKoma_banjo.Add(masuHandle, new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(masuHandle), Komasyurui14.H02_Kyo____)); break;
                    case "n": this.masubetuKoma_banjo.Add(masuHandle, new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(masuHandle), Komasyurui14.H03_Kei____)); break;
                    case "s": this.masubetuKoma_banjo.Add(masuHandle, new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(masuHandle), Komasyurui14.H04_Gin____)); break;
                    case "g": this.masubetuKoma_banjo.Add(masuHandle, new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(masuHandle), Komasyurui14.H05_Kin____)); break;
                    case "k": this.masubetuKoma_banjo.Add(masuHandle, new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(masuHandle), Komasyurui14.H06_Gyoku__)); break;
                    case "r": this.masubetuKoma_banjo.Add(masuHandle, new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(masuHandle), Komasyurui14.H07_Hisya__)); break;
                    case "b": this.masubetuKoma_banjo.Add(masuHandle, new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(masuHandle), Komasyurui14.H08_Kaku___)); break;
                    case "+p": this.masubetuKoma_banjo.Add(masuHandle, new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(masuHandle), Komasyurui14.H11_Tokin__)); break;
                    case "+l": this.masubetuKoma_banjo.Add(masuHandle, new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(masuHandle), Komasyurui14.H12_NariKyo)); break;
                    case "+n": this.masubetuKoma_banjo.Add(masuHandle, new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(masuHandle), Komasyurui14.H13_NariKei)); break;
                    case "+s": this.masubetuKoma_banjo.Add(masuHandle, new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(masuHandle), Komasyurui14.H14_NariGin)); break;
                    case "+r": this.masubetuKoma_banjo.Add(masuHandle, new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(masuHandle), Komasyurui14.H09_Ryu____)); break;
                    case "+b": this.masubetuKoma_banjo.Add(masuHandle, new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(masuHandle), Komasyurui14.H10_Uma____)); break;

                    case "":
                        // 空っぽの升。
                        break;

                    default:
                        throw new Exception("未対応のmoji=[" + masuString + "]");
                }
            });

            Debug.Assert(this.masubetuKoma_banjo.Count == 40, "将棋の駒の数が40個ではありませんでした。[" + this.masubetuKoma_banjo.Count + "]");
        }


        public static void Assert_HirateHonsyogi(SkyBuffer dst_Sky, string failMessage)
        {
            //平手本将棋用
//#if DEBUG
//            {
//                StringBuilder sb = new StringBuilder();
//                int fukuro = 0;
//                int ban = 0;
//                int dai = 0;
//                dst_Sky.Foreach_Starlights((Finger finger, Starlight light, ref bool toBreak) =>
//                {
//                    RO_Star_Koma koma = Util_Starlightable.AsKoma(light.Now);

//                    if (Util_MasuNum.OnKomabukuro(Util_Masu.AsMasuNumber(koma.Masu)))
//                    {
//                        sb.Append("[袋" + Util_Masu.AsMasuNumber(koma.Masu) + "]");
//                        fukuro++;
//                    }
//                    else if (Util_MasuNum.OnShogiban(Util_Masu.AsMasuNumber(koma.Masu)))
//                    {
//                        sb.Append("[盤" + Util_Masu.AsMasuNumber(koma.Masu) + "]");
//                        ban++;
//                    }
//                    else if (Util_MasuNum.OnKomadai(Util_Masu.AsMasuNumber(koma.Masu)))
//                    {
//                        sb.Append("[台" + Util_Masu.AsMasuNumber(koma.Masu) + "]");
//                        dai++;
//                    }
//                });
//                Debug.Assert(40 == ban + dai || 40 == fukuro, "駒袋に駒が！fukuro=[" + fukuro + "] ban=[" + ban + "] dai=[" + dai + "] " + failMessage + " "+sb.ToString());
//            }
//#endif
        }

        public SkyConst ToSky(Playerside kaisiPside, int temezumi)
        {

            // 駒40個に、Finger番号を割り当てておきます。
            SkyBuffer dst_Sky = new SkyBuffer(kaisiPside, temezumi);// 駒数０。
            //SkyBuffer dst_Sky = new SkyBuffer(Util_Sky.New_Komabukuro());// SFENインポート時


            Dictionary<Finger, RO_Star> komaDic = new Dictionary<Finger, RO_Star>();


            // ・インクリメントするので、Finger型ではなく int型で。
            // ・駒を取ったときに、先手後手は浮動するので区別できない。
            // 王 0～1
            int int_fingerK1 = 0;
            int int_fingerK2 = 1;
            // 飛車 2～3
            int int_fingerR1 = 2;
            int int_fingerR2 = 3;
            // 角 4～5
            int int_fingerB1 = 4;
            int int_fingerB2 = 5;
            // 金 6～9
            int int_fingerG1 = 6;
            int int_fingerG2 = 8;
            // 銀 10～13
            int int_fingerS1 = 10;
            int int_fingerS2 = 12;
            // 桂 14～17
            int int_fingerN1 = 14;
            int int_fingerN2 = 16;
            // 香 18～21
            int int_fingerL1 = 18;
            int int_fingerL2 = 20;
            // 歩 22～30,31～39
            int int_fingerP1 = 22;
            int int_fingerP2 = 31;

            //
            // どの升に、どの駒がいるか
            //
            foreach (KeyValuePair<int, RO_Star> entry in this.masubetuKoma_banjo)
            {
                int int_finger;

                // 今回のカウント
                switch (entry.Value.Komasyurui)
                {
                    case Komasyurui14.H01_Fu_____:
                        switch (entry.Value.Pside)
                        {
                            case Playerside.P1: int_finger = int_fingerP1; break;
                            case Playerside.P2: int_finger = int_fingerP2; break;
                            default: throw new Exception("未対応のプレイヤー番号");
                        }
                        break;

                    case Komasyurui14.H02_Kyo____:
                        switch (entry.Value.Pside)
                        {
                            case Playerside.P1: int_finger = int_fingerL1; break;
                            case Playerside.P2: int_finger = int_fingerL2; break;
                            default: throw new Exception("未対応のプレイヤー番号");
                        }
                        break;

                    case Komasyurui14.H03_Kei____:
                        switch (entry.Value.Pside)
                        {
                            case Playerside.P1: int_finger = int_fingerN1; break;
                            case Playerside.P2: int_finger = int_fingerN2; break;
                            default: throw new Exception("未対応のプレイヤー番号");
                        }
                        break;

                    case Komasyurui14.H04_Gin____:
                        switch (entry.Value.Pside)
                        {
                            case Playerside.P1: int_finger = int_fingerS1; break;
                            case Playerside.P2: int_finger = int_fingerS2; break;
                            default: throw new Exception("未対応のプレイヤー番号");
                        }
                        break;

                    case Komasyurui14.H05_Kin____:
                        switch (entry.Value.Pside)
                        {
                            case Playerside.P1: int_finger = int_fingerG1; break;
                            case Playerside.P2: int_finger = int_fingerG2; break;
                            default: throw new Exception("未対応のプレイヤー番号");
                        }
                        break;

                    case Komasyurui14.H06_Gyoku__:
                        switch (entry.Value.Pside)
                        {
                            case Playerside.P1: int_finger = int_fingerK1; break;
                            case Playerside.P2: int_finger = int_fingerK2; break;
                            default: throw new Exception("未対応のプレイヤー番号");
                        }
                        break;

                    case Komasyurui14.H07_Hisya__:
                        switch (entry.Value.Pside)
                        {
                            case Playerside.P1: int_finger = int_fingerR1; break;
                            case Playerside.P2: int_finger = int_fingerR2; break;
                            default: throw new Exception("未対応のプレイヤー番号");
                        }
                        break;

                    case Komasyurui14.H08_Kaku___:
                        switch (entry.Value.Pside)
                        {
                            case Playerside.P1: int_finger = int_fingerB1; break;
                            case Playerside.P2: int_finger = int_fingerB2; break;
                            default: throw new Exception("未対応のプレイヤー番号");
                        }
                        break;

                    case Komasyurui14.H09_Ryu____:
                        switch (entry.Value.Pside)
                        {
                            case Playerside.P1: int_finger = int_fingerR1; break;
                            case Playerside.P2: int_finger = int_fingerR2; break;
                            default: throw new Exception("未対応のプレイヤー番号");
                        }
                        break;

                    case Komasyurui14.H10_Uma____:
                        switch (entry.Value.Pside)
                        {
                            case Playerside.P1: int_finger = int_fingerB1; break;
                            case Playerside.P2: int_finger = int_fingerB2; break;
                            default: throw new Exception("未対応のプレイヤー番号");
                        }
                        break;

                    case Komasyurui14.H11_Tokin__:
                        switch (entry.Value.Pside)
                        {
                            case Playerside.P1: int_finger = int_fingerP1; break;
                            case Playerside.P2: int_finger = int_fingerP2; break;
                            default: throw new Exception("未対応のプレイヤー番号");
                        }
                        break;

                    case Komasyurui14.H12_NariKyo:
                        switch (entry.Value.Pside)
                        {
                            case Playerside.P1: int_finger = int_fingerL1; break;
                            case Playerside.P2: int_finger = int_fingerL2; break;
                            default: throw new Exception("未対応のプレイヤー番号");
                        }
                        break;

                    case Komasyurui14.H13_NariKei:
                        switch (entry.Value.Pside)
                        {
                            case Playerside.P1: int_finger = int_fingerN1; break;
                            case Playerside.P2: int_finger = int_fingerN2; break;
                            default: throw new Exception("未対応のプレイヤー番号");
                        }
                        break;

                    case Komasyurui14.H14_NariGin:
                        switch (entry.Value.Pside)
                        {
                            case Playerside.P1: int_finger = int_fingerS1; break;
                            case Playerside.P2: int_finger = int_fingerS2; break;
                            default: throw new Exception("未対応のプレイヤー番号");
                        }
                        break;

                    default: throw new Exception("未対応の駒種類=[" + entry.Value.Komasyurui + "]");
                }

                Debug.Assert(0<=int_finger && int_finger<=39, "finger=["+int_finger+"]" );

                Debug.Assert(!komaDic.ContainsKey( int_finger), "finger=[" + int_finger + "]");

                komaDic.Add(int_finger, entry.Value);


                // カウントアップ
                switch (entry.Value.Komasyurui)
                {
                    case Komasyurui14.H01_Fu_____:
                        switch (entry.Value.Pside)
                        {
                            case Playerside.P1: int_fingerP1++; break;
                            case Playerside.P2: int_fingerP2++; break;
                            default: throw new Exception("未対応のプレイヤー番号");
                        }
                        break;

                    case Komasyurui14.H02_Kyo____:
                        switch (entry.Value.Pside)
                        {
                            case Playerside.P1: int_fingerL1++; break;
                            case Playerside.P2: int_fingerL2++; break;
                            default: throw new Exception("未対応のプレイヤー番号");
                        }
                        break;

                    case Komasyurui14.H03_Kei____:
                        switch (entry.Value.Pside)
                        {
                            case Playerside.P1: int_fingerN1++; break;
                            case Playerside.P2: int_fingerN2++; break;
                            default: throw new Exception("未対応のプレイヤー番号");
                        }
                        break;

                    case Komasyurui14.H04_Gin____:
                        switch (entry.Value.Pside)
                        {
                            case Playerside.P1: int_fingerS1++; break;
                            case Playerside.P2: int_fingerS2++; break;
                            default: throw new Exception("未対応のプレイヤー番号");
                        }
                        break;

                    case Komasyurui14.H05_Kin____:
                        switch (entry.Value.Pside)
                        {
                            case Playerside.P1: int_fingerG1++; break;
                            case Playerside.P2: int_fingerG2++; break;
                            default: throw new Exception("未対応のプレイヤー番号");
                        }
                        break;

                    case Komasyurui14.H06_Gyoku__:
                        switch (entry.Value.Pside)
                        {
                            case Playerside.P1: int_fingerK1++; break;
                            case Playerside.P2: int_fingerK2++; break;
                            default: throw new Exception("未対応のプレイヤー番号");
                        }
                        break;

                    case Komasyurui14.H07_Hisya__:
                        switch (entry.Value.Pside)
                        {
                            case Playerside.P1: int_fingerR1++; break;
                            case Playerside.P2: int_fingerR2++; break;
                            default: throw new Exception("未対応のプレイヤー番号");
                        }
                        break;

                    case Komasyurui14.H08_Kaku___:
                        switch (entry.Value.Pside)
                        {
                            case Playerside.P1: int_fingerB1++; break;
                            case Playerside.P2: int_fingerB2++; break;
                            default: throw new Exception("未対応のプレイヤー番号");
                        }
                        break;

                    case Komasyurui14.H09_Ryu____:
                        switch (entry.Value.Pside)
                        {
                            case Playerside.P1: int_fingerR1++; break;
                            case Playerside.P2: int_fingerR2++; break;
                            default: throw new Exception("未対応のプレイヤー番号");
                        }
                        break;

                    case Komasyurui14.H10_Uma____:
                        switch (entry.Value.Pside)
                        {
                            case Playerside.P1: int_fingerB1++; break;
                            case Playerside.P2: int_fingerB2++; break;
                            default: throw new Exception("未対応のプレイヤー番号");
                        }
                        break;

                    case Komasyurui14.H11_Tokin__:
                        switch (entry.Value.Pside)
                        {
                            case Playerside.P1: int_fingerP1++; break;
                            case Playerside.P2: int_fingerP2++; break;
                            default: throw new Exception("未対応のプレイヤー番号");
                        }
                        break;

                    case Komasyurui14.H12_NariKyo:
                        switch (entry.Value.Pside)
                        {
                            case Playerside.P1: int_fingerL1++; break;
                            case Playerside.P2: int_fingerL2++; break;
                            default: throw new Exception("未対応のプレイヤー番号");
                        }
                        break;

                    case Komasyurui14.H13_NariKei:
                        switch (entry.Value.Pside)
                        {
                            case Playerside.P1: int_fingerN1++; break;
                            case Playerside.P2: int_fingerN2++; break;
                            default: throw new Exception("未対応のプレイヤー番号");
                        }
                        break;

                    case Komasyurui14.H14_NariGin:
                        switch (entry.Value.Pside)
                        {
                            case Playerside.P1: int_fingerS1++; break;
                            case Playerside.P2: int_fingerS2++; break;
                            default: throw new Exception("未対応のプレイヤー番号");
                        }
                        break;

                    default:
                        throw new Exception("未対応の駒種類=[" + entry.Value.Komasyurui + "]");
                }
            }

            //
            // 40個の駒が、どの升に居るか
            //
            {
                // finger の順に並べる。
                RO_Star[] komas = new RO_Star[40];

                foreach (KeyValuePair<Finger, RO_Star> entry in komaDic)
                {
                    Debug.Assert(0 <= (int)entry.Key && (int)entry.Key <= 39, "entry.Key=[" + (int)entry.Key + "]");

                    komas[(int)entry.Key] = entry.Value;
                }

                // finger の順に追加。
                int komaHandle = 0;
                foreach (RO_Star koma in komas)
                {
                    dst_Sky.PutOverwriteOrAdd_Starlight(
                        komaHandle,
                        new RO_Starlight(
                            //komaHandle,
                            koma
                        )
                    );
                    komaHandle++;
                }
            }

            StartposImporter.Assert_HirateHonsyogi(dst_Sky, "ToSkyの終了直前 this.InputLine=[" + this.InputLine + "]");

            return SkyConst.NewInstance(dst_Sky,
                -1//dst_sky で設定済みなのでそのまま。
                );
        }

    }
}
