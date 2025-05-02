namespace Acly.Assembler.Memory
{
    /// <summary>
    /// PML4 таблица
    /// </summary>
    public class PML4Table : PageTable
    {
        /// <summary>
        /// Прохерачить
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pdptAddress"></param>
        /// <param name="flags"></param>
        public void MapPDPT(int index, ulong pdptAddress, PageTableFlags flags = PageTableFlags.Present | PageTableFlags.Writable)
        {
            MapEntry(index, pdptAddress, flags,
                   (addr, name, f) => new PageTableReferenceEntry(addr, name.Replace("pml4", "pdpt"), f));
        }
    }
}
