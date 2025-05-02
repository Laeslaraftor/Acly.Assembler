using Acly.Assembler.Registers;

namespace Acly.Assembler.Interruptions
{
    /// <summary>
    /// Прерывание BIOS для работы с видеосистемой
    /// </summary>
    public class BiosVideoInterruption : BiosInterruption
    {
        /// <summary>
        /// Создать новый экземпляр прерывания BIOS для работы с видеосистемой
        /// </summary>
        public BiosVideoInterruption() : base(0x10, "Прерывание BIOS для работы с видеосистемой")
        {
        }

        #region Управление

        /// <summary>
        /// Установить видеорежим
        /// </summary>
        /// <param name="mode">Видеорежим, который необходимо установить</param>
        public void SetVideoMode(VideoMode mode)
        {
            RealMode.Accumulator.Lower.Set((byte)mode);
            PerformInterruption((byte)mode);
        }
        /// <summary>
        /// Вывести символ на экран
        /// </summary>
        /// <param name="value">Символ для вывода на экран.
        /// Если вы уже самостоятельно задали регистр AL, то следует указать NULL
        /// </param>
        /// <param name="page">Номер страницы</param>
        /// <param name="color">Цвет текста (только для графического режима)</param>
        public void PrintChar(MemoryOperand? value, MemoryOperand page, MemoryOperand color)
        {
            if (value != null)
            {
                RealMode.Accumulator.Lower.Set(value);
            }

            RealMode.Base.Higher.Set(page);
            RealMode.Base.Lower.Set(color);

            PerformInterruption(CharOutputFunction);
        }
        /// <summary>
        /// Вывести строку на экран
        /// </summary>
        /// <param name="stringAddress">Адрес строки</param>
        /// <param name="page">Номер страницы</param>
        /// <param name="color">Цвет текста (только для графического режима)</param>
        public void PrintString(MemoryOperand stringAddress, MemoryOperand page, MemoryOperand color)
        {
            _printStringCounter++;
            string printFunctionName = $"print_string_loop{_printStringCounter}";
            string printFunctionContinueName = $".{printFunctionName}_next";
            string printFunctionCompleteName = $".{printFunctionName}_done";

            Asm.Comment($"Вывод строки на экран: {stringAddress}");
            Asm.Context.SourceIndex.Set(stringAddress);
            Asm.Call(printFunctionName);

            Asm.Label(printFunctionName);
            RealMode.Accumulator.Higher.Set(CharOutputFunction);
            RealMode.Base.Higher.Xor(RealMode.Base.Higher);

            Asm.Label(printFunctionContinueName);
            Asm.LoadStringByte();
            RealMode.Accumulator.Lower.EqualsZero();
            Asm.JumpIfZero(printFunctionCompleteName);
            Asm.Interrupt(this);
            Asm.Jump(printFunctionContinueName);

            Asm.Label(printFunctionCompleteName);
            //Asm.Return();
        }
        /// <summary>
        /// Получить позицию курсора на экране
        /// </summary>
        /// <param name="page">Номер страницы</param>
        /// <remarks>В регистр DH будет записан номер строки, а в регистр DL - столбец</remarks>
        public void GetCursorPosition(MemoryOperand page)
        {
            RealMode.Base.Higher.Set(page);
            PerformInterruption(CursorPositionReadingFunction);
        }
        /// <summary>
        /// Задать позицию курсора
        /// </summary>
        /// <param name="page">Номер страницы</param>
        /// <param name="row">Номер строки</param>
        /// <param name="column">Номер столбца</param>
        public void SetCursorPosition(MemoryOperand page, MemoryOperand row, MemoryOperand column)
        {
            Asm.Comment($"Установка позиции курсора [{row}, {column}] на странице {page}");
            RealMode.Base.Higher.Set(page);
            RealMode.Data.Higher.Set(row);
            RealMode.Data.Lower.Set(column);
            PerformInterruption(CursorPositionSettingFunction);
        }
        /// <summary>
        /// Прокрутить экран
        /// </summary>
        /// <param name="direction">Направление прокрутки</param>
        /// <param name="lines">Количество строк для прокрутки.
        /// Если указать 0, то можно очистить экран (наверно)
        /// </param>
        /// <param name="color">Цвет фона</param>
        /// <param name="startRow">Номер начальной строки</param>
        /// <param name="endRow">Номер конечной строки</param>
        /// <param name="startColumn">Номер начального столбца</param>
        /// <param name="endColumn">Номер конечного столбца</param>
        public void Scroll(ScrollDirection direction, MemoryOperand lines, MemoryOperand color, MemoryOperand startRow, MemoryOperand endRow, 
                           MemoryOperand startColumn, MemoryOperand endColumn)
        {
            RealMode.Accumulator.Lower.Set(lines);
            RealMode.Base.Higher.Set(color);
            RealMode.Count.Higher.Set(startRow);
            RealMode.Count.Lower.Set(startColumn);
            RealMode.Data.Higher.Set(endRow);
            RealMode.Data.Lower.Set(endColumn);
            PerformInterruption((byte)direction);
        }
        /// <summary>
        /// Очистить экран с указанными цветом
        /// </summary>
        /// <param name="color">Цвет очистки</param>
        public void ClearScreen(MemoryOperand color)
        {
            Asm.Comment($"Очистка экрана с цветом {color}");
            RealMode.Accumulator.Lower.Set(0);
            RealMode.Base.Higher.Set(color);
            RealMode.Count.Set(0);
            RealMode.Data.Set(0x184F);
            PerformInterruption(ScrollUpFunction);
        }
        /// <summary>
        /// Очистить экран. Будет установлен белый текст на чёрном фоне
        /// </summary>
        public void ClearScreen() => ClearScreen(0x07);
        /// <summary>
        /// Нарисовать пиксель
        /// </summary>
        /// <param name="color">Цвет пикселя</param>
        /// <param name="x">Позиция по оси X</param>
        /// <param name="y">Позиция по оси Y</param>
        public void DrawPixel(MemoryOperand color, MemoryOperand x, MemoryOperand y)
        {
            RealMode.Accumulator.Lower.Set(color);
            RealMode.Count.Higher.Set(x);
            RealMode.Count.Lower.Set(x);
            PerformInterruption(DrawPixelFunction);
        }
        /// <summary>
        /// Получить состояние видеосистемы
        /// </summary>
        /// <remarks>
        /// В регистр AL будет записан текущий видеорежим, 
        /// в AH - количество столбцов (ширина), 
        /// в BH - текущая страница
        /// </remarks>
        public void GetStatus()
        {
            PerformInterruption(VideoSystemStatusReadingFunction);
        }

        #endregion

        #region Константы

        /// <summary>
        /// Номер функции установки видеорежима
        /// </summary>
        public const byte VideoModeSettingFunction = 0x00;
        /// <summary>
        /// Номер функции вывода символа на экран (телетайпный режим)
        /// </summary>
        public const byte CharOutputFunction = 0x0E;
        /// <summary>
        /// Номер функции чтения позиции курсора
        /// </summary>
        public const byte CursorPositionReadingFunction = 0x03;
        /// <summary>
        /// Номер функции установки позиции курсора
        /// </summary>
        public const byte CursorPositionSettingFunction = 0x02;
        /// <summary>
        /// Номер функции прокрутки экрана вверх
        /// </summary>
        public const byte ScrollUpFunction = 0x06;
        /// <summary>
        /// Номер функции прокрутки экрана вниз
        /// </summary>
        public const byte ScrollDownFunction = 0x07;
        /// <summary>
        /// Номер функции рисования пикселя
        /// </summary>
        public const byte DrawPixelFunction = 0x0C;
        /// <summary>
        /// Номер функции чтения состояния видеосистемы
        /// </summary>
        public const byte VideoSystemStatusReadingFunction = 0x0F;

        #endregion

        #region Статика

        private static int _printStringCounter;

        #endregion
    }
}
