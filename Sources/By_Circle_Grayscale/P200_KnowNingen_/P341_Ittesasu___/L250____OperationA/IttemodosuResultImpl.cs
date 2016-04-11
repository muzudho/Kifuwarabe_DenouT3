using Grayscale.P213_Komasyurui_.L250____Word;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P226_Tree_______.L___500_Struct;
using Grayscale.P247_KyokumenWra.L500____Struct;
using Grayscale.P341_Ittesasu___.L___250_OperationA;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P341_Ittesasu___.L250____OperationA
{
    public class IttemodosuResultImpl : IttemodosuResult
    {
        public Finger FigMovedKoma { get; set; }

        public Finger FigFoodKoma { get; set; }

        public Node<Starbeamable, KyokumenWrapper> SyuryoNode_OrNull { get; set; }

        public Komasyurui14 FoodKomaSyurui{ get; set; }

        public SkyConst Susunda_Sky_orNull{ get; set; }

        public IttemodosuResultImpl(
            Finger figMovedKoma,
            Finger figFoodKoma,
            Node<Starbeamable, KyokumenWrapper> out_newNode_OrNull,
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
