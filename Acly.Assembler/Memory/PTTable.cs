namespace Acly.Assembler.Memory
{
    /// <summary>
    /// PT таблица
    /// </summary>
    public class PTTable : PageTable
    {
        /// <summary>
        /// Прохерачить
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageAddress"></param>
        /// <param name="flags"></param>
        public void Map4KBPage(int index, ulong pageAddress, PageTableFlags flags = PageTableFlags.Present | PageTableFlags.Writable)
        {
            MapEntry(index, pageAddress, flags,
                   (addr, name, f) => new PageFrameEntry(addr, f));
        }
    }
}
