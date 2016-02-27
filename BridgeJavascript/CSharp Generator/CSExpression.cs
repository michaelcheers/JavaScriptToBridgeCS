namespace BridgeJavascript.CSharp_Generator
{
    public abstract class CSExpression
    {
        public virtual string Type{get { return "object"; }}
        public abstract string GenerateCS();
        public override string ToString()
            => GenerateCS();
    }
}