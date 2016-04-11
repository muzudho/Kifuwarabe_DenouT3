using System.Collections.Generic;
using System.Diagnostics;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //フィンガー番号

namespace Grayscale.P224_Sky________.L500____Struct
{

    /// <summary>
    /// 天空を指差すもの。
    /// </summary>
    public class Fingers
    {
        /// <summary>
        /// エラー
        /// </summary>
        //[Obsolete("40番をエラー番号扱いにするのも、作り方が古い。-1に変えておく。")]
        public static readonly Finger Error_1 = -1;//40番をエラー番号扱いにするのも、作り方が古い。-1に変えておく。



        public List<Finger> Items { get{return this.items;} }
        private List<Finger> items;

        public Finger this[int i]
        {
            set { this.items[i] = value; }
            get
            {
                Finger result;

                Debug.Assert(0 < this.Items.Count, "Itemの個数が 1個未満。");

                if (0 < this.Items.Count)
                {
                    result = this.items[i];
                }
                else
                {
                    result = Error_1;// FIXME: 1回でも[初期配置]ボタンを押していないと、配列サイズが 0 です。
                }

                return result;
            }
        }

        public int Count { get { return this.items.Count; } }

        public Fingers()
        {
            this.items = new List<Finger>();
        }

        public void Add(Finger finger)
        {
            this.items.Add(finger);
        }

        public Finger ToFirst()
        {
            Finger finger = Fingers.Error_1;

            if (0 < this.Count)
            {
                finger = this[0];
            }

            return finger;
        }

    }
}
