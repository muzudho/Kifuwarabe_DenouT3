using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P211_WordShogi__.L250____Masu;
using Grayscale.P211_WordShogi__.L500____Word;

namespace Grayscale.P212_ConvPside__.L500____Converter
{
    public abstract class Conv_Okiba
    {
        public static Playerside ToPside(Okiba okiba)
        {
            Playerside pside;
            switch (okiba)
            {
                case Okiba.Gote_Komadai:
                    pside = Playerside.P2;
                    break;
                case Okiba.Sente_Komadai:
                    pside = Playerside.P1;
                    break;
                default:
                    pside = Playerside.Empty;
                    break;
            }

            return pside;
        }

        public static SyElement GetFirstMasuFromOkiba(Okiba okiba)
        {
            SyElement firstMasu;

            switch (okiba)
            {
                case Okiba.ShogiBan:
                    firstMasu = Masu_Honshogi.Query_Basho( Masu_Honshogi.nban11_１一);//[0]
                    break;

                case Okiba.Sente_Komadai:
                    firstMasu = Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01);//[81]
                    break;

                case Okiba.Gote_Komadai:
                    firstMasu = Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01);//[121]
                    break;

                case Okiba.KomaBukuro:
                    firstMasu = Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01);//[161];
                    break;

                default:
                    //エラー
                    firstMasu = Masu_Honshogi.Query_Basho(Masu_Honshogi.nError);// -1→[201];
                    break;
            }

            return firstMasu;
        }

    }
}
