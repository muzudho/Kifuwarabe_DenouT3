﻿namespace Grayscale.Kifuwarakaku.Entities.Features
{

    public class SyElement_Default : SyElement
    {
        /// <summary>
        /// ビットフィールド。使い方は任意。
        /// </summary>
        public ulong Bitfield { get { return this.bitfield; } }
        protected ulong bitfield;

        public SyElement_Default(ulong bitfield)
        {
            this.bitfield = bitfield;// Conv_Sy.UNKNOWN_BITFIELD;
        }

    }
}
