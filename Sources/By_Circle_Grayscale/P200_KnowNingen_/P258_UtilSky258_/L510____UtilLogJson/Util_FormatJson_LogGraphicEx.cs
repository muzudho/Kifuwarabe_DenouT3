using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P035_Collection_.L500____Struct;
using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P211_WordShogi__.L___250_Masu;
using Grayscale.P212_ConvPside__.L500____Converter;
using Grayscale.P213_Komasyurui_.L250____Word;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P226_Tree_______.L___500_Struct;
using Grayscale.P234_Komahaiyaku.L500____Util;
using Grayscale.P238_Seiza______.L250____Struct;
using Grayscale.P238_Seiza______.L500____Util;
using Grayscale.P247_KyokumenWra.L500____Struct;
using Grayscale.P258_UtilSky258_.L500____UtilSky;
using Grayscale.P258_UtilSky258_.L505____ConvLogJson;
using System.Collections.Generic;
using System.Text;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P258_UtilSky258_.L510____UtilLogJson
{
    public abstract class Util_FormatJson_LogGraphicEx
    {
        /// <summary>
        /// 駒別マスをJSON化します。
        /// </summary>
        /// <param name="enableLog"></param>
        /// <param name="src_Sky_base"></param>
        /// <param name="km_sasite"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public static string JsonKyokumens_MultiKomabetuMasus(bool enableLog, SkyConst src_Sky_base, Maps_OneAndOne<Finger, SySet<SyElement>> km_sasite, string comment)
        {
            StringBuilder sb = new StringBuilder();

            if (!enableLog)
            {
                goto gt_EndMethod;
            }

            km_sasite.Foreach_Entry((Finger key, SySet<SyElement> value, ref bool toBreak) =>
            {
                // 駒１つ
                RO_Star koma = Util_Starlightable.AsKoma(src_Sky_base.StarlightIndexOf(key).Now);

                Komasyurui14 ks14 = Util_Komahaiyaku184.Syurui(koma.Haiyaku);

                sb.AppendLine("            [");

                // マスの色
                sb.AppendLine("                { act:\"colorMasu\", style:\"rgba(100,240,100,0.5)\" },");

                // 全マス
                foreach (New_Basho masu in value.Elements)
                {
                    sb.AppendLine("                { act:\"drawMasu\" , masu:" + masu.MasuNumber + " },");
                }


                string komaImg = Util_Converter_LogGraphicEx.Finger_ToString(src_Sky_base, key, "");
                sb.AppendLine("                { act:\"drawImg\", img:\"" + komaImg + "\", masu: " + Conv_SyElement.ToMasuNumber(koma.Masu) + " },");//FIXME:おかしい？

                // コメント
                sb.AppendLine("                { act:\"drawText\", text:\"" + comment + "\"  , x:0, y:20 },");

                sb.AppendLine("            ],");

            });

        gt_EndMethod:
            return sb.ToString();
        }


        /// <summary>
        /// ノードをJSON化します。
        /// </summary>
        /// <param name="enableLog"></param>
        /// <param name="src_Sky_base"></param>
        /// <param name="thisNode"></param>
        /// <param name="comment"></param>
        /// <param name="errH"></param>
        /// <returns></returns>
        public static string JsonElements_Node(bool enableLog, SkyConst src_Sky_base, Node<Starbeamable, KyokumenWrapper> thisNode, string comment, KwErrorHandler errH)
        {
            StringBuilder sb = new StringBuilder();

            if (!enableLog)
            {
                goto gt_EndMethod;
            }

            Starbeamable sasite = thisNode.Key;

            RO_Star srcKoma = Util_Starlightable.AsKoma(sasite.LongTimeAgo);
            RO_Star dstKoma = Util_Starlightable.AsKoma(sasite.Now);


            Finger finger = Util_Sky_FingersQuery.InMasuNow(src_Sky_base, srcKoma.Masu).ToFirst();

            // 駒１つ
            Komasyurui14 ks14 = Util_Komahaiyaku184.Syurui(dstKoma.Haiyaku);

            //sb.AppendLine("            [");

            // マスの色
            sb.AppendLine("                { act:\"colorMasu\", style:\"rgba(100,240,100,0.5)\" },");

            // マス
            sb.AppendLine("                { act:\"drawMasu\" , masu:" + Conv_SyElement.ToMasuNumber(dstKoma.Masu) + " },");


            string komaImg = Util_Converter_LogGraphicEx.Finger_ToString(src_Sky_base, finger, "");
            sb.AppendLine("                { act:\"drawImg\", img:\"" + komaImg + "\", masu: " + Conv_SyElement.ToMasuNumber(dstKoma.Masu) + " },");//FIXME:おかしい？

            // コメント
            sb.AppendLine("                { act:\"drawText\", text:\"" + comment + "\"  , x:0, y:20 },");

            //sb.AppendLine("            ],");

        gt_EndMethod:
            return sb.ToString();
        }


        /// <summary>
        /// ハブ･ノードの次ノード・リストをJSON化します。
        /// </summary>
        /// <param name="enableLog"></param>
        /// <param name="src_Sky_base"></param>
        /// <param name="hubNode"></param>
        /// <param name="comment"></param>
        /// <param name="errH"></param>
        /// <returns></returns>
        public static string JsonKyokumens_NextNodes(bool enableLog, SkyConst src_Sky_base, Node<Starbeamable, KyokumenWrapper> hubNode, string comment, KwErrorHandler errH)
        {
            StringBuilder sb = new StringBuilder();

            if (!enableLog)
            {
                goto gt_EndMethod;
            }

            hubNode.Foreach_ChildNodes((string key, Node<Starbeamable, KyokumenWrapper> node, ref bool toBreak) =>
            {
                Starbeamable sasite = node.Key;

                RO_Star srcKoma1 = Util_Starlightable.AsKoma(sasite.LongTimeAgo);
                RO_Star dstKoma = Util_Starlightable.AsKoma(sasite.Now);


                Finger srcKoma2 = Util_Sky_FingersQuery.InMasuNow(src_Sky_base, srcKoma1.Masu).ToFirst();

                // 駒１つ
                Komasyurui14 ks14 = Util_Komahaiyaku184.Syurui(dstKoma.Haiyaku);

                sb.AppendLine("            [");

                // マスの色
                sb.AppendLine("                { act:\"colorMasu\", style:\"rgba(100,240,100,0.5)\" },");

                // マス
                sb.AppendLine("                { act:\"drawMasu\" , masu:" + Conv_SyElement.ToMasuNumber(dstKoma.Masu) + " },");


                string komaImg = Util_Converter_LogGraphicEx.Finger_ToString(src_Sky_base, srcKoma2, "");
                sb.AppendLine("                { act:\"drawImg\", img:\"" + komaImg + "\", masu: " + Conv_SyElement.ToMasuNumber(dstKoma.Masu) + " },");//FIXME:おかしい？

                // コメント
                sb.AppendLine("                { act:\"drawText\", text:\"" + comment + "\"  , x:0, y:20 },");

                sb.AppendLine("            ],");
            });

        gt_EndMethod:
            return sb.ToString();
        }

        /// <summary>
        /// 用途例：持ち駒を確認するために使います。
        /// </summary>
        /// <param name="hkomas_gen_MOTI"></param>
        /// <returns></returns>
        public static string JsonElements_KomaHandles(bool enableLog, SkyConst src_Sky, List<int> hKomas, string comment)
        {
            StringBuilder sb = new StringBuilder();

            if (!enableLog)
            {
                goto gt_EndMethod;
            }

            //sb.AppendLine("            [");
            sb.AppendLine("                { act:\"colorMasu\", style:\"rgba(100,240,100,0.5)\" },");


            foreach (int hKoma in hKomas)
            {
                RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(hKoma).Now);


                string komaImg = Util_Converter_LogGraphicEx.Finger_ToString(src_Sky, hKoma, "");
                sb.AppendLine("                { act:\"drawImg\", img:\"" + komaImg + "\", masu: " + Conv_SyElement.ToMasuNumber(koma.Masu) + " },");//FIXME:おかしい？
            }



            sb.AppendLine("                { act:\"drawText\", text:\"" + comment + "\"  , x:0, y:20 },");

            //sb.AppendLine("            ],");

        gt_EndMethod:
            return sb.ToString();
        }

        public static string JsonElements_Masus(bool enableLog, SySet<SyElement> masus, string comment)
        {
            StringBuilder sb = new StringBuilder();

            if (!enableLog)
            {
                goto gt_EndMethod;
            }

            sb.AppendLine("                { act:\"colorMasu\", style:\"rgba(100,240,100,0.5)\" },\n");

            foreach (New_Basho masu in masus.Elements)
            {
                sb.AppendLine("                { act:\"drawMasu\" , masu:" + ((int)masu.MasuNumber) + " },\n");
            }



            sb.AppendLine("                { act:\"drawText\", text:\"" + comment + "\"  , x:0, y:20 },\n");

        gt_EndMethod:
            return sb.ToString();
        }


    }
}
