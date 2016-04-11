using Grayscale.P206_Json_______.L___500_Struct;
using System.Collections.Generic;
using System.Text;

namespace Grayscale.P206_Json_______.L500____Struct
{
    /// <summary>
    /// 配列
    /// </summary>
    public class Json_Arr : Json_Val
    {

        public List<Json_Val> elements { get; set; }

        public bool NewLineEnable { get; set; }

        public Json_Arr()
        {
            this.elements = new List<Json_Val>();
        }

        public void Add(Json_Val element)
        {
            this.elements.Add(element);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[");
            if (this.NewLineEnable)
            {
                sb.AppendLine();
            }

            int count = 0;
            foreach (Json_Val element in this.elements)
            {
                if (this.NewLineEnable)
                {
                    sb.Append("    ");
                }

                if (0 < count)
                {
                    sb.Append(",");
                }

                sb.Append(element.ToString());
                if (this.NewLineEnable)
                {
                    sb.AppendLine();
                }

                count++;
            }

            sb.Append("]");
            if (this.NewLineEnable)
            {
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
