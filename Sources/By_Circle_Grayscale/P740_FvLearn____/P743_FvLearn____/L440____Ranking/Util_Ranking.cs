using Grayscale.P521_FeatureVect.L___500_Struct;
using Grayscale.P521_FeatureVect.L500____Struct;
using System.Collections.Generic;

namespace Grayscale.P743_FvLearn____.L440____Ranking
{
    /// <summary>
    /// 評価値を、順位に変換します。
    /// 
    /// 「100点、50点、30点、0点、－100点」
    /// という数字があった場合、これを
    /// 「3,2,1,0,1」
    /// という数字に変換します。
    /// </summary>
    public abstract class Util_Ranking
    {
        /// <summary>
        ///--------------------------------------------------------------------------------
        /// トポロジー的に加工します。 
        ///--------------------------------------------------------------------------------
        /// </summary>
        public static void Perform_Ranking(FeatureVector fv)
        {
            // フィーチャーベクター（ＰＰ）に入っている数字を、0に近い方順に並べます。
            // 0 は　どちらにも含めません。
            List<float> negative_numbers = new List<float>();
            List<float> positive_numbers = new List<float>();
            for (int p1 = 0; p1 < FeatureVectorImpl.CHOSA_KOMOKU_P; p1++)
            {
                for (int p2 = 0; p2 < FeatureVectorImpl.CHOSA_KOMOKU_P; p2++)
                {
                    float value = fv.NikomaKankeiPp_ForMemory[p1, p2];

                    if (0.0d < value)
                    {
                        positive_numbers.Add(value);
                    }
                    else if (value < 0.0d)
                    {
                        negative_numbers.Add(value);
                    }
                    else
                    {
                        // 0 は無視します。
                    }
                }
            }

            // 0に近い方から、遠い方へ。
            positive_numbers.Sort((float a, float b) =>
            {
                return (int)(a - b);
            });
            negative_numbers.Sort((float a, float b) =>
            {
                return (int)(b - a);
            });

            // 置換テーブルを作成します。
            Dictionary<float, int> replaceTable = new Dictionary<float, int>();
            replaceTable.Add(0.0f, 0);

            int renumber = 1;
            foreach (float source in positive_numbers)
            {
                if (!replaceTable.ContainsKey(source))//重複は省く
                {
                    replaceTable.Add(source, renumber);
                    renumber++;
                }
            }
            renumber = -1;
            foreach (float source in negative_numbers)
            {
                if (!replaceTable.ContainsKey(source))//重複は省く
                {
                    replaceTable.Add(source, renumber);
                    renumber--;
                }
            }

            // 実データを全て置換します。
            for (int p1 = 0; p1 < FeatureVectorImpl.CHOSA_KOMOKU_P; p1++)
            {
                for (int p2 = 0; p2 < FeatureVectorImpl.CHOSA_KOMOKU_P; p2++)
                {
                    fv.NikomaKankeiPp_ForMemory[p1, p2] = replaceTable[fv.NikomaKankeiPp_ForMemory[p1, p2]];
                }
            }
        }

    }
}
