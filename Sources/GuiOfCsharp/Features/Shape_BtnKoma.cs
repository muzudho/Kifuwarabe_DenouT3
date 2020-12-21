namespace Grayscale.Kifuwarakaku.GuiOfCsharp.Features
{
    using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

    public interface Shape_BtnKoma : Shape
    {
        string WidgetName { get; }

        Finger Finger { get; set; }
        Finger Koma { get; set; }

    }
}
