namespace Acly.Assembler.Memory
{
    /// <summary>
    /// PD таблица
    /// </summary>
    public class PageDirectory : PageTable
    {
        /// <summary>
        /// Прохерачить
        /// </summary>
        /// <param name="index"></param>
        /// <param name="ptAddress"></param>
        /// <param name="flags"></param>
        public void MapPT(int index, ulong ptAddress, PageTableFlags flags = PageTableFlags.Present | PageTableFlags.Writable)
        {
            MapEntry(index, ptAddress, flags,
                   (addr, name, f) => new PageTableReferenceEntry(addr, name.Replace("pd", "pt"), f));
        }
        /// <summary>
        /// Прохерачить
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageAddress"></param>
        /// <param name="flags"></param>
        public void Map2MBPage(int index, ulong pageAddress, PageTableFlags flags = PageTableFlags.Present | PageTableFlags.Writable | PageTableFlags.LargePage)
        {
            MapEntry(index, pageAddress, flags | PageTableFlags.LargePage,
                   (addr, name, f) => new PageFrameEntry(addr, f));
        }
    }
}
