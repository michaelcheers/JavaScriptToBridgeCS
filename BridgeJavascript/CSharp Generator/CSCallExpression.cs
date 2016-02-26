using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeJavascript.CSharp_Generator
{
    public class CSCallExpression : CSExpression
    {
        public CSExpression callee;
        public CSExpression[] arguments;

        public override string GenerateCS() =>
            callee + "(" + string.Join<CSExpression>(", ", arguments) + ")";
    }
}
