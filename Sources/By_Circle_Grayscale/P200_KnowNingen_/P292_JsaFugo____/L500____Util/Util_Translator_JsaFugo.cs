using Grayscale.P062_ConvText___.L500____Converter;
using Grayscale.P212_ConvPside__.L500____Converter;
using Grayscale.P213_Komasyurui_.L500____Util;
using Grayscale.P214_Masu_______.L500____Util;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P226_Tree_______.L___500_Struct;
using Grayscale.P238_Seiza______.L250____Struct;
using Grayscale.P238_Seiza______.L500____Util;
using Grayscale.P239_ConvWords__.L500____Converter;
using Grayscale.P247_KyokumenWra.L500____Struct;
using Grayscale.P292_JsaFugo____.L250____Struct;
using System.Text;

namespace Grayscale.P292_JsaFugo____.L500____Util
{
    public abstract class Util_Translator_JsaFugo
    {
        /// <summary>
        /// ************************************************************************************************************************
        /// 棋譜用の符号テキスト(*1)を作ります。
        /// ************************************************************************************************************************
        /// 
        ///         *1…「▲５五銀上」など。
        ///         
        ///         “同”表記に「置き換えない」バージョンです。
        /// 
        /// </summary>
        /// <param name="sasite"></param>
        /// <param name="previousKomaP"></param>
        /// <returns></returns>
        public static string ToString_NoUseDou(
            JsaFugoImpl jsaFugo,
            RO_Starbeam sasite
            )
        {
            StringBuilder sb = new StringBuilder();

            RO_Star koma = Util_Starlightable.AsKoma(sasite.Now);

            sb.Append(Conv_Playerside.ToSankaku(koma.Pside));

            //------------------------------
            // “同”に変換せず、“筋・段”をそのまま出します。
            //------------------------------
            int suji;
            int dan;
            Util_MasuNum.TryMasuToSuji(koma.Masu, out suji);
            Util_MasuNum.TryMasuToDan(koma.Masu, out dan);

            sb.Append(Conv_Int.ToArabiaSuji(suji));
            sb.Append(Conv_Int.ToKanSuji(dan));

            //------------------------------
            // “歩”とか。“全”ではなく“成銀”    ＜符号用＞
            //------------------------------
            sb.Append(Util_Komasyurui14.Fugo[(int)jsaFugo.Syurui]);

            //------------------------------
            // “右”とか
            //------------------------------
            sb.Append(Conv_MigiHidari.ToStr(jsaFugo.MigiHidari));

            //------------------------------
            // “寄”とか
            //------------------------------
            sb.Append(Conv_AgaruHiku.ToStr(jsaFugo.AgaruHiku));

            //------------------------------
            // “成”とか
            //------------------------------
            sb.Append(Conv_NariNarazu.Nari_ToStr(jsaFugo.Nari));

            //------------------------------
            // “打”とか
            //------------------------------
            sb.Append(Conv_DaHyoji.ToBool(jsaFugo.DaHyoji));

            return sb.ToString();
        }


        /// <summary>
        /// ************************************************************************************************************************
        /// 棋譜用の符号テキスト(*1)を作ります。
        /// ************************************************************************************************************************
        /// 
        ///         *1…「▲５五銀上」など。
        /// 
        /// </summary>
        /// <param name="douExpr">“同”表記に置き換えるなら真。</param>
        /// <param name="previousKomaP"></param>
        /// <returns></returns>
        public static string ToString_UseDou(
            JsaFugoImpl jsaFugo,
            Node<Starbeamable, KyokumenWrapper> siteiNode
            )
        {
            StringBuilder sb = new StringBuilder();


            Starbeamable curSasite = siteiNode.Key;
            RO_Star curSrcKoma = Util_Starlightable.AsKoma(curSasite.LongTimeAgo);
            RO_Star curDstKoma = Util_Starlightable.AsKoma(curSasite.Now);


            sb.Append(Conv_Playerside.ToSankaku(curDstKoma.Pside));

            //------------------------------
            // “同”で表記できるところは、“同”で表記します。それ以外は“筋・段”で表記します。
            //------------------------------
            if (!siteiNode.IsRoot())
            {
                Starbeamable preSasite = siteiNode.GetParentNode().Key;
                if (null != preSasite)
                {
                    //RO_Star_Koma preSrcKoma = Util_Starlightable.AsKoma(preSasite.LongTimeAgo);
                    RO_Star preDstKoma = Util_Starlightable.AsKoma(preSasite.Now);
                    if (Conv_SyElement.ToMasuNumber(preDstKoma.Masu)==Conv_SyElement.ToMasuNumber(curDstKoma.Masu))
                    {
                        // “同”
                        sb.Append("同");
                        goto gt_Next1;
                    }
                }
            }

            {
                // “筋・段”
                int suji;
                int dan;
                Util_MasuNum.TryMasuToSuji(curDstKoma.Masu, out suji);
                Util_MasuNum.TryMasuToDan(curDstKoma.Masu, out dan);

                sb.Append(Conv_Int.ToArabiaSuji(suji));
                sb.Append(Conv_Int.ToKanSuji(dan));
            }
        gt_Next1:
            ;

            //------------------------------
            // “歩”とか。“全”ではなく“成銀”    ＜符号用＞
            //------------------------------
            sb.Append(Util_Komasyurui14.Fugo[(int)jsaFugo.Syurui]);

            //------------------------------
            // “右”とか
            //------------------------------
            sb.Append(Conv_MigiHidari.ToStr(jsaFugo.MigiHidari));

            //------------------------------
            // “寄”とか
            //------------------------------
            sb.Append(Conv_AgaruHiku.ToStr(jsaFugo.AgaruHiku));

            //------------------------------
            // “成”とか
            //------------------------------
            sb.Append(Conv_NariNarazu.Nari_ToStr(jsaFugo.Nari));

            //------------------------------
            // “打”とか
            //------------------------------
            sb.Append(Conv_DaHyoji.ToBool(jsaFugo.DaHyoji));


            return sb.ToString();
        }

    }
}
