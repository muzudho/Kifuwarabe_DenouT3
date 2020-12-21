using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.P341Ittesasu.I250OperationA;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P341Ittesasu.L250OperationA
{
    public class IttemodosuResultImpl : IttemodosuResult
    {
        public Finger FigMovedKoma { get; set; }

        public Finger FigFoodKoma { get; set; }

        public Node<IMove, KyokumenWrapper> SyuryoNode_OrNull { get; set; }

        public Komasyurui14 FoodKomaSyurui{ get; set; }

        public SkyConst Susunda_Sky_orNull{ get; set; }

        public IttemodosuResultImpl(
            Finger figMovedKoma,
            Finger figFoodKoma,
            Node<IMove, KyokumenWrapper> out_newNode_OrNull,
            Komasyurui14 foodKomaSyurui,
            SkyConst susunda_Sky_orNull
            )
        {
            this.FigMovedKoma = figMovedKoma;
            this.FigFoodKoma = figFoodKoma;
            this.SyuryoNode_OrNull = out_newNode_OrNull;
            this.FoodKomaSyurui = foodKomaSyurui;
            this.Susunda_Sky_orNull = susunda_Sky_orNull;
        }

    }
}
