using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P238_Seiza______.L250____Struct;
using System;

namespace Grayscale.P238_Seiza______.L500____Util
{
    public abstract class Util_Starlightable
    {

        public static RO_Star AsKoma(Starlightable light)
        {
            RO_Star koma;

            if (light is RO_Star)
            {
                koma = (RO_Star)light;
            }
            else
            {
                throw new Exception("未対応の星の光クラス");
            }

            return koma;
        }

    }
}
