namespace Acly.Assembler
{
    /// <summary>
    /// Настройки синтаксиса ассемблера
    /// </summary>
    public static class AsmSettings
    {
        /// <summary>
        /// Использовать верхний регистр при написании регистров. 
        /// Например, если true - EAX, иначе - eax. По умолчанию - true
        /// </summary>
        public static bool UpperCaseRegisters { get; set; } = true;
    }
}
