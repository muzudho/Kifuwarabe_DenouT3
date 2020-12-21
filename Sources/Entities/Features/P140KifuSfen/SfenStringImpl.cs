namespace Grayscale.Kifuwarakaku.Entities.Features
{

    /// <summary>
    /// SFENのstartpos文字列を入れているという明示をします。
    /// 
    /// string では分かりづらかったので。
    /// </summary>
    public class SfenStringImpl
    {
        public string ValueStr { get { return this.valueStr; } }
        private string valueStr;

        public SfenStringImpl()
        {
            this.valueStr = "";
        }

        public SfenStringImpl(string src)
        {
            this.valueStr = src;
        }

        public override string ToString()
        {
            return this.ValueStr;
        }
    }
}
