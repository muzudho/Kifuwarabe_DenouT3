namespace Grayscale.P145_SfenStruct_.L250____Struct
{

    /// <summary>
    /// SFENのstartpos文字列を入れているという明示をします。
    /// 
    /// string では分かりづらかったので。
    /// </summary>
    public class SfenstringImpl
    {
        public string ValueStr { get { return this.valueStr; } }
        private string valueStr;

        public SfenstringImpl()
        {
            this.valueStr = "";
        }

        public SfenstringImpl(string src)
        {
            this.valueStr = src;
        }

        public override string ToString()
        {
            return this.ValueStr;
        }
    }
}
