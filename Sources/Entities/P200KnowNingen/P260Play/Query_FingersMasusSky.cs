using Grayscale.Kifuwarakaku.Entities.Logger;
using Grayscale.P035Collection.L500Struct;
using Grayscale.P056Syugoron.I250Struct;
using Grayscale.P224Sky.L500Struct;
using Grayscale.P260Play.L250Calc;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P260Play.L500Query
{
    //public class Arg_KmEffectSameIKUSA()
    //{
    //    public Arg_KmEffectSameIKUSA()
    //    {
    //    }
    //}

    public class Query_FingersMasusSky
    {

        /// <summary>
        /// 盤上の駒の利き（利きを調べる側）
        /// </summary>
        /// <param name="fs_sirabetaiKoma">fingers</param>
        /// <param name="masus_mikata_Banjo"></param>
        /// <param name="masus_aite_Banjo"></param>
        /// <param name="src_Sky"></param>
        /// <returns></returns>
        public static Maps_OneAndOne<Finger, SySet<SyElement>> To_KomabetuKiki_OnBanjo(
            Fingers fs_sirabetaiKoma,
            SySet<SyElement> masus_mikata_Banjo,
            SySet<SyElement> masus_aite_Banjo,
            SkyConst src_Sky
            )
        {
            // 利きを調べる側の利き（戦駒）
            Maps_OneAndOne<Finger, SySet<SyElement>> komabetuKiki = QuerySkyFingers.GetPotentialMoves(src_Sky, fs_sirabetaiKoma);

            // 盤上の現手番の駒利きから、 現手番の駒がある枡を除外します。
            komabetuKiki = Play_KomaAndMove.MinusMasus(src_Sky, komabetuKiki, masus_mikata_Banjo);

            // そこから、相手番の駒がある枡「以降」を更に除外します。
            komabetuKiki = Play_KomaAndMove.Minus_OverThereMasus(src_Sky, komabetuKiki, masus_aite_Banjo);

            return komabetuKiki;
        }
    }
}
