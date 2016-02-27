using System;
using BridgeJavascript.CSharp_Generator;

namespace BridgeJavascript
{
    public class CSUnaryExpression : CSExpression
    {
        public CSExpression value;
        public enum Position
        {
            Left,
            Right
        }
        public Position valPos = Position.Right;
        public string @operator;
        public override string GenerateCS() =>
            valPos == Position.Right ? @operator + value : value + @operator;
    }
}