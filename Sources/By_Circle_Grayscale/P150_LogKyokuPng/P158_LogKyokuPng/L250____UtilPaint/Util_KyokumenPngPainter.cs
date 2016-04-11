using Grayscale.P061_ConstShogi_.L250____Const;
using Grayscale.P157_KyokumenPng.L___500_Struct;
using System.Drawing;

namespace Grayscale.P158_LogKyokuPng.L250____UtilPaint
{
    public abstract class Util_KyokumenPngPainter
    {

        public const int MOTI_LEN = 7;//飛、角、金、銀、桂、香、歩の７つ。
        public const int BN_SUJIS = 9;//将棋盤は9筋。ban sujis
        public const int BN_DANS = 9;

        public const int BN_BRD_R_W = 1;//将棋盤の右辺の枠線幅。ban border right width。
        public const int BN_BRD_B_W = 1;

        public static readonly Font Font = new Font("MS UI Gothic", 18.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));

        /// <summary>
        /// 局面を描きます。
        /// </summary>
        public static void Paint(Graphics g, KyokumenPngArgs args)
        {

            //----------------------------------------
            // 8×8 の将棋盤
            //----------------------------------------
            int bOx = args.Env.KmW + 2 * args.Env.SjW; // 将棋盤の左辺
            int bOy = 0;

            //----------------------------------------
            // 移動元の升を灰色に塗る
            //----------------------------------------
            if (0 <= args.SrcMasu_orMinusOne && args.SrcMasu_orMinusOne <= 80)
            {
                g.FillRectangle(Brushes.LightGray, Util_KyokumenPngPainter.GetBanjoRectangle(args.SrcMasu_orMinusOne, bOx, bOy, args));
            }

            //----------------------------------------
            // 駒台の駒を打った場合
            //----------------------------------------
            if (args.DropKoma != KyokumenPngArgs_FoodOrDropKoma.UNKNOWN && args.DropKoma != KyokumenPngArgs_FoodOrDropKoma.NONE)
            {
                Rectangle rect = Util_KyokumenPngPainter.GetMotiRectangle(args.DropKoma, args);
                // 駒台に塗りつぶし矩形、灰色
                g.FillRectangle(Brushes.LightGray, rect);
            }

            //----------------------------------------
            // 動かした先の升を黒く塗る
            //----------------------------------------
            if (0 <= args.DstMasu_orMinusOne && args.DstMasu_orMinusOne<=80)
            {
                // 盤上に塗りつぶし矩形
                g.FillRectangle(Brushes.Black, Util_KyokumenPngPainter.GetBanjoRectangle(args.DstMasu_orMinusOne, bOx, bOy, args));
            }

            //----------------------------------------
            // 取った駒の駒台の背景を黒く塗る
            //----------------------------------------
            if (args.FoodKoma != KyokumenPngArgs_FoodOrDropKoma.UNKNOWN && args.FoodKoma != KyokumenPngArgs_FoodOrDropKoma.NONE)
            {
                g.FillRectangle(Brushes.Black, Util_KyokumenPngPainter.GetMotiRectangle(args.FoodKoma, args));
            }


            //----------------------------------------
            // 図形
            //----------------------------------------

            {

                // 縦線
                for (int col = 0; col < 10; col++)
                {
                    g.DrawLine(Pens.Black, col * args.Env.KmW + bOx, 0 + bOy, col * args.Env.KmW + bOx, 180 + bOy);
                }

                // 横線
                for (int row = 0; row < 10; row++)
                {
                    g.DrawLine(Pens.Black, 0 + bOx, row * args.Env.KmH + bOy, 180 + bOx, row * args.Env.KmH + bOy);
                }
            }

            // 盤上の駒
            {
                for (int suji = 1; suji < 10; suji++)
                {
                    for (int dan = 1; dan < 10; dan++)
                    {
                        string sign = args.Ro_Kyokumen1.Ban[suji,dan];
                        if ("" != sign)
                        {
                            Point pt = Util_KyokumenPngPainter.CropXyBySign(sign, args);
                            g.DrawImage(Image.FromFile(args.Env.ImgFolder + args.Env.KmFile),
                                new Rectangle((9 - suji) * args.Env.KmW + bOx, (dan - 1) * args.Env.KmH + bOy, args.Env.KmW, args.Env.KmH),//dst
                                new Rectangle(pt.X, pt.Y, args.Env.KmW, args.Env.KmH),//src
                                GraphicsUnit.Pixel
                                );
                        }
                    }
                }
            }

            // 後手の持駒 （飛,角,金,銀,桂,香,歩）
            {
                string[] signs = new string[] { "r", "b", "g", "s", "n", "l", "p" };
                int ox = 0;
                int oy = 0;
                for (int moti = 0; moti < 7; moti++)
                {
                    Point pt = Util_KyokumenPngPainter.CropXyBySign(signs[moti], args);
                    // 枚数
                    int player = 2;
                    int maisu = args.Ro_Kyokumen1.Moti[player,moti];
                    if (0 < maisu)
                    {
                        //駒
                        g.DrawImage(
                            Image.FromFile(args.Env.ImgFolder + args.Env.KmFile),
                            new Rectangle(ox, (signs.Length - moti - 1) * args.Env.KmH + oy, args.Env.KmW, args.Env.KmH),//dst
                            new Rectangle(pt.X, pt.Y, args.Env.KmW, args.Env.KmH),//src
                            GraphicsUnit.Pixel
                            );

                        // 1桁目が先
                        {
                            int ichi = maisu % 10;
                            g.DrawImage(Image.FromFile(args.Env.ImgFolder + args.Env.SjFile),
                                new Rectangle(ox + args.Env.KmW, (signs.Length - moti - 1) * args.Env.KmH + (args.Env.KmH - args.Env.SjH) + oy, args.Env.SjW, args.Env.SjH),//dst
                                new Rectangle(ichi * args.Env.SjW, args.Env.SjH, args.Env.SjW, args.Env.SjH),//src
                                GraphicsUnit.Pixel
                                );// 一の位
                        }

                        // 2桁目が後
                        {
                            int ju = maisu / 10;
                            if (ju < 1)
                            {
                                ju = -1;//空桁
                            }
                            g.DrawImage(
                                Image.FromFile(args.Env.ImgFolder + args.Env.SjFile),
                                new Rectangle(ox + args.Env.KmW + args.Env.SjW, (signs.Length - moti - 1) * args.Env.KmH + (args.Env.KmH - args.Env.SjH) + oy, args.Env.SjW, args.Env.SjH),//dst
                                new Rectangle(ju * args.Env.SjW, 0, args.Env.SjW, args.Env.SjH),//src
                                GraphicsUnit.Pixel
                                );// 十の位
                        }
                    }
                }
            }

            // 先手の持駒 （飛,角,金,銀,桂,香,歩）
            {
                string[] signs = new string[] { "R", "B", "G", "S", "N", "L", "P" };
                int ox = (args.Env.KmW + 2 * args.Env.SjW) + 9 * args.Env.KmW + BN_BRD_R_W;
                int oy = (9 * args.Env.KmW + BN_BRD_B_W) - 7 * args.Env.KmH;
                for (int moti = 0; moti < 7; moti++)
                {
                    Point pt = Util_KyokumenPngPainter.CropXyBySign(signs[moti], args);

                    // 枚数
                    int player = 1;
                    int maisu = args.Ro_Kyokumen1.Moti[player,moti];
                    if (0 < maisu)
                    {
                        g.DrawImage(Image.FromFile(args.Env.ImgFolder + args.Env.KmFile),
                            new Rectangle(ox, moti * args.Env.KmH + oy, args.Env.KmW, args.Env.KmH),//dst
                            new Rectangle(pt.X, pt.Y, args.Env.KmW, args.Env.KmH),//src
                            GraphicsUnit.Pixel
                            );//駒

                        // 十の位が先
                        {
                            int ju = maisu / 10;
                            if (ju < 1)
                            {
                                ju = -1;//空桁
                            }
                            g.DrawImage(Image.FromFile(args.Env.ImgFolder + args.Env.SjFile),
                                new Rectangle(ox + args.Env.KmW, moti * args.Env.KmH + (args.Env.KmH - args.Env.SjH) + oy, args.Env.SjW, args.Env.SjH),//dst
                                new Rectangle(ju * args.Env.SjW, 0, args.Env.SjW, args.Env.SjH),//src
                                GraphicsUnit.Pixel
                                );// 十の位
                        }

                        // 一の位が後
                        {
                            int ichi = maisu % 10;
                            g.DrawImage(Image.FromFile(args.Env.ImgFolder + args.Env.SjFile),
                                new Rectangle(ox + args.Env.KmW + args.Env.SjW, moti * args.Env.KmH + (args.Env.KmH - args.Env.SjH) + oy, args.Env.SjW, args.Env.SjH),//dst
                                new Rectangle(ichi * args.Env.SjW, 0, args.Env.SjW, args.Env.SjH),//src
                                GraphicsUnit.Pixel
                                );// 一の位
                        }
                    }
                }
            }

            // 手目済み
            float x;
            if (99 < args.Ro_Kyokumen1.Temezumi)// 3桁以上のとき
            {
                x = 0.0f;
                
            }
            else if (9<args.Ro_Kyokumen1.Temezumi)// 2桁のとき
            {
                x = 9.0f;
            }
            else// 1桁のとき
            {
                x = 18.0f;
            }
            g.DrawString(args.Ro_Kyokumen1.Temezumi.ToString(), Util_KyokumenPngPainter.Font, Brushes.White, new PointF(x, 150.0f));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="komaNumber">0～80を想定。</param>
        /// <param name="bOx"></param>
        /// <param name="bOy"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static Rectangle GetBanjoRectangle(int komaNumber, int bOx, int bOy, KyokumenPngArgs args)
        {
            int col = 9 - komaNumber / ConstShogi.SUJI_SIZE - 1;//0～8
            int row = komaNumber % ConstShogi.DAN_SIZE;//0～8
            return new Rectangle(col * args.Env.KmW + bOx,
                row * args.Env.KmH + bOy,
                args.Env.KmW,
                args.Env.KmH);
        }

        private static Rectangle GetMotiRectangle(KyokumenPngArgs_FoodOrDropKoma foodOrDropKoma, KyokumenPngArgs args)
        {
            int motiRow;
            switch (foodOrDropKoma)//歩香桂銀金角飛の順。
            {
                case KyokumenPngArgs_FoodOrDropKoma.FU__: motiRow = 0; break;
                case KyokumenPngArgs_FoodOrDropKoma.KYO_: motiRow = 1; break;
                case KyokumenPngArgs_FoodOrDropKoma.KEI_: motiRow = 2; break;
                case KyokumenPngArgs_FoodOrDropKoma.GIN_: motiRow = 3; break;
                case KyokumenPngArgs_FoodOrDropKoma.KIN_: motiRow = 4; break;
                case KyokumenPngArgs_FoodOrDropKoma.KAKU: motiRow = 5; break;
                case KyokumenPngArgs_FoodOrDropKoma.HI__: motiRow = 6; break;
                default: motiRow = 0; break;
            }

            int ox = 0;
            int oy = 0;
            if (1 == args.Ro_Kyokumen1.Temezumi % 2)
            {
                // プレイヤー1
                ox = (args.Env.KmW + 2 * args.Env.SjW) + 9 * args.Env.KmW + BN_BRD_R_W;
                oy = (9 * args.Env.KmW + BN_BRD_B_W) - 7 * args.Env.KmH;
                motiRow = 6 - motiRow;//角から歩の順に逆転させます。
            }
            else
            {
                // プレイヤー2
                ox = 0;
                oy = 0;
            }

            return new Rectangle(ox,
motiRow * args.Env.KmH + oy,
args.Env.KmW,
args.Env.KmH);
        }

        /// <summary>
        /// マップ画像から切り抜く座標。
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static Point CropXyBySign(string sign, KyokumenPngArgs args)
        {
            Point pt;

            switch (sign)
            {
                case "P": pt = new Point(0 * args.Env.KmW, 0 * args.Env.KmH); break;
                case "p": pt = new Point(0 * args.Env.KmW, 1 * args.Env.KmH); break;
                case "L": pt = new Point(1 * args.Env.KmW, 0 * args.Env.KmH); break;
                case "l": pt = new Point(1 * args.Env.KmW, 1 * args.Env.KmH); break;
                case "N": pt = new Point(2 * args.Env.KmW, 0 * args.Env.KmH); break;
                case "n": pt = new Point(2 * args.Env.KmW, 1 * args.Env.KmH); break;
                case "S": pt = new Point(3 * args.Env.KmW, 0 * args.Env.KmH); break;
                case "s": pt = new Point(3 * args.Env.KmW, 1 * args.Env.KmH); break;
                case "G": pt = new Point(4 * args.Env.KmW, 0 * args.Env.KmH); break;
                case "g": pt = new Point(4 * args.Env.KmW, 1 * args.Env.KmH); break;
                case "K": pt = new Point(5 * args.Env.KmW, 0 * args.Env.KmH); break;
                case "k": pt = new Point(5 * args.Env.KmW, 1 * args.Env.KmH); break;
                case "R": pt = new Point(6 * args.Env.KmW, 0 * args.Env.KmH); break;
                case "r": pt = new Point(6 * args.Env.KmW, 1 * args.Env.KmH); break;
                case "B": pt = new Point(7 * args.Env.KmW, 0 * args.Env.KmH); break;
                case "b": pt = new Point(7 * args.Env.KmW, 1 * args.Env.KmH); break;
                case "+P": pt = new Point(8 * args.Env.KmW, 0 * args.Env.KmH); break;
                case "+p": pt = new Point(8 * args.Env.KmW, 1 * args.Env.KmH); break;
                case "+L": pt = new Point(9 * args.Env.KmW, 0 * args.Env.KmH); break;
                case "+l": pt = new Point(9 * args.Env.KmW, 1 * args.Env.KmH); break;
                case "+N": pt = new Point(10 * args.Env.KmW, 0 * args.Env.KmH); break;
                case "+n": pt = new Point(10 * args.Env.KmW, 1 * args.Env.KmH); break;
                case "+S": pt = new Point(11 * args.Env.KmW, 0 * args.Env.KmH); break;
                case "+s": pt = new Point(11 * args.Env.KmW, 1 * args.Env.KmH); break;
                case "+B": pt = new Point(12 * args.Env.KmW, 0 * args.Env.KmH); break;
                case "+b": pt = new Point(12 * args.Env.KmW, 1 * args.Env.KmH); break;
                case "+R": pt = new Point(13 * args.Env.KmW, 0 * args.Env.KmH); break;
                case "+r": pt = new Point(13 * args.Env.KmW, 1 * args.Env.KmH); break;
                default: pt = new Point(14 * args.Env.KmW, 0 * args.Env.KmH); break;
            }

            return pt;
        }

    }
}
