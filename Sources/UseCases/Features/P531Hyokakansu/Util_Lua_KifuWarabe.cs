using System;
using System.IO;
using Grayscale.Kifuwarakaku.Entities.Features;
using Nett;
using NLua;

namespace Grayscale.P531Hyokakansu.L060UtilLua
{


    public abstract class Util_Lua_KifuWarabe
    {

        private static Lua lua;

        public static float Score { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="luaFuncName">実行したいLua関数の名前。</param>
        public static void Perform(string luaFuncName)
        {
            //System.Windows.Forms.MessageBox.Show("[" + luaFuncName + "]呼び出し");

            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));
            string dataDirectory = Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("DataDirectory"));

            using (Util_Lua_KifuWarabe.lua = new Lua())
            // 要設定 プラットフォームターゲット x64。32bit/64bit混在できない。
            // KifuNaraveVs, KifuWarabe
            {
                try
                {
                    // 初期化
                    lua.LoadCLRPackage();
                    Util_Lua_KifuWarabe.Score = 0;

                    //
                    // 関数の登録
                    //

                    // Lua「debugOut("あー☆")」
                    // ↓
                    // C#「C onsole.WriteLine("あー☆")」
                    lua.RegisterFunction("debugOut", typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));

                    // Lua「random(0,100)」
                    // ↓
                    // C#「Util_Lua_KifuWarabe.Random(0,100)」
                    lua.RegisterFunction("random", typeof(Util_Lua_KifuWarabe).GetMethod("Random", new Type[] { typeof(float), typeof(float) }));

                    //----------------------------------------------------------------------------------------------------

                    string file = Path.Combine(dataDirectory, "lua/KifuWarabe/data_score.lua");
                    //System.Windows.Forms.MessageBox.Show("[" + file + "]ファイル読込み");

                    Util_Lua_KifuWarabe.lua.DoFile(file);// KifuNarabeVS の、bin/Release等に入れ忘れていないこと。

                    //System.Windows.Forms.MessageBox.Show("[" + luaFuncName + "]呼び出し");
                    Util_Lua_KifuWarabe.lua.GetFunction(luaFuncName).Call();

                    //System.Windows.Forms.MessageBox.Show("スコアは？");
                    var score2 = lua["score"];
                    //System.Windows.Forms.MessageBox.Show("スコアの型は[" + score2.GetType().Name + "]");

                    Util_Lua_KifuWarabe.Score = (float)score2;

                    //Util_Lua_KifuWarabe.lua.Close(); // アプリが終了してしまう？

                    //----------------------------------------------------------------------------------------------------

                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.GetType().Name + "：" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="begin">この値を含む始端値。int型にキャストして使われます。</param>
        /// <param name="end">この値を含む終端値。int型にキャストして使われます。</param>
        /// <returns>int型にキャストして使われます。</returns>
        public static float Random(float begin, float end)
        {
            return KwRandom.Random.Next((int)begin, (int)end);
        }

    }


}
