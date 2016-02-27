using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeJavascript.CSharp_Generator
{
    class CSExpressionStatement : CSStatement
    {
        public CSExpression value;

        public CSExpressionStatement(CSExpression value)
        {
            this.value = value;
        }

        public override TabString GenerateCS() =>
            value.GenerateCS() + ";";
    }
}
