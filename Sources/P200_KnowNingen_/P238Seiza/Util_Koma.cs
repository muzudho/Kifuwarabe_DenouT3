﻿using Grayscale.P218Starlight.I500Struct;
using Grayscale.P224Sky.L500Struct;
using Grayscale.P238Seiza.L250Struct;
using System;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //フィンガー番号

namespace Grayscale.P238Seiza.L500Util
{
    public class Util_Koma
    {

        #region 定数

        /// <summary>
        /// 働かない値として、筋に埋めておくためのものです。8～-8 程度だと、角等の射程に入るので、大きく外した数字をフラグに使います。
        /// </summary>
        public const int CTRL_NOTHING_PROPERTY_SUJI = int.MinValue;

        /// <summary>
        /// 働かない値として、段に埋めておくためのものです。8～-8 程度だと、角等の射程に入るので、大きく外した数字をフラグに使います。
        /// </summary>
        public const int CTRL_NOTHING_PROPERTY_DAN = int.MinValue;

        #endregion



        public static RO_Star FromFinger(SkyConst src_Sky,Finger finger)
        {
            RO_Star koma;

            IMoveSource lightable = src_Sky.StarlightIndexOf(finger).Now;

            if (lightable is RO_Star)
            {
                koma = (RO_Star)lightable;
            }
            else
            {
                throw new Exception("未対応の星の光クラス");
            }

            return koma;
        }

    }
}
