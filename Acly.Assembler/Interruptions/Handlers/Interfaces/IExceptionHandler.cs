using Acly.Assembler.Registers;

namespace Acly.Assembler.Interruptions
{
    /// <summary>
    /// Интерфейс обработчика исключений
    /// </summary>
    public interface IExceptionHandler
    {
        /// <summary>
        /// Обработать исключение
        /// </summary>
        /// <param name="exception">Обрабатываемое исключение</param>
        /// <returns>Адрес обработчика</returns>
        public MemoryOperand Handle(CpuInterruption exception);
    }
}
