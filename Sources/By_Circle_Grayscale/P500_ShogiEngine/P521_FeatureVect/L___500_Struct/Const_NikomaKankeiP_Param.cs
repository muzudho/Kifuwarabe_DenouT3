using Grayscale.P061_ConstShogi_.L250____Const;

namespace Grayscale.P521_FeatureVect.L___500_Struct
{
    /// <summary>
    /// 二駒関係のＰ　パラメーター・インデックス
    /// </summary>
    public class Const_NikomaKankeiP_ParamIx
    {
        //----------------------------------------
        // Ｐの１３８６種類の調査項目☆
        //----------------------------------------
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
        // 定数でも用意している。そちらはプレイヤーで分かれていない。


        public const int PLAYER1 = 0;
        public const int Ban_Fu__ = PLAYER1;
        public const int Ban_Kyo_ = Ban_Fu__ + ConstShogi.BAN_SIZE;
        public const int Ban_Kei_ = Ban_Kyo_ + ConstShogi.BAN_SIZE;
        public const int Ban_Gin_ = Ban_Kei_ + ConstShogi.BAN_SIZE;
        public const int Ban_Kin_ = Ban_Gin_ + ConstShogi.BAN_SIZE;
        public const int Ban_Oh__ = Ban_Kin_ + ConstShogi.BAN_SIZE;
        public const int Ban_Hi__ = Ban_Oh__ + ConstShogi.BAN_SIZE;
        public const int Ban_Kaku = Ban_Hi__ + ConstShogi.BAN_SIZE;
        public const int MotiFu__ = Ban_Kaku + ConstShogi.BAN_SIZE;
        public const int MotiKyo_ = MotiFu__ + 19;
        public const int MotiKei_ = MotiKyo_ + 5;
        public const int MotiGin_ = MotiKei_ + 5;
        public const int MotiKin_ = MotiGin_ + 5;
        public const int MotiHi__ = MotiKin_ + 5;
        public const int MotiKaku = MotiHi__ + 3;

        public const int PLAYER2 = MotiKaku + 3;
    }
}
