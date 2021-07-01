namespace Grayscale.Kifuwarakaku.GuiOfCsharp.Features
{
    using System;
    using System.Configuration;
    using System.IO;
    using Grayscale.Kifuwarakaku.Entities.Features;
    using Grayscale.Kifuwarakaku.UseCases.Features;
    using Nett;
    using NLua;

    public abstract class Util_Lua_Csharp
    {
        private static Lua lua;

        public static MainGui_Csharp ShogiGui { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="luaFuncName">実行したいLua関数の名前。</param>
        public static void Perform(string luaFuncName)
        {

            using (Util_Lua_Csharp.lua = new Lua())
            {
                // 初期化
                lua.LoadCLRPackage();


                //
                // 関数の登録
                //

                // Lua「debugOut("あー☆")」
                // ↓
                // C#「C onsole.WriteLine("あー☆")」
                lua.RegisterFunction("debugOut", typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));

                // Lua「screen_refresh()」
                // ↓
                // C#「Util_Lua.Screen_Redraw()」
                lua.RegisterFunction("screen_refresh", typeof(Util_Lua_Csharp).GetMethod("Screen_Redraw", new Type[] { }));

                // Lua「screen_clearOutput()」
                // ↓
                // C#「Util_Lua.screen_redrawStarlights()」
                lua.RegisterFunction("screen_refreshStarlights", typeof(Util_Lua_Csharp).GetMethod("Screen_RedrawStarlights", new Type[] { }));



                // Lua「inputBox_play()」
                // ↓
                // C#「Util_Lua.InputBox_Play()」
                lua.RegisterFunction("inputBox_play", typeof(Util_Lua_Csharp).GetMethod("InputBox_Play", new Type[] { }));



                // Lua「outputBox_clear()」
                // ↓
                // C#「Util_Lua.Screen_ClearOutput()」
                lua.RegisterFunction("outputBox_clear", typeof(Util_Lua_Csharp).GetMethod("OutputBox_Clear", new Type[] { }));



                // Lua「kifu_clear()」
                // ↓
                // C#「Util_Lua.Kifu_Clear()」
                lua.RegisterFunction("kifu_clear", typeof(Util_Lua_Csharp).GetMethod("Kifu_Clear", new Type[] { }));


                //----------------------------------------------------------------------------------------------------
                var profilePath = ConfigurationManager.AppSettings["Profile"];
                var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));
                string dataDirectory = Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("DataDirectory"));

                Util_Lua_Csharp.lua.DoFile(Path.Combine(dataDirectory, "lua/KifuNarabe/data_gui.lua"));//固定
                Util_Lua_Csharp.lua.GetFunction(luaFuncName).Call();

                // FIXME:Close()でエラーが起こってしまう。
                //Util_Lua_KifuNarabe.lua.Close();

                //----------------------------------------------------------------------------------------------------

            }
        }


        public static void Screen_Redraw()
        {
            Util_Lua_Csharp.ShogiGui.RepaintRequest.SetFlag_RefreshRequest();
        }

        public static void Screen_RedrawStarlights()
        {
            Util_Lua_Csharp.ShogiGui.RepaintRequest.SetFlag_RecalculateRequested();// 駒の再描画要求
        }

        public static void InputBox_Play()
        {
            // [再生]タイマー開始☆
            ((TimedC_SaiseiCapture)Util_Lua_Csharp.ShogiGui.TimedC).SaiseiEventQueue.Enqueue(new SaiseiEventState(SaiseiEventStateName.Start));
        }

        public static void OutputBox_Clear()
        {
            Util_Lua_Csharp.ShogiGui.RepaintRequest.SyuturyokuRequest = RepaintRequestGedanTxt.Clear;
        }

        public static void Kifu_Clear()
        {
            Util_Lua_Csharp.ClearKifu(Util_Lua_Csharp.ShogiGui, Util_Lua_Csharp.ShogiGui.RepaintRequest);
        }





        /// <summary>
        /// ************************************************************************************************************************
        /// 将棋盤の上の駒を、全て駒袋に移動します。 [クリアー]
        /// ************************************************************************************************************************
        /// </summary>
        public static void ClearKifu(MainGui_Csharp mainGui, RepaintRequest repaintRequest)
        {
            mainGui.Link_Server.Model_Taikyoku.Kifu.Clear();// 棋譜を空っぽにします。

            SkyBuffer buffer_Sky = new SkyBuffer(mainGui.Model_Manual.GuiSkyConst);

            int figKoma;

            // 先手
            figKoma = (int)Finger_Honshogi.SenteOh;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(/*figKoma,*/ new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01), PieceType.K))); //先手王
            figKoma = (int)Finger_Honshogi.GoteOh;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro02), PieceType.K))); //後手王

            figKoma = (int)Finger_Honshogi.Hi1;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro03), PieceType.R))); //飛
            figKoma = (int)Finger_Honshogi.Hi2;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro04), PieceType.R)));

            figKoma = (int)Finger_Honshogi.Kaku1;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro05), PieceType.B))); //角
            figKoma = (int)Finger_Honshogi.Kaku2;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro06), PieceType.B)));

            figKoma = (int)Finger_Honshogi.Kin1;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro07), PieceType.G))); //金
            figKoma = (int)Finger_Honshogi.Kin2;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro08), PieceType.G)));
            figKoma = (int)Finger_Honshogi.Kin3;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro09), PieceType.G)));
            figKoma = (int)Finger_Honshogi.Kin4;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro10), PieceType.G)));

            figKoma = (int)Finger_Honshogi.Gin1;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro11), PieceType.S))); //銀
            figKoma = (int)Finger_Honshogi.Gin2;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro12), PieceType.S)));
            figKoma = (int)Finger_Honshogi.Gin3;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro13), PieceType.S)));
            figKoma = (int)Finger_Honshogi.Gin4;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro14), PieceType.S)));

            figKoma = (int)Finger_Honshogi.Kei1;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro15), PieceType.N))); //桂
            figKoma = (int)Finger_Honshogi.Kei2;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro16), PieceType.N)));
            figKoma = (int)Finger_Honshogi.Kei3;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro17), PieceType.N)));
            figKoma = (int)Finger_Honshogi.Kei4;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro18), PieceType.N)));

            figKoma = (int)Finger_Honshogi.Kyo1;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro19), PieceType.L))); //香
            figKoma = (int)Finger_Honshogi.Kyo2;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro20), PieceType.L)));
            figKoma = (int)Finger_Honshogi.Kyo3;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro21), PieceType.L)));
            figKoma = (int)Finger_Honshogi.Kyo4;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro22), PieceType.L)));

            figKoma = (int)Finger_Honshogi.Fu1;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro23), PieceType.P))); //歩
            figKoma = (int)Finger_Honshogi.Fu2;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro24), PieceType.P)));
            figKoma = (int)Finger_Honshogi.Fu3;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro25), PieceType.P)));
            figKoma = (int)Finger_Honshogi.Fu4;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro26), PieceType.P)));
            figKoma = (int)Finger_Honshogi.Fu5;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro27), PieceType.P)));
            figKoma = (int)Finger_Honshogi.Fu6;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro28), PieceType.P)));
            figKoma = (int)Finger_Honshogi.Fu7;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro29), PieceType.P)));
            figKoma = (int)Finger_Honshogi.Fu8;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro30), PieceType.P)));
            figKoma = (int)Finger_Honshogi.Fu9;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P1, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro31), PieceType.P)));

            figKoma = (int)Finger_Honshogi.Fu10;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro32), PieceType.P))); //歩
            figKoma = (int)Finger_Honshogi.Fu11;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro33), PieceType.P)));
            figKoma = (int)Finger_Honshogi.Fu12;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro34), PieceType.P)));
            figKoma = (int)Finger_Honshogi.Fu13;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro35), PieceType.P)));
            figKoma = (int)Finger_Honshogi.Fu14;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro36), PieceType.P)));
            figKoma = (int)Finger_Honshogi.Fu15;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro37), PieceType.P)));
            figKoma = (int)Finger_Honshogi.Fu16;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro38), PieceType.P)));
            figKoma = (int)Finger_Honshogi.Fu17;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro39), PieceType.P)));
            figKoma = (int)Finger_Honshogi.Fu18;
            buffer_Sky.PutOverwriteOrAdd_Starlight(figKoma, new RO_Starlight(new RO_Star(Playerside.P2, Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro40), PieceType.P)));


            {
                KifuNode newNode = new KifuNodeImpl(
                            Util_Sky258A.RootMove,//ルートなので
                            new KyokumenWrapper(SkyConst.NewInstance(
                                buffer_Sky,
                                0//空っぽに戻すので、 0手済みに変更。
                                ))
                        );

                string jsaFugoStr;
                Util_Functions_Server.SetCurNode_Srv(
                    mainGui.Link_Server.Model_Taikyoku,
                    mainGui.Model_Manual,
                    newNode,
                    out jsaFugoStr);
                repaintRequest.SetFlag_RefreshRequest();

                mainGui.Link_Server.Model_Taikyoku.Kifu.SetProperty(Word_KifuTree.PropName_Startpos, "9/9/9/9/9/9/9/9/9 b K1R1B1G2S2N2L2P9 k1r1b1g2s2n2l2p9 1");
            }
        }


    }
}
