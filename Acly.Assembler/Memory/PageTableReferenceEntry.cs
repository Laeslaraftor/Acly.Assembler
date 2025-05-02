using System.Collections.Generic;

namespace Acly.Assembler.Memory
{
    /// <summary>
    /// Страница-ссылка
    /// </summary>
    public class PageTableReferenceEntry : PageTableEntry
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="tablePhysicalAddress"><inheritdoc/></param>
        /// <param name="tableName"><inheritdoc/></param>
        /// <param name="flags"><inheritdoc/></param>
        public PageTableReferenceEntry(ulong tablePhysicalAddress, string tableName, PageTableFlags flags)
            : base(tablePhysicalAddress, flags)
        {
            TableName = tableName ?? "unknown_table";
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public PageTableReferenceEntry() : base()
        {
            TableName = string.Empty;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override PageEntryType EntryType { get; } = PageEntryType.PageTableReference;
        /// <summary>
        /// Название страницы
        /// </summary>
        public string TableName { get; set; }

        #region Управление

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public override string ToAssembler()
        {
            return $"dq 0x{GetEntryValue():X16} ; -> {TableName} | {FlagsToString()}";
        }

        #endregion
    }
}
