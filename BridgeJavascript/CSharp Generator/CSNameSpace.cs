using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeJavascript.CSharp_Generator
{
    class CSNameSpace
    {
        public List<CSElement> elements;
        public string name;

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append("namespace ");
            result.Append(name);
            result.Append("\n{\n");
            result.Append(string.Join("\n", elements));
            result.Append("}");
            return result.ToString();
        }
    }
}
