using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jint.Parser.Ast;

namespace BridgeJavascript.CSharp_Generator
{
    class CSReturnStatement : CSStatement
    {
        public CSExpression value;

        public CSReturnStatement(CSExpression value)
        {
            this.value = value;
        }

        public override TabString GenerateCS() =>
            "return " + value + ";";
    }
}
