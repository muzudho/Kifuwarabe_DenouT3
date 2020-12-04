using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P693ShogiGui.I080Shape
{
    public interface Shape_BtnKoma : Shape
    {
        string WidgetName { get; }

        Finger Finger { get; set; }
        Finger Koma { get; set; }

    }
}
