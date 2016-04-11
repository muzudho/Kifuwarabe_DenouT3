using System;
using System.Text;


namespace Grayscale.P760_SigmoidTest
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Console.WriteLine("targetって何だぜ☆？");
            Console.WriteLine("func(-256.0) → [" + func(-256.0) + "]");
            Console.WriteLine("func(-192.0) → [" + func(-192.0) + "]");
            Console.WriteLine("func(-128.0) → [" + func(-128.0) + "]");
            Console.WriteLine("func(-  1.0) → [" + func(-1.0) + "]");
            Console.WriteLine("func(   0.0) → [" + func(   0.0) + "]");
            Console.WriteLine("func(   1.0) → [" + func(   1.0) + "]");
            Console.WriteLine("func( 128.0) → [" + func(128.0) + "]");
            Console.WriteLine("func( 192.0) → [" + func(192.0) + "]");
            Console.WriteLine("func( 256.0) → [" + func(256.0) + "]");
            Console.WriteLine();
             */

            /*
            Console.WriteLine("評価値の修正点？");
            Console.WriteLine("dfunc(-256.0) → [" + dfunc(-256.0) + "]。 ÷32 → [" + (dfunc(-256.0) / FV_SCALE) + "]");
            Console.WriteLine("dfunc(-255.0) → [" + dfunc(-255.0) + "]。 ÷32 → [" + (dfunc(-255.0) / FV_SCALE) + "]");
            Console.WriteLine("dfunc(-244.0) → [" + dfunc(-224.0) + "]。 ÷32 → [" + (dfunc(-224.0) / FV_SCALE) + "]");
            Console.WriteLine("dfunc(-192.0) → [" + dfunc(-192.0) + "]。 ÷32 → [" + (dfunc(-192.0) / FV_SCALE) + "]");
            Console.WriteLine("dfunc(-128.0) → [" + dfunc(-128.0) + "]。 ÷32 → [" + (dfunc(-128.0) / FV_SCALE) + "]");
            Console.WriteLine("dfunc(- 96.0) → [" + dfunc(-96.0) + "]。 ÷32 → [" + (dfunc(-96.0) / FV_SCALE) + "]");
            Console.WriteLine("dfunc(- 64.0) → [" + dfunc(-64.0) + "]。 ÷32 → [" + (dfunc(-64.0) / FV_SCALE) + "]");
            Console.WriteLine("dfunc(- 32.0) → [" + dfunc(-32.0) + "]。 ÷32 → [" + (dfunc(-32.0) / FV_SCALE) + "]");
            Console.WriteLine("dfunc(- 16.0) → [" + dfunc(-16.0) + "]。 ÷32 → [" + (dfunc(-16.0) / FV_SCALE) + "]");
            Console.WriteLine("dfunc(-  1.0) → [" + dfunc(-1.0) + "]。 ÷32 → [" + (dfunc(-1.0) / FV_SCALE) + "]");
            Console.WriteLine("dfunc(   0.0) → [" + dfunc(   0.0) + "]。 ÷32 → [" + (dfunc(0.0) / FV_SCALE) + "]");
            Console.WriteLine("dfunc(   1.0) → [" + dfunc(   1.0) + "]。 ÷32 → [" + (dfunc(1.0) / FV_SCALE) + "]");
            Console.WriteLine("dfunc(  16.0) → [" + dfunc(16.0) + "]。 ÷32 → [" + (dfunc(16.0) / FV_SCALE) + "]");
            Console.WriteLine("dfunc(  32.0) → [" + dfunc(32.0) + "]。 ÷32 → [" + (dfunc(32.0) / FV_SCALE) + "]");
            Console.WriteLine("dfunc(  64.0) → [" + dfunc(64.0) + "]。 ÷32 → [" + (dfunc(64.0) / FV_SCALE) + "]");
            Console.WriteLine("dfunc(  96.0) → [" + dfunc(96.0) + "]。 ÷32 → [" + (dfunc(96.0) / FV_SCALE) + "]");
            Console.WriteLine("dfunc( 128.0) → [" + dfunc(128.0) + "]。 ÷32 → [" + (dfunc(128.0) / FV_SCALE) + "]");
            Console.WriteLine("dfunc( 192.0) → [" + dfunc( 192.0) + "]。 ÷32 → [" + (dfunc(192.0) / FV_SCALE) + "]");
            Console.WriteLine("dfunc( 244.0) → [" + dfunc(224.0) + "]。 ÷32 → [" + (dfunc(224.0) / FV_SCALE) + "]");
            Console.WriteLine("dfunc( 255.0) → [" + dfunc(255.0) + "]。 ÷32 → [" + (dfunc(255.0) / FV_SCALE) + "]");
            Console.WriteLine("dfunc( 256.0) → [" + dfunc( 256.0) + "]。 ÷32 → [" + (dfunc(256.0) / FV_SCALE) + "]");
             */

            Console.WriteLine("Derivedシグモイド関数を眺めてみるんだぜ☆ +も-も結果は同じ☆");
            /*
            Console.WriteLine("　　　　　　　小数点第４位以下を四捨五入してみるぜ☆！");
            Console.WriteLine("dfunc(-256.0) → 約[" + string.Format("{0:0.0}", dfunc(-256.0)*1000) + "]　[" + dfunc(-256.0) + "]");
            Console.WriteLine("dfunc(-255.0) → 約[" + string.Format("{0:0.0}", dfunc(-255.0) * 1000) + "]　[" + dfunc(-255.0) + "]");
            Console.WriteLine("dfunc(-128.0) → 約[" + string.Format("{0:0.0}", dfunc(-128.0) * 1000) + "]　[" + dfunc(-128.0) + "]");
            Console.WriteLine("dfunc(-120.0) → 約[" + string.Format("{0:0.0}", dfunc(-120.0) * 1000) + "]　[" + dfunc(-120.0) + "]");
            Console.WriteLine("dfunc(-112.0) → 約[" + string.Format("{0:0.0}", dfunc(-112.0) * 1000) + "]　[" + dfunc(-112.0) + "]");
            Console.WriteLine("dfunc(-104.0) → 約[" + string.Format("{0:0.0}", dfunc(-104.0) * 1000) + "]　[" + dfunc(-104.0) + "]");
            Console.WriteLine("dfunc(- 96.0) → 約[" + string.Format("{0:0.0}", dfunc(-96.0) * 1000) + "]　[" + dfunc(-96.0) + "]");
            Console.WriteLine("dfunc(- 88.0) → 約[" + string.Format("{0:0.0}", dfunc(-88.0) * 1000) + "]　[" + dfunc(-88.0) + "]");
            Console.WriteLine("dfunc(- 80.0) → 約[" + string.Format("{0:0.0}", dfunc(-80.0) * 1000) + "]　[" + dfunc(-80.0) + "]");
            Console.WriteLine("dfunc(- 72.0) → 約[" + string.Format("{0:0.0}", dfunc(-72.0) * 1000) + "]　[" + dfunc(-72.0) + "]");
            Console.WriteLine("dfunc(- 64.0) → 約[" + string.Format("{0:0.0}", dfunc(-64.0) * 1000) + "]　[" + dfunc(-64.0) + "]");
            Console.WriteLine("dfunc(- 56.0) → 約[" + string.Format("{0:0.0}", dfunc(-56.0) * 1000) + "]　[" + dfunc(-56.0) + "]");
            Console.WriteLine("dfunc(- 48.0) → 約[" + string.Format("{0:0.0}", dfunc(-48.0) * 1000) + "]　[" + dfunc(-48.0) + "]");
            Console.WriteLine("dfunc(- 40.0) → 約[" + string.Format("{0:0.0}", dfunc(-40.0) * 1000) + "]　[" + dfunc(-40.0) + "]");
            Console.WriteLine("dfunc(- 32.0) → 約[" + string.Format("{0:0.0}", dfunc(-32.0) * 1000) + "]　[" + dfunc(-32.0) + "]");
            Console.WriteLine("dfunc(- 24.0) → 約[" + string.Format("{0:0.0}", dfunc(-24.0) * 1000) + "]　[" + dfunc(-24.0) + "]");
            Console.WriteLine("dfunc(- 16.0) → 約[" + string.Format("{0:0.0}", dfunc(-16.0) * 1000) + "]　[" + dfunc(-16.0) + "]");
            Console.WriteLine("dfunc(-  8.0) → 約[" + string.Format("{0:0.0}", dfunc(-8.0) * 1000) + "]　[" + dfunc(-8.0) + "]");
            Console.WriteLine("dfunc(   0.0) → 約[" + string.Format("{0:0.0}", dfunc(-0.0) * 1000) + "]　[" + dfunc(0.0) + "]");
             */
            Console.WriteLine("　※Ａ……Derivedシグモイドする　※Ｂ……変化手に下方修正する特徴量1000倍表示");
            Program.Display(256.0f);
            Program.Display(255.0f);
            Console.WriteLine("　～中略～");
            Program.Display(144.0f);
            Program.Display(136.0f);
            Program.Display(128.0f);
            Program.Display(120.0f);
            Program.Display(112.0f);
            Program.Display(104.0f);
            Program.Display(96.0f);
            Program.Display(88.0f);
            Program.Display(80.0f);
            Program.Display(72.0f);
            Program.Display(64.0f);
            Program.Display(56.0f);
            Program.Display(48.0f);
            Program.Display(40.0f);
            Program.Display(32.0f);
            Program.Display(24.0f);
            Program.Display(16.0f);
            Program.Display(8.0f);
            Program.Display(0.0f);

            Console.ReadKey();
        }

        static void Display(float value_diff)
        {
            string predi = string.Format("{0:0.00}", func(value_diff) );
            string dLv = string.Format("{0:0.0}", dfunc(value_diff) * 1000);
            float d;
            float.TryParse(dLv, out d);
            StringBuilder sb = new StringBuilder();
            //sb.Append("変化の本譜との誤差" + string.Format("{0,6:0.0}", value) + "→(※Ａ)→約" + level + "(※Ｂ)");
            sb.Append("評価値の差" + string.Format("{0,6:0.0}", value_diff) + "→(※Ａ)→約" + dLv + "(※Ｂ)p=" + predi);

            int max = (int)(d*10.0/2);
            for (int i = 0; i < max;i++ )
            {
                sb.Append(".");
            }
            //sb.Append("　[" + dfunc(value) + "]");

            Console.WriteLine(sb.ToString());
        }

        private const float FV_WINDOW = 256;
        private const float FV_SCALE = 32;

        /// <summary>
        /// シグモイド関数
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static float func(float x)
        {
            float delta = (float)FV_WINDOW / 7.0f;
            float d;

            if (x < -FV_WINDOW) { x = -FV_WINDOW; }
            else if (x > FV_WINDOW) { x = FV_WINDOW; }

            d = 1.0f / (1.0f+(float)Math.Exp(-x/delta));

            return d;
        }

        /// <summary>
        /// シグモイド関数(導関数；derived function)
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static float dfunc(float x)
        {
            const float delta = (float)FV_WINDOW / 7.0f;
            float dd, dn, dtemp, dret;

            if (x <= -FV_WINDOW) { dret = 0.0f; }
            else if (x >= FV_WINDOW) { dret = 0.0f; }
            else
            {
                dn = (float)Math.Exp(-x/delta);
                dtemp = dn + 1.0f;
                dd = delta * dtemp * dtemp;
                dret = dn / dd;
            }

            return dret;
        }

    }
}
