using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeJavascript.CSharp_Generator
{
    public class CSVariableDeclaration : CSStatement
    {
        public IEnumerable<VariableDeclarator> value;
        public CSExpression setTo;

        public class VariableDeclarator : CSExpression
        {
            public CSIdentifier id;
            public string type;

            public override string GenerateCS() =>
                id.GenerateCS();
        }

        public override string GenerateCS()
        {
            var result = "object ";
            result += string.Join(", ", value);
            result += " = ";
            result += setTo;
            return result + ";";
        }
    }
}
