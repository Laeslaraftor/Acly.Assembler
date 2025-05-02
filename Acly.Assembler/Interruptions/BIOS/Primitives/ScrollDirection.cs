namespace Acly.Assembler.Interruptions
{
    /// <summary>
    /// Направление прокрутки экрана
    /// </summary>
    public enum ScrollDirection : byte
    {
        /// <summary>
        /// Прокрутка вверх
        /// </summary>
        Up = BiosVideoInterruption.ScrollUpFunction,
        /// <summary>
        /// Прокрутка вниз
        /// </summary>
        Down = BiosVideoInterruption.ScrollDownFunction
    }
}
