using System;

namespace Grayscale.Kifuwarakaku.Entities.Features
{
    public static class Array_Komasyurui
    {
        /// <summary>
        /// 列挙型の要素を、配列に格納しておきます。
        /// 
        /// int型→列挙型　への変換を可能にします。
        /// </summary>
        public static PieceType[] Items_AllElements
        {
            get
            {
                return Array_Komasyurui.items_All;
            }
        }
        private static PieceType[] items_All;


        public static PieceType[] Items_OnKoma
        {
            get
            {
                return Array_Komasyurui.items_OnKoma;
            }
        }
        private static PieceType[] items_OnKoma;//[0]ヌルと[15]エラーを省きます。


        static Array_Komasyurui()
        {
            Array array = Enum.GetValues(typeof(PieceType));


            Array_Komasyurui.items_All = new PieceType[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                Array_Komasyurui.items_All[i] = (PieceType)array.GetValue(i);
            }


            Array_Komasyurui.items_OnKoma = new PieceType[array.Length - 2];//[0]ヌルと[15]エラーを省きます。
            for (int i = 1; i < array.Length - 1; i++)//[0]ヌルと[15]エラーを省きます。
            {
                Array_Komasyurui.items_OnKoma[i - 1] = (PieceType)array.GetValue(i);
            }
        }


        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 持駒７種類
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public static readonly PieceType[] MotiKoma7Syurui = new PieceType[]{
            PieceType.P,
            PieceType.L,
            PieceType.N,
            PieceType.S,
            PieceType.G,
            PieceType.R,
            PieceType.B
        };

    }
}
