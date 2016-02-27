using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeJavascript.CSharp_Generator
{
    class CSAssignmentExpression : CSExpression
    {
        public CSExpression left;
        public string @operator;
        public CSExpression right;

        public override string GenerateCS() =>
            left + " " + @operator + " " + right;

        public override string Type
        {
            get
            {
                return right.Type;
            }
        }
    }
}
