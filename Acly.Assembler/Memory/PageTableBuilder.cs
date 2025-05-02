using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acly.Assembler.Memory
{
    /// <summary>
    /// Построитель таблицы страниц памяти
    /// </summary>
    public class PageTableBuilder
    {
        /// <summary>
        /// Адрес PML4 таблицы
        /// </summary>
        public ulong PML4Address
        {
            get
            {
                foreach (var table in _tables.Values)
                {
                    if (table is PML4Table)
                    {
                        return table.PhysicalAddress;
                    }
                }

                return 0;
            }
        }

        private readonly Dictionary<string, PageTable> _tables = new();

        #region Управление

        /// <summary>
        /// Создать PML4 таблицу
        /// </summary>
        /// <param name="name">Название таблицы</param>
        /// <param name="physicalAddress">Физический адрес таблицы</param>
        /// <returns>Таблица страниц памяти</returns>
        public PML4Table CreatePML4(string name, ulong physicalAddress)
        {
            PML4Table pml4 = new()
            {
                Name = name,
                PhysicalAddress = physicalAddress
            };

            _tables.Add(name, pml4);

            return pml4;
        }
        /// <summary>
        /// Создать PDPT таблицу
        /// </summary>
        /// <param name="name">Название таблицы</param>
        /// <param name="physicalAddress">Физический адрес таблицы</param>
        /// <returns>Таблица страниц памяти</returns>
        public PDPTable CreatePDPT(string name, ulong physicalAddress)
        {
            PDPTable pdpt = new()
            {
                Name = name,
                PhysicalAddress = physicalAddress
            };

            _tables.Add(name, pdpt);

            return pdpt;
        }
        /// <summary>
        /// Создать PD таблицу
        /// </summary>
        /// <param name="name">Название таблицы</param>
        /// <param name="physicalAddress">Физический адрес таблицы</param>
        /// <returns>Таблица страниц памяти</returns>
        public PageDirectory CreatePD(string name, ulong physicalAddress)
        {
            PageDirectory pd = new()
            {
                Name = name,
                PhysicalAddress = physicalAddress
            };

            _tables.Add(name, pd);

            return pd;
        }
        /// <summary>
        /// Создать PT таблицу
        /// </summary>
        /// <param name="name">Название таблицы</param>
        /// <param name="physicalAddress">Физический адрес таблицы</param>
        /// <returns>Таблица страниц памяти</returns>
        public PTTable CreatePT(string name, ulong physicalAddress)
        {
            PTTable pt = new()
            {
                Name = name,
                PhysicalAddress = physicalAddress
            };

            _tables.Add(name, pt);

            return pt;
        }

        /// <summary>
        /// Получить исходный ассемблерный код таблицы
        /// </summary>
        /// <returns>Исходный ассемблерный код</returns>
        public string GenerateAssemblerCode()
        {
            var sb = new StringBuilder();
            sb.AppendLine("; Page Tables Structure");
            sb.AppendLine("align 4096");
            sb.AppendLine();

            foreach (var table in _tables.Values)
            {
                sb.AppendLine(table.ToAssembler());
                sb.AppendLine();
            }

            // Генерация дескриптора для CR3
            var pml4 = _tables.Values.FirstOrDefault(t => t is PML4Table);

            if (pml4 != null)
            {
                sb.AppendLine("; CR3 Register Value");
                sb.AppendLine($"cr3_value: dd 0x{pml4.PhysicalAddress:X8}");
            }

            return sb.ToString();
        }

        #endregion
    }
}
