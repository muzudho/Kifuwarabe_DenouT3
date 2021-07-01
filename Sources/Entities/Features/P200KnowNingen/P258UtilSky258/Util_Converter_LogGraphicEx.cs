using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.Kifuwarakaku.Entities.Features
{
    public abstract class Util_Converter_LogGraphicEx
    {


        /// <summary>
        /// 駒画像のファイル名。
        /// </summary>
        /// <param name="pside"></param>
        /// <param name="ks14"></param>
        /// <param name="extentionWithDot"></param>
        /// <returns></returns>
        public static string PsideKs14_ToString(Playerside pside, PieceType ks14, string extentionWithDot)
        {
            string komaImg;

            if (pside == Playerside.P1)
            {
                switch (ks14)
                {
                    case PieceType.P: komaImg = $"fu{extentionWithDot}"; break;
                    case PieceType.L: komaImg = $"kyo{extentionWithDot}"; break;
                    case PieceType.N: komaImg = $"kei{extentionWithDot}"; break;
                    case PieceType.S: komaImg = $"gin{extentionWithDot}"; break;
                    case PieceType.G: komaImg = $"kin{extentionWithDot}"; break;
                    case PieceType.K: komaImg = $"oh{extentionWithDot}"; break;
                    case PieceType.R: komaImg = $"hi{extentionWithDot}"; break;
                    case PieceType.B: komaImg = $"kaku{extentionWithDot}"; break;
                    case PieceType.PR: komaImg = $"ryu{extentionWithDot}"; break;
                    case PieceType.PB: komaImg = $"uma{extentionWithDot}"; break;
                    case PieceType.PP: komaImg = $"tokin{extentionWithDot}"; break;
                    case PieceType.PL: komaImg = $"narikyo{extentionWithDot}"; break;
                    case PieceType.PN: komaImg = $"narikei{extentionWithDot}"; break;
                    case PieceType.PS: komaImg = $"narigin{extentionWithDot}"; break;
                    default: komaImg = $"batu{extentionWithDot}"; break;
                }
            }
            else
            {
                switch (ks14)
                {
                    case PieceType.P: komaImg = $"fuV{extentionWithDot}"; break;
                    case PieceType.L: komaImg = $"kyoV{extentionWithDot}"; break;
                    case PieceType.N: komaImg = $"keiV{extentionWithDot}"; break;
                    case PieceType.S: komaImg = $"ginV{extentionWithDot}"; break;
                    case PieceType.G: komaImg = $"kinV{extentionWithDot}"; break;
                    case PieceType.K: komaImg = $"ohV{extentionWithDot}"; break;
                    case PieceType.R: komaImg = $"hiV{extentionWithDot}"; break;
                    case PieceType.B: komaImg = $"kakuV{extentionWithDot}"; break;
                    case PieceType.PR: komaImg = $"ryuV{extentionWithDot}"; break;
                    case PieceType.PB: komaImg = $"umaV{extentionWithDot}"; break;
                    case PieceType.PP: komaImg = $"tokinV{extentionWithDot}"; break;
                    case PieceType.PL: komaImg = $"narikyoV{extentionWithDot}"; break;
                    case PieceType.PN: komaImg = $"narikeiV{extentionWithDot}"; break;
                    case PieceType.PS: komaImg = $"nariginV{extentionWithDot}"; break;
                    default: komaImg = $"batu{extentionWithDot}"; break;
                }
            }

            return komaImg;
        }


        /// <summary>
        /// 駒画像のファイル名。
        /// </summary>
        /// <param name="src_Sky"></param>
        /// <param name="finger"></param>
        /// <param name="extentionWithDot"></param>
        /// <returns></returns>
        public static string Finger_ToString(SkyConst src_Sky, Finger finger, string extentionWithDot)
        {
            string komaImg = "";

            if ((int)finger < Finger_Honshogi.Items_KomaOnly.Length)
            {
                RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(finger).Now);

                Playerside pside = koma.Pside;
                PieceType ks14 = Util_Komahaiyaku184.Syurui(koma.Haiyaku);

                komaImg = Util_Converter_LogGraphicEx.PsideKs14_ToString(pside, ks14, extentionWithDot);
            }
            else
            {
                komaImg = Util_Converter_LogGraphicEx.PsideKs14_ToString(Playerside.Empty, PieceType.None, extentionWithDot);
            }

            return komaImg;
        }


    }
}
