using Acly.Assembler.Registers;
using System.Collections.Generic;
using System.Text;

namespace Acly.Assembler.Tables
{
    /// <summary>
    /// Глобальная таблица дескрипторов (GDT)
    /// </summary>
    public class GDT : ICodeGenerator
    {
        /// <summary>
        /// Название дескриптора. Это поле задаёт название дескриптора в исходном коде.
        /// </summary>
        public string Name { get; set; } = "gdt";
        /// <summary>
        /// Кодовое имя, которое можно использовать для загрузки таблицы
        /// </summary>
        public string CodeName => $"{Name}_descriptor";
        /// <summary>
        /// Сегменты глобальной таблицы дескрипторов
        /// </summary>
        public List<Descriptor> Sections { get; } = new();

        #region Управление

        /// <summary>
        /// Получить селектор дескриптора
        /// </summary>
        /// <param name="descriptor">Дескриптор, селектор которого надо получить</param>
        /// <returns>Селектор указанного дескриптора</returns>
        /// <exception cref="AssemblerException"></exception>
        public MemoryOperand GetDescriptorSelector(Descriptor descriptor)
        {
            int index = Sections.IndexOf(descriptor);

            if (index == -1)
            {
                throw new AssemblerException("Указан неизвестный дескриптор");
            }

            return (index + 1) * 8;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public GeneratedCode GenerateCode()
        {
            StringBuilder builder = new();

            string startName = $"{Name}_start";
            string endName = $"{Name}_end";

            builder.AppendLine($"{startName}:");
            builder.AppendLine($"{Asm.Tab}; Нулевой дескриптор");
            builder.AppendLine($"{Asm.Tab}dq 0x0");
            builder.AppendLine();
            AddMainCode(builder);
            builder.AppendLine($"{endName}:");
            builder.AppendLine();
            builder.AppendLine($"{CodeName}:");
            builder.AppendLine($"{Asm.Tab}dw {endName} - {startName} - 1");
            builder.AppendLine($"{Asm.Tab}dd {startName}");

            return new(Section.Data, builder.ToString());
        }

        private void AddMainCode(StringBuilder builder)
        {
            int count = 0;

            foreach (var section in Sections)
            {
                if (section is InterruptionDescriptor)
                {
                    throw new AssemblerException("Дескриптор прерывания не может быть прописан в глобальной таблице дескрипторов! Дескрипторы прерывания должны быть в таблице дескрипторов прерываний.");
                }
                if (count > 0)
                {
                    builder.AppendLine();
                }

                int localCount = count + 1;
                builder.AppendLine($"{Asm.Tab}; Дескриптор {localCount}: 0x{(localCount * 8):X}");
                builder.Append(section.GenerateCode());
                count++;
            }
        }

        #endregion
    }
}
