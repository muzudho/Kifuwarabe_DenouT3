﻿namespace Grayscale.Kifuwarakaku.GuiOfFvLearn.Features
{
    public class N54ListImpl : N54List
    {

        public int P54Next { get { return this.p54Next; } }
        public void SetP54Next(int value)
        {
            this.p54Next = value;
        }
        private int p54Next;

        /// <summary>
        /// ソートしていなくても構わないものとします。
        /// </summary>
        public int[] P54List_unsorted { get { return this.p54List_unsorted; } }
        public void SetP54List_Unsorted(int[] value)
        {
            this.p54List_unsorted = value;
        }
        private int[] p54List_unsorted;


        public N54ListImpl()
        {
            this.SetP54Next(0);
            this.SetP54List_Unsorted(new int[54]);
        }
    }
}
