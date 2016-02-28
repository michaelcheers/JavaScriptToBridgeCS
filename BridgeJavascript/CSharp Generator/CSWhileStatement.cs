using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeJavascript.CSharp_Generator
{
    public class CSWhileStatement : CSStatement
    {
        public CSExpression test;
        public IEnumerable<CSStatement> body;
        public bool @do;

        public override string GenerateCS()
        {
            var result = @do ? "do\n" : "while (" + test + ")\n";
            result += Translator.ToBlockFunction(body);
            if (@do)
                result += "\nwhile (" + test + ");";
            return result;
        }
    }
}
