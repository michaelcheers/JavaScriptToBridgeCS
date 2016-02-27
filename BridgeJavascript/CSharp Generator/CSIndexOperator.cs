using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeJavascript.CSharp_Generator
{
    public class CSIndexOperator : CSExpression
    {
        public CSExpression @object;
        public CSExpression property;

        public override string GenerateCS() =>
            @object + "[" + property + "]";
    }
}
