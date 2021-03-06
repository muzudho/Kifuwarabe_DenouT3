﻿using System.Collections.Generic;

namespace Grayscale.Kifuwarakaku.Entities.Features
{

    /// <summary>
    /// 手得ヒストリー。プレイヤー１人分。
    /// </summary>
    public interface TedokuHistory
    {
        /// <summary>
        /// 1筋～9筋の歩。配列は 1筋を0とし、 0～8 を使う。
        /// </summary>
        List<SyElement>[] Fu___ { get; }

        /// <summary>
        /// 4枚の香。配列は 0～3 を使う。
        /// 
        /// 駒を取られるなどして　同一性判定が必要なときは、『控えめ取り』を使う。
        /// つまり、持ち駒に銀２枚ある場合で、盤上に銀を打ちこんだ場合は、手得ヒストリーの空いている銀の内、リストの要素数の長いものから打った、と判定する。
        /// </summary>
        List<SyElement>[] Kyo__ { get; }

        /// <summary>
        /// 4枚の桂。配列は 0～3 を使う。
        /// 
        /// 駒を取られるなどして　同一性判定が必要なときは、『控えめ取り』を使う。
        /// つまり、持ち駒に銀２枚ある場合で、盤上に銀を打ちこんだ場合は、手得ヒストリーの空いている銀の内、リストの要素数の長いものから打った、と判定する。
        /// </summary>
        List<SyElement>[] Kei__ { get; }

        /// <summary>
        /// 4枚の銀。配列は 0～3 を使う。
        /// 
        /// 駒を取られるなどして　同一性判定が必要なときは、『控えめ取り』を使う。
        /// つまり、持ち駒に銀２枚ある場合で、盤上に銀を打ちこんだ場合は、手得ヒストリーの空いている銀の内、リストの要素数の長いものから打った、と判定する。
        /// </summary>
        List<SyElement>[] Gin__ { get; }

        /// <summary>
        /// 4枚の金。配列は 0～3 を使う。
        /// 
        /// 駒を取られるなどして　同一性判定が必要なときは、『控えめ取り』を使う。
        /// つまり、持ち駒に銀２枚ある場合で、盤上に銀を打ちこんだ場合は、手得ヒストリーの空いている銀の内、リストの要素数の長いものから打った、と判定する。
        /// </summary>
        List<SyElement>[] Kin__ { get; }

        /// <summary>
        /// 自玉。
        List<SyElement> Gyoku { get; }

        /// <summary>
        /// 2枚の飛。配列は 0～1 を使う。
        /// 
        /// 駒を取られるなどして　同一性判定が必要なときは、『控えめ取り』を使う。
        /// つまり、持ち駒に銀２枚ある場合で、盤上に銀を打ちこんだ場合は、手得ヒストリーの空いている銀の内、リストの要素数の長いものから打った、と判定する。
        /// </summary>
        List<SyElement>[] Hisya { get; }

        /// <summary>
        /// 2枚の角。配列は 0～1 を使う。
        /// 
        /// 駒を取られるなどして　同一性判定が必要なときは、『控えめ取り』を使う。
        /// つまり、持ち駒に銀２枚ある場合で、盤上に銀を打ちこんだ場合は、手得ヒストリーの空いている銀の内、リストの要素数の長いものから打った、と判定する。
        /// </summary>
        List<SyElement>[] Kaku_ { get; }

    }
}
