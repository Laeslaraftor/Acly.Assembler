using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Acly.Assembler.Memory
{
    /// <summary>
    /// Базовый класс таблицы страниц памяти
    /// </summary>
    public abstract class PageTable : List<PageTableEntry>
    {
        /// <summary>
        /// Название таблицы
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Физический адрес таблицы
        /// </summary>
        public ulong PhysicalAddress { get; set; }

        #region Управление

        /// <summary>
        /// Зегучий метод
        /// </summary>
        /// <param name="index"></param>
        /// <param name="targetAddress"></param>
        /// <param name="flags"></param>
        /// <param name="entryFactory"></param>
        public void MapEntry(int index, ulong targetAddress, PageTableFlags flags,
                            Func<ulong, string, PageTableFlags, PageTableEntry> entryFactory)
        {
            var entry = entryFactory(targetAddress, $"{Name}_entry{index}", flags);

            if (index >= Count)
            {
                Add(entry);
                return;
            }

            this[index] = entry;
        }

        /// <summary>
        /// Конвертировать таблицу страниц в исходный ассемблерный код
        /// </summary>
        /// <returns>Исходный ассемблерный код</returns>
        public virtual string ToAssembler()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{Name}:");
            sb.AppendLine($"    ; Physical: 0x{PhysicalAddress:X16}");

            for (int i = 0; i < Count; i++)
            {
                sb.Append($"    ");

                if (this[i] != null)
                {
                    sb.AppendLine(this[i].ToAssembler());
                }
                else
                {
                    sb.AppendLine($"dq 0 ; {Name}[{i}] - Not present");
                }
            }

            return sb.ToString();
        }

        #endregion

        #region Константа

        /// <summary>
        /// Количество страниц
        /// </summary>
        public const int EntryCount = 512;

        #endregion
    }
}
