using System;

namespace Acly.Assembler
{
    /// <summary>
    /// Сегмент дескриптора
    /// </summary>
    public enum Segment : byte
    {
        /// <summary>
        /// Системный сегмент (TSS, LDT, Call Gate и т.д.)
        /// </summary>
        System = 0x0,
        /// <summary>
        /// Сегмент кода/данных
        /// </summary>
        CodeData = 0x10
    }
}
