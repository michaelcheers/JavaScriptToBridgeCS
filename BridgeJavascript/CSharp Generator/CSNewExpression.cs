using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeJavascript.CSharp_Generator
{
    public class CSNewExpression : CSCallExpression
    {
        public override string GenerateCS() =>
            "new " + base.GenerateCS();
    }
}
