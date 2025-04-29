namespace Acly.Assembler.Tables
{
    /// <summary>
    /// Базовый класс дескриптора обработчика прерывания
    /// </summary>
    public abstract class HandlerInterruptDescriptor : InterruptDescriptor
    {
        /// <summary>
        /// Создать новый экземпляр дескриптора обработчика прерывания
        /// </summary>
        /// <param name="name">Название дескриптора</param>
        protected HandlerInterruptDescriptor(string name) : base(name)
        {
        }

        /// <summary>
        /// Указатель на сегмент кода
        /// </summary>
        public virtual ushort CodeSegmentSelector { get; set; }
        /// <summary>
        /// Смещение обработчика. 
        /// Это поле указывает адрес обработчика (функции\метода) относительно указателя на сегмент кода
        /// </summary>
        public virtual uint HandlerOffset { get; set; }

        #region Ассемблер

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public override string ToAssembler()
        {
            return $"; {Name} (INT {Interruption})\n" +
                   $"dw 0x{(HandlerOffset & 0xFFFF):X4}      ; Offset 0:15\n" +
                   $"dw 0x{CodeSegmentSelector:X4}      ; Selector\n" +
                   $"db 0x00        ; Reserved\n" +
                   $"db 0x{GetTypeByte():X2}        ; P={(IsPresent ? 1 : 0)}, DPL={(byte)PrivilegeLevel >> 5}, Type=0x{(byte)Type:X1}\n" +
                   $"dw 0x{(HandlerOffset >> 16):X4}      ; Offset 16:31";
        }

        #endregion
    }
}
