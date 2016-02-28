using System;

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
                            result += '\t';
                            break;
                        }
                    case '\b':
                        {
                            tabs--;
                            result = result.Remove(result.LastIndexOf('\t'), 1);
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