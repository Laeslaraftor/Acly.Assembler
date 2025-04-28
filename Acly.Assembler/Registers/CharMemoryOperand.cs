namespace Acly.Assembler.Registers
{
    internal class CharMemoryOperand : MemoryOperand
    {
        public CharMemoryOperand(char value)
        {
            Value = $"'{value}'";
        }

        public override string Value { get; }
    }
}
