using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeJavascript.CSharp_Generator
{
    class CSIfStatement : CSStatement
    {
        public CSExpression test;
        public IEnumerable<CSStatement> consequent;
        public IEnumerable<CSStatement> alternate;

        public override TabString GenerateCS()
        {
            var result = "if (" + test + ")\n";
            result += Translator.ToBlockFunction(consequent);
            if (alternate != null)
                result += "\nelse\n" + Translator.ToBlockFunction(alternate);
            return result;
        }
    }
}
