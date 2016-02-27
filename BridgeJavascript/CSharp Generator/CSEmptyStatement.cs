using System;
using BridgeJavascript.CSharp_Generator;

namespace BridgeJavascript
{
    public class CSEmptyStatement : CSStatement
    {
        public CSEmptyStatement (bool semiColon) { this.semiColon = semiColon; }

        public override TabString GenerateCS() =>
            semiColon ? ";" : "";
        public bool semiColon;
    }
}