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
        /// <param name="value"><inheritdoc/></param>
        public static implicit operator MemoryOperand(int value)
        {
            return Create(value);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="value"><inheritdoc/></param>
        public static implicit operator MemoryOperand(byte value)
        {
            return Create(value);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="value"><inheritdoc/></param>
        public static implicit operator MemoryOperand(short value)
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
        /// Создать операцию
        /// </summary>
        /// <param name="address">Адрес, регистр или строковое значение</param>
        /// <param name="asPointer">Если true, то будет считаться что регистр содержит адрес на значение 
        /// и он будет обособлен квадратными скобками</param>
        /// <returns>Операция с регистрами и адресами</returns>
        public static MemoryOperand Create(Address address, bool asPointer = false)
        {
            return new AddressMemoryOperand(address, asPointer);
        }
        /// <summary>
        /// Создать операцию с регистром
        /// </summary>
        /// <param name="address">Адрес, регистр или строковое значение</param>
        /// <param name="displacement">Константное смещение</param>
        /// <param name="scale">Масштаб</param>
        /// <returns>Операция с регистрами и адресами</returns>
        public static MemoryOperand Create(Address address, int displacement, int scale = 1)
        {
            return new AddressMemoryOperand(address, null, displacement, scale, true);
        }
        /// <summary>
        /// Создать операцию с регистром
        /// </summary>
        /// <param name="address">Адрес, регистр или строковое значение</param>
        /// <param name="index">Смещение</param>
        /// <param name="displacement">Константное смещение</param>
        /// <param name="scale">Масштаб</param>
        /// <returns>Операция с регистрами и адресами</returns>
        public static MemoryOperand Create(Address address, Address index, int displacement = 0, int scale = 1)
        {
            return new AddressMemoryOperand(address, index, displacement, scale, true);
        }
        /// <summary>
        /// Создать операцию с регистром
        /// </summary>
        /// <param name="segment">Базовый адрес сегмента или регистр</param>
        /// <param name="address">Адрес, регистр или строковое значение</param>
        /// <param name="index">Смещение</param>
        /// <param name="displacement">Константное смещение</param>
        /// <param name="scale">Масштаб</param>
        /// <returns>Операция с регистрами и адресами</returns>
        public static MemoryOperand Create(Address segment, Address address, Address? index = null, int displacement = 0, int scale = 1)
        {
            return new AddressMemoryOperand(segment, address, index, displacement, scale, true);
        }

        #endregion
    }
}
