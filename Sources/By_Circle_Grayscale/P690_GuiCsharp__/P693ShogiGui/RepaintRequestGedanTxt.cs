
namespace Grayscale.P693ShogiGui.I499Repaint
{
    /// <summary>
    /// 下段の入力欄。
    /// </summary>
    public enum RepaintRequestGedanTxt
    {
        /// <summary>
        /// 要求なし
        /// </summary>
        None,

        /// <summary>
        /// 出力欄をクリアーしたいとき
        /// </summary>
        Clear,

        /// <summary>
        /// 出力欄に棋譜を出力したいとき
        /// </summary>
        Kifu
    }
}
