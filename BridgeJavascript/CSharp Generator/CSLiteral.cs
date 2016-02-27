using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeJavascript.CSharp_Generator
{
    public class CSLiteral : CSExpression
    {
        public string value;

        public CSLiteral(string value)
        {
            this.value = value;
        }

        public override string GenerateCS() => value;

        public override string Type
        {
            get
            {
                return value == null ? "object" : ((value == "true" || value == "false") ? "bool" : (value.First() == '\"' ? "string" : "double"));
            }
        }
    }
}
