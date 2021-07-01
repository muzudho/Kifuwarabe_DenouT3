using System.Diagnostics;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.Kifuwarakaku.Entities.Features
{
    public static class Util_Sky258A
    {

        public static readonly IMove RootMove = new RO_Starbeam(
            new RO_Star(Playerside.Empty, Masu_Honshogi.Query_Basho(Masu_Honshogi.nError), PieceType.None),
            new RO_Star(Playerside.Empty, Masu_Honshogi.Query_Basho(Masu_Honshogi.nError), PieceType.None),
            null
            );

        public static readonly IMove NullObjectMove = new RO_Starbeam(
            new RO_Star(Playerside.Empty, Masu_Honshogi.Query_Basho(Masu_Honshogi.nError), PieceType.None),
            new RO_Star(Playerside.Empty, Masu_Honshogi.Query_Basho(Masu_Honshogi.nError), PieceType.None),
            null
            );





        /// <summary>
        /// 成ケース
        /// </summary>
        /// <returns></returns>
        public static PieceType ToNariCase(RO_Starlight ms)
        {
            PieceType result;

            RO_Star koma = Util_Starlightable.AsKoma(ms.Now);

            result = Util_Komasyurui14.NariCaseHandle[(int)Util_Komahaiyaku184.Syurui(koma.Haiyaku)];

            return result;
        }

        /// <summary>
        /// 外字を利用した、デバッグ用の駒の名前１文字だぜ☆
        /// </summary>
        /// <returns></returns>
        public static char ToGaiji(RO_Starlight ms)
        {
            char result;

            RO_Star koma = Util_Starlightable.AsKoma(ms.Now);

            result = Util_Komasyurui14.ToGaiji(Util_Komahaiyaku184.Syurui(koma.Haiyaku), koma.Pside);

            return result;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 元位置。
        /// ************************************************************************************************************************
        /// </summary>
        /// <returns></returns>
        public static IMove Src(IMove move)
        {
            RO_Starbeam result;


            RO_Star srcKoma = Util_Starlightable.AsKoma(move.LongTimeAgo);
            RO_Star dstKoma = Util_Starlightable.AsKoma(move.Now);


            result = new RO_Starbeam(

                new RO_Star(
                    dstKoma.Pside,
                    Masu_Honshogi.Query_Basho(Masu_Honshogi.nError), // ソースのソースは未定義。
                    PieceType.None
                ),

                // ソースの目的地はソース
                new RO_Star(
                    dstKoma.Pside,
                    srcKoma.Masu,
                    srcKoma.Komasyurui
                ),

                PieceType.None
            );

            return result;
        }

        public static RO_Starbeam BuildMove(
            IMoveSource longTimeAgo,
            IMoveSource now,
            PieceType tottaKomaSyurui
        )
        {
            return new RO_Starbeam(longTimeAgo, now, tottaKomaSyurui);
        }

        public static void Assert_Honshogi(SkyConst src_Sky)
        {
            Debug.Assert(src_Sky.Count == 40, $"siteiSky.Starlights.Count=[{src_Sky.Count}]");//将棋の駒の数

            ////デバッグ
            //{
            //    StringBuilder sb = new StringBuilder();

            //    for (int i = 0; i < 40; i++)
            //    {
            //        sb.Append("駒{i}.種類=[{((RO_Star_KomaKs)siteiSky.StarlightIndexOf(i).Now).Syurui}]\n");
            //    }

            //    MessageBox.Show(sb.ToString());
            //}


            // 王
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(0).Now).Komasyurui == PieceType.K, $"駒0.種類=[{((RO_Star)src_Sky.StarlightIndexOf(0).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(1).Now).Komasyurui == PieceType.K, $"駒1.種類=[{((RO_Star)src_Sky.StarlightIndexOf(1).Now).Komasyurui}]");

            // 飛車
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(2).Now).Komasyurui == PieceType.R || ((RO_Star)src_Sky.StarlightIndexOf(2).Now).Komasyurui == PieceType.PR, $"駒2.種類=[{((RO_Star)src_Sky.StarlightIndexOf(2).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(3).Now).Komasyurui == PieceType.R || ((RO_Star)src_Sky.StarlightIndexOf(3).Now).Komasyurui == PieceType.PR, $"駒3.種類=[{((RO_Star)src_Sky.StarlightIndexOf(3).Now).Komasyurui}]");

            // 角
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(4).Now).Komasyurui == PieceType.B || ((RO_Star)src_Sky.StarlightIndexOf(4).Now).Komasyurui == PieceType.PB, $"駒4.種類=[{((RO_Star)src_Sky.StarlightIndexOf(4).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(5).Now).Komasyurui == PieceType.B || ((RO_Star)src_Sky.StarlightIndexOf(5).Now).Komasyurui == PieceType.PB, $"駒5.種類=[{((RO_Star)src_Sky.StarlightIndexOf(5).Now).Komasyurui}]");

            // 金
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(6).Now).Komasyurui == PieceType.G, $"駒6.種類=[{((RO_Star)src_Sky.StarlightIndexOf(6).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(7).Now).Komasyurui == PieceType.G, $"駒7.種類=[{((RO_Star)src_Sky.StarlightIndexOf(7).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(8).Now).Komasyurui == PieceType.G, $"駒8.種類=[{((RO_Star)src_Sky.StarlightIndexOf(8).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(9).Now).Komasyurui == PieceType.G, $"駒9.種類=[{((RO_Star)src_Sky.StarlightIndexOf(9).Now).Komasyurui}]");

            // 銀
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(10).Now).Komasyurui == PieceType.S || ((RO_Star)src_Sky.StarlightIndexOf(10).Now).Komasyurui == PieceType.PS, $"駒10.種類=[{((RO_Star)src_Sky.StarlightIndexOf(10).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(11).Now).Komasyurui == PieceType.S || ((RO_Star)src_Sky.StarlightIndexOf(11).Now).Komasyurui == PieceType.PS, $"駒11.種類=[{((RO_Star)src_Sky.StarlightIndexOf(11).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(12).Now).Komasyurui == PieceType.S || ((RO_Star)src_Sky.StarlightIndexOf(12).Now).Komasyurui == PieceType.PS, $"駒12.種類=[{((RO_Star)src_Sky.StarlightIndexOf(12).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(13).Now).Komasyurui == PieceType.S || ((RO_Star)src_Sky.StarlightIndexOf(13).Now).Komasyurui == PieceType.PS, $"駒13.種類=[{((RO_Star)src_Sky.StarlightIndexOf(13).Now).Komasyurui}]");

            // 桂
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(14).Now).Komasyurui == PieceType.N || ((RO_Star)src_Sky.StarlightIndexOf(14).Now).Komasyurui == PieceType.PN, $"駒14.種類=[{((RO_Star)src_Sky.StarlightIndexOf(14).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(15).Now).Komasyurui == PieceType.N || ((RO_Star)src_Sky.StarlightIndexOf(15).Now).Komasyurui == PieceType.PN, $"駒15.種類=[{((RO_Star)src_Sky.StarlightIndexOf(15).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(16).Now).Komasyurui == PieceType.N || ((RO_Star)src_Sky.StarlightIndexOf(16).Now).Komasyurui == PieceType.PN, $"駒16.種類=[{((RO_Star)src_Sky.StarlightIndexOf(16).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(17).Now).Komasyurui == PieceType.N || ((RO_Star)src_Sky.StarlightIndexOf(17).Now).Komasyurui == PieceType.PN, $"駒17.種類=[{((RO_Star)src_Sky.StarlightIndexOf(17).Now).Komasyurui}]");

            // 香
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(18).Now).Komasyurui == PieceType.L || ((RO_Star)src_Sky.StarlightIndexOf(18).Now).Komasyurui == PieceType.PL, $"駒18.種類=[{((RO_Star)src_Sky.StarlightIndexOf(18).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(19).Now).Komasyurui == PieceType.L || ((RO_Star)src_Sky.StarlightIndexOf(19).Now).Komasyurui == PieceType.PL, $"駒19.種類=[{((RO_Star)src_Sky.StarlightIndexOf(19).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(20).Now).Komasyurui == PieceType.L || ((RO_Star)src_Sky.StarlightIndexOf(20).Now).Komasyurui == PieceType.PL, $"駒20.種類=[{((RO_Star)src_Sky.StarlightIndexOf(20).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(21).Now).Komasyurui == PieceType.L || ((RO_Star)src_Sky.StarlightIndexOf(21).Now).Komasyurui == PieceType.PL, $"駒21.種類=[{((RO_Star)src_Sky.StarlightIndexOf(21).Now).Komasyurui}]");

            // 歩
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(22).Now).Komasyurui == PieceType.P || ((RO_Star)src_Sky.StarlightIndexOf(22).Now).Komasyurui == PieceType.PP, "駒22.種類=[{((RO_Star)src_Sky.StarlightIndexOf(22).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(23).Now).Komasyurui == PieceType.P || ((RO_Star)src_Sky.StarlightIndexOf(23).Now).Komasyurui == PieceType.PP, "駒23.種類=[{((RO_Star)src_Sky.StarlightIndexOf(23).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(24).Now).Komasyurui == PieceType.P || ((RO_Star)src_Sky.StarlightIndexOf(24).Now).Komasyurui == PieceType.PP, "駒24.種類=[{((RO_Star)src_Sky.StarlightIndexOf(24).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(25).Now).Komasyurui == PieceType.P || ((RO_Star)src_Sky.StarlightIndexOf(25).Now).Komasyurui == PieceType.PP, "駒25.種類=[{((RO_Star)src_Sky.StarlightIndexOf(25).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(26).Now).Komasyurui == PieceType.P || ((RO_Star)src_Sky.StarlightIndexOf(26).Now).Komasyurui == PieceType.PP, "駒26.種類=[{((RO_Star)src_Sky.StarlightIndexOf(26).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(27).Now).Komasyurui == PieceType.P || ((RO_Star)src_Sky.StarlightIndexOf(27).Now).Komasyurui == PieceType.PP, "駒27.種類=[{((RO_Star)src_Sky.StarlightIndexOf(27).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(28).Now).Komasyurui == PieceType.P || ((RO_Star)src_Sky.StarlightIndexOf(28).Now).Komasyurui == PieceType.PP, "駒28.種類=[{((RO_Star)src_Sky.StarlightIndexOf(28).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(29).Now).Komasyurui == PieceType.P || ((RO_Star)src_Sky.StarlightIndexOf(29).Now).Komasyurui == PieceType.PP, "駒29.種類=[{((RO_Star)src_Sky.StarlightIndexOf(29).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(30).Now).Komasyurui == PieceType.P || ((RO_Star)src_Sky.StarlightIndexOf(30).Now).Komasyurui == PieceType.PP, "駒30.種類=[{((RO_Star)src_Sky.StarlightIndexOf(30).Now).Komasyurui}]");

            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(31).Now).Komasyurui == PieceType.P || ((RO_Star)src_Sky.StarlightIndexOf(31).Now).Komasyurui == PieceType.PP, "駒31.種類=[{((RO_Star)src_Sky.StarlightIndexOf(31).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(32).Now).Komasyurui == PieceType.P || ((RO_Star)src_Sky.StarlightIndexOf(32).Now).Komasyurui == PieceType.PP, "駒32.種類=[{((RO_Star)src_Sky.StarlightIndexOf(32).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(33).Now).Komasyurui == PieceType.P || ((RO_Star)src_Sky.StarlightIndexOf(33).Now).Komasyurui == PieceType.PP, "駒33.種類=[{((RO_Star)src_Sky.StarlightIndexOf(33).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(34).Now).Komasyurui == PieceType.P || ((RO_Star)src_Sky.StarlightIndexOf(34).Now).Komasyurui == PieceType.PP, "駒34.種類=[{((RO_Star)src_Sky.StarlightIndexOf(34).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(35).Now).Komasyurui == PieceType.P || ((RO_Star)src_Sky.StarlightIndexOf(35).Now).Komasyurui == PieceType.PP, "駒35.種類=[{((RO_Star)src_Sky.StarlightIndexOf(35).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(36).Now).Komasyurui == PieceType.P || ((RO_Star)src_Sky.StarlightIndexOf(36).Now).Komasyurui == PieceType.PP, "駒36.種類=[{((RO_Star)src_Sky.StarlightIndexOf(36).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(37).Now).Komasyurui == PieceType.P || ((RO_Star)src_Sky.StarlightIndexOf(37).Now).Komasyurui == PieceType.PP, "駒37.種類=[{((RO_Star)src_Sky.StarlightIndexOf(37).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(38).Now).Komasyurui == PieceType.P || ((RO_Star)src_Sky.StarlightIndexOf(38).Now).Komasyurui == PieceType.PP, "駒38.種類=[{((RO_Star)src_Sky.StarlightIndexOf(38).Now).Komasyurui}]");
            Debug.Assert(((RO_Star)src_Sky.StarlightIndexOf(39).Now).Komasyurui == PieceType.P || ((RO_Star)src_Sky.StarlightIndexOf(39).Now).Komasyurui == PieceType.PP, "駒39.種類=[{((RO_Star)src_Sky.StarlightIndexOf(39).Now).Komasyurui}]");



            for (int i = 0; i < 40; i++)
            {
                RO_Star koma = (RO_Star)src_Sky.StarlightIndexOf(0).Now;
                Komahaiyaku185 haiyaku = koma.Haiyaku;

                if (Okiba.ShogiBan == Conv_SyElement.ToOkiba(koma.Masu))
                {
                    Debug.Assert(!Util_KomahaiyakuEx184.IsKomabukuro(haiyaku), "将棋盤の上に、配役：駒袋　があるのはおかしい。");
                }


                //if(
                //    haiyaku==Kh185.n164_歩打
                //    )
                //{
                //}
                //koma.Syurui
                //Debug.Assert((.Syurui == Ks14.H06_Oh, $"駒0.種類=[{((RO_Star_Koma)siteiSky.StarlightIndexOf(0).Now).Syurui}]");
                //sb.Append($"駒{i}.種類=[{((RO_Star_KomaKs)siteiSky.StarlightIndexOf(i).Now).Syurui}]\n");
            }


        }

        //public static Playerside GetReverseTebanside(Playerside tebanside1)
        //{
        //    Playerside side2;
        //    switch (tebanside1)
        //    {
        //        case Playerside.P1: side2 = Playerside.P2; break;
        //        case Playerside.P2: side2 = Playerside.P1; break;
        //        case Playerside.Empty: side2 = Playerside.Empty; break;
        //        default: throw new Exception($"未定義のプレイヤーサイド [{tebanside1}]");
        //    }

        //    return side2;
        //}



        /// <summary>
        /// 無ければ追加、あれば上書き。
        /// </summary>
        /// <param name="hKoma"></param>
        /// <param name="masus"></param>
        public static void AddOverwrite(
            Maps_OneAndOne<Finger, SySet<SyElement>> komabetuMasus,
            Finger finger, SySet<SyElement> masus)
        {
            if (komabetuMasus.Items.ContainsKey(finger))
            {
                komabetuMasus.Items[finger].AddSupersets(masus);//追加します。
            }
            else
            {
                // 無かったので、新しく追加します。
                komabetuMasus.Items.Add(finger, masus);
            }
        }

        /// <summary>
        /// 指し手一覧を、駒毎に分けます。
        /// TODO: これ、SkyConstに移動できないか☆？
        /// </summary>
        /// <param name="hubNode">指し手一覧</param>
        /// <returns>駒毎の、全指し手</returns>
        public static Maps_OneAndMulti<Finger, IMove> SplitMoveByStar(
            SkyConst src_Sky,
            Node<IMove, KyokumenWrapper> hubNode)
        {
            Maps_OneAndMulti<Finger, IMove> enable_teMap = new Maps_OneAndMulti<Finger, IMove>();


            hubNode.Foreach_ChildNodes((string key, Node<IMove, KyokumenWrapper> nextNode, ref bool toBreak) =>
            {
                IMove nextMove = nextNode.Key;

                Finger figKoma = Util_Sky_FingersQuery.InMasuNow(src_Sky, Util_Starlightable.AsKoma(nextMove.LongTimeAgo).Masu).ToFirst();

                enable_teMap.Put_NewOrOverwrite(figKoma, nextMove);
            });

            return enable_teMap;
        }

    }
}
