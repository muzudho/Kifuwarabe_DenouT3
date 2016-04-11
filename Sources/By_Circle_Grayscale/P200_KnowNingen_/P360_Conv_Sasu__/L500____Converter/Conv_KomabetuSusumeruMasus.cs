using Grayscale.P035_Collection_.L500____Struct;
using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P213_Komasyurui_.L250____Word;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P238_Seiza______.L250____Struct;
using Grayscale.P238_Seiza______.L500____Util;
using Grayscale.P269_Util_Sasu__.L500____Util;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P360_Conv_Sasu__.L500____Converter
{
    public abstract class Conv_KomabetuSusumeruMasus
    {
        /// <summary>
        /// 変換「各（自駒が動ける升）」→「各（自駒が動ける手）」
        /// </summary>
        /// <param name="komaBETUSusumeruMasus">駒別の進める升</param>
        /// <param name="siteiNode">指定ノード</param>
        /// <returns></returns>
        public static Maps_OneAndMulti<Finger, Starbeamable> ToKomaBETUAllSasites(
            List_OneAndMulti<Finger, SySet<SyElement>> komaBETUSusumeruMasus,
            SkyConst src_Sky//Node<Starbeamable, KyokumenWrapper> siteiNode
            )
        {
            Maps_OneAndMulti<Finger, Starbeamable> result_komabetuAllSasite = new Maps_OneAndMulti<Finger, Starbeamable>();

            komaBETUSusumeruMasus.Foreach_Entry((Finger figKoma, SySet<SyElement> susumuMasuSet, ref bool toBreak) =>
            {
                // 動かす星。
                RO_Star srcStar = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(figKoma).Now);

                foreach (SyElement susumuMasu in susumuMasuSet.Elements)// 星が進める升。
                {
                    // 移動先の星（升の変更）
                    RO_Star dstStar = new RO_Star(
                        srcStar.Pside,
                        susumuMasu,//Masu_Honshogi.Items_All[Util_Masu10.AsMasuNumber(susumuMasu)],
                        srcStar.Komasyurui// srcStar.Haiyaku//TODO:ここで、駒の種類が「成り」に上書きされているバージョンも考えたい
                    );

                    Starbeamable sasite = new RO_Starbeam(
                        srcStar,// 移動元
                        dstStar,// 移動先
                        Komasyurui14.H00_Null___//取った駒不明
                    );
                    result_komabetuAllSasite.Put_NewOrOverwrite(figKoma, sasite);//FIXME: １つの駒に指し手は１つ？？

                    // これが通称【水際のいんちきプログラム】なんだぜ☆
                    // 必要により、【成り】の指し手を追加します。
                    Util_Sasu269.Add_KomaBETUAllNariSasites(
                        result_komabetuAllSasite,
                        figKoma,//動かす駒
                        srcStar,//動かす星
                        dstStar//移動先の星
                        );
                }
            });

            return result_komabetuAllSasite;
        }

    }
}
