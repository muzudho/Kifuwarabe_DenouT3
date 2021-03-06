﻿using System.Diagnostics;
using System.Text;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //フィンガー番号

namespace Grayscale.Kifuwarakaku.Entities.Features
{
    public abstract class Util_Sky307
    {

        public static SfenStringImpl ExportSfen(SkyConst src_Sky)
        {
            Debug.Assert(src_Sky.Count == 40, $"sky.Starlights.Count=[{src_Sky.Count}]");//将棋の駒の数

            StartposExporterImpl se = new StartposExporterImpl(src_Sky);
            return new SfenStringImpl($"sfen {Util_StartposExporter.ToSfenstring(se, false)}");
        }

        public static SfenStringImpl ExportSfen_ForDebug(SkyConst src_Sky, bool psideIsBlack)
        {
            StartposExporterImpl se = new StartposExporterImpl(src_Sky);
            return new SfenStringImpl($"sfen {Util_StartposExporter.ToSfenstring(se, true)}");
        }

        /// <summary>
        /// ログが多くなるので、１行で出力されるようにします。
        /// </summary>
        /// <returns></returns>
        public static Json_Val ToJsonVal(SkyConst src_Sky)
        {
            Json_Obj obj = new Json_Obj();

            Json_Arr arr = new Json_Arr();
            src_Sky.Foreach_Starlights((Finger finger, IMoveHalf light, ref bool toBreak) =>
            {
                if (null != light)
                {
                    arr.Add(Conv_Starlight.ToJsonVal(light));
                }
            });

            obj.Add(new Json_Prop("sprite", arr));

            return obj;
        }





        /// <summary>
        /// 「グラフィカル局面ログ」出力用だぜ☆
        /// </summary>
        public static string Json_1Sky(
            SkyConst src_Sky,
            string memo,
            string hint,
            int temezumi_yomiGenTeban_forLog//読み進めている現在の手目済

            //[CallerMemberName] string memberName = "",
            //[CallerFilePath] string sourceFilePath = "",
            //[CallerLineNumber] int sourceLineNumber = 0
            )
        {

            //...(^▽^)さて、局面は☆？
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("[");

            // コメント
            string comment;
            {
                StringBuilder cmt = new StringBuilder();

                // メモ
                cmt.Append(memo);

                comment = cmt.ToString();
            }

            sb.AppendLine($"    {{ act:\"drawText\", text:\"{comment}\", x: 20, y:20 }},");//FIXME: \記号が入ってなければいいが☆

            int hKoma = 0;
            int hMasu_sente = 81;
            int hMasu_gote = 121;

            // 全駒
            src_Sky.Foreach_Starlights((Finger finger, IMoveHalf light, ref bool toBreak) =>
            {

                RO_Star koma = Util_Starlightable.AsKoma(light.Now);

                if (Conv_SyElement.ToOkiba(koma.Masu) == Okiba.Gote_Komadai)
                {
                    // 後手持ち駒
                    sb.AppendLine($"    {{ act:\"drawImg\", img:\"{Util_Converter_LogGraphicEx.PsideKs14_ToString(koma.Pside, koma.Komasyurui, "")}\", masu: {hMasu_gote} }},");//FIXME: \記号が入ってなければいいが☆
                    hMasu_gote++;
                }
                else if (Conv_SyElement.ToOkiba(koma.Masu) == Okiba.Sente_Komadai)
                {
                    // 先手持ち駒
                    sb.AppendLine($"    {{ act:\"drawImg\", img:\"{Util_Converter_LogGraphicEx.PsideKs14_ToString(koma.Pside, koma.Komasyurui, "")}\", masu: {hMasu_sente} }},");//FIXME: \記号が入ってなければいいが☆
                    hMasu_sente++;
                }
                else if (Conv_SyElement.ToOkiba(koma.Masu) == Okiba.ShogiBan)
                {
                    // 盤上
                    sb.AppendLine($"    {{ act:\"drawImg\", img:\"{Util_Converter_LogGraphicEx.PsideKs14_ToString(koma.Pside, koma.Komasyurui, "")}\", masu: {Conv_SyElement.ToMasuNumber(koma.Masu)} }},");//FIXME: \記号が入ってなければいいが☆
                }

                hKoma++;
            });

            sb.AppendLine("],");

            // ...(^▽^)ﾄﾞｳﾀﾞｯﾀｶﾅ～☆
            return sb.ToString();
        }

    }
}
