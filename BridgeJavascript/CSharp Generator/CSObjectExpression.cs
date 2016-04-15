using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeJavascript.CSharp_Generator
{
    public class CSObjectExpression : CSExpression
    {
        public Dictionary<string, CSExpression> value;
        public string type = "";

        public override string GenerateCS()
        {
            return "new " + (string.IsNullOrEmpty(type) ? "" : (type + " ")) + value.ToJSONString(v => v.GenerateCS());
        }
    }
}
