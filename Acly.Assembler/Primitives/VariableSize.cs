namespace Acly.Assembler
{
    /// <summary>
    /// Размер переменной
    /// </summary>
    public enum VariableSize
    {
        /// <summary>
        /// db. Байт (1 байт)
        /// </summary>
        Byte,
        /// <summary>
        /// dw. Слово (2 байта)
        /// </summary>
        Word,
        /// <summary>
        /// dd. Двойное слово (4 байта)
        /// </summary>
        DoubleWord,
        /// <summary>
        /// dq. Квад-слово (8 байт)
        /// </summary>
        QuadWord,
    }
}
