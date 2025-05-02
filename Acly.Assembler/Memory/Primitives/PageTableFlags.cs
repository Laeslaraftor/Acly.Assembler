using System;

namespace Acly.Assembler.Memory
{
    /// <summary>
    /// Флаги записей в таблицах страниц 
    /// </summary>
    [Flags]
    public enum PageTableFlags : ulong
    {
        /// <summary>
        /// P (бит 0) - страница присутствует в памяти
        /// </summary>
        Present = 1UL << 0,
        /// <summary>
        /// R/W (бит 1) - доступна на запись
        /// </summary>
        Writable = 1UL << 1,
        /// <summary>
        /// U/S (бит 2) - доступна из пользовательского режима
        /// </summary>
        UserAccess = 1UL << 2,
        /// <summary>
        /// PWT (бит 3) - сквозная запись
        /// </summary>
        WriteThrough = 1UL << 3,
        /// <summary>
        /// PCD (бит 4) - кэширование отключено
        /// </summary>
        CacheDisable = 1UL << 4,
        /// <summary>
        /// A (бит 5) - страница была прочитана
        /// </summary>
        Accessed = 1UL << 5,
        /// <summary>
        /// D (бит 6) - страница была записана
        /// </summary>
        Dirty = 1UL << 6,
        /// <summary>
        /// PS (бит 7) - большой размер страницы
        /// </summary>
        LargePage = 1UL << 7,
        /// <summary>
        /// G (бит 8) - глобальная страница
        /// </summary>
        Global = 1UL << 8,
        /// <summary>
        /// XD (бит 63) - запрет исполнения
        /// </summary>
        NoExecute = 1UL << 63
    }
}
