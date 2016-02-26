using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeJavascript.CSharp_Generator
{
    public class CSIdentifier : CSExpression
    {
        public string name;

        public override string GenerateCS() =>
            name;
    }
}
