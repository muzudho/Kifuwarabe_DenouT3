using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P035_Collection_.L500____Struct;
using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P226_Tree_______.L___500_Struct;
using Grayscale.P238_Seiza______.L250____Struct;
using Grayscale.P238_Seiza______.L500____Util;
using Grayscale.P247_KyokumenWra.L500____Struct;
using Grayscale.P258_UtilSky258_.L500____UtilSky;
using System.Collections.Generic;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P360_Conv_Sasu__.L500____Converter
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
