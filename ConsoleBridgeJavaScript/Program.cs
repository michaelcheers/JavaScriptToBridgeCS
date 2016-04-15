using BridgeJavascript;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBridgeJavaScript
{
    static class Program
    {
        public static void Main(string[] args)
        {
            var translator = new Translator();
                string value = ReadEndLine();
                File.WriteAllText("output.cs", translator.TranslateCode(value));
        }

        public static string ReadEndLine()
        {
            string result = "";
            string val;
            while ((val = Console.ReadLine()) != "end")
                result += val + "\n";
            return result;
        }
    }
}