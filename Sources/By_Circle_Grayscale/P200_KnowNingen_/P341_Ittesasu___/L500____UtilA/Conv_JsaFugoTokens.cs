using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P056_Syugoron___.L250____Struct;
using Grayscale.P062_ConvText___.L500____Converter;
using Grayscale.P209_KifuJsa____.L500____Word;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P212_ConvPside__.L500____Converter;
using Grayscale.P213_Komasyurui_.L250____Word;
using Grayscale.P213_Komasyurui_.L500____Util;
using Grayscale.P214_Masu_______.L500____Util;
using Grayscale.P215_KomanoKidou.L500____Struct;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P226_Tree_______.L___500_Struct;
using Grayscale.P238_Seiza______.L250____Struct;
using Grayscale.P238_Seiza______.L500____Util;
using Grayscale.P239_ConvWords__.L500____Converter;
using Grayscale.P247_KyokumenWra.L500____Struct;
using Grayscale.P258_UtilSky258_.L500____UtilSky;
using Grayscale.P324_KifuTree___.L___250_Struct;
using System.Diagnostics;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P341_Ittesasu___.L500____UtilA
{
    public abstract class Conv_JsaFugoTokens
    {



        /// <summary>
        /// ************************************************************************************************************************
        /// 次の１手データを作ります(*1)
        /// ************************************************************************************************************************
        /// 
        ///         *1…符号１「▲６８銀上」を元に、「7968」を作ります。
        /// 
        /// ＜[再生]、[コマ送り]で呼び出されます＞
        /// </summary>
        /// <returns></returns>
        public static void ToSasite(
            string strPside, //▲△
            string strSuji, //123…9、１２３…９、一二三…九
            string strDan, //123…9、１２３…９、一二三…九
            string strDou, // “同”
            string strSrcSyurui, //(歩|香|桂|…
            string strMigiHidari,           // 右|左…
            string strAgaruHiku, // 上|引
            string strNariNarazu, //成|不成
            string strDaHyoji, //打
            out Starbeamable sasite,
            KifuTree kifu,
            KwErrorHandler errH
            )
        {
            Node<Starbeamable, KyokumenWrapper> siteiNode = kifu.CurNode;
            SkyConst src_Sky = siteiNode.Value.KyokumenConst;

            //------------------------------
            // 符号確定
            //------------------------------
            MigiHidari migiHidari = Conv_String268.Str_ToMigiHidari(strMigiHidari);
            AgaruHiku agaruHiku = Conv_String268.Str_ToAgaruHiku(strAgaruHiku);            // 上|引
            NariNarazu nariNarazu = Conv_String268.Nari_ToBool(strNariNarazu);//成
            DaHyoji daHyoji = Conv_String268.Str_ToDaHyoji(strDaHyoji);             //打

            Komasyurui14 srcSyurui = Conv_String268.Str_ToSyurui(strSrcSyurui);


            //------------------------------
            // 
            //------------------------------
            Playerside pside = Conv_String268.Pside_ToEnum(strPside);


            SyElement dstMasu;

            if ("同" == strDou)
            {
                // 1手前の筋、段を求めるのに使います。

                RO_Star koma = Util_Starlightable.AsKoma(siteiNode.Key.Now);

                    dstMasu = koma.Masu;
            }
            else
            {
                dstMasu = Util_Masu10.OkibaSujiDanToMasu(
                    Okiba.ShogiBan,
                    Conv_Suji.ToInt(strSuji),
                    Conv_Suji.ToInt(strDan)
                    );
            }

            // TODO: 駒台にあるかもしれない。
            Okiba srcOkiba1 = Okiba.ShogiBan; //Okiba.FUMEI_FUGO_YOMI_CHOKUGO;// Okiba.SHOGIBAN;
            if (DaHyoji.Visible == daHyoji)
            {
                if (Playerside.P2 == pside)
                {
                    srcOkiba1 = Okiba.Gote_Komadai;
                }
                else
                {
                    srcOkiba1 = Okiba.Sente_Komadai;
                }
            }

            // 
            SyElement dst1 = dstMasu;

            Finger foundKoma = Fingers.Error_1;

            //----------
            // 駒台の駒を(明示的に)打つなら
            //----------
            bool utsu = false;//駒台の駒を打つなら真
            if (DaHyoji.Visible == daHyoji)
            {
                utsu = true;
                goto gt_EndShogiban;
            }

            if (Komasyurui14.H01_Fu_____ == srcSyurui)
            {
                #region 歩
                //************************************************************
                // 歩
                //************************************************************

                //----------
                // 候補マス
                //----------
                //┌─┬─┬─┐
                //│  │  │  │
                //├─┼─┼─┤
                //│  │至│  │
                //├─┼─┼─┤
                //│  │Ｅ│  │
                //└─┴─┴─┘
                bool isE = true;

                SySet<SyElement> srcAll = new SySet_Default<SyElement>("J符号");
                if (isE) { srcAll.AddSupersets(KomanoKidou.SrcIppo_戻引(pside, dst1)); }

                if (Query341_OnSky.Query_Koma(pside, srcSyurui, srcAll, kifu.CurNode.Value.KyokumenConst, out foundKoma, errH))
                {
                    srcOkiba1 = Okiba.ShogiBan;
                    goto gt_EndSyurui;
                }
                #endregion
            }
            else if (Komasyurui14.H07_Hisya__ == srcSyurui)
            {
                #region 飛
                //************************************************************
                // 飛
                //************************************************************
                //----------
                // 候補マス
                //----------
                //  ┌─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┐
                //  │  │  │  │  │  │  │  │  │A7│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │  │A6│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │  │A5│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │  │A4│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //　│  │  │  │  │  │  │  │  │A3│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │  │A2│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │  │A1│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │  │A0│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │G7│G6│G5│G4│G3│G2│G1│G0│至│C0│C1│C2│C3│C4│C5│C6│C7│
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │  │E0│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │  │E1│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │  │E2│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │  │E3│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //　│  │  │  │  │  │  │  │  │E4│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │  │E5│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │  │E6│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │  │E7│  │  │  │  │  │  │  │  │
                //  └─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┘
                bool isA = true;
                bool isC = true;
                bool isE = true;
                bool isG = true;

                switch (agaruHiku)
                {
                    case AgaruHiku.Yoru:
                        isA = false;
                        isE = false;
                        break;
                    case AgaruHiku.Agaru:
                        isA = false;
                        isC = false;
                        isG = false;
                        break;
                    case AgaruHiku.Hiku:
                        isC = false;
                        isE = false;
                        isG = false;
                        break;
                }

                switch (migiHidari)
                {
                    case MigiHidari.Migi:
                        isA = false;
                        isE = false;
                        isG = false;
                        break;
                    case MigiHidari.Hidari:
                        isA = false;
                        isC = false;
                        isE = false;
                        break;
                    case MigiHidari.Sugu:
                        isA = false;
                        isC = false;
                        isG = false;
                        break;
                }

                SySet<SyElement> srcAll = new SySet_Default<SyElement>("J符号");
                if (isA) { srcAll.AddSupersets(KomanoKidou.SrcKantu_戻上(pside, dst1)); }
                if (isC) { srcAll.AddSupersets(KomanoKidou.SrcKantu_戻射(pside, dst1)); }
                if (isE) { srcAll.AddSupersets(KomanoKidou.SrcKantu_戻引(pside, dst1)); }
                if (isG) { srcAll.AddSupersets(KomanoKidou.SrcKantu_戻滑(pside, dst1)); }

                if (Query341_OnSky.Query_Koma(pside, srcSyurui, srcAll, kifu.CurNode.Value.KyokumenConst, out foundKoma, errH))
                {
                    srcOkiba1 = Okiba.ShogiBan;
                    goto gt_EndSyurui;
                }
                #endregion
            }
            else if (Komasyurui14.H08_Kaku___ == srcSyurui)
            {
                #region 角
                //************************************************************
                // 角
                //************************************************************
                //----------
                // 候補マス
                //----------
                //  ┌─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┐
                //  │H7│  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │B7│
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │H6│  │  │  │  │  │  │  │  │  │  │  │  │  │B6│  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │H5│  │  │  │  │  │  │  │  │  │  │  │B5│  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │H4│  │  │  │  │  │  │  │  │  │B4│  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //　│  │  │  │  │H3│  │  │  │  │  │  │  │B3│  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │H2│  │  │  │  │  │B2│  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │H1│  │  │  │B1│  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │H0│  │B0│  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │  │至│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │F0│  │D0│  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │F1│  │  │  │D1│  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │F2│  │  │  │  │  │D2│  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │F3│  │  │  │  │  │  │  │D3│  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //　│  │  │  │F4│  │  │  │  │  │  │  │  │  │D4│  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │F5│  │  │  │  │  │  │  │  │  │  │  │D5│  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │F6│  │  │  │  │  │  │  │  │  │  │  │  │  │D6│  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │F7│  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │D7│
                //  └─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┘
                bool isB = true;
                bool isD = true;
                bool isF = true;
                bool isH = true;

                switch (agaruHiku)
                {
                    case AgaruHiku.Yoru:
                        isB = false;
                        isD = false;
                        isF = false;
                        isH = false;
                        break;
                    case AgaruHiku.Agaru:
                        isB = false;
                        isH = false;
                        break;
                    case AgaruHiku.Hiku:
                        isB = false;
                        isH = false;
                        break;
                }

                switch (migiHidari)
                {
                    case MigiHidari.Migi:
                        isF = false;
                        isH = false;
                        break;
                    case MigiHidari.Hidari:
                        isB = false;
                        isD = false;
                        break;
                    case MigiHidari.Sugu:
                        isD = false;
                        isF = false;
                        break;
                }

                SySet_Default<SyElement> srcAll = new SySet_Default<SyElement>("J符号");
                if (isB) { srcAll.AddSupersets(KomanoKidou.SrcKantu_戻昇(pside, dst1)); }
                if (isD) { srcAll.AddSupersets(KomanoKidou.SrcKantu_戻沈(pside, dst1)); }
                if (isF) { srcAll.AddSupersets(KomanoKidou.SrcKantu_戻降(pside, dst1)); }
                if (isH) { srcAll.AddSupersets(KomanoKidou.SrcKantu_戻浮(pside, dst1)); }

                //----------
                // 候補マスＢ
                //----------
                if (Query341_OnSky.Query_Koma(pside, srcSyurui, srcAll, kifu.CurNode.Value.KyokumenConst, out foundKoma, errH))
                {
                    srcOkiba1 = Okiba.ShogiBan;
                    goto gt_EndSyurui;
                }
                #endregion
            }
            else if (Komasyurui14.H02_Kyo____ == srcSyurui)
            {
                #region 香
                //************************************************************
                // 香
                //************************************************************
                //----------
                // 候補マス
                //----------
                //  ┌─┬─┬─┐
                //  │  │至│  │
                //  ├─┼─┼─┤
                //  │  │E0│  │
                //  ├─┼─┼─┤
                //  │  │E1│  │
                //  ├─┼─┼─┤
                //  │  │E2│  │
                //  ├─┼─┼─┤
                //  │  │E3│  │
                //  ├─┼─┼─┤
                //  │  │E4│  │
                //  ├─┼─┼─┤
                //  │  │E5│  │
                //  ├─┼─┼─┤
                //  │  │E6│  │
                //  ├─┼─┼─┤
                //  │  │E7│  │
                //  └─┴─┴─┘
                bool isE = true;

                SySet<SyElement> srcAll = new SySet_Default<SyElement>("J符号");
                if (isE) { srcAll.AddSupersets(KomanoKidou.SrcKantu_戻引(pside, dst1)); }

                if (Query341_OnSky.Query_Koma(pside, srcSyurui, srcAll, kifu.CurNode.Value.KyokumenConst, out foundKoma, errH))
                {
                    srcOkiba1 = Okiba.ShogiBan;
                    goto gt_EndSyurui;
                }
                #endregion
            }
            else if (Komasyurui14.H03_Kei____ == srcSyurui)
            {
                #region 桂
                //************************************************************
                // 桂
                //************************************************************
                //----------
                // 候補マス
                //----------
                //┌─┐　┌─┐
                //│  │　│  │
                //├─┼─┼─┤
                //│　│  │  │
                //├─┼─┼─┤
                //│　│至│  │先手から見た図
                //├─┼─┼─┤
                //│　│  │  │
                //├─┼─┼─┤
                //│Ｊ│　│Ｉ│
                //└─┘　└─┘
                bool isI = true;
                bool isJ = true;

                switch (agaruHiku)
                {
                    case AgaruHiku.Yoru:
                        isI = false;
                        isJ = false;
                        break;
                    case AgaruHiku.Agaru:
                        break;
                    case AgaruHiku.Hiku:
                        isI = false;
                        isJ = false;
                        break;
                }

                switch (migiHidari)
                {
                    case MigiHidari.Migi:
                        isJ = false;
                        break;
                    case MigiHidari.Hidari:
                        isI = false;
                        break;
                    case MigiHidari.Sugu:
                        isI = false;
                        isJ = false;
                        break;
                }

                SySet<SyElement> srcAll = new SySet_Default<SyElement>("J符号");
                if (isI) { srcAll.AddSupersets(KomanoKidou.SrcKeimatobi_戻跳(pside, dst1)); }
                if (isJ) { srcAll.AddSupersets(KomanoKidou.SrcKeimatobi_戻駆(pside, dst1)); }

                if (Query341_OnSky.Query_Koma(pside, srcSyurui, srcAll, kifu.CurNode.Value.KyokumenConst, out foundKoma, errH))
                {
                    srcOkiba1 = Okiba.ShogiBan;
                    goto gt_EndSyurui;
                }
                #endregion
            }
            else if (Komasyurui14.H04_Gin____ == srcSyurui)
            {
                #region 銀
                //************************************************************
                // 銀
                //************************************************************
                //----------
                // 候補マス
                //----------
                //┌─┬─┬─┐
                //│Ｈ│  │Ｂ│
                //├─┼─┼─┤
                //│　│至│  │先手から見た図
                //├─┼─┼─┤
                //│Ｆ│Ｅ│Ｄ│
                //└─┴─┴─┘
                bool isB = true;
                bool isD = true;
                bool isE = true;
                bool isF = true;
                bool isH = true;

                switch (agaruHiku)
                {
                    case AgaruHiku.Yoru:
                        isB = false;
                        isD = false;
                        isE = false;
                        isF = false;
                        isH = false;
                        break;
                    case AgaruHiku.Agaru:
                        isB = false;
                        isH = false;
                        break;
                    case AgaruHiku.Hiku:
                        isD = false;
                        isE = false;
                        isF = false;
                        break;
                }

                switch (migiHidari)
                {
                    case MigiHidari.Migi:
                        isE = false;
                        isF = false;
                        isH = false;
                        break;
                    case MigiHidari.Hidari:
                        isB = false;
                        isD = false;
                        isE = false;
                        break;
                    case MigiHidari.Sugu:
                        isB = false;
                        isD = false;
                        isF = false;
                        isH = false;
                        break;
                }

                SySet<SyElement> srcAll = new SySet_Default<SyElement>("J符号");
                if (isB) { srcAll.AddSupersets(KomanoKidou.SrcIppo_戻昇(pside, dst1)); }
                if (isD) { srcAll.AddSupersets(KomanoKidou.SrcIppo_戻沈(pside, dst1)); }
                if (isE) { srcAll.AddSupersets(KomanoKidou.SrcIppo_戻引(pside, dst1)); }
                if (isF) { srcAll.AddSupersets(KomanoKidou.SrcIppo_戻降(pside, dst1)); }
                if (isH) { srcAll.AddSupersets(KomanoKidou.SrcIppo_戻浮(pside, dst1)); }

                if (Query341_OnSky.Query_Koma(pside, srcSyurui, srcAll, kifu.CurNode.Value.KyokumenConst, out foundKoma, errH))
                {
                    srcOkiba1 = Okiba.ShogiBan;
                    goto gt_EndSyurui;
                }
                #endregion
            }
            else if (
                Komasyurui14.H05_Kin____ == srcSyurui
                || Komasyurui14.H11_Tokin__ == srcSyurui
                || Komasyurui14.H12_NariKyo == srcSyurui
                || Komasyurui14.H13_NariKei == srcSyurui
                || Komasyurui14.H14_NariGin == srcSyurui
                )
            {
                #region △金、△と金、△成香、△成桂、△成銀
                //************************************************************
                // △金、△と金、△成香、△成桂、△成銀
                //************************************************************
                //----------
                // 候補マス
                //----------
                //┌─┬─┬─┐
                //│  │Ａ│  │
                //├─┼─┼─┤
                //│Ｇ│至│Ｃ│先手から見た図
                //├─┼─┼─┤
                //│Ｆ│Ｅ│Ｄ│
                //└─┴─┴─┘
                bool isA = true;
                bool isC = true;
                bool isD = true;
                bool isE = true;
                bool isF = true;
                bool isG = true;

                switch (agaruHiku)
                {
                    case AgaruHiku.Yoru:
                        isA = false;
                        isD = false;
                        isE = false;
                        isF = false;
                        break;
                    case AgaruHiku.Agaru:
                        isA = false;
                        isC = false;
                        isG = false;
                        break;
                    case AgaruHiku.Hiku:
                        isC = false;
                        isD = false;
                        isE = false;
                        isF = false;
                        isG = false;
                        break;
                }

                switch (migiHidari)
                {
                    case MigiHidari.Migi:
                        isA = false;
                        isE = false;
                        isF = false;
                        isG = false;
                        break;
                    case MigiHidari.Hidari:
                        isA = false;
                        isC = false;
                        isD = false;
                        isE = false;
                        break;
                    case MigiHidari.Sugu:
                        isA = false;
                        isC = false;
                        isD = false;
                        isF = false;
                        isG = false;
                        break;
                }

                SySet<SyElement> srcAll = new SySet_Default<SyElement>("J符号");
                if (isA) { srcAll.AddSupersets(KomanoKidou.SrcIppo_戻上(pside, dst1)); }
                if (isC) { srcAll.AddSupersets(KomanoKidou.SrcIppo_戻射(pside, dst1)); }
                if (isD) { srcAll.AddSupersets(KomanoKidou.SrcIppo_戻沈(pside, dst1)); }
                if (isE) { srcAll.AddSupersets(KomanoKidou.SrcIppo_戻引(pside, dst1)); }
                if (isF) { srcAll.AddSupersets(KomanoKidou.SrcIppo_戻降(pside, dst1)); }
                if (isG) { srcAll.AddSupersets(KomanoKidou.SrcIppo_戻滑(pside, dst1)); }

                if (Query341_OnSky.Query_Koma(pside, srcSyurui, srcAll, kifu.CurNode.Value.KyokumenConst, out foundKoma, errH))
                {
                    srcOkiba1 = Okiba.ShogiBan;
                    goto gt_EndSyurui;
                }
                #endregion
            }
            else if (Komasyurui14.H06_Gyoku__ == srcSyurui)
            {
                #region 王
                //************************************************************
                // 王
                //************************************************************

                //----------
                // 候補マス
                //----------
                //┌─┬─┬─┐
                //│Ｈ│Ａ│Ｂ│
                //├─┼─┼─┤
                //│Ｇ│至│Ｃ│先手から見た図
                //├─┼─┼─┤
                //│Ｆ│Ｅ│Ｄ│
                //└─┴─┴─┘
                bool isA = true;
                bool isB = true;
                bool isC = true;
                bool isD = true;
                bool isE = true;
                bool isF = true;
                bool isG = true;
                bool isH = true;

                SySet<SyElement> srcAll = new SySet_Default<SyElement>("J符号");
                if (isA) { srcAll.AddSupersets(KomanoKidou.SrcIppo_戻上(pside, dst1)); }
                if (isB) { srcAll.AddSupersets(KomanoKidou.SrcIppo_戻昇(pside, dst1)); }
                if (isC) { srcAll.AddSupersets(KomanoKidou.SrcIppo_戻射(pside, dst1)); }
                if (isD) { srcAll.AddSupersets(KomanoKidou.SrcIppo_戻沈(pside, dst1)); }
                if (isE) { srcAll.AddSupersets(KomanoKidou.SrcIppo_戻引(pside, dst1)); }
                if (isF) { srcAll.AddSupersets(KomanoKidou.SrcIppo_戻降(pside, dst1)); }
                if (isG) { srcAll.AddSupersets(KomanoKidou.SrcIppo_戻滑(pside, dst1)); }
                if (isH) { srcAll.AddSupersets(KomanoKidou.SrcIppo_戻浮(pside, dst1)); }

                // 王は１つです。
                if (Query341_OnSky.Query_Koma(pside, srcSyurui, srcAll, kifu.CurNode.Value.KyokumenConst, out foundKoma, errH))
                {
                    srcOkiba1 = Okiba.ShogiBan;
                    goto gt_EndSyurui;
                }
                #endregion
            }
            else if (Komasyurui14.H09_Ryu____ == srcSyurui)
            {
                #region 竜
                //************************************************************
                // 竜
                //************************************************************

                //----------
                // 候補マス
                //----------
                //  ┌─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┐
                //  │  │  │  │  │  │  │  │  │A7│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │  │A6│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │  │A5│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │  │A4│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //　│  │  │  │  │  │  │  │  │A3│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │  │A2│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │  │A1│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │Ｈ│A0│Ｂ│  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │G7│G6│G5│G4│G3│G2│G1│G0│至│C0│C1│C2│C3│C4│C5│C6│C7│
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │Ｆ│E0│Ｄ│  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │  │E1│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │  │E2│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │  │E3│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //　│  │  │  │  │  │  │  │  │E4│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │  │E5│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │  │E6│  │  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │  │E7│  │  │  │  │  │  │  │  │
                //  └─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┘
                bool isA = true;
                bool isB = true;
                bool isC = true;
                bool isD = true;
                bool isE = true;
                bool isF = true;
                bool isG = true;
                bool isH = true;

                switch (agaruHiku)
                {
                    case AgaruHiku.Yoru:
                        isA = false;
                        isB = false;
                        isD = false;
                        isE = false;
                        isF = false;
                        isH = false;
                        break;
                    case AgaruHiku.Agaru:
                        isA = false;
                        isB = false;
                        isC = false;
                        isG = false;
                        isH = false;
                        break;
                    case AgaruHiku.Hiku:
                        isC = false;
                        isD = false;
                        isE = false;
                        isF = false;
                        isG = false;
                        break;
                }

                switch (migiHidari)
                {
                    case MigiHidari.Migi:
                        isA = false;
                        isE = false;
                        isF = false;
                        isG = false;
                        isH = false;
                        break;
                    case MigiHidari.Hidari:
                        isA = false;
                        isB = false;
                        isC = false;
                        isD = false;
                        isE = false;
                        break;
                    case MigiHidari.Sugu:
                        isA = false;
                        isB = false;
                        isC = false;
                        isD = false;
                        isF = false;
                        isG = false;
                        isH = false;
                        break;
                }

                SySet<SyElement> srcAll = new SySet_Default<SyElement>("J符号");
                if (isA) { srcAll.AddSupersets(KomanoKidou.SrcKantu_戻上(pside, dst1)); }
                if (isB) { srcAll.AddSupersets(KomanoKidou.SrcIppo_戻昇(pside, dst1)); }
                if (isC) { srcAll.AddSupersets(KomanoKidou.SrcKantu_戻射(pside, dst1)); }
                if (isD) { srcAll.AddSupersets(KomanoKidou.SrcIppo_戻沈(pside, dst1)); }
                if (isE) { srcAll.AddSupersets(KomanoKidou.SrcKantu_戻引(pside, dst1)); }
                if (isF) { srcAll.AddSupersets(KomanoKidou.SrcIppo_戻降(pside, dst1)); }
                if (isG) { srcAll.AddSupersets(KomanoKidou.SrcKantu_戻滑(pside, dst1)); }
                if (isH) { srcAll.AddSupersets(KomanoKidou.SrcIppo_戻浮(pside, dst1)); }

                if (Query341_OnSky.Query_Koma(pside, srcSyurui, srcAll, kifu.CurNode.Value.KyokumenConst, out foundKoma, errH))
                {
                    srcOkiba1 = Okiba.ShogiBan;
                    goto gt_EndSyurui;
                }
                #endregion
            }
            else if (Komasyurui14.H10_Uma____ == srcSyurui)
            {
                #region 馬
                //************************************************************
                // 馬
                //************************************************************
                //----------
                // 候補マス
                //----------
                //  ┌─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┬─┐
                //  │H7│  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │B7│
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │H6│  │  │  │  │  │  │  │  │  │  │  │  │  │B6│  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │H5│  │  │  │  │  │  │  │  │  │  │  │B5│  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │H4│  │  │  │  │  │  │  │  │  │B4│  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //　│  │  │  │  │H3│  │  │  │  │  │  │  │B3│  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │H2│  │  │  │  │  │B2│  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │H1│  │  │  │B1│  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │H0│Ａ│B0│  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │Ｇ│至│Ｃ│  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │  │F0│Ｅ│D0│  │  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │  │F1│  │  │  │D1│  │  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │  │F2│  │  │  │  │  │D2│  │  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │  │  │F3│  │  │  │  │  │  │  │D3│  │  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //　│  │  │  │F4│  │  │  │  │  │  │  │  │  │D4│  │  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │  │F5│  │  │  │  │  │  │  │  │  │  │  │D5│  │  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │  │F6│  │  │  │  │  │  │  │  │  │  │  │  │  │D6│  │
                //  ├─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┼─┤
                //  │F7│  │  │  │  │  │  │  │  │  │  │  │  │  │  │  │D7│
                //  └─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┴─┘
                bool isA = true;
                bool isB = true;
                bool isC = true;
                bool isD = true;
                bool isE = true;
                bool isF = true;
                bool isG = true;
                bool isH = true;

                switch (agaruHiku)
                {
                    case AgaruHiku.Yoru:
                        isA = false;
                        isB = false;
                        isD = false;
                        isE = false;
                        isF = false;
                        isH = false;
                        break;
                    case AgaruHiku.Agaru:
                        isA = false;
                        isB = false;
                        isC = false;
                        isG = false;
                        isH = false;
                        break;
                    case AgaruHiku.Hiku:
                        isC = false;
                        isD = false;
                        isE = false;
                        isF = false;
                        isG = false;
                        break;
                }

                switch (migiHidari)
                {
                    case MigiHidari.Migi:
                        isA = false;
                        isE = false;
                        isF = false;
                        isG = false;
                        isH = false;
                        break;
                    case MigiHidari.Hidari:
                        isA = false;
                        isB = false;
                        isC = false;
                        isD = false;
                        isE = false;
                        break;
                    case MigiHidari.Sugu:
                        isA = false;
                        isB = false;
                        isC = false;
                        isD = false;
                        isF = false;
                        isG = false;
                        isH = false;
                        break;
                }

                SySet<SyElement> srcAll = new SySet_Default<SyElement>("J符号");
                if (isA) { srcAll.AddSupersets(KomanoKidou.SrcIppo_戻上(pside, dst1)); }
                if (isB) { srcAll.AddSupersets(KomanoKidou.SrcKantu_戻昇(pside, dst1)); }
                if (isC) { srcAll.AddSupersets(KomanoKidou.SrcIppo_戻射(pside, dst1)); }
                if (isD) { srcAll.AddSupersets(KomanoKidou.SrcKantu_戻沈(pside, dst1)); }
                if (isE) { srcAll.AddSupersets(KomanoKidou.SrcIppo_戻引(pside, dst1)); }
                if (isF) { srcAll.AddSupersets(KomanoKidou.SrcKantu_戻降(pside, dst1)); }
                if (isG) { srcAll.AddSupersets(KomanoKidou.SrcIppo_戻滑(pside, dst1)); }
                if (isH) { srcAll.AddSupersets(KomanoKidou.SrcKantu_戻浮(pside, dst1)); }

                if (Query341_OnSky.Query_Koma(pside, srcSyurui, srcAll, kifu.CurNode.Value.KyokumenConst, out foundKoma, errH))
                {
                    srcOkiba1 = Okiba.ShogiBan;
                    goto gt_EndSyurui;
                }
                #endregion
            }
            else
            {
                #region エラー
                //************************************************************
                // エラー
                //************************************************************

                #endregion
            }

        gt_EndShogiban:

            if (Fingers.Error_1 == foundKoma && utsu)
            {
                // 駒台の駒を(明示的に)打ちます。
                //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


                Fingers komas = Util_Sky_FingersQuery.InOkibaPsideKomasyuruiNow(
                    siteiNode.Value.KyokumenConst, srcOkiba1, pside, srcSyurui);//(2015-08-05 01:04)変更
                //Fingers komas = Util_Sky051Fingers.Fingers_ByOkibaPsideSyuruiNow_OldSpec(
                //    siteiNode.Value.ToKyokumenConst, srcOkiba1, pside, srcSyurui, errH);

                if (0 < komas.Count)
                {
                    switch (pside)
                    {
                        case Playerside.P2:
                            srcOkiba1 = Okiba.Gote_Komadai;
                            break;
                        case Playerside.P1:
                            srcOkiba1 = Okiba.Sente_Komadai;
                            break;
                        default:
                            srcOkiba1 = Okiba.Empty;
                            break;
                    }

                    foundKoma = komas[0];
                    goto gt_EndSyurui;
                }
            }

        gt_EndSyurui:


            int srcMasuHandle1;

            if (Fingers.Error_1 != foundKoma)
            {
                // 将棋盤の上に駒がありました。
                //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(foundKoma).Now);

                srcMasuHandle1 = Conv_SyElement.ToMasuNumber(koma.Masu);
            }
            else
            {
                // （符号に書かれていませんが）「打」のとき。
                //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                switch (pside)
                {
                    case Playerside.P2:
                        srcOkiba1 = Okiba.Gote_Komadai;
                        break;
                    case Playerside.P1:
                        srcOkiba1 = Okiba.Sente_Komadai;
                        break;
                    default:
                        srcOkiba1 = Okiba.Empty;
                        break;
                }


                Debug.Assert(0 < siteiNode.Value.KyokumenConst.Count, "星の光が 1個未満。");

                // 駒台から、該当する駒を探します。
                Fingers daiKomaFgs = Util_Sky_FingersQuery.InOkibaPsideKomasyuruiNow(
                    siteiNode.Value.KyokumenConst, srcOkiba1, pside, srcSyurui);//(2015-08-05 01:04)変更
                //Fingers daiKomaFgs = Util_Sky051Fingers.Fingers_ByOkibaPsideSyuruiNow_OldSpec(
                //    siteiNode.Value.ToKyokumenConst, srcOkiba1, pside, srcSyurui, errH);//(2014-10-04 12:46)変更


                Debug.Assert(0 < daiKomaFgs.Count, "フィンガーズが 1個未満。 srcOkiba1=[" + srcOkiba1 + "] pside=[" + pside + "] srcSyurui=[" + srcSyurui + "]");

                // 1個はヒットするはず
                Finger hitKoma = daiKomaFgs[0];//▲！[コマ送り]ボタンを連打すると、エラーになります。



                RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(hitKoma).Now);

                srcMasuHandle1 = Conv_SyElement.ToMasuNumber(koma.Masu);
            }


            Komasyurui14 dstSyurui;
            if (NariNarazu.Nari == nariNarazu)
            {
                // 成ります
                dstSyurui = Util_Komasyurui14.NariCaseHandle[(int)srcSyurui];
            }
            else
            {
                // そのままです。
                dstSyurui = srcSyurui;
            }


            // １手を、データにします。
            sasite = new RO_Starbeam(
                //foundKoma,//TODO:

                new RO_Star(
                    pside,
                    Conv_MasuHandle.ToMasu(srcMasuHandle1),
                    srcSyurui
                ),

                new RO_Star(
                    pside,
                    dstMasu,//符号は将棋盤の升目です。
                    dstSyurui
                ),

                Komasyurui14.H00_Null___ // 符号からは、取った駒の種類は分からないんだぜ☆　だがバグではない☆　あとで調べる☆
            );
        }

    }
}
