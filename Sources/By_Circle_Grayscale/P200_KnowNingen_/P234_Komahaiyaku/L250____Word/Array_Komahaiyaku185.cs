using System;

namespace Grayscale.P234_Komahaiyaku.L250____Word
{
    public static class Array_Komahaiyaku185
    {
        /// <summary>
        /// 列挙型の要素を、配列に格納しておきます。
        /// 
        /// int型→列挙型　への変換を可能にします。
        /// </summary>
        public static Komahaiyaku185[] Items
        {
            get
            {
                return Array_Komahaiyaku185.items;
            }
        }
        private static Komahaiyaku185[] items;

        static Array_Komahaiyaku185()
        {
            Array array = Enum.GetValues(typeof(Komahaiyaku185));

            Array_Komahaiyaku185.items = new Komahaiyaku185[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                Array_Komahaiyaku185.items[i] = (Komahaiyaku185)array.GetValue(i);
            }
        }

    }
}
