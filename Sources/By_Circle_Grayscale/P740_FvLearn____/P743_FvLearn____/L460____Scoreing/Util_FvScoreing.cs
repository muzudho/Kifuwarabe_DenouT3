using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P521_FeatureVect.L___500_Struct;
using Grayscale.P743_FvLearn____.L___400_54List;
using Grayscale.P743_FvLearn____.L400____54List;
using Grayscale.P743_FvLearn____.L450____Tyoseiryo;

namespace Grayscale.P743_FvLearn____.L460____Scoreing
{
    public abstract class Util_FvScoreing
    {

        ///// <summary>
        ///// 2駒関係の54要素で、持ち駒に関係する箇所のうち、
        ///// バッドな点が入っているものは　全て吸い取って　０　点にします。
        ///// 吸い取った　バッド点　を返却します。
        ///// </summary>
        //public static float Spoil_MotiBad( Playerside selfPside, FeatureVector fv_mutable, N54List n54List, KwErrorHandler errH)
        //{
        //    float result_sum = 0;

        //    //----------------------------------------
        //    // [ＰＰ]　駒台の各駒１　×　盤上の各駒２
        //    //----------------------------------------
        //    for (int iNext1 = 0; iNext1 < n54List.P14Next; iNext1++)// p1 を回す。
        //    {
        //        int p1 = n54List.P14List[iNext1];


        //        for (int iNext2 = 0; iNext2 < n54List.P40Next; iNext2++)// p2 を回す。
        //        {
        //            int p2 = n54List.P40List[iNext2];

        //            //----------------------------------------
        //            // よし、２駒関係だぜ☆！
        //            //----------------------------------------
        //            if (
        //                // プレイヤー1で、piece1 がPlayer1駒で、その点が後手寄りのとき。
        //                (Playerside.P1 == selfPside && p1 < Const_NikomaKankeiP_ParamIx.PLAYER2 && fv_mutable.NikomaKankeiPp_ForMemory[p1, p2] < 0.0f)
        //                // または、
        //                ||
        //                // プレイヤー2で、piece1 がPlayer2駒で、その点が先手寄りのとき。
        //                (Playerside.P2 == selfPside && Const_NikomaKankeiP_ParamIx.PLAYER2 <= p1 && 0.0f < fv_mutable.NikomaKankeiPp_ForMemory[p1, p2] )
        //                )
        //            {
                        
        //                result_sum += fv_mutable.NikomaKankeiPp_ForMemory[p1, p2];
        //                fv_mutable.NikomaKankeiPp_ForMemory[p1, p2] = 0;
        //            }
        //        }
        //    }

        //    //----------------------------------------
        //    // [ＰＰ]　盤上の各駒１　×　駒台の各駒２
        //    //----------------------------------------
        //    for (int iNext1 = 0; iNext1 < n54List.P40Next; iNext1++)// p1 を回す。
        //    {
        //        int p1 = n54List.P40List[iNext1];


        //        for (int iNext2 = 0; iNext2 < n54List.P14Next; iNext2++)// p2 を回す。
        //        {
        //            int p2 = n54List.P14List[iNext2];

        //            //----------------------------------------
        //            // よし、２駒関係だぜ☆！
        //            //----------------------------------------
        //            if (
        //                // プレイヤー1で、piece2 がPlayer1駒で、その点が後手寄りのとき。
        //                (Playerside.P1 == selfPside && p2 < Const_NikomaKankeiP_ParamIx.PLAYER2 && fv_mutable.NikomaKankeiPp_ForMemory[p1, p2] < 0.0f)
        //                // または、
        //                ||
        //                // プレイヤー2で、piece2 がPlayer2駒で、その点が先手寄りのとき。
        //                (Playerside.P2 == selfPside && Const_NikomaKankeiP_ParamIx.PLAYER2 <= p2 && 0.0f < fv_mutable.NikomaKankeiPp_ForMemory[p1, p2])
        //                )
        //            {

        //                result_sum += fv_mutable.NikomaKankeiPp_ForMemory[p1, p2];
        //                fv_mutable.NikomaKankeiPp_ForMemory[p1, p2] = 0.0f;
        //            }
        //        }
        //    }

        //    //----------------------------------------
        //    // [ＰＰ]　駒台の各駒１　×　駒台の各駒２
        //    //----------------------------------------
        //    //
        //    // FIXME: 持駒と、持駒の比較は、とりあえずパス。

        //    return result_sum;
        //}

        ///// <summary>
        ///// ２駒関係の54要素で表される箇所のうち、盤上の２駒に関する評価値に加算します。
        ///// </summary>
        //public static void Fill54x54_Add_ToBanjo(float offset, SkyConst src_Sky, FeatureVector fv_mutable, N54List n54List, KwErrorHandler errH)
        //{
        //    //----------------------------------------
        //    // [ＰＰ]　盤上の各駒１　×　盤上の各駒２
        //    //----------------------------------------
        //    for (int iNext1 = 0; iNext1 < n54List.P40Next; iNext1++)// p1 を回す。
        //    {
        //        int p1 = n54List.P40List[iNext1];


        //        for (int iNext2 = 0; iNext2 < n54List.P40Next; iNext2++)// p2 を回す。
        //        {
        //            int p2 = n54List.P40List[iNext2];

        //            //----------------------------------------
        //            // よし、２駒関係だぜ☆！
        //            //----------------------------------------
        //            fv_mutable.NikomaKankeiPp_ForMemory[p1, p2] += offset;
        //        }
        //    }
        //}

        /// <summary>
        /// ２駒関係の54要素で表される箇所全ての評価値に加算します。
        /// </summary>
        public static void Fill54x54_Add(out int changedCells, float offset, SkyConst src_Sky, FeatureVector fv_mutable,
            N54List n54List, KwErrorHandler errH)
        {
            changedCells = 0;

            //
            //
            // 例えば、[1P１三歩、2P２一桂]という組み合わせと、[2P２一桂、1P１三歩]という組み合わせは、同じだが欄が２つある。
            // そこで、表の半分を省きたい。
            // しかし、表を三角形にするためには、要素は昇順にソートされている必要がある。
            // 合法手１つごとにソートしていては、本末転倒。
            // そこで、表は正方形に読み、内容は三角形の部分にだけ入っているということにする。
            //
            //
            // 例えば、[1P１三歩、1P１三歩]という組み合わせもある。これは、自分自身の絶対位置の評価として試しに、残しておいてみる☆
            //
            //

            for (int iA = 0; iA < n54List.P54Next; iA++)// p1 を回す。
            {
                int p1 = n54List.P54List_unsorted[iA];

                for (int iB = 0; iB < n54List.P54Next; iB++)// p2 を回す。
                {
                    int p2 = n54List.P54List_unsorted[iB];

                    if (p1 <= p2) // 「p2 < p1」という組み合わせは同じ意味なので省く。「p1==p2」は省かない。
                    {
                        //----------------------------------------
                        // よし、組み合わせだぜ☆！
                        //----------------------------------------
                        fv_mutable.NikomaKankeiPp_ForMemory[p1, p2] += offset;
                        changedCells++;//実際に調整した量。
                    }
                    else
                    {
                        //----------------------------------------
                        // 使っていない方の三角形だぜ☆！
                        //----------------------------------------
                        fv_mutable.NikomaKankeiPp_ForMemory[p1, p2] = 0;
                    }
                }
            }


            ////----------------------------------------
            //// [ＰＰ]　盤上の各駒１　×　盤上の各駒２
            ////----------------------------------------
            //for (int iNext1 = 0; iNext1 < n54List.P40Next; iNext1++)// p1 を回す。
            //{
            //    int p1 = n54List.P40List[iNext1];


            //    for (int iNext2 = 0; iNext2 < n54List.P40Next; iNext2++)// p2 を回す。
            //    {
            //        int p2 = n54List.P40List[iNext2];

            //        fv_mutable.NikomaKankeiPp_ForMemory[p1, p2] += offset;
            //        changedCells++;//実際に調整した量。

            //    }
            //}

            ////----------------------------------------
            //// [ＰＰ]　駒台の各駒１　×　盤上の各駒２
            ////----------------------------------------
            ////
            //// ※注 「盤上の各駒１　×　駒台の各駒２」の組み合わせは　同じ意味なので省く。
            ////
            //for (int iNext1 = 0; iNext1 < n54List.P14Next; iNext1++)// p1 を回す。
            //{
            //    int p1 = n54List.P14List[iNext1];


            //    for (int iNext2 = 0; iNext2 < n54List.P40Next; iNext2++)// p2 を回す。
            //    {
            //        int p2 = n54List.P40List[iNext2];

            //        //----------------------------------------
            //        // よし、２駒関係だぜ☆！
            //        //----------------------------------------
            //        fv_mutable.NikomaKankeiPp_ForMemory[p1, p2] += offset;
            //        changedCells++;//実際に調整した量。
            //    }
            //}

            //////----------------------------------------
            ////// [ＰＰ]　盤上の各駒１　×　駒台の各駒２
            //////----------------------------------------
            ////for (int iNext1 = 0; iNext1 < n54List.P40Next; iNext1++)// p1 を回す。
            ////{
            ////    int p1 = n54List.P40List[iNext1];


            ////    for (int iNext2 = 0; iNext2 < n54List.P14Next; iNext2++)// p2 を回す。
            ////    {
            ////        int p2 = n54List.P14List[iNext2];

            ////        //----------------------------------------
            ////        // よし、２駒関係だぜ☆！
            ////        //----------------------------------------
            ////        fv_mutable.NikomaKankeiPp_ForMemory[p1, p2] += offset;
            ////        changedCells++;//実際に調整した量。
            ////    }
            ////}

            ////----------------------------------------
            //// [ＰＰ]　駒台の各駒１　×　駒台の各駒２
            ////----------------------------------------
            //for (int iNext1 = 0; iNext1 < n54List.P14Next; iNext1++)// p1 を回す。
            //{
            //    int p1 = n54List.P14List[iNext1];


            //    for (int iNext2 = 0; iNext2 < n54List.P14Next; iNext2++)// p2 を回す。
            //    {
            //        int p2 = n54List.P14List[iNext2];

            //        if (p1 <= p2) // 「p2 < p1」という組み合わせは同じ意味なので省く。「p1==p2」は省かない。
            //        {
            //            //----------------------------------------
            //            // よし、組み合わせだぜ☆！
            //            //----------------------------------------
            //            fv_mutable.NikomaKankeiPp_ForMemory[p1, p2] += offset;
            //            changedCells++;//実際に調整した量。
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 指定した局面の評価を、加点（減点）します。
        /// </summary>
        public static void UpdateKyokumenHyoka(
            N54List n54List,
            SkyConst src_Sky,
            FeatureVector fv_mutable,
            float tyoseiryo,
            out float out_real_tyoseiryo,//実際に調整した量。
            KwErrorHandler errH
            )
        {
            //
            // 盤上の駒数と、持駒の先後の種類数
            //
            //int banjoKomaKazu_motiSyuruiKazu = 0;

            // 増 or 減
            {
                // 調整量を刻みます。
                float kizami_up = Util_Tyoseiryo.Average_54x54Parameters(tyoseiryo, n54List);

                int changedCells;
                Util_FvScoreing.Fill54x54_Add(out changedCells, kizami_up, src_Sky, fv_mutable, n54List, errH);
                out_real_tyoseiryo = changedCells * kizami_up;
            }

            // 持ち駒の BAD値を矯正 FIXME: 持ち駒を打つのが悪手なのは、だいたい合っているのでは？
            //if(false)
            //{
            //    float sum_bad = Util_FvScoreing.Spoil_MotiBad(src_Sky.KaisiPside, fv_mutable, n54List, errH);

            //    // 盤上の三駒関係に、均等に BAD値 を配分。
            //    Util_FvScoreing.Fill54x54_Add_ToBanjo(
            //        sum_bad / (float)n54List.P40Next,//平均にする
            //        src_Sky,
            //        fv_mutable,
            //        n54List,
            //        errH
            //        );
            //}
        }
    }
}
