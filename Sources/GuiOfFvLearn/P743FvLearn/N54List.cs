namespace Grayscale.P743FvLearn.I400N54List
{
    public interface N54List
    {

        int P54Next { get; }
        void SetP54Next(int value);

        /// <summary>
        /// ソートしていなくても構わない使い方をしてください。
        /// </summary>
        int[] P54List_unsorted { get; }
        void SetP54List_Unsorted(int[] value);


    }
}
