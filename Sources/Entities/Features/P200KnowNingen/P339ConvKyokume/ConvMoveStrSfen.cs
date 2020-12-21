using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Grayscale.P339ConvKyokume.L500Converter
{

    public abstract class ConvMoveStrSfen
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
        public static string ToMoveStrSfen(
            IMove move,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            )
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                if (Util_Sky258A.RootMove == move)
                {
                    sb.Append( ConvMoveStrSfen.KIFU_TREE_LOG_ROOT_FOLDER);
                    goto gt_EndMethod;
                }

                RO_Star srcKoma = Util_Starlightable.AsKoma(move.LongTimeAgo);
                RO_Star dstKoma = Util_Starlightable.AsKoma(move.Now);



                //int srcDan;
                //if (!Util_MasuNum.TryMasuToDan(srcKoma.Masu, out srcDan))
                //{
                //    throw new Exception("指定の元マス[" + Util_Masu10.AsMasuNumber(srcKoma.Masu) + "]は、段に変換できません。　：　" + memberName + "." + sourceFilePath + "." + sourceLineNumber);
                //}

                //int dan;
                //if (!Util_MasuNum.TryMasuToDan(dstKoma.Masu, out dan))
                //{
                //    throw new Exception("指定の先マス[" + Util_Masu10.AsMasuNumber(dstKoma.Masu) + "]は、段に変換できません。　：　" + memberName + "." + sourceFilePath + "." + sourceLineNumber);
                //}


                if (Util_Sky_BoolQuery.IsDaAction(move))
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
                if (Util_Sky_BoolQuery.IsNattaMove(move))
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
        public static string ToMoveStrSfenWithTottaKomasyurui(RO_Starbeam ss)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ConvMoveStrSfen.ToMoveStrSfen(ss));
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
        public static string ToMoveStrSfenForFilename(
            IMove move,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            )
        {
            string moveText = ConvMoveStrSfen.ToMoveStrSfen(move);
            moveText = Conv_Filepath.ToEscape(moveText);
            return moveText;
        }


    }
}
