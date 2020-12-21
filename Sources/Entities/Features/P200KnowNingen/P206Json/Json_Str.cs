using System.Text;

namespace Grayscale.Kifuwarakaku.Entities.Features
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
