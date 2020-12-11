using System;
using System.Drawing;
using System.IO;
using System.Text;
using Grayscale.P003Log.I500Struct;
using Grayscale.P211WordShogi.L500Word;
using Grayscale.P234Komahaiyaku.L250Word;
using Grayscale.P234Komahaiyaku.L500Util;
using Grayscale.P238Seiza.L250Struct;
using Grayscale.P238Seiza.L500Util;
using Grayscale.P239ConvWords.L500Converter;
using Grayscale.P693ShogiGui.I080Shape;
using Grayscale.P693ShogiGui.I500Gui;
using Nett;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P693ShogiGui.L080Shape
{



    /// <summary>
    /// ************************************************************************************************************************
    /// 描かれる図画です。将棋の駒を描きます。
    /// ************************************************************************************************************************
    /// </summary>
    [Serializable]
    public class Shape_BtnKomaImpl : Shape_Abstract, Shape_BtnKoma
    {

        #region プロパティー

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 局面の駒リストでの格納位置番号。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public Finger Finger { get; set; }
        public Finger Koma { get { return this.Finger; } set { this.Finger = value; } }

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 光。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public bool Light
        {
            get;
            set;
        }

        #endregion


        /// <summary>
        /// ************************************************************************************************************************
        /// コンストラクターです。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="koma"></param>
        public Shape_BtnKomaImpl(string widgetName, Finger koma)
            : base(widgetName, 42, 42, 35, 35)
        {
            this.Finger = koma;
        }


        private void PaintText(Graphics g, RO_Star komaKs, Point location)
        {
            if (null == komaKs)
            {
                goto gt_EndMethod;
            }

            Komahaiyaku185 haiyaku = komaKs.Haiyaku;

            if (haiyaku == Komahaiyaku185.n000_未設定)
            {
                // 配役未設定時は、普通に駒を描画します。
                g.DrawString(komaKs.Text_Label, new Font(FontFamily.GenericSerif, 20.0f), Brushes.Black, location);
            }
            else
            {
                // 配役設定時は、絵修飾字を描画します。

                string text = Util_Komahaiyaku184.Name2[(int)haiyaku];
                string text1;
                string text2;
                string text3;

                if (4 < text.Length)
                {
                    text1 = text.Substring(0, 2);
                    text2 = text.Substring(2, 2);
                    text3 = text.Substring(4, text.Length - 4);
                }
                else if (2 < text.Length)
                {
                    text1 = text.Substring(0, 2);
                    text2 = text.Substring(2, text.Length - 2);
                    text3 = "";
                }
                else
                {
                    text1 = text;
                    text2 = "";
                    text3 = "";
                }

                // １行目
                g.DrawString(text1, new Font(FontFamily.GenericSerif, 10.0f), Brushes.Black, location);

                // ２行目
                g.DrawString(text2, new Font(FontFamily.GenericSerif, 10.0f), Brushes.Black, new Point(location.X, location.Y + 11));

                // ３行目
                g.DrawString(text3, new Font(FontFamily.GenericSerif, 10.0f), Brushes.Black, new Point(location.X, location.Y + 22));
            }

        gt_EndMethod:
            ;
        }


        /// <summary>
        /// ************************************************************************************************************************
        /// 駒ボタンの描画はここに書きます。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="g1"></param>
        public void Paint(Graphics g1, MainGui_Csharp mainGui, IErrorController errH)
        {

            if (!this.Visible)
            {
                goto gt_EndMethod;
            }

            //----------
            // 背景
            //----------
            if (mainGui.FigTumandeiruKoma == this.Finger)//>>>>> 駒をつまんでいる時
            {
                g1.FillRectangle(Brushes.Brown, this.Bounds);// 駒の背景は茶色。
            }
            else if (mainGui.Shape_PnlTaikyoku.MovedKoma == this.Finger)//>>>>> 駒を移動した時
            {
                g1.FillRectangle(Brushes.DarkKhaki, this.Bounds);// 駒の背景はカーキ。
            }
            else if (this.Light)//>>>>> マウスカーソルが当たっている時
            {
            }

            // この駒について。
            RO_Star koma = Util_Starlightable.AsKoma(mainGui.Model_Manual.GuiSkyConst.StarlightIndexOf(this.Finger).Now);

            if(true)
            {
                //----------
                // 駒画像
                //----------
                var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
                var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));
                string dataDirectory = Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("DataDirectory"));

                StringBuilder sb = new StringBuilder();
                sb.Append(Path.Combine(dataDirectory, "img/koma/"));
                sb.Append(Conv_Komasyurui.ToStr_ImageName(koma.Komasyurui));
                sb.Append(".png");
                Image img = Image.FromFile(sb.ToString());

                if (koma.Pside == Playerside.P2)
                {
                    // 画像を180度回転させたい☆
                    img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                }
                else
                {
                }

                g1.DrawImage(img, this.Bounds);
            }
            else
            {
                /*
                //----------
                // 配役画像
                //----------
                var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
                var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));
                string dataDirectory = Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("DataDirectory"));

                StringBuilder sb = new StringBuilder();
                sb.Append(Path.Combine(dataDirectory, "img/mobility/"));
                sb.Append((int)koma.Haiyaku);//配役番号
                sb.Append(".png");
                Image img = Image.FromFile(sb.ToString());

                if (koma.Pside == Playerside.P2)
                {
                    // 画像を180度回転させたい☆
                    img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                }
                else
                {
                }

                g1.DrawImage(img, this.Bounds);
                */
            }


        /*
        // 枠線
        if(false)
        {
            Pen pen;
            if (this.Light)
            {
                pen = Pens.Yellow;
            }
            else
            {
                pen = Pens.Black;
            }

            g1.DrawRectangle(pen, this.Bounds);
        }
        */

        /*
        // 文字
        if(false)
        {
            if (koma.Pside == Playerside.P1)
            {
                //----------
                // 先手
                //----------
                //
                // ただ描画するだけ☆
                //

                this.PaintText(g1, koma, this.Bounds.Location);
            }
            else
            {
                //----------
                // 後手
                //----------
                //
                // １８０度回転させて描画するために、大掛かりになっています。
                //

                //string moji = siteiSk.KomaDoors[this.KomaHandle].Text_Label;

                //----------
                // 使用するフォント
                //----------
                //Font fnt = new Font(FontFamily.GenericSerif, 20.0f);

                //----------
                // 文字の大きさにあった白紙（b）
                //----------
                Graphics bG;
                Bitmap bImg;
                {
                    int w;
                    int h;
                    {
                        //----------
                        // 文字の大きさを調べるための白紙（a）
                        //----------
                        Bitmap aImg = new Bitmap(1, 1);

                        //imgのGraphicsオブジェクトを取得
                        Graphics aG = Graphics.FromImage(aImg);

                        //文字列を描画したときの大きさを計算する
                        w = 48;
                        h = 48;
                        //w = (int)aG.MeasureString(moji, fnt).Width;
                        //h = (int)fnt.GetHeight(aG);

                        //if (w == 0 || h == 0)
                        //{
                        //    System.C onsole.WriteLine("moji=["+moji+"]");
                        //}

                        //if (w < 1)
                        //{
                        //    w = 1;
                        //}

                        //if (h < 1)
                        //{
                        //    h = 1;
                        //}

                        aG.Dispose();
                        aImg.Dispose();
                    }

                    bImg = new Bitmap(w, h);
                }

                // 文字描画
                bG = Graphics.FromImage(bImg);

                this.PaintText(bG, koma, new Point(0, 0)); //bG.DrawString(moji, fnt, Brushes.Black, 0, 0);

                //----------
                // 回転軸座標
                //----------
                float x = (float)this.Bounds.X + (float)this.Bounds.Width / 2;
                float y = (float)this.Bounds.Y + (float)this.Bounds.Height / 2;

                //----------
                // 回転
                //----------

                // 180度で回転するための座標を計算
                //ラジアン単位に変換
                float d = 180.0f / (180.0f / (float)Math.PI);
                //新しい座標位置を計算する
                float x1 = x + bImg.Width * (float)Math.Cos(d);
                float y1 = y + bImg.Width * (float)Math.Sin(d);
                float x2 = x - bImg.Height * (float)Math.Sin(d);
                float y2 = y + bImg.Height * (float)Math.Cos(d);
                //PointF配列を作成
                PointF[] destinationPoints = {new PointF(x + (float)this.Bounds.Width / 2, y + (float)this.Bounds.Height / 2),
                            new PointF(x1 + (float)this.Bounds.Width / 2, y1 + (float)this.Bounds.Height / 2),
                            new PointF(x2 + (float)this.Bounds.Width / 2, y2 + (float)this.Bounds.Height / 2)};

                //画像を描画
                g1.DrawImage(bImg, destinationPoints);

                //リソースを解放する
                bImg.Dispose();
                bG.Dispose();
                //fnt.Dispose();
            }
        }
        */


        //// フィンガー番号
        //if (false)
        //{
        //    g1.DrawString(this.Finger.ToString(), new Font(FontFamily.GenericSerif, 10.0f), Brushes.Black, this.Bounds.Location);
        //}

        ////----------
        //// デバッグ用
        ////----------
        //if (true)
        //{
        //    string moji = siteiSk.KomaDoors[this.Handle].SrcOkiba.ToString();

        //    g1.DrawString(moji, new Font(FontFamily.GenericSerif, 12.0f), Brushes.Red, this.Bounds.Location);
        //}

        gt_EndMethod:
            ;
        }


        /// <summary>
        /// ************************************************************************************************************************
        /// マウスが重なった駒は、光フラグを立てます。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void LightByMouse(int x, int y)
        {
            if (this.HitByMouse(x,y)) // マウスが重なっているなら
            {
                this.Light = true;
            }
            else // マウスが重なっていないなら
            {
                this.Light = false;
            }
        }

    }



}
