namespace Acly.Assembler.Memory
{
    /// <summary>
    /// PDP таблица
    /// </summary>
    public class PDPTable : PageTable
    {
        #region Управление

        /// <summary>
        /// Прохерачить
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pdAddress"></param>
        /// <param name="flags"></param>
        public void MapPD(int index, ulong pdAddress, PageTableFlags flags = PageTableFlags.Present | PageTableFlags.Writable)
        {
            MapEntry(index, pdAddress, flags,
                   (addr, name, f) => new PageTableReferenceEntry(addr, name.Replace("pdpt", "pd"), f));
        }
        /// <summary>
        /// Прохерачить
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageAddress"></param>
        /// <param name="flags"></param>
        public void Map1GBPage(int index, ulong pageAddress, PageTableFlags flags = PageTableFlags.Present | PageTableFlags.Writable | PageTableFlags.LargePage)
        {
            MapEntry(index, pageAddress, flags | PageTableFlags.LargePage,
                   (addr, name, f) => new PageFrameEntry(addr, f));
        }

        #endregion
    }
}
