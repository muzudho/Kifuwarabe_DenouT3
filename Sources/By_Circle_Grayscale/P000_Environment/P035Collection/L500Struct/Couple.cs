namespace Grayscale.P035Collection.L500Struct
{

    /// <summary>
    /// カップル。組になっている２つのもの。
    /// 
    /// 使用例：「スプライトと、升」、「スプライトと、升のリスト」など。
    /// </summary>
    public class Couple<T1,T2>
    {

        public T1 A { get { return this.a; } }
        private T1 a;

        public T2 B { get { return this.b; } }
        public T2 b;

        public Couple(T1 src_a, T2 src_b)
        {
            this.a = src_a;
            this.b = src_b;
        }
    }
}
