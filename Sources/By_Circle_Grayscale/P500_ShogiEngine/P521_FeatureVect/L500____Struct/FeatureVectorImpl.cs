using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P061_ConstShogi_.L250____Const;
using Grayscale.P062_ConvText___.L500____Converter;
using Grayscale.P212_ConvPside__.L500____Converter;
using Grayscale.P213_Komasyurui_.L250____Word;
using Grayscale.P214_Masu_______.L500____Util;
using Grayscale.P521_FeatureVect.L___500_Struct;
using System.Text;

#if DEBUG
#endif

namespace Grayscale.P521_FeatureVect.L500____Struct
{

    public class FeatureVectorImpl : FeatureVector
    {

        /// <summary>
        //----------------------------------------
        // 新
        //----------------------------------------
        // （玉を追加）
        // Ｐの１３８６種類の調査項目☆
        //                         （内訳
        //                                 ※まず、プレイヤー１の駒
        //                                 歩1～81・香1～81・桂1～81・銀1～81・金1～81・
        //                                 玉1～81・飛1～81・角1～81、                                ※８１マス上の自分の駒
        //                                 歩0～18・香0～4・桂0～4・銀0～4・金0～4・飛0～2・角0～2、       ※自分の持ち駒
        //                                 
        //                                 ※次に、プレイヤー２の駒
        //                                 歩1～81・香1～81・桂1～81・銀1～81・金1～81・
        //                                 玉1～81・飛1～81・角1～81、                                 ※８１マス上の相手の駒
        //                                 歩0～18・香0～4・桂0～4・銀0～4・金0～4・飛0～2・角0～2、        ※相手の持ち駒
        //                         ）
        /// enum でも用意している。そちらはプレイヤーで分かれている。
        /// </summary>
        public const int CHOSA_KOMOKU_P = 1386;// 0～1385の要素。
        public const int CHOSA_KOMOKU_1P = 0;   //1 player
        public const int CHOSA_KOMOKU_2P = CHOSA_KOMOKU_P / 2; //2 player。2で割り切れるはず。
        // 変域の見易さを考慮して、逆順で定義。
        public const int CHOSA_KOMOKU_MOTIKAKU___ = CHOSA_KOMOKU_2P          - 3;   //「691」～693（0～ 2枚）の持ち角
        public const int CHOSA_KOMOKU_MOTIHISYA__ = CHOSA_KOMOKU_MOTIKAKU___ - 3;   //「687」～690（0～ 2枚）の持ち飛
        public const int CHOSA_KOMOKU_MOTIKIN____ = CHOSA_KOMOKU_MOTIHISYA__ - 5;   //「682」～686（0～ 4枚）の持ち金
        public const int CHOSA_KOMOKU_MOTIGIN____ = CHOSA_KOMOKU_MOTIKIN____ - 5;   //「677」～681（0～ 4枚）の持ち銀
        public const int CHOSA_KOMOKU_MOTIKEI____ = CHOSA_KOMOKU_MOTIGIN____ - 5;   //「672」～576（0～ 4枚）の持ち桂
        public const int CHOSA_KOMOKU_MOTIKYO____ = CHOSA_KOMOKU_MOTIKEI____ - 5;   //「667」～571（0～ 4枚）の持ち香
        public const int CHOSA_KOMOKU_MOTIFU_____ = CHOSA_KOMOKU_MOTIKYO____ - 19;  //「648」～666（0～18枚）の持ち歩
        public const int CHOSA_KOMOKU_____KAKU___ = CHOSA_KOMOKU_MOTIFU_____ - ConstShogi.BAN_SIZE;  //「567」～647（0～80升）の盤上の角
        public const int CHOSA_KOMOKU_____HISYA__ = CHOSA_KOMOKU_____KAKU___ - ConstShogi.BAN_SIZE;  //「486」～566（0～80升）の盤上の飛
        public const int CHOSA_KOMOKU_____OH_____ = CHOSA_KOMOKU_____HISYA__ - ConstShogi.BAN_SIZE;  //「405」～485（0～80升）の盤上の王
        public const int CHOSA_KOMOKU_____KIN____ = CHOSA_KOMOKU_____OH_____ - ConstShogi.BAN_SIZE;  //「324」～404（0～80升）の盤上の金
        public const int CHOSA_KOMOKU_____GIN____ = CHOSA_KOMOKU_____KIN____ - ConstShogi.BAN_SIZE;  //「243」～323（0～80升）の盤上の銀
        public const int CHOSA_KOMOKU_____KEI____ = CHOSA_KOMOKU_____GIN____ - ConstShogi.BAN_SIZE;  //「162」～242（0～80升）の盤上の桂
        public const int CHOSA_KOMOKU_____KYO____ = CHOSA_KOMOKU_____KEI____ - ConstShogi.BAN_SIZE;  //「 81」～161（0～80升）の盤上の香
        public const int CHOSA_KOMOKU_____FU_____ = CHOSA_KOMOKU_____KYO____ - ConstShogi.BAN_SIZE;  //「  0」～ 80（0～80升）の盤上の歩
        // エラー用、項目該当無し用。
        public const int CHOSA_KOMOKU_ERROR = -1;


        /// <summary>
        /// 駒割の価値。
        /// </summary>
        public float[] Komawari { get; set; }

        /// <summary>
        /// 二駒関係ＰＰの評価値に掛ける倍率。
        /// </summary>
        public float Bairitu_NikomaKankeiPp { get { return this.bairitu_NikomaKankeiPp; } }
        public void SetBairitu_NikomaKankeiPp(float bairitu)
        {
            this.bairitu_NikomaKankeiPp = bairitu;
        }
        private float bairitu_NikomaKankeiPp;


        /// <summary>
        /// 二駒関係ＰＰの評価値調整量の最小値。
        /// </summary>
        public float TyoseiryoSmallest_NikomaKankeiPp { get { return this.tyoseiryoSmallest_NikomaKankeiPp; } }
        public void SetTyoseiryoSmallest_NikomaKankeiPp(float value)
        {
            this.tyoseiryoSmallest_NikomaKankeiPp = value;
        }
        private float tyoseiryoSmallest_NikomaKankeiPp;

        /// <summary>
        /// 二駒関係ＰＰの評価値調整量の最大値。
        /// </summary>
        public float TyoseiryoLargest_NikomaKankeiPp { get { return this.tyoseiryoLargest_NikomaKankeiPp; } }
        public void SetTyoseiryoLargest_NikomaKankeiPp(float value)
        {
            this.tyoseiryoLargest_NikomaKankeiPp = value;
        }
        private float tyoseiryoLargest_NikomaKankeiPp;

        /// <summary>
        /// 二駒関係ＰＰの評価値調整量の初期値。
        /// </summary>
        public float TyoseiryoInit_NikomaKankeiPp { get { return this.tyoseiryoInit_NikomaKankeiPp; } }
        public void SetTyoseiryoInit_NikomaKankeiPp(float value)
        {
            this.tyoseiryoInit_NikomaKankeiPp = value;
        }
        private float tyoseiryoInit_NikomaKankeiPp;


        /// <summary>
        /// 二駒関係ＰＰ☆
        /// 
        /// １３８６種類の調査項目　×　１３８６種類の調査項目
        /// </summary>
        public float[,] NikomaKankeiPp_ForMemory { get; set; }

        ///// <summary>
        ///// 二駒関係ＰＰ☆ ファイルに保存する用。
        ///// </summary>
        //public int Get_NikomaKankeiPp_ForFile(int p1, int p2)
        //{
        //    // 復元します。
        //    float value = 
        //    return (int)
        //}

        public FeatureVectorImpl()
        {
            this.Komawari = new float[Array_Komasyurui.Items_AllElements.Length];
            this.SetBairitu_NikomaKankeiPp( 0.5963f);//ダミー 1.0f;
            this.SetTyoseiryoSmallest_NikomaKankeiPp( 0.4649f);//ダミー
            this.SetTyoseiryoLargest_NikomaKankeiPp( 0.5963f);//ダミー
            this.SetTyoseiryoInit_NikomaKankeiPp(0.5963f);//ダミー
            this.NikomaKankeiPp_ForMemory = new float[FeatureVectorImpl.CHOSA_KOMOKU_P, FeatureVectorImpl.CHOSA_KOMOKU_P];
        }

        /// <summary>
        /// 「0」→「１一」。
        /// </summary>
        /// <param name="hMasu"></param>
        /// <returns></returns>
        public static string Handle_To_Label(int hMasu)
        {
            string result;

            if (0 <= hMasu && hMasu < ConstShogi.BAN_SIZE)
            {
                SyElement masu = Conv_MasuHandle.ToMasu(hMasu);
                int suji;
                Util_MasuNum.TryMasuToSuji(masu, out suji);
                int dan;
                Util_MasuNum.TryMasuToDan(masu, out dan);

                StringBuilder sb = new StringBuilder();
                sb.Append(Conv_Int.ToArabiaSuji(suji));
                sb.Append(Conv_Int.ToKanSuji(dan));
                result = sb.ToString();
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("エラー");
                sb.Append("(");
                sb.Append(hMasu);
                sb.Append(")");
                result = sb.ToString();
            }

            return result;
        }

        /// <summary>
        /// 項目Ｋの番号を、項目名に翻訳。
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public static string K_To_Label(int k)
        {
            StringBuilder sb = new StringBuilder();

            if (0 <= k && k < ConstShogi.BAN_SIZE)
            {
                sb.Append("(");
                sb.Append(k);
                sb.Append(")");
                sb.Append(FeatureVectorImpl.Handle_To_Label(k));
            }
            else//エラー
            {
                sb.Append("エラー_k=[");
                sb.Append(k);
                sb.Append("]");
            }

            return sb.ToString();
        }


        /// <summary>
        /// 項目Pの番号を、項目名に翻訳。
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string P_To_Label(int p)
        {
            StringBuilder sb = new StringBuilder();

            if (0 <= p && p < FeatureVectorImpl.CHOSA_KOMOKU_P)
            {
                sb.Append("(");
                sb.Append(p);
                sb.Append(")");

                /// Ｐの１２２４種類の調査項目☆
                ///                         （内訳
                ///                                 ※まず、先手の駒
                ///                                 歩1～81・香1～81・桂1～81・銀1～81・金1～81・飛1～81・角1～81、  ※８１マス上の自分の駒
                ///                                 歩0～18・香0～4・桂0～4・銀0～4・金0～4・飛0～2・角0～2、        ※自分の持ち駒
                ///                                 
                ///                                 ※次に、後手の駒
                ///                                 歩1～81・香1～81・桂1～81・銀1～81・金1～81・飛1～81・角1～81、　※８１マス上の相手の駒
                ///                                 歩0～18・香0～4・桂0～4・銀0～4・金0～4・飛0～2・角0～2、        ※相手の持ち駒
                ///                         ）

                // 逆順に並べているので注意
                if (Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.MotiKaku <= p)//角0～2
                {
                    sb.Append("敵持角");
                    sb.Append(p - Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.MotiKaku);
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.MotiHi__ <= p)//飛0～2
                {
                    sb.Append("敵持飛");
                    sb.Append(p - Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.MotiHi__);
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.MotiKin_ <= p)//金0～4
                {
                    sb.Append("敵持金");
                    sb.Append(p - Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.MotiKin_);
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.MotiGin_ <= p)//銀0～4
                {
                    sb.Append("敵持銀");
                    sb.Append(p - Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.MotiGin_);
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.MotiKei_ <= p)//桂0～4
                {
                    sb.Append("敵持桂");
                    sb.Append(p - Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.MotiKei_);
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.MotiKyo_ <= p)//香0～4
                {
                    sb.Append("敵持香");
                    sb.Append(p - Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.MotiKyo_);
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.MotiFu__ <= p)//歩0～18
                {
                    sb.Append("敵持歩");
                    sb.Append(p - Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.MotiFu__);
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.Ban_Kaku <= p)//角1～81
                {
                    sb.Append(FeatureVectorImpl.Handle_To_Label(p - Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.Ban_Kaku));
                    sb.Append("敵角");
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.Ban_Hi__ <= p)//飛1～81
                {
                    sb.Append(FeatureVectorImpl.Handle_To_Label(p - Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.Ban_Hi__));
                    sb.Append("敵飛");
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.Ban_Oh__ <= p)//玉1～81
                {
                    sb.Append(FeatureVectorImpl.Handle_To_Label(p - Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.Ban_Oh__));
                    sb.Append("敵王");
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.Ban_Kin_ <= p)//金1～81
                {
                    sb.Append(FeatureVectorImpl.Handle_To_Label(p - Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.Ban_Kin_));
                    sb.Append("敵金");
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.Ban_Gin_ <= p)//銀1～81
                {
                    sb.Append(FeatureVectorImpl.Handle_To_Label(p - Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.Ban_Gin_));
                    sb.Append("敵銀");
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.Ban_Kei_ <= p)//桂1～81
                {
                    sb.Append(FeatureVectorImpl.Handle_To_Label(p - Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.Ban_Kei_));
                    sb.Append("敵桂");
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.Ban_Kyo_ <= p)//香1～81
                {
                    sb.Append(FeatureVectorImpl.Handle_To_Label(p - Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.Ban_Kyo_));
                    sb.Append("敵香");
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.Ban_Fu__ <= p)//歩1～81
                {
                    sb.Append(FeatureVectorImpl.Handle_To_Label(p - Const_NikomaKankeiP_ParamIx.PLAYER2 + Const_NikomaKankeiP_ParamIx.Ban_Fu__));
                    sb.Append("敵歩");
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.MotiKaku <= p)//角0～2
                {
                    sb.Append("自持角");
                    sb.Append(p - Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.MotiKaku);
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.MotiHi__ <= p)//飛0～2
                {
                    sb.Append("自持飛");
                    sb.Append(p - Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.MotiHi__);
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.MotiKin_ <= p)//金0～4
                {
                    sb.Append("自持金");
                    sb.Append(p - Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.MotiKin_);
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.MotiGin_ <= p)//銀0～4
                {
                    sb.Append("自持銀");
                    sb.Append(p - Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.MotiGin_);
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.MotiKei_ <= p)//桂0～4
                {
                    sb.Append("自持桂");
                    sb.Append(p - Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.MotiKei_);
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.MotiKyo_ <= p)//香0～4
                {
                    sb.Append("自持香");
                    sb.Append(p - Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.MotiKyo_);
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.MotiFu__ <= p)//歩0～18
                {
                    sb.Append("自持歩");
                    sb.Append(p - Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.MotiFu__);
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.Ban_Kaku <= p)//角1～81
                {
                    sb.Append(FeatureVectorImpl.Handle_To_Label(p - Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.Ban_Kaku));
                    sb.Append("自角");
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.Ban_Hi__ <= p)//飛1～81
                {
                    sb.Append(FeatureVectorImpl.Handle_To_Label(p - Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.Ban_Hi__));
                    sb.Append("自飛");
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.Ban_Oh__ <= p)//玉1～81
                {
                    sb.Append(FeatureVectorImpl.Handle_To_Label(p - Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.Ban_Oh__));
                    sb.Append("自玉");
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.Ban_Kin_ <= p)//金1～81
                {
                    sb.Append(FeatureVectorImpl.Handle_To_Label(p - Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.Ban_Kin_));
                    sb.Append("自金");
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.Ban_Gin_ <= p)//銀1～81
                {
                    sb.Append(FeatureVectorImpl.Handle_To_Label(p - Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.Ban_Gin_));
                    sb.Append("自銀");
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.Ban_Kei_ <= p)//桂1～81
                {
                    sb.Append(FeatureVectorImpl.Handle_To_Label(p - Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.Ban_Kei_));
                    sb.Append("自桂");
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.Ban_Kyo_ <= p)//香1～81
                {
                    sb.Append(FeatureVectorImpl.Handle_To_Label(p - Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.Ban_Kyo_));
                    sb.Append("自香");
                }
                else if (Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.Ban_Fu__ <= p)//歩1～81
                {
                    sb.Append(FeatureVectorImpl.Handle_To_Label(p - Const_NikomaKankeiP_ParamIx.PLAYER1 + Const_NikomaKankeiP_ParamIx.Ban_Fu__));
                    sb.Append("自歩");
                }
                else// エラー
                {                    
                    sb.Append("エラー_p=[");
                    sb.Append(p);
                    sb.Append("]");
                }
            }
            else// エラー
            {
                sb.Append("エラー_p=[");
                sb.Append(p);
                sb.Append("]");
            }

            return sb.ToString();
        }

    }

}
