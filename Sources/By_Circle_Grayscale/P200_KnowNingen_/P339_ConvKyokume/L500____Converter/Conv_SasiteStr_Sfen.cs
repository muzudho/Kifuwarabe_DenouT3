using Grayscale.P062_ConvText___.L500____Converter;
using Grayscale.P213_Komasyurui_.L250____Word;
using Grayscale.P213_Komasyurui_.L500____Util;
using Grayscale.P214_Masu_______.L500____Util;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P234_Komahaiyaku.L500____Util;
using Grayscale.P238_Seiza______.L250____Struct;
using Grayscale.P238_Seiza______.L500____Util;
using Grayscale.P258_UtilSky258_.L500____UtilSky;
using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Grayscale.P339_ConvKyokume.L500____Converter
{

    public abstract class Conv_SasiteStr_Sfen
    {
        /// <summary>
        /// 自動で削除される、棋譜ツリー・ログのルートフォルダー名。
        /// </summary>
        public const string KIFU_TREE_LOG_ROOT_FOLDER = "temp_root";

        /// <summary>
        /// ************************************************************************************************************************
        /// SFEN符号表記。
        /// ************************************************************************************************************************
        /// </summary>
        /// <returns></returns>
        public static string ToSasiteStr_Sfen(
            Starbeamable sasite,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            )
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                if (Util_Sky258A.ROOT_SASITE == sasite)
                {
                    sb.Append( Conv_SasiteStr_Sfen.KIFU_TREE_LOG_ROOT_FOLDER);
                    goto gt_EndMethod;
                }

                RO_Star srcKoma = Util_Starlightable.AsKoma(sasite.LongTimeAgo);
                RO_Star dstKoma = Util_Starlightable.AsKoma(sasite.Now);



                //int srcDan;
                //if (!Util_MasuNum.TryMasuToDan(srcKoma.Masu, out srcDan))
                //{
                //    string message = "指定の元マス[" + Util_Masu10.AsMasuNumber(srcKoma.Masu) + "]は、段に変換できません。　：　" + memberName + "." + sourceFilePath + "." + sourceLineNumber;
                //    //LarabeLogger.GetInstance().WriteLineError(LarabeLoggerList.ERROR, message);
                //    throw new Exception(message);
                //}

                //int dan;
                //if (!Util_MasuNum.TryMasuToDan(dstKoma.Masu, out dan))
                //{
                //    string message = "指定の先マス[" + Util_Masu10.AsMasuNumber(dstKoma.Masu) + "]は、段に変換できません。　：　" + memberName + "." + sourceFilePath + "." + sourceLineNumber;
                //    //LarabeLogger.GetInstance().WriteLineError(LarabeLoggerList.ERROR, message);
                //    throw new Exception(message);
                //}


                if (Util_Sky_BoolQuery.IsDaAction(sasite))
                {
                    // 打でした。
                    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                    // (自)筋・(自)段は書かずに、「P*」といった表記で埋めます。
                    sb.Append(Util_Komasyurui14.SfenDa[(int)Util_Komahaiyaku184.Syurui(srcKoma.Haiyaku)]);
                    sb.Append("*");
                }
                else
                {
                    //------------------------------------------------------------
                    // (自)筋
                    //------------------------------------------------------------
                    string strSrcSuji;
                    int srcSuji;
                    if (Util_MasuNum.TryMasuToSuji(srcKoma.Masu, out srcSuji))
                    {
                        strSrcSuji = srcSuji.ToString();
                    }
                    else
                    {
                        strSrcSuji = "Ｎ筋";//エラー表現
                    }
                    sb.Append(strSrcSuji);

                    //------------------------------------------------------------
                    // (自)段
                    //------------------------------------------------------------
                    string strSrcDan2;
                    int srcDan2;
                    if (Util_MasuNum.TryMasuToDan(srcKoma.Masu, out srcDan2))
                    {
                        strSrcDan2 = Conv_Int.ToAlphabet(srcDan2);
                    }
                    else
                    {
                        strSrcDan2 = "Ｎ段";//エラー表現
                    }
                    sb.Append(strSrcDan2);
                }

                //------------------------------------------------------------
                // (至)筋
                //------------------------------------------------------------
                string strSuji;
                int suji2;
                if (Util_MasuNum.TryMasuToSuji(dstKoma.Masu, out suji2))
                {
                    strSuji = suji2.ToString();
                }
                else
                {
                    strSuji = "Ｎ筋";//エラー表現
                }
                sb.Append(strSuji);


                //------------------------------------------------------------
                // (至)段
                //------------------------------------------------------------
                string strDan;
                int dan2;
                if (Util_MasuNum.TryMasuToDan(dstKoma.Masu, out dan2))
                {
                    strDan = Conv_Int.ToAlphabet(dan2);
                }
                else
                {
                    strDan = "Ｎ段";//エラー表現
                }
                sb.Append(strDan);


                //------------------------------------------------------------
                // 成
                //------------------------------------------------------------
                if (Util_Sky_BoolQuery.IsNatta_Sasite(sasite))
                {
                    sb.Append("+");
                }
            }
            catch (Exception e)
            {
                sb.Append(e.Message);//FIXME:
            }

        gt_EndMethod:
            ;
            return sb.ToString();
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// SFEN符号表記。（取った駒付き）
        /// ************************************************************************************************************************
        /// </summary>
        /// <returns></returns>
        public static string ToSasiteStr_Sfen_WithTottaKomasyurui(RO_Starbeam ss)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Conv_SasiteStr_Sfen.ToSasiteStr_Sfen(ss));
            if (Komasyurui14.H00_Null___ != (Komasyurui14)ss.FoodKomaSyurui)
            {
                sb.Append("(");
                sb.Append(ss.FoodKomaSyurui);
                sb.Append(")");
            }

            return sb.ToString();
        }


        /// <summary>
        /// ************************************************************************************************************************
        /// SFEN符号表記。
        /// ************************************************************************************************************************
        /// 
        /// ファイル名にも使えるように、ファイル名に使えない文字を置換します。
        /// </summary>
        /// <returns></returns>
        public static string ToSasiteStr_Sfen_ForFilename(
            Starbeamable sasite,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            )
        {
            string sasiteText = Conv_SasiteStr_Sfen.ToSasiteStr_Sfen(sasite);
            sasiteText = Conv_Filepath.ToEscape(sasiteText);
            return sasiteText;
        }


    }
}
