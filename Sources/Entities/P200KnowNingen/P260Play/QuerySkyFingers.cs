using System;
using Grayscale.Kifuwarakaku.Entities.Logger;
using Grayscale.P035Collection.L500Struct;
using Grayscale.P056Syugoron.I250Struct;
using Grayscale.P224Sky.L500Struct;
using Grayscale.P258UtilSky258.L500UtilSky;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

#if DEBUG

#endif

namespace Grayscale.P260Play.L250Calc
{
    public abstract class QuerySkyFingers
    {
        /// <summary>
        /// 指定した駒全てについて、基本的な駒の動きを返します。（金は「前、ななめ前、横、下に進める」のような）
        /// </summary>
        /// <param name="srcSky"></param>
        /// <param name="fingers"></param>
        /// <param name="logTag"></param>
        /// <returns></returns>
        public static Maps_OneAndOne<Finger, SySet<SyElement>> GetPotentialMoves(
            SkyConst srcSky,
            Fingers fingers,
            ILogTag logTag
            )
        {
            if (fingers is null) throw new ArgumentNullException(nameof(fingers));

            Maps_OneAndOne<Finger, SySet<SyElement>> kiki_fMs = new Maps_OneAndOne<Finger, SySet<SyElement>>();// 「どの駒を、どこに進める」の一覧

            foreach (Finger finger in fingers.Items)
            {
                // ポテンシャル・ムーブを調べます。
                SySet<SyElement> move = Util_Sky_SyugoQuery.KomaKidou_Potential(finger, srcSky);//←ポテンシャル・ムーブ取得関数を選択。歩とか。

                if (!move.IsEmptySet())
                {
                    // 移動可能升があるなら
                    Util_Sky258A.AddOverwrite(kiki_fMs, finger, move);
                }
            }

            return kiki_fMs;
        }

    }
}
