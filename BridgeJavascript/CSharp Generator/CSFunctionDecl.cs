using System;
using System.Collections.Generic;
using System.Text;

namespace BridgeJavascript.CSharp_Generator
{
    public class CSFunctionDecl : CSClass.Declarable
    {
        public CSFunction function;
        public CSAttribute[] attributes;
        public string name;
        public FuncKeywords keyWords;
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
            result.Append(returnType);
            result.Append(" ");
            result.Append(name);
            result.Append(" ");
            result.Append(function.ToString(false, false, false));
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