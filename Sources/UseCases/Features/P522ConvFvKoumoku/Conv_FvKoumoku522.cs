using System;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.UseCases.Features;

namespace Grayscale.Kifuwarakaku.UseCases.Features
{
    public abstract class Conv_FvKoumoku522
    {

        /// <summary>
        /// 添え字変換機。KK表→PP表
        /// </summary>
        /// <param name="player1dan"></param>
        /// <param name="player2dan"></param>
        /// <param name="player1suji"></param>
        /// <param name="player2suji"></param>
        public static void Converter_KK_to_PP(int player1dan, int player2dan, int player1suji, int player2suji, out int p1, out int p2)
        {
            p1 = Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.Ban_Oh__ + Square.ToMasu(player1suji, player1dan);
            p2 = Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.Ban_Oh__ + Square.ToMasu(player2suji, player2dan);
        }

        /// <summary>
        /// 添え字変換機。KP→PP表
        /// </summary>
        public static void Converter_K1_to_P(Playerside pside, int kingDan, int kingSuji, out int p)
        {
            switch (pside)
            {
                case Playerside.P1: p = Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.Ban_Oh__ + Square.ToMasu(kingSuji, kingDan); break;
                case Playerside.P2: p = Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.Ban_Oh__ + Square.ToMasu(kingSuji, kingDan); break;
                default: throw new Exception("項目P番号を探している途中でしたが、未登録のプレイヤー番号でした。");
            }
        }


    }
}
