using Grayscale.P003Log.I500Struct;

namespace Grayscale.P693ShogiGui.I491Event
{
    /// <summary>
    /// クリックされたときの動きです。
    /// </summary>
    /// <param name="shape_PnlTaikyoku"></param>
    public delegate void DELEGATE_MouseHitEvent(
         object obj_shogiGui //ShogiGui
        , object userWidget // UerWidget
        , object shape_BtnKoma_Selected //Shape_BtnKoma
        , IKwErrorHandler errH2
    );

    ///// <summary>
    ///// この名前空間は、デリゲートを定義しているだけ。使っていないインターフェース。
    ///// </summary>
    //interface ShogibanGuiEvent
    //{
    //}
}
