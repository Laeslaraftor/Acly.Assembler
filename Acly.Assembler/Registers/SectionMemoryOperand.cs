namespace Acly.Assembler.Registers
{
    internal class SectionMemoryOperand : MemoryOperand
    {
        public SectionMemoryOperand(Address section, MemoryOperand offsetOperand)
        {
            Section = section;
            Offset = offsetOperand;

            string offsetWithoutBrackets = offsetOperand.Value.Replace("[", string.Empty).Replace("[", string.Empty);
            Value = $"[{section}:{offsetWithoutBrackets}]";
        }

        public Address Section { get; }
        public MemoryOperand Offset { get; }
        public override string Value { get; }
    }
}
