using System;

namespace Acly.Assembler.Registers
{
    internal readonly struct Address
    {
        public Address(Register register)
        {
            Value = register.Name;
        }
        public Address(string label)
        {
            Value = label;
        }
        public Address(ulong value)
        {
            Value = $"0x{value:X}";
        }

        public string Value { get; }

        #region Операторы

        public static implicit operator Address(Register register)
        {
            return new(register);
        }
        public static implicit operator Address(ulong value)
        {
            return new(value);
        }
        public static implicit operator Address(string label)
        {
            return new(label);
        }
        public static implicit operator string(Address address)
        {
            return address.Value;
        }

        #endregion

        #region Дополнительно

        public readonly override bool Equals(object? obj)
        {
            return obj is Address address &&
                   Value == address.Value;
        }
        public readonly override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        public readonly override string ToString()
        {
            return Value;
        }

        #endregion
    }
}
