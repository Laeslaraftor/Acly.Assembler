using Acly.Assembler.Registers;

namespace Acly.Assembler.Tables
{
    /// <summary>
    /// Базовый класс системного дескриптора
    /// </summary>
    public abstract class SystemDescriptor : Descriptor
    {
        /// <summary>
        /// Селектор дескриптора кода.
        /// </summary>
        public MemoryOperand SegmentSelector { get; set; } = 0;
    }
}
