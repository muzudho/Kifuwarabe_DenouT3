namespace Grayscale.Kifuwarakaku.GuiOfCsharp.Features
{
    using System.Collections.Generic;
    using Grayscale.Kifuwarakaku.Entities.Features;
    using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

    public abstract class Util_Menace
    {
        /// <summary>
        /// v(^▽^)v超能力『メナス』だぜ☆ 未来の脅威を予測し、可視化するぜ☆ｗｗｗ
        /// </summary>
        public static void Menace(MainGui_Csharp mainGui)
        {
            if (0 < mainGui.Model_Manual.GuiTemezumi)
            {
                // 処理の順序が悪く、初回はうまく判定できない。
                SkyConst src_Sky = mainGui.Model_Manual.GuiSkyConst;


                //----------
                // 将棋盤上の駒
                //----------
                mainGui.RepaintRequest.SetFlag_RefreshRequest();

                // [クリアー]
                mainGui.Shape_PnlTaikyoku.Shogiban.ClearHMasu_KikiKomaList();

                // 全駒
                foreach (Finger figKoma in Finger_Honshogi.Items_KomaOnly)
                {
                    RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(figKoma).Now);


                    if (
                        Okiba.ShogiBan == Conv_SyElement.ToOkiba(koma.Masu)
                        &&
                        mainGui.Model_Manual.GuiPside != koma.Pside
                        )
                    {
                        // 駒の利き
                        SySet<SyElement> kikiZukei = Util_Sky_SyugoQuery.KomaKidou_Potential(figKoma, src_Sky);

                        IEnumerable<SyElement> kikiMasuList = kikiZukei.Elements;
                        foreach (SyElement masu in kikiMasuList)
                        {
                            // その枡に利いている駒のハンドルを追加
                            if (!Masu_Honshogi.IsErrorBasho(masu))
                            {
                                mainGui.Shape_PnlTaikyoku.Shogiban.HMasu_KikiKomaList[Conv_SyElement.ToMasuNumber(masu)].Add((int)figKoma);
                            }
                        }
                    }
                }
            }
        }

    }
}
