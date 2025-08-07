namespace Acly.Assembler.Tables
{
    /// <summary>
    /// Тип обрабатываемого прерывания
    /// </summary>
    public enum InterruptionType : byte
    {
        /// <summary>
        /// Переключение контекста (TSS)
        /// </summary>
        TaskGate = 0x5,
        /// <summary>
        /// 16 битный шлюз прерывания
        /// </summary>
        x16Interrupt = 0x6,
        /// <summary>
        /// 16 битный шлюз ловушки (не маскируется)
        /// </summary>
        x16Trap = 0x7,
        /// <summary>
        /// Стандартный шлюз прерывания
        /// </summary>
        x32_64Interrupt = 0xE,
        /// <summary>
        /// Шлюз ловушки (не маскируется)
        /// </summary>
        x32_64Trap = 0xF
    }
}
