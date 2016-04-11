using Grayscale.P163_KifuCsa____.L___250_Struct;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Grayscale.P163_KifuCsa____.L250____Struct
{

    public abstract class Util_Csa
    {

        public static CsaKifu ReadFile(string filepath)
        {
            CsaKifuImpl csaKifuData = new CsaKifuImpl();

            if (!File.Exists(filepath))
            {
                MessageBox.Show("ファイルがありません。\nファイルパス=[" + filepath + "]", "エラー");

                goto gt_EndMethod;
            }

            string[] allLines = File.ReadAllLines(filepath);

            int phase = 0;

            foreach (string line in allLines)
            {
                if (line.Length < 1)
                {
                    goto gt_EndLoop1;
                }

            gt_Continue1:
                ;

                switch (phase)
                {
                    case 0:
                        //
                        // V,N,$部
                        //
                        switch (line[0])
                        {
                            case 'V': Util_Csa.ReadV(line, csaKifuData); break;
                            case 'N': Util_Csa.ReadN(line, csaKifuData); break;
                            case '$': break;// 対局情報は無視します。
                            case '\'': break;// コメントは無視します。
                            default:
                                phase = 1;
                                goto gt_Continue1;
                        }
                        break;
                    case 1:
                        //
                        // P部
                        //
                        switch (line[0])
                        {
                            case 'P': Util_Csa.ReadP(line, csaKifuData); break;
                            case '\'': break;// コメントは無視します。
                            default:
                                phase = 2;
                                goto gt_Continue1;
                        }
                        break;
                    case 2:
                        //
                        // ±部
                        //
                        switch (line[0])
                        {
                            case '+'://thru
                            case '-': Util_Csa.ReadPlaceMinus_FirstSengo(line, csaKifuData); phase = 3; break;
                            case '\'': break;// コメントは無視します。
                            default:
                                phase = 3;//本当はエラーのはず。
                                goto gt_Continue1;
                        }
                        break;
                    case 3:
                        //
                        // 指し手の±部、T部
                        //
                        switch (line[0])
                        {
                            case '+':
                            case '-': Util_Csa.ReadPlaceMinus_Sasite(line, csaKifuData); break;
                            case 'T': Util_Csa.ReadT(line, csaKifuData); break;
                            case '\'': break;// コメントは無視します。
                            default:
                                phase = 4;
                                goto gt_Continue1;
                        }
                        break;
                    case 4:
                        //
                        // %部
                        //
                        switch (line[0])
                        {
                            case '%': Util_Csa.ReadFinishedStatus(line, csaKifuData); break;
                            case '\'': break;// コメントは無視します。
                            default:
                                phase = 5;
                                goto gt_Next1;
                        }
                        break;
                    default:
                        break;
                }


            gt_EndLoop1:
                ;
            }
        gt_Next1:
            ;

        gt_EndMethod:
            ;
            return csaKifuData;
        }

        /// <summary>
        /// V部。
        /// </summary>
        /// <param name="line"></param>
        private static void ReadV(string line, CsaKifuImpl csaKifuData)
        {
            csaKifuData.Version = line.Substring(1);
        }

        /// <summary>
        /// N部。
        /// </summary>
        /// <param name="line"></param>
        private static void ReadN(string line, CsaKifuImpl csaKifuData)
        {
            if (line.Length < 2)
            {
                goto gt_EndMethod;
            }

            // 2文字目が「+」なら先手の名前、「-」なら後手の名前。
            switch (line[1])
            {
                case '+':
                    csaKifuData.Player1Name = line.Substring(2);
                    break;
                case '-':
                    csaKifuData.Player2Name = line.Substring(2);
                    break;
                default:
                    break;
            }

        gt_EndMethod:
            ;
        }

        /// <summary>
        /// P部。
        /// </summary>
        /// <param name="line"></param>
        private static void ReadP(string line, CsaKifu csaKifuData)
        {
            if (line.Length < 2)
            {
                goto gt_EndMethod;
            }

            // 3通りあります。
            // （１）2文字目が「I」の場合。
            // （２）2文字目が「1」～「9」の場合。
            // （３）2文字目が「+」「-」の場合。
            switch (line[1])
            {
                case 'I': Util_Csa.ReadP_1(line); break;
                case '1': //thru
                case '2': //thru
                case '3': //thru
                case '4': //thru
                case '5': //thru
                case '6': //thru
                case '7': //thru
                case '8': //thru
                case '9': Util_Csa.ReadP_2(line, csaKifuData); break;
                case '+': //thru
                case '-': Util_Csa.ReadP_3(line); break;
                default:
                    break;
            }

        gt_EndMethod:
            ;
        }

        /// <summary>
        /// P部。パターン1。
        /// </summary>
        /// <param name="line"></param>
        private static void ReadP_1(string line)
        {
            // 未対応です。
        }

        /// <summary>
        /// P部。パターン2。
        /// </summary>
        /// <param name="line"></param>
        private static void ReadP_2(string line, CsaKifu csaKifuData)
        {
            // 先頭に 「P1」～「P9」の２文字、そのあと３文字を１塊りとして９回くり返しがあり、少なくとも29文字はあるはずです。
            if (line.Length < 29)
            {
                goto gt_EndMethod;
            }

            // 2文字目が、段を表します。
            int dan;
            switch (line[1])
            {
                case '1': dan = 1; break;
                case '2': dan = 2; break;
                case '3': dan = 3; break;
                case '4': dan = 4; break;
                case '5': dan = 5; break;
                case '6': dan = 6; break;
                case '7': dan = 7; break;
                case '8': dan = 8; break;
                case '9': dan = 9; break;
                default: goto gt_EndMethod;
            }

            // 3文字目以降、3文字ずつ、9回取ります。
            for (int suji = 1; suji <= 9; suji++)
            {
                csaKifuData.Shogiban[suji, dan] = line.Substring(2 + (suji - 1) * 3, 3);
            }

        gt_EndMethod:
            ;
        }

        /// <summary>
        /// P部。パターン3。
        /// </summary>
        /// <param name="line"></param>
        private static void ReadP_3(string line)
        {
            // 未対応です。
        }

        /// <summary>
        /// 初手の先後の±部
        /// </summary>
        private static void ReadPlaceMinus_FirstSengo(string line, CsaKifuImpl csaKifuData)
        {
            csaKifuData.FirstSengo = line;
        }

        /// <summary>
        /// 指し手の±部
        /// </summary>
        private static void ReadPlaceMinus_Sasite(string line, CsaKifuImpl csaKifuData)
        {
            if (line.Length < 7)
            {
                goto gt_EndMethod;
            }

            CsaKifuSasite sasite = new CsaKifuSasiteImpl();

            // 1文字目
            switch (line[0])
            {
                case '+': sasite.Sengo = line[0].ToString(); break;
                case '-': sasite.Sengo = line[0].ToString(); break;
            }

            // 2～3文字目
            sasite.SourceMasu = line.Substring(1, 2);

            // 4～5文字目
            sasite.DestinationMasu = line.Substring(3, 2);

            // 6～7文字目
            sasite.Syurui = line.Substring(5, 2);

            // オプション。手目済み。
            sasite.OptionTemezumi = csaKifuData.SasiteList.Count + 1;

            csaKifuData.SasiteList.Add(sasite);

        gt_EndMethod:
            ;
        }

        /// <summary>
        /// T部。
        /// </summary>
        /// <param name="line"></param>
        private static void ReadT(string line, CsaKifuImpl csaKifuData)
        {
            if (line.Length < 2)
            {
                goto gt_EndMethod;
            }

            if (csaKifuData.SasiteList.Count < 1)
            {
                goto gt_EndMethod;
            }

            int second;
            if (int.TryParse(line.Substring(1), out second))
            {
                csaKifuData.SasiteList.Last().Second = second;
            }

        gt_EndMethod:
            ;
        }

        /// <summary>
        /// %部。
        /// </summary>
        private static void ReadFinishedStatus(string line, CsaKifuImpl csaKifuData)
        {
            csaKifuData.FinishedStatus = line.Substring(1);
        }

    }

}
