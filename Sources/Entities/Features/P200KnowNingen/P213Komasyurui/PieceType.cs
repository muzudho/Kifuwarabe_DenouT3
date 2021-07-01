
namespace Grayscale.Kifuwarakaku.Entities.Features
{
    /// <summary>
    /// 先後無しの駒の種類（14個）
    /// </summary>
    public enum PieceType
    {
        None = 0,// (H00) 符号読取時など、取った駒が分からない状況など☆
        P = 1,// (H01) 歩
        L,// (H02) 香
        N,// (H03) 桂
        S,// (H04) 銀
        G,// (H05) 金
        K,// (H06) 王
        R,// (H07) 飛車
        B,// (H08) 角
        PR,// (H09) 竜
        PB,// (H10) 馬
        PP,// (H11) と金
        PL,// (H12) 成香
        PN,// (H13) 成桂
        PS// (H14) 成銀
    }
}
