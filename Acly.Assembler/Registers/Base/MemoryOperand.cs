namespace Acly.Assembler.Registers
{
    /// <summary>
    /// Базовый класс операции с регистрами и адресами
    /// </summary>
    public abstract class MemoryOperand
    {
        /// <summary>
        /// Строковое значение операции. Например 0x1000, RAX, [RAX + 2] 
        /// </summary>
        public abstract string Value { get; }

        #region Операторы

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="value"><inheritdoc/></param>
        public static implicit operator MemoryOperand(char value)
        {
            return Create(value);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="value"><inheritdoc/></param>
        public static implicit operator MemoryOperand(ulong value)
        {
            return Create(value);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="register"><inheritdoc/></param>
        public static implicit operator MemoryOperand(Register register)
        {
            return Create(register);
        }

        #endregion

        #region Дополниельно

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public override string ToString()
        {
            return Value;
        }

        #endregion

        #region Статика

        /// <summary>
        /// Создать операцию с символом
        /// </summary>
        /// <param name="symbol">Символ операции</param>
        /// <returns>Операция с символом</returns>
        public static MemoryOperand Create(char symbol)
        {
            return new CharMemoryOperand(symbol);
        }
        /// <summary>
        /// Создать операцию с регистром
        /// </summary>
        /// <param name="register">Регистр операции</param>
        /// <param name="asPointer">Если true, то будет считаться что регистр содержит адрес на значение 
        /// и он будет обособлен квадратными скобками</param>
        /// <returns>Операция с регистрами и адресами</returns>
        public static MemoryOperand Create(Register register, bool asPointer = false)
        {
            return new AddressMemoryOperand(register, asPointer);
        }
        /// <summary>
        /// Создать операцию со значением
        /// </summary>
        /// <param name="value">Значение операции</param>
        /// <param name="asPointer">Если true, то будет считаться что значение содержит адрес 
        /// и он будет обособлен квадратными скобками</param>
        /// <returns>Операция с регистрами и адресами</returns>
        public static MemoryOperand Create(ulong value, bool asPointer = false)
        {
            return new AddressMemoryOperand(value, asPointer);
        }
        /// <summary>
        /// Создать операцию с регистром
        /// </summary>
        /// <param name="register">Регистр операции</param>
        /// <param name="displacement">Константное смещение</param>
        /// <param name="scale">Масштаб</param>
        /// <returns>Операция с регистрами и адресами</returns>
        public static MemoryOperand Create(Register register, int displacement, int scale = 1)
        {
            return new AddressMemoryOperand(register, null, displacement, scale, true);
        }
        /// <summary>
        /// Создать операцию со значением
        /// </summary>
        /// <param name="value">Значение операции</param>
        /// <param name="displacement">Константное смещение</param>
        /// <param name="scale">Масштаб</param>
        /// <returns>Операция с регистрами и адресами</returns>
        public static MemoryOperand Create(ulong value, int displacement, int scale = 1)
        {
            return new AddressMemoryOperand(value, null, displacement, scale, true);
        }
        /// <summary>
        /// Создать операцию с регистром
        /// </summary>
        /// <param name="register">Регистр операции</param>
        /// <param name="index">Смещение</param>
        /// <param name="displacement">Константное смещение</param>
        /// <param name="scale">Масштаб</param>
        /// <returns>Операция с регистрами и адресами</returns>
        public static MemoryOperand Create(Register register, Register index, int displacement = 0, int scale = 1)
        {
            return new AddressMemoryOperand(register, index, displacement, scale, true);
        }
        /// <summary>
        /// Создать операцию с регистром
        /// </summary>
        /// <param name="register">Регистр операции</param>
        /// <param name="index">Смещение</param>
        /// <param name="displacement">Константное смещение</param>
        /// <param name="scale">Масштаб</param>
        /// <returns>Операция с регистрами и адресами</returns>
        public static MemoryOperand Create(Register register, ulong index, int displacement = 0, int scale = 1)
        {
            return new AddressMemoryOperand(register, index, displacement, scale, true);
        }
        /// <summary>
        /// segment:offset. Если что то является указателем, то оно будет в скобках. Например, смещение как указатель: segment[offset]
        /// </summary>
        /// <param name="segment">Базовый адрес сегмента</param>
        /// <param name="offsetOperand">Смещение внутри сегмента</param>
        /// <returns>Операция с регистрами и адресами</returns>
        public static MemoryOperand Create(Register segment, MemoryOperand offsetOperand)
        {
            return new SectionMemoryOperand(segment, offsetOperand);
        }
        /// <summary>
        /// segment:offset. Если что то является указателем, то оно будет в скобках. Например, смещение как указатель: segment[offset]
        /// </summary>
        /// <param name="segment">Базовый адрес сегмента</param>
        /// <param name="offsetOperand">Смещение внутри сегмента</param>
        /// <returns>Операция с регистрами и адресами</returns>
        public static MemoryOperand Create(ulong segment, MemoryOperand offsetOperand)
        {
            return new SectionMemoryOperand(segment, offsetOperand);
        }

        #endregion
    }
}
