using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeJavascript.CSharp_Generator
{
    public class CSFunction
    {
        public List<CSStatement> blocks = new List<CSStatement>();
        public List<CSAttribute> attributes = new List<CSAttribute>();
        public List<Parameter> parameters = new List<Parameter>();

        public class Parameter
        {
            public string type;
            public string name;
            
            public Parameter (string name, string type)
            {

            }
        }

        public override string ToString() =>
            ToString(true, false, true);
        
        public string ToString (bool useLinq, bool usingLinq, bool useCS60)
        {
            string inForeach = useLinq && parameters.Count == 1 ? "" : "(";
            for (int n = 0; n < parameters.Count; n++)
            {
                var item = parameters[n];
                inForeach += usingLinq ? "" : item.type + " ";
                inForeach += item.name;
                if (parameters.Count - 1 != n)
                    inForeach += ", ";
            }
            inForeach += useLinq && parameters.Count == 1 ? "" : ")";
            if (useLinq)
                inForeach += " => ";
            inForeach += "\n";
            inForeach += Translator.ToBlockFunction(blocks);
            return inForeach;
        }
    }
}