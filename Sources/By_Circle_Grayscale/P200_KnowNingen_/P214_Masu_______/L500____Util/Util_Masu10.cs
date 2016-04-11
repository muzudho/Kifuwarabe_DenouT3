using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P062_ConvText___.L500____Converter;
using Grayscale.P211_WordShogi__.L250____Masu;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P212_ConvPside__.L500____Converter;
using System.Text;

/*
     /// <summary>
    /// ------------------------------------------------------------------------------------------------------------------------
    /// 枡ハンドルの一覧。
    /// ------------------------------------------------------------------------------------------------------------------------
    /// 
    /// ┌─┬─┬─┬─┬─┬─┬─┬─┬─┐
    /// │72│63│54│45│36│27│18│ 9│ 0│
    /// ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
    /// │73│64│55│46│37│28│19│10│ 1│
    /// ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
    /// │74│65│56│47│38│29│20│11│ 2│
    /// ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
    /// │75│66│57│48│39│30│21│12│ 3│
    /// ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
    /// │76│67│58│49│40│31│22│13│ 4│
    /// ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
    /// │77│68│59│50│41│32│23│14│ 5│
    /// ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
    /// │78│69│60│51│42│33│24│15│ 6│
    /// ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
    /// │79│70│61│52│43│34│25│16│ 7│
    /// ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
    /// │80│71│62│53│44│35│26│17│ 8│
    /// └─┴─┴─┴─┴─┴─┴─┴─┴─┘
    /// 先手駒台：81～120
    /// 後手駒台：121～160
    /// 駒袋：161～200
    /// エラー：201
    /// の、計202。
    /// 
    /// 将棋盤上の枡のリスト。
    /// 
    /// ・Add、Removeといった、データ構造に縛られるメソッドは持たせません。
    ///   変わりに、Minus といった汎用的に操作できるメソッドを持たせます。
    /// 
    /// ・Clearメソッドは持たせません。インスタンスを作り直して親要素にセットし直してください。
    ///   空にすることができないオブジェクト（線分など）があることが理由です。
    /// </summary>

 */

namespace Grayscale.P214_Masu_______.L500____Util
{
    public abstract class Util_Masu10
    {

        /// <summary>
        /// 先手駒台は40マス、後手駒台は40マス、駒袋は40マスです。
        /// </summary>
        public const int KOMADAI_KOMABUKURO_SPACE_LENGTH = 40;

        public const int KOMADAI_LAST_SUJI = 4;
        public const int KOMADAI_LAST_DAN = 10;

        public const int SHOGIBAN_LAST_SUJI = 9;
        public const int SHOGIBAN_LAST_DAN = 9;






        static Util_Masu10()
        {
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="okiba"></param>
        /// <param name="suji"></param>
        /// <param name="dan"></param>
        /// <returns></returns>
        public static int Handle_OkibaSujiDanToMasu(Okiba okiba, int suji, int dan)
        {
            int masuHandle = -1;

            switch (okiba)
            {
                case Okiba.ShogiBan:
                    if (1 <= suji && suji <= Util_Masu10.SHOGIBAN_LAST_SUJI && 1 <= dan && dan <= Util_Masu10.SHOGIBAN_LAST_DAN)
                    {
                        masuHandle = (suji - 1) * Util_Masu10.SHOGIBAN_LAST_DAN + (dan - 1);
                    }
                    break;

                case Okiba.Sente_Komadai:
                case Okiba.Gote_Komadai:
                case Okiba.KomaBukuro:
                    if (1 <= suji && suji <= Util_Masu10.KOMADAI_LAST_SUJI && 1 <= dan && dan <= Util_Masu10.KOMADAI_LAST_DAN)
                    {
                        masuHandle = (suji - 1) * Util_Masu10.KOMADAI_LAST_DAN + (dan - 1);
                        masuHandle += Conv_SyElement.ToMasuNumber(Conv_Okiba.GetFirstMasuFromOkiba(okiba));
                    }
                    break;

                default:
                    break;
            }

            return masuHandle;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="okiba"></param>
        /// <param name="suji"></param>
        /// <param name="dan"></param>
        /// <returns></returns>
        public static SyElement OkibaSujiDanToMasu(Okiba okiba, int suji, int dan)
        {
            int masuHandle = Util_Masu10.Handle_OkibaSujiDanToMasu(okiba,suji,dan);


            SyElement masu = Masu_Honshogi.Query_Basho(Masu_Honshogi.nError);//範囲外が指定されることもあります。

            if (Conv_MasuHandle.Yuko(masuHandle))
            {
                masu = Masu_Honshogi.Masus_All[masuHandle];
            }


            return masu;
        }

        public static SyElement OkibaSujiDanToMasu(Okiba okiba, int masuHandle)
        {
            switch (Conv_SyElement.ToOkiba(masuHandle))
            {
                case Okiba.Sente_Komadai:
                    masuHandle -= Conv_SyElement.ToMasuNumber(Conv_Okiba.GetFirstMasuFromOkiba(Okiba.Sente_Komadai));
                    break;

                case Okiba.Gote_Komadai:
                    masuHandle -= Conv_SyElement.ToMasuNumber(Conv_Okiba.GetFirstMasuFromOkiba(Okiba.Gote_Komadai));
                    break;

                case Okiba.KomaBukuro:
                    masuHandle -= Conv_SyElement.ToMasuNumber(Conv_Okiba.GetFirstMasuFromOkiba(Okiba.KomaBukuro));
                    break;

                case Okiba.ShogiBan:
                    // そのんまま
                    break;

                default:
                    // エラー
                    break;
            }

            masuHandle = masuHandle + Conv_SyElement.ToMasuNumber(Conv_Okiba.GetFirstMasuFromOkiba(okiba));

            return Conv_MasuHandle.ToMasu( masuHandle);
        }




        /// <summary>
        /// ************************************************************************************************************************
        /// １マス上、のように指定して、マスを取得します。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="offsetSuji"></param>
        /// <param name="offsetDan"></param>
        /// <returns></returns>
        public static SyElement Offset(Okiba okiba, SyElement masu, Playerside pside, Hogaku muki)
        {
            int offsetSuji;
            int offsetDan;
            Util_Muki.MukiToOffsetSujiDan(muki, pside, out offsetSuji, out offsetDan);

            int suji;
            int dan;
            Util_MasuNum.TryMasuToSuji(masu, out suji);
            Util_MasuNum.TryMasuToDan(masu, out dan);

            return Util_Masu10.OkibaSujiDanToMasu(
                okiba,
                suji + offsetSuji,
                dan + offsetDan);
        }




        /// <summary>
        /// ************************************************************************************************************************
        /// １マス上、のように指定して、マスを取得します。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="offsetSuji"></param>
        /// <param name="offsetDan"></param>
        /// <returns></returns>
        public static SyElement Offset(Okiba okiba, SyElement masu, int offsetSuji, int offsetDan)
        {
            int suji;
            int dan;
            Util_MasuNum.TryMasuToSuji(masu, out suji);
            Util_MasuNum.TryMasuToDan(masu, out dan);

            return Util_Masu10.OkibaSujiDanToMasu(
                    okiba,
                    suji + offsetSuji,
                    dan + offsetDan
                );
        }


        /// <summary>
        /// 後手からも、先手のような座標で指示できるように変換します。
        /// </summary>
        /// <param name="masu"></param>
        /// <param name="pside"></param>
        /// <returns></returns>
        public static SyElement BothSenteView(SyElement masu, Playerside pside)
        {
            SyElement result = masu;

            // 将棋盤上で後手なら、180°回転します。
            if (Okiba.ShogiBan == Conv_SyElement.ToOkiba(masu) && pside == Playerside.P2)
            {
                result = Masu_Honshogi.Masus_All[80 - Conv_SyElement.ToMasuNumber(masu)];
            }

            // 将棋盤で先手、または　駒台か　駒袋なら、指定されたマスにそのまま入れます。

            return result;
        }




        /// <summary>
        /// ************************************************************************************************************************
        /// 相手陣に入っていれば真。
        /// ************************************************************************************************************************
        /// 
        ///         後手は 7,8,9 段。
        ///         先手は 1,2,3 段。
        /// </summary>
        /// <returns></returns>
        public static bool InAitejin(SyElement masu, Playerside pside)
        {
            int dan;
            Util_MasuNum.TryMasuToDan(masu, out dan);

            return (Playerside.P2 == pside && 7 <= dan)
                || (Playerside.P1 == pside && dan <= 3);
        }


        #region 定数
        //------------------------------------------------------------
        /// <summary>
        /// 筋は 1～9 だけ有効です。
        /// </summary>
        public const int YUKO_SUJI_MIN = 1;
        public const int YUKO_SUJI_MAX = 9;

        /// <summary>
        /// 段は 1～9 だけ有効です。
        /// </summary>
        public const int YUKO_DAN_MIN = 1;
        public const int YUKO_DAN_MAX = 9;
        //------------------------------------------------------------
        #endregion



        /// <summary>
        /// 「２八」といった表記にして返します。
        /// 
        /// Conv_SyElement使用。
        /// </summary>
        /// <param name="masu"></param>
        /// <returns></returns>
        public static string ToSujiKanji(SyElement masu)
        {
            StringBuilder sb = new StringBuilder();

            int suji;
            int dan;
            Util_MasuNum.TryMasuToSuji(masu, out suji);
            Util_MasuNum.TryMasuToDan(masu, out dan);

            sb.Append(Conv_Int.ToArabiaSuji(suji));
            sb.Append(Conv_Int.ToKanSuji(dan));

            return sb.ToString();
        }




    }
}
