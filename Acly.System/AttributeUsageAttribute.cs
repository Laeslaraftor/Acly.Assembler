namespace System
{
    public sealed class AttributeUsageAttribute(AttributeTargets validOn) : Attribute
    {
        public bool AllowMultiple { get; set; }
        public bool Inherited { get; set; }
        public AttributeTargets ValidOn { get; } = validOn;
    }
}
