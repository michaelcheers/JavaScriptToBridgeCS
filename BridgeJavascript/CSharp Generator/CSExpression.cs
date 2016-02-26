namespace BridgeJavascript.CSharp_Generator
{
    public abstract class CSExpression
    {
        public abstract string GenerateCS();
        public override string ToString()
            => GenerateCS();
    }
}