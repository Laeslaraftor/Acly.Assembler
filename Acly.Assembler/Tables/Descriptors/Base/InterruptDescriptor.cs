using Acly.Assembler.Interruptions;

namespace Acly.Assembler.Tables
{
    /// <summary>
    /// Базовый класс дескриптора прерывания
    /// </summary>
    public abstract class InterruptDescriptor : Descriptor
    {
        /// <summary>
        /// Создать новый экземпляр дескриптора прерывания
        /// </summary>
        /// <param name="name">Название дескриптора</param>
        protected InterruptDescriptor(string name) : base(name)
        {
        }

        /// <summary>
        /// Прерывание дескриптора
        /// </summary>
        public virtual Interruption Interruption { get; set; } = 0;
        /// <summary>
        /// Тип шлюза
        /// </summary>
        public abstract GateType Type { get; }
        /// <summary>
        /// Размер шлюза
        /// </summary>
        public virtual GateSize Size { get; set; }

        #region Управление

        /// <summary>
        /// Получить байт типа
        /// </summary>
        /// <returns>Байт типа</returns>
        protected byte GetTypeByte()
        {
            byte typeByte = (byte)Type;

            if (IsPresent)
            {
                typeByte |= 0x80;
            }

            typeByte |= (byte)PrivilegeLevel;

            return typeByte;
        }

        #endregion

        #region Ассемблер

        /// <summary>
        /// Сгенерировать основной код дескриптора
        /// </summary>
        /// <param name="selector">Селектор дескриптора</param>
        /// <param name="offset">Смещение дескриптора</param>
        /// <returns>Основной код дескриптора</returns>
        protected string GenerateCommonAssembler(ushort selector, uint offset)
        {
            return $"; {Name} (INT {Interruption})\n" +
                   $"dw 0x{(offset & 0xFFFF):X4}      ; Offset 0:15\n" +
                   $"dw 0x{selector:X4}      ; Selector\n" +
                   $"db 0x00        ; Reserved\n" +
                   $"db 0x{GetTypeByte():X2}        ; P={(IsPresent ? 1 : 0)}, DPL={(byte)PrivilegeLevel >> 5}, Type=0x{(byte)Type:X1}\n" +
                   $"dw 0x{(offset >> 16):X4}      ; Offset 16:31";
        }

        #endregion
    }
}
