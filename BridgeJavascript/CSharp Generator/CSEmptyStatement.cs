using System;
using BridgeJavascript.CSharp_Generator;

namespace BridgeJavascript
{
    public class CSEmptyStatement : CSStatement
    {
        public override string GenerateCS() =>
            "";
    }
}