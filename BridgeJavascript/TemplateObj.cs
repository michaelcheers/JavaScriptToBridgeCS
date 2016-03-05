using BridgeJavascript.CSharp_Generator;
using System.Collections.Generic;

namespace BridgeJavascript
{
    public class TemplateObj
    {
        public string name;
        public string templateText;
        public List<CSFunction.Parameter> parameters;
        public string returnType;
        public CSFunctionDecl.FuncKeywords keyWords = CSFunctionDecl.FuncKeywords.Static;
        public string[] generics = new string[] { };
    }
}