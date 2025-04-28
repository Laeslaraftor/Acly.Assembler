namespace Acly.Assembler.Registers
{
    internal class AddressMemoryOperand : MemoryOperand
    {
        public AddressMemoryOperand(Address register, bool asPointer)
            : this(register, null, 0, 1, asPointer)
        {
        }
        public AddressMemoryOperand(Address register, Address? index, int displacement, int scale, bool asPointer)
        {
            Register = register;
            Index = index;
            Displacement = displacement;
            Scale = scale;
            AsPointer = asPointer;

            Value = register;

            if (index != null)
            {
                Value += $" + {index}";
            }
            if (scale != 1)
            {
                Value += $" + {scale}";
            }
            if (displacement != 0)
            {
                Value += $" + {displacement}";
            }

            if (asPointer)
            {
                Value = $"[{Value}]";
            }
        }

        public Address Register { get; }
        public Address? Index { get; }
        public int Displacement { get; }
        public int Scale { get; }
        public bool AsPointer { get; }
        public override string Value { get; }
    }
}
