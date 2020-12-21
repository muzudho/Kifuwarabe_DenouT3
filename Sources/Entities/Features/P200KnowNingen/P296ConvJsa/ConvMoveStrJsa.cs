namespace Grayscale.Kifuwarakaku.Entities.Features
{
    public abstract class ConvMoveStrJsa
    {
        /// <summary>
        /// 「▲７六歩」といった符号にして返します。
        /// </summary>
        /// <param name="node">keyで指し手の指定、かつ、１つ前のノードに移動するのに使います。</param>
        /// <param name="kyokumenWrapper">現局面です。</param>
        /// <returns></returns>
        public static string ToMoveStrJsa(
            Node<IMove, KyokumenWrapper> node
            //KyokumenWrapper kyokumenWrapper
            )
        {
            RO_Star koma = Util_Starlightable.AsKoma(((IMove)node.Key).LongTimeAgo);

            JsaFugoImpl jsaFugo = Array_JsaFugoCreator15.ItemMethods[(int)Util_Komahaiyaku184.Syurui(koma.Haiyaku)](node.Key,
                node.Value// kyokumenWrapper,
                );//「▲２二角成」なら、馬（dst）ではなくて角（src）。

            return Util_Translator_JsaFugo.ToString_UseDou(jsaFugo, node);
        }

    }
}
