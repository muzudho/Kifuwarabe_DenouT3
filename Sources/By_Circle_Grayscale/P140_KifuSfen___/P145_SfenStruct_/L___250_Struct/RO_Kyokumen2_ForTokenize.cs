
namespace Grayscale.P145_SfenStruct_.L___250_Struct
{

    public delegate void DELEGATE_Masu201(int masuHandle, string masuString, ref bool toBreak);

    /// <summary>
    /// 「position sfen ～ moves」の、「sfen ～」の部分を読み込んだあとの、局面情報。
    /// 
    /// 指し手情報を含まない。
    /// 
    /// プロパティ１つ１つごとにメソッド（アクセッサー）を用意している。
    /// </summary>
    public interface RO_Kyokumen2_ForTokenize
    {

        RO_Kyokumen1_ForFormat ToKyokumen1();

        void Foreach_Masu201(DELEGATE_Masu201 delegate_method);

        string GetKomaAs(int suji, int dan);

        /// <summary>
        /// 持駒▲王
        /// </summary>
        int Moti1K { get; }

        /// <summary>
        /// 持駒▲飛
        /// </summary>
        int Moti1R { get; }


        /// <summary>
        /// 持駒▲角
        /// </summary>
        int Moti1B { get; }


        /// <summary>
        /// 持駒▲金
        /// </summary>
        int Moti1G { get; }


        /// <summary>
        /// 持駒▲銀
        /// </summary>
        int Moti1S { get; }


        /// <summary>
        /// 持駒▲桂
        /// </summary>
        int Moti1N { get; }


        /// <summary>
        /// 持駒▲香
        /// </summary>
        int Moti1L { get; }


        /// <summary>
        /// 持駒▲歩
        /// </summary>
        int Moti1P { get; }


        /// <summary>
        /// 持駒△王
        /// </summary>
        int Moti2k { get; }


        /// <summary>
        /// 持駒△飛
        /// </summary>
        int Moti2r { get; }


        /// <summary>
        /// 持駒△角
        /// </summary>
        int Moti2b { get; }


        /// <summary>
        /// 持駒△金
        /// </summary>
        int Moti2g { get; }


        /// <summary>
        /// 持駒△銀
        /// </summary>
        int Moti2s { get; }


        /// <summary>
        /// 持駒△桂
        /// </summary>
        int Moti2n { get; }


        /// <summary>
        /// 持駒△香
        /// </summary>
        int Moti2l { get; }


        /// <summary>
        /// 持駒△歩
        /// </summary>
        int Moti2p { get; }


        /// <summary>
        /// 駒袋 王
        /// </summary>
        int FukuroK { get; }

        /// <summary>
        /// 駒袋 飛
        /// </summary>
        int FukuroR { get; }

        /// <summary>
        /// 駒袋 角
        /// </summary>
        int FukuroB { get; }

        /// <summary>
        /// 駒袋 金
        /// </summary>
        int FukuroG { get; }

        /// <summary>
        /// 駒袋 銀
        /// </summary>
        int FukuroS { get; }

        /// <summary>
        /// 駒袋 桂
        /// </summary>
        int FukuroN { get; }

        /// <summary>
        /// 駒袋 香
        /// </summary>
        int FukuroL { get; }

        /// <summary>
        /// 駒袋 歩
        /// </summary>
        int FukuroP { get; }


        /// <summary>
        /// 先後。
        /// </summary>
        bool PsideIsBlack { get; }




        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 手目済
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        int Temezumi { get; }

    }
}
