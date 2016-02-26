using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeJavascript.CSharp_Generator
{
    public class CSLiteral : CSExpression
    {
        string value;

        public CSLiteral(string value)
        {
            this.value = value;
        }

        public override string GenerateCS() => value;
    }
}
