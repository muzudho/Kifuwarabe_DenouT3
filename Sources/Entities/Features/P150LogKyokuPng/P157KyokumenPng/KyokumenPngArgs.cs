using Grayscale.Kifuwarakaku.Entities.Features;

namespace Grayscale.Kifuwarakaku.Entities.Features
{
    public interface KyokumenPngArgs
    {

        KyokumenPngEnvironment Env { get; }

        /// <summary>
        /// 出力ファイルへのパス。
        /// </summary>
        string OutFile { get; }

        ISfenPosition1 Ro_Kyokumen1 { get; }

        /// <summary>
        /// 移動元升。１一を0とし、１二を1、９九を80とする。なければ-1。範囲外の数字は無視するだけ。
        /// </summary>
        int SrcMasu_orMinusOne { get; }

        /// <summary>
        /// 移動先升。１一を0とし、１二を1、９九を80とする。なければ-1。範囲外の数字は無視するだけ。
        /// </summary>
        int DstMasu_orMinusOne { get; }

        /// <summary>
        /// 取った駒の種類。enum定数を使うこと。
        /// </summary>
        KyokumenPngArgs_FoodOrDropKoma FoodKoma { get; }

        /// <summary>
        /// 打った駒の種類。enum定数を使うこと。
        /// </summary>
        KyokumenPngArgs_FoodOrDropKoma DropKoma { get; }
    }
}
