using System;
using System.Collections.Generic;
using System.Linq;

namespace BridgeJavascript
{
    internal class TabString
    {
        public static string Create(string inForeach)
        {
            int tabs = 0;
            string result = "";

            foreach (var item in inForeach)
            {
                switch (item)
                {
                    case '\t':
                        {
                            tabs++;
                            goto default;
                        }
                    case '\b':
                        {
                            tabs--;
                            result = result.Remove(result.LastIndexOf('\t'), 1);
                            break;
                        }
                    case '\n':
                        {
                            result += '\n';
                            result += new string(Array.ConvertAll(new char[tabs], v => '\t'));
                            break;
                        }
                    default:
                        {
                            result += item;
                            break;
                        }
                }
            }
            return result;
        }
    }
}