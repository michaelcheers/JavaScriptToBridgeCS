using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeJavascript.CSharp_Generator
{
    public class CSArrayExpression : CSExpression
    {
        public override string GenerateCS() =>
            "new " + type + "{" + string.Join<CSExpression>(", ", elements) + "}";
        public CSExpression[] elements;
        public string type = "object[]";

        public override string Type
        {
            get
            {
                return type;
            }
        }
    }
}
