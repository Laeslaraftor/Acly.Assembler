using System.Text;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Acly.Assembler.Tables
{
    /// <summary>
    /// Базовый класс таблицы дескрипторов
    /// </summary>
    public abstract class DescriptorTable
    {
        /// <summary>
        /// Создать экземпляр таблицы дескрипторов
        /// </summary>
        /// <param name="name">Название таблицы</param>
        protected DescriptorTable(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Название таблицы дескрипторов
        /// </summary>
        public string Name { get; }

        private readonly Dictionary<string, Descriptor> _descriptors = new();

        #region Управление

        /// <summary>
        /// Добавить дескриптор в таблицу
        /// </summary>
        /// <param name="descriptor">Дескриптор, который надо добавить в таблицу</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected void AddDescriptor(Descriptor descriptor)
        {
            if (descriptor == null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            _descriptors.Add(descriptor.Name, descriptor);
        }

        /// <summary>
        /// Пройтись по всем добавленным дескрипторам
        /// </summary>
        /// <typeparam name="T">Тип дескриптора</typeparam>
        /// <param name="action">Действие для прохода по дескриптору</param>
        protected void ForEachDescriptors<T>(Action<T> action) where T : Descriptor
        {
            foreach (var descriptor in _descriptors.Values)
            {
                if (descriptor is T typedDescriptor)
                {
                    action(typedDescriptor);
                }
            }
        }

        #endregion

        #region Ассемблер

        /// <summary>
        /// Получить ассемблерный код таблицы дескрипторов
        /// </summary>
        /// <returns>Ассемблерный код таблицы дескрипторов</returns>
        public virtual string ToAssembler()
        {
            string formattedName = FormatName();
            var builder = new StringBuilder();

            builder.AppendLine($"; {GetType().Name} - {formattedName}");
            builder.AppendLine($"{formattedName}_start:");
            builder.AppendLine("    ; NULL Descriptor");
            builder.AppendLine("    dq 0");

            foreach (var descriptor in _descriptors.Values)
            {
                var lines = descriptor.ToAssembler().Split('\n');

                builder.AppendLine($"    ; Дескриптор {descriptor.Name}");

                foreach (var line in lines)
                {
                    builder.AppendLine("    " + line);
                }

                builder.AppendLine();
            }

            builder.AppendLine($"{formattedName}_end:");
            builder.AppendLine();
            builder.AppendLine($"{formattedName}{NamePostfix}:");
            builder.AppendLine($"    dw {formattedName}_end - {formattedName}_start - 1  ; Размер таблицы");
            builder.AppendLine($"    dd {formattedName}_start                ; Адрес таблицы");

            return builder.ToString();
        }

        /// <summary>
        /// Получить отформатированное имя таблицы
        /// </summary>
        /// <returns>Отформатированное имя таблицы</returns>
        protected string FormatName()
        {
            return Name.Trim().Replace(" ", string.Empty);
        }
        internal string GetFormattedName() => FormatName();

        #endregion

        #region Дополнительно

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Таблица: {Name}";
        }

        #endregion

        #region Константы

        internal const string NamePostfix = "_descriptor";

        #endregion
    }
}
