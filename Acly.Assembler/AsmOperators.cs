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
        public static void Set(Prefix? destinationPrefix, MemoryOperand destination,
                               Prefix? sourcePrefix, MemoryOperand source)
        {
            Operation("mov", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// mov. Задать значение
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void Set(MemoryOperand destination, MemoryOperand source)
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
        public static void Add(Prefix? destinationPrefix, MemoryOperand destination,
                               Prefix? sourcePrefix, MemoryOperand source)
        {
            Operation("add", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// add. Прибавить значение
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void Add(MemoryOperand destination, MemoryOperand source)
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
        public static void Subtract(Prefix? destinationPrefix, MemoryOperand destination,
                                     Prefix? sourcePrefix, MemoryOperand source)
        {
            Operation("sub", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// sub. Вычесть значение
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void Subtract(MemoryOperand destination, MemoryOperand source)
        {
            Subtract(null, destination, null, source);
        }

        /// <summary>
        /// mul. Умножить без знака на значение
        /// </summary>
        /// <param name="destinationPrefix">Тип данных назначения</param>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="sourcePrefix">Тип данных источника</param>
        /// <param name="source">Источник значения</param>
        public static void Multiply(Prefix? destinationPrefix, MemoryOperand destination,
                                    Prefix? sourcePrefix, MemoryOperand source)
        {
            Operation("mul", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// mul. Умножить без знака на значение
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void Multiply(MemoryOperand destination, MemoryOperand source)
        {
            Multiply(null, destination, null, source);
        }

        /// <summary>
        /// imul. Умножить со знаком на значение
        /// </summary>
        /// <param name="destinationPrefix">Тип данных назначения</param>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="sourcePrefix">Тип данных источника</param>
        /// <param name="source">Источник значения</param>
        public static void IMultiply(Prefix? destinationPrefix, MemoryOperand destination,
                                     Prefix? sourcePrefix, MemoryOperand source)
        {
            Operation("imul", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// imul. Умножить со знаком на значение
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void IMultiply(MemoryOperand destination, MemoryOperand source)
        {
            IMultiply(null, destination, null, source);
        }

        /// <summary>
        /// div. Разделить без знака на значение
        /// </summary>
        /// <param name="divider">Делитель</param>
        public static void Divide(MemoryOperand divider)
        {
            Operation("div", divider);
        }
        /// <summary>
        /// idiv. Разделить со знаком на значение
        /// </summary>
        /// <param name="divider">Делитель</param>
        public static void IDivide(MemoryOperand divider)
        {
            Operation("idiv", divider);
        }

        /// <summary>
        /// Увеличить значение на 1
        /// </summary>
        /// <param name="register">Регистр в котором будет увеличено значение</param>
        public static void Increment(Register register)
        {
            Operation("inc", register);
        }
        /// <summary>
        /// Уменьшить значение на 1
        /// </summary>
        /// <param name="register">Регистр в котором будет уменьшено значение</param>
        public static void Decrement(Register register)
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
        public static void LoadEffectiveAddress(Prefix? destinationPrefix, MemoryOperand destination,
                                                Prefix? sourcePrefix, MemoryOperand source)
        {
            Operation("lea", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// lea. Вычислить настоящий (эффективный) адрес источника и установить его
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void LoadEffectiveAddress(MemoryOperand destination, MemoryOperand source)
        {
            LoadEffectiveAddress(null, destination, null, source);
        }

        /// <summary>
        /// Поместить значение регистра в стек
        /// </summary>
        /// <param name="register">Регистр, значение которого будет помещено в стек</param>
        public static void Push(Register register)
        {
            Operation("push", register);
        }
        /// <summary>
        /// Извлечь значение регистра из стека
        /// </summary>
        /// <param name="register">Регистр, значение которого будет извлечено из стека</param>
        public static void Pop(Register register)
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
        public static void ShiftLeft(Prefix? destinationPrefix, MemoryOperand destination,
                                     Prefix? sourcePrefix, MemoryOperand source)
        {
            CountOperation("shl", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// shl. Сдвинуть влево
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void ShiftLeft(MemoryOperand destination, MemoryOperand source)
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
        public static void ShiftRight(Prefix? destinationPrefix, MemoryOperand destination,
                                      Prefix? sourcePrefix, MemoryOperand source)
        {
            CountOperation("shr", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// shr. Сдвинуть вправо
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void ShiftRight(MemoryOperand destination, MemoryOperand source)
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
        public static void ShiftArithmeticLeft(Prefix? destinationPrefix, MemoryOperand destination,
                                               Prefix? sourcePrefix, MemoryOperand source)
        {
            CountOperation("sal", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// sal. Арифметически сдвинуть влево
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void ShiftArithmeticLeft(MemoryOperand destination, MemoryOperand source)
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
        public static void ShiftArithmeticRight(Prefix? destinationPrefix, MemoryOperand destination,
                                                Prefix? sourcePrefix, MemoryOperand source)
        {
            CountOperation("sar", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// sar. Арифметически сдвинуть вправо
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void ShiftArithmeticRight(MemoryOperand destination, MemoryOperand source)
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
        public static void RotateLeft(Prefix? destinationPrefix, MemoryOperand destination,
                                      Prefix? sourcePrefix, MemoryOperand source)
        {
            CountOperation("rol", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// rol. Циклически сдвинуть влево.
        /// Сдвигает все биты операнда влево, но вышедшие за пределы старшие биты возвращаются на место младших битов (циклически)
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void RotateLeft(MemoryOperand destination, MemoryOperand source)
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
        public static void RotateRight(Prefix? destinationPrefix, MemoryOperand destination,
                                       Prefix? sourcePrefix, MemoryOperand source)
        {
            CountOperation("ror", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// ror. Циклически сдвинуть вправо. 
        /// Сдвигает все биты операнда вправо, но вышедшие за пределы старшие биты возвращаются на место младших битов (циклически)
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void RotateRight(MemoryOperand destination, MemoryOperand source)
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
        public static void RotateCarryLeft(Prefix? destinationPrefix, MemoryOperand destination,
                                           Prefix? sourcePrefix, MemoryOperand source)
        {
            CountOperation("rcl", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// rcl. Циклически сдвинуть влево через флаг переноса CF.
        /// Сдвигает все биты операнда влево, но старший бит перемещается в флаг переноса (CF), а значение CF перемещается на место младшего бита
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void RotateCarryLeft(MemoryOperand destination, MemoryOperand source)
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
        public static void RotateCarryRight(Prefix? destinationPrefix, MemoryOperand destination,
                                            Prefix? sourcePrefix, MemoryOperand source)
        {
            CountOperation("rcr", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// rcr. Циклически сдвинуть вправо через флаг переноса CF.
        /// Сдвигает все биты операнда вправо, но старший бит перемещается в флаг переноса (CF), а значение CF перемещается на место младшего бита
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void RotateCarryRight(MemoryOperand destination, MemoryOperand source)
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
        public static void And(Prefix? destinationPrefix, MemoryOperand destination,
                               Prefix? sourcePrefix, MemoryOperand source)
        {
            Operation("and", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// and. Логическая операция И
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void And(MemoryOperand destination, MemoryOperand source)
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
        public static void Or(Prefix? destinationPrefix, MemoryOperand destination,
                              Prefix? sourcePrefix, MemoryOperand source)
        {
            Operation("or", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// or. Логическая операция ИЛИ
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void Or(MemoryOperand destination, MemoryOperand source)
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
        public static void Xor(Prefix? destinationPrefix, MemoryOperand destination,
                               Prefix? sourcePrefix, MemoryOperand source)
        {
            Operation("xor", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// xor. Логическая операция исключающее ИЛИ
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void Xor(MemoryOperand destination, MemoryOperand source)
        {
            Xor(null, destination, null, source);
        }

        /// <summary>
        /// not. Логическая операция НЕ. Инвертирует все биты
        /// </summary>
        /// <param name="operand"></param>
        public static void Not(MemoryOperand operand)
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
        public static void Test(Prefix? destinationPrefix, MemoryOperand destination,
                                Prefix? sourcePrefix, MemoryOperand source)
        {
            Operation("test", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// test. Побитовое сравнение (AND без сохранения результата)
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void Test(MemoryOperand destination, MemoryOperand source)
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
        public static void Compare(Prefix? destinationPrefix, MemoryOperand destination,
                                   Prefix? sourcePrefix, MemoryOperand source)
        {
            Operation("cmp", destinationPrefix, destination, sourcePrefix, source);
        }
        /// <summary>
        /// cmp. Проверить равны ли значения
        /// </summary>
        /// <param name="destination">Адрес или регистр в котором будет изменено значение</param>
        /// <param name="source">Источник значения</param>
        public static void Compare(MemoryOperand destination, MemoryOperand source)
        {
            Compare(null, destination, null, source);
        }

        #endregion

        #region Дополнительно

        private static void CountOperation(string opCode, Prefix? destinationPrefix, MemoryOperand destination,
                                                   Prefix? sourcePrefix, MemoryOperand source)
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
        private static void Operation(string opCode, Prefix? destinationPrefix, MemoryOperand destination,
                                                     Prefix? sourcePrefix, MemoryOperand source)
        {
            string line = opCode;
            line = AddData(line, destinationPrefix, destination) + ",";
            line = AddData(line, sourcePrefix, source);

            Emit(line);
        }
        private static void Operation(string opCode, MemoryOperand register)
        {
            Emit($"{opCode} {register}");
        }

        private static string AddData(string line, Prefix? prefix, MemoryOperand operand)
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
