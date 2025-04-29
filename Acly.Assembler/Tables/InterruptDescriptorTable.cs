using System.Runtime.Serialization;
using System.Text;

namespace Acly.Assembler.Tables
{
    /// <summary>
    /// Таблица прерываний
    /// </summary>
    public class InterruptDescriptorTable : DescriptorTable
    {
        internal InterruptDescriptorTable(string name) : base(name)
        {
        }

        #region Управление

        /// <summary>
        /// Создать дескриптор шлюза прерывания
        /// </summary>
        /// <param name="name">Название дескриптора</param>
        /// <returns>Дескриптор шлюза прерывания</returns>
        public InterruptGateDescriptor CreateInterruptGate(string name)
        {
            InterruptGateDescriptor result = new(name);
            AddDescriptor(result);

            return result;
        }
        /// <summary>
        /// Создать дескриптор ловушки прерывания
        /// </summary>
        /// <param name="name">Название дескриптора</param>
        /// <returns>Дескриптор ловушки прерывания</returns>
        public TrapGateDescriptor CreateTrapGate(string name)
        {
            TrapGateDescriptor result = new(name);
            AddDescriptor(result);

            return result;
        }
        /// <summary>
        /// Создать дескриптор шлюза задачи
        /// </summary>
        /// <param name="name">Название дескриптора</param>
        /// <returns>Дескриптор шлюза задачи</returns>
        public TaskGateDescriptor CreateTaskGate(string name)
        {
            TaskGateDescriptor result = new(name);
            AddDescriptor(result);

            return result;
        }

        #endregion

        #region Ассемблер

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public override string ToAssembler()
        {
            string formattedName = FormatName();
            StringBuilder builder = new();

            builder.AppendLine($"; {GetType().Name} - {formattedName}");
            builder.AppendLine($"{formattedName}_start:");

            ForEachDescriptors<InterruptDescriptor>(descriptor =>
            {
                var lines = descriptor.ToAssembler().Split('\n');
                builder.AppendLine($"    ; Дескриптор {descriptor.Name}");

                foreach (var line in lines)
                {
                    builder.AppendLine("    " + line);
                }

                builder.AppendLine();
            });

            builder.AppendLine($"{formattedName}_end:");
            builder.AppendLine();

            // Дескриптор для LIDT
            builder.AppendLine($"{formattedName}{NamePostfix}:");
            builder.AppendLine($"    dw {formattedName}_end - {formattedName}_start - 1  ; Размер IDT");
            builder.AppendLine($"    dd {formattedName}_start                ; Адрес IDT");

            return builder.ToString();
        }

        #endregion
    }
}
