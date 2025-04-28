namespace Acly.Assembler.Registers
{
    /// <summary>
    /// Класс с методами расширения для операций над регистрами
    /// </summary>
    public static class RegisterOperationExtensions
    {
        #region Основные операции

        /// <summary>
        /// Задать значение
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник значения</param>
        public static void Set(this Register register, MemoryOperand source)
        {
            register.Set(null, false, source);
        }
        /// <summary>
        /// Прибавить значение
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник прибавляемого значения</param>
        public static void Add(this Register register, MemoryOperand source)
        {
            register.Add(null, false, source);
        }
        /// <summary>
        /// Вычесть значение
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник вычитаемого значения</param>
        public static void Subtract(this Register register, MemoryOperand source)
        {
            register.Subtract(null, false, source);
        }
        /// <summary>
        /// Умножить без знака на значение
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник множителя</param>
        public static void Multiply(this Register register, MemoryOperand source)
        {
            register.Multiply(null, false, source);
        }
        /// <summary>
        /// Умножить со знаком на значение
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник множителя</param>
        public static void IMultiply(this Register register, MemoryOperand source)
        {
            register.IMultiply(null, false, source);
        }

        /// <summary>
        /// Вычислить настоящий (эффективный) адрес источника и установить его
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник, адрес которого надо вычислить и установить</param>
        public static void LoadEffectiveAddress(this Register register, MemoryOperand source)
        {
            register.LoadEffectiveAddress(null, false, source);
        }

        #endregion

        #region Сдвиги

        /// <summary>
        /// Сдвинуть влево
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник значения сдвига</param>
        public static void ShiftLeft(this Register register, MemoryOperand source)
        {
            register.ShiftLeft(null, false, source);
        }
        /// <summary>
        /// Сдвинуть вправо
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник значения сдвига</param>
        public static void ShiftRight(this Register register, MemoryOperand source)
        {
            register.ShiftRight(null, false, source);
        }
        /// <summary>
        /// Арифметически сдвинуть влево
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник значения сдвига</param>
        public static void ShiftArithmeticLeft(this Register register, MemoryOperand source)
        {
            register.ShiftArithmeticLeft(null, false, source);
        }
        /// <summary>
        /// Арифметически сдвинуть вправо
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник значения сдвига</param>
        public static void ShiftArithmeticRight(this Register register, MemoryOperand source)
        {
            register.ShiftArithmeticRight(null, false, source);
        }
        /// <summary>
        /// Циклически сдвинуть влево. 
        /// Сдвигает все биты операнда влево, но вышедшие за пределы старшие биты возвращаются на место младших битов (циклически)
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник значения сдвига</param>
        public static void RotateLeft(this Register register, MemoryOperand source)
        {
            register.RotateLeft(null, false, source);
        }
        /// <summary>
        /// Циклически сдвинуть вправо. 
        /// Сдвигает все биты операнда вправо, но вышедшие за пределы старшие биты возвращаются на место младших битов (циклически)
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник значения сдвига</param>
        public static void RotateRight(this Register register, MemoryOperand source)
        {
            register.RotateRight(null, false, source);
        }
        /// <summary>
        /// Циклически сдвинуть влево через флаг переноса CF.
        /// Сдвигает все биты операнда влево, но старший бит перемещается в флаг переноса (CF), а значение CF перемещается на место младшего бита
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник значения сдвига</param>
        public static void RotateCarryLeft(this Register register, MemoryOperand source)
        {
            register.RotateCarryLeft(null, false, source);
        }
        /// <summary>
        /// Циклически сдвинуть вправо через флаг переноса CF.
        /// Сдвигает все биты операнда вправо, но старший бит перемещается в флаг переноса (CF), а значение CF перемещается на место младшего бита
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник значения сдвига</param>
        public static void RotateCarryRight(this Register register, MemoryOperand source)
        {
            register.RotateCarryRight(null, false, source);
        }

        #endregion

        #region Логические операции

        /// <summary>
        /// Логическая операция И.
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник значения для выполнения операции</param>
        public static void And(this Register register, MemoryOperand source)
        {
            register.And(null, false, source);
        }
        /// <summary>
        /// Логическая операция ИЛИ.
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник значения для выполнения операции</param>
        public static void Or(this Register register, MemoryOperand source)
        {
            register.Or(null, false, source);
        }
        /// <summary>
        /// Логическая операция исключающее ИЛИ.
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник значения для выполнения операции</param>
        public static void Xor(this Register register, MemoryOperand source)
        {
            register.Xor(null, false, source);
        }

        #endregion

        #region Проверки

        /// <summary>
        /// Проверить равны ли значения
        /// </summary>
        /// <param name="register">Регистр, который необходимо проверить</param>
        /// <param name="source">Источник значения для выполнения операции</param>
        public static void Compare(this Register register, MemoryOperand source)
        {
            register.Compare(null, false, source);
        }
        /// <summary>
        /// Побитовое сравнение (AND без сохранения результата)
        /// </summary>
        /// <param name="register">Регистр, который необходимо проверить</param>
        public static void EqualsZero(this Register register)
        {
            register.EqualsZero(null, false, register);
        }

        #endregion
    }
}
