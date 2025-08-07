using System.Collections.Generic;
using System.Text;

namespace Acly.Assembler.Tables
{
    /// <summary>
    /// Таблица дескрипторов прерываний (IDT)
    /// </summary>
    public class IDT : ICodeGenerator
    {
        /// <summary>
        /// Название дескриптора. Это поле задаёт название дескриптора в исходном коде.
        /// </summary>
        public string Name { get; set; } = "idt";
        /// <summary>
        /// Кодовое имя, которое можно использовать для загрузки таблицы
        /// </summary>
        public string CodeName => $"{Name}_descriptors";
        /// <summary>
        /// Кодовое имя, которое обозначает начало таблицы дескрипторов прерывания
        /// </summary>
        public string StartCodeName => $"{Name}_start";
        /// <summary>
        /// Сегменты глобальной таблицы дескрипторов
        /// </summary>
        public List<InterruptionDescriptor> Sections { get; } = new();

        #region Управление

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public GeneratedCode GenerateCode()
        {
            StringBuilder builder = new();

            string endName = $"{Name}_end";

            builder.AppendLine("align 16");
            builder.AppendLine($"{StartCodeName}:");
            AddMainCode(builder);
            builder.AppendLine($"{endName}:");
            builder.AppendLine();
            builder.AppendLine($"{CodeName}:");
            builder.AppendLine($"{Asm.Tab}dw {endName} - {StartCodeName} - 1");
            builder.AppendLine($"{Asm.Tab}dq {StartCodeName}");

            return new(Section.Data, builder.ToString());
        }

        private void AddMainCode(StringBuilder builder)
        {
            int count = 0;

            foreach (var section in Sections)
            {
                if (count > 0)
                {
                    builder.AppendLine();
                }

                builder.AppendLine($"{Asm.Tab}; Дескриптор {count + 1}");
                builder.Append(section.GenerateCode());
                count++;
            }
        }

        #endregion
    }
}
