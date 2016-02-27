namespace BridgeJavascript.CSharp_Generator
{
    public abstract class CSStatement
    {
        public abstract TabString GenerateCS();
        public override string ToString() =>
            GenerateCS();
    }
}