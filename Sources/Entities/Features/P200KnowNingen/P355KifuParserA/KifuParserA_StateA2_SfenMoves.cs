﻿using System;
using Grayscale.Kifuwarakaku.Entities.Logging;
using Grayscale.P140KifuSfen;
using Grayscale.P213Komasyurui.L250Word;
using Grayscale.P218Starlight.I500Struct;
using Grayscale.P224Sky.L500Struct;
using Grayscale.P258UtilSky258.L500UtilSky;
using Grayscale.P325PnlTaikyoku.I250Struct;
using Grayscale.P339ConvKyokume.L500Converter;
using Grayscale.P341Ittesasu.I250OperationA;
using Grayscale.P341Ittesasu.L250OperationA;
using Grayscale.P341Ittesasu.L500UtilA;
using Grayscale.P355_KifuParserA.I500Parser;

namespace Grayscale.P355_KifuParserA.L500Parser
{

    /// <summary>
    /// 「moves」を読込みました。
    /// 
    /// 処理の中で、一手指すルーチンを実行します。
    /// </summary>
    public class KifuParserA_StateA2_SfenMoves : KifuParserA_State
    {

        public static KifuParserA_StateA2_SfenMoves GetInstance()
        {
            if (null == instance)
            {
                instance = new KifuParserA_StateA2_SfenMoves();
            }

            return instance;
        }
        private static KifuParserA_StateA2_SfenMoves instance;


        private KifuParserA_StateA2_SfenMoves()
        {
        }

        public string Execute(
            ref KifuParserA_Result result,
            Model_Taikyoku model_Taikyoku,
            out KifuParserA_State nextState,
            KifuParserA owner,
            KifuParserA_Genjo genjo,
            ILogTag logTag
            )
        {
            int exceptionArea = 0;


            // 現局面。
            SkyConst src_Sky = model_Taikyoku.Kifu.CurNode.Value.KyokumenConst;
//            Debug.Assert(!Util_MasuNum.OnKomabukuro((int)((RO_Star_Koma)src_Sky.StarlightIndexOf((Finger)0).Now).Masu), "カレント、駒が駒袋にあった。");

            bool isHonshogi = true;//FIXME:暫定

            // 現在の手番の開始局面+1
            int korekaranoTemezumi = src_Sky.Temezumi + 1;

            nextState = this;

            try
            {
                if (0 < genjo.InputLine.Trim().Length)
                {
                    IMove nextTe = Util_Sky258A.NullObjectMove;
                    string rest;

                    try
                    {
                        //「6g6f」形式と想定して、１手だけ読込み
                        string str1;
                        string str2;
                        string str3;
                        string str4;
                        string str5;
                        string str6;
                        string str7;
                        string str8;
                        string str9;
                        if (SfenMovesTextConv.ToTokens(
                            genjo.InputLine, out str1, out str2, out str3, out str4, out str5, out rest)
                            &&
                            !(str1=="" && str2=="" && str3=="" && str4=="" && str5=="")
                            )
                        {

                            ConvSfenMoveTokens.ToMove(
                                isHonshogi,
                                str1,  //123456789 か、 PLNSGKRB
                                str2,  //abcdefghi か、 *
                                str3,  //123456789
                                str4,  //abcdefghi
                                str5,  //+
                                out nextTe,
                                model_Taikyoku.Kifu,
                                "棋譜パーサーA_SFENパース1",
                                logTag
                                );
                        }
                        else
                        {
                            //>>>>> 「6g6f」形式ではなかった☆

                            //「▲６六歩」形式と想定して、１手だけ読込み
                            if (Conv_JsaFugoText.ToTokens(
                                genjo.InputLine, out str1, out str2, out str3, out str4, out str5, out str6, out str7, out str8, out str9, out rest, model_Taikyoku.Kifu, logTag))
                            {
                                if (!(str1 == "" && str2 == "" && str3 == "" && str4 == "" && str5 == "" && str6 == "" && str7 == "" && str8 == "" && str9 == ""))
                                {
                                    Conv_JsaFugoTokens.ToMove(
                                        str1,  //▲△
                                        str2,  //123…9、１２３…９、一二三…九
                                        str3,  //123…9、１２３…９、一二三…九
                                        str4,  // “同”
                                        str5,  //(歩|香|桂|…
                                        str6,           // 右|左…
                                        str7,  // 上|引
                                        str8, //成|不成
                                        str9,  //打
                                        out nextTe,
                                        model_Taikyoku.Kifu,
                                        logTag
                                        );
                                }

                            }
                            else
                            {
                                //「6g6f」形式でもなかった☆

                                Logger.WriteLineError(logTag,"（＾△＾）「" + genjo.InputLine + "」vs【" + this.GetType().Name + "】　：　！？　次の一手が読めない☆　inputLine=[" + genjo.InputLine + "]");
                                genjo.ToBreak_Abnormal();
                                goto gt_EndMethod;
                            }

                        }

                        genjo.InputLine = rest;
                    }
                    catch (Exception ex) { Logger.Panic(LogTags.Error, ex, "moves解析中☆"); throw; }




                    if (null != nextTe)
                    {
                        exceptionArea = 1000;

                        IttesasuResult ittesasuResult = new IttesasuResultImpl(Fingers.Error_1, Fingers.Error_1, null, Komasyurui14.H00_Null___, null);

                        try
                        {
                            exceptionArea = 1010;

                            //
                            //FIXME: これが悪さをしていないか☆？
                            //FIXME: スピードが必要なので省略。
                            //Application.DoEvents(); // 時間のかかる処理の間にはこれを挟みます。
                            //

                            exceptionArea = 1020;
                            //------------------------------
                            // ★棋譜読込専用  駒移動
                            //------------------------------
                            //logTag.Logger.WriteLineAddMemo("一手指し開始　：　残りの符号つ「" + genjo.InputLine + "」");


                            exceptionArea = 1030;
                            //
                            //↓↓将棋エンジンが一手指し（進める）
                            //
                            Util_IttesasuRoutine.Before1(
                                new IttesasuArgImpl(
                                    model_Taikyoku.Kifu.CurNode.Value,
                                    src_Sky.KaisiPside,
                                    nextTe,//FIXME: if文で分けているので、これがヌルなはずはないと思うが。
                                    korekaranoTemezumi//これから作る局面の、手目済み。
                                ),
                                out ittesasuResult,
                                "KifuParserA_StateA2_SfenMoves#Execute"
                                );

                            exceptionArea = 1040;

                            exceptionArea = 1050;
                            Util_IttesasuRoutine.Before2(
                                ref ittesasuResult
                                );

                            exceptionArea = 1060;
                            //----------------------------------------
                            // 次ノード追加、次ノードをカレントに。
                            //----------------------------------------
                            exceptionArea = 1070;
                            Util_IttesasuRoutine.After3_ChangeCurrent(
                                model_Taikyoku.Kifu,
                                ConvMoveStrSfen.ToMoveStrSfen(ittesasuResult.Get_SyuryoNode_OrNull.Key),
                                ittesasuResult.Get_SyuryoNode_OrNull
                                );

                            exceptionArea = 1080;
                            result.Out_newNode_OrNull = ittesasuResult.Get_SyuryoNode_OrNull;
                            //↑↑一手指し

                            //exceptionArea = 1090;
                            //logTag.Logger.WriteLineAddMemo(Util_Sky307.Json_1Sky(
                            //    src_Sky,
                            //    "一手指し終了",
                            //    "SFENパース2",
                            //    src_Sky.Temezumi//読み進めている現在の手目
                            //    ));


                        }
                        catch (Exception ex)
                        {
                            //>>>>> エラーが起こりました。

                            // どうにもできないので  ログだけ取って無視します。
                            string message = this.GetType().Name + "#Execute（B）： exceptionArea=" + exceptionArea + "\n" + ex.GetType().Name + "：" + ex.Message;
                            Logger.WriteLineError(logTag, message);
                        }

                    }
                    else
                    {
                        genjo.ToBreak_Abnormal();
                        throw new Exception($"＼（＾ｏ＾）／Moveオブジェクトがない☆！　inputLine=[{genjo.InputLine}]");
                    }
                }
                else
                {
                    //logTag.Logger.WriteLineAddMemo("（＾△＾）現局面まで進んだのかだぜ☆？\n" + Util_Sky307.Json_1Sky(
                    //    src_Sky,
                    //    "棋譜パース",
                    //    "SFENパース3",
                    //    src_Sky.Temezumi//読み進めている現在の手目
                    //    ));
                    genjo.ToBreak_Normal();//棋譜パーサーＡの、唯一の正常終了のしかた。
                }
            }
            catch (Exception ex)
            {
                // エラーが起こりました。
                //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                // どうにもできないので  ログだけ取って無視します。
                string message = this.GetType().Name + "#Execute：" + ex.GetType().Name + "：" + ex.Message;
                Logger.WriteLineError(logTag,message);
            }

        gt_EndMethod:
            return genjo.InputLine;
        }

    }
}