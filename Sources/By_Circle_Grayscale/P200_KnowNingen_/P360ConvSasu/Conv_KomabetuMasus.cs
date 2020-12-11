﻿using Grayscale.P003Log.I500Struct;
using Grayscale.P035Collection.L500Struct;
using Grayscale.P056Syugoron.I250Struct;
using Grayscale.P211WordShogi.L250Masu;
using Grayscale.P212ConvPside.L500Converter;
using Grayscale.P213Komasyurui.L250Word;
using Grayscale.P218Starlight.I500Struct;
using Grayscale.P224Sky.L500Struct;
using Grayscale.P226Tree.I500Struct;
using Grayscale.P238Seiza.L250Struct;
using Grayscale.P238Seiza.L500Util;
using Grayscale.P247KyokumenWra.L500Struct;
using Grayscale.P258UtilSky258.L500UtilSky;
using Grayscale.P339ConvKyokume.L500Converter;
using Grayscale.P353ConvSasuEx.L500Converter;
using System.Collections.Generic;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P360ConvSasu.L500Converter
{
    public abstract class Conv_KomabetuMasus
    {

        /// <summary>
        /// FIXME: 使ってない？
        /// 
        /// 変換「自駒が動ける升」→「自駒が動ける手」
        /// </summary>
        /// <param name="kmDic_Self"></param>
        /// <returns></returns>
        public static Maps_OneAndMulti<Finger, Starbeamable> ToKomabetuSasite(
            Maps_OneAndOne<Finger, SySet<SyElement>> kmDic_Self,
            Node<Starbeamable, KyokumenWrapper> siteiNode_genzai
            )
        {

            Maps_OneAndMulti<Finger, Starbeamable> komaTe = new Maps_OneAndMulti<Finger, Starbeamable>();

            //
            //
            kmDic_Self.Foreach_Entry((Finger key, SySet<SyElement> value, ref bool toBreak) =>
            {
                foreach (SyElement masuHandle in value.Elements)
                {
                    RO_Star koma = Util_Starlightable.AsKoma(siteiNode_genzai.Value.KyokumenConst.StarlightIndexOf(key).Now);


                    Starbeamable sasite = new RO_Starbeam(
                        //key,
                        // 元
                            koma,
                        // 先
                            new RO_Star(
                                koma.Pside,
                                Masu_Honshogi.Masus_All[Conv_SyElement.ToMasuNumber(masuHandle)],
                                koma.Haiyaku//TODO:成るとか考えたい
                            ),

                            Komasyurui14.H00_Null___//取った駒不明
                        );
                    //sbSfen.Append(sbSfen.ToString());

                    if (komaTe.ContainsKey(key))
                    {
                        // すでに登録されている駒
                        komaTe.AddExists(key, sasite);
                    }
                    else
                    {
                        // まだ登録されていない駒
                        komaTe.AddNew(key, sasite);
                    }

                }
            });

            return komaTe;
        }




        public static Dictionary<string, SasuEntry> ToSasitebetuSky1(
            Maps_OneAndOne<Finger, SySet<SyElement>> komabetuSusumuMasus,
            SkyConst src_Sky,
            IErrorController errH
        )
        {
            Dictionary<string, SasuEntry> result_komabetuEntry = new Dictionary<string, SasuEntry>();

            komabetuSusumuMasus.Foreach_Entry((Finger key, SySet<SyElement> value, ref bool toBreak) =>
            {
                RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(key).Now);


                foreach (SyElement dstMasu in value.Elements)
                {

                    Starbeamable sasite = Util_Sky258A.BuildSasite(
                        //key,
                        new RO_Star(src_Sky.KaisiPside, koma.Masu, koma.Haiyaku),
                        new RO_Star(src_Sky.KaisiPside, dstMasu, koma.Haiyaku),//FIXME:配役は適当。
                        Komasyurui14.H00_Null___
                        );

                    string sasiteStr = Conv_SasiteStr_Sfen.ToSasiteStr_Sfen(sasite);//重複防止用のキー

                    if (!result_komabetuEntry.ContainsKey(sasiteStr))
                    {
                        result_komabetuEntry.Add(
                            sasiteStr,
                            new SasuEntry(
                                sasite,
                                key,//動かす駒
                                dstMasu,//移動先升
                                false//成りません
                                )
                            );
                    }
                }
            });

            return result_komabetuEntry;
        }


        //Dictionary<Starbeamable, KyokumenWrapper>
        public static Dictionary<string, SasuEntry> KomabetuMasus_ToSasitebetuSky(
            List_OneAndMulti<Finger, SySet<SyElement>> sMs, SkyConst src_Sky, IErrorController errH)
        {
            Dictionary<string, SasuEntry> sasitebetuEntry = new Dictionary<string, SasuEntry>();


            sMs.Foreach_Entry((Finger key, SySet<SyElement> value, ref bool toBreak) =>
            {
                RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(key).Now);


                foreach (SyElement dstMasu in value.Elements)
                {
                    Starbeamable sasite = Util_Sky258A.BuildSasite(
                        new RO_Star(src_Sky.KaisiPside, koma.Masu, koma.Haiyaku),
                        new RO_Star(src_Sky.KaisiPside, dstMasu, koma.Haiyaku),//FIXME:配役は適当。
                        Komasyurui14.H00_Null___
                        );

                    string sasiteStr = Conv_SasiteStr_Sfen.ToSasiteStr_Sfen(sasite);//重複防止用のキー
                    SasuEntry sasuEntry = new SasuEntry(
                        sasite,
                        key,//動かす駒
                        dstMasu,//移動先升
                        false//成りません。
                        );
                    if (!sasitebetuEntry.ContainsKey(sasiteStr))
                    {
                        sasitebetuEntry.Add(sasiteStr, sasuEntry);
                    }
                }
            });

            return sasitebetuEntry;

            /*
            Dictionary<Starbeamable, KyokumenWrapper> result = new Dictionary<Starbeamable, KyokumenWrapper>();
            foreach (KeyValuePair<string, SasuEntry> entry in sasitebetuEntry)
            {
                result.Add(
                    entry.Value.NewSasite,
                    new KyokumenWrapper(Util_Sasu341.Sasu(
                    src_Sky,//指定局面
                    entry.Value.Finger,//動かす駒
                    entry.Value.Masu,//移動先升
                    entry.Value.Naru,//成りません。
                    errH
                )));
            }

            return result;
             */
        }

    }
}