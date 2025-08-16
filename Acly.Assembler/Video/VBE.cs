using Acly.Assembler.Contexts;
using Acly.Assembler.Interruptions;
using Acly.Assembler.Registers;

namespace Acly.Assembler.Video
{
    /// <summary>
    /// Статический класс с методами для работы с VBE (VESA BIOS Extensions)
    /// </summary>
    public static class VBE
    {
        private static RealModeContext RealMode => RealModeContext.Instance;
        private static ProtectedModeContext ProtectedMode => ProtectedModeContext.Instance;

        /// <summary>
        /// Получить VBE информацию
        /// </summary>
        /// <param name="errorHandler">Обработчик ошибки. Будет вызван если не удастся получить VBE информацию</param>
        /// <returns>Структура с VBE информацией</returns>
        public static VbeInfoStructure GetInfo(MemoryOperand? errorHandler = null)
        {
            VbeInfoStructure vbeInfo = new();

            GetInfo(vbeInfo, errorHandler);

            return vbeInfo;
        }
        /// <summary>
        /// Получить VBE информацию
        /// </summary>
        /// <param name="vbeInfoStructure">VBE структура, которая будет заполнена информацией</param>
        /// <param name="errorHandler">Обработчик ошибки. Будет вызван если не удастся получить VBE информацию</param>
        public static void GetInfo(VbeInfoStructure vbeInfoStructure, MemoryOperand? errorHandler = null)
        {
            RealMode.Accumulator.Set(0x4F00);
            RealMode.DestinationIndex.Set(vbeInfoStructure.Name);
            Asm.Interrupt(Ints.BIOS.Video);

            if (errorHandler != null)
            {
                RealMode.Accumulator.Compare(0x004F);
                Asm.JumpIfNotEquals(errorHandler);
            }

            Asm.Add(vbeInfoStructure, true);
        }

        /// <summary>
        /// Найти и включить наилучший графический режим
        /// </summary>
        /// <param name="vbeInfoStructure">VBE информация</param>
        /// <param name="errorHandler">Обработчик ошибки. Будет вызван если не удастся найти наилучший графический режим</param>
        /// <returns>Структура с информацией о найденом графическом режиме</returns>
        public static VbeModeInfoBlockStructure EnableBestGraphicMode(VbeInfoStructure vbeInfoStructure, MemoryOperand? errorHandler = null)
        {
            VbeModeInfoBlockStructure result = new($"{vbeInfoStructure.Name}_mode_info");
            BestVbeModeFinderCodeGenerator finderCodeGenerator = new(vbeInfoStructure, result, errorHandler);

            Asm.Call(finderCodeGenerator.FunctionName);

            //if (errorHandler != null)
            //{
            //    RealMode.Accumulator.EqualsZero(null, false, RealMode.Accumulator);
            //    Asm.JumpIfZero(errorHandler);
            //}

            Numbers.PrintBios(MemoryOperand.Create(result.Width, true));

            //RealMode.Base.Set(RealMode.Accumulator);
            //RealMode.Base.Or(0x4000);
            //RealMode.Accumulator.Set(0x4F02);
            //Asm.Interrupt(Ints.BIOS.Video);

            //if (errorHandler != null)
            //{
            //    RealMode.Accumulator.Compare(0x004F);
            //    Asm.JumpIfNotEquals(errorHandler);
            //}

            Asm.Add(result, true);
            Asm.Add(finderCodeGenerator, true);

            return result;
        }
        /// <summary>
        /// Включить графический режим 13h (320x200 256 цветов)
        /// </summary>
        public static void EnableGraphics()
        {
            RealMode.Data.Set(0x03C2);
            RealMode.Accumulator.Lower.Set(0x63);
            RealMode.Data.Output(RealMode.Accumulator.Lower);
        }
        /// <summary>
        /// Заполнить пиксель
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        public static void DrawPixel(int x, int y, MemoryOperand color)
        {
            MemoryOperand.Create(0xA0000, y * 200 + x).Set(Prefix.Byte, false, RealMode.Accumulator.Lower);
        }
        /// <summary>
        /// Заполнить прямоугольник
        /// </summary>
        /// <param name="x">Позиция по оси X</param>
        /// <param name="y">Позиция по оси Y</param>
        /// <param name="width">Ширина прямоугольника</param>
        /// <param name="height">Высота прямоугольника</param>
        /// <param name="color">Цвет заливки</param>
        public static void DrawRect(int x, int y, int width, int height, MemoryOperand color)
        {
            for (int xPosition = x; xPosition < x + width; xPosition++)
            {
                for (int yPosition = y; yPosition < y + height; yPosition++)
                {
                    DrawPixel(xPosition, yPosition, color);
                }
            }
        }
        /// <summary>
        /// Очистить экран
        /// </summary>
        /// <param name="color">Цвет которым будет заполнен экран</param>
        public static void ClearScreen(MemoryOperand color)
        {
            Asm.Context.Data.Set(0xA0000);
            Asm.Context.Count.Set(320 * 200);
            RealMode.Accumulator.Lower.Set(color);
            Asm.RepeatStoreByte();
        }

        #region Классы

        private class BestVbeModeFinderCodeGenerator : ICodeGenerator
        {
            public BestVbeModeFinderCodeGenerator(VbeInfoStructure vbeInfoStructure, VbeModeInfoBlockStructure vbeModeStructure, MemoryOperand? errorHandler)
            {
                VbeInfo = vbeInfoStructure;
                VbeMode = vbeModeStructure;
                ErrorHandler = errorHandler;
                FunctionName = $"{vbeInfoStructure.Name}_find_best_vbe_mode";
            }

            public VbeInfoStructure VbeInfo { get; }
            public VbeModeInfoBlockStructure VbeMode { get; }
            public MemoryOperand? ErrorHandler { get; }
            public string FunctionName { get; }

            public GeneratedCode GenerateCode()
            {
                string checkModeLabel = $".{VbeInfo.Name}_check_mode_loop";
                string nextModeLabel = $".{VbeInfo.Name}_next_mode";
                string doneLabel = $".{VbeInfo.Name}_done";

                Asm.Label(FunctionName);
                Asm.Comment("Поиск наилучшего VBE режима");
                Asm.Comment("Возвращает: AX = номер режима (0 если не найден)");

                RealMode.ExtraSegment.Push();
                RealMode.SourceIndex.Push();
                RealMode.DestinationIndex.Push();
                RealMode.BasePointer.Push();

                Asm.EmptyLine();

                RealMode.SourceIndex.Set(MemoryOperand.Create(VbeInfo.VideoModePointer, true));
                RealMode.Accumulator.Set(MemoryOperand.Create(VbeInfo.VideoModePointer, 2));
                RealMode.FrameSegment.Set(RealMode.Accumulator);

                Asm.EmptyLine();

                RealMode.Base.Xor(RealMode.Base);
                ProtectedMode.Count.Set(0);

                Asm.Label(checkModeLabel);
                RealMode.Data.Set(MemoryOperand.Create(RealMode.FrameSegment, RealMode.SourceIndex, null));
                RealMode.Data.Compare(0xFFFF);
                Asm.JumpIfEquals(doneLabel);

                Asm.EmptyLine();

                RealMode.Accumulator.Set(0x4F01);
                RealMode.DestinationIndex.Set(VbeMode.Name);
                Asm.Interrupt(Ints.BIOS.Video);
                RealMode.Accumulator.Compare(0x004F);
                Asm.JumpIfNotEquals(nextModeLabel);

                Asm.EmptyLine();

                MemoryOperand.Create(VbeMode.Attributes).EqualsZero(Prefix.Word, true, 0x90);
                Asm.JumpIfZero(nextModeLabel);

                Asm.EmptyLine();

                RealMode.Accumulator.Lower.Set(MemoryOperand.Create(VbeMode.BitsPerPixel, true));
                RealMode.Accumulator.Lower.Compare(24);
                Asm.JumpIfBelow(nextModeLabel);

                Asm.EmptyLine();

                ProtectedMode.Accumulator.Set(MemoryOperand.Create(VbeMode.Width, true));
                MemoryOperand.Create(VbeMode.Height, true).Multiply(Prefix.Dword);

                Asm.EmptyLine();

                ProtectedMode.Accumulator.Compare(ProtectedMode.Count);
                Asm.JumpIfBelowOrEquals(nextModeLabel);

                Asm.PushAll();

                Asm.PopAll();

                Asm.EmptyLine();

                ProtectedMode.Count.Set(ProtectedMode.Accumulator);
                RealMode.Base.Set(RealMode.Data);

                Asm.Label(nextModeLabel);
                RealMode.SourceIndex.Add(2);
                Asm.Jump(checkModeLabel);

                Asm.Label(doneLabel);
                RealMode.Accumulator.Set(RealMode.Base);
                RealMode.BasePointer.Pop();
                RealMode.DestinationIndex.Pop();
                RealMode.SourceIndex.Pop();
                RealMode.ExtraSegment.Pop();
                Asm.Return();

                return new(Section.Text, string.Empty, Mode.x16);
            }
        }

        #endregion
    }
}
