using Acly.Assembler.Contexts;
using Acly.Assembler.Registers;

namespace Acly.Assembler.Interruptions
{
    /// <summary>
    /// Прерывание BIOS для работы с системными функциями
    /// </summary>
    public class BiosSystemFunctionsInterruption : BiosInterruption
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public BiosSystemFunctionsInterruption() : base(0x15, "Прерывание для работы с системными функциями")
        {
        }

        #region Управление

        /// <summary>
        /// Задержать на указанное время
        /// </summary>
        /// <param name="microseconds">Время задержки в микросекундах</param>
        public void Delay(MemoryOperand microseconds)
        {
            RealMode.Count.Set(0);
            RealMode.Count.Set(microseconds);
            PerformInterruption(DelayFunction);
        }
        /// <summary>
        /// Получить размер расширенной памяти (памяти выше 1мб).
        /// </summary>
        /// <remarks>	
        /// AX = размер расширенной памяти в килобайтах
        /// </remarks>
        public void GetExtendedMemorySize()
        {
            PerformInterruption(ExtendedMemorySizeGetterFunction);
        }
        /// <summary>
        /// Получить карту памяти системы, включая доступные и зарезервированные области
        /// </summary>
        /// <param name="start">Начало</param>
        /// <param name="bufferSize">Размер буфера для каждой записи</param>
        /// <param name="bufferAddress">Адрес буфера в который будут записаны данные</param>
        /// <remarks>
        /// CF = 0 (успех), CF = 1 (ошибка), EBX = следующий блок, ECX = размер данных
        /// </remarks>
        public void GetMemoryCard(MemoryOperand start, MemoryOperand bufferSize, MemoryOperand bufferAddress)
        {
            ProtectedMode.Accumulator.Set(MemoryCardGetterFunction);
            ProtectedMode.Base.Set(start);
            ProtectedMode.Count.Set(bufferSize);
            ProtectedMode.Data.Set(0x534D4150);
            ProtectedMode.DestinationIndex.Set(bufferAddress);
            Asm.Interrupt(this);
        }
        /// <summary>
        /// Получить карту памяти системы, включая доступные и зарезервированные области
        /// </summary>
        /// <param name="bufferAddress">Адрес буфера в который будут записаны данные</param>
        /// <remarks>
        /// CF = 0 (успех), CF = 1 (ошибка), EBX = следующий блок, ECX = размер данных
        /// </remarks>
        public void GetMemoryCard(MemoryOperand bufferAddress)
        {
            GetMemoryCard(0, 24, bufferAddress);
        }
        /// <summary>
        /// Получить информацию о системе
        /// </summary>
        /// <remarks>
        /// AX = модель процессора, BX:CX = маска функций, DX = оборудование
        /// </remarks>
        public void GetSystemInfo()
        {
            PerformInterruption(SystemInformationGetterFunction);
        }

        #endregion

        #region Константы

        /// <summary>
        /// Номер функции задержки на указанное время
        /// </summary>
        public const byte DelayFunction = 0x86;
        /// <summary>
        /// Номер функции переключения между блоками памяти
        /// </summary>
        public const byte MemoryBlockSwitchFunction = 0x87;
        /// <summary>
        /// Номер функции получения размера расширенной памяти
        /// </summary>
        public const byte ExtendedMemorySizeGetterFunction = 0x88;
        /// <summary>
        /// Номер функции получения карты памяти системы
        /// </summary>
        public const int MemoryCardGetterFunction = 0xE820;
        /// <summary>
        /// Номер функции получения информации о системе
        /// </summary>
        public const byte SystemInformationGetterFunction = 0xC0;

        #endregion

        #region Статика

        private static ProtectedModeContext ProtectedMode => ProtectedModeContext.Instance;

        #endregion
    }
}
