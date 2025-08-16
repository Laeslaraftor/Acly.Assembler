namespace System.Diagnostics.CodeAnalysis
{
    public sealed class NotNullWhenAttribute(bool returnValue) : Attribute
    {
        public bool ReturnValue { get; } = returnValue;
    }
}
