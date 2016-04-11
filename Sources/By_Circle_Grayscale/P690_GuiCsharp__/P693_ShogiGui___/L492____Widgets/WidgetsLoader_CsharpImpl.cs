using Grayscale.P011_Csv________.L500____Util;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P693_ShogiGui___.L___492_Widgets;
using Grayscale.P693_ShogiGui___.L___500_Gui;
using Grayscale.P693_ShogiGui___.L080____Shape;
using Grayscale.P693_ShogiGui___.L491____Event;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Grayscale.P693_ShogiGui___.L492____Widgets
{
    /// <summary>
    /// ファイル１つにつき、１インスタンス作成？
    /// </summary>
    public class WidgetsLoader_CsharpImpl : WidgetsLoader
    {
        /// <summary>
        /// ファイル名。
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// ウィジェットを置く土台。
        /// </summary>
        public MainGui_Csharp ShogibanGui { get; set; }

        public WidgetsLoader_CsharpImpl(string fileName, MainGui_Csharp shogibanGui)
        {
            this.FileName = fileName;
            this.ShogibanGui = shogibanGui;
        }

        /// <summary>
        /// ファイルの読込み。
        /// </summary>
        /// <param name="obj_shogiGui"></param>
        public virtual void Step1_ReadFile()
        {
            // CSVファイル読取り
            List<List<string>> rows = Util_Csv.ReadCsv(this.FileName, Encoding.UTF8);

            //----------------------------------------
            // ヘッダー行
            //----------------------------------------
            // 最初の1行は、列名のリスト。
            Dictionary<string, int> columnNameIndex = new Dictionary<string, int>();
            {
                int i = 0;
                foreach (string columnName in rows[0])
                {
                    columnNameIndex.Add(columnName, i);
                    i++;
                }
            }
            rows.RemoveRange(0, 1);

            //----------------------------------------
            // データ行
            //----------------------------------------
            foreach (List<string> row in rows)
            {
                if (0 == row.Count)
                {
                    // 列のない行は無視します。
                    goto gt_NextRow;
                }

                //
                // 名称
                //
                if (!columnNameIndex.ContainsKey("name"))
                {
                    // name列のない行は無視します。
                    goto gt_NextRow;
                }
                string name = row[columnNameIndex["name"]];

                //
                // 型
                //
                string type = "Button"; // 既定値：ボタン
                if (columnNameIndex.ContainsKey("type"))
                {
                    type = row[columnNameIndex["type"]];
                }

                //
                // ウィンドウ・ガジェット生成、パネルへ追加
                //
                UserWidget widget;
                switch (type)
                {
                    case "Masu":
                        widget = new UserWidget_MasuImpl(new Shape_BtnMasuImpl(name));
                        break;
                    default://Button
                        widget = new UserButtonImpl(new Shape_BtnBoxImpl(name));
                        break;
                }
                widget.Type = type;
                widget.Name = name;
                this.ShogibanGui.SetWidget(name, widget);

                //
                // 表示するウィンドウ名。"Shogiban", "Console"
                //
                if (columnNameIndex.ContainsKey("window"))
                {
                    widget.Window = row[columnNameIndex["window"]];
                }

                //
                // 「IsLight_OnFlowB_1TumamitaiKoma」のTRUE/FALSE
                //
                if (columnNameIndex.ContainsKey("IsLight_OnFlowB_1TumamitaiKoma"))
                {
                    string value = row[columnNameIndex["IsLight_OnFlowB_1TumamitaiKoma"]];
                    bool b;
                    if (bool.TryParse(value, out b))
                    {
                        widget.IsLight_OnFlowB_1TumamitaiKoma = b;
                    }
                }

                //
                // 「x」
                //
                if (columnNameIndex.ContainsKey("x"))
                {
                    string value = row[columnNameIndex["x"]];
                    int x;
                    if (int.TryParse(value, out x))
                    {
                        widget.SetBounds(new Rectangle(x, widget.Bounds.Y, widget.Bounds.Width, widget.Bounds.Height));
                    }
                }

                //
                // 「y」
                //
                if (columnNameIndex.ContainsKey("y"))
                {
                    string value = row[columnNameIndex["y"]];
                    int y;
                    if (int.TryParse(value, out y))
                    {
                        widget.SetBounds(new Rectangle(widget.Bounds.X, y, widget.Bounds.Width, widget.Bounds.Height));
                    }
                }

                //
                // 「label」
                //
                if (columnNameIndex.ContainsKey("label"))
                {
                    string value = row[columnNameIndex["label"]];
                    widget.Text = value;
                }

                //
                // 「fontSize」
                //
                if (columnNameIndex.ContainsKey("fontSize"))
                {
                    string value = row[columnNameIndex["fontSize"]];
                    float fontSize;
                    if (float.TryParse(value, out fontSize))
                    {
                        widget.FontSize = fontSize;
                    }
                }

                //
                // 「fugo」
                //
                if (columnNameIndex.ContainsKey("fugo"))
                {
                    string value = row[columnNameIndex["fugo"]];
                    widget.Fugo = value;
                }

                //
                // 「width」
                //
                if (columnNameIndex.ContainsKey("width"))
                {
                    string value = row[columnNameIndex["width"]];
                    int width;
                    if (int.TryParse(value, out width))
                    {
                        widget.SetBounds(new Rectangle(widget.Bounds.X, widget.Bounds.Y, width, widget.Bounds.Height));
                    }
                }

                //
                // 「height」
                //
                if (columnNameIndex.ContainsKey("height"))
                {
                    string value = row[columnNameIndex["height"]];
                    int height;
                    if (int.TryParse(value, out height))
                    {
                        widget.SetBounds(new Rectangle(widget.Bounds.X, widget.Bounds.Y, widget.Bounds.Width, height));
                    }
                }

                //
                // 「visible」
                //
                if (columnNameIndex.ContainsKey("visible"))
                {
                    string value = row[columnNameIndex["visible"]];
                    bool visible;
                    if (bool.TryParse(value, out visible))
                    {
                        widget.Visible = visible;
                    }
                }

                //
                // 「backColor」
                //
                if (columnNameIndex.ContainsKey("backColor"))
                {
                    string value = row[columnNameIndex["backColor"]];
                    if ("" != value)
                    {
                        widget.BackColor = Color.FromName(value);
                    }
                }

                //
                // 「okiba」
                //
                if (columnNameIndex.ContainsKey("okiba"))
                {
                    string value = row[columnNameIndex["okiba"]];
                    switch (value)
                    {
                        case "ShogiBan":
                            widget.Okiba = Okiba.ShogiBan;
                            break;
                        case "Sente_Komadai":
                            widget.Okiba = Okiba.Sente_Komadai;
                            break;
                        case "Gote_Komadai":
                            widget.Okiba = Okiba.Gote_Komadai;
                            break;
                        case "KomaBukuro":
                            widget.Okiba = Okiba.KomaBukuro;
                            break;
                        default:
                            break;
                    }
                }

                //
                // 「suji」
                //
                if (columnNameIndex.ContainsKey("suji"))
                {
                    string value = row[columnNameIndex["suji"]];
                    int suji;
                    if (int.TryParse(value, out suji))
                    {
                        widget.Suji = suji;
                    }
                }

                //
                // 「dan」
                //
                if (columnNameIndex.ContainsKey("dan"))
                {
                    string value = row[columnNameIndex["dan"]];
                    int dan;
                    if (int.TryParse(value, out dan))
                    {
                        widget.Dan = dan;
                    }
                }

                //
                // 「masuHandle」
                //
                if (columnNameIndex.ContainsKey("masuHandle"))
                {
                    string value = row[columnNameIndex["masuHandle"]];
                    int masuHandle;
                    if (int.TryParse(value, out masuHandle))
                    {
                        widget.MasuHandle = masuHandle;
                    }
                }

            gt_NextRow:
                ;
            }
        }

        /// <summary>
        /// ウィジェットのコンパイル。
        /// </summary>
        /// <param name="obj_shogiGui"></param>
        public virtual void Step2_Compile_AllWidget(object obj_shogiGui)
        {
            MainGui_Csharp shogibanGui = (MainGui_Csharp)obj_shogiGui;

            foreach (UserWidget widget in shogibanGui.Widgets.Values)
            {
                widget.Compile();
            }
        }

        /// <summary>
        /// イベントの設定。
        /// </summary>
        /// <param name="obj_shogiGui"></param>
        public virtual void Step3_SetEvent(object obj_shogiGui)
        {
            MainGui_Csharp shogibanGui1 = (MainGui_Csharp)obj_shogiGui;

            //----------
            // [成る]ボタン
            //----------
            {
                UserWidget widget = shogibanGui1.GetWidget("BtnNaru");
                widget.Delegate_MouseHitEvent = Event_CsharpImpl.GetInstance().Delegate_BtnNaru;
            }

            //----------
            // [成らない]ボタン
            //----------
            {
                UserWidget widget = shogibanGui1.GetWidget("BtnNaranai");
                widget.Delegate_MouseHitEvent = Event_CsharpImpl.GetInstance().Delegate_BtnNaranai;
            }

            //----------
            // [クリアー]ボタン
            //----------
            {
                UserWidget widget = shogibanGui1.GetWidget("BtnClear");
                widget.Delegate_MouseHitEvent = Event_CsharpImpl.GetInstance().Delegate_BtnClear;
            }

            //----------
            // [再生]ボタン
            //----------
            {
                UserWidget widget = shogibanGui1.GetWidget("BtnPlay");
                widget.Delegate_MouseHitEvent = Event_CsharpImpl.GetInstance().Delegate_BtnPlay;
            }

            //----------
            // [コマ送り]ボタン
            //----------
            {
                UserWidget widget = shogibanGui1.GetWidget("BtnForward");
                widget.Delegate_MouseHitEvent = Event_CsharpImpl.GetInstance().Delegate_BtnForward;
            }

            //----------
            // [巻戻し]ボタン
            //----------
            {
                UserWidget widget = shogibanGui1.GetWidget("BtnBackward");
                widget.Delegate_MouseHitEvent = Event_CsharpImpl.GetInstance().Delegate_BtnBackward;
            }

            //----------
            // ログ出せボタン
            //----------
            {
                UserWidget widget = shogibanGui1.GetWidget("BtnLogdase");
                widget.Delegate_MouseHitEvent = Event_CsharpImpl.GetInstance().Delegate_BtnLogdase;
            }

            //----------
            // [壁置く]ボタン
            //----------
            {
                UserWidget widget = shogibanGui1.GetWidget("BtnKabeOku");
                widget.Delegate_MouseHitEvent = Event_CsharpImpl.GetInstance().Delegate_BtnKabeOku;
            }

            //----------
            // [出力切替]ボタン
            //----------
            {
                UserWidget widget = shogibanGui1.GetWidget("BtnSyuturyokuKirikae");
                widget.Delegate_MouseHitEvent = Event_CsharpImpl.GetInstance().Delegate_BtnSyuturyokuKirikae;
            }

            //----------
            // [▲]～[打]符号ボタン
            //----------
            {
                string[] buttonNames = new string[]{
                        "BtnFugo_Sente"// [▲]～[打]符号ボタン
                        ,"BtnFugo_Gote"
                        ,"BtnFugo_1"
                        ,"BtnFugo_2"
                        ,"BtnFugo_3"
                        ,"BtnFugo_4"
                        ,"BtnFugo_5"
                        ,"BtnFugo_6"
                        ,"BtnFugo_7"
                        ,"BtnFugo_8"
                        ,"BtnFugo_9"
                        ,"BtnFugo_Dou"
                        ,"BtnFugo_Fu"
                        ,"BtnFugo_Hisya"
                        ,"BtnFugo_Kaku"
                        ,"BtnFugo_Kyo"
                        ,"BtnFugo_Kei"
                        ,"BtnFugo_Gin"
                        ,"BtnFugo_Kin"
                        ,"BtnFugo_Oh"
                        ,"BtnFugo_Gyoku"
                        ,"BtnFugo_Tokin"
                        ,"BtnFugo_Narikyo"
                        ,"BtnFugo_Narikei"
                        ,"BtnFugo_Narigin"
                        ,"BtnFugo_Ryu"
                        ,"BtnFugo_Uma"
                        ,"BtnFugo_Yoru"
                        ,"BtnFugo_Hiku"
                        ,"BtnFugo_Agaru"
                        ,"BtnFugo_Migi"
                        ,"BtnFugo_Hidari"
                        ,"BtnFugo_Sugu"
                        ,"BtnFugo_Nari"
                        ,"BtnFugo_Funari"
                        ,"BtnFugo_Da"
                    };

                foreach (string buttonName in buttonNames)
                {
                    UserWidget widget = shogibanGui1.GetWidget(buttonName);
                    widget.Delegate_MouseHitEvent = Event_CsharpImpl.GetInstance().Delegate_BtnKakusyuFugo;
                }
            }

            //----------
            // [全消]ボタン
            //----------
            {
                UserWidget widget = shogibanGui1.GetWidget("BtnFugo_Zenkesi");
                widget.Delegate_MouseHitEvent = Event_CsharpImpl.GetInstance().Delegate_BtnZenkesi;
            }

            //----------
            // [ここから採譜]ボタン
            //----------
            {
                UserWidget widget = shogibanGui1.GetWidget("BtnFugo_KokokaraSaifu");
                widget.Delegate_MouseHitEvent = Event_CsharpImpl.GetInstance().Delegate_BtnKokokaraSaifu;
            }

            //----------
            // 初期配置ボタン
            //----------
            {
                UserWidget widget = shogibanGui1.GetWidget("BtnSyokihaichi");
                widget.Delegate_MouseHitEvent = Event_CsharpImpl.GetInstance().Delegate_BtnSyokihaichi;
            }

            //----------
            // [向き]ボタン
            //----------
            {
                UserWidget widget = shogibanGui1.GetWidget("BtnMuki");
                widget.Delegate_MouseHitEvent = Event_CsharpImpl.GetInstance().Delegate_BtnMuki;
            }
        }

    }
}
