namespace BridgeJavascript.CSharp_Generator
{
    using System;
    public class TabString
    {
        public string Value
        {
            get
            {
                string result = "";
                int tabs = 0;
                foreach (var item in value)
                {
                    switch (item)
                    {
                        case '\n':
                            {
                                result += "\n";
                                for (int n = 0; n < tabs; n++)
                                    result += '\t';
                                break;
                            }
                        case '\t':
                            {
                                result += '\t';
                                tabs++;
                                break;
                            }
                        case '\b':
                            {
                                tabs--;
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

        public void AddTabs(int number = 1)
        {
            for (int n = 0; n < number; n++)
                value += "\t";
        }

        public void RemoveTabs(int number = 1)
        {
            for (int n = 0; n < number; n++)
                value += "\b";
        }

        public override string ToString() =>
            Value;

        public static implicit operator string (TabString value) =>
            value.Value;

        public static implicit operator TabString (string value) =>
            new TabString(value);

        string value;

        public TabString(string value)
        {
            this.value = value;
        }
    }
}