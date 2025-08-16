using Acly.Assembler.Contexts;
using Acly.Assembler.Registers;
using static Acly.Assembler.Asm;

namespace Acly.Assembler
{
    /// <summary>
    /// Статический класс с операторами
    /// </summary>
    public static class AsmOperators
    {
        #region Основные операторы

        /// <summary>
        /// mov. Задать значение
        /// </summary>
        /// <param name="destinationPrefix">Тип данных назначения</param>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="sourcePrefix">Тип данных источника</param>
        /// <param name="source">Источник значения</param>
        public static void Set(Prefix? destinationPrefix, ValueContainer destination,
                               Prefix? sourcePrefix, ValueContainer source)
        {
            Operation("mov", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// mov. Задать значение
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void Set(ValueContainer destination, ValueContainer source)
        {
            Set(null, destination, null, source);
        }

        /// <summary>
        /// add. Прибавить значение
        /// </summary>
        /// <param name="destinationPrefix">Тип данных назначения</param>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="sourcePrefix">Тип данных источника</param>
        /// <param name="source">Источник значения</param>
        public static void Add(Prefix? destinationPrefix, ValueContainer destination,
                               Prefix? sourcePrefix, ValueContainer source)
        {
            Operation("add", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// add. Прибавить значение
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void Add(ValueContainer destination, ValueContainer source)
        {
            Add(null, destination, null, source);
        }

        /// <summary>
        /// sub. Вычесть значение
        /// </summary>
        /// <param name="destinationPrefix">Тип данных назначения</param>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="sourcePrefix">Тип данных источника</param>
        /// <param name="source">Источник значения</param>
        public static void Subtract(Prefix? destinationPrefix, ValueContainer destination,
                                     Prefix? sourcePrefix, ValueContainer source)
        {
            Operation("sub", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// sub. Вычесть значение
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void Subtract(ValueContainer destination, ValueContainer source)
        {
            Subtract(null, destination, null, source);
        }

        /// <summary>
        /// mul. Умножить без знака на значение
        /// </summary>
        /// <param name="multiplier">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="prefix">Тип данных множителя</param>
        public static void Multiply(Prefix? prefix, ValueContainer multiplier)
        {
            Operation("mul", prefix, multiplier);
        }
        /// <summary>
        /// imul. Умножить со знаком на значение
        /// </summary>
        /// <param name="multiplier">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="prefix">Тип данных множителя</param>
        public static void IMultiply(Prefix? prefix, ValueContainer multiplier)
        {
            Operation("imul", prefix, multiplier);
        }

        /// <summary>
        /// div. Разделить без знака на значение
        /// </summary>
        /// <param name="divider">Делитель</param>
        /// <param name="prefix">Тип данных делителя</param>
        public static void Divide(Prefix? prefix, ValueContainer divider)
        {
            Operation("div", prefix, divider);
        }
        /// <summary>
        /// idiv. Разделить со знаком на значение
        /// </summary>
        /// <param name="divider">Делитель</param>
        /// <param name="prefix">Тип данных делителя</param>
        public static void IDivide(Prefix? prefix, ValueContainer divider)
        {
            Operation("idiv", prefix, divider);
        }

        /// <summary>
        /// Сделать число противоположным по знаку. 
        /// Если было отрицательное, то станет положительным и наоборот
        /// </summary>
        /// <param name="value">Число, которое надо инвертировать</param>
        public static void Negative(ValueContainer value)
        {
            Operation("neg", value);
        }

        /// <summary>
        /// Увеличить значение на 1
        /// </summary>
        /// <param name="register">Регистр в котором будет увеличено значение</param>
        public static void Increment(ValueContainer register)
        {
            Operation("inc", register);
        }
        /// <summary>
        /// Уменьшить значение на 1
        /// </summary>
        /// <param name="register">Регистр в котором будет уменьшено значение</param>
        public static void Decrement(ValueContainer register)
        {
            Operation("dec", register);
        }

        /// <summary>
        /// lea. Вычислить настоящий (эффективный) адрес источника и установить его
        /// </summary>
        /// <param name="destinationPrefix">Тип данных назначения</param>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="sourcePrefix">Тип данных источника</param>
        /// <param name="source">Источник значения</param>
        public static void LoadEffectiveAddress(Prefix? destinationPrefix, ValueContainer destination,
                                                Prefix? sourcePrefix, ValueContainer source)
        {
            Operation("lea", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// lea. Вычислить настоящий (эффективный) адрес источника и установить его
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void LoadEffectiveAddress(ValueContainer destination, ValueContainer source)
        {
            LoadEffectiveAddress(null, destination, null, source);
        }

        /// <summary>
        /// out. Вывести данные в порт ввода-вывода.
        /// </summary>
        /// <param name="destinationPrefix">Тип данных назначения</param>
        /// <param name="destination">Адрес, регистр или порт в которое надо вывести значение</param>
        /// <param name="sourcePrefix">Тип данных источника</param>
        /// <param name="source">Источник значения</param>
        public static void Output(Prefix? destinationPrefix, ValueContainer destination,
                                                Prefix? sourcePrefix, ValueContainer source)
        {
            Operation("out", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// out. Вывести данные в порт ввода-вывода.
        /// </summary>
        /// <param name="destination">Адрес, регистр или порт в которое надо вывести значение</param>
        /// <param name="source">Источник значения</param>
        public static void Output(ValueContainer destination, ValueContainer source)
        {
            Output(null, destination, null, source);
        }
        /// <summary>
        /// in. Получить данные из порта ввода-вывода.
        /// </summary>
        /// <param name="destinationPrefix">Тип данных назначения</param>
        /// <param name="destination">Место в которое будет записано значение из порта</param>
        /// <param name="sourcePrefix">Тип данных источника</param>
        /// <param name="source">Адрес, регистр или порт в которое надо вывести значение</param>
        public static void Input(Prefix? destinationPrefix, ValueContainer destination,
                                                Prefix? sourcePrefix, ValueContainer source)
        {
            Operation("in", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// in. Вывести данные в порт ввода-вывода.
        /// </summary>
        /// <param name="destination">Место в которое будет записано значение из порта</param>
        /// <param name="source">Адрес, регистр или порт из которого надо получить значение</param>
        public static void Input(ValueContainer destination, ValueContainer source)
        {
            Input(null, destination, null, source);
        }

        /// <summary>
        /// Поместить значение регистра в стек
        /// </summary>
        /// <param name="register">Регистр, значение которого будет помещено в стек</param>
        public static void Push(ValueContainer register)
        {
            Operation("push", register);
        }
        /// <summary>
        /// Извлечь значение регистра из стека
        /// </summary>
        /// <param name="register">Регистр, значение которого будет извлечено из стека</param>
        public static void Pop(ValueContainer register)
        {
            Operation("pop", register);
        }

        #endregion

        #region Сдвиги

        /// <summary>
        /// shl. Сдвинуть влево
        /// </summary>
        /// <param name="destinationPrefix">Тип данных назначения</param>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="sourcePrefix">Тип данных источника</param>
        /// <param name="source">Источник значения</param>
        public static void ShiftLeft(Prefix? destinationPrefix, ValueContainer destination,
                                     Prefix? sourcePrefix, ValueContainer source)
        {
            Operation("shl", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// shl. Сдвинуть влево
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void ShiftLeft(ValueContainer destination, ValueContainer source)
        {
            ShiftLeft(null, destination, null, source);
        }

        /// <summary>
        /// shr. Сдвинуть вправо
        /// </summary>
        /// <param name="destinationPrefix">Тип данных назначения</param>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="sourcePrefix">Тип данных источника</param>
        /// <param name="source">Источник значения</param>
        public static void ShiftRight(Prefix? destinationPrefix, ValueContainer destination,
                                      Prefix? sourcePrefix, ValueContainer source)
        {
            Operation("shr", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// shr. Сдвинуть вправо
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void ShiftRight(ValueContainer destination, ValueContainer source)
        {
            ShiftRight(null, destination, null, source);
        }

        /// <summary>
        /// sal. Арифметически сдвинуть влево
        /// </summary>
        /// <param name="destinationPrefix">Тип данных назначения</param>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="sourcePrefix">Тип данных источника</param>
        /// <param name="source">Источник значения</param>
        public static void ShiftArithmeticLeft(Prefix? destinationPrefix, ValueContainer destination,
                                               Prefix? sourcePrefix, ValueContainer source)
        {
            Operation("sal", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// sal. Арифметически сдвинуть влево
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void ShiftArithmeticLeft(ValueContainer destination, ValueContainer source)
        {
            ShiftArithmeticLeft(null, destination, null, source);
        }

        /// <summary>
        /// sar. Арифметически сдвинуть вправо
        /// </summary>
        /// <param name="destinationPrefix">Тип данных назначения</param>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="sourcePrefix">Тип данных источника</param>
        /// <param name="source">Источник значения</param>
        public static void ShiftArithmeticRight(Prefix? destinationPrefix, ValueContainer destination,
                                                Prefix? sourcePrefix, ValueContainer source)
        {
            Operation("sar", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// sar. Арифметически сдвинуть вправо
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void ShiftArithmeticRight(ValueContainer destination, ValueContainer source)
        {
            ShiftArithmeticRight(null, destination, null, source);
        }

        /// <summary>
        /// rol. Циклически сдвинуть влево.
        /// Сдвигает все биты операнда влево, но вышедшие за пределы старшие биты возвращаются на место младших битов (циклически)
        /// </summary>
        /// <param name="destinationPrefix">Тип данных назначения</param>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="sourcePrefix">Тип данных источника</param>
        /// <param name="source">Источник значения</param>
        public static void RotateLeft(Prefix? destinationPrefix, ValueContainer destination,
                                      Prefix? sourcePrefix, ValueContainer source)
        {
            Operation("rol", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// rol. Циклически сдвинуть влево.
        /// Сдвигает все биты операнда влево, но вышедшие за пределы старшие биты возвращаются на место младших битов (циклически)
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void RotateLeft(ValueContainer destination, ValueContainer source)
        {
            RotateLeft(null, destination, null, source);
        }

        /// <summary>
        /// ror. Циклически сдвинуть вправо. 
        /// Сдвигает все биты операнда вправо, но вышедшие за пределы старшие биты возвращаются на место младших битов (циклически)
        /// </summary>
        /// <param name="destinationPrefix">Тип данных назначения</param>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="sourcePrefix">Тип данных источника</param>
        /// <param name="source">Источник значения</param>
        public static void RotateRight(Prefix? destinationPrefix, ValueContainer destination,
                                       Prefix? sourcePrefix, ValueContainer source)
        {
            Operation("ror", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// ror. Циклически сдвинуть вправо. 
        /// Сдвигает все биты операнда вправо, но вышедшие за пределы старшие биты возвращаются на место младших битов (циклически)
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void RotateRight(ValueContainer destination, ValueContainer source)
        {
            RotateRight(null, destination, null, source);
        }

        /// <summary>
        /// rcl. Циклически сдвинуть влево через флаг переноса CF.
        /// Сдвигает все биты операнда влево, но старший бит перемещается в флаг переноса (CF), а значение CF перемещается на место младшего бита
        /// </summary>
        /// <param name="destinationPrefix">Тип данных назначения</param>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="sourcePrefix">Тип данных источника</param>
        /// <param name="source">Источник значения</param>
        public static void RotateCarryLeft(Prefix? destinationPrefix, ValueContainer destination,
                                           Prefix? sourcePrefix, ValueContainer source)
        {
            Operation("rcl", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// rcl. Циклически сдвинуть влево через флаг переноса CF.
        /// Сдвигает все биты операнда влево, но старший бит перемещается в флаг переноса (CF), а значение CF перемещается на место младшего бита
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void RotateCarryLeft(ValueContainer destination, ValueContainer source)
        {
            RotateCarryLeft(null, destination, null, source);
        }

        /// <summary>
        /// rcr. Циклически сдвинуть вправо через флаг переноса CF.
        /// Сдвигает все биты операнда вправо, но старший бит перемещается в флаг переноса (CF), а значение CF перемещается на место младшего бита
        /// </summary>
        /// <param name="destinationPrefix">Тип данных назначения</param>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="sourcePrefix">Тип данных источника</param>
        /// <param name="source">Источник значения</param>
        public static void RotateCarryRight(Prefix? destinationPrefix, ValueContainer destination,
                                            Prefix? sourcePrefix, ValueContainer source)
        {
            Operation("rcr", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// rcr. Циклически сдвинуть вправо через флаг переноса CF.
        /// Сдвигает все биты операнда вправо, но старший бит перемещается в флаг переноса (CF), а значение CF перемещается на место младшего бита
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void RotateCarryRight(ValueContainer destination, ValueContainer source)
        {
            RotateCarryRight(null, destination, null, source);
        }

        #endregion

        #region Логические операции

        /// <summary>
        /// and. Логическая операция И
        /// </summary>
        /// <param name="destinationPrefix">Тип данных назначения</param>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="sourcePrefix">Тип данных источника</param>
        /// <param name="source">Источник значения</param>
        public static void And(Prefix? destinationPrefix, ValueContainer destination,
                               Prefix? sourcePrefix, ValueContainer source)
        {
            Operation("and", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// and. Логическая операция И
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void And(ValueContainer destination, ValueContainer source)
        {
            And(null, destination, null, source);
        }

        /// <summary>
        /// or. Логическая операция ИЛИ
        /// </summary>
        /// <param name="destinationPrefix">Тип данных назначения</param>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="sourcePrefix">Тип данных источника</param>
        /// <param name="source">Источник значения</param>
        public static void Or(Prefix? destinationPrefix, ValueContainer destination,
                              Prefix? sourcePrefix, ValueContainer source)
        {
            Operation("or", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// or. Логическая операция ИЛИ
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void Or(ValueContainer destination, ValueContainer source)
        {
            Or(null, destination, null, source);
        }

        /// <summary>
        /// xor. Логическая операция исключающее ИЛИ
        /// </summary>
        /// <param name="destinationPrefix">Тип данных назначения</param>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="sourcePrefix">Тип данных источника</param>
        /// <param name="source">Источник значения</param>
        public static void Xor(Prefix? destinationPrefix, ValueContainer destination,
                               Prefix? sourcePrefix, ValueContainer source)
        {
            Operation("xor", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// xor. Логическая операция исключающее ИЛИ
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void Xor(ValueContainer destination, ValueContainer source)
        {
            Xor(null, destination, null, source);
        }

        /// <summary>
        /// not. Логическая операция НЕ. Инвертирует все биты
        /// </summary>
        /// <param name="operand"></param>
        public static void Not(ValueContainer operand)
        {
            Operation("not", operand);
        }

        #endregion

        #region Проверки

        /// <summary>
        /// test. Побитовое сравнение (AND без сохранения результата)
        /// </summary>
        /// <param name="destinationPrefix">Тип данных назначения</param>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="sourcePrefix">Тип данных источника</param>
        /// <param name="source">Источник значения</param>
        public static void Test(Prefix? destinationPrefix, ValueContainer destination,
                                Prefix? sourcePrefix, ValueContainer source)
        {
            Operation("test", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// test. Побитовое сравнение (AND без сохранения результата)
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void Test(ValueContainer destination, ValueContainer source)
        {
            Test(null, destination, null, source);
        }
        /// <summary>
        /// cmp. Проверить равны ли значения
        /// </summary>
        /// <param name="destinationPrefix">Тип данных назначения</param>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="sourcePrefix">Тип данных источника</param>
        /// <param name="source">Источник значения</param>
        public static void Compare(Prefix? destinationPrefix, ValueContainer destination,
                                   Prefix? sourcePrefix, ValueContainer source)
        {
            Operation("cmp", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// cmp. Проверить равны ли значения
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void Compare(ValueContainer destination, ValueContainer source)
        {
            Compare(null, destination, null, source);
        }

        #endregion

        #region Дополнительно

        private static void CountOperation(string opCode, Prefix? destinationPrefix, ValueContainer destination,
                                                   Prefix? sourcePrefix, ValueContainer source)
        {
            if (Context.Count.Lower == null || RealModeContext.Instance.Count.Lower == null)
            {
                return;
            }

            Context.Count.Push();
            Context.Count.Set(source);

            Operation(opCode, destinationPrefix, destination, sourcePrefix, RealModeContext.Instance.Count.Lower);

            Context.Count.Pop();
        }
        private static void Operation(string opCode, Prefix? destinationPrefix, ValueContainer destination,
                                                     Prefix? sourcePrefix, ValueContainer source)
        {
            string line = opCode;
            line = AddData(line, destinationPrefix, destination) + ",";
            line = AddData(line, sourcePrefix, source);

            Emit(line);
        }
        private static void Operation(string opCode, ValueContainer register)
        {
            Emit($"{opCode} {register}");
        }
        private static void Operation(string opCode, Prefix? prefix, ValueContainer register)
        {
            if (prefix == null)
            {
                Operation(opCode, register);
                return;
            }

            Emit($"{opCode} {prefix.ToString().ToLower()} {register}");
        }

        private static string AddData(string line, Prefix? prefix, ValueContainer operand)
        {
            if (prefix != null)
            {
                line += $" {prefix.ToString().ToLower()}";
            }

            line += $" {operand}";

            return line;
        }

        #endregion
    }
}
