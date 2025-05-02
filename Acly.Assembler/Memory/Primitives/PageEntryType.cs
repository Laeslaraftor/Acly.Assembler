namespace Acly.Assembler.Memory
{
    /// <summary>
    /// Типы записей в таблицах страниц
    /// </summary>
    public enum PageEntryType
    {
        /// <summary>
        /// Ссылка на следующую таблицу
        /// </summary>
        PageTableReference,
        /// <summary>
        /// Ссылка на физическую страницу
        /// </summary>
        PageFrame
    }
}
