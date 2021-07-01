using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.Kifuwarakaku.Entities.Features
{
    public interface IttesasuResult
    {
        /// <summary>
        /// 指した駒の番号。
        /// </summary>
        Finger FigMovedKoma { get; set; }

        /// <summary>
        /// 取った駒があれば、取った駒の番号。
        /// </summary>
        Finger FigFoodKoma { get; set; }

        /// <summary>
        /// 取った駒があれば、取った駒の種類。
        /// </summary>
        PieceType FoodKomaSyurui { get; set; }

        /// <summary>
        /// 終了ノード。
        /// 「進む」ときは、一手指す局面の「指した後」のツリー・ノード。
        /// 「巻き戻す」のときは、ヌル。
        /// </summary>
        Node<IMove, KyokumenWrapper> Get_SyuryoNode_OrNull { get; }
        Node<IMove, KyokumenWrapper> Set_SyuryoNode_OrNull { set; }

        /// <summary>
        /// 終了ノードの局面データ。
        /// </summary>
        SkyConst Susunda_Sky_orNull { get; set; }

    }
}
