using Grayscale.P003Log.I500Struct;
using Grayscale.P035Collection.L500Struct;
using Grayscale.P056Syugoron.I250Struct;
using Grayscale.P218Starlight.I500Struct;
using Grayscale.P224Sky.L500Struct;
using Grayscale.P226Tree.I500Struct;
using Grayscale.P238Seiza.L250Struct;
using Grayscale.P238Seiza.L500Util;
using Grayscale.P247KyokumenWra.L500Struct;
using Grayscale.P258UtilSky258.L500UtilSky;
using System.Collections.Generic;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P360ConvSasu.L500Converter
{
    public abstract class Conv_SasitebetuKyokumenwrappers361
    {
        /// <summary>
        /// FIXME:使ってない？
        /// 
        /// 変換『「指し手→局面」のコレクション』→『「駒、指し手」のペアのリスト』
        /// </summary>
        public static List<Couple<Finger, SyElement>> NextNodes_ToKamList(
            SkyConst src_Sky_genzai,
            Node<Starbeamable, KyokumenWrapper> hubNode,
            KwErrorHandler errH
            )
        {
            List<Couple<Finger, SyElement>> kmList = new List<Couple<Finger, SyElement>>();

            // TODO:
            hubNode.Foreach_ChildNodes((string key, Node<Starbeamable, KyokumenWrapper> nextNode, ref bool toBreak) =>
            {
                RO_Star srcKoma = Util_Starlightable.AsKoma(nextNode.Key.LongTimeAgo);
                RO_Star dstKoma = Util_Starlightable.AsKoma(nextNode.Key.Now);


                SyElement srcMasu = srcKoma.Masu;
                SyElement dstMasu = dstKoma.Masu;

                Finger figKoma = Util_Sky_FingersQuery.InMasuNow(src_Sky_genzai, srcMasu).ToFirst();

                kmList.Add(new Couple<Finger, SyElement>(figKoma, dstMasu));
            });

            return kmList;
        }
    }
}
