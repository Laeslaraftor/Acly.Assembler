namespace Acly.Assembler.Tables
{
    /// <summary>
    /// Дескриптор шлюза задачи
    /// </summary>
    public class TaskGateDescriptor : InterruptDescriptor
    {
        internal TaskGateDescriptor(string name) : base(name)
        {
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override GateType Type { get; } = GateType.TaskGate32;
        /// <summary>
        /// Селектор состояния задачи (TSS)
        /// </summary>
        public ushort TaskStateSectionSelector { get; }

        #region Ассемблер

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public override string ToAssembler()
        {
            return $"; {Name} (INT {Interruption})\n" +
                   $"dw 0x0000      ; Reserved\n" +
                   $"dw 0x{TaskStateSectionSelector:X4}      ; TSS Selector\n" +
                   $"db 0x00        ; Reserved\n" +
                   $"db 0x{GetTypeByte():X2}        ; P={(IsPresent ? 1 : 0)}, DPL={(byte)PrivilegeLevel >> 5}, Type=0x{(byte)Type:X1}\n" +
                   $"dw 0x0000      ; Reserved";
        }

        #endregion
    }
}
