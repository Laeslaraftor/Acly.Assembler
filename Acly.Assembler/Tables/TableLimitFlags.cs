using System;
using static Acly.Assembler.Tables.TablesExtensions;

namespace Acly.Assembler.Tables
{
    /// <summary>
    /// Граница сегмента с флагами
    /// </summary>
    public class TableLimitFlags
    {
        /// <summary>
        /// Единица измерения <see cref="Limit"/>
        /// </summary>
        public Granularity Granularity { get; set; }
        /// <summary>
        /// Для кода:
        /// <see cref="OperationSize.x16"/> - 16 битные инструкции.
        /// <see cref="OperationSize.x32_64"/> - 32 битные/64 битные инструкции.
        /// Для данных:
        /// <see cref="OperationSize.x16"/> - 16 битный стек.
        /// <see cref="OperationSize.x32_64"/> - 32 битный стек.
        /// </summary>
        public OperationSize DefaultOperationSize { get; set; }
        /// <summary>
        /// true - сегмент работает в 64 битном режиме (только для кода).
        /// false - 32 битный или 16 битный режим
        /// </summary>
        public bool IsLongMode { get; set; }
        /// <summary>
        /// Свободный бит для использования. Не влияет на процессор.
        /// </summary>
        public bool IsAvailable { get; set; }
        /// <summary>
        /// Граница сегмента до 20 бит. Максимальное значение 0xF (15).
        /// </summary>
        public byte Limit { get; set; } = 0xF;

        #region Управление

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public override string ToString()
        {
            return $"G={(int)Granularity}, D/B={(int)DefaultOperationSize}, L={ToInt(IsLongMode)}, AVL={ToInt(IsAvailable)}, Limit=0x{Limit:X}";
        }

        #endregion

        #region Операторы

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="flagsLimit"><inheritdoc/></param>
        public static implicit operator byte(TableLimitFlags flagsLimit)
        {
            string binaryValue = $"{(int)flagsLimit.Granularity}{(int)flagsLimit.DefaultOperationSize}{ToInt(flagsLimit.IsLongMode)}{ToInt(flagsLimit.IsAvailable)}";
            binaryValue += Convert.ToString(flagsLimit.Limit, 2);

            return Convert.ToByte(binaryValue, 2);
        }

        #endregion
    }
}
