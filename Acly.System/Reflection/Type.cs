namespace System.Reflection
{
    public abstract class Type
    {
        public abstract string Namespace { get; }
        public abstract string Name { get; }
        public string FullName => string.Empty;
    }
}
