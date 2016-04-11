using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P003_Log________.L500____Struct;
using Grayscale.P145_SfenStruct_.L___250_Struct;
using Grayscale.P145_SfenStruct_.L250____Struct;
using Grayscale.P146_ConvSfen___.L500____Converter;
using Grayscale.P157_KyokumenPng.L___500_Struct;
using Grayscale.P157_KyokumenPng.L500____Struct;
using Grayscale.P158_LogKyokuPng.L250____UtilPaint;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Grayscale.P158_LogKyokuPng.L500____UtilWriter
{

    /// <summary>
    /// 局面.pngを書き出します。
    /// </summary>
    public abstract class Util_KyokumenPng_Writer
    {

        /// <summary>
        /// SFEN文字列と、出力ファイル名を指定することで、局面の画像ログを出力します。
        /// </summary>
        /// <param name="sfenstring"></param>
        /// <param name="outFile"></param>
        /// <param name="reportEnvironment"></param>
        /// <returns></returns>
        public static bool Write_ForTest(
            string sfenstring,
            string relFolder,
            string outFile,
            KyokumenPngEnvironment reportEnvironment
            )
        {
            bool successful = true;


            //System.Windows.Forms.MessageBox.Show(sfenstring + "\n" + outFile, "局面PNG書き出し");

            //
            // SFEN → RO_SfenStartpos
            //
            RO_Kyokumen2_ForTokenize ro_SfenStartpos;
            string rest;
            if (!Conv_Sfenstring146.ToKyokumen2(sfenstring, out rest, out ro_SfenStartpos))
            {
                //System.Windows.Forms.MessageBox.Show(sfenstring,"sfenstringパース失敗");
                successful = false;
                goto gt_EndMethod;
            }

            // テスト・プログラム用
            Util_KyokumenPng_Writer.Write1(
                ro_SfenStartpos.ToKyokumen1(),
                -1,//移動元升不明
                -1,//移動先升不明
                KyokumenPngArgs_FoodOrDropKoma.UNKNOWN,//取った駒不明
                "",//指し手SFEN
                relFolder,
                outFile,
                reportEnvironment,
                Util_OwataMinister.TEST_PROGRAM
                );

        gt_EndMethod:
            return successful;
        }

        /// <summary>
        /// 局面データと、出力ファイル名を指定することで、局面の画像ログを出力します。
        /// </summary>
        /// <param name="ro_Kyokumen1"></param>
        /// <param name="dstMasu_orMinusOne">あれば、移動先升番号。なければ -1。</param>
        /// <param name="appendFilepath"></param>
        /// <param name="outFile"></param>
        /// <param name="reportEnvironment"></param>
        /// <returns></returns>
        public static bool Write1(
            RO_Kyokumen1_ForFormat ro_Kyokumen1,
            int srcMasu_orMinusOne,
            int dstMasu_orMinusOne,
            KyokumenPngArgs_FoodOrDropKoma foodKoma,
            string sfenSasiteStr_orEmpty,
            string appendFilepath,
            string outFile,
            KyokumenPngEnvironment reportEnvironment,
            KwErrorHandler errH
            )
        {
            bool successful = true;


            //----------------------------------------
            // 打った駒を調べる
            //----------------------------------------
            KyokumenPngArgs_FoodOrDropKoma dropKoma;
            {
                dropKoma = KyokumenPngArgs_FoodOrDropKoma.UNKNOWN;
                if ("" != sfenSasiteStr_orEmpty)
                {
                    string moji1;
                    string moji2;
                    string moji3;
                    string moji4;
                    string moji5;
                    string rest;
                    Conv_SfenSasitesText.ToTokens(
                        sfenSasiteStr_orEmpty,
                        out moji1,
                        out moji2,
                        out moji3,
                        out moji4,
                        out moji5,
                        out rest,
                        errH
                        );
                    //System.Console.WriteLine("moji1=[" + moji1 + "] moji2=[" + moji2 + "] moji3=[" + moji3 + "] moji4=[" + moji4 + "] moji5=[" + moji5 + "] rest=[" + rest + "]");

                    if (moji2 == "*")
                    {
                        switch (moji1)
                        {
                            case Word_Sfen.P_PAWN__: dropKoma = KyokumenPngArgs_FoodOrDropKoma.FU__; break;
                            case Word_Sfen.L_LANCE_: dropKoma = KyokumenPngArgs_FoodOrDropKoma.KYO_; break;
                            case Word_Sfen.N_KNIGHT: dropKoma = KyokumenPngArgs_FoodOrDropKoma.KEI_; break;
                            case Word_Sfen.S_SILVER: dropKoma = KyokumenPngArgs_FoodOrDropKoma.GIN_; break;
                            case Word_Sfen.G_GOLD__: dropKoma = KyokumenPngArgs_FoodOrDropKoma.KIN_; break;
                            case Word_Sfen.B_BISHOP: dropKoma = KyokumenPngArgs_FoodOrDropKoma.KAKU; break;
                            case Word_Sfen.R_ROOK__: dropKoma = KyokumenPngArgs_FoodOrDropKoma.HI__; break;
                            default: break;
                        }
                    }
                    else
                    {
                        dropKoma = KyokumenPngArgs_FoodOrDropKoma.NONE;
                    }
                }
            }


            //----------------------------------------
            // ファイルに使える文字に変換
            //----------------------------------------
            appendFilepath = appendFilepath.Replace('*', '＊');
            outFile = outFile.Replace('*', '＊');

            KyokumenPngArgs args = new KyokumenPngArgsImpl(
                ro_Kyokumen1,
                srcMasu_orMinusOne,
                dstMasu_orMinusOne,
                foodKoma,
                dropKoma,
                outFile,
                reportEnvironment
                );

            // 局面画像を描きだします。
            Bitmap bmp = new Bitmap(
                2 * (args.Env.KmW + 2 * args.Env.SjW) + Util_KyokumenPngPainter.BN_SUJIS * args.Env.KmW + Util_KyokumenPngPainter.BN_BRD_R_W,
                Util_KyokumenPngPainter.BN_DANS * args.Env.KmH + Util_KyokumenPngPainter.BN_BRD_B_W
                );


            Util_KyokumenPngPainter.Paint(Graphics.FromImage(bmp), args);


            //args.Env.OutFolder + args.OutFile, "bmp.Save"
            string filepath = args.Env.OutFolder + appendFilepath + args.OutFile;
            // フォルダーが無ければ、作る必要があります。
            {
                DirectoryInfo dirInfo = Directory.GetParent(filepath);
                if (!Directory.Exists(dirInfo.FullName))
                {
                    Directory.CreateDirectory(dirInfo.FullName);
                }
            }

            try
            {
                bmp.Save(filepath);
            }
            catch (ExternalException ex)
            {
                MessageBox.Show(ex.GetType().Name + "：" + ex.Message + "\n filepath：" + filepath);
                throw ex;
            }

            return successful;
        }

    }
}
