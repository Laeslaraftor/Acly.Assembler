using System;

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
        /// <param name="size">Битовый размер регистра</param>
        public GeneralRegister(Size size, string name)
            : base(size, name)
        {
        }
        /// <summary>
        /// Создать экземпляр общего регистра
        /// </summary>
        /// <param name="name">Название общего регистра</param>
        /// <param name="higher">Старший регистр</param>
        /// <param name="lower">Младший регистр</param>
        /// <param name="size">Битовый размер регистра</param>
        public GeneralRegister(Size size, string name, GeneralRegister? higher = null, GeneralRegister? lower = null)
            : this(size, name, () => higher, () => lower)
        {
        }
        internal GeneralRegister(Size size, string name, Func<GeneralRegister?>? higherGetter = null, Func<GeneralRegister?>? lowerGetter = null)
            : base(size, name)
        {
            _higherGetter = higherGetter;
            _lowerGetter = lowerGetter;
        }

#nullable disable

        /// <summary>
        /// Старший регистр
        /// </summary>
        public GeneralRegister Higher => _higherGetter?.Invoke();
        /// <summary>
        /// Младший регистр
        /// </summary>
        public GeneralRegister Lower => _lowerGetter?.Invoke();

#nullable enable

        private readonly Func<GeneralRegister?>? _higherGetter;
        private readonly Func<GeneralRegister?>? _lowerGetter;
    }
}
