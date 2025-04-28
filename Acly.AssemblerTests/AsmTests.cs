using Acly.Assembler.Contexts;
using Acly.Assembler.Registers;
using System.Diagnostics;

namespace Acly.Assembler.Tests
{
    [TestClass()]
    public class AsmTests
    {
        [TestMethod()]
        public void SwitchTest()
        {
            var buffer = Asm.CreateArrayVariable("buffer", Size.x16, false);
            buffer.Length = 20;

            Asm.StartWithMode(Mode.x64);

            Asm.Comment($"R8 = 10 (0x{10:X})");
            Asm.Context.R8.Set(10);
            Asm.Comment("R9 = R8");
            Asm.Context.R9.Set(Asm.Context.R8);
            Asm.EmptyLine();

            Asm.Comment("R8 << R9 должно быть: (10 << 10)");
            Asm.Context.R8.ShiftLeft(Asm.Context.R9);

            Asm.EmptyLine();
            Asm.Comment("Вызов функции для отображения числа из R8");
            Asm.Call("print");

            Asm.Label("print");
            Asm.Context.Accumulator.Set(Asm.Context.R8);
            Asm.Context.Data.LoadEffectiveAddress(MemoryOperand.Create(buffer, displacement: 19));
            Asm.Context.Data.Set(Prefix.Byte, true, 10);
            Asm.Context.Data.Decrement();
            Asm.Context.Count.Set(10);

            Asm.Label("convert_loop");
            Asm.Context.Data.Xor(Asm.Context.Data);
            Asm.Context.Count.Divide();
            RealModeContext.Instance.Data.Lower?.Add('0');
            Asm.Context.Data.Decrement();
            Asm.Context.Accumulator.EqualsZero();
            Asm.JumpIfNotEquals("convert_loop");

            Asm.EmptyLine();

            Asm.Context.Data.Increment();
            LongModeContext.Instance.SourceIndex.LoadEffectiveAddress(MemoryOperand.Create(Asm.Context.Accumulator, true));
            Asm.Context.Data.Set(MemoryOperand.Create(buffer, 20));
            Asm.Context.Data.Subtract(LongModeContext.Instance.SourceIndex);
            Asm.Context.Accumulator.Set(1);
            Asm.Context.Data.Set(1);
            Asm.SystemCall();

            Asm.EmptyLine();

            Asm.Context.Accumulator.Set(60);
            Asm.Context.Data.Xor(Asm.Context.Data);
            Asm.SystemCall();

            Debug.WriteLine(Asm.GetAssembly());
        }
    }
}