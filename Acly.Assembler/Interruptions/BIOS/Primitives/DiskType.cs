namespace Acly.Assembler.Interruptions
{
    /// <summary>
    /// Тип диска
    /// </summary>
    public enum DiskType : byte
    {
        /// <summary>
        /// Флоппи диск
        /// </summary>
        Floppy = 0x00,
        /// <summary>
        /// Жёсткий диск
        /// </summary>
        HardDrive = 0x80,
    }
}
