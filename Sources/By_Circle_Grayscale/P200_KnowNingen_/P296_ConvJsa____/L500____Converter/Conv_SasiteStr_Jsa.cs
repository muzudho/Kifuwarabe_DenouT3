﻿using Grayscale.P003Log.I500Struct;
using Grayscale.P218Starlight.I500Struct;
using Grayscale.P226Tree.I500Struct;
using Grayscale.P234Komahaiyaku.L500Util;
using Grayscale.P238_Seiza______.L250____Struct;
using Grayscale.P238_Seiza______.L500____Util;
using Grayscale.P247_KyokumenWra.L500____Struct;
using Grayscale.P292_JsaFugo____.L250____Struct;
using Grayscale.P292_JsaFugo____.L500____Util;
using Grayscale.P295_JsaFugoWrit.L500____Writer;

namespace Grayscale.P296_ConvJsa____.L500____Converter
{
    public abstract class Conv_SasiteStr_Jsa
    {
        /// <summary>
        /// 「▲７六歩」といった符号にして返します。
        /// </summary>
        /// <param name="node">keyで指し手の指定、かつ、１つ前のノードに移動するのに使います。</param>
        /// <param name="kyokumenWrapper">現局面です。</param>
        /// <param name="errH"></param>
        /// <returns></returns>
        public static string ToSasiteStr_Jsa(
            Node<Starbeamable, KyokumenWrapper> node,
            //KyokumenWrapper kyokumenWrapper,
            KwErrorHandler errH
            )
        {
            RO_Star koma = Util_Starlightable.AsKoma(((Starbeamable)node.Key).LongTimeAgo);

            JsaFugoImpl jsaFugo = Array_JsaFugoCreator15.ItemMethods[(int)Util_Komahaiyaku184.Syurui(koma.Haiyaku)](node.Key,
                node.Value,// kyokumenWrapper,
                errH);//「▲２二角成」なら、馬（dst）ではなくて角（src）。

            return Util_Translator_JsaFugo.ToString_UseDou(jsaFugo, node);
        }

    }
}