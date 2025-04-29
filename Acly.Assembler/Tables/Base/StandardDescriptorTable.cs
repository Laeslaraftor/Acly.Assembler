namespace Acly.Assembler.Tables
{
    /// <summary>
    /// Базовый класс стандартной таблицы дескрипторов (GDT, LDT)
    /// </summary>
    public abstract class StandardDescriptorTable : DescriptorTable
    {
        /// <summary>
        /// Создать новый экземпляр стандартной таблицы дескрипторов
        /// </summary>
        /// <param name="name">Название таблицы</param>
        public StandardDescriptorTable(string name) : base(name)
        {
        }

        #region Управление

        /// <summary>
        /// Создать дескриптор сегмента кода
        /// </summary>
        /// <param name="name">Название нового дескриптора</param>
        /// <returns>Новый дескриптор сегмента кода</returns>
        public virtual CodeDescriptor CreateCodeSegment(string name)
        {
            CodeDescriptor result = new(name);
            AddDescriptor(result);

            return result;
        }
        /// <summary>
        /// Создать дескриптор сегмента данных
        /// </summary>
        /// <param name="name">Название нового дескриптора</param>
        /// <returns>Новый дескриптор сегмента данных</returns>
        public virtual DataDescriptor CreateDataSegment(string name)
        {
            DataDescriptor result = new(name);
            AddDescriptor(result);

            return result;
        }

        #endregion
    }
}
