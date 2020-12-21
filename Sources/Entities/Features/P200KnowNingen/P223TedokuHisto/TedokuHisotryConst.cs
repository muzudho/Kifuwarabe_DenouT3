using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using System.Collections.Generic;

#if DEBUG
using Grayscale.Kifuwarakaku.Entities.Features;
using Nett;
using System.IO;
#endif

namespace Grayscale.Kifuwarakaku.Entities.Features
{
    /// <summary>
    /// 手得ヒストリー
    /// </summary>
    public class TedokuHistoryConst : TedokuHistory
    {
        #region プロパティー

        public List<SyElement>[] Fu___ { get { return this.fu; } }
        private List<SyElement>[] fu;

        public List<SyElement>[] Kyo__ { get { return this.kyo; } }
        private List<SyElement>[] kyo;

        public List<SyElement>[] Kei__ { get { return this.kei; } }
        private List<SyElement>[] kei;

        public List<SyElement>[] Gin__ { get { return this.gin; } }
        private List<SyElement>[] gin;

        public List<SyElement>[] Kin__ { get { return this.kin; } }
        private List<SyElement>[] kin;

        public List<SyElement> Gyoku { get { return this.gyoku; } }
        private List<SyElement> gyoku;

        public List<SyElement>[] Hisya { get { return this.hisya; } }
        private List<SyElement>[] hisya;

        public List<SyElement>[] Kaku_ { get { return this.kaku; } }
        private List<SyElement>[] kaku;

        #endregion

        /// <summary>
        /// 手得ヒストリーを進めます。
        /// </summary>
        /// <param name="src"></param>
        /// <param name="komasyurui"></param>
        /// <param name="index"></param>
        /// <param name="masu"></param>
        /// <returns></returns>
        public static TedokuHistory NewInstance_AppendSasitamasu(TedokuHistory src, Komasyurui14 komasyurui, int index, SyElement masu)
        {
            TedokuHistoryConst result = (TedokuHistoryConst)TedokuHistoryConst.New_Clone(src);

            // 持駒を打った場合はどうする？
            switch(komasyurui)
            {
                case Komasyurui14.H11_Tokin__://どの歩？
                case Komasyurui14.H01_Fu_____: result.Fu___[index].Add(masu); break;//suji-1=index
                case Komasyurui14.H12_NariKyo:
                case Komasyurui14.H02_Kyo____: result.Kyo__[index].Add(masu); break;
                case Komasyurui14.H13_NariKei:
                case Komasyurui14.H03_Kei____: result.Kei__[index].Add(masu); break;
                case Komasyurui14.H14_NariGin:
                case Komasyurui14.H04_Gin____: result.Gin__[index].Add(masu); break;
                case Komasyurui14.H05_Kin____: result.Kin__[index].Add(masu); break;
                case Komasyurui14.H06_Gyoku__: result.Gyoku.Add(masu); break;
                case Komasyurui14.H09_Ryu____:
                case Komasyurui14.H07_Hisya__: result.Hisya[index].Add(masu); break;
                case Komasyurui14.H10_Uma____:
                case Komasyurui14.H08_Kaku___: result.Kaku_[index].Add(masu); break;
                default: break;
            }

#if DEBUG
            //
            // ログ出力
            //
            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));
            Util_TedokuHistory.WriteLog(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("N19TedokuKeisanLog")), result);
#endif

            return result;
        }

        /// <summary>
        /// 平手初期局面、1P（盤の下側）。
        /// </summary>
        /// <returns></returns>
        public static TedokuHistory New_HirateSyokikyokumen_1P()
        {
            TedokuHistory result = new TedokuHistoryConst(
                new List<SyElement>[]{//歩
                    new List<SyElement>{Masu_Honshogi.Query_Basho( Masu_Honshogi.nban17_１七)},
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban27_２七)},
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban37_３七)},
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban47_４七)},
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban57_５七)},
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban67_６七)},
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban77_７七)},
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban87_８七)},
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban97_９七)},
                },
                new List<SyElement>[]{//香
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban19_１九)},
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban99_９九)},
                    new List<SyElement>{},
                    new List<SyElement>{},
                },
                new List<SyElement>[]{//桂
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban29_２九)},
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban89_８九)},
                    new List<SyElement>{},
                    new List<SyElement>{},
                },
                new List<SyElement>[]{//銀
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban39_３九)},
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban79_７九)},
                    new List<SyElement>{},
                    new List<SyElement>{},
                },
                new List<SyElement>[]{//金
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban49_４九)},
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban69_６九)},
                    new List<SyElement>{},
                    new List<SyElement>{},
                },
                new List<SyElement>{//玉
                    Masu_Honshogi.Query_Basho(Masu_Honshogi.nban59_５九)
                },
                new List<SyElement>[]{//飛
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban28_２八)},
                    new List<SyElement>{},
                },
                new List<SyElement>[]{//角
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban88_８八)},
                    new List<SyElement>{},
                }
                );
            return result;
        }


        /// <summary>
        /// 平手初期局面、2P（盤の上側）。
        /// </summary>
        /// <returns></returns>
        public static TedokuHistory New_HirateSyokikyokumen_2P()
        {
            TedokuHistory result = new TedokuHistoryConst(
                new List<SyElement>[]{//歩
                    new List<SyElement>{Masu_Honshogi.Query_Basho( Masu_Honshogi.nban13_１三)},//１三
                    new List<SyElement>{Masu_Honshogi.Query_Basho( Masu_Honshogi.nban23_２三)},
                    new List<SyElement>{Masu_Honshogi.Query_Basho( Masu_Honshogi.nban33_３三)},
                    new List<SyElement>{Masu_Honshogi.Query_Basho( Masu_Honshogi.nban43_４三)},
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban53_５三)},
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban63_６三)},
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban73_７三)},
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban83_８三)},
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban93_９三)},
                },
                new List<SyElement>[]{//香
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一)},
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban91_９一)},
                    new List<SyElement>{},
                    new List<SyElement>{},
                },
                new List<SyElement>[]{//桂
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban21_２一)},
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban81_８一)},
                    new List<SyElement>{},
                    new List<SyElement>{},
                },
                new List<SyElement>[]{//銀
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban31_３一)},
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban71_７一)},
                    new List<SyElement>{},
                    new List<SyElement>{},
                },
                new List<SyElement>[]{//金
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban41_４一)},
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban61_６一)},
                    new List<SyElement>{},
                    new List<SyElement>{},
                },
                new List<SyElement>{//玉
                    Masu_Honshogi.Query_Basho(Masu_Honshogi.nban51_５一)
                },
                new List<SyElement>[]{//飛
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban82_８二)},
                    new List<SyElement>{},
                },
                new List<SyElement>[]{//角
                    new List<SyElement>{Masu_Honshogi.Query_Basho(Masu_Honshogi.nban22_２二)},
                    new List<SyElement>{},
                }
                );
            return result;
        }

        /// <summary>
        /// 平手初期局面、2P（盤の上側）。
        /// </summary>
        /// <returns></returns>
        public static TedokuHistory New_Clone(TedokuHistory src)
        {
            // 歩のリスト配列
            List<SyElement>[] fu = new List<SyElement>[9];
            for (int i = 0; i < 9; i++)
            {
                fu[i] = new List<SyElement>(src.Fu___[i]);
            }

            // 香のリスト配列
            List<SyElement>[] kyo = new List<SyElement>[4];
            for (int i = 0; i < 4; i++)
            {
                kyo[i] = new List<SyElement>(src.Kyo__[i]);
            }

            // 桂のリスト配列
            List<SyElement>[] kei = new List<SyElement>[4];
            for (int i = 0; i < 4; i++)
            {
                kei[i] = new List<SyElement>(src.Kei__[i]);
            }

            // 銀のリスト配列
            List<SyElement>[] gin = new List<SyElement>[4];
            for (int i = 0; i < 4; i++)
            {
                gin[i] = new List<SyElement>(src.Gin__[i]);
            }

            // 金のリスト配列
            List<SyElement>[] kin = new List<SyElement>[4];
            for (int i = 0; i < 4; i++)
            {
                kin[i] = new List<SyElement>(src.Kin__[i]);
            }

            // 飛のリスト配列
            List<SyElement>[] hisya = new List<SyElement>[2];
            for (int i = 0; i < 2; i++)
            {
                hisya[i] = new List<SyElement>(src.Hisya[i]);
            }

            // 角のリスト配列
            List<SyElement>[] kaku = new List<SyElement>[2];
            for (int i = 0; i < 2; i++)
            {
                kaku[i] = new List<SyElement>(src.Kaku_[i]);
            }

            TedokuHistory result = new TedokuHistoryConst(
                fu,//歩
                kyo,//香
                kei,//桂
                gin,//銀
                kin,//金
                src.Gyoku,//玉
                hisya,//飛
                kaku//角
                );
            return result;
        }

        public TedokuHistoryConst(
            List<SyElement>[] fu,
            List<SyElement>[] kyo,
            List<SyElement>[] kei,
            List<SyElement>[] gin,
            List<SyElement>[] kin,
            List<SyElement> gyoku,
            List<SyElement>[] hisya,
            List<SyElement>[] kaku
            )
        {
            this.fu = fu;
            this.kyo = kyo;
            this.kei = kei;
            this.gin = gin;
            this.kin = kin;
            this.gyoku = gyoku;
            this.hisya = hisya;
            this.kaku = kaku;
        }
    }
}
