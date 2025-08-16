namespace System.Runtime.Versioning
{
    public sealed class TargetFrameworkAttribute(string frameworkName) : Attribute
    {
        public string? FrameworkDisplayName { get; set; }
        public string FrameworkName { get; } = frameworkName;
    }
}
