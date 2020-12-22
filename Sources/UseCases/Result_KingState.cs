namespace Grayscale.Kifuwarakaku.UseCases
{
    public enum Result_KingState
    {
        /// <summary>
        /// 無判断
        /// </summary>
        Empty,

        /// <summary>
        /// 先手王が、将棋盤上にいない場合
        /// </summary>
        Lost_SenteOh,

        /// <summary>
        /// 後手王が、将棋盤上にいない場合
        /// </summary>
        Lost_GoteOh
    }
}
