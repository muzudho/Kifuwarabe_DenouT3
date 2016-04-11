using Grayscale.P035_Collection_.L500____Struct;
using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P211_WordShogi__.L___250_Masu;
using Grayscale.P211_WordShogi__.L250____Masu;
using System.Collections.Generic;
using System.Text;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P246_MasusWriter.L250____Writer
{

    public abstract class ShogibanTermDisplay
    {


        public static string Kamd_ToTerm(Maps_OneAndOne<Finger, SySet<SyElement>> kamd)
        {
            StringBuilder sb = new StringBuilder();
            kamd.Foreach_Entry((Finger key, SySet<SyElement> value, ref bool toBreak) =>
            {
                sb.AppendLine("entry.Key=" + key);
                sb.AppendLine( ShogibanTermDisplay.Masus_ToTerm(value) );
                sb.AppendLine("  ");//空行
            });

            return sb.ToString();
        }


        public static string Masus_ToTerm(SySet<SyElement> masus)
        {
            // masus に、升が登場した回数。
            Dictionary<int, int> kaisu = new Dictionary<int, int>();
            foreach (New_Basho masu in masus.Elements)
            {
                if (kaisu.ContainsKey(masu.MasuNumber))
                {
                    kaisu[masu.MasuNumber] += 1;
                }
                else
                {
                    kaisu.Add(masu.MasuNumber, 1);
                }
            }

            // 表示テキスト
            Dictionary<int, string> hyoji = new Dictionary<int, string>();
            for (int masuNumber = Masu_Honshogi.nban11_１一; masuNumber <= Masu_Honshogi.nban99_９九; masuNumber++ )
            {
                if (kaisu.ContainsKey(masuNumber))
                {
                    hyoji[masuNumber] = kaisu[masuNumber].ToString().PadLeft(2);
                }
                else
                {
                    // データがなければ空マス。
                    hyoji.Add(masuNumber, "  ");
                }
            }


            // 次のような表を作ります。
            //
            // [ 0] 後手持ち駒           ９  ８  ７  ６  ５  ４  ３  ２  １     先手持ち駒         駒袋
            // [ 1] ┌─┬─┬─┬─┐ ┌─┬─┬─┬─┬─┬─┬─┬─┬─┐   ┌─┬─┬─┬─┐ ┌─┬─┬─┬─┐
            // [ 2] │  │  │  │  │ │  │  │  │  │  │  │  │  │  │一 │  │  │  │  │ │  │  │  │  │
            // [ 3] ├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤
            // [ 4] │  │  │  │  │ │  │  │  │  │  │  │  │  │  │二 │  │  │  │  │ │  │  │  │  │
            // [ 5] ├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤
            // [ 6] │  │  │  │  │ │  │  │  │  │  │  │  │  │  │三 │  │  │  │  │ │  │  │  │  │
            // [ 7] ├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤
            // [ 8] │  │  │  │  │ │  │  │  │  │  │  │  │  │  │四 │  │  │  │  │ │  │  │  │  │
            // [ 9] ├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤
            // [10] │  │  │  │  │ │  │  │  │  │  │  │  │  │  │五 │  │  │  │  │ │  │  │  │  │
            // [11] ├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤
            // [12] │  │  │  │  │ │  │  │  │  │  │  │  │  │  │六 │  │  │  │  │ │  │  │  │  │
            // [13] ├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤
            // [14] │  │  │  │  │ │  │  │  │  │  │  │  │  │  │七 │  │  │  │  │ │  │  │  │  │
            // [15] ├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤
            // [16] │  │  │  │  │ │  │  │  │  │  │  │  │  │  │八 │  │  │  │  │ │  │  │  │  │
            // [17] ├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤
            // [18] │  │  │  │  │ │  │  │  │  │  │  │  │  │  │九 │  │  │  │  │ │  │  │  │  │
            // [19] └─┴─┴─┴─┘ └─┴─┴─┴─┴─┴─┴─┴─┴─┘   └─┴─┴─┴─┘ └─┴─┴─┴─┘
            // [20] エラー:


            StringBuilder sb = new StringBuilder();
            sb.AppendLine("kaisu.Count=" + kaisu.Count);
            sb.AppendLine("hyoji.Count=" + hyoji.Count);
            sb.AppendLine("数字は、その升が選ばれている回数。");
            sb.AppendLine("後手持ち駒           ９  ８  ７  ６  ５  ４  ３  ２  １     先手持ち駒         駒袋");
            sb.AppendLine("┌─┬─┬─┬─┐ ┌─┬─┬─┬─┬─┬─┬─┬─┬─┐   ┌─┬─┬─┬─┐ ┌─┬─┬─┬─┐");
            sb.AppendLine("│" + hyoji[Masu_Honshogi.ngo31] + "│" + hyoji[Masu_Honshogi.ngo21] + "│" + hyoji[Masu_Honshogi.ngo11] + "│" + hyoji[Masu_Honshogi.ngo01] + "│ │" + hyoji[Masu_Honshogi.nban11_１一 + 72] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 63] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 54] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 45] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 36] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 27] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 18] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 9] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 0] + "│一 │" + hyoji[Masu_Honshogi.nsen31] + "│" + hyoji[Masu_Honshogi.nsen21] + "│" + hyoji[Masu_Honshogi.nsen11] + "│" + hyoji[Masu_Honshogi.nsen01] + "│ │" + hyoji[Masu_Honshogi.nfukuro31] + "│" + hyoji[Masu_Honshogi.nfukuro21] + "│" + hyoji[Masu_Honshogi.nfukuro11] + "│" + hyoji[Masu_Honshogi.nfukuro01] + "│");
            sb.AppendLine("├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤");
            sb.AppendLine("│" + hyoji[Masu_Honshogi.ngo32] + "│" + hyoji[Masu_Honshogi.ngo22] + "│" + hyoji[Masu_Honshogi.ngo12] + "│" + hyoji[Masu_Honshogi.ngo02] + "│ │" + hyoji[Masu_Honshogi.nban11_１一 + 73] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 64] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 55] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 46] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 37] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 28] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 19] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 10] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 1] + "│二 │" + hyoji[Masu_Honshogi.nsen32] + "│" + hyoji[Masu_Honshogi.nsen22] + "│" + hyoji[Masu_Honshogi.nsen12] + "│" + hyoji[Masu_Honshogi.nsen02] + "│ │" + hyoji[Masu_Honshogi.nfukuro32] + "│" + hyoji[Masu_Honshogi.nfukuro22] + "│" + hyoji[Masu_Honshogi.nfukuro12] + "│" + hyoji[Masu_Honshogi.nfukuro02] + "│");
            sb.AppendLine("├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤");
            sb.AppendLine("│" + hyoji[Masu_Honshogi.ngo33] + "│" + hyoji[Masu_Honshogi.ngo23] + "│" + hyoji[Masu_Honshogi.ngo13] + "│" + hyoji[Masu_Honshogi.ngo03] + "│ │" + hyoji[Masu_Honshogi.nban11_１一 + 74] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 65] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 56] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 47] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 38] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 29] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 20] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 11] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 2] + "│三 │" + hyoji[Masu_Honshogi.nsen33] + "│" + hyoji[Masu_Honshogi.nsen23] + "│" + hyoji[Masu_Honshogi.nsen13] + "│" + hyoji[Masu_Honshogi.nsen03] + "│ │" + hyoji[Masu_Honshogi.nfukuro33] + "│" + hyoji[Masu_Honshogi.nfukuro23] + "│" + hyoji[Masu_Honshogi.nfukuro13] + "│" + hyoji[Masu_Honshogi.nfukuro03] + "│");
            sb.AppendLine("├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤");
            sb.AppendLine("│" + hyoji[Masu_Honshogi.ngo34] + "│" + hyoji[Masu_Honshogi.ngo24] + "│" + hyoji[Masu_Honshogi.ngo14] + "│" + hyoji[Masu_Honshogi.ngo04] + "│ │" + hyoji[Masu_Honshogi.nban11_１一 + 75] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 66] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 57] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 48] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 39] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 30] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 21] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 12] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 3] + "│四 │" + hyoji[Masu_Honshogi.nsen34] + "│" + hyoji[Masu_Honshogi.nsen24] + "│" + hyoji[Masu_Honshogi.nsen14] + "│" + hyoji[Masu_Honshogi.nsen04] + "│ │" + hyoji[Masu_Honshogi.nfukuro34] + "│" + hyoji[Masu_Honshogi.nfukuro24] + "│" + hyoji[Masu_Honshogi.nfukuro14] + "│" + hyoji[Masu_Honshogi.nfukuro04] + "│");
            sb.AppendLine("├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤");
            sb.AppendLine("│" + hyoji[Masu_Honshogi.ngo35] + "│" + hyoji[Masu_Honshogi.ngo25] + "│" + hyoji[Masu_Honshogi.ngo15] + "│" + hyoji[Masu_Honshogi.ngo05] + "│ │" + hyoji[Masu_Honshogi.nban11_１一 + 76] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 67] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 58] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 49] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 40] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 31] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 22] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 13] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 4] + "│五 │" + hyoji[Masu_Honshogi.nsen35] + "│" + hyoji[Masu_Honshogi.nsen25] + "│" + hyoji[Masu_Honshogi.nsen15] + "│" + hyoji[Masu_Honshogi.nsen05] + "│ │" + hyoji[Masu_Honshogi.nfukuro35] + "│" + hyoji[Masu_Honshogi.nfukuro25] + "│" + hyoji[Masu_Honshogi.nfukuro15] + "│" + hyoji[Masu_Honshogi.nfukuro05] + "│");
            sb.AppendLine("├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤");
            sb.AppendLine("│" + hyoji[Masu_Honshogi.ngo36] + "│" + hyoji[Masu_Honshogi.ngo26] + "│" + hyoji[Masu_Honshogi.ngo16] + "│" + hyoji[Masu_Honshogi.ngo06] + "│ │" + hyoji[Masu_Honshogi.nban11_１一 + 77] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 68] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 59] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 50] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 41] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 32] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 23] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 14] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 5] + "│六 │" + hyoji[Masu_Honshogi.nsen36] + "│" + hyoji[Masu_Honshogi.nsen26] + "│" + hyoji[Masu_Honshogi.nsen16] + "│" + hyoji[Masu_Honshogi.nsen06] + "│ │" + hyoji[Masu_Honshogi.nfukuro36] + "│" + hyoji[Masu_Honshogi.nfukuro26] + "│" + hyoji[Masu_Honshogi.nfukuro16] + "│" + hyoji[Masu_Honshogi.nfukuro06] + "│");
            sb.AppendLine("├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤");
            sb.AppendLine("│" + hyoji[Masu_Honshogi.ngo37] + "│" + hyoji[Masu_Honshogi.ngo27] + "│" + hyoji[Masu_Honshogi.ngo17] + "│" + hyoji[Masu_Honshogi.ngo07] + "│ │" + hyoji[Masu_Honshogi.nban11_１一 + 78] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 69] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 60] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 51] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 42] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 33] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 24] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 15] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 6] + "│七 │" + hyoji[Masu_Honshogi.nsen37] + "│" + hyoji[Masu_Honshogi.nsen27] + "│" + hyoji[Masu_Honshogi.nsen17] + "│" + hyoji[Masu_Honshogi.nsen07] + "│ │" + hyoji[Masu_Honshogi.nfukuro37] + "│" + hyoji[Masu_Honshogi.nfukuro27] + "│" + hyoji[Masu_Honshogi.nfukuro17] + "│" + hyoji[Masu_Honshogi.nfukuro07] + "│");
            sb.AppendLine("├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤");
            sb.AppendLine("│" + hyoji[Masu_Honshogi.ngo38] + "│" + hyoji[Masu_Honshogi.ngo28] + "│" + hyoji[Masu_Honshogi.ngo18] + "│" + hyoji[Masu_Honshogi.ngo08] + "│ │" + hyoji[Masu_Honshogi.nban11_１一 + 79] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 70] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 61] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 52] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 43] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 34] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 25] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 16] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 7] + "│八 │" + hyoji[Masu_Honshogi.nsen38] + "│" + hyoji[Masu_Honshogi.nsen28] + "│" + hyoji[Masu_Honshogi.nsen18] + "│" + hyoji[Masu_Honshogi.nsen08] + "│ │" + hyoji[Masu_Honshogi.nfukuro38] + "│" + hyoji[Masu_Honshogi.nfukuro28] + "│" + hyoji[Masu_Honshogi.nfukuro18] + "│" + hyoji[Masu_Honshogi.nfukuro08] + "│");
            sb.AppendLine("├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤");
            sb.AppendLine("│" + hyoji[Masu_Honshogi.ngo39] + "│" + hyoji[Masu_Honshogi.ngo29] + "│" + hyoji[Masu_Honshogi.ngo19] + "│" + hyoji[Masu_Honshogi.ngo09] + "│ │" + hyoji[Masu_Honshogi.nban11_１一 + 80] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 71] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 62] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 53] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 44] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 35] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 26] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 17] + "│" + hyoji[Masu_Honshogi.nban11_１一 + 8] + "│九 │" + hyoji[Masu_Honshogi.nsen39] + "│" + hyoji[Masu_Honshogi.nsen29] + "│" + hyoji[Masu_Honshogi.nsen19] + "│" + hyoji[Masu_Honshogi.nsen09] + "│ │" + hyoji[Masu_Honshogi.nfukuro39] + "│" + hyoji[Masu_Honshogi.nfukuro29] + "│" + hyoji[Masu_Honshogi.nfukuro19] + "│" + hyoji[Masu_Honshogi.nfukuro09] + "│");
            sb.AppendLine("├─┼─┼─┼─┤ └─┴─┴─┴─┴─┴─┴─┴─┴─┘   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤");
            sb.AppendLine("│" + hyoji[Masu_Honshogi.ngo40] + "│" + hyoji[Masu_Honshogi.ngo30] + "│" + hyoji[Masu_Honshogi.ngo20] + "│" + hyoji[Masu_Honshogi.ngo10] + "│                                          │" + hyoji[Masu_Honshogi.nsen40] + "│" + hyoji[Masu_Honshogi.nsen30] + "│" + hyoji[Masu_Honshogi.nsen20] + "│" + hyoji[Masu_Honshogi.nsen10] + "│ │" + hyoji[Masu_Honshogi.nfukuro40] + "│" + hyoji[Masu_Honshogi.nfukuro30] + "│" + hyoji[Masu_Honshogi.nfukuro20] + "│" + hyoji[Masu_Honshogi.nfukuro10] + "│");
            sb.AppendLine("└─┴─┴─┴─┘                                          └─┴─┴─┴─┘ └─┴─┴─┴─┘");
            sb.AppendLine("エラー:");

            return sb.ToString();
        }


        //public static string[] Masus_ToTerm(Masus<SyElement> masus)
        //{
        //    List<M201> masuList = masus.Elements.ToList();


        //    // 次のような表を作ります。
        //    //
        //    // [ 0] 後手持ち駒           ９  ８  ７  ６  ５  ４  ３  ２  １     先手持ち駒         駒袋
        //    // [ 1] ┌─┬─┬─┬─┐ ┌─┬─┬─┬─┬─┬─┬─┬─┬─┐   ┌─┬─┬─┬─┐ ┌─┬─┬─┬─┐
        //    // [ 2] │  │  │  │  │ │  │  │  │  │  │  │  │  │  │一 │  │  │  │  │ │  │  │  │  │
        //    // [ 3] ├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤
        //    // [ 4] │  │  │  │  │ │  │  │  │  │  │  │  │  │  │二 │  │  │  │  │ │  │  │  │  │
        //    // [ 5] ├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤
        //    // [ 6] │  │  │  │  │ │  │  │  │  │  │  │  │  │  │三 │  │  │  │  │ │  │  │  │  │
        //    // [ 7] ├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤
        //    // [ 8] │  │  │  │  │ │  │  │  │  │  │  │  │  │  │四 │  │  │  │  │ │  │  │  │  │
        //    // [ 9] ├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤
        //    // [10] │  │  │  │  │ │  │  │  │  │  │  │  │  │  │五 │  │  │  │  │ │  │  │  │  │
        //    // [11] ├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤
        //    // [12] │  │  │  │  │ │  │  │  │  │  │  │  │  │  │六 │  │  │  │  │ │  │  │  │  │
        //    // [13] ├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤
        //    // [14] │  │  │  │  │ │  │  │  │  │  │  │  │  │  │七 │  │  │  │  │ │  │  │  │  │
        //    // [15] ├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤
        //    // [16] │  │  │  │  │ │  │  │  │  │  │  │  │  │  │八 │  │  │  │  │ │  │  │  │  │
        //    // [17] ├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤
        //    // [18] │  │  │  │  │ │  │  │  │  │  │  │  │  │  │九 │  │  │  │  │ │  │  │  │  │
        //    // [19] └─┴─┴─┴─┘ └─┴─┴─┴─┴─┴─┴─┴─┴─┘   └─┴─┴─┴─┘ └─┴─┴─┴─┘
        //    // [20] エラー:



        //    string[] stringArray = new string[]{
        //        "後手持ち駒           ９  ８  ７  ６  ５  ４  ３  ２  １     先手持ち駒         駒袋",//[ 0]
        //        "┌─┬─┬─┬─┐ ┌─┬─┬─┬─┬─┬─┬─┬─┬─┐   ┌─┬─┬─┬─┐ ┌─┬─┬─┬─┐",//[ 1]
        //        "│  │  │  │  │ │  │  │  │  │  │  │  │  │  │一 │  │  │  │  │ │  │  │  │  │",//[ 2]
        //        "├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤",//[ 3]
        //        "│  │  │  │  │ │  │  │  │  │  │  │  │  │  │二 │  │  │  │  │ │  │  │  │  │",//[ 4]
        //        "├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤",//[ 5]
        //        "│  │  │  │  │ │  │  │  │  │  │  │  │  │  │三 │  │  │  │  │ │  │  │  │  │",//[ 6]
        //        "├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤",//[ 7]
        //        "│  │  │  │  │ │  │  │  │  │  │  │  │  │  │四 │  │  │  │  │ │  │  │  │  │",//[ 8]
        //        "├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤",//[ 9]
        //        "│  │  │  │  │ │  │  │  │  │  │  │  │  │  │五 │  │  │  │  │ │  │  │  │  │",//[10]
        //        "├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤",//[11]
        //        "│  │  │  │  │ │  │  │  │  │  │  │  │  │  │六 │  │  │  │  │ │  │  │  │  │",//[12]
        //        "├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤",//[13]
        //        "│  │  │  │  │ │  │  │  │  │  │  │  │  │  │七 │  │  │  │  │ │  │  │  │  │",//[14]
        //        "├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤",//[15]
        //        "│  │  │  │  │ │  │  │  │  │  │  │  │  │  │八 │  │  │  │  │ │  │  │  │  │",//[16]
        //        "├─┼─┼─┼─┤ ├─┼─┼─┼─┼─┼─┼─┼─┼─┤   ├─┼─┼─┼─┤ ├─┼─┼─┼─┤",//[17]
        //        "│  │  │  │  │ │  │  │  │  │  │  │  │  │  │九 │  │  │  │  │ │  │  │  │  │",//[18]
        //        "└─┴─┴─┴─┘ └─┴─┴─┴─┴─┴─┴─┴─┴─┘   └─┴─┴─┴─┘ └─┴─┴─┴─┘",//[19]
        //        "エラー:",//[20]
        //    };

        //    return stringArray;
        //}

    }

}
