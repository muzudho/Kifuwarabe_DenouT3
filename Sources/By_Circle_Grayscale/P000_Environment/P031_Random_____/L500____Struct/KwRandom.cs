using System;

namespace Grayscale.P031_Random_____.L500____Struct
{
    /// <summary>
    /// きふわらべランダム。
    /// </summary>
    public class KwRandom
    {

        #region 静的プロパティー類

        /// <summary>
        /// 乱数のたね。
        /// </summary>
        public static int Seed
        {
            get
            {
                return KwRandom.seed;
            }
        }
        private static int seed;

        public static Random Random
        {
            get
            {
                return KwRandom.random;
            }
        }
        private static Random random;

        static KwRandom()
        {
            //------------------------------
            // 乱数のたね
            //------------------------------
            //LarabeRandom.seed = 0;//乱数固定
            KwRandom.seed = DateTime.Now.Millisecond;//乱数使用

            random = new Random(KwRandom.seed);
        }

        #endregion

    }
}
