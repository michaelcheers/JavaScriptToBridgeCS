namespace BridgeJavascript.CSharp_Generator
{
    public abstract class CSStatement
    {
        public abstract string GenerateCS();
        public override string ToString() =>
            GenerateCS();
    }
}