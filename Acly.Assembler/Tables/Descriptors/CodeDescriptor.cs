using System;

namespace Acly.Assembler.Tables
{
    /// <summary>
    /// Дескриптор сегмента кода
    /// </summary>
    public class CodeDescriptor : StandardDescriptor
    {
        internal CodeDescriptor(string name) : base(name)
        {
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override Segment Segment { get; } = Segment.CodeData;
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override SegmentFlags Flags
        {
            get => base.Flags;
            set
            {
                if (base.Flags != value)
                {
                    if (!value.HasFlag(SegmentFlags.Executable))
                    {
                        throw new ArgumentException($"Сегмент кода должен быть исполняемым! Иначе надо использовать {nameof(DataDescriptor)}", 
                            nameof(value));
                    }

                    base.Flags = value;
                }
            }
        }

        #region Ассемблер

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public override string ToAssembler()
        {
            byte type = 0x08; // Execute-only базовый тип

            if (Flags.HasFlag(SegmentFlags.Readable))
            {
                type |= 0x02;
            }
            if (Flags.HasFlag(SegmentFlags.Conforming))
            {
                type |= 0x04;
            }

            return $"; {Name} (Code Segment)\n" +
                   GenerateBaseLimitAssembler() + "\n" +
                   $"db 0x{GetAccessByte(type):X2}        ; P={(IsPresent ? 1 : 0)}, " +
                   $"DPL={(byte)PrivilegeLevel >> 5}, S=1, Type=0x{type:X1}\n" +
                   $"db 0x{GetFlagsByte(Flags):X2}        ; Flags: {GetFlagsDescription()}\n" +
                   $"db 0x{(BaseAddress >> 24):X2}        ; Base 24:31";
        }

        #endregion
    }
}
