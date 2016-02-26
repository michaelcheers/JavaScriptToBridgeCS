using BridgeJavascript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBridgeJavaScript
{
    class Program
    {
        static void Main(string[] args)
        {
            var translator = new Translator();
            while (true)
            {
                string value = Console.ReadLine();
                Console.WriteLine(translator.TranslateCode(value));
            }
        }
    }
}