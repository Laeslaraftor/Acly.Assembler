namespace Acly.Assembler.Registers
{
    /// <summary>
    /// Базовый класс регистра
    /// </summary>
    public class Register : ValueContainer
    {
        /// <summary>
        /// Создать экземпляр регистра
        /// </summary>
        /// <param name="name">Название регистра</param>
        /// <param name="size">Битовый размер регистра</param>
        public Register(Size size, string name) : this(size)
        {
            _name = name;
        }
        /// <summary>
        /// Создать экземпляр регистра
        /// </summary>
        /// <param name="size">Битовый размер регистра</param>
        protected Register(Size size)
        {
            Size = size;
        }

        /// <summary>
        /// Размер регистра в битах.
        /// </summary>
        public Size Size { get; }
        /// <summary>
        /// Название регистра.
        /// </summary>
        public override string Name
        {
            get
            {
                if (!AsmSettings.UpperCaseRegisters && CanChangeCase)
                {
                    return _name.ToLower();
                }

                return _name;
            }
        }

        /// <summary>
        /// Можно ли менять регистр названия
        /// </summary>
        protected virtual bool CanChangeCase { get; } = true;

        private readonly string _name = string.Empty;
    }
}
