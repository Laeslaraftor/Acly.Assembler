namespace Acly.Assembler
{
    /// <summary>
    /// Интерфейс генератора ассемблерного кода
    /// </summary>
    public interface ICodeGenerator
    {
        /// <summary>
        /// Сгенерировать ассемблерный код
        /// </summary>
        /// <returns></returns>
        public GeneratedCode GenerateCode();
    }
}
