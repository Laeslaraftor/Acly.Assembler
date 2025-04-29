namespace Acly.Assembler
{
    /// <summary>
    /// Уровни привилегий
    /// </summary>
    public enum PrivilegeLevel : byte
    {
        /// <summary>
        /// Уровень ядра операционной системы
        /// </summary>
        Ring0 = 0x1,
        /// <summary>
        /// Уровень драйверов операционной системы
        /// </summary>
        Ring1 = 0x2,
        /// <summary>
        /// Уровень системы. Например, оболочки операционной системы
        /// </summary>
        Ring2 = 0x4,
        /// <summary>
        /// Пользовательский уровень. На этом уровне работают прикладные программы
        /// </summary>
        Ring3 = 0x6
    }
}
