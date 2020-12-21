namespace Grayscale.Kifuwarakaku.GuiOfCsharp.Features
{
    /// <summary>
    /// クリックされたときの動きです。
    /// </summary>
    /// <param name="shape_PnlTaikyoku"></param>
    public delegate void DELEGATE_MouseHitEvent(
         object obj_shogiGui //ShogiGui
        , object userWidget // UerWidget
        , object shape_BtnKoma_Selected //Shape_BtnKoma
    );

    ///// <summary>
    ///// この名前空間は、デリゲートを定義しているだけ。使っていないインターフェース。
    ///// </summary>
    //interface ShogibanGuiEvent
    //{
    //}
}
