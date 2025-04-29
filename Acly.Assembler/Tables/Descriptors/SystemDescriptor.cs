namespace Acly.Assembler.Tables
{
    /// <summary>
    /// Системный дескриптор
    /// </summary>
    public class SystemDescriptor : StandardDescriptor
    {
        internal SystemDescriptor(string name) : base(name)
        {
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Segment Segment { get; } = Segment.System;
        /// <summary>
        /// Тип системного дескриптора
        /// </summary>
        public SystemDescriptorType Type { get; set; }

        #region Ассемблер

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public override string ToAssembler()
        {
            return $"; {Name} (System: {Type})\n" +
                   GenerateBaseLimitAssembler() + "\n" +
                   $"db 0x{GetAccessByte((byte)Type):X2}        ; P={(IsPresent ? 1 : 0)}, " +
                   $"DPL={(byte)PrivilegeLevel >> 5}, S=0, Type=0x{(byte)Type:X1}\n" +
                   $"db 0x{GetFlagsByte(Flags):X2}        ; Flags: {GetFlagsDescription()}\n" +
                   $"db 0x{(BaseAddress >> 24):X2}        ; Base 24:31";
        }

        #endregion
    }
}
