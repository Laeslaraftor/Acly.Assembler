using System;

namespace Acly.Assembler.Tables
{
    /// <summary>
    /// Класс, представляющий байт доступа
    /// </summary>
    public class InterruptionAccessByte : AccessByte
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override DescriptorType DescriptorType { get; } = DescriptorType.System;
        /// <summary>
        /// Тип обрабатываемого прерывания
        /// </summary>
        public InterruptionType Type { get; set; }

        #region Управление

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public override string ToString()
        {
            return $"{base.ToString()}, Type={Convert.ToString((byte)Type, 2)} ({Type})";
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        protected override byte ToByte()
        {
            string binaryType = Convert.ToString((byte)Type, 2);
            string binaryValue = $"{ToInt(IsPresent)}{DPL}{(int)DescriptorType}{binaryType}";
            return Convert.ToByte(binaryValue, 2);
        }

        #endregion
    }
}
