using Grayscale.P144_ConvSujiDan.L500____Converter;
using Grayscale.P145_SfenStruct_.L___250_Struct;

namespace Grayscale.P145_SfenStruct_.L250____Struct
{

    /// <summary>
    /// SFENのstartpos。読取専用。
    /// </summary>
    public class RO_Kyokumen2_ForTokenizeImpl : RO_Kyokumen2_ForTokenize
    {

        #region プロパティー

        /// <summary>
        /// 盤上の駒を、文字の配列で。
        /// 持ち駒を、カウントで。
        /// 
        /// [0～80] 盤上
        /// [81～120] 先手駒台
        /// [121～160] 後手駒台
        /// [161～200] 駒袋
        /// </summary>
        public string[] Masu201 { get { return masu201; } } private string[] masu201;
        public string AsMasu(int masuHandle)
        {
            return this.masu201[masuHandle];
        }

        public string GetKomaAs(int suji, int dan)
        {
            return this.Masu201[ Conv_SujiDan.ToMasu(suji, dan)];
        }

        public void Foreach_Masu201(DELEGATE_Masu201 delegate_method)
        {
            bool toBreak = false;

            int masuHandle = 0;
            foreach (string masuString in this.masu201)
            {
                System.Diagnostics.Debug.Assert(null != masuString, "masuStringがヌル");

                delegate_method(masuHandle, masuString, ref toBreak);
                masuHandle++;

                if (toBreak)
                {
                    break;
                }
            }
        }



        /// <summary>
        /// 持駒▲王
        /// </summary>
        public int Moti1K { get { return moti1K; } } private int moti1K;

        /// <summary>
        /// 持駒▲飛
        /// </summary>
        public int Moti1R { get { return moti1R; } } private int moti1R;


        /// <summary>
        /// 持駒▲角
        /// </summary>
        public int Moti1B { get { return moti1B; } } private int moti1B;


        /// <summary>
        /// 持駒▲金
        /// </summary>
        public int Moti1G { get { return moti1G; } } private int moti1G;


        /// <summary>
        /// 持駒▲銀
        /// </summary>
        public int Moti1S { get { return moti1S; } } private int moti1S;


        /// <summary>
        /// 持駒▲桂
        /// </summary>
        public int Moti1N { get { return moti1N; } } private int moti1N;


        /// <summary>
        /// 持駒▲香
        /// </summary>
        public int Moti1L { get { return moti1L; } } private int moti1L;


        /// <summary>
        /// 持駒▲歩
        /// </summary>
        public int Moti1P { get { return moti1P; } } private int moti1P;


        /// <summary>
        /// 持駒△王
        /// </summary>
        public int Moti2k { get { return moti2k; } } private int moti2k;


        /// <summary>
        /// 持駒△飛
        /// </summary>
        public int Moti2r { get { return moti2r; } } private int moti2r;


        /// <summary>
        /// 持駒△角
        /// </summary>
        public int Moti2b { get { return moti2b; } } private int moti2b;


        /// <summary>
        /// 持駒△金
        /// </summary>
        public int Moti2g { get { return moti2g; } } private int moti2g;


        /// <summary>
        /// 持駒△銀
        /// </summary>
        public int Moti2s { get { return moti2s; } } private int moti2s;


        /// <summary>
        /// 持駒△桂
        /// </summary>
        public int Moti2n { get { return moti2n; } } private int moti2n;


        /// <summary>
        /// 持駒△香
        /// </summary>
        public int Moti2l { get { return moti2l; } } private int moti2l;


        /// <summary>
        /// 持駒△歩
        /// </summary>
        public int Moti2p { get { return moti2p; } } private int moti2p;


        /// <summary>
        /// 駒袋 王
        /// </summary>
        public int FukuroK { get { return fukuroK; } } private int fukuroK;

        /// <summary>
        /// 駒袋 飛
        /// </summary>
        public int FukuroR { get { return fukuroR; } } private int fukuroR;

        /// <summary>
        /// 駒袋 角
        /// </summary>
        public int FukuroB { get { return fukuroB; } } private int fukuroB;

        /// <summary>
        /// 駒袋 金
        /// </summary>
        public int FukuroG { get { return fukuroG; } } private int fukuroG;

        /// <summary>
        /// 駒袋 銀
        /// </summary>
        public int FukuroS { get { return fukuroS; } } private int fukuroS;

        /// <summary>
        /// 駒袋 桂
        /// </summary>
        public int FukuroN { get { return fukuroN; } } private int fukuroN;

        /// <summary>
        /// 駒袋 香
        /// </summary>
        public int FukuroL { get { return fukuroL; } } private int fukuroL;

        /// <summary>
        /// 駒袋 歩
        /// </summary>
        public int FukuroP { get { return fukuroP; } } private int fukuroP;


        /// <summary>
        /// 先後。
        /// </summary>
        public bool PsideIsBlack { get { return psideIsBlack; } } private bool psideIsBlack;




        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 手目済
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public int Temezumi { get { return this.temezumi; } }private int temezumi;


        #endregion



        public RO_Kyokumen2_ForTokenizeImpl(
            string[] masu201,
            int moti1K, //持駒▲王
            int moti1R, //持駒▲飛
            int moti1B, //持駒▲角
            int moti1G, //持駒▲金
            int moti1S, //持駒▲銀
            int moti1N, //持駒▲桂
            int moti1L, //持駒▲香
            int moti1P, //持駒▲歩
            int moti2k, //持駒△王
            int moti2r, //持駒△飛
            int moti2b, //持駒△角
            int moti2g, //持駒△金
            int moti2s, //持駒△銀
            int moti2n, //持駒△桂
            int moti2l, //持駒△香
            int moti2p, //持駒△歩
            int fK,//駒袋 王
            int fR,//駒袋 飛
            int fB,//駒袋 角
            int fG,//駒袋 金
            int fS,//駒袋 銀
            int fN,//駒袋 桂
            int fL,//駒袋 香
            int fP,//駒袋 歩
            string bwStr,
            string temezumiStr
            )
        {
            //盤
            this.masu201 = masu201;

            this.moti1K = moti1K; //持駒▲王
            this.moti1R = moti1R; //持駒▲飛
            this.moti1B = moti1B; //持駒▲角
            this.moti1G = moti1G; //持駒▲金
            this.moti1S = moti1S; //持駒▲銀
            this.moti1N = moti1N; //持駒▲桂
            this.moti1L = moti1L; //持駒▲香
            this.moti1P = moti1P; //持駒▲歩
            this.moti2k = moti2k; //持駒△王
            this.moti2r = moti2r; //持駒△飛
            this.moti2b = moti2b; //持駒△角
            this.moti2g = moti2g; //持駒△金
            this.moti2s = moti2s; //持駒△銀
            this.moti2n = moti2n; //持駒△桂
            this.moti2l = moti2l; //持駒△香
            this.moti2p = moti2p; //持駒△歩

            // 駒袋の中に残っている駒の数を数えます。
            this.fukuroK = fK;
            this.fukuroR = fR;
            this.fukuroB = fB;
            this.fukuroG = fG;
            this.fukuroS = fS;
            this.fukuroN = fN;
            this.fukuroL = fL;
            this.fukuroP = fP;

            //先後
            if (bwStr == "b")
            {
                this.psideIsBlack = true;
            }
            else
            {
                this.psideIsBlack = false;
            }


            //手目済
            int temezumi;
            int.TryParse(temezumiStr, out temezumi);
            this.temezumi = temezumi;
        }


        public RO_Kyokumen1_ForFormat ToKyokumen1()
        {
            RO_Kyokumen1_ForFormat ro_Kyokumen1 = new RO_Kyokumen1_ForFormatImpl();

            for (int suji = 1; suji < 10; suji++)
            {
                for (int dan = 1; dan < 10; dan++)
                {
                    ro_Kyokumen1.Ban[suji,dan] = this.GetKomaAs(suji, dan);
                }
            }

            int player;
            player = 1;
            ro_Kyokumen1.Moti[player, 0] = this.Moti1R;
            ro_Kyokumen1.Moti[player, 1] = this.Moti1B;
            ro_Kyokumen1.Moti[player, 2] = this.Moti1G;
            ro_Kyokumen1.Moti[player, 3] = this.Moti1S;
            ro_Kyokumen1.Moti[player, 4] = this.Moti1N;
            ro_Kyokumen1.Moti[player, 5] = this.Moti1L;
            ro_Kyokumen1.Moti[player, 6] = this.Moti1P;

            player = 2;
            ro_Kyokumen1.Moti[player, 0] = this.Moti2r;
            ro_Kyokumen1.Moti[player, 1] = this.Moti2b;
            ro_Kyokumen1.Moti[player, 2] = this.Moti2g;
            ro_Kyokumen1.Moti[player, 3] = this.Moti2s;
            ro_Kyokumen1.Moti[player, 4] = this.Moti2n;
            ro_Kyokumen1.Moti[player, 5] = this.Moti2l;
            ro_Kyokumen1.Moti[player, 6] = this.Moti2p;

            return ro_Kyokumen1;
        }
    }
}
