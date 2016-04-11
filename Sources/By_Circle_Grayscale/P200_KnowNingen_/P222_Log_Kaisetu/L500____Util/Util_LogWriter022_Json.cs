using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P222_Log_Kaisetu.L250____Struct;
using System.Text;

namespace Grayscale.P222_Log_Kaisetu.L500____Util
{
    public abstract class Util_LogWriter_Json
    {
        /// <summary>
        /// 棋譜ログをJSON形式で出力します。
        /// </summary>
        /// <returns></returns>
        public static string ToJsonStr(KaisetuBoard board)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("[");

            // 色つきマス
            {
                // 青
                {
                    sb.AppendLine("{ \"act\":\"colorMasu\", \"style\":\"rgba(100,100,240,0.5)\" },");

                    foreach (int masu in board.Masu_theMove)
                    {
                        sb.AppendLine("{ \"act\":\"drawMasu\" , \"masu\":" + masu.ToString() + " },");
                    }
                }

                // 赤
                {
                    sb.AppendLine("{ \"act\":\"colorMasu\", \"style\":\"rgba(240,100,100,0.5)\" },");

                    foreach (int masu in board.Masu_theEffect)
                    {
                        sb.AppendLine("{ \"act\":\"drawMasu\" , \"masu\":" + masu.ToString() + " },");
                    }
                }

                // 水色
                {
                    sb.AppendLine("{ \"act\":\"colorMasu\", \"style\":\"rgba(100,240,240,0.5)\" },");

                    foreach (int masu in board.Masu_3)
                    {
                        sb.AppendLine("{ \"act\":\"drawMasu\" , \"masu\":" + masu.ToString() + " },");
                    }
                }

                // 緑
                {
                    sb.AppendLine("{ \"act\":\"colorMasu\", \"style\":\"rgba(100,240,100,0.5)\" },");

                    foreach (int masu in board.Masu_4)
                    {
                        sb.AppendLine("{ \"act\":\"drawMasu\" , \"masu\":" + masu.ToString() + " },");
                    }
                }
            }

            // マークがあるマス
            {
                // 赤駒
                {
                    sb.AppendLine("{");
                    sb.AppendLine("    \"act\":\"begin_imgColor\",");
                    sb.AppendLine("    \"colors\":[");
                    sb.AppendLine("        { \"sR\":255, \"sG\":255, \"sB\":255, \"sA\":255, \"dR\":0, \"dG\":0, \"dB\":200, \"dA\":255 },");
                    sb.AppendLine("    ],");
                    sb.AppendLine("},");

                    foreach (int masu in board.MarkMasu1)
                    {
                        sb.AppendLine("{ \"act\":\"drawImg\", \"img\":\"mark1\", \"masu\":" + masu + " },");
                    }

                    sb.AppendLine("{ \"act\":\"end_imgColor\", },");
                }

                // 青駒
                {
                    sb.AppendLine("{");
                    sb.AppendLine("    \"act\":\"begin_imgColor\",");
                    sb.AppendLine("    \"colors\":[");
                    sb.AppendLine("        { \"sR\":255, \"sG\":255, \"sB\":255, \"sA\":255, \"dR\":200, \"dG\":0, \"dB\":0, \"dA\":255 },");
                    sb.AppendLine("    ],");
                    sb.AppendLine("},");

                    foreach (int masu in board.MarkMasu2)
                    {
                        sb.AppendLine("{ \"act\":\"drawImg\", \"img\":\"mark1\", \"masu\":" + masu + " },");
                    }

                    sb.AppendLine("{ \"act\":\"end_imgColor\", },");
                }

                // 水色
                {
                    sb.AppendLine("{");
                    sb.AppendLine("    \"act\":\"begin_imgColor\",");
                    sb.AppendLine("    \"colors\":[");
                    sb.AppendLine("        { \"sR\":255, \"sG\":255, \"sB\":255, \"sA\":255, \"dR\":0, \"dG\":200, \"dB\":200, \"dA\":255 },");
                    sb.AppendLine("    ],");
                    sb.AppendLine("},");

                    foreach (int masu in board.MarkMasu3)
                    {
                        sb.AppendLine("{ \"act\":\"drawImg\", \"img\":\"mark1\", \"masu\":" + masu + " },");
                    }

                    sb.AppendLine("{ \"act\":\"end_imgColor\", },");
                }

                // 緑駒
                {
                    sb.AppendLine("{");
                    sb.AppendLine("    \"act\":\"begin_imgColor\",");
                    sb.AppendLine("    \"colors\":[");
                    sb.AppendLine("        { \"sR\":255, \"sG\":255, \"sB\":255, \"sA\":255, \"dR\":0, \"dG\":200, \"dB\":0, \"dA\":255 },");
                    sb.AppendLine("    ],");
                    sb.AppendLine("},");

                    foreach (int masu in board.MarkMasu4)
                    {
                        sb.AppendLine("{ \"act\":\"drawImg\", \"img\":\"mark1\", \"masu\":" + masu + " },");
                    }

                    sb.AppendLine("{ \"act\":\"end_imgColor\", },");
                }
            }

            // 脳内攻め手
            switch (board.NounaiSeme)
            {
                case Gkl_NounaiSeme.Sente:
                    {
                        sb.AppendLine("{");
                        sb.AppendLine("    \"act\":\"begin_imgColor\",");
                        sb.AppendLine("    \"colors\":[");
                        sb.AppendLine("        { \"sR\":255, \"sG\":255, \"sB\":255, \"sA\":255, \"dR\":  0, \"dG\":  0, \"dB\":200, \"dA\":255 },");
                        sb.AppendLine("        { \"sR\":238, \"sG\":238, \"sB\":238, \"sA\":255, \"dR\":128, \"dG\":128, \"dB\":  0, \"dA\":128 },");
                        sb.AppendLine("    ],");
                        sb.AppendLine("},");
                        sb.AppendLine("{ \"act\":\"drawImg\", \"img\":\"nounaiSeme\", \"masu\": 118 },");
                        sb.AppendLine("{ \"act\":\"end_imgColor\", },");
                    }
                    break;
                case Gkl_NounaiSeme.Gote:
                    {
                        sb.AppendLine("{");
                        sb.AppendLine("    \"act\":\"begin_imgColor\",");
                        sb.AppendLine("    \"colors\":[");
                        sb.AppendLine("        { \"sR\":255, \"sG\":255, \"sB\":255, \"sA\":255, \"dR\":  0, \"dG\":  0, \"dB\":200, \"dA\":255 },");
                        sb.AppendLine("        { \"sR\":238, \"sG\":238, \"sB\":238, \"sA\":255, \"dR\":128, \"dG\":128, \"dB\":  0, \"dA\":128 },");
                        sb.AppendLine("    ],");
                        sb.AppendLine("},");
                        sb.AppendLine("{ \"act\":\"drawImg\", \"img\":\"nounaiSemeV\", \"masu\": 151 },");
                        sb.AppendLine("{ \"act\":\"end_imgColor\", },");
                    }
                    break;
                default:
                    break;
            }

            // 赤駒
            {
                sb.AppendLine("{");
                sb.AppendLine("    \"act\":\"begin_imgColor\",");
                sb.AppendLine("    \"colors\":[");
                sb.AppendLine("        { \"sR\":255, \"sG\":255, \"sB\":255, \"sA\":255, \"dR\":0, \"dG\":0, \"dB\":200, \"dA\":255 },");
                sb.AppendLine("    ],");
                sb.AppendLine("},");

                foreach (Gkl_KomaMasu km in board.KomaMasu1)
                {
                    sb.AppendLine("{ act:\"drawImg\", img:\"" + km.KomaImg + "\", masu:" + km.Masu + " },");
                }

                sb.AppendLine("{ act:\"end_imgColor\", },");
            }

            // 青駒
            {
                sb.AppendLine("{");
                sb.AppendLine("    \"act\":\"begin_imgColor\",");
                sb.AppendLine("    \"colors\":[");
                sb.AppendLine("        { \"sR\":255, \"sG\":255, \"sB\":255, \"sA\":255, \"dR\":200, \"dG\":0, \"dB\":0, \"dA\":255 },");
                sb.AppendLine("    ],");
                sb.AppendLine("},");

                foreach (Gkl_KomaMasu km in board.KomaMasu2)
                {
                    sb.AppendLine("{ act:\"drawImg\", img:\"" + km.KomaImg + "\", masu:" + km.Masu + " },");
                }

                sb.AppendLine("{ act:\"end_imgColor\", },");
            }

            // 水色
            {
                sb.AppendLine("{");
                sb.AppendLine("    act:\"begin_imgColor\",");
                sb.AppendLine("    colors:[");
                sb.AppendLine("        { sR:255, sG:255, sB:255, sA:255, dR:0, dG:200, dB:200, dA:255 },");
                sb.AppendLine("    ],");
                sb.AppendLine("},");

                foreach (Gkl_KomaMasu km in board.KomaMasu3)
                {
                    sb.AppendLine("{ act:\"drawImg\", img:\"" + km.KomaImg + "\", masu:" + km.Masu + " },");
                }

                sb.AppendLine("{ act:\"end_imgColor\", },");
            }

            // 緑駒
            {
                sb.AppendLine("{");
                sb.AppendLine("    act:\"begin_imgColor\",");
                sb.AppendLine("    colors:[");
                sb.AppendLine("        { sR:255, sG:255, sB:255, sA:255, dR:0, dG:200, dB:0, dA:255 },");
                sb.AppendLine("    ],");
                sb.AppendLine("},");

                foreach (Gkl_KomaMasu km in board.KomaMasu4)
                {
                    sb.AppendLine("{ act:\"drawImg\", img:\"" + km.KomaImg + "\", masu:" + km.Masu + " },");
                }

                sb.AppendLine("{ act:\"end_imgColor\", },");
            }

            // 矢印
            {
                // 赤色
                sb.AppendLine("{ act:\"colorArrow\", style:\"rgba(240,100,100,0.8)\" },");

                foreach (Gkl_Arrow a in board.Arrow)
                {
                    sb.AppendLine("{ act:\"drawArrow\", from:\"" + a.From + "\", to:" + a.To + " },");
                }
            }

            // テキスト
            {
                sb.AppendLine();
                sb.Append("\"テキスト\",");

                // キャプション
                if (board.Caption != "")
                {
                    sb.Append("{ act:\"drawText\", text:\"");
                    sb.Append(board.Caption);
                    sb.Append("\"  , x:0, y:20 },");
                    sb.AppendLine();
                }

                // 例：「3手済 先手番/脳内 1手先」
                {
                    sb.Append("{ act:\"drawText\", text:\"");

                    if (board.Temezumi != int.MinValue)
                    {
                        sb.Append(board.Temezumi + "手済");
                    }

                    switch (board.GenTeban)
                    {
                        case Playerside.P1:
                            sb.Append(" 先手番");
                            break;
                        case Playerside.P2:
                            sb.Append(" 後手番");
                            break;
                        default:
                            break;
                    }

                    if (board.YomikaisiTemezumi != int.MinValue)
                    {
                        sb.Append("/脳内 ");
                        sb.Append((board.Temezumi - board.YomikaisiTemezumi + 1).ToString());
                        sb.Append("手先");
                    }

                    if (board.Score != float.MinValue)
                    {
                        sb.Append(" 評価");
                        sb.Append(((float)board.Score).ToString());
                    }

                    sb.Append("\"  , x:120, y:270 },");
                    sb.AppendLine();
                }
            }

            sb.AppendLine("],");

            return sb.ToString();
        }


    }
}
