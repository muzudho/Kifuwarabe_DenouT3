using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

#if DEBUG
using System.Diagnostics;
#endif

namespace Grayscale.Kifuwarakaku.Entities.Features
{


    public abstract class Play
    {


        /// <summary>
        /// 指定した持ち駒全てについて、基本的な駒の動きを返します。（金は「前、ななめ前、横、下に進める」のような）
        /// （ポテンシャル・ムーブ＝障害物がなかったときの動き）
        /// 
        /// １局面につき、１回実行される。
        /// </summary>
        /// <param name="src_Sky"></param>
        /// <param name="fingers_sirabetaiMOTIkoma"></param>
        /// <param name="motiOkenaiMasus">持ち駒を置けないマス（味方駒、敵駒が置いてあるマス）</param>
        /// <returns></returns>
        public static List_OneAndMulti<Finger, SySet<SyElement>> Translate_Motikoma_ToMove(
            SkyConst src_Sky,
            Fingers fingers_sirabetaiMOTIkoma,
            SySet<SyElement> masus_mikata_onBanjo,//打ち歩詰めチェック用
            SySet<SyElement> masus_aite_onBanjo,//打ち歩詰めチェック用
            SySet<SyElement> motiOkenaiMasus
            )
        {
            // 駒種類別、置こうとする升
            SySet<SyElement>[] aMasus = new SySet<SyElement>[Array_Komasyurui.Items_AllElements.Length];
            // 駒種類別、置こうとする駒（持駒の代表）
            RO_Star[] aDaihyo = new RO_Star[Array_Komasyurui.Items_AllElements.Length];
            // 駒種類別、置こうとする駒番号
            Finger[] aFigKoma = new Finger[Array_Komasyurui.Items_AllElements.Length];


            Finger[] daihyoArray;// 持駒。駒の種類代表１個
            Util_Fingers_KomasyuruiQuery.Translate_Fingers_ToKomasyuruiBETUFirst(
                src_Sky,
                fingers_sirabetaiMOTIkoma,
                out daihyoArray
                );
            foreach (Finger figDaihyo in daihyoArray)
            {
                if (Fingers.Error_1 != figDaihyo)
                {
                    RO_Star daihyo = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(figDaihyo).Now);
#if DEBUG
                    Debug.Assert(daihyo != null, "持ち駒の代表がヌル");
#endif
                    // 駒種類別、置こうとする駒
                    aDaihyo[(int)daihyo.Komasyurui] = daihyo;
                    // 駒種類別、置こうとする升
                    aMasus[(int)daihyo.Komasyurui] = Util_Sky_SyugoQuery.KomaKidou_Potential(figDaihyo, src_Sky);
                    // 駒種類別、置こうとする駒番号
                    aFigKoma[(int)daihyo.Komasyurui] = figDaihyo;
                }
            }


            if (aDaihyo[(int)Komasyurui14.H01_Fu_____] != null)// 攻め手が、歩を持っているなら
            {
                //----------------------------------------
                // 二歩チェック
                //----------------------------------------
                //
                // 打てない筋の升を除外します。
                //
#if DEBUG
                //if (null != errH_orNull)
                //{
                //    Logger.Trace("----------------------------------------");
                //    Logger.Trace($"歩を置きたかった升＝{Util_SySet.Dump_Elements(aMasus[(int)Komasyurui14.H01_Fu_____])}");
                //    Logger.Trace("----------------------------------------");
                //    Logger.Trace("歩の置けない筋チェック（二歩チェック）開始");
                //}
#endif
                // 将棋盤上の自歩一覧。
                Fingers banjoJiFus = Util_Sky_FingersQuery.InOkibaPsideKomasyuruiNow(
                    src_Sky,//指定局面
                    Okiba.ShogiBan,//将棋盤上の
                    aDaihyo[(int)Komasyurui14.H01_Fu_____].Pside,//持駒を持っているプレイヤー側の
                    Komasyurui14.H01_Fu_____//歩
                    );

#if DEBUG
                //if (null != errH_orNull)
                //{
                //    Logger.Trace("banjoJiFus.Count=[" + banjoJiFus.Count + "]");
                //    foreach (Finger figKoma in banjoJiFus.Items)
                //    {
                //        Logger.Trace("figKoma=[" + (int)figKoma + "]");
                //    }
                //    Logger.Trace("----------------------------------------");
                //}
#endif

                // 筋別、歩のあるなしチェック
                bool[] existsFu_sujibetu = new bool[10];
                foreach (Finger figBanjoJiFu in banjoJiFus.Items)
                {
                    RO_Star banjoJiFu = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(figBanjoJiFu).Now);
                    int suji;//1～9
                    Util_MasuNum.TryMasuToSuji(banjoJiFu.Masu, out suji);
                    existsFu_sujibetu[suji] = true;
                }

                // 9筋 確認します。
                for (int suji = 1; suji < 10; suji++)
                {
                    if (existsFu_sujibetu[suji])
                    {
                        // 二歩になる筋番号の発見☆
#if DEBUG
                        //Logger.Trace("二歩チェック： " + suji + "筋は二歩。");
#endif

                        // 筋一列を、クリアーします。

                        // 駒種類別、置こうとする升
                        aMasus[(int)Komasyurui14.H01_Fu_____] = aMasus[(int)Komasyurui14.H01_Fu_____].Minus_Closed(
                            Masu_Honshogi.BAN_SUJIS[suji], Util_SyElement_BinaryOperator.Dlgt_Equals_MasuNumber);
                    }
                }
#if DEBUG
                //if (null != errH_orNull)
                //{
                //    Logger.Trace("歩の置けない筋チェック（二歩チェック）終了");
                //    Logger.Trace("----------------------------------------");
                //    Logger.Trace("歩の置ける升＝" + Util_SySet.Dump_Elements(aMasus[(int)Komasyurui14.H01_Fu_____]));
                //    Logger.Trace("----------------------------------------");
                //}
#endif

                //----------------------------------------
                // 打ち歩詰めチェック
                //----------------------------------------
                if (false)
                {
                    Util_Utifudume.Utifudume(
                        src_Sky,
                        masus_mikata_onBanjo,//打ち歩詰めチェック用
                        masus_aite_onBanjo,//打ち歩詰めチェック用
                        aMasus//駒種類別、置こうとする升
                    );
                }
            }



            //----------------------------------------
            // 集計
            //----------------------------------------
            // 「どの持ち駒を」「どこに置ける」のコレクション。
            List_OneAndMulti<Finger, SySet<SyElement>> result = new List_OneAndMulti<Finger, SySet<SyElement>>();
            foreach (Komasyurui14 ks in Array_Komasyurui.MotiKoma7Syurui)
            {
                // 置こうとする駒があれば
                if (null != aDaihyo[(int)ks])
                {
                    // 置けない升を引きます。
                    aMasus[(int)ks] = aMasus[(int)ks].Minus_Closed(
                        motiOkenaiMasus, Util_SyElement_BinaryOperator.Dlgt_Equals_MasuNumber);

                    if (!aMasus[(int)ks].IsEmptySet())
                    {
                        // 置ける升があれば登録
                        result.AddNew(
                            aFigKoma[(int)ks],//置こうとする持駒番号
                            aMasus[(int)ks]//置ける升
                            );
                    }
                }
            }
            return result;
        }

    }


}
