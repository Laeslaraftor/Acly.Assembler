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
        public static void Set(this ValueContainer register, ValueContainer source)
        {
            register.Set(null, false, source);
        }
        /// <summary>
        /// Прибавить значение
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник прибавляемого значения</param>
        public static void Add(this ValueContainer register, ValueContainer source)
        {
            register.Add(null, false, source);
        }
        /// <summary>
        /// Вычесть значение
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник вычитаемого значения</param>
        public static void Subtract(this ValueContainer register, ValueContainer source)
        {
            register.Subtract(null, false, source);
        }

        /// <summary>
        /// Умножить без знака на значение
        /// </summary>
        /// <param name="register">Множитель</param>
        public static void Multiply(this ValueContainer register)
        {
            register.Multiply(null);
        }
        /// <summary>
        /// Умножить со знаком на значение
        /// </summary>
        /// <param name="register">Множитель</param>
        public static void IMultiply(this ValueContainer register)
        {
            register.IMultiply(null);
        }
        /// <summary>
        /// Разделить без знака на значение
        /// </summary>
        /// <param name="register">Делитель</param>
        public static void Divide(this ValueContainer register)
        {
            register.Divide(null);
        }
        /// <summary>
        /// Разделить со знаком на значение
        /// </summary>
        /// <param name="register">Делитель</param>
        public static void IDivide(this ValueContainer register)
        {
            register.IDivide(null);
        }

        /// <summary>
        /// Вычислить настоящий (эффективный) адрес источника и установить его
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник, адрес которого надо вычислить и установить</param>
        public static void LoadEffectiveAddress(this ValueContainer register, ValueContainer source)
        {
            register.LoadEffectiveAddress(null, false, source);
        }

        /// <summary>
        /// Вывести данные в порт ввода-вывода.
        /// </summary>
        /// <param name="register">Адрес, регистр или порт в которое надо вывести значение</param>
        /// <param name="source">Источник значения</param>
        public static void Output(this ValueContainer register, ValueContainer source)
        {
            register.Output(null, false, source);
        }
        /// <summary>
        /// in. Вывести данные в порт ввода-вывода.
        /// </summary>
        /// <param name="register">Место в которое будет записано значение из порта</param>
        /// <param name="source">Адрес, регистр или порт из которого надо получить значение</param>
        public static void Input(this ValueContainer register, ValueContainer source)
        {
            register.Input(null, false, source);
        }

        #endregion

        #region Сдвиги

        /// <summary>
        /// Сдвинуть влево
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник значения сдвига</param>
        public static void ShiftLeft(this ValueContainer register, ValueContainer source)
        {
            register.ShiftLeft(null, false, source);
        }
        /// <summary>
        /// Сдвинуть вправо
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник значения сдвига</param>
        public static void ShiftRight(this ValueContainer register, ValueContainer source)
        {
            register.ShiftRight(null, false, source);
        }
        /// <summary>
        /// Арифметически сдвинуть влево
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник значения сдвига</param>
        public static void ShiftArithmeticLeft(this ValueContainer register, ValueContainer source)
        {
            register.ShiftArithmeticLeft(null, false, source);
        }
        /// <summary>
        /// Арифметически сдвинуть вправо
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник значения сдвига</param>
        public static void ShiftArithmeticRight(this ValueContainer register, ValueContainer source)
        {
            register.ShiftArithmeticRight(null, false, source);
        }
        /// <summary>
        /// Циклически сдвинуть влево. 
        /// Сдвигает все биты операнда влево, но вышедшие за пределы старшие биты возвращаются на место младших битов (циклически)
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник значения сдвига</param>
        public static void RotateLeft(this ValueContainer register, ValueContainer source)
        {
            register.RotateLeft(null, false, source);
        }
        /// <summary>
        /// Циклически сдвинуть вправо. 
        /// Сдвигает все биты операнда вправо, но вышедшие за пределы старшие биты возвращаются на место младших битов (циклически)
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник значения сдвига</param>
        public static void RotateRight(this ValueContainer register, ValueContainer source)
        {
            register.RotateRight(null, false, source);
        }
        /// <summary>
        /// Циклически сдвинуть влево через флаг переноса CF.
        /// Сдвигает все биты операнда влево, но старший бит перемещается в флаг переноса (CF), а значение CF перемещается на место младшего бита
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник значения сдвига</param>
        public static void RotateCarryLeft(this ValueContainer register, ValueContainer source)
        {
            register.RotateCarryLeft(null, false, source);
        }
        /// <summary>
        /// Циклически сдвинуть вправо через флаг переноса CF.
        /// Сдвигает все биты операнда вправо, но старший бит перемещается в флаг переноса (CF), а значение CF перемещается на место младшего бита
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник значения сдвига</param>
        public static void RotateCarryRight(this ValueContainer register, ValueContainer source)
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
        public static void And(this ValueContainer register, ValueContainer source)
        {
            register.And(null, false, source);
        }
        /// <summary>
        /// Логическая операция ИЛИ.
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник значения для выполнения операции</param>
        public static void Or(this ValueContainer register, ValueContainer source)
        {
            register.Or(null, false, source);
        }
        /// <summary>
        /// Логическая операция исключающее ИЛИ.
        /// </summary>
        /// <param name="register">Регистр, который необходимо изменить</param>
        /// <param name="source">Источник значения для выполнения операции</param>
        public static void Xor(this ValueContainer register, ValueContainer source)
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
        public static void Compare(this ValueContainer register, ValueContainer source)
        {
            register.Compare(null, false, source);
        }
        /// <summary>
        /// Побитовое сравнение (AND без сохранения результата)
        /// </summary>
        /// <param name="register">Регистр, который необходимо проверить</param>
        public static void EqualsZero(this ValueContainer register)
        {
            register.EqualsZero(null, false, MemoryOperand.Create(register.Name));
        }

        #endregion
    }
}
