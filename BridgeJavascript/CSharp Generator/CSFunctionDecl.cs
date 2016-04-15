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
        public string[] genericParameters = new string[] { };
        public bool externAble = false;

        public override string GenerateCS()
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
            result.Append(externAble && function.blocks.TrueForAll(v => v is CSEmptyStatement) ? "extern " : "");
            result.Append(returnType);
            result.Append(" ");
            result.Append(name);
            result.Append(" ");
            result.Append(function.ToString(false, false, externAble));
            return result.ToString();
        }

        [Flags]
        public enum FuncKeywords
        {
            Static = 1
        }
    }
}