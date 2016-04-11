using Grayscale.P145_SfenStruct_.L___250_Struct;
using Grayscale.P157_KyokumenPng.L___500_Struct;

namespace Grayscale.P157_KyokumenPng.L500____Struct
{


    public class KyokumenPngArgsImpl : KyokumenPngArgs
    {
        /// <summary>
        /// 特に変わらない設定。
        /// </summary>
        public KyokumenPngEnvironment Env { get { return this.env; } }
        private KyokumenPngEnvironment env;

        /// <summary>
        /// 出力ファイルへのパス。
        /// </summary>
        public string OutFile { get { return this.outFile; } }
        private string outFile;

        public RO_Kyokumen1_ForFormat Ro_Kyokumen1 { get { return this.ro_Kyokumen1; } }
        private RO_Kyokumen1_ForFormat ro_Kyokumen1;

        /// <summary>
        /// 移動元升。１一を0とし、１二を1、９九を80とする。なければ-1。範囲外の数字は無視するだけ。
        /// </summary>
        public int SrcMasu_orMinusOne { get { return this.srcMasu_orMinusOne; } }
        private int srcMasu_orMinusOne;

        /// <summary>
        /// 移動先升。１一を0とし、１二を1、９九を80とする。なければ-1。範囲外の数字は無視するだけ。
        /// </summary>
        public int DstMasu_orMinusOne { get { return this.dstMasu_orMinusOne; } }
        private int dstMasu_orMinusOne;

        /// <summary>
        /// 取った駒の種類。enum定数を使うこと。
        /// </summary>
        public KyokumenPngArgs_FoodOrDropKoma FoodKoma { get { return this.foodKoma; } }
        private KyokumenPngArgs_FoodOrDropKoma foodKoma;

        /// <summary>
        /// 打った駒の種類。enum定数を使うこと。
        /// </summary>
        public KyokumenPngArgs_FoodOrDropKoma DropKoma { get { return this.dropKoma; } }
        private KyokumenPngArgs_FoodOrDropKoma dropKoma;


        public KyokumenPngArgsImpl(
            RO_Kyokumen1_ForFormat ro_Kyokumen1,
            int srcMasu_orMinusOne,
            int dstMasu_orMinusOne,
            KyokumenPngArgs_FoodOrDropKoma foodKoma,
            KyokumenPngArgs_FoodOrDropKoma dropKoma,
            string outFile,  
            KyokumenPngEnvironment reportEnvironment)
        {
            this.ro_Kyokumen1 = ro_Kyokumen1;
            this.srcMasu_orMinusOne = srcMasu_orMinusOne;
            this.dstMasu_orMinusOne = dstMasu_orMinusOne;
            this.foodKoma = foodKoma;
            this.dropKoma = dropKoma;

            //// デバッグ
            //{
            //    Debug.Assert(this.ro_Kyokumen1.Ban.Length == 10, "サイズ違反");
            //    Debug.Assert(this.ro_Kyokumen1.Ban[0].Length == 10, "サイズ違反");
            //    Debug.Assert(this.ro_Kyokumen1.Ban[1].Length == 10, "サイズ違反");
            //    Debug.Assert(this.ro_Kyokumen1.Ban[2].Length == 10, "サイズ違反");
            //    Debug.Assert(this.ro_Kyokumen1.Ban[3].Length == 10, "サイズ違反");
            //    Debug.Assert(this.ro_Kyokumen1.Ban[4].Length == 10, "サイズ違反");
            //    Debug.Assert(this.ro_Kyokumen1.Ban[5].Length == 10, "サイズ違反");
            //    Debug.Assert(this.ro_Kyokumen1.Ban[6].Length == 10, "サイズ違反");
            //    Debug.Assert(this.ro_Kyokumen1.Ban[7].Length == 10, "サイズ違反");
            //    Debug.Assert(this.ro_Kyokumen1.Ban[8].Length == 10, "サイズ違反");
            //    Debug.Assert(this.ro_Kyokumen1.Ban[9].Length == 10, "サイズ違反");
            //}

            this.outFile = outFile;
            this.env = reportEnvironment;
        }
    }


}
