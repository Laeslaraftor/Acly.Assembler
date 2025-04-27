namespace Acly.Assembler.Registers
{
    /// <summary>
    /// Базовый класс общего регистра
    /// </summary>
    public class GeneralRegister : Register
    {
        /// <summary>
        /// Создать экземпляр общего регистра
        /// </summary>
        /// <param name="name">Название общего регистра</param>
        /// <param name="higher">Старший регистр</param>
        /// <param name="lower">Младший регистр</param>
        /// <param name="size">Битовый размер регистра</param>
        public GeneralRegister(Size size, string name, GeneralRegister? higher = null, GeneralRegister? lower = null)
            : base(size, name)
        {
            Higher = higher;
            Lower = lower;
        }

        /// <summary>
        /// Старший регистр
        /// </summary>
        public GeneralRegister? Higher { get; }
        /// <summary>
        /// Младший регистр
        /// </summary>
        public GeneralRegister? Lower { get; }
    }
}
