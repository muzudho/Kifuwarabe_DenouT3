﻿using System;

namespace Grayscale.Kifuwarakaku.Entities.Features
{

    /// <summary>
    /// ************************************************************************************************************************
    /// 駒置き場です。
    /// ************************************************************************************************************************
    /// 
    /// ・将棋盤
    /// ・先手駒台
    /// ・後手駒台
    /// ・駒袋
    /// ・駒はどこにもない
    /// 
    /// の５つです。
    /// 
    /// これをビットフィールドで表現します。
    /// 
    /// [0]００００　駒はどこにもない
    /// [1]０００１　将棋盤
    /// [2]００１０　先手駒台
    /// [4]０１００　後手駒台
    /// [8]１０００　駒袋
    /// </summary>
    [Flags]// Enum型をビット・フィールド化
    public enum Okiba
    {

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// なんの働きもしない値(*1)として。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        Empty = 0,

        /// <summary>
        /// 将棋盤
        /// </summary>
        ShogiBan = 1,

        /// <summary>
        /// 先手駒台
        /// </summary>
        Sente_Komadai = 2,

        /// <summary>
        /// 後手駒台
        /// </summary>
        Gote_Komadai = 4,

        /// <summary>
        /// 駒袋
        /// </summary>
        KomaBukuro = 8,
    }

}
