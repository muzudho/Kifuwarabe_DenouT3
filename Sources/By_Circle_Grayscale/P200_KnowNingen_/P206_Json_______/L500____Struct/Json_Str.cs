using Grayscale.P206_Json_______.L___500_Struct;
using System.Text;

namespace Grayscale.P206_Json_______.L500____Struct
{
    /// <summary>
    /// 文字列
    /// </summary>
    public class Json_Str : Json_Val
    {

        public string Value { get; set; }

        public Json_Str(string value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\"");
            sb.Append(this.Value);
            sb.Append("\"");

            return sb.ToString();
        }
    }
}
