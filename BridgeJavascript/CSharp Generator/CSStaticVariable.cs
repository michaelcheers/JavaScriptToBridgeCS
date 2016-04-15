namespace BridgeJavascript.CSharp_Generator
{
    using System;
    using System.Collections.Generic;
    using static CSVariableDeclaration;

    public class CSStaticVariable : CSClass.Declarable
    {
        public List<VariableDeclarator> value = new List<VariableDeclarator>();
        public CSExpression setTo;
        public string type = "object";

        public override string GenerateCS()
        {
            var result = "[Name(false)]\npublic static " + type + " "; 
            result += string.Join(", ", value);
            result += " = ";
            result += setTo;
            return result + ";";
        }
    }
}