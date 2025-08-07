using Acly.Assembler.Contexts;
using Acly.Assembler.Interruptions;
using Acly.Assembler.Registers;

namespace Acly.Assembler.Memory
{
    /// <summary>
    /// Класс с методами для работы с видеопамятью
    /// </summary>
    public static class VideoMemory
    {
        /// <summary>
        /// Адрес начала видеопамяти
        /// </summary>
        public static MemoryOperand Address { get; } = 0xB8000;

        /// <summary>
        /// Вывести символ на экран
        /// </summary>
        /// <param name="symbol">Символ для вывода на экран</param>
        /// <param name="color">Цвет символа</param>
        /// <param name="x">Положение символа по оси X</param>
        /// <param name="y">Положение символа по оси Y</param>
        /// <param name="width">Ширина экрана</param>
        public static void PrintChar(char symbol, int x, int y, int width, ColorBuilder color)
        {
            RealMode.Accumulator.Higher.Set(color);
            RealMode.Accumulator.Lower.Set(symbol);

            MemoryOperand memory = MemoryOperand.Create(RealMode.ExtraSegment, Address, null, x * 2 + (width * y), 1);
            memory.Set(RealMode.Accumulator);
        }
        /// <summary>
        /// Вывести строку на экран. Этот метод генерирует очень много кода!
        /// </summary>
        /// <param name="value">Строка, которую надо вывести на экран</param>
        /// <param name="color">Цвет текста</param>
        /// <param name="x">Положение текста по оси X</param>
        /// <param name="y">Положение текста по оси Y</param>
        /// <param name="width">Ширина экрана</param>
        public static void PrintString(string value, byte x, byte y, byte width, ColorBuilder color)
        {
            for (int i = 0; i < value.Length; i++)
            {
                PrintChar(value[i], x + i, y, width, color);
            }
        }
        /// <summary>
        /// Заполнить экран цветом и символом (текстовый режим)
        /// </summary>
        /// <param name="value">Цвет и символ</param>
        /// <param name="size">Размер. Здесь должно быть указано значение Ширина * Высота</param>
        public static void Fill(MemoryOperand value, MemoryOperand size)
        {
            RealModeContext.Instance.Accumulator.Set(value);
            RealModeContext.Instance.Count.Set(size);
            ProtectedModeContext.Instance.DestinationIndex.Set(Address);
            Asm.RepeatStoreWord();
        }
        /// <summary>
        /// Задать позицию курсора (текстовый режим)
        /// </summary>
        /// <param name="x">Позиция курсора по оси X</param>
        /// <param name="y">Позиция курсора по оси Y</param>
        /// <param name="width">Ширина экрана</param>
        public static void SetCursorPosition(MemoryOperand x, MemoryOperand y, MemoryOperand width)
        {
            RealMode.Accumulator.Push();
            RealMode.Base.Push();
            RealMode.Data.Push();

            RealMode.Accumulator.Set(y);
            RealMode.Base.Set(width);
            RealMode.Base.Multiply();
            RealMode.Data.Add(x);
            RealMode.Base.Set(RealMode.Data);

            SetCursorByte(RealMode.Base.Higher, 0x0E);
            SetCursorByte(RealMode.Base.Lower, 0x0F);

            RealMode.Accumulator.Pop();
            RealMode.Base.Pop();
            RealMode.Data.Pop();
        }

        private static void SetCursorByte(MemoryOperand cursorByte, MemoryOperand register)
        {
            RealMode.Accumulator.Lower.Set(register);
            RealMode.Data.Set(0x3D4);
            RealMode.Data.Output(RealMode.Accumulator.Lower);
            RealMode.Accumulator.Lower.Set(cursorByte);
            RealMode.Data.Set(0x3D5);
            RealMode.Data.Output(RealMode.Accumulator.Lower);
        }

        #region Статика

        private static RealModeContext RealMode => RealModeContext.Instance;

        #endregion
    }
}
