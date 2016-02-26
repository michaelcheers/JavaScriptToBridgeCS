using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeJavascript.CSharp_Generator
{
    class CSMemberExpression : CSExpression
    {
        public CSExpression @object;
        public string property;

        public override string GenerateCS() =>
            @object + "." + property;
    }
}
