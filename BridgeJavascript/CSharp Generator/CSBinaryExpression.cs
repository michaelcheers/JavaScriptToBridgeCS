using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeJavascript.CSharp_Generator
{
    class CSBinaryExpression : CSExpression
    {
        public string @operator;
        public CSExpression left;
        public CSExpression right;

        public override string GenerateCS() =>
            left + @operator + right;
    }
}
