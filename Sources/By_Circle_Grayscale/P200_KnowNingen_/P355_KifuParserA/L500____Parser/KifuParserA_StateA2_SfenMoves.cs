using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P003_Log________.L500____Struct;
using Grayscale.P146_ConvSfen___.L500____Converter;
using Grayscale.P213_Komasyurui_.L250____Word;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P258_UtilSky258_.L500____UtilSky;
using Grayscale.P325_PnlTaikyoku.L___250_Struct;
using Grayscale.P339_ConvKyokume.L500____Converter;
using Grayscale.P341_Ittesasu___.L___250_OperationA;
using Grayscale.P341_Ittesasu___.L250____OperationA;
using Grayscale.P341_Ittesasu___.L500____UtilA;
using Grayscale.P355_KifuParserA.L___500_Parser;
using System;
using System.Windows.Forms;

namespace Grayscale.P355_KifuParserA.L500____Parser
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
            KwErrorHandler errH
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
                    Starbeamable nextTe = Util_Sky258A.NULL_OBJECT_SASITE;
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
                        if (Conv_SfenSasitesText.ToTokens(
                            genjo.InputLine, out str1, out str2, out str3, out str4, out str5, out rest, errH)
                            &&
                            !(str1=="" && str2=="" && str3=="" && str4=="" && str5=="")
                            )
                        {

                            Conv_SfenSasiteTokens.ToSasite(
                                isHonshogi,
                                str1,  //123456789 か、 PLNSGKRB
                                str2,  //abcdefghi か、 *
                                str3,  //123456789
                                str4,  //abcdefghi
                                str5,  //+
                                out nextTe,
                                model_Taikyoku.Kifu,
                                "棋譜パーサーA_SFENパース1",
                                errH
                                );
                        }
                        else
                        {
                            //>>>>> 「6g6f」形式ではなかった☆

                            //「▲６六歩」形式と想定して、１手だけ読込み
                            if (Conv_JsaFugoText.ToTokens(
                                genjo.InputLine, out str1, out str2, out str3, out str4, out str5, out str6, out str7, out str8, out str9, out rest, model_Taikyoku.Kifu, errH))
                            {
                                if (!(str1 == "" && str2 == "" && str3 == "" && str4 == "" && str5 == "" && str6 == "" && str7 == "" && str8 == "" && str9 == ""))
                                {
                                    Conv_JsaFugoTokens.ToSasite(
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
                                        errH
                                        );
                                }

                            }
                            else
                            {
                                //「6g6f」形式でもなかった☆

                                errH.Logger.WriteLine_Error("（＾△＾）「" + genjo.InputLine + "」vs【" + this.GetType().Name + "】　：　！？　次の一手が読めない☆　inputLine=[" + genjo.InputLine + "]");
                                genjo.ToBreak_Abnormal();
                                goto gt_EndMethod;
                            }

                        }

                        genjo.InputLine = rest;
                    }
                    catch (Exception ex) { Util_OwataMinister.ERROR.DonimoNaranAkirameta(ex, "moves解析中☆"); throw ex; }




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
                            //errH.Logger.WriteLine_AddMemo("一手指し開始　：　残りの符号つ「" + genjo.InputLine + "」");


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
                                errH,
                                "KifuParserA_StateA2_SfenMoves#Execute"
                                );

                            exceptionArea = 1040;

                            exceptionArea = 1050;
                            Util_IttesasuRoutine.Before2(
                                ref ittesasuResult,
                                errH
                                );

                            exceptionArea = 1060;
                            //----------------------------------------
                            // 次ノード追加、次ノードをカレントに。
                            //----------------------------------------
                            exceptionArea = 1070;
                            Util_IttesasuRoutine.After3_ChangeCurrent(
                                model_Taikyoku.Kifu,
                                Conv_SasiteStr_Sfen.ToSasiteStr_Sfen(ittesasuResult.Get_SyuryoNode_OrNull.Key),
                                ittesasuResult.Get_SyuryoNode_OrNull,
                                errH
                                );

                            exceptionArea = 1080;
                            result.Out_newNode_OrNull = ittesasuResult.Get_SyuryoNode_OrNull;
                            //↑↑一手指し

                            //exceptionArea = 1090;
                            //errH.Logger.WriteLine_AddMemo(Util_Sky307.Json_1Sky(
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
                            errH.Logger.WriteLine_Error(message);
                        }

                    }
                    else
                    {
                        genjo.ToBreak_Abnormal();
                        string message = "＼（＾ｏ＾）／teSasiteオブジェクトがない☆！　inputLine=[" + genjo.InputLine + "]";
                        errH.Logger.WriteLine_Error(message);
                        throw new Exception(message);
                    }
                }
                else
                {
                    //errH.Logger.WriteLine_AddMemo("（＾△＾）現局面まで進んだのかだぜ☆？\n" + Util_Sky307.Json_1Sky(
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
                errH.Logger.WriteLine_Error(message);
            }

        gt_EndMethod:
            return genjo.InputLine;
        }

    }
}
