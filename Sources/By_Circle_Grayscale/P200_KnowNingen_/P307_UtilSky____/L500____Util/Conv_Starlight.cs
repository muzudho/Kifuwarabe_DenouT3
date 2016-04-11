using Grayscale.P206_Json_______.L___500_Struct;
using Grayscale.P206_Json_______.L500____Struct;
using Grayscale.P212_ConvPside__.L500____Converter;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P234_Komahaiyaku.L500____Util;
using Grayscale.P238_Seiza______.L250____Struct;
using Grayscale.P238_Seiza______.L500____Util;
using Grayscale.P239_ConvWords__.L500____Converter;

namespace Grayscale.P307_UtilSky____.L500____Util
{
    public abstract class Conv_Starlight
    {

        public static Json_Val ToJsonVal(Starlight light)
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
