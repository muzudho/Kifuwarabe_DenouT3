using System.Collections.Generic;

namespace Grayscale.P056_Syugoron___.L___250_Struct
{
    /// <summary>
    /// 二項演算。
    /// </summary>
    /// <param name="element1"></param>
    /// <param name="element2"></param>
    /// <returns></returns>
    public delegate bool DLGT_SyElement_BynaryOperate(SyElement operand1, SyElement operand2);


    /// <summary>
    /// 素朴集合論の「集合」。
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    public interface SySet<T1>
        where T1 : SyElement
    {

        string Word { get; }


        SySet<T1> Clone();

        /// <summary>
        /// 将棋盤上の枡番号。
        /// 
        /// これは、筋も木っ端微塵に切ってしまっているので、順序はばらばら。
        /// 
        /// 要素ｘ、集合Ｘが「ｘ∈Ｘ」の関係とき、全てのｘを取る操作です。
        /// 
        /// 例えばＡが、「１→２」と、「３」を持っているとき、
        /// ｘとは「１」「２」「３」といった最も細かい各要素のどれか１つのことです。
        /// ｘを全部拾うということです。
        /// </summary>
        IEnumerable<T1> Elements
        {
            get;
        }

        /// <summary>
        /// 将棋盤上の枡番号。
        /// 
        /// これは、筋も残しているので、先頭から順番に読むことを想定しています。
        /// 
        /// 集合Ａ、Ｂは「Ａ⊆Ｂ」の関係とし、ＡがＢを取る操作です。
        /// 
        /// 例えばＡが、「１→２」と、「３」を持っているとき、
        /// Ｂとは「１→２」「３」といった子要素のことです。
        /// </summary>
        IEnumerable<SySet<T1>> Supersets
        {
            get;
        }

        int Count
        {
            get;
        }



        #region 一致系
        
        /// <summary>
        /// ************************************************************************************************************************
        /// この図形の指定の場所に、指定のドットが全て打たれているか。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        bool ContainsAll(SySet<T1> superset);

        /// <summary>
        /// ************************************************************************************************************************
        /// この図形に、指定のドットが含まれているか。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        bool Contains(T1 element);

        /// <summary>
        /// ************************************************************************************************************************
        /// 空集合なら真です。
        /// ************************************************************************************************************************
        /// </summary>
        /// <returns></returns>
        bool IsEmptySet();

        #endregion




        #region 操作系

        /// <summary>
        /// この集合Ａと、その要素ａが「ａ∈Ａ」の関係のとき、ａを１つ仲間に加える操作です。
        /// 
        /// 例えばＡが、「１→２」と、「３」を持っているときに、
        /// ここに「４」を追加する、といった操作がこれです。
        /// </summary>
        /// <param name="masuHandle"></param>
        void AddElement(T1 element);

        /// <summary>
        /// この集合Ａと、引数に指定する集合Ｂの関係が「Ａ⊆Ｂ」になるようにする操作です。
        /// </summary>
        /// <param name="supersetB"></param>
        void AddSupersets(SySet<T1> supersetB);

        /// <summary>
        /// この集合Ａと、その要素ａが「ａ∈Ａ」の関係のとき、ａを１つ仲間から外す操作です。
        /// 
        /// 例えばＡが、「１→２」と、「３」を持っているときに、
        /// ここから「２」を外して　「１」と「３」だけにする、といった操作がこれです。
        /// 
        /// もしこのときＡが、「１→２→３」を持っていた場合は、
        /// 「２」が外れたことによって「２→３」は丸ごと消え、「１」だけが残ります。
        /// </summary>
        /// <param name="masuHandle"></param>
        void Minus_Closed(T1 bElement, DLGT_SyElement_BynaryOperate dlgt_equals);

        /// <summary>
        /// もし順序があるならば、「ａ　＝　１→２→３→４」のときに
        /// 「ａ　ＲｅｍｏｖｅＥｌｅｍｅｎｔ＿ＯｖｅｒＴｈｅｒｅ（　２　）」とすれば、
        /// 答えは「３→４」
        /// となる操作がこれです。
        /// 
        /// ｂを含めず、それより後ろを切る、という操作です。
        /// 順序がなければ、ＲｅｍｏｖｅＥｌｅｍｅｎｔと同等です。
        /// </summary>
        /// <param name="masuHandle"></param>
        void Minus_Opened(T1 bElement, DLGT_SyElement_BynaryOperate dlgt_equals);

        /// <summary>
        /// this - b = c
        /// 
        /// この集合のメンバーから、指定の集合のメンバーを削除します。
        /// </summary>
        /// <param name="masus"></param>
        /// <returns></returns>
        SySet<T1> Minus_Closed(SySet<T1> b, DLGT_SyElement_BynaryOperate dlgt_equals);

        /// <summary>
        /// this から、 b以降を削除する単項演算子です。
        /// 
        /// この集合のメンバーから、指定の集合のメンバー「より向こう」を削除します。
        /// </summary>
        /// <param name="masus"></param>
        /// <returns></returns>
        void MinusMe_Opened(SySet<T1> b, DLGT_SyElement_BynaryOperate dlgt_equals);

        #endregion


        ///// <summary>
        ///// ２要素を比較します。
        ///// </summary>
        ///// <param name="operand1"></param>
        ///// <param name="operand2"></param>
        ///// <param name="dlgt_SyElement_BynaryOperate"></param>
        ///// <returns></returns>
        //bool BynaryOperate(SyElement operand1, SyElement operand2, DLGT_SyElement_BynaryOperate dlgt_SyElement_BynaryOperate);

    }
}
