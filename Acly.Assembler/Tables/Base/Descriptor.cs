using System.Text;

namespace Acly.Assembler.Tables
{
    /// <summary>
    /// Базовый класс дескриптора
    /// </summary>
    public abstract class Descriptor
    {
        /// <summary>
        /// Описание дескриптора
        /// </summary>
        public string? Description { get; set; }

        #region Управление

        /// <summary>
        /// Получить исходный код дескриптора.
        /// </summary>
        /// <returns>Исходный код дескриптора</returns>
        public string GenerateCode()
        {
            StringBuilder builder = new();

            if (Description != null)
            {
                builder.AppendLine($"{Asm.Tab}; {Description}");
            }

            GenerateCode(builder);

            return builder.ToString();
        }

        /// <summary>
        /// Получить исходный код дескриптора
        /// </summary>
        /// <param name="builder"></param>
        protected abstract void GenerateCode(StringBuilder builder);

        #endregion
    }
}
