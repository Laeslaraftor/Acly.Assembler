using System;

namespace Acly.Assembler.Tables
{
    /// <summary>
    /// Класс, представляющий байт доступа
    /// </summary>
    public class DataCodeAccessByte : AccessByte
    {
        /// <summary>
        /// Тип дескриптора.
        /// </summary>
        public override DescriptorType DescriptorType { get; } = DescriptorType.DataCode;
        /// <summary>
        /// Исполняемый ли сегмент. Если true - то сегмент должен содержать код.
        /// </summary>
        public bool IsExecutable { get; set; }
        /// <summary>
        /// Для кода:
        /// Если true то код можно вызывать из сегментов с меньшими привилегиями. 
        /// false - код доступен только из сегментов с такими же привилегиями.
        /// Для данных:
        /// true - сегмент растёт вниз (используется для стека).
        /// false - сегмент растёт вверх (обычное поведение).
        /// </summary>
        public bool Conforming { get; set; }
        /// <summary>
        /// Для кода:
        /// true - код можно читать.
        /// false - код только для исполнения, чтение вызовет исключение.
        /// Для данных:
        /// true - данные можно записывать и читать.
        /// false - данные доступны только для чтения.
        /// </summary>
        public bool IsReadable { get; set; } = true;
        /// <summary>
        /// CPU устанавливает этот бит в 1 (true) при первом обращении к сегменту. 
        /// </summary>
        /// <remarks>Лучше не трогать это поле</remarks>
        public bool IsAccessed { get; set; }

        #region Управление

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public override string ToString()
        {
            return $"{base.ToString()}, Executable={ToInt(IsExecutable)}, Conforming={ToInt(Conforming)}, Readable={ToInt(IsReadable)}, Accessed={ToInt(IsAccessed)}";
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        protected override byte ToByte()
        {
            string binaryValue = $"{ToInt(IsPresent)}{DPL}{(int)DescriptorType}{ToInt(IsExecutable)}{ToInt(Conforming)}{ToInt(IsReadable)}{ToInt(IsAccessed)}";
            return Convert.ToByte(binaryValue, 2);
        }

        #endregion
    }
}
