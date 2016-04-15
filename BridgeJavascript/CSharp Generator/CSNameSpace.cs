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
        public string[] @using;

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append("namespace ");
            result.Append(name);
            result.Append("\n{\n\t");
            for (int n = 0; n < @using.Length; n++)
            {
                var item = @using[n];
                result.Append("using ");
                result.Append(item);
                result.Append(";\n");
                if (n == @using.Length - 1)
                    result.Append("\n");
            }
            result.Append(string.Join("\n", elements));
            result.Append("\n}\b");
            return TabString.Create(result.ToString());
        }
    }
}
