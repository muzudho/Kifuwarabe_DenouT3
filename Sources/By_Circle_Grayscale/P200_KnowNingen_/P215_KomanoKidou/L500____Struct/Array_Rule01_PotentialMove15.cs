using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P056_Syugoron___.L250____Struct;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P212_ConvPside__.L500____Converter;


namespace Grayscale.P215_KomanoKidou.L500____Struct
{

    /// <summary>
    /// 駒の種類１５個ごとの、「周りに障害物がないときに、ルール上移動可能なマス」。
    /// </summary>
    public abstract class Array_Rule01_PotentialMove15
    {

        public delegate SySet<SyElement> DELEGATE_CreateLegalMoveLv1(Playerside pside, SyElement masu_ji);

        public static DELEGATE_CreateLegalMoveLv1[] ItemMethods
        {
            get
            {
                return Array_Rule01_PotentialMove15.itemMethods;
            }
        }
        private static DELEGATE_CreateLegalMoveLv1[] itemMethods;

        static Array_Rule01_PotentialMove15()
        {
            Array_Rule01_PotentialMove15.itemMethods = new DELEGATE_CreateLegalMoveLv1[]{
                null,//[0]取った駒が分からない状況など用
                Array_Rule01_PotentialMove15.Create_01Fu,//[1]歩
                Array_Rule01_PotentialMove15.Create_02Kyo,//香
                Array_Rule01_PotentialMove15.Create_03Kei,//桂
                Array_Rule01_PotentialMove15.Create_04Gin,//銀
                Array_Rule01_PotentialMove15.Create_05Kin,//金
                Array_Rule01_PotentialMove15.Create_06Oh,//王
                Array_Rule01_PotentialMove15.Create_07Hisya,//飛車
                Array_Rule01_PotentialMove15.Create_08Kaku,//角
                Array_Rule01_PotentialMove15.Create_09Ryu,//竜
                Array_Rule01_PotentialMove15.Create_10Uma,//[10]馬
                Array_Rule01_PotentialMove15.Create_05Kin,//と金
                Array_Rule01_PotentialMove15.Create_05Kin,//成香
                Array_Rule01_PotentialMove15.Create_05Kin,//成桂
                Array_Rule01_PotentialMove15.Create_05Kin,//成銀
                Array_Rule01_PotentialMove15.Create_15ErrorKoma,//[15]
            };
        }


        /// <summary>
        /// 合法手レベル１
        /// </summary>
        /// <returns></returns>
        public static SySet<SyElement> Create_01Fu(Playerside pside, SyElement masu_ji)
        {
            //----------------------------------------
            // 歩
            //----------------------------------------
            SySet_Default<SyElement> dst = new SySet_Default<SyElement>("歩の移動先");

            if (Okiba.ShogiBan == Conv_SyElement.ToOkiba(masu_ji))
            {
                //----------------------------------------
                // 将棋盤上の歩の移動先
                //----------------------------------------
                dst.AddSupersets(KomanoKidou.DstIppo_上(pside, masu_ji));
            }
            else if( (Okiba.Sente_Komadai|Okiba.Gote_Komadai).HasFlag(
                Conv_SyElement.ToOkiba(masu_ji)))
            {
                //----------------------------------------
                // 駒台上の歩の移動先
                //----------------------------------------
                dst.AddSupersets(KomanoKidou.Dst_歩打面(pside));
            }

            return dst;
        }

        /// <summary>
        /// 合法手レベル１
        /// </summary>
        /// <returns></returns>
        public static SySet<SyElement> Create_02Kyo(Playerside pside, SyElement masu_ji)
        {
            SySet_Default<SyElement> dst = new SySet_Default<SyElement>("香の移動先");

            if (Okiba.ShogiBan == Conv_SyElement.ToOkiba(masu_ji))
            {
                dst.AddSupersets(KomanoKidou.DstKantu_上(pside, masu_ji));
            }
            else if ((Okiba.Sente_Komadai | Okiba.Gote_Komadai).HasFlag(
                Conv_SyElement.ToOkiba(masu_ji)))
            {
                dst.AddSupersets(KomanoKidou.Dst_歩打面(pside));//香も同じ
            }

            return dst;
        }


        /// <summary>
        /// 合法手レベル１
        /// </summary>
        /// <returns></returns>
        public static SySet<SyElement> Create_03Kei(Playerside pside, SyElement masu_ji)
        {
            SySet_Default<SyElement> dst = new SySet_Default<SyElement>("桂の移動先");

            if (Okiba.ShogiBan == Conv_SyElement.ToOkiba(masu_ji))
            {
                dst.AddSupersets(KomanoKidou.DstKeimatobi_駆(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstKeimatobi_跳(pside, masu_ji));
            }
            else if ((Okiba.Sente_Komadai | Okiba.Gote_Komadai).HasFlag(
                Conv_SyElement.ToOkiba(masu_ji)))
            {
                dst.AddSupersets(KomanoKidou.Dst_桂打面(pside));
            }

            return dst;
        }


        /// <summary>
        /// 合法手レベル１
        /// </summary>
        /// <returns></returns>
        public static SySet<SyElement> Create_04Gin(Playerside pside, SyElement masu_ji)
        {
            SySet_Default<SyElement> dst = new SySet_Default<SyElement>("銀の移動先");

            if (Okiba.ShogiBan == Conv_SyElement.ToOkiba(masu_ji))
            {
                dst.AddSupersets(KomanoKidou.DstIppo_上(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstIppo_昇(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstIppo_沈(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstIppo_降(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstIppo_浮(pside, masu_ji));
            }
            else if ((Okiba.Sente_Komadai | Okiba.Gote_Komadai).HasFlag(
                Conv_SyElement.ToOkiba(masu_ji)))
            {
                dst.AddSupersets(KomanoKidou.Dst_全打面(pside));
            }

            return dst;
        }

        /// <summary>
        /// 合法手レベル１
        /// </summary>
        /// <returns></returns>
        public static SySet<SyElement> Create_05Kin(Playerside pside, SyElement masu_ji)
        {
            SySet<SyElement> dst = new SySet_Default<SyElement>("金の移動先");

            if (Okiba.ShogiBan == Conv_SyElement.ToOkiba(masu_ji))
            {
                dst = Array_Rule01_PotentialMove15.CreateKin_static(pside, masu_ji);
            }
            else if ((Okiba.Sente_Komadai | Okiba.Gote_Komadai).HasFlag(
                Conv_SyElement.ToOkiba(masu_ji)))
            {
                dst.AddSupersets(KomanoKidou.Dst_全打面(pside));
            }

            return dst;
        }

        public static SySet<SyElement> CreateKin_static(Playerside pside, SyElement masu_ji)
        {
            SySet_Default<SyElement> dst = new SySet_Default<SyElement>("カナゴマの移動先");

            if (Okiba.ShogiBan == Conv_SyElement.ToOkiba(masu_ji))
            {
                dst.AddSupersets(KomanoKidou.DstIppo_上(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstIppo_昇(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstIppo_射(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstIppo_引(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstIppo_滑(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstIppo_浮(pside, masu_ji));
            }

            return dst;
        }


        /// <summary>
        /// 合法手レベル１
        /// </summary>
        /// <returns></returns>
        public static SySet<SyElement> Create_06Oh(Playerside pside, SyElement masu_ji)
        {
            SySet_Default<SyElement> dst = new SySet_Default<SyElement>("王の移動先");

            if (Okiba.ShogiBan == Conv_SyElement.ToOkiba(masu_ji))
            {
                dst.AddSupersets(KomanoKidou.DstIppo_上(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstIppo_昇(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstIppo_射(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstIppo_沈(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstIppo_引(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstIppo_降(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstIppo_滑(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstIppo_浮(pside, masu_ji));
            }
            else if ((Okiba.Sente_Komadai | Okiba.Gote_Komadai).HasFlag(
                Conv_SyElement.ToOkiba(masu_ji)))
            {
                dst.AddSupersets(KomanoKidou.Dst_全打面(pside));
            }

            return dst;
        }


        /// <summary>
        /// 合法手レベル１
        /// </summary>
        /// <returns></returns>
        public static SySet<SyElement> Create_07Hisya(Playerside pside, SyElement masu_ji)
        {
            SySet_Default<SyElement> dst = new SySet_Default<SyElement>("飛車の移動先");

            if (Okiba.ShogiBan == Conv_SyElement.ToOkiba(masu_ji))
            {
                dst.AddSupersets(KomanoKidou.DstKantu_上(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstKantu_射(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstKantu_引(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstKantu_滑(pside, masu_ji));
            }
            else if ((Okiba.Sente_Komadai | Okiba.Gote_Komadai).HasFlag(
                Conv_SyElement.ToOkiba(masu_ji)))
            {
                dst.AddSupersets(KomanoKidou.Dst_全打面(pside));
            }

            return dst;
        }


        /// <summary>
        /// 合法手レベル１
        /// </summary>
        /// <returns></returns>
        public static SySet<SyElement> Create_08Kaku(Playerside pside, SyElement masu_ji)
        {
            SySet_Default<SyElement> dst = new SySet_Default<SyElement>("角の移動先");

            if (Okiba.ShogiBan == Conv_SyElement.ToOkiba(masu_ji))
            {
                dst.AddSupersets(KomanoKidou.DstKantu_昇(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstKantu_沈(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstKantu_降(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstKantu_浮(pside, masu_ji));
            }
            else if ((Okiba.Sente_Komadai | Okiba.Gote_Komadai).HasFlag(
                Conv_SyElement.ToOkiba(masu_ji)))
            {
                dst.AddSupersets(KomanoKidou.Dst_全打面(pside));
            }

            return dst;
        }



        /// <summary>
        /// 合法手レベル１
        /// </summary>
        /// <returns></returns>
        public static SySet<SyElement> Create_09Ryu(Playerside pside, SyElement masu_ji)
        {
            SySet_Default<SyElement> dst = new SySet_Default<SyElement>("竜の移動先");

            if (Okiba.ShogiBan == Conv_SyElement.ToOkiba(masu_ji))
            {
                dst.AddSupersets(KomanoKidou.DstKantu_上(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstIppo_昇(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstKantu_射(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstIppo_沈(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstKantu_引(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstIppo_降(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstKantu_滑(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstIppo_浮(pside, masu_ji));
            }
            else if ((Okiba.Sente_Komadai | Okiba.Gote_Komadai).HasFlag(
                Conv_SyElement.ToOkiba(masu_ji)))
            {
                dst.AddSupersets(KomanoKidou.Dst_全打面(pside));
            }

            return dst;
        }



        /// <summary>
        /// 合法手レベル１
        /// </summary>
        /// <returns></returns>
        public static SySet<SyElement> Create_10Uma(Playerside pside, SyElement masu_ji)
        {
            SySet_Default<SyElement> dst = new SySet_Default<SyElement>("馬の移動先");

            if (Okiba.ShogiBan == Conv_SyElement.ToOkiba(masu_ji))
            {
                dst.AddSupersets(KomanoKidou.DstIppo_上(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstKantu_昇(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstIppo_射(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstKantu_沈(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstIppo_引(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstKantu_降(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstIppo_滑(pside, masu_ji));
                dst.AddSupersets(KomanoKidou.DstKantu_浮(pside, masu_ji));
            }
            else if ((Okiba.Sente_Komadai | Okiba.Gote_Komadai).HasFlag(
                Conv_SyElement.ToOkiba(masu_ji)))
            {
                dst.AddSupersets(KomanoKidou.Dst_全打面(pside));
            }

            return dst;
        }

        /// <summary>
        /// 合法手レベル１
        /// </summary>
        /// <returns></returns>
        public static SySet<SyElement> Create_15ErrorKoma(Playerside pside, SyElement masu_ji)
        {
            return new SySet_Default<SyElement>("エラー駒の移動先");
        }


    }


}
