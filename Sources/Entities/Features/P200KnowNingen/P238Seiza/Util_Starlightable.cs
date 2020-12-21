using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using System;

namespace Grayscale.Kifuwarakaku.Entities.Features
{
    public abstract class Util_Starlightable
    {

        public static RO_Star AsKoma(IMoveSource light)
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
