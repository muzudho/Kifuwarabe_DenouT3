﻿#if DEBUG
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.Kifuwarakaku.Entities.Features
{
    /// <summary>
    /// ログを取るためのもの。
    /// </summary>
    public class MmLogGenjoImpl : MmLogGenjo
    {

        public KaisetuBoard BrdMove { get; set; }

        public int Temezumi_yomiCur { get { return this.temezumi_yomiCur; } }
        private int temezumi_yomiCur;

        /// <summary>
        /// 読み開始手目済み
        /// </summary>
        public int YomikaisiTemezumi { get { return this.yomikaisiTemezumi; } }
        private int yomikaisiTemezumi;


        public IMove Move { get { return this.move; } }
        private IMove move;

        public MmLogGenjoImpl(
            int yomikaisiTemezumi,
            KaisetuBoard brdMove,
            int temezumi_yomiCur,
            IMove move
            )
        {
            this.BrdMove = brdMove;
            this.yomikaisiTemezumi = yomikaisiTemezumi;
            this.temezumi_yomiCur = temezumi_yomiCur;
            this.move = move;
        }

        public void Log1(Playerside pside_genTeban3)
        {
            this.BrdMove.Caption = $"移動可能_{ConvMove.ChangeMoveTo_KsString_ForLog(this.Move, pside_genTeban3)}";
            this.BrdMove.Temezumi = this.Temezumi_yomiCur;
            this.BrdMove.YomikaisiTemezumi = this.YomikaisiTemezumi;
            this.BrdMove.GenTeban = pside_genTeban3;// 現手番
        }

        public void Log2(
            Playerside tebanSeme,//手番（利きを調べる側）
            Playerside tebanKurau//手番（喰らう側）
        )
        {
            if (Playerside.P1 == tebanSeme)
            {
                this.BrdMove.NounaiSeme = Gkl_NounaiSeme.Sente;
            }
            else if (Playerside.P2 == tebanSeme)
            {
                this.BrdMove.NounaiSeme = Gkl_NounaiSeme.Gote;
            }
        }


        public void Log3(
            SkyConst src_Sky,
            Playerside tebanKurau,//手番（喰らう側）
            Playerside tebanSeme,//手番（利きを調べる側）
            Fingers fingers_kurau_IKUSA,//戦駒（喰らう側）
            Fingers fingers_kurau_MOTI,// 持駒（喰らう側）
            Fingers fingers_seme_IKUSA,//戦駒（利きを調べる側）
            Fingers fingers_seme_MOTI// 持駒（利きを調べる側）
        )
        {
            // 攻め手の駒の位置
            KaisetuBoard boardLog_clone = new KaisetuBoard(this.BrdMove);
            foreach (Finger finger in fingers_seme_IKUSA.Items)
            {
                RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(finger).Now);

                Gkl_KomaMasu km = new Gkl_KomaMasu(
                    Util_Converter_LogGraphicEx.PsideKs14_ToString(tebanSeme, Util_Komahaiyaku184.Syurui(koma.Haiyaku), ""),
                    Conv_SyElement.ToMasuNumber(koma.Masu)
                    );
                boardLog_clone.KomaMasu1.Add(km);
            }

            foreach (Finger finger in fingers_kurau_IKUSA.Items)
            {
                RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(finger).Now);

                this.BrdMove.KomaMasu2.Add(new Gkl_KomaMasu(
                    Util_Converter_LogGraphicEx.PsideKs14_ToString(tebanKurau, Util_Komahaiyaku184.Syurui(koma.Haiyaku), ""),
                    Conv_SyElement.ToMasuNumber(koma.Masu)
                    ));
            }

            foreach (Finger finger in fingers_seme_MOTI.Items)
            {
                RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(finger).Now);

                Gkl_KomaMasu km = new Gkl_KomaMasu(
                    Util_Converter_LogGraphicEx.PsideKs14_ToString(tebanSeme, Util_Komahaiyaku184.Syurui(koma.Haiyaku), ""),
                    Conv_SyElement.ToMasuNumber(koma.Masu)
                    );
                this.BrdMove.KomaMasu3.Add(km);
            }

            foreach (Finger finger in fingers_kurau_MOTI.Items)
            {
                RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(finger).Now);

                this.BrdMove.KomaMasu4.Add(new Gkl_KomaMasu(
                    Util_Converter_LogGraphicEx.PsideKs14_ToString(tebanKurau, Util_Komahaiyaku184.Syurui(koma.Haiyaku), ""),
                    Conv_SyElement.ToMasuNumber(koma.Masu)
                    ));
            }
            this.BrdMove = boardLog_clone;
        }

        public void Log4(
            SkyConst src_Sky,
            Playerside tebanSeme,//手番（利きを調べる側）
            Maps_OneAndOne<Finger, SySet<SyElement>> kmMove_seme_IKUSA
        )
        {
            // 戦駒の移動可能場所
            KaisetuBoard boardLog_clone = new KaisetuBoard(this.BrdMove);
            kmMove_seme_IKUSA.Foreach_Entry((Finger key, SySet<SyElement> value, ref bool toBreak) =>
            {
                RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(key).Now);

                string komaImg = Util_Converter_LogGraphicEx.PsideKs14_ToString(tebanSeme, Util_Komahaiyaku184.Syurui(koma.Haiyaku), "");

                foreach (New_Basho masu in value.Elements)
                {
                    boardLog_clone.Masu_theMove.Add((int)masu.MasuNumber);
                }
            });

            this.BrdMove = boardLog_clone;
        }

    }
}
#endif