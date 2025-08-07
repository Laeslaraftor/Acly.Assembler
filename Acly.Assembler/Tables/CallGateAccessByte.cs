using System;

namespace Acly.Assembler.Tables
{
    /// <summary>
    /// Класс, представляющий байт доступа
    /// </summary>
    public class CallGateAccessByte : AccessByte
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override DescriptorType DescriptorType { get; } = DescriptorType.System;
        /// <summary>
        /// Тип системного дескриптора
        /// </summary>
        public byte Type { get; } = 0xC;

        #region Управление

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public override string ToString()
        {
            return $"{base.ToString()}, Type={Convert.ToString(Type, 2)}";
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        protected override byte ToByte()
        {
            string binaryType = Convert.ToString(Type, 2);
            string binaryValue = $"{ToInt(IsPresent)}{DPL}{(int)DescriptorType}{binaryType}";
            return Convert.ToByte(binaryValue, 2);

        }

        #endregion
    }
}
