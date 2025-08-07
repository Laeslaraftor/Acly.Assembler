using System.Collections.Generic;

namespace Acly.Assembler.Tables
{
    /// <summary>
    /// Базовый класс стандартного дескриптора
    /// </summary>
    public abstract class StandardDescriptor : Descriptor2
    {
        /// <summary>
        /// Создать новый экземпляр стандартного дескриптора
        /// </summary>
        /// <param name="name">Название дескриптора</param>
        protected StandardDescriptor(string name) : base(name)
        {
        }

        /// <summary>
        /// Базовый адрес дескриптора. 
        /// Это должен быть адрес данных, которые этот дескриптор описывает.
        /// </summary>
        public virtual uint BaseAddress { get; set; }
        /// <summary>
        /// Ограничение размера дескриптора
        /// </summary>
        public virtual uint Limit { get; set; }
        /// <summary>
        /// Сегмент, который описывает дескриптор
        /// </summary>
        public abstract Segment Segment { get; }
        /// <summary>
        /// Размер дескриптора
        /// </summary>
        public virtual DescriptorSize Size { get; set; }
        /// <summary>
        /// Флаги сегмента
        /// </summary>
        public virtual SegmentFlags Flags { get; set; }

        #region Управление

        /// <summary>
        /// Получить байт уровня привилегий
        /// </summary>
        /// <param name="typeBits">Байт типа</param>
        /// <returns>Байт уровня привилегий</returns>
        protected byte GetAccessByte(byte typeBits)
        {
            byte access = typeBits;
            if (IsPresent) access |= 0x80;
            access |= (byte)PrivilegeLevel;
            access |= (byte)Segment;
            return access;
        }
        /// <summary>
        /// Получить байт флагов сегмента
        /// </summary>
        /// <param name="flags">Флаги сегментов</param>
        /// <returns>Байт флагов сегмента</returns>
        protected byte GetFlagsByte(SegmentFlags flags)
        {
            byte result = 0;

            if (flags.HasFlag(SegmentFlags.Granularity)) result |= 0x80;
            if (flags.HasFlag(SegmentFlags.DefaultBig)) result |= 0x40;
            if (flags.HasFlag(SegmentFlags.LongMode)) result |= 0x20;

            // Оставляем 4 бита для лимита (0-3)
            result |= (byte)((Limit >> 16) & 0x0F);

            return result;
        }

        #endregion

        #region Ассемблер

        /// <summary>
        /// Получить ассемблерный код базового адреса и ограничения памяти
        /// </summary>
        /// <returns></returns>
        protected string GenerateBaseLimitAssembler()
        {
            return $"dw 0x{(Limit & 0xFFFF):X4}      ; Limit 0:15\n" +
                   $"dw 0x{(BaseAddress & 0xFFFF):X4}      ; Base 0:15\n" +
                   $"db 0x{((BaseAddress >> 16) & 0xFF):X2}        ; Base 16:23";
        }
        /// <summary>
        /// Получить описание флагов
        /// </summary>
        /// <returns></returns>
        protected virtual string GetFlagsDescription()
        {
            List<string> flags = new();

            if (Flags.HasFlag(SegmentFlags.Granularity))
            {
                flags.Add("G=1 (4K)");
            }
            if (Flags.HasFlag(SegmentFlags.DefaultBig))
            {
                flags.Add("D/B=1 (32-bit)");
            }
            if (Flags.HasFlag(SegmentFlags.LongMode))
            {
                flags.Add("L=1 (64-bit)");
            }
            if (Flags.HasFlag(SegmentFlags.ExpandDown))
            {
                flags.Add("E=1 (Expand Down)");
            }

            return string.Join(", ", flags);
        }

        #endregion
    }
}
