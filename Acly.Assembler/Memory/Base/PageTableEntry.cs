using System.Collections.Generic;

namespace Acly.Assembler.Memory
{
    /// <summary>
    /// Базовый класс записей таблиц страниц
    /// </summary>
    public abstract class PageTableEntry
    {
        /// <summary>
        /// Новый экземпляр записи таблицы страниц
        /// </summary>
        /// <param name="physicalAddress">Физический адрес страницы</param>
        /// <param name="flags">Флаги страницы</param>
        public PageTableEntry(ulong physicalAddress, PageTableFlags flags)
        {
            PhysicalAddress = physicalAddress;
            Flags = flags;
        }
        /// <summary>
        /// Новый экземпляр записи таблицы страниц
        /// </summary>
        public PageTableEntry()
        {
        }

        /// <summary>
        /// Физический адрес страницы
        /// </summary>
        public ulong PhysicalAddress { get; set; }
        /// <summary>
        /// Флаги записи
        /// </summary>
        public PageTableFlags Flags { get; set; }
        /// <summary>
        /// Тип страницы
        /// </summary>
        public abstract PageEntryType EntryType { get; }

        #region Управление

        /// <summary>
        /// Получить значение страницы
        /// </summary>
        /// <returns>Значение страницы</returns>
        public ulong GetEntryValue()
        {
            ulong value = PhysicalAddress & 0x000FFFFFFFFFF000;
            value |= (ulong)Flags;

            if (EntryType == PageEntryType.PageTableReference)
            {
                value |= (ulong)PageTableFlags.Present;
            }

            return value;
        }

        /// <summary>
        /// Получить исходный ассемблерный код страницы
        /// </summary>
        /// <returns>Ассемблерный код страницы</returns>
        public abstract string ToAssembler();

        /// <summary>
        /// Конвертировать флаги в строковой ассемблерный вариант
        /// </summary>
        /// <returns>Флаги в строковом ассемблерном варианте</returns>
        protected virtual string FlagsToString()
        {
            List<string> flags = new();

            if (Flags.HasFlag(PageTableFlags.Writable))
            {
                flags.Add("RW");
            }
            if (Flags.HasFlag(PageTableFlags.UserAccess))
            {
                flags.Add("US");
            }
            if (Flags.HasFlag(PageTableFlags.WriteThrough))
            {
                flags.Add("PWT");
            }
            if (Flags.HasFlag(PageTableFlags.CacheDisable))
            {
                flags.Add("PCD");
            }
            if (Flags.HasFlag(PageTableFlags.NoExecute))
            {
                flags.Add("NX");
            }

            return string.Join("|", flags);
        }

        #endregion
    }
}
