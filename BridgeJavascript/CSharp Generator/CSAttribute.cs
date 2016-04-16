using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeJavascript.CSharp_Generator
{
    public class CSAttribute : CSCallExpression
    {
        public override string GenerateCS() =>
            "[" + (arguments.Length == 0 ? base.GenerateCS().Replace("()", "") : base.GenerateCS()) + "]";
    }
}
