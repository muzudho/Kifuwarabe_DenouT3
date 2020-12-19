﻿using Grayscale.Kifuwarakaku.Entities.Logging;
using Grayscale.P521FeatureVect.I500Struct;
using Grayscale.P521FeatureVect.L500Struct;

namespace Grayscale.P743FvLearn.L420Inspection
{
    /// <summary>
    /// フィーチャー・ベクターの概要をデバッグ出力します。
    /// </summary>
    public abstract class Util_Inspection
    {

        /// <summary>
        /// フィーチャー・ベクターのパラメーターは -999～+999 の整数で保存されています。
        /// 内部的には、これに　bairitu　を掛け算して使用します。
        /// </summary>
        /// <returns></returns>        
        public static float FvParamRange(FeatureVector fv)
        {
            // およそ。
            return 999.0f * fv.Bairitu_NikomaKankeiPp;
        }

        /// <summary>
        /// フィーチャー・ベクターの概要をデバッグ出力します。
        /// </summary>
        public static void Inspection1(FeatureVector fv, ILogTag logTag)
        {
            float negative_length;// 負の数の一番小さな値の絶対値。
            float positive_length;// 正の数の一番大きな値の絶対値。
            bool longest_positive; // 正の方の絶対値の方が大きければ真。
            int negative_items;//負の項目数。平均値を求めるのに使う。
            int positive_items;//正の項目数
            float negative_total;//負の合計。平均値を求めるのに使う。
            float positive_total;//正の合計。
            //float zoom;
            int notZero;
            {
                negative_length = 0.0f;
                positive_length = 0.0f;
                negative_items = 0;
                positive_items = 0;
                negative_total = 0.0f;
                positive_total = 0.0f;
                notZero = 0;
                for (int p1 = 0; p1 < FeatureVectorImpl.CHOSA_KOMOKU_P; p1++)
                {
                    for (int p2 = 0; p2 < FeatureVectorImpl.CHOSA_KOMOKU_P; p2++)
                    {
                        float value = fv.NikomaKankeiPp_ForMemory[p1, p2];
                        if (value < -negative_length)
                        {
                            negative_length = -value;
                        }
                        else if (positive_length < value)
                        {
                            positive_length = value;
                        }

                        if (value != 0.0f)
                        {
                            notZero++;
                        }

                        if (value < 0.0f)
                        {
                            negative_items++;
                            negative_total += value;
                        }
                        else if (0.0f < value)
                        {
                            positive_items++;
                            positive_total += value;
                        }
                    }
                }

                // 長いのは正負のどちらか。
                if (negative_length < positive_length)
                {
                    longest_positive = true;
                }
                else
                {
                    longest_positive = false;
                }
#if DEBUG
                logTag.Logger.WriteLineAddMemo("PP");
                logTag.Logger.WriteLineAddMemo("----------------------------------------");
                logTag.Logger.WriteLineAddMemo("begin");
                logTag.Logger.WriteLineAddMemo("   negative_length =" + negative_length);
                logTag.Logger.WriteLineAddMemo("   positive_length =" + positive_length);
                logTag.Logger.WriteLineAddMemo("   longest_positive=" + longest_positive);
                logTag.Logger.WriteLineAddMemo("   negative_average=" + (negative_items == 0 ? 0 : negative_total / negative_items));
                logTag.Logger.WriteLineAddMemo("   positive_average=" + (positive_items == 0 ? 0 : positive_total / positive_items));
                logTag.Logger.WriteLineAddMemo("   notZero         =" + notZero);
                logTag.Logger.WriteLineAddMemo("----------------------------------------");
#endif
            }
        }
    }
}
