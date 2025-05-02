using Acly.Assembler.Registers;

namespace Acly.Assembler.Interruptions
{
    /// <summary>
    /// Прерывание BIOS для работы с COM портами
    /// </summary>
    public class BiosComInterruption : BiosInterruption
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public BiosComInterruption() : base(0x14, "Прерывание для работы с COM портами")
        {
        }

        #region Управление

        /// <summary>
        /// Инициализировать COM порт
        /// </summary>
        /// <param name="speed">Скорость передачи данных в бодах</param>
        /// <param name="portNumber">Номер COM порта, который надо инициализировать</param>
        /// <remarks>
        /// AH = статус, AL = конфигурация
        /// </remarks>
        public void InitializePort(MemoryOperand speed, MemoryOperand portNumber)
        {
            RealMode.Accumulator.Lower.Set(speed);
            PerformInterruption(ComInitializeFunction, portNumber);
        }
        /// <summary>
        /// Отправить символ через COM порт
        /// </summary>
        /// <param name="symbol">Символ для отправки</param>
        /// <param name="portNumber">Номер COM порта через который надо отправить символ</param>
        /// <remarks>
        /// AH = статус
        /// </remarks>
        public void SendChar(MemoryOperand symbol, MemoryOperand portNumber)
        {
            RealMode.Accumulator.Lower.Set(symbol);
            PerformInterruption(CharSendFunction, portNumber);
        }
        /// <summary>
        /// Прочитать символ из COM порта
        /// </summary>
        /// <param name="portNumber">Номер COM порта из которого надо прочитать символ</param>
        /// <remarks>
        /// AH = статус, AL = полученный символ
        /// </remarks>
        public void ReadChar(MemoryOperand portNumber)
        {
            PerformInterruption(CharReadFunction, portNumber);
        }
        /// <summary>
        /// Получить статус COM порта
        /// </summary>
        /// <param name="portNumber">Номер COM порта, статус которого надо получить</param>
        public void GetPortStatus(MemoryOperand portNumber)
        {
            PerformInterruption(PortStatusGetterFunction, portNumber);
        }

        private void PerformInterruption(byte code, MemoryOperand portNumber)
        {
            RealMode.Data.Set(portNumber);
            PerformInterruption(code);
        }

        #endregion

        #region Константы

        /// <summary>
        /// Номер функции инициализации COM порта
        /// </summary>
        public const byte ComInitializeFunction = 0x00;
        /// <summary>
        /// Номер функции отправки символа через COM порт
        /// </summary>
        public const byte CharSendFunction = 0x01;
        /// <summary>
        /// Номер функции чтения символа из COM порта
        /// </summary>
        public const byte CharReadFunction = 0x02;
        /// <summary>
        /// Номер функции получения статуса COM порта
        /// </summary>
        public const byte PortStatusGetterFunction = 0x03;

        #endregion
    }
}
