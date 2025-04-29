namespace Acly.Assembler.Tables
{
    /// <summary>
    /// Глобальная таблица дескрипторов
    /// </summary>
    public class GlobalDescriptorTable : StandardDescriptorTable
    {
        /// <summary>
        /// Создать новый экземпляр таблицы дескрипторов
        /// </summary>
        /// <param name="name"></param>
        public GlobalDescriptorTable(string name) : base(name)
        {
        }

        #region Управление

        /// <summary>
        /// Создать системный дескриптор
        /// </summary>
        /// <param name="name">Название нового дескриптора</param>
        /// <returns>Новый системный дескриптор</returns>
        public SystemDescriptor CreateSystemDescriptor(string name)
        {
            SystemDescriptor result = new(name);
            AddDescriptor(result);

            return result;
        }

        #endregion
    }
}
