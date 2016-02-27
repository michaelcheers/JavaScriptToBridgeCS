using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeJavascript.CSharp_Generator
{
    public class CSForStatement : CSStatement
    {
        public CSStatement init;
        public List<CSStatement> body;
        public CSExpression test;
        public CSExpression update;

        public override TabString GenerateCS() =>
            "for (" + init + " " + test + "; " + update + ")\n" + Translator.ToBlockFunction(body);
    }
}
