using System;

namespace Acly.Assembler.Registers
{
    /// <summary>
    /// Адрес
    /// </summary>
    public readonly struct Address
    {
        /// <summary>
        /// Создать адрес из регистра
        /// </summary>
        /// <param name="register"></param>
        public Address(ValueContainer register)
        {
            Value = register.Name;
        }
        /// <summary>
        /// Создать регистр из строкового значения
        /// </summary>
        /// <param name="label"></param>
        public Address(string label)
        {
            Value = label;
        }
        /// <summary>
        /// Создать адрес из значения
        /// </summary>
        /// <param name="value"></param>
        public Address(ulong value)
        {
            Value = $"0x{value:X}";
        }
        /// <summary>
        /// Создать адрес из значения
        /// </summary>
        /// <param name="value"></param>
        public Address(int value)
        {
            Value = $"0x{value:X}";
        }
        /// <summary>
        /// Создать адрес из значения
        /// </summary>
        /// <param name="value"></param>
        public Address(byte value)
        {
            Value = $"0x{value:X}";
        }
        /// <summary>
        /// Создать адрес из значения
        /// </summary>
        /// <param name="value"></param>
        public Address(short value)
        {
            Value = $"0x{value:X}";
        }
        /// <summary>
        /// Адрес в виде строки
        /// </summary>
        public string Value { get; }

        #region Операторы

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="register"><inheritdoc/></param>
        public static implicit operator Address(ValueContainer register)
        {
            return new(register);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="value"><inheritdoc/></param>
        public static implicit operator Address(ulong value)
        {
            return new(value);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="value"><inheritdoc/></param>
        public static implicit operator Address(int value)
        {
            return new(value);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="value"><inheritdoc/></param>
        public static implicit operator Address(byte value)
        {
            return new(value);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="value"><inheritdoc/></param>
        public static implicit operator Address(short value)
        {
            return new(value);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="label"><inheritdoc/></param>
        public static implicit operator Address(string label)
        {
            return new(label);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="address"><inheritdoc/></param>
        public static implicit operator string(Address address)
        {
            return address.Value;
        }

        #endregion

        #region Дополнительно

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="obj"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public readonly override bool Equals(object? obj)
        {
            return obj is Address address &&
                   Value == address.Value;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public readonly override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public readonly override string ToString()
        {
            return Value;
        }

        #endregion
    }
}
