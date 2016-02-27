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
                this.name = name;
                this.type = type;
            }
        }

        public override string ToString() =>
            ToString(true, true);
        
        public string ToString (bool useLinq, bool usingLinq = false, bool allowEmptyExtern = false)
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
            if (allowEmptyExtern && blocks.TrueForAll(v => v is CSEmptyStatement))
                return inForeach + ";";
            if (useLinq)
                inForeach += " => ";
            inForeach += "\n";
            if (blocks.TrueForAll(v => v is CSEmptyStatement) && allowEmptyExtern)
                inForeach += Translator.ToBlockFunction(blocks);
            return inForeach;
        }
    }
}