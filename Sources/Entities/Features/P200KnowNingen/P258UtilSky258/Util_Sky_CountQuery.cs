using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.Kifuwarakaku.Entities.Features
{

    /// <summary>
    /// 棋譜ノードのユーティリティー。
    /// </summary>
    public abstract class Util_Sky_CountQuery
    {


        /// <summary>
        /// 持ち駒を数えます。
        /// </summary>
        /// <param name="src_Sky"></param>
        /// <param name="mK"></param>
        /// <param name="mR"></param>
        /// <param name="mB"></param>
        /// <param name="mG"></param>
        /// <param name="mS"></param>
        /// <param name="mN"></param>
        /// <param name="mL"></param>
        /// <param name="mP"></param>
        /// <param name="mk"></param>
        /// <param name="mr"></param>
        /// <param name="mb"></param>
        /// <param name="mg"></param>
        /// <param name="ms"></param>
        /// <param name="mn"></param>
        /// <param name="ml"></param>
        /// <param name="mp"></param>
        /// <param name="logTag"></param>
        public static void CountMoti(
            SkyConst src_Sky,
            out int mK,
            out int mR,
            out int mB,
            out int mG,
            out int mS,
            out int mN,
            out int mL,
            out int mP,

            out int mk,
            out int mr,
            out int mb,
            out int mg,
            out int ms,
            out int mn,
            out int ml,
            out int mp
        )
        {
            mK = 0;
            mR = 0;
            mB = 0;
            mG = 0;
            mS = 0;
            mN = 0;
            mL = 0;
            mP = 0;

            mk = 0;
            mr = 0;
            mb = 0;
            mg = 0;
            ms = 0;
            mn = 0;
            ml = 0;
            mp = 0;

            Fingers komas_moti1p;// 先手の持駒
            Fingers komas_moti2p;// 後手の持駒
            Util_Sky_FingersQueryFx.Split_Moti1p_Moti2p(out komas_moti1p, out komas_moti2p, src_Sky);

            foreach (Finger figKoma in komas_moti1p.Items)
            {
                RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(figKoma).Now);

                PieceType syurui = Util_Komasyurui14.NarazuCaseHandle(Util_Komahaiyaku184.Syurui(koma.Haiyaku));
                if (PieceType.K == syurui)
                {
                    mK++;
                }
                else if (PieceType.R == syurui)
                {
                    mR++;
                }
                else if (PieceType.B == syurui)
                {
                    mB++;
                }
                else if (PieceType.G == syurui)
                {
                    mG++;
                }
                else if (PieceType.S == syurui)
                {
                    mS++;
                }
                else if (PieceType.N == syurui)
                {
                    mN++;
                }
                else if (PieceType.L == syurui)
                {
                    mL++;
                }
                else if (PieceType.P == syurui)
                {
                    mP++;
                }
                else
                {
                }
            }

            // 後手の持駒
            foreach (Finger figKoma in komas_moti2p.Items)
            {
                RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf((int)figKoma).Now);

                PieceType syurui = Util_Komasyurui14.NarazuCaseHandle(Util_Komahaiyaku184.Syurui(koma.Haiyaku));

                if (PieceType.K == syurui)
                {
                    mk++;
                }
                else if (PieceType.R == syurui)
                {
                    mr++;
                }
                else if (PieceType.B == syurui)
                {
                    mb++;
                }
                else if (PieceType.G == syurui)
                {
                    mg++;
                }
                else if (PieceType.S == syurui)
                {
                    ms++;
                }
                else if (PieceType.N == syurui)
                {
                    mn++;
                }
                else if (PieceType.L == syurui)
                {
                    ml++;
                }
                else if (PieceType.P == syurui)
                {
                    mp++;
                }
                else
                {
                }
            }

        }




    }
}
