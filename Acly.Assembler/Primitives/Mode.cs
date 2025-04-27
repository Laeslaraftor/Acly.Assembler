namespace Acly.Assembler
{
    /// <summary>
    /// Режим процессора
    /// </summary>
    public enum Mode : int
    {
        /// <summary>
        /// 16 бит (Real Mode)
        /// </summary>
        x16 = 16,
        /// <summary>
        /// 32 бит (Protected Mode)
        /// </summary>
        x32 = 32,
        /// <summary>
        /// 64 бит (Long Mode)
        /// </summary>
        x64 = 64
    }
}
