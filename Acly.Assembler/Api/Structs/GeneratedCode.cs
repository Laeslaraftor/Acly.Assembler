namespace Acly.Assembler
{
    /// <summary>
    /// Структура сгенерированного кода
    /// </summary>
    public readonly struct GeneratedCode
    {
        /// <summary>
        /// Создать сгенерированный код
        /// </summary>
        /// <param name="section">Сегмент в котором должен находится сгенерированный код</param>
        /// <param name="code">Сгенерированный код</param>
        public GeneratedCode(Section section, string code) : this(section, code, Asm.CurrentMode)
        {
        }
        /// <summary>
        /// Создать сгенерированный код
        /// </summary>
        /// <param name="section">Сегмент в котором должен находится сгенерированный код</param>
        /// <param name="code">Сгенерированный код</param>
        /// <param name="mode">Режим работы процессора в котором должен выполняться сгенерированный код</param>
        public GeneratedCode(Section section, string code, Mode mode)
        {
            Section = section;
            Code = code;
            Mode = mode;
        }

        /// <summary>
        /// Секция в которой должен находиться код
        /// </summary>
        public Section Section { get; }
        /// <summary>
        /// Сгенерированный код
        /// </summary>
        public string Code { get; }
        /// <summary>
        /// Режим работы процессора
        /// </summary>
        public Mode Mode { get; }
    }
}
