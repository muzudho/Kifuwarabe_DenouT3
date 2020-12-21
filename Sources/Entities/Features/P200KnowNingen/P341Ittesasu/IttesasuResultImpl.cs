using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.Kifuwarakaku.Entities.Features
{
    public class IttesasuResultImpl : IttesasuResult
    {
        public Finger FigMovedKoma { get; set; }

        public Finger FigFoodKoma { get; set; }

        public Node<IMove, KyokumenWrapper> Get_SyuryoNode_OrNull { get { return this.syuryoNode_OrNull; } }
        public Node<IMove, KyokumenWrapper> Set_SyuryoNode_OrNull { set { this.syuryoNode_OrNull = value; } }
        private Node<IMove, KyokumenWrapper> syuryoNode_OrNull;

        public Komasyurui14 FoodKomaSyurui { get; set; }

        public SkyConst Susunda_Sky_orNull { get; set; }

        public IttesasuResultImpl(
            Finger figMovedKoma,
            Finger figFoodKoma,
            Node<IMove, KyokumenWrapper> syuryoNode_OrNull,
            Komasyurui14 foodKomaSyurui,
            SkyConst susunda_Sky_orNull
            )
        {
            this.FigMovedKoma = figMovedKoma;
            this.FigFoodKoma = figFoodKoma;
            this.syuryoNode_OrNull = syuryoNode_OrNull;
            this.FoodKomaSyurui = foodKomaSyurui;
            this.Susunda_Sky_orNull = susunda_Sky_orNull;
        }

    }
}
