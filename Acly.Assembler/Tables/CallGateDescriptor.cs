using Acly.Assembler.Registers;
using System;
using System.Text;

namespace Acly.Assembler.Tables
{
    /// <summary>
    /// Дескриптор шлюза вызова
    /// </summary>
    public class CallGateDescriptor : SystemDescriptor
    {
        /// <summary>
        /// Младшие биты точки входа.
        /// </summary>
        public short OffsetLower { get; set; }
        /// <summary>
        /// Число параметров, копируемых в стек ядра.
        /// </summary>
        public byte ParamCount { get; set; }
        /// <summary>
        /// Настройки дескриптора.
        /// </summary>
        public CallGateAccessByte AccessByte { get; } = new();
        /// <summary>
        /// Старшие биты точки входа.
        /// </summary>
        public short OffsetHigh { get; set; }

        #region Управление

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="builder"><inheritdoc/></param>
        protected override void GenerateCode(StringBuilder builder)
        {
            builder.AppendLine($"{Asm.Tab}dw 0x{OffsetLower:X}");
            builder.AppendLine($"{Asm.Tab}dw {SegmentSelector.Value}");
            builder.AppendLine($"{Asm.Tab}db 0x{ParamCount:X}");
            builder.AppendLine($"{Asm.Tab}db {Convert.ToString(AccessByte, 2)}b{Asm.Tab}; {AccessByte}");
            builder.AppendLine($"{Asm.Tab}dw 0x{OffsetHigh:X}");
        }

        #endregion
    }
}
