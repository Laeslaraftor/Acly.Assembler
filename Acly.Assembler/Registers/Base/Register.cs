namespace Acly.Assembler.Registers
{
    /// <summary>
    /// Базовый класс регистра
    /// </summary>
    public class Register
    {
        /// <summary>
        /// Создать экземпляр регистра
        /// </summary>
        /// <param name="name">Название регистра</param>
        /// <param name="size">Битовый размер регистра</param>
        public Register(Size size, string name)
        {
            Name = name;
            Size = size;
        }

        /// <summary>
        /// Размер регистра в битах.
        /// </summary>
        public Size Size { get; }
        /// <summary>
        /// Название регистра.
        /// </summary>
        public string Name { get; }

        #region Дополнительно

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
