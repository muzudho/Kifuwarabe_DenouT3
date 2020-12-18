namespace Grayscale.P580_Form_______
{
    using System;
    using Grayscale.P003Log.I500Struct;
    using Grayscale.P003Log.L500Struct;
    using Grayscale.P571KifuWarabe.L250UsiLoop;
    using Grayscale.P571KifuWarabe.L500KifuWarabe;

    /// <summary>
    /// プログラムのエントリー・ポイントです。
    /// </summary>
    class Program
    {
        /// <summary>
        /// Ｃ＃のプログラムは、
        /// この Main 関数から始まり、 Main 関数を抜けて終わります。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            IErrorController errH = ErrorControllerReference.EngineDefault;

            // 将棋エンジン　きふわらべ
            ProgramSupport programSupport = new ProgramSupport();
            programSupport.AtBegin(errH);
            bool isTimeoutShutdown;

            isTimeoutShutdown = false;

            try
            {

                // 
                // 図.
                // 
                //     プログラムの開始：  ここの先頭行から始まります。
                //     プログラムの実行：  この中で、ずっと無限ループし続けています。
                //     プログラムの終了：  この中の最終行を終えたとき、
                //                         または途中で Environment.Exit(0); が呼ばれたときに終わります。
                //                         また、コンソールウィンドウの[×]ボタンを押して強制終了されたときも  ぶつ切り  で突然終わります。


                //************************************************************************************************************************
                // ループ（全体）
                //************************************************************************************************************************
                //
                // 図.
                //
                //      無限ループ（全体）
                //          │
                //          ├─無限ループ（１）
                //          │                      将棋エンジンであることが認知されるまで、目で訴え続けます(^▽^)
                //          │                      認知されると、無限ループ（２）に進みます。
                //          │
                //          └─無限ループ（２）
                //                                  対局中、ずっとです。
                //                                  対局が終わると、無限ループ（１）に戻ります。
                //
                // 無限ループの中に、２つの無限ループが入っています。
                //

                while (true)
                {
#if DEBUG_STOPPABLE
            MessageBox.Show("きふわらべのMainの無限ループでブレイク☆！", "デバッグ");
            System.Diagnostics.Debugger.Break();
#endif
                    //
                    // USIループ（１つ目）
                    //
                    UsiLoop1 usiLoop1 = new UsiLoop1(programSupport);
                    usiLoop1.AtStart();
                    bool isTimeoutShutdown_temp;
                    PhaseResult_UsiLoop1 result_UsiLoop1 = usiLoop1.AtBody(out isTimeoutShutdown_temp);
                    usiLoop1.AtEnd();
                    if (isTimeoutShutdown_temp)
                    {
                        //MessageBox.Show("ループ１で矯正終了するんだぜ☆！");
                        isTimeoutShutdown = isTimeoutShutdown_temp;
                        goto gt_EndMethod;
                    }
                    else if (result_UsiLoop1 == PhaseResult_UsiLoop1.Quit)
                    {
                        goto gt_EndMethod;//全体ループを抜けます。
                    }

                    //
                    // USIループ（２つ目）
                    //
                    UsiLoop2 usiLoop2 = new UsiLoop2(programSupport.shogisasi, programSupport);
                    usiLoop2.AtBegin();
                    usiLoop2.AtBody(out isTimeoutShutdown_temp, errH);
                    usiLoop2.AtEnd();
                    if (isTimeoutShutdown_temp)
                    {
                        //MessageBox.Show("ループ２で矯正終了するんだぜ☆！");
                        isTimeoutShutdown = isTimeoutShutdown_temp;
                        goto gt_EndMethod;//全体ループを抜けます。
                    }
                }

            }
            catch (Exception ex)
            {
                // エラーが起こりました。
                //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                // どうにもできないので  ログだけ取って無視します。
                ErrorControllerReference.EngineDefault.Panic("Program「大外枠でキャッチ」：" + ex.GetType().Name + " " + ex.Message);
            }

        gt_EndMethod:
            ;

            programSupport.AtEnd();
        }
    }
}
