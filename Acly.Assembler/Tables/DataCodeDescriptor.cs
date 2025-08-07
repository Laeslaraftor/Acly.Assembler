using System;
using System.Text;

namespace Acly.Assembler.Tables
{
    /// <summary>
    /// Сегмент глобальной таблицы дескрипторов
    /// </summary>
    public class DataCodeDescriptor : Descriptor
    {
        /// <summary>
        /// Младшие 16 бит границы сегмента.
        /// </summary>
        public ushort LimitLower { get; set; }
        /// <summary>
        /// Младшие 16 бит базового адреса.
        /// </summary>
        public ushort BaseLower { get; set; }
        /// <summary>
        /// Следующие 8 бит базового адреса.
        /// </summary>
        public byte Base { get; set; }
        /// <summary>
        /// Настройки дескриптора.
        /// </summary>
        public DataCodeAccessByte AccessByte { get; } = new();
        /// <summary>
        /// 4 бита флагов + старшие 4 бита Limit.
        /// </summary>
        public TableLimitFlags LimitHighFlags { get; } = new();
        /// <summary>
        /// Старшие 8 бит базового адреса.
        /// </summary>
        public byte BaseHigh { get; set; }

        #region Управление

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="builder"><inheritdoc/></param>
        protected override void GenerateCode(StringBuilder builder)
        {
            builder.AppendLine($"{Asm.Tab}dw 0x{LimitLower:X}");
            builder.AppendLine($"{Asm.Tab}dw 0x{BaseLower:X}");
            builder.AppendLine($"{Asm.Tab}db 0x{Base:X}");
            builder.AppendLine($"{Asm.Tab}db {Convert.ToString(AccessByte, 2)}b{Asm.Tab}; {AccessByte}");
            builder.AppendLine($"{Asm.Tab}db {Convert.ToString(LimitHighFlags, 2)}b{Asm.Tab}; {LimitHighFlags}");
            builder.AppendLine($"{Asm.Tab}db 0x{BaseHigh:X}");

        }

        #endregion
    }
}
