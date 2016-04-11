using Grayscale.P321_KyokumHyoka.L___250_Struct;

namespace Grayscale.P321_KyokumHyoka.L250____Struct
{

    /// <summary>a
    /// 指定局面限定での評価明細の項目値。内訳は省略可能。
    /// 
    /// 評価を付けます。
    /// </summary>
    public class KyHyokaMeisai_KoumokuImpl : KyHyokaMeisai_Koumoku
    {
        /// <summary>
        /// 内訳。
        /// </summary>
        public string Utiwake { get { return this.utiwake; } }
        private string utiwake;

        /// <summary>
        /// 例えばスコアなど。
        /// </summary>
        public float UtiwakeValue { get { return this.utiwakeValue; } }
        private float utiwakeValue;


        public KyHyokaMeisai_KoumokuImpl(string utiwake, float utiwakeValue)
        {
            this.utiwake = utiwake;
            this.utiwakeValue = utiwakeValue;
        }

    }
}
