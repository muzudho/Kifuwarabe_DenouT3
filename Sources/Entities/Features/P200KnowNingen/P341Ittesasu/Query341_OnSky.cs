using Grayscale.Kifuwarakaku.Entities.Logging;
using Grayscale.P056Syugoron.I250Struct;
using Grayscale.P211WordShogi.I250Masu;
using Grayscale.P211WordShogi.L500Word;
using Grayscale.P212ConvPside.L500Converter;
using Grayscale.P213Komasyurui.L250Word;
using Grayscale.P213Komasyurui.L500Util;
using Grayscale.P224Sky.L500Struct;
using Grayscale.P234Komahaiyaku.L500Util;
using Grayscale.P238Seiza.L250Struct;
using Grayscale.P238Seiza.L500Util;
using Grayscale.P256SeizaFinger.L250Struct;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P341Ittesasu.L500UtilA
{
    public class Query341_OnSky
    {

        /// <summary>
        /// 将棋盤上での検索
        /// </summary>
        /// <param name="srcAll">候補マス</param>
        /// <param name="komas"></param>
        /// <returns></returns>
        public static bool Query_Koma(
            Playerside pside,
            Komasyurui14 syurui,
            SySet<SyElement> srcAll,
            SkyConst src_Sky,//KifuTree kifu,
            out Finger foundKoma
            )
        {
            //SkyConst src_Sky = kifu.CurNode.Value.ToKyokumenConst;

            bool hit = false;
            foundKoma = Fingers.Error_1;


            foreach (New_Basho masu1 in srcAll.Elements)//筋・段。（先後、種類は入っていません）
            {
                foreach (Finger koma1 in Finger_Honshogi.Items_KomaOnly)
                {
                    RO_Star koma2 = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(koma1).Now);


                        if (pside == koma2.Pside
                            && Okiba.ShogiBan == Conv_SyElement.ToOkiba(koma2.Masu)
                            && Util_Komasyurui14.Matches(syurui, Util_Komahaiyaku184.Syurui(koma2.Haiyaku))
                            && masu1 == koma2.Masu
                            )
                        {
                            // 候補マスにいた
                            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                            hit = true;
                            foundKoma = koma1;
                            break;
                        }
                }
            }

            return hit;
        }





    }
}
