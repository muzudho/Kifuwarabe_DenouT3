using Grayscale.P206_Json_______.L___500_Struct;
using System.Text;

namespace Grayscale.P206_Json_______.L500____Struct
{
    public class Json_Prop
    {

        public string Name { get; set; }

        public Json_Val Value { get; set; }


        public Json_Prop(string name, string value)
        {
            this.Name = name;
            this.Value = new Json_Str( value);
        }

        public Json_Prop(string name, Json_Val value)
        {
            this.Name = name;
            this.Value = value;
        }

        public Json_Prop(string name, int value)
        {
            this.Name = name;
            this.Value = new Json_Str( value.ToString());
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\"");
            sb.Append(this.Name);
            sb.Append("\"");

            sb.Append(":");

            string value2 = this.Value.ToString();
            float d;
            if (float.TryParse(value2, out d))
            {
                // 数字
                sb.Append(value2.ToString());
            }
            else if(
                this.Value is Json_Str// 文字
                || this.Value is Json_Arr// 配列
                || this.Value is Json_Obj// オブジェクト
                )
            {                
                sb.Append(this.Value.ToString());
            }
            else
            {
                // それ以外は未定義？
                sb.Append("\"未定義:");
                sb.Append(value2);
                sb.Append("\"");
            }

            return sb.ToString();
        }
    }
}
