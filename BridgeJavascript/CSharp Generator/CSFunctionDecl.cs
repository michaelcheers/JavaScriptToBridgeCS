using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BridgeJavascript.CSharp_Generator
{
    public class CSFunctionDecl : CSClass.Declarable
    {
        public CSFunction function;
        public CSAttribute[] attributes;
        public string name;
        public FuncKeywords keyWords = FuncKeywords.Static;
        public string returnType;

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            if (attributes.Length > 0)
            {
                result.Append(string.Join<CSAttribute>("\n", attributes));
                result.Append("\n");
            }
            result.Append("public ");
            result.Append(keyWords.ToString().ToLower().Replace(',', ' '));
            result.Append(" ");
            result.Append(function.blocks.TrueForAll(v=>v is CSEmptyStatement) ? "extern " : "");
            result.Append(returnType);
            result.Append(" ");
            result.Append(name);
            result.Append(" ");
            result.Append(function.ToString(false, false, true));
            return result.ToString();
        }

        public override string GenerateCS()
        {
            throw new NotImplementedException();
        }

        [Flags]
        public enum FuncKeywords
        {
            Static = 1
        }
    }
}