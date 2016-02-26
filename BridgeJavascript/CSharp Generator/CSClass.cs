using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeJavascript.CSharp_Generator
{
    public class CSClass : CSElement
    {
        public abstract class Declarable
        {
            public abstract string GenerateCS();
            public override string ToString() =>
                GenerateCS();
        }
        
        public List<CSAttribute> attributes = new List<CSAttribute>();
        public List<CSStaticVariable> variables = new List<CSStaticVariable>();
        public List<Declarable> declarables;
        public string name;
        public CSFunctionDecl.FuncKeywords keyWords;

        public string ConvertToCSharp ()
        {
            var result = "";
            result += string.Join("\n", attributes);
            if (attributes.Count > 0)
            result += "\n";
            result += keyWords.ToString().ToLower().Replace(',', ' ');
            result += " class ";
            result += name;
            result += "\n{\n";
            result += string.Join("\n", declarables);
            result += "\n}";
            return result;
        }

        public override string ToString() =>
            ConvertToCSharp();
    }
}
