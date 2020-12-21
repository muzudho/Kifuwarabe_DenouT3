using Grayscale.Kifuwarakaku.Entities.Logging;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using System.Collections.Generic;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P360ConvSasu.L500Converter
{
    public abstract class ConvMoveBetuKyokumenwrappers361
    {
        /// <summary>
        /// FIXME:使ってない？
        /// 
        /// 変換『「指し手→局面」のコレクション』→『「駒、指し手」のペアのリスト』
        /// </summary>
        public static List<Couple<Finger, SyElement>> NextNodes_ToKamList(
            SkyConst src_Sky_genzai,
            Node<IMove, KyokumenWrapper> hubNode
            )
        {
            List<Couple<Finger, SyElement>> kmList = new List<Couple<Finger, SyElement>>();

            // TODO:
            hubNode.Foreach_ChildNodes((string key, Node<IMove, KyokumenWrapper> nextNode, ref bool toBreak) =>
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
