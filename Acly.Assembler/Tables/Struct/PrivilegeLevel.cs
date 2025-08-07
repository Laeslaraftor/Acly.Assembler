using System;

namespace Acly.Assembler
{
    /// <summary>
    /// Уровень привилегий
    /// </summary>
    public readonly struct PrivilegeLevel
    {
        /// <summary>
        /// Создать уровень привилегий
        /// </summary>
        /// <param name="byteValue">Байт уровня привилегий</param>
        /// <param name="name">Название уровня привилегий</param>
        public PrivilegeLevel(byte byteValue, string name)
        {
            Name = name;
            Value = byteValue;
        }

        /// <summary>
        /// Название уровня
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Байтовое значение
        /// </summary>
        public byte Value { get; }

        #region Управление

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public override string ToString()
        {
            if (Value == 0)
            {
                return "00";
            }

            return Convert.ToString(Value, 2);
        }

        #endregion

        #region Операторы

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="level"><inheritdoc/></param>
        public static explicit operator byte(PrivilegeLevel level)
        {
            return level.Value;
        }

        #endregion

        #region Статика

        /// <summary>
        /// Уровень ядра ОС. Максимальные привилегии.
        /// </summary>
        public static PrivilegeLevel Ring0 { get; } = new(0b00, nameof(Ring0));
        /// <summary>
        /// Уровень драйверов.
        /// </summary>
        public static PrivilegeLevel Ring1 { get; } = new(0b01, nameof(Ring1));
        /// <summary>
        /// Уровень гипервизора.
        /// </summary>
        public static PrivilegeLevel Ring2 { get; } = new(0b10, nameof(Ring2));
        /// <summary>
        /// Уровень пользовательских программ.
        /// </summary>
        public static PrivilegeLevel Ring3 { get; } = new(0b11, nameof(Ring3));

        #endregion
    }
}
