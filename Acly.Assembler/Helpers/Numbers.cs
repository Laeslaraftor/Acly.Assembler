using Acly.Assembler.Contexts;
using Acly.Assembler.Interruptions;
using Acly.Assembler.Registers;

namespace Acly.Assembler
{
    /// <summary>
    /// Статический класс с методами для упрощения работы с числами
    /// </summary>
    public static class Numbers
    {
        private static int _printCounter;

        /// <summary>
        /// Получить строковой вариант числа
        /// </summary>
        /// <param name="number">Число, которое надо конвертировать строку</param>
        /// <param name="buffer">Буфер, в который будет записана строка</param>
        public static void NumberToString(MemoryOperand number, MemoryOperand buffer)
        {
            Asm.Context.Data.Set(number);
            Asm.Context.SourceIndex.Set(buffer);
            Asm.PushAll();
            Asm.Call(NumberConverterCodeGenerator.Instance.FunctionName);
            Asm.PopAll();

            Asm.Add(NumberConverterCodeGenerator.Instance, true);
        }
        /// <summary>
        /// Вывести число на экран
        /// </summary>
        /// <param name="number">Число, которое надо вывести на экран</param>
        /// <returns>Буфер в котором будет хранится строковой вариант числа</returns>
        public static ArrayVariable PrintBios(MemoryOperand number)
        {
            var buffer = Asm.CreateArrayVariable($"number_buffer_{_printCounter}", Size.x16);
            buffer.Length = 7;

            NumberToString(number, buffer);
            Ints.BIOS.Video.PrintString(buffer);

            _printCounter++;

            return buffer;
        }

        #region Классы

        private class NumberConverterCodeGenerator : ICodeGenerator
        {
            public NumberConverterCodeGenerator(string functionName)
            {
                FunctionName = functionName;
            }

            public string FunctionName { get; }

            public GeneratedCode GenerateCode()
            {
                string loopLabel = $".{FunctionName}_loop";
                string writeLabel = $".{FunctionName}_write";

                Asm.Label(FunctionName);
                Context.DestinationIndex.Set(Asm.Context.SourceIndex);
                Context.Accumulator.Set(Context.Data);
                Context.Base.Set(10);
                Context.Count.Xor(Context.Count);
                Context.Accumulator.EqualsZero();
                Asm.JumpIfNotSign(loopLabel);
                Context.Accumulator.Negative();
                Asm.Context.SourceIndex.Set(Prefix.Byte, true, '-');
                Context.SourceIndex.Increment();

                Asm.Label(loopLabel, false);
                Context.Data.Xor(Context.Data);
                Context.Base.Divide();
                Context.Data.Push();
                Context.Count.Increment();
                Context.Accumulator.EqualsZero();
                Asm.JumpIfNotZero(loopLabel);

                Asm.Label(writeLabel, false);
                Context.Data.Pop();
                Context.Data.Lower.Add('0');
                Context.SourceIndex.Set(null, true, Context.Data.Lower);
                Context.SourceIndex.Increment();
                Asm.Loop(writeLabel);
                Context.SourceIndex.Set(Prefix.Byte, true, 0);
                Context.SourceIndex.Set(Context.DestinationIndex);
                Asm.Return();

                return new(Section.Text, string.Empty);
            }

            #region Статика

            public static NumberConverterCodeGenerator Instance { get; } = new("number_to_string");

            private static CpuModeContext Context => Asm.Context;

            #endregion
        }

        #endregion
    }
}
