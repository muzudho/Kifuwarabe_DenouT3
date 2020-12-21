using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;

namespace Grayscale.P307UtilSky.L500Util
{
    public abstract class Conv_Starlight
    {

        public static Json_Val ToJsonVal(IMoveHalf light)
        {
            Json_Obj obj = new Json_Obj();


            RO_Star koma = Util_Starlightable.AsKoma(light.Now);


            // プレイヤーサイド
            obj.Add(new Json_Prop("pside", Conv_Playerside.ToSankaku(koma.Pside)));// ▲△

            // マス  
            obj.Add(new Json_Prop("masu", Conv_SyElement.ToMasuNumber(koma.Masu)));// ▲△

            // 駒の種類。歩、香、桂…。
            obj.Add(new Json_Prop("syurui", Conv_Komasyurui.ToStr_Ichimoji(Util_Komahaiyaku184.Syurui(koma.Haiyaku))));// ▲△

            return obj;
        }

    }
}
