using Grayscale.P213Komasyurui.L250Word;
using Grayscale.P218Starlight.I500Struct;
using Grayscale.P224Sky.L500Struct;
using Grayscale.P226Tree.I500Struct;
using Grayscale.P247KyokumenWra.L500Struct;
using Grayscale.P341_Ittesasu___.L___250_OperationA;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P341_Ittesasu___.L250____OperationA
{
    public class IttesasuResultImpl : IttesasuResult
    {
        public Finger FigMovedKoma { get; set; }

        public Finger FigFoodKoma { get; set; }

        public Node<Starbeamable, KyokumenWrapper> Get_SyuryoNode_OrNull { get { return this.syuryoNode_OrNull; } }
        public Node<Starbeamable, KyokumenWrapper> Set_SyuryoNode_OrNull { set { this.syuryoNode_OrNull = value; } }
        private Node<Starbeamable, KyokumenWrapper> syuryoNode_OrNull;

        public Komasyurui14 FoodKomaSyurui{ get; set; }

        public SkyConst Susunda_Sky_orNull{ get; set; }

        public IttesasuResultImpl(
            Finger figMovedKoma,
            Finger figFoodKoma,
            Node<Starbeamable, KyokumenWrapper> syuryoNode_OrNull,
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
