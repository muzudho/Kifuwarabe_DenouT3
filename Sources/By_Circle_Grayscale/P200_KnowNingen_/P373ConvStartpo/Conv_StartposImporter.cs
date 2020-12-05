using Grayscale.P003Log.I500Struct;
using Grayscale.P056Syugoron.I250Struct;
using Grayscale.P211WordShogi.L500Word;
using Grayscale.P213Komasyurui.L250Word;
using Grayscale.P224Sky.L500Struct;
using Grayscale.P238Seiza.L250Struct;
using Grayscale.P247KyokumenWra.L500Struct;
using Grayscale.P258UtilSky258.L500UtilSky;
using Grayscale.P276SeizaStartp.L500Struct;
using Grayscale.P324KifuTree.L250Struct;
using Grayscale.P325PnlTaikyoku.I250Struct;
using Grayscale.P341Ittesasu.L500UtilA;
using Grayscale.P355_KifuParserA.I500Parser;
using Grayscale.P372KyokuParser.I500Parser;
using Grayscale.P372KyokuParser.L500Parser;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P373ConvStartpo.L500Converter
{
    public abstract class Conv_StartposImporter
    {
                /// <summary>
        /// 
        /// </summary>
        /// <param name="kifu"></param>
        /// <param name="restText"></param>
        /// <param name="startposImporter"></param>
        /// <param name="logTag"></param>
        public static ParsedKyokumen ToParsedKyokumen(
            Model_Manual model_Manual,// Gui局面を使用
            StartposImporter startposImporter,
            KifuParserA_Genjo genjo,
            IKwErrorHandler errH
            )
        {
            ParsedKyokumen parsedKyokumen = new ParsedKyokumenImpl();

            //------------------------------
            // 初期局面の先後
            //------------------------------
            if (startposImporter.RO_SfenStartpos.PsideIsBlack)
            {
                // 黒は先手。
                parsedKyokumen.FirstPside = Playerside.P1;
            }
            else
            {
                // 白は後手。
                parsedKyokumen.FirstPside = Playerside.P2;
            }

            //------------------------------
            // 駒の配置
            //------------------------------
            parsedKyokumen.KifuNode = new KifuNodeImpl(
                    Util_Sky258A.ROOT_SASITE,//ルートなので
                    new KyokumenWrapper(
                        SkyConst.NewInstance(
                            startposImporter.ToSky(
                                parsedKyokumen.FirstPside,
                                startposImporter.RO_SfenStartpos.Temezumi// FIXME: 将棋所だと常に 1 かも？？
                            ),
                            -1 // src_Sky_Newで設定済みだからそのまま。
                        )
                    )
                );

            //------------------------------
            // 駒袋に表示されている駒を、駒台に表示されるように移します。
            //------------------------------
            {
                //------------------------------
                // 持ち駒 ▲王
                //------------------------------
                if (0 < startposImporter.RO_SfenStartpos.Moti1K)
                {
                    parsedKyokumen.MotiList.Add(new MotiItemImpl(Komasyurui14.H06_Gyoku__, startposImporter.RO_SfenStartpos.Moti1K, Playerside.P1));
                    //System.C onsole.WriteLine("mK=" + ro_SfenStartpos.Moti1K);
                }

                //------------------------------
                // 持ち駒 ▲飛
                //------------------------------
                if (0 < startposImporter.RO_SfenStartpos.Moti1R)
                {
                    parsedKyokumen.MotiList.Add(new MotiItemImpl(Komasyurui14.H07_Hisya__, startposImporter.RO_SfenStartpos.Moti1R, Playerside.P1));
                    //System.C onsole.WriteLine("mR=" + ro_SfenStartpos.Moti1R);
                }

                //------------------------------
                // 持ち駒 ▲角
                //------------------------------
                if (0 < startposImporter.RO_SfenStartpos.Moti1B)
                {
                    parsedKyokumen.MotiList.Add(new MotiItemImpl(Komasyurui14.H08_Kaku___, startposImporter.RO_SfenStartpos.Moti1B, Playerside.P1));
                    //System.C onsole.WriteLine("mB=" + ro_SfenStartpos.Moti1B);
                }

                //------------------------------
                // 持ち駒 ▲金
                //------------------------------
                if (0 < startposImporter.RO_SfenStartpos.Moti1G)
                {
                    parsedKyokumen.MotiList.Add(new MotiItemImpl(Komasyurui14.H05_Kin____, startposImporter.RO_SfenStartpos.Moti1G, Playerside.P1));
                    //System.C onsole.WriteLine("mG=" + ro_SfenStartpos.Moti1G);
                }

                //------------------------------
                // 持ち駒 ▲銀
                //------------------------------
                if (0 < startposImporter.RO_SfenStartpos.Moti1S)
                {
                    parsedKyokumen.MotiList.Add(new MotiItemImpl(Komasyurui14.H04_Gin____, startposImporter.RO_SfenStartpos.Moti1S, Playerside.P1));
                    //System.C onsole.WriteLine("mS=" + ro_SfenStartpos.Moti1S);
                }

                //------------------------------
                // 持ち駒 ▲桂
                //------------------------------
                if (0 < startposImporter.RO_SfenStartpos.Moti1N)
                {
                    parsedKyokumen.MotiList.Add(new MotiItemImpl(Komasyurui14.H03_Kei____, startposImporter.RO_SfenStartpos.Moti1N, Playerside.P1));
                    //System.C onsole.WriteLine("mN=" + ro_SfenStartpos.Moti1N);
                }

                //------------------------------
                // 持ち駒 ▲香
                //------------------------------
                if (0 < startposImporter.RO_SfenStartpos.Moti1L)
                {
                    parsedKyokumen.MotiList.Add(new MotiItemImpl(Komasyurui14.H02_Kyo____, startposImporter.RO_SfenStartpos.Moti1L, Playerside.P1));
                    //System.C onsole.WriteLine("mL=" + ro_SfenStartpos.Moti1L);
                }

                //------------------------------
                // 持ち駒 ▲歩
                //------------------------------
                if (0 < startposImporter.RO_SfenStartpos.Moti1P)
                {
                    parsedKyokumen.MotiList.Add(new MotiItemImpl(Komasyurui14.H01_Fu_____, startposImporter.RO_SfenStartpos.Moti1P, Playerside.P1));
                    //System.C onsole.WriteLine("mP=" + ro_SfenStartpos.Moti1P);
                }

                //------------------------------
                // 持ち駒 △王
                //------------------------------
                if (0 < startposImporter.RO_SfenStartpos.Moti2k)
                {
                    parsedKyokumen.MotiList.Add(new MotiItemImpl(Komasyurui14.H06_Gyoku__, startposImporter.RO_SfenStartpos.Moti2k, Playerside.P2));
                    //System.C onsole.WriteLine("mk=" + ro_SfenStartpos.Moti2k);
                }

                //------------------------------
                // 持ち駒 △飛
                //------------------------------
                if (0 < startposImporter.RO_SfenStartpos.Moti2r)
                {
                    parsedKyokumen.MotiList.Add(new MotiItemImpl(Komasyurui14.H07_Hisya__, startposImporter.RO_SfenStartpos.Moti2r, Playerside.P2));
                    //System.C onsole.WriteLine("mr=" + ro_SfenStartpos.Moti2r);
                }

                //------------------------------
                // 持ち駒 △角
                //------------------------------
                if (0 < startposImporter.RO_SfenStartpos.Moti2b)
                {
                    parsedKyokumen.MotiList.Add(new MotiItemImpl(Komasyurui14.H08_Kaku___, startposImporter.RO_SfenStartpos.Moti2b, Playerside.P2));
                    //System.C onsole.WriteLine("mb=" + ro_SfenStartpos.Moti2b);
                }

                //------------------------------
                // 持ち駒 △金
                //------------------------------
                if (0 < startposImporter.RO_SfenStartpos.Moti2g)
                {
                    parsedKyokumen.MotiList.Add(new MotiItemImpl(Komasyurui14.H05_Kin____, startposImporter.RO_SfenStartpos.Moti2g, Playerside.P2));
                    //System.C onsole.WriteLine("mg=" + ro_SfenStartpos.Moti2g);
                }

                //------------------------------
                // 持ち駒 △銀
                //------------------------------
                if (0 < startposImporter.RO_SfenStartpos.Moti2s)
                {
                    parsedKyokumen.MotiList.Add(new MotiItemImpl(Komasyurui14.H04_Gin____, startposImporter.RO_SfenStartpos.Moti2s, Playerside.P2));
                    //System.C onsole.WriteLine("ms=" + ro_SfenStartpos.Moti2s);
                }

                //------------------------------
                // 持ち駒 △桂
                //------------------------------
                if (0 < startposImporter.RO_SfenStartpos.Moti2n)
                {
                    parsedKyokumen.MotiList.Add(new MotiItemImpl(Komasyurui14.H03_Kei____, startposImporter.RO_SfenStartpos.Moti2n, Playerside.P2));
                    //System.C onsole.WriteLine("mn=" + ro_SfenStartpos.Moti2n);
                }

                //------------------------------
                // 持ち駒 △香
                //------------------------------
                if (0 < startposImporter.RO_SfenStartpos.Moti2l)
                {
                    parsedKyokumen.MotiList.Add(new MotiItemImpl(Komasyurui14.H02_Kyo____, startposImporter.RO_SfenStartpos.Moti2l, Playerside.P2));
                    //System.C onsole.WriteLine("ml=" + ro_SfenStartpos.Moti2l);
                }

                //------------------------------
                // 持ち駒 △歩
                //------------------------------
                if (0 < startposImporter.RO_SfenStartpos.Moti2p)
                {
                    parsedKyokumen.MotiList.Add(new MotiItemImpl(Komasyurui14.H01_Fu_____, startposImporter.RO_SfenStartpos.Moti2p, Playerside.P2));
                    //System.C onsole.WriteLine("mp=" + ro_SfenStartpos.Moti2p);
                }
            }

            //------------------------------------------------------------------------------------------------------------------------
            // 移動
            //------------------------------------------------------------------------------------------------------------------------
            for (int i = 0; i < parsedKyokumen.MotiList.Count; i++)
            {
                Playerside itaruPside;   //(至)先後
                Okiba itaruOkiba;   //(至)置き場

                if (Playerside.P2 == parsedKyokumen.MotiList[i].Playerside)
                {
                    // 宛：後手駒台
                    itaruPside = Playerside.P2;
                    itaruOkiba = Okiba.Gote_Komadai;
                }
                else
                {
                    // 宛：先手駒台
                    itaruPside = Playerside.P1;
                    itaruOkiba = Okiba.Sente_Komadai;
                }


                //------------------------------
                // 駒を、駒袋から駒台に移動させます。
                //------------------------------
                {
                    parsedKyokumen.buffer_Sky = new SkyBuffer(model_Manual.GuiSkyConst);


                    Fingers komas = Util_Sky_FingersQuery.InOkibaKomasyuruiNow(
                        SkyConst.NewInstance(// FIXME: SkyConstではなく、Skyではだめなのか☆？
                            parsedKyokumen.buffer_Sky,
                            -1//そのまま。
                        ),
                        Okiba.KomaBukuro,
                        parsedKyokumen.MotiList[i].Komasyurui
                    );
                    int moved = 1;
                    foreach (Finger koma in komas.Items)
                    {
                        // 駒台の空いている枡
                        SyElement akiMasu = Util_IttesasuRoutine.GetKomadaiKomabukuroSpace(
                            itaruOkiba,
                            SkyConst.NewInstance(
                                parsedKyokumen.buffer_Sky,
                                -1//そのまま
                            )
                        );

                        parsedKyokumen.buffer_Sky.PutOverwriteOrAdd_Starlight(
                            koma,
                            new RO_Starlight(
                                new RO_Star(
                                    itaruPside,
                                    akiMasu,
                                    parsedKyokumen.MotiList[i].Komasyurui
                                )
                            )
                        );

                        if (parsedKyokumen.MotiList[i].Maisu <= moved)
                        {
                            break;
                        }

                        moved++;
                    }
                }

            }//for


            return parsedKyokumen;
        }

    }
}
